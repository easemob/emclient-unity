#include <thread>
#include "client.h"
#include "chat_manager.h"

#include "emlogininfo.h"
#include "emchatconfigs.h"
#include "emchatprivateconfigs.h"
#include "emmultidevices_listener.h"
#include "emclient.h"
#include "contact_manager.h"
#include "group_manager.h"
#include "room_manager.h"
#include "tool.h"

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

using namespace easemob;

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
}

static bool G_DEBUG_MODE = false;
static bool G_AUTO_LOGIN = true;
static bool G_LOGIN_STATUS = false;

static bool NeedAllocResource = false;

struct AutoLoginConfig {
    std::string userName;
    std::string passwd;
    std::string token;
    std::string expireTS; // milli-second
    int64_t expireTsInt; //second
    int64_t availablePeriod; //second
};

AutoLoginConfig global_autologin_config;

EMClient *gClient = nullptr;
EMConnectionListener *gConnectionListener = nullptr;
EMConnectionCallbackListener *gConnectionCallbackListener = nullptr;
EMMultiDevicesListener *gMultiDevicesListener = nullptr;
EMChatConfigsPtr configs;

bool SetTokenInAutoLogin(const std::string& username, const std::string& token, const std::string expireTS) {

    if (username.size() == 0 || token.size() == 0) {
        LOG("Error: SetTokenInAutoLogin failed due to empty username or token!");
        return false;
    }

    if (expireTS.size() > 0) {

        int64_t expireTsInt = atoll(expireTS.c_str()); // milli-second
        expireTsInt = expireTsInt / 1000; // second

        time_t nowTS = time(NULL);

        int64_t availablePeriod = expireTsInt - nowTS;
        // no available time or expireTs is same with last time, then no need to update related fields
        if (availablePeriod <= 0 ) {
            LOG("Error: SetTokenInAutoLogin failed due to expired expireTS! nowTS:%ld, expireTS:%ld", nowTS, expireTsInt);
            return false;
        }

        //expireTs is same with last time, then no need to update related fields
        if (global_autologin_config.expireTsInt == expireTsInt) {
            LOG("SetTokenInAutoLogin failed due to same expireTS!");
            return false;
        }

        global_autologin_config.expireTS = expireTS;
        global_autologin_config.expireTsInt = expireTsInt;
        global_autologin_config.availablePeriod = availablePeriod;
    } 
    else {
        // Note: if expireTS is empty, but token is not, means
        // current using easemob token to login, not agora token!
        global_autologin_config.expireTS = "";
        global_autologin_config.expireTsInt = 0;
        global_autologin_config.availablePeriod = 0;
    }

    global_autologin_config.userName = username;
    global_autologin_config.passwd = "";
    global_autologin_config.token = token;
    return true;
}

void SetPasswdInAutoLogin(const std::string& username, const std::string& passwd) {
    global_autologin_config.userName = username;
    global_autologin_config.passwd = passwd;
    global_autologin_config.token = "";
    global_autologin_config.expireTS = "";
    global_autologin_config.expireTsInt = 0;
    global_autologin_config.availablePeriod = 0;
}

bool GetTokenCofigFromJson(std::string& raw, std::string& token, std::string& expireTS)
{
    const std::string ACCESS_TOKEN = "access_token";
    const std::string EXPIRE_TS = "expire_timestamp";
    
    //std::string expire_in;
    
    int64_t expireTS_int = 0;
    
    if (raw.length() < 3) {
        LOG("Input parameter error.");
        return false;
    }
    
    Document d;
    if (d.Parse(raw.c_str()).HasParseError()) {
        LOG("Parse json failed, raw:%s", raw.c_str());
        return false;
    }
    
    if (d.HasMember(ACCESS_TOKEN.c_str())) {
        token = d[ACCESS_TOKEN.c_str()].GetString();
    }
    else {
        LOG("Error:No token exist in response from server.");
        return false;
    }
    
    if (d.HasMember(EXPIRE_TS.c_str())) {
        expireTS_int = d[EXPIRE_TS.c_str()].GetInt64();
        expireTS = std::to_string(expireTS_int);
    }
    else {
        LOG("Error:No token expire-timestamp in response from server.");
        return false;
    }
    
    //LOG("Token from server:%s, expireTS:%s", token.c_str(), expireTS.c_str());
    LOG("GetTokenCofigFromJson expireTS:%s", expireTS.c_str());
    
    return true;
}

