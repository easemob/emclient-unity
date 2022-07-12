using System;
using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK {
    /// <summary>
    /// 会话列表，用于管理消息
    /// </summary>
    public class Conversation
    {

        private IConversationManager manager { get => SDKClient.Instance.ConversationManager; }

        /// <summary>
        /// 会话id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 会话类型
        /// </summary>
        public ConversationType Type { get; }

        /// <summary>
        /// 会话中最后一条消息
        /// </summary>
        public Message LastMessage {
            get => manager.LastMessage(Id, Type);
        }

        /// <summary>
        /// 会话中收到的最后一条消息
        /// </summary>
        public Message LastReceivedMessage {
            get => manager.LastReceivedMessage(Id, Type);
        }

        /// <summary>
        /// 会话扩展
        /// </summary>
        public Dictionary<string, string> Ext {
            get => manager.GetExt(Id, Type);
            set { manager.SetExt(Id, Type, value); }
        }

        /// <summary>
        /// 会话未读数
        /// </summary>
        public int UnReadCount {
            get => manager.UnReadCount(Id, Type);
        }

        /// <summary>
        /// 标记消息为已读
        /// </summary>
        /// <param name="messageId">已读消息id</param>
        public void MarkMessageAsRead(string messageId) {
            manager.MarkMessageAsRead(Id, Type, messageId);
        }

        /// <summary>
        /// 标记会话中所有消息都为已读
        /// </summary>
        public void MarkAllMessageAsRead() {
            manager.MarkAllMessageAsRead(Id, Type);
        }

        /// <summary>
        /// 向会话中插入消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public bool InsertMessage(Message message) {
            return manager.InsertMessage(Id, Type, message);
        }

        /// <summary>
        /// 向会话中添加消息，消息为会话的最后一条
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public bool AppendMessage(Message message) {
            return manager.AppendMessage(Id, Type, message);
        }

        /// <summary>
        /// 更新消息
        /// </summary>
        /// <param name="message">要更新的消息</param>
        /// <returns></returns>
        public bool UpdateMessage(Message message) {
            return manager.UpdateMessage(Id, Type, message);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageId">要删除的消息id</param>
        /// <returns></returns>
        public bool DeleteMessage(string messageId) {
            return manager.DeleteMessage(Id, Type, messageId);
        }

        /// <summary>
        /// 删除会话中的所有消息
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllMessages() {
            return manager.DeleteAllMessages(Id, Type);
        }

        /// <summary>
        /// 根据消息id获取消息
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns></returns>
        public Message LoadMessage(string messageId) {
            return manager.LoadMessage(Id, Type, messageId);
        }

        /// <summary>
        /// 根据类型获取消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="sender">消息发送方</param>
        /// <param name="timestamp">时间范围</param>
        /// <param name="count">获取数量</param>
        /// <param name="direction">获取消息的方向</param>
        /// <param name="handle">返回结果</param>
        public void LoadMessagesWithMsgType(MessageBodyType type, string sender = null, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> handle = null)
        {
            manager.LoadMessagesWithMsgType(Id, Type, type, sender, timestamp, count, direction, handle);
        }

        /// <summary>
        /// 根据消息id获取消息
        /// </summary>
        /// <param name="startMessageId">起始消息id</param>
        /// <param name="count">获取数量</param>
        /// <param name="direction">获取消息的方向</param>
        /// <param name="handle">返回结果</param>
        public void LoadMessages(string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> handle = null) {
            manager.LoadMessages(Id, Type, startMessageId ?? "", count, direction, handle);
        }

        /// <summary>
        /// 根据关键字获取消息
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="sender">消息发送方</param>
        /// <param name="timestamp">时间范围</param>
        /// <param name="count">获取数量</param>
        /// <param name="direction">获取消息的方向</param>
        /// <param name="handle">返回结果</param>
        public void LoadMessagesWithKeyword(string keywords, string sender = null, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> handle = null)
        {
            manager.LoadMessagesWithKeyword(Id, Type, keywords, sender, timestamp, count, direction, handle);
        }

        /// <summary>
        /// 根据起始时间获取消息
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="count">获取数量</param>
        /// <param name="handle">返回结果</param>
        public void LoadMessagesWithTime(long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> handle = null)
        {
            manager.LoadMessagesWithTime(Id, Type, startTime, endTime, count, handle);
        }

        /// <summary>
        /// 获取会话中消息数
        /// </summary>
        /// <returns>消息数</returns>
        public int MessagesCount() {
            return manager.MessagesCount(Id, Type);
        }

        public bool IsThread()
        {
            return manager.IsThread(Id, Type);
        }


        internal Conversation(string jsonString) {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    Id = jo["con_id"].Value;
                    Type = typeFromInt(jo["type"].AsInt);
                }
            } 
        }

        internal Conversation(string id, ConversationType type)
        {
            Id = id;
            Type = type;
        }


        private ConversationType typeFromInt(int intType) {
            ConversationType type = ConversationType.Chat;
            switch (intType) {
                case 0: type = ConversationType.Chat; break;
                case 1: type = ConversationType.Group; break;
                case 2: type = ConversationType.Room; break;
            }
            return type;
        }
    }
}