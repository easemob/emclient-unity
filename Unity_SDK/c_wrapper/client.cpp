#include <thread>
#include "client.h"
#include "chat_manager.h"

#include "emlogininfo.h"
#include "emchatconfigs.h"
#include "emchatprivateconfigs.h"
#include "emclient.h"
#include "contact_manager.h"
#include "group_manager.h"
#include "room_manager.h"

using namespace easemob;

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
#define CALLBACK static_cast<Callback *>(callback)
}

static bool G_DEBUG_MODE = false;
static bool G_AUTO_LOGIN = true;
static bool G_LOGIN_STATUS = false;

static bool NeedAllocResource = false;

Hypheante_API void Client_CreateAccount(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *password)
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

EMChatConfigsPtr ConfigsFromOptions(Options *options) {
    const char *appKey = options->AppKey;
    LOG("Client_InitWithOptions() called with AppKey=%s", appKey);
    EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs("","",appKey,0));
    //TODO: non null-ptr assertion
    const char *dnsURL = options->DNSURL;
    const char *imServer = options->IMServer;
    const char *restServer = options->RestServer;
    int imPort = options->IMPort;
    bool enableDNSConfig = options->EnableDNSConfig;
    LOG("Options with DNSURL=%s, IMServer=%s, IMPort=%d, RestServer=%s, EnableDNSConfig=%s", dnsURL, imServer, imPort, restServer, enableDNSConfig ? "true" : "false");
    configs->setDnsURL(options->DNSURL);
    configs->privateConfigs().chatServer() = options->IMServer;
    configs->privateConfigs().restServer() = options->RestServer;
    configs->privateConfigs().chatPort() = options->IMPort;
    configs->privateConfigs().enableDns() = options->EnableDNSConfig;
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
    return configs;
}

EMClient *gClient = NULL;
EMConnectionListener *gConnectionListener = NULL;

Hypheante_API void* Client_InitWithOptions(Options *options, FUNC_OnConnected onConnected, FUNC_OnDisconnected onDisconnected, FUNC_OnPong onPong)
{
    // global switch
    G_DEBUG_MODE = options->DebugMode;
    G_AUTO_LOGIN = options->AutoLogin;
    // singleton client handle
    if(gClient == nullptr) {
        EMChatConfigsPtr configs = ConfigsFromOptions(options);
        gClient = EMClient::create(configs);
    } else {
        if(NeedAllocResource)
            gClient->allocResource();
    }
    
    if(gConnectionListener == NULL) { //only set once
        gConnectionListener = new ConnectionListener(onConnected, onDisconnected, onPong);
        gClient->addConnectionListener(gConnectionListener);
    }
    
    return gClient;
}

Hypheante_API void Client_Login(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *pwdOrToken, bool isToken)
{
    std::string usernameStr = username;
    std::string pwdOrTokenStr = pwdOrToken;
    
    LOG("Client_Login() called with username=%s, pwdOrToken=%s, isToken=%d", usernameStr.c_str(), pwdOrTokenStr.c_str(), isToken);
    std::thread t([=](){
        EMErrorPtr result;
        result = isToken ? CLIENT->loginWithToken(usernameStr, pwdOrTokenStr) : CLIENT->login(usernameStr, pwdOrTokenStr);
        if(EMError::isNoError(result)) {
            LOG("Login succeeds.");
            G_LOGIN_STATUS = true;
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("Login failed with code=%d desc=%s!", result->mErrorCode, result->mDescription.c_str());
            if(onError) onError(result->mErrorCode, result->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void Client_Logout(void *client, int callbackId, FUNC_OnSuccess onSuccess, bool unbindDeviceToken)
{
    std::thread t([=](){
        if(G_LOGIN_STATUS) {
            LOG("Execute logout action.");
            CLIENT->logout();
            G_LOGIN_STATUS = false;
            if(onSuccess) onSuccess(callbackId);
        } else {
            LOG("Already logout, NO need to execute logout action.");
        }
    });
    t.detach();
}

Hypheante_API void Client_StartLog(const char *logFilePath) {
    LogHelper::getInstance().startLogService(logFilePath);
}

Hypheante_API void Client_StopLog() {
    return LogHelper::getInstance().stopLogService();
}

Hypheante_API void Client_LoginToken(void *client, FUNC_OnSuccess_With_Result onSuccess) {
    const EMLoginInfo& loginInfo = CLIENT->getLoginInfo();
    const char* data[1];
    data[0] = loginInfo.loginToken().c_str();
    if(onSuccess) onSuccess((void **)data, DataType::String, 1, -1);
}

// this function must be executed after logout!!!
Hypheante_API void Client_ClearResource(void *client) {
    if(true == G_LOGIN_STATUS) {
        LOG("Still in login status, cannot clear resource.");
        return;
    }
    
    LOG("Clear resource begin");
    CLIENT->clearResource();
    
    // set flag for next replay
    NeedAllocResource = true;
    
    // clear all listeners when replay
    ChatManager_RemoveListener(client);
    GroupManager_RemoveListener(client);
    RoomManager_RemoveListener(client);
    ContactManager_RemoveListener(client);
    
    CLIENT->removeConnectionListener(gConnectionListener);
    LOG("Connection listener cleared.");
    delete gConnectionListener;
    gConnectionListener = nullptr;
    LOG("Clear resource completed");
}
