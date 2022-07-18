using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    internal sealed class ThreadManager_Common : IThreadManager
    {
        private IntPtr client;
        private ThreadManagerHub threadManagerHub;

        internal ThreadManager_Common(IClient _client)
        {
            if (_client is Client_Common clientCommon)
            {
                client = clientCommon.client;
            }
            //register listeners
            threadManagerHub = new ThreadManagerHub();
            ChatAPINative.ThreadManager_AddListener(client, threadManagerHub.OnCreatThread_, threadManagerHub.OnUpdateMyThread_,
                threadManagerHub.OnThreadNotifyChange_, threadManagerHub.OnLeaveThread_, threadManagerHub.OnMemberJoinedThread_, threadManagerHub.OnMemberLeaveThread_);
        }

        public override void ChangeThreadSubject(string threadId, string newSubject, CallBack handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_ChangeThreadSubject(client, callbackId, threadId, newSubject ?? "",
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ThreadEvent> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_CreateThread(client, callbackId, threadName, msgId, groupId,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    ThreadEvent thread = ThreadEvent.FromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<ThreadEvent>(cbId, thread);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<ThreadEvent>(cbId, code, desc);
                });
        }

        public override void DestroyThread(string threadId, CallBack handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_DestroyThread(client, callbackId, threadId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ThreadEvent>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_FetchThreadListOfGroup(client, callbackId, cursor, pageSize, groupId, joined,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    CursorResult<ThreadEvent> cursorResult = ThreadEvent.CursorThreadFromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<ThreadEvent>>(cbId, cursorResult);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<ThreadEvent>>(cbId, code, desc);
                });
        }

        public override void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_FetchThreadMembers(client, callbackId, threadId, cursor, pageSize,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    CursorResult<string> cursorResult = TransformTool.JsonStringToStringResult(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<string>>(cbId, cursorResult);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<string>>(cbId, code, desc);
                });
        }

        public override void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            int count = threadIds.Count;
            string[] idArray = TransformTool.GetArrayFromList(threadIds);

            ChatAPINative.ThreadManager_GetLastMessageAccordingThreads(client, callbackId, idArray, count,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    Dictionary<string, Message> dict = ThreadEvent.DictFromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<Dictionary<string, Message>>(cbId, dict);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<Dictionary<string, Message>>(cbId, code, desc);
                });
        }

        public override void GetThreadDetail(string threadId, ValueCallBack<ThreadEvent> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_GetThreadDetail(client, callbackId, threadId,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    ThreadEvent thread = ThreadEvent.FromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<ThreadEvent>(cbId, thread);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<ThreadEvent>(cbId, code, desc);
                });
        }

        public override void GetThreadWithThreadId(string threadId, ValueCallBack<ThreadEvent> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_GetThreadWithThreadId(client, callbackId, threadId,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    ThreadEvent thread = ThreadEvent.FromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<ThreadEvent>(cbId, thread);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<ThreadEvent>(cbId, code, desc);
                });
        }

        public override void JoinThread(string threadId, ValueCallBack<ThreadEvent> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_JoinThread(client, callbackId, threadId,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    ThreadEvent thread = ThreadEvent.FromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<ThreadEvent>(cbId, thread);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<ThreadEvent>(cbId, code, desc);
                });
        }

        public override void LeaveThread(string threadId, CallBack handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_LeaveThread(client, callbackId, threadId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void RemoveThreadMember(string threadId, string username, CallBack handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_RemoveThreadMember(client, callbackId, threadId, username,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ThreadEvent>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ThreadManager_FetchMineJoinedThreadList(client, callbackId, cursor, pageSize,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
                {
                    string json = TransformTool.PtrToString(data[0]);
                    CursorResult<ThreadEvent> cursorResult = ThreadEvent.CursorThreadFromJson(json);
                    ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<ThreadEvent>>(cbId, cursorResult);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<ThreadEvent>>(cbId, code, desc);
                });
        }
    }
}
