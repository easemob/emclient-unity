using System.Collections.Generic;
using System;

namespace AgoraChat
{
	   /**
        * \~chinese
        * 聊天管理器抽象类。
        * \~english
        * The abstract class for the chat manager.
        */
    public abstract class IChatManager
    {
       /**
        * \~chinese
        * 删除本地数据库中的指定会话及其历史消息。
		* 
        * 若将 `deleteMessages` 设置为 `true`，删除会话的同时也会删除该会话的本地历史消息。
        *
        * @param conversationId 会话 ID。
        * @param deleteMessages 是否同时删除本地历史消息：
		*                       - `true` ：是；
		*                       - `false` ：否。
		* @return               会话是否成功删除：
		*                       - `true` ：是；
		*                       - `false` ：否。
        *
        * \~english
        * Deletes a conversation from the local database.
        * 
        * If you set `deleteMessages` to `true`, local historical messages will be deleted with the conversation.
        *
        * @param conversationId     The conversation ID.
        * @param deleteMessages 	Whether to delete local historical messages with the conversation.
        *                           - `true`: Yes.
		*                           - `false`: No.
		*
		* @return 					Whether the conversation is successfully deleted:
		*                           - `true`: Yes. 
		*                           - `false`: No.
        */
        public abstract bool DeleteConversation(string conversationId, bool deleteMessages = true);

        /**
	     * \~chinese
	     * 下载消息的附件。
	     * 
	     * 若附件自动下载失败，也可以调用此方法下载。
	     *
	     * @param messageId	要下载附件的消息 ID。
	     * @param handle    下载结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Downloads the message attachment.
	     * 
	     * You can also call this method if the attachment fails to be downloaded automatically.
	     *
	     * @param messageId The ID of the message with the attachment to be downloaded.
	     * @param handle    The download status callback. See {@link CallBack}.
	     */
        public abstract void DownloadAttachment(string messageId, CallBack handle = null);

        /**
	     * \~chinese
	     * 下载消息的缩略图。
		 * 
		 * 若消息缩略图自动下载失败，也可以调用该方法下载。
	     *
	     * @param messageId 要下载缩略图的消息 ID，一般图片消息和视频消息有缩略图。
	     * @param handle    下载结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Downloads the message thumbnail.
		 * 
	     * You can also call this method if the message thumbnail fails to be downloaded automatically.
		 * 
	     * @param messageId The ID of the message with the thumbnail to be downloaded.
	     * @param handle    The download status callback. See {@link CallBack}.
	     */
        public abstract void DownloadThumbnail(string messageId, CallBack handle = null);

        /**
	     * \~chinese
	     * 从服务器分页获取历史消息。
	     *
	     * 异步方法。
	     *
	     * @param conversationId 		会话 ID。
	     * @param type 					会话类型，详见 {@link ConversationType}。	     
	     * @param startMessageId 		查询的起始消息 ID。该参数设置后，SDK 从指定的消息 ID 开始，按消息检索方向获取。如果传入消息的 ID 为空，SDK 忽略该参数，按搜索方向查询消息。
	     *                              - 若 `direction` 为 `UP`，SDK 从最新消息开始，按照服务器接收消息时间的逆序获取；
         *                              - 若 `direction` 为 `DOWN`，SDK 从最早消息开始，按照服务器接收消息时间的正序获取。
	     * @param count 				每页期望返回的的消息条数。
	     * @param direction     		消息获取的方向。
		 *                              - （默认）`UP`：按照服务器接收消息时间的逆序获取；
	     *                              - `DOWN`：按照服务器接收消息时间的正序获取。
	     * @param handle                结果回调，返回消息列表。
	     *
	     * \~english
	     * Uses the pagination to get historical messages of the conversation from the server.
	     *
	     * This is an asynchronous method.
	     *
	     * @param conversationId 		The conversation ID.
	     * @param type 					The conversation type. See {@link ConversationType}.
	     * @param startMessageId 		The starting message ID for query. After this parameter is set, the SDK retrieves messages, starting from the specified one, according to the message search direction.
	     *                              If this parameter is set as "null" or an empty string, the SDK retrieves messages according to the message search direction while ignoring this parameter. 
	     *                              - If `direction` is set as `UP`, the SDK retrieves messages, starting from the latest one, in the reverse chronological order of when the server receives them.
         *                              - If `direction` is set as `DOWN`, the SDK retrieves messages, starting from the oldest one, in the chronological order of the time the server receives them.
	     * @param count 				The number of messages that you expect to get on each page.
	     * @param direction     		The message search direction:
         *                   			- (Default) `UP`: The SDK retrieves messages in the reverse chronological order of when the server receives them;
         *                  			- `DOWN`: The SDK retrieves messages in the chronological order of the time the server receives them.
	     * @param handle				The result callback. Returns the list of obtained messages.
	     */
        public abstract void FetchHistoryMessagesFromServer(string conversationId, ConversationType type = ConversationType.Chat, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack < CursorResult<Message>> handle = null);

