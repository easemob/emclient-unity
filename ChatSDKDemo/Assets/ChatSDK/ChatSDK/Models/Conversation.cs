using System;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK {
    public class Conversation
    {

        private IConversationManager manager { get => IConversationManager.Instance; }

        public string Id { get; }

        public ConversationType Type { get; }

        public Message LastMessage {
            get => manager.LastMessage(Id, Type);
        }
        public Message LastReceivedMessage {
            get => manager.LastReceivedMessage(Id, Type);
        }

        public Dictionary<string, string> Ext {
            get => manager.GetExt(Id, Type);
            set { manager.SetExt(Id, Type, value); }
        }
        public int UnReadCount {
            get => manager.UnReadCount(Id, Type);
        }

        public void MarkMessageAsRead(string messageId) {
            manager.MarkMessageAsRead(Id, Type, messageId);
        }
        public void MarkAllMessageAsRead() {
            manager.MarkAllMessageAsRead(Id, Type);
        }
        public bool InsertMessage(Message message) {
            return manager.InsertMessage(Id, Type, message);
        }
        public bool AppendMessage(Message message) {
            return manager.AppendMessage(Id, Type, message);
        }
        public bool UpdateMessage(Message message) {
            return manager.UpdateMessage(Id, Type, message);
        }
        public bool DeleteMessage(string messageId) {
            return manager.DeleteMessage(Id, Type, messageId);
        }
        public bool DeleteAllMessages() {
            return manager.DeleteAllMessages(Id, Type);
        }

        public Message LoadMessage(string messageId) {
            return manager.LoadMessage(Id, Type, messageId);
        }

        public List<Message> LoadMessagesWithMsgType(MessageBodyType type, string sender, int timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP) {
            return manager.LoadMessagesWithMsgType(Id, Type, type, sender, timestamp, count, direction);
        }

        public List<Message> LoadMessages(string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP) {
            return manager.LoadMessages(Id, Type, startMessageId, count, direction);
        }

        public List<Message> LoadMessagesWithKeyword(string keywords, string sender, int timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP) {
            return manager.LoadMessagesWithKeyword(Id, Type, keywords, sender, timestamp, count, direction);
        }

        public List<Message> LoadMessagesWithTime(int startTime, int endTime, int count = 20) {
            return manager.LoadMessagesWithTime(Id, Type, startTime, endTime, count);
        }

        
        internal Conversation(string jsonString) {
            JSONNode jn = JSON.Parse(jsonString);
            if (!jn.IsNull && jn.IsObject) {
                JSONObject jo = jn.AsObject;
                Id = jo["con_id"].Value;
                Type = typeFromInt(jo["type"].AsInt);
            }
        }

        internal Conversation(string id, ConversationType type)
        {
            Id = id;
            Type = type;
        }


        private ConversationType typeFromInt(int intType) {
            ConversationType type = ConversationType.Chat;
            switch (intType) {
                case 0: type = ConversationType.Chat; break;
                case 1: type = ConversationType.Group; break;
                case 2: type = ConversationType.Room; break;
            }
            return type;
        }
    }
}