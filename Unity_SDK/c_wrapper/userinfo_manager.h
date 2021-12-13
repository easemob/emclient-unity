#ifndef _USERINFO_MANAGER_H_
#define _USERINFO_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus

HYPHENATE_API void UserInfoManager_UpdateOwnInfo(void *client, int callbackId, void* userInfo, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void UserInfoManager_UpdateOwnInfoByAttribute(void *client, int callbackId, int userinfoType, const char* value, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void UserInfoManager_FetchUserInfoByUserId(void *client, int callbackId, const char * users[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void UserInfoManager_FetchUserInfoByAttribute(void *client, int callbackId, const char * users[], int userSize, int userinfoTypes[], int typeSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);

#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CHAT_MANAGER_H_

