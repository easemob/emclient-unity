﻿using System.Collections.Generic;
using SimpleJSON;

namespace AgoraChat {
    /**
     * \~chinese
     * 会话类，用于定义单聊会话、群聊会话和聊天室会话。
     * 
     * 每类会话中包含发送和接收的消息。
     * 
     * \~english
     * The conversation class, which defines one-to-one conversations, group conversations, and chat room conversations. 
     * 
     * Each type of conversation involves messages that are sent and received.
     * 
    */
    public class Conversation
    {

        private IConversationManager manager { get => SDKClient.Instance.ConversationManager; }

        /**
         * \~chinese
         * 会话 ID。
         * 
         * \~english
         * The conversation ID.
         * 
        */
        public string Id { get; }

        /**
         * \~chinese
         * 会话类型。
         *
         * \~english
         * The conversation type.
         */
        public ConversationType Type { get; }


        /**
         * \~chinese
         * 判断该会话是否为子区会话。
         * @return 是否为子区会话。
         *
         * \~english
         * Check a conversation is thread or not.
         * @return a conversation is thread or not.
         */
        public bool IsThread { get; internal set; }


        /**
         * \~chinese
         * 获取指定会话的最新消息。
         * 
         * 该方法的调用不影响会话的未读消息数。
         * 
         * SDK 首先从内存中获取最新消息，若在内存中未找到，则从数据库中加载，然后将其存放在内存中。
         *
         * @return 消息实例。
         *
         * \~english
         * Gets the latest message in the conversation.
         * 
         * A call of this method has no impact on the unread message count of the conversation.
         * 
         * The SDK first retrieves the latest message from the memory. If no message is found, the SDK retrieves it from the local database and loads it.
         *
         * @return  The message instance.
         */
        public Message LastMessage {
            get => manager.LastMessage(Id, Type);
        }

        /**
         * \~chinese
         * 获取指定会话中收到的最新消息。
         * 
         * @return 消息实例。
         *
         * \~english
         * Gets the latest message received in the conversation.
         * 
         * @return  The message instance.
         */
        public Message LastReceivedMessage {
            get => manager.LastReceivedMessage(Id, Type);
        }

        /**
         * \~chinese
         * 获取指定会话的扩展信息。
         * 
         * @return  会话扩展信息。
         *
         * \~english
         * Gets the extension information of the conversation.
         * 
         * @return  The extension information of the conversation.
         */
        public Dictionary<string, string> Ext {
            get => manager.GetExt(Id, Type);
            set { manager.SetExt(Id, Type, value); }
        }

        /**
         * \~chinese
         * 获取指定会话的未读消息数。
         * 
         * @return 未读消息数。
         *
         * \~english
         * Gets the count of unread messages in the conversation.
         * 
         * @return The count of unread messages.
         */
        public int UnReadCount {
            get => manager.UnReadCount(Id, Type);
        }

        /**
         * \~chinese
         * 设置指定消息为已读。
         *
         * @param messageId 消息 ID。 
         *
         * \~english
         * Marks a message as read.
         * 
         * @param messageId The message ID.
         */
        public void MarkMessageAsRead(string messageId) {
            manager.MarkMessageAsRead(Id, Type, messageId);
        }

        /**
         * \~chinese
         * 将指定会话的所有未读消息设置为已读。
         *
         * \~english
         * Marks all unread messages in the conversation as read.
         */
        public void MarkAllMessageAsRead() {
            manager.MarkAllMessageAsRead(Id, Type);
        }

        /**
         * \~chinese
         * 在本地数据库的指定会话中插入一条消息。
         * 
         * 消息的会话 ID 应该和会话的 ID 一致。
         * 
         * 消息会根据消息里的 Unix 时间戳被插入本地数据库，SDK 会更新会话的 `latestMessage` 等属性。
         * 
         * @param message 消息实例。
         * 
         * @return 该消息是否成功插入。
         *         - `true`: 成功；
         *         - `false`: 失败。
         *
         * \~english
         * Inserts a message into a conversation in the local database.
         * 
         * To insert the message correctly, ensure that the conversation ID of the message is the same as that of the conversation. 
         * 
         * The message will be inserted based on the Unix timestamp included in it. Upon message insertion, the SDK will automatically update attributes of the conversation, including `latestMessage`.
         *
         * @param message  The message instance.
         * 
         * @return Whether the message is successfully inserted.
         *           - `true`: Yes.
         *           - `false`: No.
         * 
         */
        public bool InsertMessage(Message message) {
            return manager.InsertMessage(Id, Type, message);
        }