int TOKEN_CHECK_INTERVAL = 180; // 180s
int GetTokenCheckInterval(int availablePeriod)
{
    if (TOKEN_CHECK_INTERVAL > floor(availablePeriod/2)) {
        TOKEN_CHECK_INTERVAL = floor(availablePeriod/3);
    }
    
    return TOKEN_CHECK_INTERVAL;
}

void TokenCheck(int signo)
{
    // Note: No need mutex for locking global_autologin_config。
    // Since if timer is running, means still in login status.
    // global_autologin_config noly can be changed from logout status
    // to login status.
    
    time_t nowTS = time(NULL);
    int64_t remainTS = global_autologin_config.expireTsInt - nowTS;
    
    if (remainTS <= 0) { // expired
        
        LOG("Token was expired.");
        EMErrorPtr error(new EMError(EMError::TOKEN_EXPIRED));
        error->mDescription = "Token was expired.";
     
        StopTimer();
        gConnectionListener->onTokenNotification(error);
        Client_Logout(gClient, -1, NULL, false);
    }
    else { // Not expired
        
        if (remainTS < ceil(global_autologin_config.availablePeriod/2)) { // will expire
            LOG("Token will expire. remain: (%d) secs.", remainTS);
            EMErrorPtr error(new EMError(EMError::TOKEN_WILL_EXPIRE));
            error->mDescription = "Token will expire after ";
            error->mDescription.append(std::to_string((int)remainTS));
            error->mDescription.append(" seconds.");
            gConnectionListener->onTokenNotification(error);
            
            // reset timer trigger point and only trigger once
            // so no need to stop timer when expired!!
            if (abs(TOKEN_CHECK_INTERVAL - remainTS) <= 3 ||
                TOKEN_CHECK_INTERVAL > remainTS) {
                TOKEN_CHECK_INTERVAL = (int)remainTS + 2;
                StartTimer(TOKEN_CHECK_INTERVAL, TokenCheck);
                LOG("Change timer with interval %d", TOKEN_CHECK_INTERVAL);
            }
            
        } else { // still has enough time before expire
            LOG("Token is still valid. remain: (%d) secs.", remainTS);
        }
    }
}


void SaveAutoLoginConfigToFile()
{
    // merge token config
    std::string mergeStr = "userName";
    mergeStr.append("=");
    mergeStr.append(global_autologin_config.userName);
    mergeStr.append("\n");
    
    mergeStr.append("passwd");
    mergeStr.append("=");
    mergeStr.append(global_autologin_config.passwd);
    mergeStr.append("\n");
    
    mergeStr.append("token");
    mergeStr.append("=");
    mergeStr.append(global_autologin_config.token);
    mergeStr.append("\n");
    
    mergeStr.append("expireTS");
    mergeStr.append("=");
    mergeStr.append(global_autologin_config.expireTS);
    
    EncryptAndSaveToFile(mergeStr, configs->deviceUuid());
}

