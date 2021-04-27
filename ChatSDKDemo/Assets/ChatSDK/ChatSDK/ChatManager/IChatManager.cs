using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IChatManager
    {

        internal WeakDelegater<IChatManagerDelegate> Delegate = new WeakDelegater<IChatManagerDelegate>();

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息体</param>
        /// <param name="callBack">结果回调</param>
        /// <returns>将要发送的消息</returns>
        public abstract Message SendMessage(Message message, CallBack callBack = null);

        public abstract Message ResendMessage(Message message, CallBack callBack = null);

        public abstract bool SendMessageReadAck(Message message, CallBack callBack = null);

        public abstract bool SendConversationReadAck(string conversationId, CallBack callback = null);

        public abstract bool RecallMessage(string messageId, CallBack callBack = null);

        public abstract Message LoadMessage(string messageId);

        public abstract Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true);

        public abstract bool MarkAllConversationsAsRead();

        public abstract int GetUnreadMessageCount();

        public abstract void UpdateMessage(Message message, ValueCallBack<Message> callBack = null);

        public abstract void ImportMessage(Message message, ValueCallBack<Message> callback = null);

        public abstract void DownloadAttachment(Message message, ValueCallBack<Message> callBack = null);

        public abstract void DownloadThumbnail(Message message, ValueCallBack<Message> callBack = null);

        public abstract List<Conversation> LoadAllConversations();

        public abstract void GetConversationsFromServer(Message message, ValueCallBack<List<Conversation>> callBack = null);

        public abstract bool DeleteConversation(string conversationId);

        public abstract void FetchHistoryMessages(string conversationId, ValueCallBack<CursorResult<Message>> callBack = null);

        public abstract void SearchMsgFromDB(string keywards, MessageType type, long timestamp = 0, int maxCount = 20, string from = "", MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callBack = null);

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