        /**
         * \~chinese
         * 在本地数据库中指定会话的尾部插入一条消息。
         * 
         * 消息的会话 ID 应与会话的 ID 一致。
         * 
         * 消息插入后，SDK 会自动更新会话的 `latestMessage` 等属性。
         *
         * @param message 消息实例。
         * 
         * @return 该消息是否成功插入。
         *         - `true`: 成功；
         *         - `false`: 失败。
         *
         * \~english
         * Inserts a message to the end of a conversation in the local database.
         * 
         * The conversation ID of the message should be the same as that of the conversation to make sure that the message is correctly inserted.
         * 
         * After a message is inserted, the SDK will automatically update attributes of the conversation, including `latestMessage`.
         *
         * @param message The message instance.
         * 
         * @return Whether the message is successfully inserted.
         *         - `true`: Yes.
         *         - `false`: No.
         *
         */
        public bool AppendMessage(Message message) {
            return manager.AppendMessage(Id, Type, message);
        }

        /**
         * \~chinese
         * 更新本地数据库的指定消息。
         * 
         * 消息更新时，消息 ID 不会修改。
         * 
         * 消息更新后，SDK 会自动更新会话的 `latestMessage` 等属性。
         *
         * @param message 要更新的消息。
         * 
         * @return 该消息是否成功更新。
         *         - `true`: 成功；
         *         - `false`: 失败。
         *  
         * \~english
         * Updates a message in the local database.
         * 
         * The ID of the message remains unchanged during message updates.
         * 
         * After a message is updated, the SDK will automatically update attributes of the conversation, including `latestMessage`.
         *
         * @param message  The message to be updated.
         * 
         * @return Whether this message is successfully updated.
         *         - `true`: Yes.
         *         - `false`: No.
         */
        public bool UpdateMessage(Message message) {
            return manager.UpdateMessage(Id, Type, message);
        }

        /**
         * \~chinese
         * 删除本地数据库中的一条指定消息。
         *
         * @param messageId     要删除消息的 ID。
         * 
         * @return 该消息是否成功删除。
         *          - `true`: 成功；
         *          - `false`: 失败。
         * 
         * \~english
         * Deletes a message from the local database.
         *
         * @param messageId    The ID of the message to be deleted.
         * 
         * @return Whether the message is successfully deleted.
         *           - `true`: Yes.
         *           - `false`: No.
         */
        public bool DeleteMessage(string messageId) {
            return manager.DeleteMessage(Id, Type, messageId);
        }

        /**
         * \~chinese
         * 删除指定会话中所有消息。
         * 
         * 该方法同时删除指定会话在内存和数据库中的所有消息。
         * 
         * @return 消息是否成功删除。
         *          - `true`: 成功；
         *          - `false`: 失败。
         *
         * \~english
         * Deletes all the messages in the conversation.
         * 
         * This method deletes all messages in the conversation from both the memory and the local database.
         * 
         * @return Whether messages are successfully deleted.
         *         - `true`: Yes.
         *         - `false`: No.
         */
        public bool DeleteAllMessages() {
            return manager.DeleteAllMessages(Id, Type);
        }

        /**
         * \~chinese
         * 加载指定消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param messageId         要加载的消息的 ID。
         * @return                  返回对象消息。若在本地内存和数据库均未找到消息，返回 `null`。
         *
         * \~english
         * Loads a message.
         * The SDK first retrieves the message from the memory. If no message is found, the SDK retrieves it from the local database and loads it.
         *
         * @param messageId         The ID of the message to load.
         * @return                  The message instance. If the message is not found in both the local memory and local database, the SDK returns `null`.
         */
        public Message LoadMessage(string messageId) {
            return manager.LoadMessage(Id, Type, messageId);
        }

