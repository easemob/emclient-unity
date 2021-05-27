using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IConversationManager
    {

        private static IConversationManager instance;
        public static IConversationManager Instance
        {
            get
            {
                if (instance == null)
                {
#if UNITY_ANDROID
                    instance = new ConversationManager_Android();
#elif UNITY_IOS
                    //instance = new Client_iOS();
#elif UNITY_STANDALONE_OSX
                    //instance = new Client_Mac();
#elif UNITY_STANDALONE_WIN
                    //instance = new Client_Win();
#endif
                }

                return instance;
            }
        }

        public abstract Message LastMessage(string conversationId, ConversationType converationType);
        public abstract Message LastReceivedMessage(string conversationId, ConversationType converationType);
        public abstract Dictionary<string, string> GetExt(string conversationId, ConversationType converationType);
        public abstract void SetExt(string conversationId, ConversationType converationType, Dictionary<string, string> ext);
        public abstract int UnReadCount(string conversationId, ConversationType converationType);

        public abstract void MarkMessageAsRead(string conversationId, ConversationType converationType, string messageId);
        public abstract void MarkAllMessageAsRead(string conversationId, ConversationType converationType);
        public abstract bool InsertMessage(string conversationId, ConversationType converationType, Message message);
        public abstract bool AppendMessage(string conversationId, ConversationType converationType, Message message);
        public abstract bool UpdateMessage(string conversationId, ConversationType converationType, Message message);
        public abstract bool DeleteMessage(string conversationId, ConversationType converationType, string messageId);
        public abstract bool DeleteAllMessages(string conversationId, ConversationType converationType);

        public abstract Message LoadMessage(string conversationId, ConversationType converationType, string messageId);
        public abstract List<Message> LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP);
        public abstract List<Message> LoadMessages(string conversationId, ConversationType converationType, string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP);
        public abstract List<Message> LoadMessagesWithKeyword(string conversationId, ConversationType converationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP);
        public abstract List<Message> LoadMessagesWithTime(string conversationId, ConversationType converationType, long startTime, long endTime, int count = 20);
    }
}
