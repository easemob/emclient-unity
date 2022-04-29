﻿using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IConversationManager
    {
        private static IConversationManager instance;

        public abstract Message LastMessage(string conversationId, ConversationType conversationType);
        public abstract Message LastReceivedMessage(string conversationId, ConversationType conversationType);
        public abstract Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType);
        public abstract void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext);
        public abstract int UnReadCount(string conversationId, ConversationType conversationType);
        public abstract int MessagesCount(string conversationId, ConversationType conversationType);

        public abstract void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId);
        public abstract void MarkAllMessageAsRead(string conversationId, ConversationType conversationType);
        public abstract bool InsertMessage(string conversationId, ConversationType conversationType, Message message);
        public abstract bool AppendMessage(string conversationId, ConversationType conversationType, Message message);
        public abstract bool UpdateMessage(string conversationId, ConversationType conversationType, Message message);
        public abstract bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId);
        public abstract bool DeleteAllMessages(string conversationId, ConversationType conversationType);

        public abstract Message LoadMessage(string conversationId, ConversationType conversationType, string messageId);
        public abstract void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);
        public abstract void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);
        public abstract void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);
        public abstract void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null);
    }
}
