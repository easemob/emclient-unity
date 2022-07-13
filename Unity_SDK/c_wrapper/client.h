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
HYPHENATE_API void Client_CreateAccount(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *password);
HYPHENATE_API void* Client_InitWithOptions(Options *options, FUNC_OnConnected, FUNC_OnDisconnected, FUNC_OnPong, FUNC_onTokenNotification);
HYPHENATE_API void Client_AddMultiDeviceListener(FUNC_onContactMultiDevicesEvent, FUNC_onGroupMultiDevicesEvent, FUNC_undisturbMultiDevicesEvent, FUNC_onThreadMultiDevicesEvent threadEventFunc);
HYPHENATE_API void Client_Login(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *pwdOrToken, bool isToken);
HYPHENATE_API void Client_Logout(void *client, int callbackId, FUNC_OnSuccess onSuccess, bool unbindDeviceToken);
HYPHENATE_API void Client_StartLog(const char *logFilePath);
HYPHENATE_API void Client_StopLog();
HYPHENATE_API void Client_LoginToken(void *client, FUNC_OnSuccess_With_Result onSuccess);
HYPHENATE_API bool Client_isConnected(void* client);
HYPHENATE_API bool Client_isLoggedIn(void* client);
HYPHENATE_API void Client_ClearResource(void *client);
HYPHENATE_API void Client_LoginWithAgoraToken(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, const char *username, const char *agoraToken);
HYPHENATE_API void Client_RenewAgoraToken(void *client, const char *agoraToken);
HYPHENATE_API void Client_AutoLogin(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus
#endif //_CLIENT_H_
