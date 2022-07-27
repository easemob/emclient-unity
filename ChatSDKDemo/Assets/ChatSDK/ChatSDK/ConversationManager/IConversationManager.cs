using System;
using System.Collections.Generic;

namespace ChatSDK
{
    /**
         * \~chinese
         * 会话管理器抽象类。
         *
         * \~english
         * The abstract class for the conversation manager.
         * 
         */
    internal abstract class IConversationManager
    {
        private static IConversationManager instance;

        /**
         * \~chinese
         * 获取指定会话的最新消息。
         * 
         * 该方法的调用不影响会话的未读消息数。
         * 
         * SDK 首先在内存中获取最新消息，若在内存中未找到，则从数据库中查询并加载。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
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
         * @param conversationId    The ID of the conversation to which the last message belongs.
         * @param conversationType  The type of the conversation to which the last message belongs. See {@link ConversationType}.
         *
         * @return  The message instance.
         */
        internal abstract Message LastMessage(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 获取指定会话中收到的最新消息。
         * 
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * 
         * @return 消息实例。
         *
         * \~english
         * Gets the latest message received in the conversation.
         * 
         * @param conversationId    The ID of the conversation to which the latest received message belongs.
         * @param conversationType  The type of the conversation to which the latest received message belongs. See {@link ConversationType}.
         * 
         * @return  The message instance.
         */
        internal abstract Message LastReceivedMessage(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 获取指定会话的扩展信息。
         * 
         * 会话的扩展信息只保存在本地内存和数据库，不同步到服务器。
         * 
         * @param conversationId    扩展信息所属会话的 ID。
         * @param conversationType  扩展信息所属会话的类型，详见 {@link ConversationType}。
         *
         * @return                  会话的扩展信息。
         *
         * \~english
         * Gets the extension information of the conversation.
         * 
         * The conversation extension information is stored only in the local memory and local database, but not in the server.
         * 
         * @param conversationId    The ID of the conversation to which the extension information belongs.
         * @param conversationType  The type of the conversation to which the extension information belongs. See {@link ConversationType}.
         *
         * @return                  The extension information of the conversation.
         */
        internal abstract Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 设置指定会话的扩展信息。
         * 
         * 会话的扩展信息只保存在本地内存和数据库，不同步到服务器。
         *
         * @param conversationId    扩展信息所属会话的 ID。
         * @param conversationType  扩展字段所属会话的类型，详见 {@link ConversationType}。
         * @param ext               会话扩展信息。
         *
         * \~english
         * Sets the extension information of the conversation.
         * 
         * The conversation extension information is stored only in the local memory and local database, but not in the server.
         *
         * @param conversationId    The ID of the conversation to which the extension field belongs.
         * @param conversationType  The type of the conversation to which the extension field belongs. See {@link ConversationType}.
         * @param ext               The extension information of the conversation.
         */
        internal abstract void SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext);

        /**
         * \~chinese
         * 获取指定会话的未读消息数。
         * 
         * @param conversationId    会话 ID。
         * @param conversationType  会话类型，详见 {@link ConversationType}。
         * 
         * @return                  未读消息数。
         *
         * \~english
         * Gets the count of unread messages in the conversation.
         * 
         * @param conversationId    The conversation ID.
         * @param conversationType  The conversation type. See {@link ConversationType}.
         * 
         * @return                  The count of unread messages.
         */
        internal abstract int UnReadCount(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 获取本地数据库中指定会话的消息总数。
         * 
         * @param conversationId    会话 ID。
         * @param conversationType  会话类型，详见 {@link ConversationType}。
         * 
         * @return 会话的消息总数。
         *
         * \~english
         * Gets the total number of messages in the conversation in the local database.
         * 
         * @param conversationId    The conversation ID.
         * @param conversationType  The conversation type. See {@link ConversationType}.
         * 
         * @return                  The total number of messages in the conversation.
         */
        internal abstract int MessagesCount(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 设置指定消息为已读。
         * 
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param messageId         消息 ID。 
         *
         * \~english
         * Marks a message as read.
         * 
         * @param conversationId    The ID of the conversation to which the message belongs.
         * @param conversationType  The type of the conversation to which the message belongs. See {@link ConversationType}。
         * @param messageId         The message ID.
         */
        internal abstract void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId);

        /**
         *  \~chinese
         * 将指定会话的所有未读消息设置为已读。
         *  
         * @param conversationId    会话 ID。
         * @param conversationType  会话类型。详见 {@link ConversationType}。
         *
         *  \~english
         * Marks all unread messages in the conversation as read.
         *  
         * @param conversationId    The conversation ID.
         * @param conversationType  The conversation type. See {@link ConversationType}.
         */
        internal abstract void MarkAllMessageAsRead(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 在本地数据库的指定会话中插入一条消息。
         * 
         * 消息的会话 ID 应该和会话的 ID 一致。
         * 
         * 消息会根据消息里的 Unix 时间戳插入本地数据库，SDK 会更新会话的 `latestMessage` 等属性。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param message           消息实例。

         * @return 该消息是否成功插入。
         *         - `true`: 成功；
         *         - `false`: 失败。
         *
         * \~english
         * Inserts a message into a conversation in the local database.
         * 
         * The conversation ID of the message should be the same as that of the conversation to make sure that the message is correctly inserted. 
         * 
         * The message will be inserted based on the Unix timestamp included in it. Upon message insertion, the SDK will automatically update attributes of the conversation, including `latestMessage`.
         * 
         * @param conversationId    The ID of the conversation to which the message belongs.
         * @param conversationType  The type of the conversation to which the message belongs. See {@link ConversationType}.
         * @param message           The message instance.
         *
         * @return Whether the message is successfully inserted.
         *           - `true`: Yes.
         *           - `false`: No.
         */
        internal abstract bool InsertMessage(string conversationId, ConversationType conversationType, Message message);

        /**
        * \~chinese
        * 在本地数据库中指定会话的尾部插入一条消息。
        * 
        * 消息的会话 ID 应与会话的 ID 一致。
        * 
        * 消息插入后，SDK 会自动更新会话的 `latestMessage` 等属性。
        *
        * @param conversationId    消息所属会话的 ID。
        * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
        * @param message           消息实例。
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
        * @param conversationId    The ID of the conversation to which the message belongs.
        * @param conversationType  The type of the conversation to which the message belongs. See {@link ConversationType}.
        * @param message           The message instance.
        *
        * @return Whether the message is successfully inserted.
        *           - `true`: Yes.
        *           - `false`: No.
        */
        internal abstract bool AppendMessage(string conversationId, ConversationType conversationType, Message message);

        /**
         * \~chinese
         * 更新本地数据库的指定消息。
         * 
         * 消息更新时，消息 ID 不会修改。
         * 
         * 消息更新后，SDK 会自动更新会话的 `latestMessage` 等属性。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param message           要更新的消息。
         * 
         * @return 该消息是否成功更新。
         *         - `true`: 成功；
         *         - `false`: 失败。
         *
         * \~english
         * Updates a message in the local database.
         * 
         * The ID of the message cannot be changed during message update.
         * 
         * After a message is updated, the SDK will automatically update attributes of the conversation, including `latestMessage`.
         *
         * @param conversationId    The ID of the conversation to which the message belongs.
         * @param conversationType  The type of the conversation to which the message belongs. See {@link ConversationType}.
         * @param message           The message to be updated.
         * 
         * @return Whether this message is successfully updated.
         *         - `true`: Yes.
         *         - `false`: No.

         */
        internal abstract bool UpdateMessage(string conversationId, ConversationType conversationType, Message message);

        /**
         * \~chinese
         * 删除本地数据库中的指定消息。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param messageId         要删除的消息的 ID。
         * 
         * @return 该消息是否成功删除。
         *          - `true`: 成功；
         *          - `false`: 失败。
         *
         * \~english
         * Deletes a message from the local database.
         *
         * @param conversationId    The ID of the conversation to which the message belongs.
         * @param conversationType  The type of the conversation to which the message belongs. See {@link ConversationType}.
         * @param messageId         The message to be deleted.
         * 
         * @return Whether the message is successfully deleted.
         *         - `true`: Yes.
         *         - `false`: No.

         */
        internal abstract bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId);

        /**
         * \~chinese
         * 删除指定会话中所有消息。
         * 
         * 该方法同时删除指定会话在内存和数据库中的所有消息。
         * 
         * @param conversationId    消息所属会话 ID。
         * @param conversationType  消息所属会话类型，详见 {@link ConversationType}。
         *
         * @return 消息是否成功删除。
         *          - `true`: 成功；
         *          - `false`: 失败。
         *
         * \~english
         * Deletes all the messages of the conversation.
         * 
         * This method deletes all messages of the conversation from both the memory and the local database.
         * 
         * @param conversationId    The ID of the conversation for which all messages will be deleted.
         * @param conversationType  The type of the conversation for which all messages will be deleted. See {@link ConversationType}.
         * 
         * @return Whether messages are successfully deleted.
         *         - `true`: Yes.
         *         - `false`: No.
         */
        internal abstract bool DeleteAllMessages(string conversationId, ConversationType conversationType);

        /**
         * \~chinese
         * 加载指定消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param messageId         要加载的消息的 ID。
         * @return                  返回消息对象。若消息未找到，SDK 返回 `null`。
         *
         * \~english
         * Loads a message.
         * 
         * The SDK first retrieves the message from the memory. If the message is not found, the SDK will retrieve it from the local database and load it.
         *
         * @param conversationId    The ID of the conversation to which the message belongs.
         * @param conversationType  The type of the conversation to which the message belongs. See {@link ConversationType}.
         * @param messageId         The ID of the message to be loaded.
         * @return                  The message instance. If the message is not found, the SDK returns `null`.
         */
        internal abstract Message LoadMessage(string conversationId, ConversationType conversationType, string messageId);

        /**
         * \~chinese
         * 加载特定类型的多条消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param conversationId    消息所属会话 ID。
         * @param conversationType  消息所属会话类型，详见 {@link ConversationType}。
         * @param bodyType          加载消息类型。该参数必填。
         * @param sender            消息发送方的用户 ID。该参数必填。
         * @param timestamp         查询的起始时间戳。默认值为 `-1`，表示当前时间戳。
         * @param count             加载的最大消息数目。默认值为 `20`。
         * @param direction         消息加载方向。默认按消息中的时间戳（{@link SortMessageByServerTime}）的倒序加载，详见 {@link MessageSearchDirection}。
         * @param callback          加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages of a specific type.
         * 
         * The SDK first retrieves the messages from the memory. If no message is not found, the SDK will retrieve them from the local database and load them.
         *
         * @param conversationId    The ID of the conversation to which the messages belong.
         * @param conversationType  The type of the conversation to which the messages belong. See {@link ConversationType}.
         * @param bodyType          The type of messages to load. Ensure that you set this parameter.
         * @param sender            The user ID of the message sender. Ensure that you set this parameter.
         * @param timestamp         The starting Unix timestamp for query. The default value is `-1`, indicating the current Unix timestamp.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param direction         The message loading direction. By default, the SDK loads messages in the reverse chronological order of the Unix timestamp ({@link SortMessageByServerTime}) in the messages. See {@link MessageSearchDirection}.
         * @param callback          The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        internal abstract void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);

        /**
         * \~chinese
         * 从指定消息 ID 开始加载消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param startMessageId    加载的起始消息 ID。若该参数设置为 "" 或者 `null`，SDK 会从最新消息开始加载。
         * @param count             加载的最大消息数目，默认值为 `20`。
         * @param direction         消息加载方向。默认按消息中的时间戳（{@link SortMessageByServerTime}）的倒序加载，详见 {@link MessageSearchDirection}。
         * @param callback          加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages, starting from a specific message ID.
         * 
         * The SDK first retrieves the messages from the memory. If no message is not found, the SDK will retrieve them from the local database and load them.
         *
         * @param conversationId    The ID of the conversation to which the messages belong.
         * @param conversationType  The type of the conversation to which the messages belong. See {@link ConversationType}.
         * @param startMessageId    The starting message ID for query. If this parameter is set as "" or `null`, the SDK will load from the latest message.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param direction         The message loading direction. By default, the SDK loads messages in the reverse chronological order of the Unix timestamp ({@link SortMessageByServerTime}) in the messages. See {@link MessageSearchDirection}.
         * @param callback          The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        internal abstract void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);

        /**
         * \~chinese
         * 根据关键字加载消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         *
         * @param conversationId    消息所属会话 ID。
         * @param conversationType  消息所属会话类型，详见 {@link ConversationType}。
         * @param keywords          查询使用的关键字。
         * @param sender            消息发送方的用户 ID。
         * @param timestamp         查询的起始时间戳。默认值为 `-1`，表示当前 Unix 时间戳。
         * @param count             加载的最大消息数目，默认值为 `20`。
         * @param direction         消息加载方向。默认按消息中的时间戳（{@link SortMessageByServerTime}）的倒序加载，详见 {@link MessageSearchDirection}。
         * @param callback          加载结果回调，成功返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages by keyword.
         * 
         * The SDK first retrieves the messages from the memory. If no message is not found, the SDK will retrieve them from the local database and load them.
         *
         * @param conversationId    The ID of the conversation to which the messages belong.
         * @param conversationType  The type of the conversation to which the messages belong. See {@link ConversationType}.
         * @param keywords          The keyword for query. 
         * @param sender            The user ID of the message sender. 
         * @param timestamp         The starting Unix timestamp for query. The default value is `-1`, indicating the current Unix timestamp.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param direction         The message loading direction. By default, the SDK loads messages in the reverse chronological order of the Unix timestamp ({@link SortMessageByServerTime}) in the messages. See {@link MessageSearchDirection}.
         * @param callback          The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        internal abstract void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null);

        /**
         * \~chinese
         * 从本地数据库中查询指定时间段内一定数量的消息。
         * 
         * SDK 首先在内存中查找消息，若在内存中未找到，SDK 会在本地数据库查询并加载。
         * 
         * @note 若加载的消息数量较大，请注意内存使用情况。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param startTime         查询的起始时间。该参数必填。
         * @param endTime           查询的结束时间。该参数必填。
         * @param count             加载的最大消息条数。默认值为 `20`。
         * @param callback          加载结果回调，成功则返回消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Loads the messages within a period.
         * 
         * The SDK first retrieves the messages from the memory. If no message is not found, the SDK will retrieve them from the local database and load them.
         * 
         * Pay attention to the memory usage when you load a great number of messages.
         *
         * @param conversationId    The ID of the conversation to which the messages belong.
         * @param conversationType  The type of the conversation to which the messages belong. See {@link ConversationType}.
         * @param startTime         The starting Unix timestamp for query. Ensure that you set this parameter.
         * @param endTime           The ending Unix timestamp for query. Ensure that you set this parameter.
         * @param count             The maximum number of messages to load. The default value is `20`.
         * @param callback          The loading result callback. If success, a list of loaded messages are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        internal abstract void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null);

        /**
         * \~chinese
         * 判断会话是否为子区会话。
         *
         * @param conversationId    消息所属会话的 ID。
         * @param conversationType  消息所属会话的类型，详见 {@link ConversationType}。
         * @param return            返回会话是否为子区会话。
         *
         * \~english
         * Check a conversation is thread or not.
         *
         * @param conversationId    The ID of the conversation to which the messages belong.
         * @param conversationType  The type of the conversation to which the messages belong. See {@link ConversationType}.
         * @return                  Return the coversation is thread or not.
         */
        internal abstract bool IsThread(string conversationId, ConversationType conversationType);
    }
}
