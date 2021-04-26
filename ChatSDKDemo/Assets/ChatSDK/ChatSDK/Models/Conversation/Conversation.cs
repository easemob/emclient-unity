using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class Conversation
    {
        public abstract string Id { get; }

        public abstract ConversationType Type { get; }

        public abstract Message LastMessage { get; }
        public abstract Message LastReceivedMessage { get; }
        public abstract Dictionary<string, string> Ext { get; set; }
        public abstract int UnReadCount { get; }

        public abstract void MarkMessageAsRead(string messageId);
        public abstract void MarkAllMessageAsRead();
        public abstract bool InsertMessage(Message message);
        public abstract bool AppendMessage(Message message);
        public abstract bool UpdateMessage(Message message);
        public abstract bool DeleteMessage(string messageId);
        public abstract bool DeleteAllMessages();

        public abstract Message LoadMessage(string messageId);
        public abstract List<Message> LoadMessagesWithMsgType(MessageBodyType type, string sender, int timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP);
        public abstract List<Message> LoadMessages(string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP);
        public abstract List<Message> LoadMessagesWithKeyword(string keywords, string sender, int timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP);
        public abstract List<Message> LoadMessagesWithTime(int startTime, int endTime, int count = 20);

        internal abstract Conversation ConversationFromJson(string jsonString);
        internal abstract string ToJson();
    }
}