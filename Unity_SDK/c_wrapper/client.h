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

  AGORA_API void Client_CreateAccount(void *client, const char *username, const char *password);
  AGORA_API void* Client_InitWithOptions(Options *options, ConnListenerFptrs fptrs);
  AGORA_API void Client_Login(void *client, void *callback, const char *username, const char *pwdOrToken, bool isToken);
  AGORA_API void Client_Logout(void *client, bool unbindDeviceToken);
  AGORA_API void Client_StartLog(const char *logFilePath);
  AGORA_API void Client_StopLog();


#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CLIENT_H_
