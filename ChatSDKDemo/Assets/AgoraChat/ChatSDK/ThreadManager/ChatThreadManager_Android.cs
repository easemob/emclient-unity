using System.Collections.Generic;
using UnityEngine;

namespace AgoraChat
{
    internal sealed class ChatThreadManager_Android : IChatThreadManager
    {

        private AndroidJavaObject wrapper;

        public ChatThreadManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMChatThreadManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }


        public override void ChangeThreadName(string threadId, string newName, CallBack handle = null)
        {
            wrapper.Call("changeThreadName", threadId, newName, handle?.callbackId);
        }

        public override void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ChatThread> handle = null)
        {
            wrapper.Call("createThread", threadName, msgId, groupId, handle?.callbackId);
        }

        public override void DestroyThread(string threadId, CallBack handle = null)
        {
            wrapper.Call("destroyThread", threadId, handle?.callbackId);
        }

        public override void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null)
        {
            wrapper.Call("fetchMineJoinedThreadList", cursor ?? "" , pageSize, handle?.callbackId);
        }

        public override void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null)
        {
            wrapper.Call("fetchThreadListOfGroup", groupId, joined, cursor ?? "", pageSize, handle?.callbackId);
        }

        public override void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null)
        {
            wrapper.Call("fetchThreadMembers", threadId, cursor ?? "", pageSize, handle?.callbackId);
        }

        public override void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null)
        {
            wrapper.Call("getLastMessageAccordingThreads", TransformTool.JsonStringFromStringList(threadIds), handle?.callbackId);
        }

        public override void GetThreadDetail(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            wrapper.Call("getThreadDetail", threadId, handle?.callbackId);
        }

        public override void JoinThread(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            wrapper.Call("joinThread", threadId, handle?.callbackId);
        }

        public override void LeaveThread(string threadId, CallBack handle = null)
        {
            wrapper.Call("leaveThread", threadId, handle?.callbackId);
        }

        public override void RemoveThreadMember(string threadId, string username, CallBack handle = null)
        {
            wrapper.Call("removeThreadMember", threadId, username, handle?.callbackId);
        }
    }
}