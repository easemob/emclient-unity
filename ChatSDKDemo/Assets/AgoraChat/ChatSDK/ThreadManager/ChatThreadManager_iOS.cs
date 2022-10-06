using System.Collections.Generic;
using SimpleJSON;

namespace AgoraChat
{
    internal sealed class ChatThreadManager_iOS : IChatThreadManager
    {
        public override void ChangeThreadName(string threadId, string newName, CallBack handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            json.Add("name", newName);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("changeThreadName", json.ToString(), handle?.callbackId);
        }

        public override void CreateThread(string threadName, string msgId, string groupId, ValueCallBack<ChatThread> handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("msgId", msgId);
            json.Add("groupId", groupId);
            json.Add("name", threadName);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("createThread", json.ToString(), handle?.callbackId);
        }

        public override void DestroyThread(string threadId, CallBack handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("destroyThread", json.ToString(), handle?.callbackId);
        }

        public override void FetchMineJoinedThreadList(string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("cursor", cursor ?? "");
            json.Add("pageSize", pageSize);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("fetchMineJoinedThreadList", json.ToString(), handle?.callbackId);
        }

        public override void FetchThreadListOfGroup(string groupId, bool joined, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<ChatThread>> handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("groupId", groupId);
            json.Add("joined", joined);
            json.Add("cursor", cursor ?? "");
            json.Add("pageSize", pageSize);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("fetchThreadListOfGroup", json.ToString(), handle?.callbackId);
        }

        public override void FetchThreadMembers(string threadId, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<string>> handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            json.Add("cursor", cursor);
            json.Add("pageSize", pageSize);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("fetchThreadMembers", json.ToString(), handle?.callbackId);
        }

        public override void GetLastMessageAccordingThreads(List<string> threadIds, ValueCallBack<Dictionary<string, Message>> handle = null)
        {
            JSONArray jAry = new JSONArray();
            foreach (string threadId in threadIds) {
                jAry.Add(threadId);
            }
            JSONObject json = new JSONObject();
            json.Add("threadIds", jAry);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("getLastMessageAccordingThreads", json.ToString(), handle?.callbackId);
        }

        public override void GetThreadDetail(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("getThreadDetail", json.ToString(), handle?.callbackId);
        }

        public override void JoinThread(string threadId, ValueCallBack<ChatThread> handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("joinThread", json.ToString(), handle?.callbackId);
        }

        public override void LeaveThread(string threadId, CallBack handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("leaveThread", json.ToString(), handle?.callbackId);
        }

        public override void RemoveThreadMember(string threadId, string username, CallBack handle = null)
        {
            JSONObject json = new JSONObject();
            json.Add("threadId", threadId);
            json.Add("username", username);
            ChatAPIIOS.ChatThreadManager_HandleMethodCall("removeThreadMember", json.ToString(), handle?.callbackId);
        }
    }
}