void GetAutoLoginConfigFromFile()
{
    std::string mergeStr = DecryptAndGetFromFile(configs->deviceUuid());
    
    // mergeStr has the format: aaa\nbbb\nccc
    std::string::size_type pos, pos1;
    
    std::string userName = "";
    pos = mergeStr.find("\n");
    if (std::string::npos != pos) {
        std::string str = std::string(mergeStr, 0, pos - 0);
        userName = GetRightValue(str);
    } else {
        LOG("Error: bad format of decrypted autologin config");
        return;
    }
 
    std::string passwd = "";
    pos = pos + 1;
    pos1 = mergeStr.find("\n", pos);
    if (std::string::npos != pos1) {
        std::string str = std::string(mergeStr, pos, pos1 - pos);
        passwd = GetRightValue(str);
    } else {
        LOG("Error: bad format of decrypted autologin config");
        return;
    }
    
    std::string token = "";
    pos = pos1 + 1;
    pos1 = mergeStr.find("\n", pos);
    if (std::string::npos != pos1) {
        std::string str = std::string(mergeStr, pos, pos1 - pos);
        token = GetRightValue(str);
    } else {
        LOG("Error: bad format of decrypted autologin config");
        return;
    }
    
    pos = pos1 + 1;
    if (pos > mergeStr.size() - 1) {
        LOG("Error: bad format of decrypted autologin config");
        return;
    }
    
    std::string expireTS = "";
    std::string str = std::string(mergeStr, pos, mergeStr.size() - pos);
    expireTS = GetRightValue(str);
    
    global_autologin_config.userName = userName;
    global_autologin_config.passwd = passwd;
    global_autologin_config.token = token;
    global_autologin_config.expireTS = expireTS;
    global_autologin_config.expireTsInt = 0;
    global_autologin_config.availablePeriod = 0;
    
    if (global_autologin_config.expireTS.size() > 0) {
        time_t nowTS = time(NULL); // second
        int64_t expireTsInt = atoll(global_autologin_config.expireTS.c_str()); // milli-second
        
        global_autologin_config.expireTsInt = (int)(expireTsInt/1000); // second
        global_autologin_config.availablePeriod = global_autologin_config.expireTsInt - nowTS;
    }
    
    //TODO: need to remove, just for testing
    LOG("Fromfile, u:%s, p:%s, t:%s, e:%s, ei:%ld, ai:%ld",
        global_autologin_config.userName.c_str(),
        global_autologin_config.passwd.c_str(),
        global_autologin_config.token.c_str(),
        global_autologin_config.expireTS.c_str(),
        global_autologin_config.expireTsInt,
        global_autologin_config.availablePeriod);
}

