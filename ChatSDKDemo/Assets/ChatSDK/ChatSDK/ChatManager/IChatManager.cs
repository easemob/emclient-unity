using System.Collections.Generic;
using System;

namespace ChatSDK
{
    public abstract class IChatManager
    {
        /// <summary>
        /// 删除会话
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="deleteMessages">是否同时删除会话中的消息</param>
        /// <returns></returns>
        public abstract bool DeleteConversation(string conversationId, bool deleteMessages = true);

        /// <summary>
        ///下载消息附件
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="handle">返回结果</param>
        public abstract void DownloadAttachment(string messageId, CallBack handle = null);

        /// <summary>
        /// 下载消息缩略图
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="handle">返回结果</param>
        public abstract void DownloadThumbnail(string messageId, CallBack handle = null);

        /// <summary>
        /// 从服务器获取历史消息
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="type">会话类型</param>
        /// <param name="startMessageId">起始消息id</param>
        /// <param name="count">返回数量</param>
        /// <param name="handle">返回结果</param>
        public abstract void FetchHistoryMessagesFromServer(string conversationId, ConversationType type = ConversationType.Chat, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack < CursorResult<Message>> handle = null);

        /// <summary>
        /// 获取会话
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="type">会话类型</param>
        /// <param name="createIfNeed">db中不存在时是否创建</param>
        /// <returns>会话对象</returns>
        public abstract Conversation GetConversation(string conversationId, ConversationType type = ConversationType.Chat, bool createIfNeed = true);

        /// <summary>
        /// 从服务器获取会话列表
        /// </summary>
        /// <param name="handle">返回结果</param>
        public abstract void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null);

        /// <summary>
        /// 获取总未读数
        /// </summary>
        /// <returns>未读数</returns>
        public abstract int GetUnreadMessageCount();

        /// <summary>
        /// 插入消息
        /// </summary>
        /// <param name="messages"></param>
        /// <returns>返回结果</returns>
        public abstract bool ImportMessages(List<Message> messages);

        /// <summary>
        /// 获取所有会话列表
        /// </summary>
        /// <returns>执行结果</returns>
        public abstract List<Conversation> LoadAllConversations();

        /// <summary>
        /// 根据id获取消息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>执行结果</returns>
        public abstract Message LoadMessage(string messageId);

        /// <summary>
        /// 设置所有会话已读
        /// </summary>
        /// <returns>执行结果</returns>
        public abstract bool MarkAllConversationsAsRead();

        /// <summary>
        /// 消息撤回
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="handle">执行结果</param>
        public abstract void RecallMessage(string messageId, CallBack handle = null);

        /// <summary>
        /// 消息重发
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="handle">执行结果</param>
        /// <returns>要发送的消息</returns>
        public abstract Message ResendMessage(string messageId, CallBack handle = null);

        /// <summary>
        /// 从db中搜索消息
        /// </summary>
        /// <param name="keywords">搜索的关键字</param>
        /// <param name="timestamp">起始时间</param>
        /// <param name="maxCount">返回消息数量</param>
        /// <param name="from">消息发送方</param>
        /// <param name="direction">搜索消息方向</param>
        /// <returns>执行结果</returns>
        public abstract List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP);

        /// <summary>
        /// 发送会话已读
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="handle">执行结果</param>
        public abstract void SendConversationReadAck(string conversationId, CallBack handle = null);

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="handle">执行结果</param>
        /// <returns>将要发送的消息</returns>
        public abstract void SendMessage(ref Message message, CallBack handle = null);

        /// <summary>
        /// 发送消息已读回执
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="handle">执行结果</param>
        public abstract void SendMessageReadAck(string messageId, CallBack handle = null);

        public abstract void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null);

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="message">要更新的消息</param>
        /// <returns>执行结果</returns>
        public abstract bool UpdateMessage(Message message);

        /// <summary>
        /// 删除指定时间戳之前的消息
        /// </summary>
        /// <param name="timeStamp">要更新的消息</param>
        /// <param name="handle">执行结果</param>
        public abstract void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack handle = null);

        /// <summary>
        /// 删除服务器段会话信息
        /// </summary>
        /// <param name="conversationId">要删除的会话id</param>
        /// <param name="conversationType">要删除的会话类型</param>
        /// <param name="isDeleteServerMessages">是否删除服务器端该会话的消息</param>
        public abstract void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack handle = null);


        public abstract void FetchSupportLanguages(ValueCallBack<List<SupportLanguages>> handle = null);

        public abstract void TranslateMessage(ref Message message, List<string> targetLanguages, CallBack handle = null);

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="chatManagerDelegate">实现IChatManagerDelegate的对象</param>
        public void AddChatManagerDelegate(IChatManagerDelegate chatManagerDelegate)
        {
            if (!CallbackManager.Instance().chatManagerListener.delegater.Contains(chatManagerDelegate))
            {
                CallbackManager.Instance().chatManagerListener.delegater.Add(chatManagerDelegate);
            }
        }

        /// <summary>
        /// 移除聊天监听
        /// </summary>
        /// <param name="chatManagerDelegate"></param>
        public void RemoveChatManagerDelegate(IChatManagerDelegate chatManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().chatManagerListener.delegater.Contains(chatManagerDelegate))
            {
                CallbackManager.Instance().chatManagerListener.delegater.Remove(chatManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().chatManagerListener.delegater.Clear();
        }

    }
}