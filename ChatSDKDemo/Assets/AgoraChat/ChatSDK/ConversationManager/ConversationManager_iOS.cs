﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;


namespace AgoraChat {
    internal class ConversationManager_iOS : IConversationManager
    {
        internal override bool AppendMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("msg", message.ToJson());
            string ret = ChatAPIIOS.Conversation_GetMethodCall("appendMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        internal override bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            string ret = ChatAPIIOS.Conversation_GetMethodCall("clearAllMessages", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        internal override bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("msgId", messageId);
            string ret = ChatAPIIOS.Conversation_GetMethodCall("removeMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        internal override Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            string ret = ChatAPIIOS.Conversation_GetMethodCall("conversationExt", obj.ToString());
            return TransformTool.JsonStringToDictionary(ret);
        }

        internal override bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("msg", message.ToJson().ToString());
            string ret = ChatAPIIOS.Conversation_GetMethodCall("insertMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        internal override Message LastMessage(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            string jsonString = ChatAPIIOS.Conversation_GetMethodCall("getLatestMessage", obj.ToString());
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return new Message(jsonString);
        }

        internal override Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            string ret = ChatAPIIOS.Conversation_GetMethodCall("getLatestMessageFromOthers", obj.ToString());
            if (ret == null || ret.Length == 0)
            {
                return null;
            }
            return new Message(ret);
        }

        internal override Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("msgId", messageId);
            string ret = ChatAPIIOS.Conversation_GetMethodCall("loadMsgWithId", obj.ToString());
            if (ret == null || ret.Length == 0)
            {
                return null;
            }
            return new Message(ret);
        }

        internal override void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("startId", startMessageId ?? "");
            obj.Add("count", count);
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            ChatAPIIOS.Conversation_HandleMethodCall("loadMsgWithStartId", obj.ToString(), callback?.callbackId);
            
        }

        internal override void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("keywords", keywords ?? "");
            obj.Add("sender", sender ?? "");
            obj.Add("count", count);
            obj.Add("timestamp", timestamp.ToString());
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            ChatAPIIOS.Conversation_HandleMethodCall("loadMsgWithKeywords", obj.ToString(), callback?.callbackId);
        }

        internal override void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("type", TransformTool.MessageBodyTypeToString(bodyType));
            obj.Add("sender", sender ?? "");
            obj.Add("count", count);
            obj.Add("timestamp", timestamp.ToString());
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            ChatAPIIOS.Conversation_HandleMethodCall("loadMsgWithMsgType", obj.ToString(), callback?.callbackId);
        }

        internal override void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("startTime", startTime.ToString());
            obj.Add("endTime", endTime.ToString());
            obj.Add("count", count);
            ChatAPIIOS.Conversation_HandleMethodCall("loadMsgWithTime", obj.ToString(), callback?.callbackId);
        }

        internal override void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            ChatAPIIOS.Conversation_HandleMethodCall("markAllMessagesAsRead", obj.ToString());
        }

        internal override void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("msgId", messageId);
            ChatAPIIOS.Conversation_HandleMethodCall("markMessageAsRead", obj.ToString());
        }

        internal override void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("ext", TransformTool.JsonStringFromDictionary(ext));
            ChatAPIIOS.Conversation_HandleMethodCall("syncConversationExt", obj.ToString());
        }

        internal override int UnReadCount(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            string jsonString = ChatAPIIOS.Conversation_GetMethodCall("getUnreadMsgCount", obj.ToString());
            Dictionary<string, string> dict =  TransformTool.JsonStringToDictionary(jsonString);
            string countString = dict["count"];
            return int.Parse(countString);
        }

        internal override int MessagesCount(string conversationId, ConversationType conversationType)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            string jsonString = ChatAPIIOS.Conversation_GetMethodCall("messageCount", obj.ToString());
            Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(jsonString);
            string countString = dict["count"];
            return int.Parse(countString);
        }

        internal override bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(conversationType));
            obj.Add("msg", message.ToJson());
            string ret = ChatAPIIOS.Conversation_GetMethodCall("updateConversationMessage", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }
    }
}