        /**
	     * \~chinese
	     * 获取本地指定会话对象。
	     * 
	     * @param conversationId    会话 ID。
	     * @param type              会话类型，详见 {@link ConversationType}。
	     * @param createIfNeed      本地数据库中未找到相应会话时是否自动创建。
	     *                          - `true`：是；
	     *                          - `false`：否。
	     * @return                  根据指定会话 ID 找到的会话对象。未找到会话会返回空值。
	     *
	     * \~english
	     * Gets the local conversation object.
	     *
	     * @param conversationId 	The conversation ID.
	     * @param type              The conversation type. See {@link ConversationType}.
	     * @param createIfNeed      Whether to automatically create a conversation if the conversation is not found. 
	     *                          - `true`: Yes.
	     *                          - `false`: No.
	     * @return 		            The conversation found. Returns `null` if the conversation is not found.
	     */
        public abstract Conversation GetConversation(string conversationId, ConversationType type = ConversationType.Chat, bool createIfNeed = true);

		/**
	     * \~chinese
	     * 获取本地指定子区会话对象。
	     * 
	     * @param threadId			子区 ID。
	     * 
	     * @return                  根据指定会话 ID 获取子区会话对象。
	     *
	     * \~english
	     * Gets the local thread conversation object.
	     *
	     * @param threadId 			The threadId ID.
	     * 
	     * @return 		            The conversation found. Returns `null` if the conversation is not found.
	     */
		public abstract Conversation GetThreadConversation(string threadId);

        /**
	     * \~chinese
	     * 从服务器获取所有会话对象。
	     * 
	     * 未找到任何会话对象返回的列表为空。
	     * 
	     * @param handle    获取的会话列表，详见 {@link ValueCallBack}。
	     *
	     * \~english
	     * Gets the all conversations from the server.
	     *
	     * @param handle    The list of obtained conversations. See {@link ValueCallBack}. An empty list will be returned if no conversation is found.
	     */
        public abstract void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null);

        /**
	     * \~chinese
	     * 获取未读消息数。
		 * 
	     * @return		未读消息数。
	     *
	     *
	     * \~english
	     * Gets the unread message count.
	     * @return		The count of unread messages.
	     *
	     */
        public abstract int GetUnreadMessageCount();

        /**
	     * \~chinese
	     * 将消息导入本地数据库。
		 *
	     * 你只能将你发送或接受的消息导入本地数据库。
	     *
	     * @param messages 要导入数据库的消息。
         * 
		 * @return  消息是否成功导入本地数据库。
		 *           - `true`: 成功；
         *           - `false`: 失败。

	     *
	     * \~english
	     * Imports messages to the local database.
	     * 
	     * You can only import messages that you sent or received.
	     *
	     * @param messages The messages to be imported.
		 * 
		 * @return Whether messages are successfully imported to the local database.
		 *         - `true`: Yes.
         *         - `false`: No.

	     */
        public abstract bool ImportMessages(List<Message> messages);

        /**
	     * \~chinese
	     * 将本地数据库中的所有会话加载到内存。
		 * 
	     * 一般在登录成功后使用此方法，可以加快会话列表的加载速度。
	     * 
	     * @return		加载的会话列表。
	     *
	     * \~english
	     * Loads all conversations from the local database into the memory.
		 * 
	     * Generally, this method is called upon successful login to speed up the loading of the conversation list.
	     * 
	     * @return      The list of loaded conversations.
	     */
        public abstract List<Conversation> LoadAllConversations();

		/**
	     * \~chinese
	     *  将本地数据库中的指定消息加载到内存。
	     * 
	     * @param messageId		需加载的消息的 ID。
	     * @return				加载的消息对象。
	     *
	     * \~english
	     * Loads a specified message from the local database into the memory.
	     * 
	     * @param messageId		The ID of the message to be loaded.
	     * @return				The loaded message object.
	     */
		public abstract Message LoadMessage(string messageId);

