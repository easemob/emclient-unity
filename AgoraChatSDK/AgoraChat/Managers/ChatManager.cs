using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    public class ChatManager : BaseManager
    {
        internal List<IChatManagerDelegate> delegater;

        Object msgMapLocker;
        Dictionary<string, Message> msgMap;

        internal ChatManager(NativeListener listener) : base(listener, SDKMethod.chatManager)
        {
            listener.ChatManagerEvent += NativeEventHandle;
            delegater = new List<IChatManagerDelegate>();
            msgMapLocker = new Object();
            msgMap = new Dictionary<string, Message>();
        }

        private void AddMsgMap(string cbid, Message msg)
        {
            lock (msgMapLocker)
            {
                msgMap.Add(cbid, msg);
            }
        }

        private void UpdatedMsg(string cbid, JSONNode jsonNode)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    var msg = msgMap[cbid];
                    if (jsonNode != null && jsonNode.IsObject)
                    {
                        msg.FromJsonObject(jsonNode.AsObject);
                    }
                }
            }
        }

        private void DeleteFromMsgMap(string cbid)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    msgMap.Remove(cbid);
                }
            }
        }



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
        * @param isThread       删除会话是否是子区会话。
        *                       - `true` 是子区会话；
        *                       - `false` 不是子区会话。
        *
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
        public bool DeleteConversation(string conversationId, bool deleteMessages = true)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("deleteMessages", deleteMessages);
            NativeCall(SDKMethod.deleteConversation, jo_param);
            return true;
        }

        /**
	     * \~chinese
	     * 下载消息的附件。
	     * 
	     * 若附件自动下载失败，也可以调用此方法下载。
	     *
	     * @param messageId	要下载附件的消息 ID。
	     * @param callback  下载结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Downloads the message attachment.
	     * 
	     * You can also call this method if the attachment fails to be downloaded automatically.
	     *
	     * @param messageId The ID of the message with the attachment to be downloaded.
	     * @param callback  The download status callback. See {@link CallBack}.
	     */
        public void DownloadAttachment(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            NativeCall(SDKMethod.downloadAttachment, jo_param, callback);
        }

        /**
	     * \~chinese
	     * 下载消息的缩略图。
		 * 
		 * 若消息缩略图自动下载失败，也可以调用该方法下载。
	     *
	     * @param messageId 要下载缩略图的消息 ID，一般图片消息和视频消息有缩略图。
	     * @param callback  下载结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Downloads the message thumbnail.
		 * 
	     * You can also call this method if the message thumbnail fails to be downloaded automatically.
		 * 
	     * @param messageId The ID of the message with the thumbnail to be downloaded.
	     * @param callback  The download status callback. See {@link CallBack}.
	     */
        public void DownloadThumbnail(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            NativeCall(SDKMethod.downloadThumbnail, jo_param, callback);
        }

        /**
	     * \~chinese
	     * 从服务器获取历史消息。
	     * 
	     * 分页获取。
	     *
	     * 异步方法。
	     *
	     * @param conversationId 		会话 ID。
	     * @param type 					会话类型，详见 {@link ConversationType}。	     
	     * @param startMessageId 		漫游消息的开始消息 ID。如果为空，SDK 按服务器接收消息时间的倒序获取。
	     * @param count 				每页期望返回的的消息条数。
	     * @param direction     		消息获取的方向。
	     * @param callback              结果回调，返回消息列表。
	     *
	     * \~english
	     * Gets historical messages of the conversation from the server.
	     * 
	     * Historical messages of a conversation can also be obtained with pagination.
	     *
	     * This is an asynchronous method.
	     *
	     * @param conversationId 		The conversation ID.
	     * @param type 					The conversation type. See {@link ConversationType}.
	     * @param startMessageId 		The starting message ID for the query. 
		 *                              If `null` is passed, the SDK gets messages in the reverse chronological order of when the server received the messages.
	     * @param count 				The number of messages that you expect to get on each page.
	     * @param direction     		The direction in which the message is fetched. MessageSearchDirection can be set with following:
         *                   				- `UP`: Gets messages before the timestamp of the specified message ID;
         *                  				- `DOWN`: Gets messages after the timestamp of the specified message ID.
	     * @param callback				The result callback. Returns the list of obtained messages. 
	     */
        public void FetchHistoryMessagesFromServer(string conversationId, ConversationType type = ConversationType.Chat, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<CursorResult<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", type.ToInt());
            jo_param.AddWithoutNull("startMsgId", startMessageId ?? "");
            jo_param.AddWithoutNull("direction", direction == MessageSearchDirection.UP ? 0 : 1);
            jo_param.AddWithoutNull("count", count);

            Process process = (_, jsonNode) =>
            {
                CursorResult<Message> cursor_msg = new CursorResult<Message>(_, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<Message>(jn);
                });

                cursor_msg.FromJsonObject(jsonNode.AsObject);
                return cursor_msg;

            };

            NativeCall<CursorResult<Message>>(SDKMethod.fetchHistoryMessages, jo_param, callback, process);
        }

        /**
	     * \~chinese
	     * 根据根据消息拉取参数配置类 `FetchServerMessagesOption` 从服务器分页获取历史消息。
	     *
         * 分页获取历史消息。
	     *
	     * 异步方法。
	     *
	     * @param conversationId 		会话 ID。
	     * @param type 					会话类型，详见 {@link ConversationType}。
	     * @param cursor                查询的起始游标位置。
	     * @param pageSize              每页期望获取的消息条数。取值范围为 [1,50]。
	     * @param option                查询历史消息的参数配置接口，详见 {@link FetchServerMessagesOption}。
	     * @param callback              结果回调，返回消息列表。
	     *
	     * \~english
	     * Gets historical messages of a conversation from the server according to the parameter configuration class for pulling historical messages `FetchServerMessagesOption`.
	     *
	     * Historical messages of a conversation can be obtained with pagination.
	     *
	     * This is an asynchronous method.
	     *
	     * @param conversationId 		The conversation ID.
	     * @param type 					The conversation type. See {@link ConversationType}.
	     * @param cursor                The cursor position from which to start querying data.
	     * @param pageSize              The number of messages that you expect to get on each page. The value range is [1,50].
	     * @param option                The parameter configuration class for pulling historical messages from the server. See {@link FetchServerMessagesOption}.
	     * @param callback				The result callback. The SDK returns the list of obtained messages.
	     */
        public void FetchHistoryMessagesFromServerBy(string conversationId, ConversationType type = ConversationType.Chat, string cursor = null, int pageSize = 10, FetchServerMessagesOption option = null, ValueCallBack<CursorResult<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", type.ToInt());
            jo_param.AddWithoutNull("cursor", cursor ?? "");
            jo_param.AddWithoutNull("pageSize", pageSize);
            if (null != option) jo_param.AddWithoutNull("options", option.ToJsonObject());

            Process process = (_, jsonNode) =>
            {
                CursorResult<Message> cursor_msg = new CursorResult<Message>(_, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<Message>(jn);
                });

                cursor_msg.FromJsonObject(jsonNode.AsObject);
                return cursor_msg;

            };

            NativeCall<CursorResult<Message>>(SDKMethod.fetchHistoryMessagesBy, jo_param, callback, process);
        }

        /**
	     * \~chinese
	     * 获取本地指定会话对象。
	     * 
	     * @param conversationId    会话 ID。
	     * @param type              会话类型，详见 {@link ConversationType}。
	     * @param createIfNeed      本地数据库中未找到相应会话时是否自动创建。
	     *                          - `true`：是；
	     *                          - `false`：否。
	     * @param isThread       获取会话是否是子区会话。
	     *                       - `true` 是子区会话；
	     *                       - `false` 不是子区会话。
	     * @return                  根据指定会话 ID 找到的会话对象。未找到会话会返回空值。
	     *
	     * \~english
	     * Gets the local conversation object.
	     * 
	     * The SDK wil return `null` if the conversation is not found.
	     *
	     * @param conversationId 	The conversation ID.
	     * @param type              The conversation type. See {@link ConversationType}.
	     * @param createIfNeed      Whether to automatically create a conversation if the conversation is not found. 
	     *                          - `true`: Yes.
	     *                          - `false`: No.
	     * @return 		            The conversation found. Returns `null` if the conversation is not found.
	     */
        public Conversation GetConversation(string conversationId, ConversationType type = ConversationType.Chat, bool createIfNeed = true)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", type.ToInt());
            jo_param.AddWithoutNull("createIfNeed", createIfNeed);
            jo_param.AddWithoutNull("isThread", false);

            JSONNode jn = NativeGet(SDKMethod.getConversation, jo_param).GetReturnJsonNode();

            if (null == jn) return null;
            return new Conversation(jn.AsObject);
        }

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
	     * The SDK wil return `null` if the conversation is not found.
	     *
	     * @param threadId 			The thread ID.
	     * 
	     * @return 		            The conversation found. Returns `null` if the conversation is not found.
	     */
        public Conversation GetThreadConversation(string threadId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", threadId);
            jo_param.AddWithoutNull("convType", ConversationType.Group.ToInt());
            jo_param.AddWithoutNull("createIfNeed", true);
            jo_param.AddWithoutNull("isThread", true);

            string json = NativeGet(SDKMethod.getThreadConversation, jo_param);

            if (null == json || json.Length == 0) return null;
            return new Conversation(json);
        }

        /**
	     * \~chinese
	     * 从服务器获取所有会话对象。
	     * 
	     * 未找到任何会话对象返回的列表为空。
	     * 
	     * @param callback    获取的会话列表，详见 {@link ValueCallBack}。
	     *
	     * \~english
	     * Gets all conversations from the server.
	     * 
	     * An empty list will be returned if no conversation is found.
	     *
	     * @param callback    The list of obtained conversations. See {@link ValueCallBack}.
	     */
        [Obsolete]
        public void GetConversationsFromServer(ValueCallBack<List<Conversation>> callback = null)
        {
            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Conversation>(jsonNode);
            };

            NativeCall<List<Conversation>>(SDKMethod.getConversationsFromServer, null, callback, process);
        }

        /**
	     * \~chinese
	     * 根据指定参数从服务器获取相关会话对象。
	     *
	     * @param pinOnly     是否只获取置顶会话：
         * - `true`：是。只获取置顶会话。SDK 按照会话置顶时间倒序返回。
         * - `false`：否。
	     * @param cursor      开始获取数据的游标位置。
	     * @param limit       每页返回的会话数。取值范围为 [1,50]。
	     * @param callback    获取的会话列表，详见 {@link ValueCallBack}。
	     *
	     * \~english
	     * Gets the conversations from the server.
	     *
	     * @param pingOnly    Whether to return pinned conversations only:
         * - `true`: Yes. The SDK only returns pinned conversations in the reverse chronological order of their pinning.
         * - `false`: No.
         *
	     * @param cursor      The position from which to start getting data.
	     * @param limit       The number of conversations that you expect to get on each page. The value range is [1,50].
	     * @param callback    The list of obtained conversations. See {@link ValueCallBack}.
	     */
        public void GetConversationsFromServerWithCursor(bool pinOnly, string cursor = "", int limit = 20, ValueCallBack<CursorResult<Conversation>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("pinOnly", pinOnly);
            jo_param.AddWithoutNull("cursor", cursor);
            jo_param.AddWithoutNull("limit", limit);

            Process process = (_, jsonNode) =>
            {
                CursorResult<Conversation> cursor_conversation = new CursorResult<Conversation>(_, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<Conversation>(jn);
                });

                cursor_conversation.FromJsonObject(jsonNode.AsObject);
                return cursor_conversation;
            };

            NativeCall<CursorResult<Conversation>>(SDKMethod.getConversationsFromServerWithCursor, jo_param, callback, process);
        }

        /**
         * \~chinese
         * 根据标记等参数从服务器获取相关会话对象。
         *
         * @param mark：      会话标记。
         * @param cursor      开始获取数据的游标位置。
         * @param limit       每页返回的会话数。取值范围为 [1,50]。
         * @param callback    获取的会话列表，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets the conversations from the server based on the conversation mark.
         * 
         * @param mark        The mark value used for searching.
         * @param cursor      The position from which to start getting data.
         * @param limit       The number of conversations that you expect to get on each page. The value range is [1,50].
         * @param callback    The list of obtained conversations. See {@link ValueCallBack}.
         */
        public void GetConversationsFromServerWithCursor(MarkType mark, string cursor = "", int limit = 20, ValueCallBack<CursorResult<Conversation>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("needMark", true);
            jo_param.AddWithoutNull("mark", (int)mark);
            jo_param.AddWithoutNull("cursor", cursor);
            jo_param.AddWithoutNull("limit", limit);

            Process process = (_, jsonNode) =>
            {
                CursorResult<Conversation> cursor_conversation = new CursorResult<Conversation>(_, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<Conversation>(jn);
                });

                cursor_conversation.FromJsonObject(jsonNode.AsObject);
                return cursor_conversation;
            };

            NativeCall<CursorResult<Conversation>>(SDKMethod.getConversationsFromServerWithCursorAndMark, jo_param, callback, process);
        }

        /**
	     * \~chinese
	     * 获取未读消息数。
		 * 
	     * @return		未读消息数。
	     *
	     *
	     * \~english
	     * Gets the unread message count.
	     *
	     * @return		The count of unread messages.
	     *
	     */
        public int GetUnreadMessageCount()
        {
            string json = NativeGet(SDKMethod.getUnreadMessageCount);

            if (null == json || json.Length == 0) return 0;

            JSONObject jo = JSON.Parse(json).AsObject;
            return int.Parse(jo["ret"].Value);
        }

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
        public void ImportMessages(List<Message> messages, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("list", JsonObject.JsonArrayFromList(messages));
            NativeCall(SDKMethod.importMessages, jo_param, callback);
        }

        /**
	     * \~chinese
	     * 将本地数据库中的所有会话加载到内存。
		 * 
	     * 一般情况下，该方法在成功登录后调用，以提升会话列表的加载速度。
	     * 
	     * @return            加载的会话列表。
	     *
	     * \~english
	     * Loads all conversations from the local database into the memory.
		 * 
	     * To accelerate the loading, call this method immediately after the user is logged in.
	     * 
	     * @return            The list of loaded conversations.
	     */
        public List<Conversation> LoadAllConversations()
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("isSort", true);

            JSONNode jn = NativeGet(SDKMethod.loadAllConversations, jo_param).GetReturnJsonNode();

            if (null == jn) return new List<Conversation>();

            return List.BaseModelListFromJsonArray<Conversation>(jn);
        }

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
        public Message LoadMessage(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.getMessage, jo_param).GetReturnJsonNode();

            if (null == jn) return null;
            return new Message(jn.AsObject);
        }

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
        public bool MarkAllConversationsAsRead()
        {
            string json = NativeGet(SDKMethod.markAllChatMsgAsRead);
            if (null == json || json.Length == 0) return false;

            JSONNode jn = JSON.Parse(json);
            return jn["ret"].AsBool;
        }

        /**
		 * \~chinese
		 * 撤回已发送的消息。
		 *
		 * 异步方法。
		 *
		 * @param messageId 要撤回消息的 ID。
		 * @param callback    撤回结果回调，详见 {@link CallBack}。
		 *
		 *
		 * \~english
		 * Recalls the message.
		 *
		 * This is an asynchronous method.
		 *
		 * @param message	The ID of the message to be recalled.
		 * @param callback    The recall status callback. See {@link CallBack}.
		 */
        public void RecallMessage(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);

            NativeCall(SDKMethod.recallMessage, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 重新发送指定消息。[作废]
		 *
		 * 异步方法。
		 *
		 * @param messageId 重发消息的 ID。
		 * @param callback  重发结果回调，详见 {@link CallBack}。
		 * @return			重发的消息对象。
		 *
		 *
		 * \~english
		 * Resends the message.[Deprecated]
		 *
		 * This is an asynchronous method.
		 *
		 * @param message	The ID of the message to be resent.
		 * @param callback  The resending status callback. See {@link CallBack}.
		 * @return			The message that is resent.
		 */
        [Obsolete("ResendMessage is deprecated", false)]
        public Message ResendMessage(string messageId, CallBack callback = null)
        {
            /*
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);

            JSONNode jn = NativeGet(SDKMethod.resendMessage, jo_param, callback).GetReturnJsonNode();

            if (null == jn) return null;

            return new Message(jn);
            */
            return null;
        }

        /**
		 * \~chinese
		 * 查询指定数量的本地消息。
		 * 
		 * **注意**
		 * 
		 * 若查询消息数量较大，需考虑内存消耗，每次最多可查询 200 条消息。
		 *
		 * @param keywords   查找关键字，字符串类型。
		 * @param timestamp  查询的起始时间戳，单位为毫秒。
		 * @param maxCount   查询的最大消息数。
		 * @param from       消息发送方的用户 ID。若不设置该参数，SDK 搜索消息时会忽略该参数。
		 * @param direction	 查询方向，详见 {@link MessageSearchDirection}。
		 * @param callback   成功返回合并消息中的消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Retrieves local messages of a certain quantity.
		 * 
		 * **Note**
		 * If you want to query a great number of messages, pay attention to the memory consumption. A maximum number of 200 messages can be retrieved each time.
		 *
		 * @param keywords   The keyword for query. The data format is String.
		 * @param timestamp  The starting Unix timestamp for query, which is in milliseconds. After this parameter is set, the SDK retrieves messages, starting from the specified one, according to the message search direction.
		 *                   If you set this parameter as a negative value, the SDK retrieves messages, starting from the current time, in the descending order of the the Unix timestamp included in them.
		 * @param maxCount   The maximum number of messages to retrieve.
		 * @param from       The user ID of the message sender. If you do not set this parameter, the SDK ignores this parameter when retrieving messages.
		 * @param direction	 The query direction. See {@link MessageSearchDirection}.
		 * @param callback   If success, a list of original messages included in the combined message are returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 */
        public void SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("keywords", keywords);
            jo_param.AddWithoutNull("from", from ?? "");
            jo_param.AddWithoutNull("count", maxCount);
            jo_param.AddWithoutNull("timestamp", timestamp.ToString());
            jo_param.AddWithoutNull("direction", direction.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.searchChatMsgFromDB, jo_param, callback, process);
        }

        /**
        * \~chinese
        * 基于消息范围查询指定数量的本地消息。
        *
        * **注意**
        *
        * 若查询消息数量较大，需考虑内存消耗，每次最多可查询 200 条消息。
        *
        * @param keywords   查找关键字，字符串类型。
        * @param timestamp  查询的起始时间戳，单位为毫秒。
        * @param maxCount   查询的最大消息数。
        * @param from       消息发送方的用户 ID。若不设置该参数，SDK 搜索消息时会忽略该参数。
        * @param direction	查询方向，详见 {@link MessageSearchDirection}。
        * @param scope	    查询范围，详见 {@link MessageSearchScope}。
        * @param callback   成功返回合并消息中的消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
        *
        * \~english
        * Queries local messages based on the message scope.
        *
        * **Note**
        * If you want to query a great number of messages, pay attention to the memory consumption. A maximum number of 200 messages can be retrieved each time.
        *
        * @param keywords   The keyword for query. The data format is String.
        * @param timestamp  The starting Unix timestamp for query, which is in milliseconds.
        * @param maxCount   The maximum number of messages to retrieve.
        * @param from       The user ID of the message sender. If you do not set this parameter, the SDK ignores this parameter when retrieving messages.
        * @param direction	The query direction. See {@link MessageSearchDirection}.
        * @param scope	    The query direction. See {@link MessageSearchScope}.
        * @param callback   If success, a list of original messages included in the combined message are returned; otherwise, an error is returned. See {@link ValueCallBack}.
        */
        public void SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP, MessageSearchScope scope = MessageSearchScope.CONTENT, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("keywords", keywords);
            jo_param.AddWithoutNull("from", from ?? "");
            jo_param.AddWithoutNull("count", maxCount);
            jo_param.AddWithoutNull("timestamp", timestamp.ToString());
            jo_param.AddWithoutNull("direction", direction.ToInt());
            jo_param.AddWithoutNull("scope", scope.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.searchChatMsgFromDBWithScope, jo_param, callback, process);
        }

        /**
		 * \~chinese
		 * 发送会话的已读回执。
		 * 
		 * 该方法通知服务器将此会话未读数设置为 `0`，消息发送方（包含多端多设备）将会收到 {@link IChatManagerDelegate#OnConversationRead(string from, string to)} 回调。
		 * 
		 * @param conversationId	会话 ID。
		 * @param callback			发送回执的结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends the conversation read receipt to the server.
         *
		 * After this method is called, the sever will set the message status from unread to read. 
         *
		 * The SDK triggers the {@link IChatManagerDelegate#OnConversationRead(string from, string to)} callback on the message sender's client, notifying that the messages are read. This also applies to multi-device scenarios.
		 *
		 * @param conversationId	The conversation ID.
		 * @param callback			The result callback. See {@link CallBack}.
		 */
        public void SendConversationReadAck(string conversationId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);

            NativeCall(SDKMethod.ackConversationRead, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 发送消息。
		 *
		 * 异步方法。
		 *
		 * 对于语音、图片等带有附件的消息，SDK 在默认情况下会自动上传附件。请参见 {@link Options#ServerTransfer}。
		 *
		 * @param message   要发送的消息对象，必填。
		 * @param callback	发送结果回调，详见 {@link CallBack}。
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
		 * @param callback	 The result callback. See {@link CallBack}.
		 */
        public void SendMessage(ref Message message, CallBack callback = null)
        {
            Process process = (_cbid, _json) =>
            {
                UpdatedMsg(_cbid, _json["ret"]);
                DeleteFromMsgMap(_cbid);
                return null;
            };

            callbackManager.AddCallbackAction(callback, process);

            AddMsgMap(callback.callbackId, message);

            JSONObject jo_param = message.ToJsonObject();

            JSONNode jn = CWrapperNative.NativeGet(managerName, SDKMethod.sendMessage, jo_param, callback?.callbackId ?? "").GetReturnJsonNode();

            if (null != jn)
            {
                UpdatedMsg(callback.callbackId, jn["ret"]);
            }
        }

        /**
		 * \~chinese
		 * 发送单聊消息已读回执。
		 * 
		 * 该方法会通知服务器将此消息置为已读，消息发送方（包含多端多设备）将会收到 {@link IChatManagerDelegate#OnMessagesRead(List<Message>)} 回调。
		 * 
		 * @param messageId		消息 ID。
		 * @param callback		发送回执的结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends the read receipt of a one-to-one message to the server.
		 * 
		 * After this method is called, the sever will set the message status from unread to read. 
		 *
		 * The SDK triggers the {@link IChatManagerDelegate#OnMessagesRead(List<Message>)} callback on the message sender's client, notifying that the messages are read. This also applies to multi-device scenarios.
		 *
		 * @param messageId		The message ID.
		 * @param callback		The result callback. See {@link CallBack}.
		 */
        public void SendMessageReadAck(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            NativeCall(SDKMethod.ackMessageRead, jo_param, callback);
        }

        /**
        * \~chinese
        * 发送群消息已读回执。
        *
        * 调用该方法的前提条件是设置了 {@link Options#RequireAck(boolean)} 和 {@link Message#IsNeedGroupAck(boolean)}。
        *
        * 发送单聊消息已读回执，详见 {@link #SendMessageReadAck(String)}。
        *
        * 会话已读回执，详见 {@link #SendConversationReadAck(String)}。
        *
        * @param messageId     消息 ID。
        * @param ackContent    回执信息。`ackContent` 属性是用户自己定义的关键字，接收后，解析出自定义的字符串，可以自行处理。
        * @param callback	   发送回执的结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Sends a read receipt for a group message to the server.
        *
        * You can only call the method after setting {@link Options#RequireAck(boolean)} and {@link Message#IsNeedGroupAck(boolean)}.
        *
        * To send the read recipient for a one-to-one chat message to the server, call {@link #SendMessageReadAck(String)}.
        *
        * To send the conversation read receipt to the server, call {@link #SendConversationReadAck(String)}.
        *
        * @param messageId     The message ID.
        * @param ackContent    The content of the read receipt. The content is a self-defined string that can be used for specifying custom action/command.
        * @param callback	   The result callback. See{@ link CallBack}.
    */
        public void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("content", ackContent);
            NativeCall(SDKMethod.ackGroupMessageRead, jo_param, callback);
        }

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
        public bool UpdateMessage(Message message)
        {
            JSONObject jo_param = message.ToJsonObject();

            string json = NativeGet(SDKMethod.updateChatMessage, jo_param);

            if (null == json || json.Length == 0) return false;

            JSONObject jsonObject = JSON.Parse(json).AsObject;
            return jsonObject["ret"].AsBool;
        }

        /**
         * \~chinese
         * 修改消息内容。
         *
         * 调用该方法修改消息内容后，本地和服务端的消息均会修改。
         *
         * 调用该方法只能修改单聊和群聊中的文本消息，不能修改聊天室消息。

         * @param messageId 要修改的消息的 ID。
         * @param body      内容修改后的消息体。
         * @param callback 完成的回调，详见 {@link #CallBack()}。
         *
         * \~english
         * Modifies a message.
         *
         * After this method is called to modify a message, both the local message and the message on the server are modified.
         *
         * This method can only modify a text message in one-to-one chats or group chats, but not in chat rooms.
         *
         * @param messageId The ID of the message to modify.
         * @param body      The modified message body.
         * @param callBack The result callback. See {@link #CallBack()}.
         */
        public void ModifyMessage(string messageId, MessageBody.TextBody body, ValueCallBack<Message> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("body", body.ToJsonObject());

            Process process = (_, jsonNode) =>
            {
                return ModelHelper.CreateWithJsonObject<Message>(jsonNode);
            };

            NativeCall<Message>(SDKMethod.modifyMessage, jo_param, callback, process);
        }

        /**
		 * \~chinese
		 * 将指定 Unix 时间戳之前收发的消息从本地内存和数据库中移除。
		 *
		 * @param timeStamp	移除的 Unix 时间戳，单位为毫秒。
		 * @param callback	移除结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes messages that are sent and received before the Unix timestamp from the local memory and database.
		 *
		 * @param timeStamp	The starting Unix timestamp for removal.
		 * @param callback	The removal result callback. See {@link CallBack}.
		 */

        public void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("timestamp", timeStamp.ToString());
            NativeCall(SDKMethod.deleteMessagesBeforeTimestamp, jo_param, callback);
        }

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
		 *                                  - `false` 是。
		 * @param callback					会话删除成功与否的回调，详见 {@link CallBack}。
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
		 * @param callback					Callback for whether the conversation is deleted. See {@link CallBack}.
		 */
        public void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("isDeleteServerMessages", isDeleteServerMessages);

            NativeCall(SDKMethod.deleteRemoteConversation, jo_param, callback);
        }

        /**
         * 获取翻译服务支持的语言。
         *
         * @param callBack 完成的回调，详见 {@link #ValueCallBack()}。
         *
         * \~english
         * Gets all languages supported by the translation service.
         *
         * @param callBack The result callback. See {@link #ValueCallBack()}.
         */
        public void FetchSupportLanguages(ValueCallBack<List<SupportLanguage>> callback = null)
        {

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<SupportLanguage>(jsonNode);
            };

            NativeCall<List<SupportLanguage>>(SDKMethod.fetchSupportedLanguages, null, callback, process);
        }

        /**
         * \~chinese
         * 翻译消息。
         * @param message 消息对象。
         * @param languages 要翻译的目标语言 code 列表。
         * @param callback 完成的回调，详见 {@link #CallBack()}。
         *
         * \~english
         * Translates a message.
         * @param message The message object.
         * @param languages The code list of the target languages.
         * @param callBack The result callback. See {@link #CallBack()}.
         */
        public void TranslateMessage(Message message, List<string> targetLanguages, ValueCallBack<Message> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("message", message.ToJsonObject());
            jo_param.AddWithoutNull("languages", JsonObject.JsonArrayFromStringList(targetLanguages));

            Process process = (_, jsonNode) =>
            {
                return ModelHelper.CreateWithJsonObject<Message>(jsonNode);
            };

            NativeCall<Message>(SDKMethod.translateMessage, jo_param, callback, process);
        }

        /**
         * \~chinese
         * 从服务器分页获取群组消息回执详情。
         *
         * 发送群组消息回执，详见 {@link #SendReadAckForGroupMessage}。
         *
         * 异步方法。
         *
         * @param messageId		消息 ID。
		 * @param groupId		群组 ID。
         * @param pageSize		每页获取群消息已读回执的条数。取值范围[1,50]。
         * @param startAckId    已读回执的 ID，如果为空，从最新的回执向前开始获取。
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
         *                      If you set this parameter as null, the SDK will retrieve from the latest read receipt.
         * @param callBack		The result callback. If the call succeeds, the SDK executes {@link ValueCallBack#onSuccess(Object)};
         *                      if the call fails, the SDK executes {@link ValueCallBack#onError(int, String)}.
         */
        public void FetchGroupReadAcks(string messageId, string groupId, int pageSize = 20, string startAckId = null, ValueCallBack<CursorResult<GroupReadAck>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("pageSize", pageSize);
            jo_param.AddWithoutNull("groupId", groupId);
            jo_param.AddWithoutNull("ackId", startAckId ?? "");


            Process process = (_, jsonNode) =>
            {
                CursorResult<GroupReadAck> cursor_msg = new CursorResult<GroupReadAck>(_, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<GroupReadAck>(jn);
                });

                cursor_msg.FromJsonObject(jsonNode.AsObject);
                return cursor_msg;
            };
            NativeCall<CursorResult<GroupReadAck>>(SDKMethod.asyncFetchGroupAcks, jo_param, callback, process);
        }

        /**
         * \~chinese
         * 举报违规消息。
         *
         * 同步方法，会阻塞当前线程。
         *
         * @param messageId		要举报的消息 ID。
         * @param tag			非法消息的标签。你需要填写自定义标签，例如`涉政`或`广告`。
         * @param reason		举报原因。你需要自行填写举报原因。
         * @param callBack 		完成的回调，详见 {@link #CallBack()}。
         *
         * \~english
         * Reports an inappropriate message.
         *
         * @param messageId		The ID of the message to report.
         * @param tag			The tag of the inappropriate message. You need to type a custom tag, like `porn` or `ad`.
         * @param reason		The reporting reason. You need to type a specific reason.
         *
         * @param callBack The result callback，see {@link #CallBack()}.
         */
        public void ReportMessage(string messageId, string tag, string reason, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("tag", tag);
            jo_param.AddWithoutNull("reason", reason);

            NativeCall(SDKMethod.reportMessage, jo_param, callback);
        }

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
         * @param reaction  The message Reaction.
         * @param callback  The result callback which contains the error information if the method fails.
         */
        public void AddReaction(string messageId, string reaction, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("reaction", reaction);

            NativeCall(SDKMethod.addReaction, jo_param, callback);
        }

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

        public void RemoveReaction(string messageId, string reaction, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("reaction", reaction);

            NativeCall(SDKMethod.removeReaction, jo_param, callback);
        }

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
        * @param groupId        The group ID, which is valid only when the chat type is group chat.
        * @param callback       The result callback, which contains the Reaction list under the specified message ID（The user list of EMMessageReaction is the summary data, which only contains the information of the first three users）.
        */
        public void GetReactionList(List<string> messageIdList, MessageType chatType, string groupId, ValueCallBack<Dictionary<string, List<MessageReaction>>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgIds", JsonObject.JsonArrayFromStringList(messageIdList));
            jo_param.AddWithoutNull("groupId", groupId);
            //TODO: need to check
            jo_param.AddWithoutNull("type", chatType == MessageType.Group ? "groupchat" : "chat");
            Process process = (_, jsonNode) =>
            {
                return Dictionary.ListBaseModelDictionaryFromJsonObject<MessageReaction>(jsonNode);
            };

            NativeCall<Dictionary<string, List<MessageReaction>>>(SDKMethod.fetchReactionList, jo_param, callback, process);
        }

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
        public void GetReactionDetail(string messageId, string reaction, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<MessageReaction>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("reaction", reaction);
            jo_param.AddWithoutNull("cursor", cursor ?? "");
            jo_param.AddWithoutNull("pageSize", pageSize);


            Process process = (_, jsonNode) =>
            {
                CursorResult<MessageReaction> cursor_msg = new CursorResult<MessageReaction>(_, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<MessageReaction>(jn);
                });

                cursor_msg.FromJsonObject(jsonNode.AsObject);
                return cursor_msg;
            };

            NativeCall<CursorResult<MessageReaction>>(SDKMethod.fetchReactionDetail, jo_param, callback, process);
        }

        /**
         * \~chinese
         * 从服务器获取指定数目的会话对象。
         *
         * 未找到任何会话对象返回的列表为空。
         *
         * @param pageNum     当前页码。
         * @param pageSize    每页期望返回的会话数。
         * @param callback    获取的会话列表，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets the conversations from the server.
         *
         * An empty list will be returned if no conversation is found.
         *
         * @param pageNum     The current page number.
         * @param pageSize    The number of conversations to get on each page.
         * @param callback    The list of obtained conversations. See {@link ValueCallBack}.
         */
        public void GetConversationsFromServerWithPage(int pageNum, int pageSize, ValueCallBack<List<Conversation>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("pageNum", pageNum);
            jo_param.AddWithoutNull("pageSize", pageSize);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Conversation>(jsonNode);
            };

            NativeCall<List<Conversation>>(SDKMethod.getConversationsFromServerWithPage, jo_param, callback, process);
        }

        /**
         * \~chinese
         * 从会话中删除消息（包括本地存储和服务器存储）。
         *
         * 异步方法。
         *
         * @param conversationId    会话 ID。
         * @param conversationType  会话类型，详见 {@link ConversationType}。
         * @param messageIdList     要移除的消息的 ID 列表。
         * @param callback          处理结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Removes messages in a conversation (from both local storage and the server).
         *
         * This is an asynchronous method.
         *
         * @param conversationId     The conversation ID.
         * @param conversationType   The conversation type. See {@link ConversationType}.
         * @param messageIdList      The list of IDs of messages to be removed.
         * @param callback           Callback for the operation. See {@link CallBack}.
         */

        public void RemoveMessagesFromServer(string conversationId, ConversationType conversationType, List<string> messageIdList, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msgIds", JsonObject.JsonArrayFromStringList(messageIdList));

            NativeCall(SDKMethod.removeMessagesFromServerWithMsgIds, jo_param, callback);
        }

        /**
         * \~chinese
         * 从会话中删除消息（包括本地存储和服务器存储）。
         *
         * 异步方法。
         *
         * @param conversationId    会话 ID。
         * @param conversationType  会话类型，详见 {@link ConversationType}。
         * @param timeStamp	        指定的时间戳, 单位为毫秒。该时间戳之前的消息会被删除。
         * @param callback          处理结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Removes messages in a conversation (from both local storage and the server).
         *
         * This is an asynchronous method.
         *
         * @param conversationId     The conversation ID.
         * @param conversationType   The conversation type. See {@link ConversationType}.
         * @param timeStamp          The specified Unix timestamp in milliseconds. Messages with a timestamp before the specified one will be removed from the conversation.
         * @param callback           Callback for the operation. See {@link CallBack}.
         */

        public void RemoveMessagesFromServer(string conversationId, ConversationType conversationType, long timeStamp, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("timestamp", timeStamp.ToString());

            NativeCall(SDKMethod.removeMessagesFromServerWithTs, jo_param, callback);
        }

        /**
         * \~chinese
         * 设置会话是否置顶。
         *
         * 异步方法。
         *
         * @param conversationId    会话 ID。
         * @param isPinned          是否将会话设置为置顶。
         * @param callback          处理结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Sets whether to pin a conversation.
         *
         * This is an asynchronous method.
         *
         * @param conversationId     The conversation ID.
         * @param isPinned           Whether to pin the conversation:
         *                           - `true`: Yes.
         *                           - `false`: No.
         * @param callback           Callback for the operation. See {@link CallBack}.
         */

        public void PinConversation(string conversationId, bool isPinned, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("isPinned", isPinned);

            NativeCall(SDKMethod.pinConversation, jo_param, callback);
        }

        /**
         * \~chinese
         * 获取并解析合并消息。
         *
         * @param msg               需要获取和解析的合并消息。
         * @param callback          成功返回合并消息中的消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets and parses the combined message.
         *
         * @param msg               The combined message to get and parse.
         * @param callback          If success, a list of original messages included in the combined message are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void FetchCombineMessageDetail(Message msg, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msg", msg.ToJsonObject());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.downloadCombineMessages, jo_param, callback, process);
        }

        /**
        * \~chinese
        * 标记会话或移除会话标记。
        *
        * 异步方法。
        * 
        * 调用该方法会同时为本地和服务器端的会话添加标记。
        *
        * @param conversationIds   会话 ID 列表。
        * @param isMarked          添加或者移除标记：
        *                          - `true`：添加；
        *                          - `false`：移除。
        * @param mark              添加或移除的会话标记。
        * @param callback          处理结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Marks or unmarks conversations.
        *
        * This is an asynchronous method.
        *
        * This method marks conversations both locally and on the server.
        *
        * @param conversationIds    The list of conversation IDs.
        * @param isMarked           Whether to add or remove the mark for the conversations.
        *                           - `true`: add. 
        *                           - `false`: remove.
        * @param mark               The conversation mark to add or remove.
        * @param callback           Callback for the operation. See {@link CallBack}.
        */
        public void MarkConversations(List<string> conversationIds, bool isMarked, MarkType mark, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convIds", JsonObject.JsonArrayFromStringList(conversationIds));
            jo_param.AddWithoutNull("isMarked", isMarked);
            jo_param.AddWithoutNull("mark", (int)mark);

            NativeCall(SDKMethod.markConversations, jo_param, callback);
        }

        /**
        * \~chinese
        * 清空所有会话及其消息。
        *
        * 异步方法。
        *
        * @param clearServerData   是否删除服务端所有会话及其消息： 
        *                       - `true`：是。服务端的所有会话及其消息会被清除，当前用户无法再从服务端拉取消息和会话，其他用户不受影响。
        *                       - （默认）`false`：否。只清除本地所有会话及其消息，服务端的会话及其消息仍保留。
        * @param callback          处理结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Clears all conversations and all messages in them.
        *
        * This is an asynchronous method.
        *
        * @param clearServerData   Whether to clear all conversations and all messages in them on the server. 
        *   - `true`：Yes. All conversations and all messages in them will be cleared on the server side. 
            The current user cannot retrieve messages and conversations from the server, while this has no impact on other users.
        *  - (Default) `false`：No. All local conversations and all messages in them will be cleared, while those on the server remain.
        * @param callback           Callback for the operation. See {@link CallBack}.
        */
        public void DeleteAllMessagesAndConversations(bool clearServerData, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("clearServerData", clearServerData);

            NativeCall(SDKMethod.deleteAllMessagesAndConversations, jo_param, callback);
        }

        /**
        * \~chinese
        * 消息置顶或取消置顶。
        *
        * 仅支持群组消息。
        *
        * 异步方法。
        *
        * @param messageId         置顶或取消置顶的消息 ID。
        * @param isPinned          是否置顶消息：
        * - `true`: 置顶；
        * - `false`：取消置顶。
        * @param callback          处理结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Pins or unpins a message.
        *
        * This method is used only for group messages.
        *
        * This is an asynchronous method.
        *
        * @param messageId          The message ID to be pinned or unpinned.
        * @param isPinned           Whether to pin the message:
        *   - `true`: pin.
        *   - `false`: unpin.
        * @param callback           Callback for the operation. See {@link CallBack}.
        */
        public void PinMessage(string messageId, bool isPinned, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            jo_param.AddWithoutNull("isPinned", isPinned);

            NativeCall(SDKMethod.pinMessage, jo_param, callback);
        }

        /**
         * \~chinese
         * 从服务端获取指定会话的置顶消息列表。
         *
         * @param conversationId    会话 ID。
         * @param callback          成功返回合并消息中的消息列表，失败返回错误原因，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets the list of pinned messages in the conversation from the server.
         *
         * @param msg               The conversation ID.
         * @param callback          If success, the list of pined messages in the conversation are returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void GetPinnedMessagesFromServer(string conversationId, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.getPinnedMessagesFromServer, jo_param, callback, process);
        }

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
            if (!delegater.Contains(chatManagerDelegate))
            {
                delegater.Add(chatManagerDelegate);
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
            if (delegater.Contains(chatManagerDelegate))
            {
                delegater.Remove(chatManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            delegater.Clear();
        }

        internal void NativeEventHandle(string method, JSONNode jsonNode)
        {
            if (delegater.Count == 0) return;

            switch (method)
            {
                case SDKMethod.onMessagesReceived:
                    {
                        List<Message> list = List.BaseModelListFromJsonArray<Message>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.OnMessagesReceived(list);
                        }
                    }
                    break;
                case SDKMethod.onCmdMessagesReceived:
                    {
                        List<Message> list = List.BaseModelListFromJsonArray<Message>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.OnCmdMessagesReceived(list);
                        }
                    }
                    break;
                case SDKMethod.onMessagesRead:
                    {
                        List<Message> list = List.BaseModelListFromJsonArray<Message>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.OnMessagesRead(list);
                        }
                    }
                    break;
                case SDKMethod.onMessagesDelivered:
                    {
                        List<Message> list = List.BaseModelListFromJsonArray<Message>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.OnMessagesDelivered(list);
                        }
                    }
                    break;
                case SDKMethod.onMessagesRecalled:
                    {
                        List<Message> list = List.BaseModelListFromJsonArray<Message>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.OnMessagesRecalled(list);
                        }
                    }
                    break;
                case SDKMethod.onReadAckForGroupMessageUpdated:
                    {
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            it.OnReadAckForGroupMessageUpdated();
                        }
                    }
                    break;
                case SDKMethod.onGroupMessageRead:
                    {
                        List<GroupReadAck> list = List.BaseModelListFromJsonArray<GroupReadAck>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.OnGroupMessageRead(list);
                        }
                    }
                    break;
                case SDKMethod.onConversationsUpdate:
                    {
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            it.OnConversationsUpdate();
                        }
                    }
                    break;
                case SDKMethod.onConversationRead:
                    {
                        string from = jsonNode["from"];
                        string to = jsonNode["to"];
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            it.OnConversationRead(from, to);
                        }
                    }
                    break;
                case SDKMethod.onMessageReactionDidChange:
                    {
                        List<MessageReactionChange> list = List.BaseModelListFromJsonArray<MessageReactionChange>(jsonNode);
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            if (list.Count > 0) it.MessageReactionDidChange(list);
                        }
                    }
                    break;
                case SDKMethod.onMessageContentChanged:
                    {
                        Message msg = new Message(jsonNode["msg"].AsObject);
                        string operatorId = jsonNode["operatorId"];
                        long operationTime = (long)jsonNode["operationTime"].AsDouble;

                        foreach (IChatManagerDelegate it in delegater)
                        {
                            it.OnMessageContentChanged(msg, operatorId, operationTime);
                        }
                    }
                    break;
                case SDKMethod.onMessagePinChanged:
                    {
                        string messageId = jsonNode["msgId"];
                        string conversationId = jsonNode["convId"];
                        bool isPinned = jsonNode["isPinned"].AsBool;
                        string operatorId = jsonNode["operatorId"];
                        long operationTime = (long)jsonNode["ts"].AsDouble;

                        foreach (IChatManagerDelegate it in delegater)
                        {
                            it.OnMessagePinChanged(messageId, conversationId, isPinned, operatorId, operationTime);
                        }
                    }
                    break;
                case SDKMethod.onMessageIdChanged:
                    {
                        /*
                        string conversationId = jsonNode["convId"];
                        string oldMsgId = jsonNode["oldMsgId"];
                        string newMsgId = jsonNode["newMsgId"];
                        foreach (IChatManagerDelegate it in delegater)
                        {
                            it.onMessageIdChanged(conversationId, oldMsgId, newMsgId);
                        }
                        */
                    }
                    break;
            }
        }
    }
}