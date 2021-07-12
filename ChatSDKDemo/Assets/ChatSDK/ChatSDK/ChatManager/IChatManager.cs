using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IChatManager
    {

        internal WeakDelegater<IChatManagerDelegate> Delegate = new WeakDelegater<IChatManagerDelegate>();

        public abstract bool DeleteConversation(string conversationId, bool deleteMessages = true);

        public abstract void DownloadAttachment(string messageId, CallBack handle = null);

        public abstract void DownloadThumbnail(string messageId, CallBack handle = null);

        public abstract void FetchHistoryMessages(string conversationId, ConversationType type = ConversationType.Chat, string startMessageId = null, int count = 20, ValueCallBack<CursorResult<Message>> handle = null);

        public abstract Conversation GetConversation(string conversationId, ConversationType type = ConversationType.Chat, bool createIfNeed = true);

        public abstract void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null);

        public abstract int GetUnreadMessageCount();

        public abstract bool ImportMessages(List<Message> messages);

        public abstract List<Conversation> LoadAllConversations();

        public abstract Message LoadMessage(string messageId);

        public abstract bool MarkAllConversationsAsRead();

        public abstract void RecallMessage(string messageId, CallBack handle = null);

        public abstract Message ResendMessage(string messageId, ValueCallBack<Message> handle = null);

        public abstract List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP);

        public abstract void SendConversationReadAck(string conversationId, CallBack handle = null);

        public abstract Message SendMessage(Message message, CallBack handle = null);

        public abstract void SendMessageReadAck(string messageId, CallBack handle = null);

        public abstract void UpdateMessage(Message message, CallBack handle = null);

        public void AddChatManagerDelegate(IChatManagerDelegate chatManagerDelegate)
        {
            Delegate.Add(chatManagerDelegate);
        }

        internal void ClearDelegates()
        {
            Delegate.Clear();
        }

    }
}