		/**
		 * \~chinese
		 * 将本地所有会话设置为已读。
		 * 
		 *  @return		所有会话是否成功设置为已读。
		 *              - `true`：是；
		 *              - `false`：否。
		 *
		 * \~english
		 * Marks all local conversations as read.
		 * 
		 *  @return	 Whether all local conversations are marked as read. 
		 *           - `true`: Yes. 
		 *           - `false`: No.
		 */
		public abstract bool MarkAllConversationsAsRead();

		/**
		 * \~chinese
		 * 撤回已发送的消息。
		 *
		 * 异步方法。
		 *
		 * @param messageId 要撤回消息的 ID。
		 * @param handle    撤回结果回调，详见 {@link CallBack}。
		 *
		 *
		 * \~english
		 * Recalls the message.
		 *
		 * This is an asynchronous method.
		 *
		 * @param message	The ID of the message to be recalled.
		 * @param handle    The recall status callback. See {@link CallBack}.
		 */
		public abstract void RecallMessage(string messageId, CallBack handle = null);

		/**
		 * \~chinese
		 * 重新发送指定消息。
		 *
		 * 异步方法。
		 *
		 * @param messageId 重发消息的 ID。
		 * @param handle    重发结果回调，详见 {@link CallBack}。
		 * @return			重发的消息对象。
		 *
		 *
		 * \~english
		 * Resends the message.
		 *
		 * This is an asynchronous method.
		 *
		 * @param message	The ID of the message to be resent.
		 * @param handle    The resending status callback. See {@link CallBack}.
		 * @return			The message that is resent.
		 */
		public abstract Message ResendMessage(string messageId, CallBack handle = null);

