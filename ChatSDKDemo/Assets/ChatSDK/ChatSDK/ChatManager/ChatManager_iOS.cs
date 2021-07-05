using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK
{
    public class ChatManager_iOS : IChatManager
    {

        static string Obj = "unity_chat_emclient_chatmanager_delegate_obj";

        GameObject listenerGameObj;

        public ChatManager_iOS()
        {
            CallbackManager.Instance();
            listenerGameObj = new GameObject(Obj);
            ChatManagerListener listener = listenerGameObj.AddComponent<ChatManagerListener>();
            listener.delegater = Delegate;
        }

        public override bool DeleteConversation(string conversationId, bool deleteMessages)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("deleteMessages", deleteMessages);
            string ret = ChatManagerNative.ChatManager_GetMethodCall("deleteConversation", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override void DownloadAttachment(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatManagerNative.ChatManager_HandleMethodCall("downloadAttachment", obj.ToString(), handle?.callbackId);
        }

        public override void DownloadThumbnail(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatManagerNative.ChatManager_HandleMethodCall("downloadThumbnail", obj.ToString(), handle?.callbackId);
        }

        public override void FetchHistoryMessages(string conversationId, ConversationType type, string startMessageId = null, int count = 20, ValueCallBack<CursorResult<Message>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(type));
            obj.Add("startMsgId", startMessageId);
            obj.Add("count", count);
            ConversationNative.Conversation_GetMethodCall("fetchHistoryMessages", obj.ToString(), handle?.callbackId);
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(type));
            obj.Add("createIfNeed", createIfNeed);
            string jsonString = ChatManagerNative.ChatManager_GetMethodCall("getConversation", obj.ToString());
            return new Conversation(jsonString);
        }

        public override void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null)
        {
            ChatManagerNative.ChatManager_GetMethodCall("getConversationsFromServer", handle?.callbackId);
        }

        public override int GetUnreadMessageCount()
        {
            string jsonString = ChatManagerNative.ChatManager_GetMethodCall("getUnreadMessageCount");
            Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(jsonString);
            string countString = dict["ret"];
            return int.Parse(countString);
        }

        public override bool ImportMessages(List<Message> messages)
        {
            JSONObject obj = new JSONObject();
            obj.Add("list", TransformTool.JsonStringFromMessageList(messages));
            string ret = ChatManagerNative.ChatManager_GetMethodCall("importMessages", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override List<Conversation> LoadAllConversations()
        {
            string jsonString = ChatManagerNative.ChatManager_GetMethodCall("loadAllConversations");
            return TransformTool.JsonStringToConversationList(jsonString);
        }

        public override Message LoadMessage(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("messageId", messageId);
            string ret = ChatManagerNative.ChatManager_GetMethodCall("getMessage", obj.ToString());
            return new Message(ret);
        }

        public override bool MarkAllConversationsAsRead()
        {
            JSONObject obj = new JSONObject();
            string ret = ChatManagerNative.ChatManager_GetMethodCall("markAllChatMsgAsRead", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override void RecallMessage(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatManagerNative.ChatManager_GetMethodCall("recallMessage", obj.ToString());
        }

        public override Message ResendMessage(string messageId, ValueCallBack<Message> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            string ret = ChatManagerNative.ChatManager_GetMethodCall("resendMessage", obj.ToString());
            return new Message(ret);
        }

        public override List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            JSONObject obj = new JSONObject();
            obj.Add("keywords", keywords);
            obj.Add("from", from);
            obj.Add("count", maxCount);
            obj.Add("timestamp", timestamp);
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            string ret = ChatManagerNative.ChatManager_GetMethodCall("searchChatMsgFromDB", obj.ToString());
            return TransformTool.JsonStringToMessageList(ret);
        }

        public override void SendConversationReadAck(string conversationId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            ChatManagerNative.ChatManager_GetMethodCall("ackConversationRead", obj.ToString(), handle?.callbackId);
        }

        public override Message SendMessage(Message message, CallBack handle = null)
        {
            string ret = ChatManagerNative.ChatManager_GetMethodCall("sendMessage", message.ToJson().ToString());
            return new Message(ret);
        }

        public override void SendMessageReadAck(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatManagerNative.ChatManager_GetMethodCall("ackMessageRead", obj.ToString(), handle?.callbackId);
        }

        public override void UpdateMessage(Message message, CallBack handle = null)
        {
            ChatManagerNative.ChatManager_HandleMethodCall("updateChatMessage", message.ToJson().ToString(), handle?.callbackId);
        }
    }

    class ChatManagerNative
    {
        [DllImport("__Internal")]
        internal extern static void ChatManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string ChatManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}