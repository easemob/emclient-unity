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

EMChatConfigsPtr ConfigsFromOptions(Options *options) {
    const char *appKey = options->AppKey;
    LOG("Client_InitWithOptions() called with AppKey=%s", appKey);
    EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs("./sdkdata","./sdkdata",appKey,0));
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

EMClient *gClient = nullptr;
EMConnectionListener *gConnectionListener = nullptr;
EMConnectionCallbackListener *gConnectionCallbackListener = nullptr;
EMMultiDevicesListener *gMultiDevicesListener = nullptr;

HYPHENATE_API void* Client_InitWithOptions(Options *options, FUNC_OnConnected onConnected, FUNC_OnDisconnected onDisconnected, FUNC_OnPong onPong)
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
        gConnectionListener = new ConnectionListener(onConnected, onDisconnected, onPong);
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

HYPHENATE_API void Client_Logout(void *client, int callbackId, FUNC_OnSuccess onSuccess, bool unbindDeviceToken)
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
