//
//  thread_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2022/07/04.
//  Copyright © 2021 easemob. All rights reserved.
//
#include <thread>
#include "thread_manager.h"
#include "emclient.h"
#include "tool.h"

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

ThreadManagerListener* gThreadListener = nullptr;

HYPHENATE_API void ThreadManager_AddListener(void* client,
    FUNC_OnCreatThread OnCreatThread,
    FUNC_OnUpdateMyThread OnUpdateMyThread,
    FUNC_OnThreadNotifyChange OnThreadNotifyChange,
    FUNC_OnLeaveThread OnLeaveThread,
    FUNC_OnMemberJoinedThread OnMemberJoined,
    FUNC_OnMemberLeave OnMemberLeave
)
{
    if (nullptr == gThreadListener) { //only set once!
        gThreadListener = new ThreadManagerListener(OnCreatThread, OnUpdateMyThread, OnThreadNotifyChange, OnLeaveThread, OnMemberJoined, OnMemberLeave);
        CLIENT->getThreadManager().addListener(gThreadListener);
        LOG("New ThreadManager listener and hook it.");
    }
}

HYPHENATE_API void ThreadManager_GetThreadWithThreadId(void* client, int callbackId, const char* threadId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string threadIdStr = threadId;

    std::thread t([=]() {
        EMError error;
        EMThreadEventPtr result = CLIENT->getThreadManager().getThreadWithThreadId(threadIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_GetThreadWithThreadId succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_GetThreadWithThreadId failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_CreateThread(void* client, int callbackId, const char* threadName, const char* msgId, const char* groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadName, msgId, groupId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string threadNameStr = GetUTF8FromUnicode(threadName);
    std::string msgIdStr = msgId;
    std::string groupIdStr = groupId;

    std::thread t([=]() {
        EMError error;
        EMThreadEventPtr result = CLIENT->getThreadManager().createThread(threadNameStr, msgIdStr, groupIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_CreateThread succeeds: group:%s", threadNameStr.c_str());
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_CreateThread failed: threadNameId=%s, code=%d, desc=%s", threadNameStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_JoinThread(void* client, int callbackId, const char* threadId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string threadIdStr = threadId;

    std::thread t([=]() {
        EMError error;
        EMThreadEventPtr result = CLIENT->getThreadManager().joinThread(threadIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_JoinThread succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_JoinThread failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_LeaveThread(void* client, int callbackId, const char* threadId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string threadIdStr = threadId;

    std::thread t([=]() {
        EMError error;
        CLIENT->getThreadManager().leaveThread(threadIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_LeaveThread succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                onSuccess(callbackId);
            }
        }
        else {
            LOG("ThreadManager_LeaveThread failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_DestroyThread(void* client, int callbackId, const char* threadId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string threadIdStr = threadId;

    std::thread t([=]() {
        EMError error;
        CLIENT->getThreadManager().destroyThread(threadIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_DestroyThread succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                onSuccess(callbackId);
            }
        }
        else {
            LOG("ThreadManager_DestroyThread failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_RemoveThreadMember(void* client, int callbackId, const char* threadId, const char* username, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string threadIdStr = threadId;
    std::string usernameStr = username;

    std::thread t([=]() {
        EMError error;
        CLIENT->getThreadManager().removeThreadMember(threadIdStr, usernameStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_RemoveThreadMember succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                onSuccess(callbackId);
            }
        }
        else {
            LOG("ThreadManager_RemoveThreadMember failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_ChangeThreadSubject(void* client, int callbackId, const char* threadId, const char* newSubject, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, newSubject, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string threadIdStr = threadId;
    std::string newSubjectStr = GetUTF8FromUnicode(newSubject);

    std::thread t([=]() {
        EMError error;
        CLIENT->getThreadManager().changeThreadSubject(threadIdStr, newSubjectStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_ChangeThreadSubject succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                onSuccess(callbackId);
            }
        }
        else {
            LOG("ThreadManager_ChangeThreadSubject failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_FetchThreadMembers(void* client, int callbackId, const char* threadId, const char* cursor, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string threadIdStr = threadId;
    std::string cursorStr = OptionalStrParamCheck(cursor);

    std::thread t([=]() {
        EMError error;
        EMCursorResultRaw<std::string> result = CLIENT->getThreadManager().fetchThreadMembers(threadIdStr, cursorStr, pageSize, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_FetchThreadMembers succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                std::string json = JsonStringFromCursorResult(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_FetchThreadMembers failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_FetchThreadListOfGroup(void* client, int callbackId, const char* cursor, int pageSize, const char* groupId, bool joined, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(groupId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string cursorStr = OptionalStrParamCheck(cursor);
    std::string groupIdStr = groupId;

    std::thread t([=]() {
        EMError error;
        EMCursorResultRaw<EMThreadEventPtr> result = CLIENT->getThreadManager().fetchThreadListOfGroup(cursorStr, pageSize, groupIdStr, joined, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_FetchThreadListOfGroup succeeds");
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_FetchThreadListOfGroup failed: code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_FetchMineJoinedThreadList(void* client, int callbackId, const char* cursor, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::string cursorStr = OptionalStrParamCheck(cursor);

    std::thread t([=]() {
        EMError error;
        EMCursorResultRaw<EMThreadEventPtr> result = CLIENT->getThreadManager().fetchMineJoinedThreadList(cursorStr, pageSize, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_FetchMineJoinedThreadList succeeds");
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_FetchMineJoinedThreadList failed: code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_GetThreadDetail(void* client, int callbackId, const char* threadId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(threadId, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string threadIdStr = threadId;

    std::thread t([=]() {
        EMError error;
        EMThreadEventPtr result = CLIENT->getThreadManager().getThreadDetail(threadIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_GetThreadDetail succeeds: group:%s", threadIdStr.c_str());
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_GetThreadDetail failed: threadId=%s, code=%d, desc=%s", threadIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ThreadManager_GetLastMessageAccordingThreads(void* client, int callbackId, const char* threadIds[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::vector<std::string> vec;
    for (int i = 0; i < size; i++) {
        vec.push_back(threadIds[i]);
    }

    std::thread t([=]() {
        EMError error;
        std::map<std::string, EMMessagePtr> result = CLIENT->getThreadManager().getLastMessageAccordingThreads(vec, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ThreadManager_GetLastMessageAccordingThreads succeeds");
            if (onSuccess) {
                std::string json = ThreadEventTO::ToJson(result);
                const char* data[1] = { json.c_str() };
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ThreadManager_GetLastMessageAccordingThreads failed: code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}
