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

AGORA_API void Client_CreateAccount(void *client, const char *username, const char *password)
{
    CLIENT->createAccount(username, password);
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
    LOG("DebugMode=%s", options->DebugMode ? "true" : "false");
    LOG("AutoLogin=%s", options->AutoLogin ? "true" : "false");
    bool acceptInvitationAlways = options->AcceptInvitationAlways;
    LOG("AcceptInvitationAlways=%s", options->AcceptInvitationAlways ? "true" : "false");
    configs->setAutoAcceptFriend(acceptInvitationAlways);
    bool autoAcceptGroupInvitation = options->AutoAcceptGroupInvitation;
    LOG("AutoAcceptGroupInvitation=%s", autoAcceptGroupInvitation ? "true" : "false");
    configs->setAutoAcceptGroup(autoAcceptGroupInvitation);
    bool requireAck = options->RequireAck;
    LOG("RequireAck=%s", requireAck ? "true" : "false");
    configs->setRequireReadAck(requireAck);
    bool requireDeliveryAck = options->RequireDeliveryAck;
    LOG("RequireDeliveryAck=%s", requireDeliveryAck ? "true" : "false");
    configs->setRequireDeliveryAck(requireDeliveryAck);
    bool deleteMessagesAsExitGroup = options->DeleteMessagesAsExitGroup;
    LOG("DeleteMessagesAsExitGroup=%s", deleteMessagesAsExitGroup ? "true" : "false");
    configs->setDeleteMessageAsExitGroup(deleteMessagesAsExitGroup);
    bool deleteMessagesAsExitRoom = options->DeleteMessagesAsExitRoom;
    LOG("DeleteMessagesAsExitRoom=%s", deleteMessagesAsExitRoom ? "true" : "false");
    configs->setDeleteMessageAsExitChatRoom(deleteMessagesAsExitRoom);
    bool isRoomOwnerLeaveAllowed = options->IsRoomOwnerLeaveAllowed;
    LOG("IsRoomOwnerLeaveAllowed=%s", isRoomOwnerLeaveAllowed ? "true" : "false");
    configs->setIsChatroomOwnerLeaveAllowed(isRoomOwnerLeaveAllowed);
    bool sortMessageByServerTime = options->SortMessageByServerTime;
    LOG("SortMessageByServerTime=%s", sortMessageByServerTime ? "true" : "false");
    configs->setSortMessageByServerTime(sortMessageByServerTime);
    bool usingHttpsOnly = options->UsingHttpsOnly;
    LOG("UsingHttpsOnly=%s", usingHttpsOnly ? "true" : "false");
    configs->setUsingHttps(usingHttpsOnly);
    bool serverTransfer = options->ServerTransfer;
    LOG("ServerTransfer=%s", serverTransfer ? "true" : "false");
    configs->setTransferAttachments(serverTransfer);
    bool isAutoDownload = options->IsAutoDownload;
    LOG("IsAutoDownload=%s", isAutoDownload ? "true" : "false");
    configs->setAutoDownloadThumbnail(isAutoDownload);
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
    client->addConnectionListener(new ConnectionListener(fptrs));
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

AGORA_API void Client_Logout(void *client, bool unbindDeviceToken)
{
    CLIENT->logout();
}

AGORA_API void Client_Release(void *client)
{
    delete CLIENT;
    client = nullptr;
    LogHelper::getInstance().stopLogService();
}

AGORA_API void Client_StartLog(const char *logFilePath) {
    LogHelper::getInstance().startLogService(logFilePath);
}

AGORA_API void Client_StopLog() {
    return LogHelper::getInstance().stopLogService();
}

AGORA_API void ChatManager_SendMessage(void *client, void *callback, MessageTransferObject *mto) {
    EMMessagePtr messagePtr = mto->toEMMessage();
    //CLIENT->getChatManager().sendMessage(messagePtr);
}