HYPHENATE_API void Client_CreateAccount(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *password)
{
    std::string usernameStr = username;
    std::string passwordStr = password;
    
    LOG("Client_CreateAccount() called with username=%s password=%s", usernameStr.c_str(), passwordStr.c_str());
    std::thread t([=](){
        EMErrorPtr result = CLIENT->createAccount(usernameStr, passwordStr);
        
        if(EMError::isNoError(result)) {
            LOG("Account creation succeeds!");
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("Account creation failed!");
            if(onError) onError(result->mErrorCode, result->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

std::string ManagleAppKey(const char* appKey) {
    std::string ret = "";
    if (nullptr == appKey || strlen(appKey) == 0)
        return ret;
    for (int i=0; i <=strlen(appKey)-1; i++) {
        if (i % 2 == 0) 
            ret.append(1, appKey[i]);
        else
            ret.append(".");
    }
    return ret;
}

EMChatConfigsPtr ConfigsFromOptions(Options *options) {
    const char *appKey = options->AppKey;

    string ak = ManagleAppKey(appKey);
    LOG("Client_InitWithOptions() called with AppKey=%s", ak.c_str());

    configs = EMChatConfigsPtr(new EMChatConfigs("./sdkdata","./sdkdata",appKey,0));
    //TODO: non null-ptr assertion
    const char *dnsURL = options->DNSURL;
    const char *imServer = options->IMServer;
    const char *restServer = options->RestServer;
    int imPort = options->IMPort;
    bool enableDNSConfig = options->EnableDNSConfig;
    LOG("Options with DNSURL=%s, IMServer=%s, IMPort=%d, RestServer=%s, EnableDNSConfig=%s", dnsURL, imServer, imPort, restServer, enableDNSConfig ? "true" : "false");
    configs->setDnsURL(options->DNSURL);
    configs->privateConfigs()->chatServer() = options->IMServer;
    configs->privateConfigs()->restServer() = options->RestServer;
    configs->privateConfigs()->chatPort() = options->IMPort;
    configs->privateConfigs()->enableDnsConfig(options->EnableDNSConfig);
    configs->setAutoAcceptFriend(options->AcceptInvitationAlways);
    configs->setAutoAcceptGroup(options->AutoAcceptGroupInvitation);
    configs->setRequireReadAck(options->RequireAck);
    configs->setRequireDeliveryAck(options->RequireDeliveryAck);
    configs->setDeleteMessageAsExitGroup(options->DeleteMessagesAsExitGroup);
    configs->setDeleteMessageAsExitChatRoom(options->DeleteMessagesAsExitRoom);
    configs->setIsChatroomOwnerLeaveAllowed(options->IsRoomOwnerLeaveAllowed);
    configs->setSortMessageByServerTime(options->SortMessageByServerTime);
    configs->setUsingHttps(options->UsingHttpsOnly);
    configs->setTransferAttachments(options->ServerTransfer);
    configs->setAutoDownloadThumbnail(options->IsAutoDownload);
    //configs->setLogPath("/tmp/sdk.log");
    //configs->setEnableConsoleLog(true);
#ifndef _WIN32
    std::string uuid = GetMacUuid();
    if (uuid.size() > 0)
        configs->setDeviceUuid(uuid);
#endif
    return configs;
}

HYPHENATE_API void* Client_InitWithOptions(Options *options, FUNC_OnConnected onConnected,
                                           FUNC_OnDisconnected onDisconnected, FUNC_OnPong onPong,
                                           FUNC_onTokenNotification onTokenNotification)
{
    // global switch
    G_DEBUG_MODE = options->DebugMode;
    G_AUTO_LOGIN = options->AutoLogin;
    
    // singleton client handle
    if(nullptr == gClient) {
        EMChatConfigsPtr configs = ConfigsFromOptions(options);
        gClient = EMClient::create(configs);
        LOG("Emclient created.");
    } else {
        if(NeedAllocResource) {
            gClient->allocResource();
            NeedAllocResource = false;
            LOG("Alloc sdk resource.");
        }
    }
    
    if(nullptr == gConnectionListener) { //only set once
        gConnectionListener = new ConnectionListener(onConnected, onDisconnected, onPong, onTokenNotification);
        gClient->addConnectionListener(gConnectionListener);
        LOG("New connection listener and hook it.");
    }
    
    if(nullptr == gConnectionCallbackListener) { //only set once
        gConnectionCallbackListener = new ConnectionCallbackListener();
        gClient->addConnectionCallbackListener(gConnectionCallbackListener);
        LOG("New connection callback listener and hook it.");
    }
    
    return gClient;
}

// Must be called afer Client_InitWithOptions!
HYPHENATE_API void Client_AddMultiDeviceListener(FUNC_onContactMultiDevicesEvent contactEventFunc,
                                                 FUNC_onGroupMultiDevicesEvent groupEventFunc,
                                                 FUNC_undisturbMultiDevicesEvent undisturbEventFunc)
{
    if(nullptr == gMultiDevicesListener) { // only set once
        gMultiDevicesListener = new MultiDevicesListener(contactEventFunc, groupEventFunc, undisturbEventFunc);
        gClient->addMultiDevicesListener(gMultiDevicesListener);
        LOG("New multi device listener and hook it.");
    }
}

HYPHENATE_API void Client_Login(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *pwdOrToken, bool isToken)
{
    if (true == G_LOGIN_STATUS) {
        LOG("Already in login status, no need to login again.");
        return;
    }
    
    std::string usernameStr = username;
    std::string pwdOrTokenStr = pwdOrToken;
    
    LOG("Client_Login() called with username=%s, pwdOrToken=%s, isToken=%d", usernameStr.c_str(), pwdOrTokenStr.c_str(), isToken);
    std::thread t([=](){
        EMErrorPtr result;
        result = isToken ? CLIENT->loginWithToken(usernameStr, pwdOrTokenStr) : CLIENT->login(usernameStr, pwdOrTokenStr);
        
        if(EMError::isNoError(result)) {
            LOG("Login succeeds.");
            G_LOGIN_STATUS = true;
            
            if (isToken)
                SetTokenInAutoLogin(usernameStr, pwdOrTokenStr, "");
            else
                SetPasswdInAutoLogin(usernameStr, pwdOrTokenStr);
            SaveAutoLoginConfigToFile();

            if(onSuccess) onSuccess(callbackId);
            
        }else{
            LOG("Login failed with code=%d desc=%s!", result->mErrorCode, result->mDescription.c_str());
            if(onError) onError(result->mErrorCode, result->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void Client_Logout(void *client, int callbackId, FUNC_OnSuccess onSuccess, bool unbindDeviceToken)
{
    if (false == G_LOGIN_STATUS) {
        LOG("Already in logout status.");
        return;
    }
    
    std::thread t([=](){
        if(G_LOGIN_STATUS) {
            LOG("Execute logout action.");
            CLIENT->logout();
            G_LOGIN_STATUS = false;

            StopTimer();
            if(onSuccess) onSuccess(callbackId);
        } else {
            LOG("Already logout, NO need to execute logout action.");
        }
    });
    t.join();
}

HYPHENATE_API void Client_StartLog(const char *logFilePath) {
    LogHelper::getInstance().startLogService(logFilePath);
}

HYPHENATE_API void Client_StopLog() {
    return LogHelper::getInstance().stopLogService();
}

HYPHENATE_API void Client_LoginToken(void *client, FUNC_OnSuccess_With_Result onSuccess) {
    const EMLoginInfo& loginInfo = CLIENT->getLoginInfo();
    const char* data[1];
    data[0] = loginInfo.loginToken().c_str();
    if(onSuccess) onSuccess((void **)data, DataType::String, 1, -1);
}

HYPHENATE_API bool Client_isConnected(void* client) {
    return CLIENT->isConnected();
}
HYPHENATE_API bool Client_isLoggedIn(void* client) {
    return CLIENT->isLoggedIn();
}

// this function must be executed after logout!!!
HYPHENATE_API void Client_ClearResource(void *client) {
    if(true == G_LOGIN_STATUS) {
        LOG("Still in login status, cannot clear resource.");
        return;
    }
    
    LOG("Clear resource begin--------------");
    
    CLIENT->clearResource();
    
    // set flag for next replay
    NeedAllocResource = true;
    
    // clear all listeners when replay
    ChatManager_RemoveListener(client);
    GroupManager_RemoveListener(client);
    RoomManager_RemoveListener(client);
    ContactManager_RemoveListener(client);
    
    CLIENT->removeConnectionListener(gConnectionListener);
    LOG("Connection listener removed.");
    delete gConnectionListener;
    gConnectionListener = nullptr;
    
    CLIENT->removeConnectionCallbackListener(gConnectionCallbackListener);
    LOG("Connection callback listener removed.");
    delete gConnectionCallbackListener;
    gConnectionCallbackListener = nullptr;
    
    CLIENT->removeMultiDevicesListener(gMultiDevicesListener);
    LOG("Multi device listener removed.");
    delete gMultiDevicesListener;
    gMultiDevicesListener = nullptr;

    LOG("Clear resource completed----------");
}

HYPHENATE_API void Client_LoginWithAgoraToken(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *agoraToken)
{
    if (true == G_LOGIN_STATUS) {
        LOG("Already in login status, no need to login again.");
        return;
    }
    
    EMError error;
    if(!MandatoryCheck(username, agoraToken, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    std::string usernameStr = username;
    std::string agoraTokenStr = agoraToken;
    
    //LOG("Agora token:%s", agoraTokenStr.c_str());
    
    std::string response;
    CLIENT->getChatTokenbyAgoraToken(agoraTokenStr, response, error);
    
    if (EMError::EM_NO_ERROR != error.mErrorCode) {
        LOG("getChatTokenbyAgoraToken failed. code:%d, desc:%s", error.mErrorCode, error.mDescription.c_str());
        if (onError)
            onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    LOG("getChatTokenbyAgoraToken successfully.");
    
    std::string token;
    std::string expireTS;
    // parse json
    if (!GetTokenCofigFromJson(response, token, expireTS)) {
        LOG("Error: cannot get token config from response: %s", response.c_str());
        error.mErrorCode = EMError::GENERAL_ERROR;
        error.mDescription = "Cannot get token config from response.";
        
        if (onError)
            onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
        
    // async login with easemob token
    std::thread t([=](){
        EMErrorPtr error = CLIENT->loginWithToken(usernameStr, token);
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("loginWithToken succeeds for user: %s", usernameStr.c_str());
            
            G_LOGIN_STATUS = true;
            
            SetTokenInAutoLogin(usernameStr, token, expireTS);
            SaveAutoLoginConfigToFile();
            StartTimer(GetTokenCheckInterval((int)global_autologin_config.availablePeriod), TokenCheck);
            
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("loginWithToken failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void Client_RenewAgoraToken(void *client, const char *agoraToken)
{
    if (false == G_LOGIN_STATUS) {
        LOG("Not in login status, cannot execute renew token action.");
        return;
    }
    
    if(!MandatoryCheck(agoraToken)) {
        return;
    }
    
    std::string agoraTokenStr = agoraToken;
    std::string response;
    
    //LOG("Renew agora token:%s", agoraTokenStr.c_str());
    
    EMError error;
    CLIENT->getChatTokenbyAgoraToken(agoraTokenStr, response, error);
    
    if (EMError::EM_NO_ERROR != error.mErrorCode) {
        LOG("Renew getChatTokenbyAgoraToken failed. err:%d, desc:%s", error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    LOG("getChatTokenbyAgoraToken successfully.");
    
    std::string token;
    std::string expireTS;
    // parse json
    if (!GetTokenCofigFromJson(response, token, expireTS)) {
        LOG("Error: cannot get token config from response: %s", response.c_str());
        return;
    }
    
    // Check expireTS first, then renewToken
    if (!SetTokenInAutoLogin(global_autologin_config.userName, token, expireTS)) {
        LOG("Error: Client_RenewAgoraToken failed due to failure of SetTokenInAutoLogin.");
        return;
    }

    CLIENT->renewToken(token);
    SaveAutoLoginConfigToFile();
    
    StartTimer(GetTokenCheckInterval((int)global_autologin_config.availablePeriod), TokenCheck);
    LOG("renewToken complete");
}

HYPHENATE_API void Client_AutoLogin(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    if (true == G_LOGIN_STATUS) {
        LOG("Already in login status, cannot execute autologin.");
        return;
    }
    
    EMError error;
    
    GetAutoLoginConfigFromFile();
    
    // validate token config
    if (global_autologin_config.userName.size() == 0 ||
        (global_autologin_config.token.size() == 0 && global_autologin_config.passwd.size() == 0)) {
        
        LOG("Error: Load token config failed.");
        error.mErrorCode = EMError::GENERAL_ERROR;
        error.mDescription = "AutoLogin load token config failed";
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    // default it is passwd login
    bool isToken = false;
    std::string code = global_autologin_config.passwd;
    
    if (global_autologin_config.token.size() > 0) {
        isToken = true;
        code = global_autologin_config.token;
    }
    
    // async login with easemob token
    std::thread t([=](){
        
        EMErrorPtr error = CLIENT->autoLogin(global_autologin_config.userName, code, isToken);
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("AutoLogin succeeds for user: %s", global_autologin_config.userName.c_str());
            
            G_LOGIN_STATUS = true;
            if(global_autologin_config.availablePeriod > 0)
                StartTimer(GetTokenCheckInterval((int)global_autologin_config.availablePeriod), TokenCheck);
            
            if(onSuccess) {
                const char* data[1] = {global_autologin_config.userName.c_str()};
                onSuccess((void **)data, DataType::String, 1, callbackId);
            }
        }else{
            LOG("AutoLogin failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}
