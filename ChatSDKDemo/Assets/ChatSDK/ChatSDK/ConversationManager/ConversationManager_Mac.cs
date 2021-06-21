using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public class ConversationManager_Mac : IConversationManager
    {
        public override bool AppendMessage(string conversationId, ConversationType converationType, Message message)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAllMessages(string conversationId, ConversationType converationType)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteMessage(string conversationId, ConversationType converationType, string messageId)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GetExt(string conversationId, ConversationType converationType)
        {
            throw new NotImplementedException();
        }

        public override bool InsertMessage(string conversationId, ConversationType converationType, Message message)
        {
            throw new NotImplementedException();
        }

        public override Message LastMessage(string conversationId, ConversationType converationType)
        {
            throw new NotImplementedException();
        }

        public override Message LastReceivedMessage(string conversationId, ConversationType converationType)
        {
            throw new NotImplementedException();
        }

        public override Message LoadMessage(string conversationId, ConversationType converationType, string messageId)
        {
            throw new NotImplementedException();
        }

        public override List<Message> LoadMessages(string conversationId, ConversationType converationType, string startMessageId, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new NotImplementedException();
        }

        public override List<Message> LoadMessagesWithKeyword(string conversationId, ConversationType converationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new NotImplementedException();
        }

        public override List<Message> LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            throw new NotImplementedException();
        }

        public override List<Message> LoadMessagesWithTime(string conversationId, ConversationType converationType, long startTime, long endTime, int count = 20)
        {
            throw new NotImplementedException();
        }

        public override void MarkAllMessageAsRead(string conversationId, ConversationType converationType)
        {
            throw new NotImplementedException();
        }

        public override void MarkMessageAsRead(string conversationId, ConversationType converationType, string messageId)
        {
            throw new NotImplementedException();
        }

        public override void SetExt(string conversationId, ConversationType converationType, Dictionary<string, string> ext)
        {
            throw new NotImplementedException();
        }

        public override int UnReadCount(string conversationId, ConversationType converationType)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateMessage(string conversationId, ConversationType converationType, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
