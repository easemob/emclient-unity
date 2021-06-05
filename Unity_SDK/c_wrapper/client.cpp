#include "client.h"

#include "emchatconfigs.h"
#include "emchatprivateconfigs.h"
#include "emclient.h"

using namespace easemob;

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
}

static bool G_DEBUG_MODE = false;
static bool G_AUTO_LOGIN = true;

AGORA_API void Client_CreateAccount(void *client, const char *username, const char *password)
{
    CLIENT->createAccount(username, password);
}

EMChatConfigsPtr fromOptions(Options *options) {
    const char *appKey = options->AppKey;
    LOG("Client_InitWithOptions() called with AppKey=%s", appKey);
    EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs("","",appKey,0));
    const char *dnsURL = options->DNSURL;
    const char *imServer = options->IMServer;
    const char *restServer = options->RestServer;
    int imPort = options->IMPort;
    bool enableDNSConfig = options->EnableDNSConfig;
    LOG("Options with DNSURL=%s, IMServer=%s, IMPort=%d, RestServer=%s, EnableDNSConfig=%s", dnsURL, imServer, imPort, restServer, enableDNSConfig ? "false" : "true");
    configs->setDnsURL(options->DNSURL);
    configs->privateConfigs().chatServer() = options->IMServer;
    configs->privateConfigs().restServer() = options->RestServer;
    configs->privateConfigs().chatPort() = options->IMPort;
    configs->privateConfigs().enableDns() = options->EnableDNSConfig;
    bool acceptInvitationAlways = options->AcceptInvitationAlways;
    LOG("AccepInvitationAlways=%s", options->AcceptInvitationAlways ? "false" : "true");
    configs->setAutoAcceptFriend(acceptInvitationAlways);
    bool autoAcceptGroupInvitation = options->AutoAcceptGroupInvitation;
    LOG("AutoAcceptGroupInvitation=%s", autoAcceptGroupInvitation ? "false" : "true");
    configs->setAutoAcceptGroup(autoAcceptGroupInvitation);
    bool requireAck = options->RequireAck;
    LOG("RequireAck=%s", requireAck ? "false" : "true");
    configs->setRequireReadAck(requireAck);
    configs->setRequireDeliveryAck(options->RequireDeliveryAck);
    configs->setDeleteMessageAsExitGroup(options->DeleteMessagesAsExitGroup);
    configs->setDeleteMessageAsExitChatRoom(options->DeleteMessagesAsExitRoom);
    configs->setIsChatroomOwnerLeaveAllowed(options->IsRoomOwnerLeaveAllowed);
    configs->setUsingHttps(options->UsingHttpsOnly);
    configs->setTransferAttachments(options->ServerTransfer);
    configs->setAutoDownloadThumbnail(options->IsAutoDownload);
    return configs;
}

AGORA_API void* Client_InitWithOptions(Options *options)
{
    // global switch
    G_DEBUG_MODE = options->DebugMode;
    G_AUTO_LOGIN = options->AutoLogin;
    EMChatConfigsPtr configs = fromOptions(options);
    return EMClient::create(configs);
}



AGORA_API void Client_Login(void *client, const char *username, const char *pwdOrToken, bool isToken)
{
    LOG("Client_Login() called with username=%s, pwdOrToken=%s, isToken=%d", username, pwdOrToken, isToken);
    if(!isToken)
        CLIENT->login(username, pwdOrToken);
    else
        CLIENT->loginWithToken(username, pwdOrToken);
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
