#ifndef _CLIENT_H_
#define _CLIENT_H_

#pragma once
#include "common.h"
#include "models.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus

  AGORA_API void Client_CreateAccount(void *client, const char *username, const char *password);
  AGORA_API void* Client_InitWithOptions(Options *options);
  AGORA_API void Client_Login(void *client, const char *username, const char *pwdOrToken, bool isToken);
  AGORA_API void Client_Logout(void *client, bool unbindDeviceToken);

#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CLIENT_H_
