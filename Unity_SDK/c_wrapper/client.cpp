#include "client.h"

#include "emchatconfigs.h"
#include "emchatprivateconfigs.h"
#include "emclient.h"
#include "emchatmanager_interface.h"

using namespace easemob;

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
#define CALLBACK static_cast<Callback *>(callback)
}

static bool G_DEBUG_MODE = false;
static bool G_AUTO_LOGIN = true;

AGORA_API void Client_CreateAccount(void *client, void *callback, const char *username, const char *password)
{
    LOG("Client_CreateAccount() called with username=%s password=%s", username, password);
    EMErrorPtr result = CLIENT->createAccount(username, password);
    if(callback != nullptr) {
        if(EMError::isNoError(result)) {
            LOG("Account creation succeeds!");
            CALLBACK->OnSuccess();
        }else{
            LOG("Account creation failed!");
            CALLBACK->OnError(result->mErrorCode, result->mDescription.c_str());
        }
    }
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
    return configs;
}

AGORA_API void* Client_InitWithOptions(Options *options, ConnListenerFptrs fptrs)
{
    // global switch
    G_DEBUG_MODE = options->DebugMode;
    G_AUTO_LOGIN = options->AutoLogin;
    EMChatConfigsPtr configs = ConfigsFromOptions(options);
    EMClient * client = EMClient::create(configs);
    //TODO: keep connection listener instance disposable!
    LOG("ConnectionListener FPtrs: OnConnect=%d, OnDisconnected=%d,", fptrs.Connected, fptrs.Disconnected);
    //client->addConnectionListener(new ConnectionListener(fptrs));
    return client;
}

AGORA_API void Client_Login(void *client, void *callback, const char *username, const char *pwdOrToken, bool isToken)
{
    LOG("Client_Login() called with username=%s, pwdOrToken=%s, isToken=%d", username, pwdOrToken, isToken);
    LOG("callback instance address=%d", callback);
    EMErrorPtr result;
    if(!isToken) {
        result = CLIENT->login(username, pwdOrToken);
    } else {
        result = CLIENT->loginWithToken(username, pwdOrToken);
    }
    if(callback != nullptr) { // callback processing
        if(EMError::isNoError(result)) {
            LOG("Login succeeds.");
            //call OnSuccess callback
            CALLBACK->OnSuccess();
        }else{
            LOG("Login error with error code %d, description: %s", result->mErrorCode, result->mDescription.c_str());
            //call OnError callback
            CALLBACK->OnError(result->mErrorCode, result->mDescription.c_str());
        }
    }
}

AGORA_API void Client_Logout(void *client, void *callback, bool unbindDeviceToken)
{
    CLIENT->logout();
    if(callback != nullptr) {
        CALLBACK->OnSuccess();
    }
}

AGORA_API void Client_Release(void *client)
{
    delete CLIENT;
    client = nullptr;
}

AGORA_API void Client_StartLog(const char *logFilePath) {
    LogHelper::getInstance().startLogService(logFilePath);
}

AGORA_API void Client_StopLog() {
    return LogHelper::getInstance().stopLogService();
}

EMCallbackObserverHandle gCallbackObserverHandle;

AGORA_API void ChatManager_SendMessage(void *client, void *callback, MessageTransferObject *mto) {
    EMMessagePtr messagePtr = mto->toEMMessage();
    if(callback != nullptr) {
        LOG("ChatManager_SendMessage's callback address=%x", callback);
        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                                 [callback]()->bool {
                                                    LOG("Message sent succeeds.");
                                                    //CALLBACK->OnSuccess();
                                                    return true;
                                                 },
                                                 [callback](const easemob::EMErrorPtr error)->bool{
                                                    LOG("Message sent failed with code=%d.", error->mErrorCode);
                                                    //CALLBACK->OnError(error->mErrorCode,error->mDescription.c_str());
                                                    return true;
                                                 }));
        messagePtr->setCallback(callbackPtr);
    }
    CLIENT->getChatManager().sendMessage(messagePtr);
}
