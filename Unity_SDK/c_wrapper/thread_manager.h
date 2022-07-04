#ifndef _THREAD_MANAGER_H_
#define _THREAD_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus

HYPHENATE_API void Threadanager_GetThreadWithThreadId(void *client, int callbackId, const char* threadId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void ThreadManager_CreateThread(void *client, int callbackId, const char* threadName, const char* msgId, const char* groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_JoinThread(void* client, int callbackId, const char* threadId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_LeaveThread(void* client, int callbackId, const char* threadId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_DestroyThread(void* client, int callbackId, const char* threadId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_RemoveThreadMember(void* client, int callbackId, const char* threadId, const char* username, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_ChangeThreadSubject(void* client, int callbackId, const char* threadId, const char* newSubject, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_FetchThreadMembers(void* client, int callbackId, const char* threadId, const char* cursor, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_FetchThreadListOfGroup(void* client, int callbackId, const char* cursor, int pageSize, const char* groupId, bool joined, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_FetchMineJoinedThreadList(void* client, int callbackId, const char* cursor, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_GetThreadDetail(void* client, int callbackId, const char* threadId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void Threadanager_GetLastMessageAccordingThreads(void* client, int callbackId, const char* threadIds[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_THREAD_MANAGER_H_

