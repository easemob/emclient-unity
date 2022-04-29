﻿using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class ChatManager_iOS : IChatManager
    {
        public ChatManager_iOS()
        {
           
        }

        public override bool DeleteConversation(string conversationId, bool deleteMessages)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("deleteMessages", deleteMessages);
            string ret = ChatAPIIOS.ChatManager_GetMethodCall("deleteConversation", obj.ToString());
            JSONNode jn = JSON.Parse(ret);
            return jn["ret"].AsBool;
        }

        public override void DownloadAttachment(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatAPIIOS.ChatManager_HandleMethodCall("downloadAttachment", obj.ToString(), handle?.callbackId);
        }

        public override void DownloadThumbnail(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatAPIIOS.ChatManager_HandleMethodCall("downloadThumbnail", obj.ToString(), handle?.callbackId);
        }

        public override void FetchHistoryMessagesFromServer(string conversationId, ConversationType type, string startMessageId = null, int count = 20, ValueCallBack<CursorResult<Message>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(type));
            obj.Add("startMsgId", startMessageId ?? "");
            obj.Add("count", count);
            string jsonString = obj.ToString();
            ChatAPIIOS.ChatManager_HandleMethodCall("fetchHistoryMessages", jsonString, handle?.callbackId);
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            obj.Add("convType", TransformTool.ConversationTypeToInt(type));
            obj.Add("createIfNeed", createIfNeed);
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("getConversation", obj.ToString());
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return new Conversation(jsonString);
        }

        public override void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null)
        {
            ChatAPIIOS.ChatManager_HandleMethodCall("getConversationsFromServer", null, handle?.callbackId);
        }

        public override int GetUnreadMessageCount()
        {
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("getUnreadMessageCount");
            if (jsonString == null || jsonString.Length == 0) {
                return 0;
            }
            Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(jsonString);
            string countString = dict["ret"];
            return int.Parse(countString);
        }

        public override bool ImportMessages(List<Message> messages)
        {
            JSONObject obj = new JSONObject();
            obj.Add("list", TransformTool.JsonObjectFromMessageList(messages));
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("importMessages", obj.ToString());
            if (jsonString == null || jsonString.Length == 0) {
                return false;
            }
            JSONNode jn = JSON.Parse(jsonString);
            return jn["ret"].AsBool;
        }

        public override List<Conversation> LoadAllConversations()
        {
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("loadAllConversations");
            if (jsonString == null || jsonString.Length == 0) {
                return null;
            }
            return TransformTool.JsonStringToConversationList(jsonString);
        }

        public override Message LoadMessage(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("getMessage", obj.ToString());
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return new Message(jsonString);
        }

        public override bool MarkAllConversationsAsRead()
        {
            JSONObject obj = new JSONObject();
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("markAllChatMsgAsRead", obj.ToString());
            if (jsonString == null || jsonString.Length == 0)
            {
                return false;
            }
            JSONNode jn = JSON.Parse(jsonString);
            return jn["ret"].AsBool;
        }

        public override void RecallMessage(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatAPIIOS.ChatManager_HandleMethodCall("recallMessage", obj.ToString(), handle?.callbackId);
        }

        public override Message ResendMessage(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("resendMessage", obj.ToString(), handle?.callbackId);
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return new Message(jsonString);
        }

        public override List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            JSONObject obj = new JSONObject();
            obj.Add("keywords", keywords);
            obj.Add("from", from ?? "");
            obj.Add("count", maxCount);
            obj.Add("timestamp", timestamp);
            obj.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("searchChatMsgFromDB", obj.ToString());
            return TransformTool.JsonStringToMessageList(jsonString);
        }

        public override void SendConversationReadAck(string conversationId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("convId", conversationId);
            ChatAPIIOS.ChatManager_HandleMethodCall("ackConversationRead", obj.ToString(), handle?.callbackId);
        }

        public override void SendMessage(ref Message message, CallBack handle = null)
        {
            CallbackManager.Instance().tempMsgDict.Add(message.LocalTime.ToString(), message);

            ChatAPIIOS.ChatManager_GetMethodCall("sendMessage", message.ToJson().ToString(), handle?.callbackId);
        }

        public override void SendMessageReadAck(string messageId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("msgId", messageId);
            ChatAPIIOS.ChatManager_GetMethodCall("ackMessageRead", obj.ToString(), handle?.callbackId);
        }

        public override void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null)
        {
            //TODO: Add code
        }

        public override bool UpdateMessage(Message message)
        {
            string jsonString = ChatAPIIOS.ChatManager_GetMethodCall("updateChatMessage", message.ToJson().ToString());
            if (jsonString == null || jsonString.Length == 0) {
                return false;
            }
            JSONObject jsonObject = JSON.Parse(jsonString).AsObject;
            return jsonObject["isLoggedIn"].AsBool;
        }

        public override void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack callback = null)
        {
            //TO-DO
        }

        public override void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack callback = null)
        {
            //TO-DO
        }

        public override void FetchSupportLanguages(ValueCallBack<List<SupportLanguages>> handle = null)
        {
            //TO-DO
        }

        public override void TranslateMessage(ref Message message, List<string> targetLanguages, CallBack handle = null)
        {
            //TO-DO
        }
    }
}