#include "client.h"

#include "emchatconfigs.h"
#include "emclient.h"

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
    EMChatConfigsPtr configs = EMChatConfigsPtr(new EMChatConfigs("","",options->AppKey,0));
    return EMClient::create(configs);
}

AGORA_API void Client_Login(void *client, const char *username, const char *pwdOrToken, bool isToken)
{
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
}
