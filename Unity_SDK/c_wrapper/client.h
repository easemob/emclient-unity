#ifndef _CLIENT_H_
#define _CLIENT_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//Client methods
AGORA_API void Client_CreateAccount(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *password);
AGORA_API void* Client_InitWithOptions(Options *options, FUNC_OnConnected, FUNC_OnDisconnected, FUNC_OnPong);
AGORA_API void Client_Login(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *pwdOrToken, bool isToken);
AGORA_API void Client_Logout(void *client, FUNC_OnSuccess onSuccess, bool unbindDeviceToken);
AGORA_API void Client_StartLog(const char *logFilePath);
AGORA_API void Client_StopLog();

#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CLIENT_H_
