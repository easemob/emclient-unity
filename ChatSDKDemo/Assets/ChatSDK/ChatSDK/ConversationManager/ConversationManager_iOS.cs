using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;


namespace ChatSDK {
    public class ConversationManager_iOS : IConversationManager
    {
        public override bool AppendMessage(string conversationId, ConversationType converationType, Message message)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("msg", message.ToJsonString());
            string ret = ConversationNative.Conversation_GetMethodCall("appendMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override bool DeleteAllMessages(string conversationId, ConversationType converationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            string ret = ConversationNative.Conversation_GetMethodCall("deleteAllMessages", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override bool DeleteMessage(string conversationId, ConversationType converationType, string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("msgId", messageId);
            string ret = ConversationNative.Conversation_GetMethodCall("deleteAllMessages", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType converationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            string ret = ConversationNative.Conversation_GetMethodCall("conversationExt", obj.ToString());
            return TransformTool.JsonStringToDictionary(ret);
        }

        public override bool InsertMessage(string conversationId, ConversationType converationType, Message message)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("msg", message.ToJsonString());
            string ret = ConversationNative.Conversation_GetMethodCall("insertMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override Message LastMessage(string conversationId, ConversationType converationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            string ret = ConversationNative.Conversation_GetMethodCall("getLatestMessage", obj.ToString());
            return new Message(ret);
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType converationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            string ret = ConversationNative.Conversation_GetMethodCall("getLatestMessageFromOthers", obj.ToString());
            return new Message(ret);
        }

        public override Message LoadMessage(string conversationId, ConversationType converationType, string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("msgId", messageId);
            string ret = ConversationNative.Conversation_GetMethodCall("loadMsgWithId", obj.ToString());
            return new Message(ret);
        }

        public override List<Message> LoadMessages(string conversationId, ConversationType converationType, string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("startId", startMessageId);
            obj.Add("count", count);
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            string messageListString = ConversationNative.Conversation_GetMethodCall("loadMsgWithStartId", obj.ToString());
            return TransformTool.JsonStringToMessageList(messageListString);
        }

        public override List<Message> LoadMessagesWithKeyword(string conversationId, ConversationType converationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("keywords", keywords);
            obj.Add("sender", sender);
            obj.Add("count", count);
            obj.Add("timestamp", timestamp);
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            string messageListString = ConversationNative.Conversation_GetMethodCall("loadMsgWithKeywords", obj.ToString());
            return TransformTool.JsonStringToMessageList(messageListString);
        }

        public override List<Message> LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("type", TransformTool.MessageBodyTypeToString(bodyType));
            obj.Add("sender", sender);
            obj.Add("count", count);
            obj.Add("timestamp", timestamp);
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            string messageListString = ConversationNative.Conversation_GetMethodCall("loadMsgWithMsgType", obj.ToString());
            return TransformTool.JsonStringToMessageList(messageListString);
        }

        public override List<Message> LoadMessagesWithTime(string conversationId, ConversationType converationType, long startTime, long endTime, int count = 20)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("startTime", startTime);
            obj.Add("endTime", endTime);
            obj.Add("count", count);
            string messageListString = ConversationNative.Conversation_GetMethodCall("loadMsgWithTime", obj.ToString());
            return TransformTool.JsonStringToMessageList(messageListString);
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType converationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            ConversationNative.Conversation_HandleMethodCall("markAllMessagesAsRead", obj.ToString());
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType converationType, string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("msgId", messageId);
            ConversationNative.Conversation_HandleMethodCall("markMessageAsRead", obj.ToString());
        }

        public override void SetExt(string conversationId, ConversationType converationType, Dictionary<string, string> ext)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("ext", TransformTool.JsonStringFromDictionary(ext));
            ConversationNative.Conversation_HandleMethodCall("syncConversationExt", obj.ToString());
        }

        public override int UnReadCount(string conversationId, ConversationType converationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            string jsonString = ConversationNative.Conversation_GetMethodCall("getUnreadMsgCount", obj.ToString());
            Dictionary<string, string> dict =  TransformTool.JsonStringToDictionary(jsonString);
            string countString = dict["count"];
            return int.Parse(countString);
        }

        public override bool UpdateMessage(string conversationId, ConversationType converationType, Message message)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(converationType));
            obj.Add("msg", message.ToJsonString());
            string ret = ConversationNative.Conversation_GetMethodCall("updateConversationMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }
    }

    class ConversationNative
    {
        [DllImport("__Internal")]
        internal extern static void Conversation_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string Conversation_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}