using System;
using System.Collections.Generic;

namespace ChatSDK
{
    internal abstract class IConversationManager
    {
        private static IConversationManager instance;

        internal abstract Message LastMessage(string conversationId, ConversationType conversationType);
        internal abstract Message LastReceivedMessage(string conversationId, ConversationType conversationType);
        internal abstract Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType);
        internal abstract void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext);
        internal abstract int UnReadCount(string conversationId, ConversationType conversationType);
        internal abstract int MessagesCount(string conversationId, ConversationType conversationType);

        internal abstract void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId);
        internal abstract void MarkAllMessageAsRead(string conversationId, ConversationType conversationType);
        internal abstract bool InsertMessage(string conversationId, ConversationType conversationType, Message message);
        internal abstract bool AppendMessage(string conversationId, ConversationType conversationType, Message message);
        internal abstract bool UpdateMessage(string conversationId, ConversationType conversationType, Message message);
        internal abstract bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId);
        internal abstract bool DeleteAllMessages(string conversationId, ConversationType conversationType);

        internal abstract Message LoadMessage(string conversationId, ConversationType conversationType, string messageId);
        internal abstract void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);
        internal abstract void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);
        internal abstract void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);
        internal abstract void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null);
    }
}
