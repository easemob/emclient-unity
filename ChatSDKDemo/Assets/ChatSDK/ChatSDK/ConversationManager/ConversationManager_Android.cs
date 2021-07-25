using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace ChatSDK {
    public class ConversationManager_Android : IConversationManager
    {

        private AndroidJavaObject wrapper;

        public ConversationManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMConversationWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override bool AppendMessage(string conversationId, ConversationType conversationType, Message message)
        {
            return wrapper.Call<bool>("appendMessage", conversationId, TransformTool.ConversationTypeToInt(conversationType), message.ToJson().ToString());
        }

        public override bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            return wrapper.Call<bool>("deleteAllMessages", conversationId, TransformTool.ConversationTypeToInt(conversationType));
        }

        public override bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            return wrapper.Call<bool>("removeMessage", conversationId, TransformTool.ConversationTypeToInt(conversationType), messageId);
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            string jsonString = wrapper.Call<string>("getExt", conversationId, TransformTool.ConversationTypeToInt(conversationType));
            return TransformTool.JsonStringToDictionary(jsonString);
        }

        public override bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            return wrapper.Call<bool>("insertMessage", conversationId, TransformTool.ConversationTypeToInt(conversationType), message.ToJson().ToString());
        }

        public override Message LastMessage(string conversationId, ConversationType conversationType)
        {
            string jsonString = wrapper.Call<string>("lastMessage", conversationId, TransformTool.ConversationTypeToInt(conversationType));

            if (jsonString == null || jsonString.Length == 0) {    
                return null;
            }

            return new Message(jsonString);
            
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            string jsonString = wrapper.Call<string>("lastReceivedMessage", conversationId, TransformTool.ConversationTypeToInt(conversationType));
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }

            return new Message(jsonString);
        }

        public override Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            string jsonString = wrapper.Call<string>("loadMsgWithId", conversationId, TransformTool.ConversationTypeToInt(conversationType), messageId);
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }

            return new Message(jsonString);
        }

        public override void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            wrapper.Call("loadMsgWithStartId", conversationId, TransformTool.ConversationTypeToInt(conversationType), startMessageId, count, SearchDirectionToString(direction), callback?.callbackId);
        }

        public override void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            wrapper.Call("loadMsgWithKeywords", conversationId, TransformTool.ConversationTypeToInt(conversationType), keywords, sender, timestamp, count, SearchDirectionToString(direction), callback?.callbackId);
        }

        public override void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            string typeString = "txt";
            switch (bodyType) {
                case MessageBodyType.TXT: typeString = "txt"; break;
                case MessageBodyType.LOCATION: typeString = "loc"; break;
                case MessageBodyType.CMD: typeString = "cmd"; break;
                case MessageBodyType.CUSTOM: typeString = "custom"; break;
                case MessageBodyType.FILE: typeString = "file"; break;
                case MessageBodyType.IMAGE: typeString = "img"; break;
                case MessageBodyType.VIDEO: typeString = "video"; break;
                case MessageBodyType.VOICE: typeString = "voice"; break;
            }
            wrapper.Call("loadMsgWithMsgType", conversationId, TransformTool.ConversationTypeToInt(conversationType), typeString, sender, timestamp, count, SearchDirectionToString(direction), callback?.callbackId);
        }

        public override void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            wrapper.Call("loadMsgWithTime", conversationId, TransformTool.ConversationTypeToInt(conversationType), startTime, endTime, count, callback?.callbackId);
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            wrapper.Call("markAllMessagesAsRead", conversationId, TransformTool.ConversationTypeToInt(conversationType));
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            wrapper.Call("markMessageAsRead", conversationId, TransformTool.ConversationTypeToInt(conversationType), messageId);
        }

        public override void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            wrapper.Call("SetExt", conversationId, TransformTool.ConversationTypeToInt(conversationType), TransformTool.JsonStringFromDictionary(ext));
        }

        public override int UnReadCount(string conversationId, ConversationType conversationType)
        {
            return wrapper.Call<int>("unreadCount", conversationId, TransformTool.ConversationTypeToInt(conversationType));
        }

        public override bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            return wrapper.Call<bool>("updateConversationMessage", conversationId, TransformTool.ConversationTypeToInt(conversationType), message.ToJson().ToString());
        }

        private string SearchDirectionToString(MessageSearchDirection direction) {
            if (direction == MessageSearchDirection.UP)
            {
                return "up";
            }
            else {
                return "down";
            }
        }
    }
}