		/**
		 * \~chinese
		 * 查询包含特定关键字的本地消息。
		 *
		 * @param keywords   查找关键字，字符串类型。
		 * @param timestamp  查询的起始消息 Unix 时间戳，单位为毫秒。该参数设置后，SDK 从指定时间戳的消息开始，按消息搜索方向获取。
	 *                       如果该参数设置为负数，SDK 从当前时间开始搜索。
		 * @param maxCount   每次获取的最大消息数。取值范围为 [1,400]。
		 * @param from       消息来源，一般指会话 ID。
		 * @param direction	 消息查询方向，详见 {@link MessageSearchDirection}。
		 * 
		 * @return           消息列表。
		 *
		 * \~english
		 * Retrieves messages with keywords in the local database.
		 *
		 * @param keywords   The keyword for query. The data format is String.
		 * @param timestamp  The starting Unix timestamp in the message for query. The unit is millisecond. After this parameter is set, the SDK retrieves messages, starting from the specified one, according to the message search direction.
	 *                       If you set this parameter as a negative value, the SDK retrieves messages, starting from the current time, in the descending order of the timestamp included in them.
		 * @param maxCount   The maximum number of messages to retrieve each time. The value range is [1,400].
		 * @param from       The message source, which is usually a conversation ID.
		 * @param direction	 The message search direction. See {@link MessageSearchDirection}.
		 *
		 * @return           The list of messages.
		 */
		public abstract List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP);

		/**
		 * \~chinese
		 * 发送会话的已读回执。
		 * 
		 * 该方法通知服务器将此会话未读数设置为 `0`，消息发送方（包含多端多设备）将会收到 {@link IChatManagerDelegate#OnConversationRead(string from, string to)} 回调。
		 * 
		 * @param conversationId	会话 ID。
		 * @param handle			发送回执的结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends the conversation read receipt to the server.
		 * After this method is called, the sever will set the message status from unread to read. 
		 * The SDK triggers the {@link IChatManagerDelegate#OnConversationRead(string from, string to)} callback on the message sender's client, notifying that the messages are read. This also applies to multi-device scenarios.
		 *
		 * @param conversationId	The conversation ID.
		 * @param handle			The result callback. See {@link CallBack}.
		 */
		public abstract void SendConversationReadAck(string conversationId, CallBack handle = null);

		/**
		 * \~chinese
		 * 发送消息。
		 * 
		 * 异步方法。
		 * 
		 * 对于语音、图片等带有附件的消息，SDK 在默认情况下会自动上传附件。请参见 {@link Options#ServerTransfer}。
		 * 
		 * @param message   要发送的消息对象，必填。
		 * @param handle	发送结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends a message。
		 * 
		 * This is an asynchronous method.
		 *  
		 * For attachment messages such as voice or image, the SDK will automatically upload the attachment by default. See {@link Options#ServerTransfer}.
		 * 
		 * 
		 * @param msg		 The message object to be sent. Ensure that you set this parameter. 
		 * @param handle	 The result callback. See {@link CallBack}.
		 */
		public abstract void SendMessage(ref Message message, CallBack handle = null);

		/**
		 * \~chinese
		 * 发送消息已读回执。
         * 
		 ***Note** 
		 * 
		 * 该方法仅适用于单聊会话，仅在 {@link Options#RequireAck(boolean)} 为 `true` 时生效。
		 * 
		 * 该方法会通知服务器将此消息置为已读，消息发送方（包含多端多设备）将会收到 {@link IChatManagerDelegate#OnMessagesRead(List<Message>)} 回调。
		 * 
		 * @param messageId		消息 ID。
		 * @param handle		发送回执的结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends the read receipt of a message to the server.
		 * 
		 * *Note**
         * 
		 * - This method only takes effect if you set {@link Options#RequireAck(boolean)} as `true`.
		 * 
		 * - After this method is called, the sever will set the message status from unread to read. 
		 *
		 * - The SDK triggers the {@link IChatManagerDelegate#OnMessagesRead(List<Message>)} callback on the message sender's client, notifying that the messages are read. This also applies to multi-device scenarios.
		 *
		 * @param messageId		The message ID.
		 * @param handle		The result callback. See {@link CallBack}.
		 */
		public abstract void SendMessageReadAck(string messageId, CallBack handle = null);


       /**
        * \~chinese
        * 发送群消息已读回执。
        *
		* *Note**
        *
        * - 该方法仅在 {@link Message#IsNeedGroupAck(boolean)} 设置为 `true` 时有效；
        *
        * - 发送单聊消息已读回执，详见 {@link #SendMessageReadAck(String)}；
		* 
        * - 会话已读回执，详见 {@link #SendConversationReadAck(String)}。
        *
        * @param messageId     消息 ID。
        * @param ackContent    已读回执信息。消息的接收方自定义已读回执信息，消息的发送方收到该已读回执后，解析出自定义的字符串，自行处理。
        * @param handle		   发送回执的结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Sends the read receipt for a group message to the server.
		*
		* **Note**
		* 
        * - This method takes effect only if you set {@link Message#IsNeedGroupAck(boolean)} to `true`.
        *
        * - To send the read receipt for a one-to-one message, call {@link #SendMessageReadAck(String)}.
        * 
        * - To send the conversation receipt, call {@link #SendConversationReadAck(String)}.
        *
        * @param messageId     The message ID.
        * @param ackContent    The read receipt information, which is a custom keyword that specifies a custom action or command.
        * @param handle		   The result callback. See {@link CallBack}.
	 	*/
		public abstract void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null);

		/**
		 * \~chinese
		 * 更新本地消息。
		 * 
		 * 该方法调用后，本地内存和数据库中的消息均更新。
		 *
		 * @param message	要更新的消息对象。
		 * @return			本地消息是否成功更新。
		 *                  - `true`：是；
		 *                  - `false`：否。
		 *
		 * \~english
		 * Updates the local message.
		 * 
		 * After this method is called, messages in both the local memory and local database will be updated.
		 *
		 * @param message	The message object to update.
		 * @return			Whether the local message is successfully updated.	 
		 *                  - `true`: Yes. 
		 *                  - `false`: No.
		 */
		public abstract bool UpdateMessage(Message message);

		/**
		 * \~chinese
		 * 将指定 Unix 时间戳之前收发的消息从本地内存和数据库中移除。
		 *
		 * @param timeStamp	移除的 Unix 时间戳，单位为毫秒。
		 * @param handle	移除结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes messages that are sent and received before the Unix timestamp from the local memory and database.
		 *
		 * @param timeStamp	The starting Unix timestamp for removal. The unit is millisecond.
		 * @param handle	The removal result callback. See {@link CallBack}.
		 */

		public abstract void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack handle = null);

		/**
		 * \~chinese
		 * 删除服务端的指定会话及其历史消息。
		 * 
		 * 异步方法。
		 *
		 * @param conversationId 			会话 ID。
		 * @param conversationType          会话类型，详见 {@link ConversationType}。
		 * @param isDeleteServerMessages	是否删除会话时同时删除相应的历史消息。
		 *                                  - `true`：是。
		 *                                  - `false`：否。
		 * @param handle					会话删除成功与否的回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Deletes the specified conversation and its historical messages from the server.
		 * 
		 * This is an asynchronous method.
		 *
		 * @param conversationId 			The conversation ID.
		 * @param conversationType          The conversation type. See {@link ConversationType}.
		 * @param isDeleteServerMessages 	Whether to delete the historical messages with the conversation.
		 *                                  - `true`: Yes.
		 *                                  - `false`: No.
		 * @param handle					The callback for whether the conversation is deleted. See {@link CallBack}.
		 */
		public abstract void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack handle = null);


        /**
         * 获取翻译服务支持的语言。
         * @param callBack 完成的回调，详见 {@link #ValueCallBack()}。
         *
         * \~english
         * Gets all languages supported by the translation service.
         * @param callBack The result callback. See {@link #ValueCallBack()}.
         */
        public abstract void FetchSupportLanguages(ValueCallBack<List<SupportLanguage>> handle = null);

        /**
         * \~chinese
         * 翻译消息。

         * @param message 要翻译的消息对象。
         * @param languages 目标语言代码列表。
         * @param ValuecallBack 完成的回调，详见 {@link #ValueCallBack()}。
         *
         * \~english
         * Translates a message.
		 * 
         * @param message The message object.
         * @param languages  The list of target language codes.
         * @param ValuecallBack The result callback. See {@link #ValueCallBack()}.
         */
        public abstract void TranslateMessage(Message message, List<string> targetLanguages, ValueCallBack<Message> handle = null);

        /**
         * \~chinese
         * 从服务器分页获取群组消息已读回执详情。
         *
         * 发送群组消息回执，详见 {@link #SendReadAckForGroupMessage}。
         *
         * 异步方法。
         *
         * @param messageId		消息 ID。
		 * @param groupId		群组 ID。
         * @param pageSize		每页期望返回的群消息已读数。取值范围为 [1,50]。
         * @param startAckId    查询起始的已读回执 ID。该参数设置后，SDK 从指定的已读回执 ID 开始，按服务器接收已读回执的时间的逆序获取。
	     *                      若该参数为空，SDK 从最新的已读回执开始按服务器接收回执时间的逆序获取。
         * @param callBack      结果回调，成功执行 {@link ValueCallBack#onSuccess(Object)}，失败执行 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Uses the pagination to get read receipts for a group message from the server.
         *
         * To send a read receipt for a group message, you can call {@link #SendReadAckForGroupMessage}.
         *
         * This is an asynchronous method.
         *
         * @param msgId			The message ID.
		 * @param groupId		The group ID。
         * @param pageSize		The number of read receipts for the group message that you expect to get on each page. The value range is [1,50].
         * @param startAckId    The starting read receipt ID for query. After this parameter is set, the SDK retrieves read receipts, from the specified one, in the reverse chronological order of when the server receives them.
	     *                      If this parameter is set as `null` or an empty string, the SDK retrieves read receipts, from the latest one, in the reverse chronological order of when the server receives them.
         * @param callBack		The result callback:
		 *                      - If the call succeeds, the SDK will execute the method {@link ValueCallBack#onSuccess(Object)};
         *                      - If the call fails, the SDK will execute the method {@link ValueCallBack#onError(int, String)}.
         */
        public abstract void FetchGroupReadAcks(string messageId, string groupId, int pageSize = 20, string startAckId = null, ValueCallBack<CursorResult<GroupReadAck>> handle = null);

        /**
         * \~chinese
         * 举报违规消息。
         *
         * 同步方法，会阻塞当前线程。
         *
         * @param messageId		要举报的消息 ID。
         * @param tag			举报类型(例如：涉黄、涉恐)。
         * @param reason		举报原因。
         * @param callBack 		完成的回调，详见 {@link #CallBack()}。
         *
         * \~english
         * Reports a violation message.
         *
         * @param messageId		The ID of the message to report.
         * @param tag			The report type (For example: involving pornography and terrorism).
         * @param reason		The report reason.
         *
         * @param callBack      The result callback. See {@link #CallBack()}.
         */
        public abstract void ReportMessage(string messageId, string tag, string reason, CallBack handle = null);

        /**
         * \~chinese
         * 添加 Reaction。
         *
         * 异步方法。
         *
         * @param messageId 消息 ID。
         * @param reaction  消息 Reaction。
         * @param callback  处理结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Adds a Reaction.
         *
         * This is an asynchronous method.
         *
         * @param messageId The message ID.
         * @param reaction  The Reaction content.
         * @param callback  The result callback which contains the error information if the method fails.
         */
        public abstract void AddReaction(string messageId, string reaction, CallBack handle = null);

        /**
         * \~chinese
         * 删除 Reaction。
         *
         * 异步方法。
         *
         * @param messageId 消息 ID。
         * @param reaction  消息 Reaction。
         * @param callback  处理结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Deletes a Reaction.
         *
         * This is an asynchronous method.
         *
         * @param messageId The message ID.
         * @param reaction  The Reaction content.
         * @param callback  The result callback which contains the error information if the method fails.
         */

        public abstract void RemoveReaction(string messageId, string reaction, CallBack handle = null);

		 /**
         * \~chinese
         * 获取 Reaction 列表。
         *
         * 异步方法。
         *
         * @param messageIdList 消息 ID。
         * @param chatType      会话类型，仅支持单聊（ {@link ConversationType.Chat} ）和群聊（{@link ConversationType.Group}）。
         * @param groupId       群组 ID，该参数只在群聊生效。
         * @param callback      处理结果回调，包含消息 ID 对应的 Reaction 列表（EMMessageReaction 的用户列表为概要数据，只包含前三个用户信息）。
         *
         * \~english
         * Gets the list of Reactions.
         *
         * This is an asynchronous method.
         *
         * @param messageIdList  The message ID.
         * @param chatType       The chat type. Only one-to-one chat ({@link ConversationType.Chat} and group chat ({@link ConversationType.Group}) are allowed.
         * @param groupId        The group ID, which is invalid only when the chat type is group chat.
         * @param callback       The result callback, which contains the Reaction list under the specified message ID（The user list of EMMessageReaction is the summary data, which only contains the information of the first three users）.
         */

        public abstract void GetReactionList(List<string> messageIdList, MessageType chatType, string groupId, ValueCallBack<Dictionary<string, List<MessageReaction>>> handle = null);

         /**
         * \~chinese
         * 获取 Reaction 详细信息。
         *
         * 异步方法。
         *
         * @param messageId   消息 ID。
         * @param reaction    消息 Reaction。
         * @param cursor      查询 cursor。
         * @param pageSize    每页获取的 Reaction 条数。
         * @param callback    处理结果回调，包含 cursor 和 MessageReaction 列表（仅使用该列表第一个数据即可）。
         *
         * \~english
         * Gets the Reaction details.
         *
         * This is an asynchronous method.
         *
         * @param messageId    The message ID.
         * @param reaction     The Reaction content.
         * @param cursor       The query cursor.
         * @param pageSize     The number of Reactions you expect to get on each page.
         * @param callback     The result callback, which contains the reaction list obtained from the server and the cursor for the next query. Returns null if all the data is fetched.
         */

        public abstract void GetReactionDetail(string messageId, string reaction, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<MessageReaction>> handle = null);

		/**
		 * \~chinese
		 * 注册聊天管理器的监听器。
		 * 
		 * @param chatManagerDelegate 	要注册的聊天管理器的监听器，继承自 {@link IChatManagerDelegate}。
		 *
		 * \~english
		 * Adds a chat manager listener.
		 *
		 * @param chatManagerDelegate 	The chat manager listener to add. It is inherited from {@link IChatManagerDelegate}.
		 * 
		 */
		public void AddChatManagerDelegate(IChatManagerDelegate chatManagerDelegate)
        {
            if (!CallbackManager.Instance().chatManagerListener.delegater.Contains(chatManagerDelegate))
            {
                CallbackManager.Instance().chatManagerListener.delegater.Add(chatManagerDelegate);
            }
        }

		/**
		 * 移除聊天管理器的监听器。
		 *
		 * @param chatManagerDelegate 	要移除的聊天管理器的监听器，继承自 {@link IChatManagerDelegate}。
		 *
		 * \~english
		 * Removes a chat manager listener.
		 *
		 * @param chatManagerDelegate 	The chat manager listener to remove. It is inherited from {@link IChatManagerDelegate}.
		 * 
		 */
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
