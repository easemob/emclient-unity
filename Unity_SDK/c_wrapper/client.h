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
Hypheante_API void Client_CreateAccount(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *password);
Hypheante_API void* Client_InitWithOptions(Options *options, FUNC_OnConnected, FUNC_OnDisconnected, FUNC_OnPong);
Hypheante_API void Client_Login(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *pwdOrToken, bool isToken);
Hypheante_API void Client_Logout(void *client, int callbackId, FUNC_OnSuccess onSuccess, bool unbindDeviceToken);
Hypheante_API void Client_StartLog(const char *logFilePath);
Hypheante_API void Client_StopLog();
Hypheante_API void Client_LoginToken(void *client, FUNC_OnSuccess_With_Result onSuccess);

#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CLIENT_H_