        /**
         * \~chinese
         * 加载特定类型的多条消息。
         * 
         * SDK 首先在内存中查询消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param type              加载消息类型。该参数必填。
         * @param sender            消息发送方的用户 ID。该参数必填。
         * @param timestamp         查询的起始 Unix 时间戳。默认值为 `-1`，表示当前 Unix 时间戳。
         * @param count             加载的最大消息数目。默认值为 `20`。
         * @param direction         消息加载方向。默认按消息中的时间戳（{@link SortMessageByServerTime}）的倒序加载，详见 {@link MessageSearchDirection}。
         * @param callback          加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages of a specific type.
         * 
         * The SDK first retrieves the messages from the memory. If no message is found, the SDK will retrieve them from the local database and load them.
         *
         * @param type              The type of messages to load. Ensure that you set this parameter.
         * @param sender            The user ID of the message sender. Ensure that you set this parameter.
         * @param timestamp         The starting Unix timestamp for query. The default value is `-1`, indicating the current Unix timestamp.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param direction         The message loading direction. By default, the SDK loads messages in the reverse chronological order of the Unix timestamp ({@link SortMessageByServerTime}) in the message. See {@link MessageSearchDirection}.
         * @param callback          The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void LoadMessagesWithMsgType(MessageBodyType type, string sender = null, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> handle = null)
        {
            manager.LoadMessagesWithMsgType(Id, Type, type, sender, timestamp, count, direction, handle);
        }

        /**
         * \~chinese
         * 从指定消息 ID 开始加载消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param startMessageId    加载的起始消息 ID。若该参数设置为 "" 或者 `null`，将从最近的消息开始加载。
         * @param count             加载的最大消息数目。默认值为 `20`。
         * @param direction         消息加载方向。默认按消息中的时间戳（{@link SortMessageByServerTime}）的倒序加载，详见 {@link MessageSearchDirection}。
         * @param handle            加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages, starting from a specific message ID.
         *
         * The SDK first retrieves the messages from the memory. If no message is found, the SDK will retrieve them from the local database and load them.
         *
         * @param startMessageId    The starting message ID for loading. If this parameter is set as "" or `null`, the SDK will load from the latest message.
         * @param count             The maximum number of messages to load. The default value is `20`.
           @param direction         The message loading direction. By default, the SDK loads messages in the reverse chronological order of the Unix timestamp ({@link SortMessageByServerTime}) in the messages. See {@link MessageSearchDirection}.
         * @param handle            The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void LoadMessages(string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> handle = null) {
            manager.LoadMessages(Id, Type, startMessageId ?? "", count, direction, handle);
        }

        /**
         * \~chinese
         * 根据关键字加载消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param keywords          查询使用的关键字。
         * @param sender            消息发送方的用户 ID。
         * @param timestamp         查询的起始时间戳。默认值为 `-1`，表示当前时间戳。
         * @param count             加载的最大消息数目，默认值为 `20`。
         * @param direction         消息加载方向。默认按消息中的时间戳的倒序加载，详见 {@link MessageSearchDirection}。
         * @param callback          加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages by keywords.
         * 
         * The SDK first retrieves the messages from the memory. If no message is found, the SDK will retrieve them from the local database and load them.
         *
         * @param keywords          The keywords for query.
         * @param sender            The user ID of the message sender.
         * @param timestamp         The starting Unix timestamp for query. The default value is `-1`, indicating the current Unix timestamp.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param direction         The message loading direction. By default, the SDK loads messages in the reverse chronological order of the Unix timestamp ({@link SortMessageByServerTime}) in the messages. See {@link MessageSearchDirection}.
         * @param callback          The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void LoadMessagesWithKeyword(string keywords, string sender = null, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> handle = null)
        {
            manager.LoadMessagesWithKeyword(Id, Type, keywords, sender, timestamp, count, direction, handle);
        }

        /**
         * \~chinese
         * 加载指定时间段内的消息。
         * 
         * **注意**
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         * 
         * 若加载的消息数量较大，请注意内存使用情况。
         *
         * @param startTimeStamp   查询的起始时间戳。
         * @param endTimeStamp     查询的结束时间戳。
         * @param count            加载的最大消息条数。
         * @param handle           加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages within a period.
         * 
         * **Note**
         * 
         * The SDK first retrieves the messages from the memory. If no message is found, the SDK will retrieve them from the local database and load them.
         * 
         * Pay attention to the memory usage when you load a great number of messages.
         *
         * @param startTimeStamp    The starting Unix timestamp for query.
         * @param endTimeStamp      The ending Unix timestamp for query.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param handle            The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void LoadMessagesWithTime(long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> handle = null)
        {
            manager.LoadMessagesWithTime(Id, Type, startTime, endTime, count, handle);
        }

	/**
	 * \~chinese
	 * 获取 SDK 本地数据库中会话的全部消息数目。
	 * @return 会话的全部消息数量。
	 *
	 * \~english
	 * Gets the count of all messages in this conversation in the local database.
	 * @return The count of all the messages in this conversation.
	 */
        public int MessagesCount() {
            return manager.MessagesCount(Id, Type);
        }


        internal Conversation(string jsonString) {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    Id = jo["con_id"].Value;
                    Type = typeFromInt(jo["type"].AsInt);
                    IsThread = jo["isThread"].AsBool;
                }
            } 
        }

        internal Conversation(string id, ConversationType type, bool isThread)
        {
            Id = id;
            Type = type;
            IsThread = isThread;
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