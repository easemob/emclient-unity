#ifndef _PUSH_MANAGER_H_
#define _PUSH_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//PushManager methods
AGORA_API void PushManager_GetIgnoredGroupIds(void *client, FUNC_OnSuccess_With_Result onSuccess);
AGORA_API void PushManager_GetPushConfig(void *client, FUNC_OnSuccess_With_Result onSuccess);
AGORA_API void PushManager_GetUserConfigsFromServer(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void PushManager_IgnoreGroupPush(void *client, int callbackId, const char * groupId, bool noDisturb, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void PushManager_UpdatePushNoDisturbing(void *client, int callbackId, bool noDisturb, int startTime, int endTime, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void PushManager_UpdatePushDisplayStyle(void *client, int callbackId, EMPushConfigs::EMPushDisplayStyle style, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void PushManager_UpdateFCMPushToken(void *client, int callbackId, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void PushManager_UpdateHMSPushToken(void *client, int callbackId, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void PushManager_UpdatePushNickName(void *client, int callbackId, const char * nickname, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_PUSH_MANAGER_H_

