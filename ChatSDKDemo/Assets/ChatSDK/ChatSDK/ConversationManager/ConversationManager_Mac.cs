using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public class ConversationManager_Mac : IConversationManager
    {
        public override bool AppendMessage(string conversationId, ConversationType conversationType, Message message)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            throw new NotImplementedException();
        }

        public override bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            throw new NotImplementedException();
        }

        public override Message LastMessage(string conversationId, ConversationType conversationType)
        {
            throw new NotImplementedException();
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            throw new NotImplementedException();
        }

        public override Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            throw new NotImplementedException();
        }

        public override void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            throw new NotImplementedException();
        }

        public override void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            throw new NotImplementedException();
        }

        public override void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            throw new NotImplementedException();
        }

        public override void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            throw new NotImplementedException();
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            throw new NotImplementedException();
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            throw new NotImplementedException();
        }

        public override void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            throw new NotImplementedException();
        }

        public override int UnReadCount(string conversationId, ConversationType conversationType)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
