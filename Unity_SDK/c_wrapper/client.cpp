#include "client.h"

#include "emchatconfigs.h"
#include "emclient.h"
#include "LogHelper.h"

using namespace easemob;

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
}

AGORA_API void Client_CreateAccount(void *client, const char *username, const char *password)
{
    CLIENT->createAccount(username, password);
}

AGORA_API void* Client_InitWithOptions(Options *options)
{
    const char *appKey = options->AppKey;
    LogHelper::getInstance().writeLog("Client_InitWithOptions() called with AppKey=s%", appKey);
    EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs("","",appKey,0));
    return EMClient::create(configs);
}

AGORA_API void Client_Login(void *client, const char *username, const char *pwdOrToken, bool isToken)
{
    LogHelper::getInstance().writeLog("Client_Login() called with username=%s, pwdOrToken=%s, isToken=%d", username, pwdOrToken, isToken);
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
