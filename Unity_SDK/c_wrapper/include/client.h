#ifndef CLIENT_H
#define CLIENT_H

#pragma once
#include "common_header.h"
#include "models.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus

  DLLEXPORT void Client_CreateAccount(const char *username, const char *password);
  DLLEXPORT void Client_InitWithOptions(Options options);
  DLLEXPORT void Client_Login(const char *username, const char *pwdOrToken, bool isToken);
  DLLEXPORT void Client_Logout(bool unbindDeviceToken);

#ifdef __cplusplus
}
#endif //__cplusplus

#endif //CLIENT_H