using System.Collections.Generic;

namespace AgoraChat
{
    internal class ConversationManager
    {
        internal Message LastMessage(string conversationId, ConversationType conversationType)
        {
            return null;
        }

        internal  Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            return null;
        }

        internal Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            return null;
        }

        internal void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            
        }

        internal int UnReadCount(string conversationId, ConversationType conversationType)
        {
            return 0;
        }

        internal int MessagesCount(string conversationId, ConversationType conversationType)
        {
            return 0;
        }

        internal void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {

        }

        internal void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            
        }

        internal bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            return false;
        }

        internal bool AppendMessage(string conversationId, ConversationType conversationType, Message message)
        {
            return false;
        }

        internal bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            return false;
        }

        internal bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            return false;
        }

        internal bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            return false;
        }

        internal Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            return null;
        }

        internal void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {

        }

        internal void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {

        }

        internal void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {

        }

        internal void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {

        }

        internal ConversationManager()
        {
        }
    }
}