//
//  presence_manager.h
//  hyphenateCWrapper
//
//  Created by yuqiang on 2022/4/25.
//  Copyright © 2022 easemob. All rights reserved.
//

#ifndef _PRESENCE_MANAGER_H_
#define _PRESENCE_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus

HYPHENATE_API void PresenceManager_AddListener(void *client, FUNC_OnPresenceUpdated onPresenceUpdated);

HYPHENATE_API void PresenceManager_PublishPresence(void * client, int callbackId, int presenceStatus,const char* ext, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void PresenceManager_SubscribePresences(void * client, int callbackId, const char * members[], int size, int64_t expiry, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void PresenceManager_UnsubscribePresences(void * client, int callbackId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void PresenceManager_FetchSubscribedMembers(void * client, int callbackId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void PresenceManager_FetchPresenceStatus(void * client, int callbackId, const char * members[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);

#ifdef __cplusplus
}
#endif //__cplusplus

#endif /* _PRESENCE_MANAGER_H_ */
