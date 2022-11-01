using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    public class ChatManager
    {
        internal CallbackManager callbackManager;
        internal List<IChatManagerDelegate> delegater;

        internal string NAME_CHATMANAGER = "ChatManager";

        Object msgMapLocker;
        Dictionary<string, Message> msgMap;

        internal ChatManager(NativeListener listener)
        {
            callbackManager = listener.callbackManager;
            listener.chatManagerEvent += NativeEventHandle;

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

        private void UpdatedMsg(string cbid, string json)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    var msg = msgMap[cbid];
                    if (json.Length > 0)
                    {
                        JSONNode jn = JSON.Parse(json);
                        if (null != jn && jn.IsObject)
                        {
                            msg.FromJsonObject(jn.AsObject);
                        }
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
            jo_param.Add("convId", conversationId);
            jo_param.Add("deleteMessages", deleteMessages);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "deleteConversation", jo_param, "");
            return true; //TODO: no any result from removeConversation...
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
            jo_param.Add("msgId", messageId);
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "downloadAttachment", jo_param, (null != callback) ? callback.callbackId : "");
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
            jo_param.Add("msgId", messageId);
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "downloadThumbnail", jo_param, (null != callback) ? callback.callbackId : "");
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
         *                   				- `UP`: fetch messages before the timestamp of the specified message ID;
         *                  				- `DOWN`: fetch messages after the timestamp of the specified message ID.
	     * @param callback				The result callback. Returns the list of obtained messages.
	     */
        public void FetchHistoryMessagesFromServer(string conversationId, ConversationType type = ConversationType.Chat, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<CursorResult<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", Conversation.ConversationTypeToInt(type));
            jo_param.Add("startMsgId", startMessageId ?? "");
            jo_param.Add("direction", direction == MessageSearchDirection.UP ? 0 : 1);
            jo_param.Add("count", count);

            Process process = (_cbid, _json) =>
            {
                CursorResult<Message>.ItemCallback parse = (JSONObject jo) =>
                {
                    return new Message(jo.Value);
                };

                CursorResult<Message> cursor_result = new CursorResult<Message>(_json, parse);
                return cursor_result;
            };

            callbackManager.AddCallbackAction<CursorResult<Message>>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "fetchHistoryMessages", jo_param, (null != callback) ? callback.callbackId : "");
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
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", Conversation.ConversationTypeToInt(type));
            jo_param.Add("createIfNeed", createIfNeed);
            jo_param.Add("isThread", false);

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "getConversation", jo_param, "");

            if (null == json || json.Length == 0) return null;
            return new Conversation(json);
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
	     * @param threadId 			The threadId ID.
	     * 
	     * @return 		            The conversation found. Returns `null` if the conversation is not found.
	     */
        public Conversation GetThreadConversation(string threadId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", threadId);
            jo_param.Add("convType", Conversation.ConversationTypeToInt(ConversationType.Group));
            jo_param.Add("createIfNeed", true);
            jo_param.Add("isThread", true);

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "getConversation", jo_param, "");

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
	     * Gets the all conversations from the server.
	     * 
	     * An empty list will be returned if no conversation is found.
	     *
	     * @param callback    The list of obtained coversations. See {@link ValueCallBack}.
	     */
        public void GetConversationsFromServer(ValueCallBack<List<Conversation>> callback = null)
        {
            Process process = (_cbid, _json) =>
            {
                List<Conversation> list = Conversation.ListFromJson(_json);
                return list;
            };

            callbackManager.AddCallbackAction<List<Conversation>>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "getConversationsFromServer", null, (null != callback) ? callback.callbackId : "");
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
	     * @return		The count of unread messages.
	     *
	     */
        public int GetUnreadMessageCount()
        {
            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "getUnreadMessageCount", null, null);

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
        public bool ImportMessages(List<Message> messages)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("list", Message.ListToJsonObject(messages));

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "importMessages", jo_param, null);
            if (null == json || json.Length == 0) return false;

            JSONNode jn = JSON.Parse(json);
            return jn["ret"].AsBool;
        }

        /**
	     * \~chinese
	     * 将本地数据库中的所有会话加载到内存。
		 * 
	     * 一般情况下，该方法在成功登录后调用，以提升会话列表的加载速度。
	     * 
	     * @return		加载的会话列表。
	     *
	     * \~english
	     * Loads all conversations from the local database into the memory.
		 * 
	     * To accelerate the loading, call this method immediately after the user is logged in.
	     * 
	     * @return      The list of loaded conversations.
	     */
        public List<Conversation> LoadAllConversations()
        {
            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "loadAllConversations", null, null);

            if (null == json || json.Length == 0) return new List<Conversation>();

            return Conversation.ListFromJson(json);
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
            jo_param.Add("msgId", messageId);
            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "getMessage", jo_param, null);

            if (null == json || json.Length == 0) return null;
            return new Message(json);
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
            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "markAllChatMsgAsRead", null, null);
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
        public void RecallMessage(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "recallMessage", jo_param, (null != callback) ? callback.callbackId : "");
        }

        /**
		 * \~chinese
		 * 重新发送指定消息。
		 *
		 * 异步方法。
		 *
		 * @param messageId 重发消息的 ID。
		 * @param callback  重发结果回调，详见 {@link CallBack}。
		 * @return			重发的消息对象。
		 *
		 *
		 * \~english
		 * Resends the message.
		 *
		 * This is an asynchronous method.
		 *
		 * @param message	The ID of the message to be resent.
		 * @param callback  The resending status callback. See {@link CallBack}.
		 * @return			The message that is resent.
		 */
        public Message ResendMessage(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);

            callbackManager.AddCallbackAction<Object>(callback, null);
            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "resendMessage", jo_param, (null != callback) ? callback.callbackId : "");

            if (null == json || json.Length == 0) return null;

            return new Message(json);
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
		 * @param timestamp  查询的 Unix 时间戳，单位为毫秒。
		 * @param maxCount   查询的最大消息数。
		 * @param from       消息来源，一般指会话 ID。
		 * @param direction	 查询方向，详见 {@link MessageSearchDirection}。
		 * @return           消息列表。
		 *
		 * \~english
		 * Queries local messages.
		 * 
		 * **Note**
		 * If you want to query a great number of messages, pay attention to the memory consumption. A maximum number of 200 messages can be retrieved each time.
		 *
		 * @param keywords   The keyword for query. The data format is String.
		 * @param timestamp  The Unix timestamp for query, which is in milliseconds.
		 * @param maxCount   The maximum number of messages to retrieve.
		 * @param from       The message source, which is usually a conversation ID.
		 * @param direction	 The query direction. See {@link MessageSearchDirection}.
		 * @return           The list of messages.
		 */
        public List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("keywords", keywords);
            jo_param.Add("from", from ?? "");
            jo_param.Add("count", maxCount);
            jo_param.Add("timestamp", timestamp.ToString());
            jo_param.Add("direction", direction == MessageSearchDirection.UP ? "up" : "down");

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "searchChatMsgFromDB", jo_param, "");
            return List.ListFromJson<Message>(json);
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
		 * After this method is called, the sever will set the message status from unread to read. 
		 * The SDK triggers the {@link IChatManagerDelegate#OnConversationRead(string from, string to)} callback on the message sender's client, notifying that the messages are read. This also applies to multi-device scenarios.
		 *
		 * @param conversationId	The conversation ID.
		 * @param callback			The result callback. See {@link CallBack}.
		 */
        public void SendConversationReadAck(string conversationId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "ackConversationRead", jo_param, (null != callback) ? callback.callbackId : "");
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
                UpdatedMsg(_cbid, _json);
                DeleteFromMsgMap(_cbid);
                return null;
            };

            callbackManager.AddCallbackAction<Object>(callback, process);

            string cbid = (null != callback) ? callback.callbackId : message.MsgId;

            AddMsgMap(cbid, message);

            JSONObject jo_param = message.ToJsonObject();

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "sendMessage", jo_param, (null != callback) ? callback.callbackId : "");

            if (json.Length > 0)
            {
                UpdatedMsg(cbid, json);
            }
        }

        /**
		 * \~chinese
		 * 发送消息已读回执。
		 * 
		 * 该方法会通知服务器将此消息置为已读，消息发送方（包含多端多设备）将会收到 {@link IChatManagerDelegate#OnMessagesRead(List<Message>)} 回调。
		 * 
		 * @param messageId		消息 ID。
		 * @param callback		发送回执的结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends the read receipt of a message to the server.
		 * 
		 * After this method is called, the sever will set the message status from unread to read. 
		 *
		 * The SDK triggers the {@link IChatManagerDelegate#OnMessagesRead(List<Message>)} callback on the message sender's client, notifying that the messages are read. This also applies to multi-device scenarios.
		 *
		 * @param messageId		The message ID.
		 * @param callback		The result callback. See{@link CallBack}.
		 */
        public void SendMessageReadAck(string messageId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "ackMessageRead", jo_param, (null != callback) ? callback.callbackId : "");
        }

        /**
        * \~chinese
        * 发送群消息已读回执。
        *
        * 前提条件：设置了 {@link Options#RequireAck(boolean)} 和 {@link Message#IsNeedGroupAck(boolean)}。
        *
        * 参考：
        * 发送单聊消息已读回执，详见 {@link #SendMessageReadAck(String)} ;
        * 会话已读回执，详见 {@link #SendConversationReadAck(String)}。
        *
        * @param messageId     消息 ID。
        * @param ackContent    回执信息。`ackContent` 属性是用户自己定义的关键字，接收后，解析出自定义的字符串，可以自行处理。
        * @param handle		发送回执的结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Sends the group message receipt to the server.
        *
        * You can only call the method after setting the following method: {@link Options#RequireAck(boolean)} and {@link Message#IsNeedGroupAck(boolean)}.
        *
        * Reference:
        * To send the one-to-one chat message receipt to server, call {@link #SendMessageReadAck(String)};
        * To send the conversation receipt to the server, call {@link #SendConversationReadAck(String)}.
        *
        * @param messageId     The message ID.
        * @param ackContent    The ack content information. Developer self-defined command string that can be used for specifying custom action/command.
        * @param handle		The result callback. See{@link CallBack}.
    */
        public void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);
            jo_param.Add("content", ackContent);
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "ackMessageRead", jo_param, (null != callback) ? callback.callbackId : "");
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

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "updateChatMessage", jo_param, "");
           
            if (null == json || json.Length == 0) return false;

            JSONObject jsonObject = JSON.Parse(json).AsObject;
            return jsonObject["ret"].AsBool; // TODO: need to check with teammate
        }

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
		 * @param timeStamp	The starting Unix timestamp for removal.
		 * @param handle	The removal result callback. See {@link CallBack}.
		 */

        public void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("timestamp", timeStamp.ToString());
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "removeMessageBeforeTimestamp", jo_param, (null != callback) ? callback.callbackId : "");
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
		 * @param handle					Callback for whether the conversation is deleted. See {@link CallBack}.
		 */
        public void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", Conversation.ConversationTypeToInt(conversationType));
            jo_param.Add("isDeleteServerMessages", isDeleteServerMessages);
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "deleteConversationFromServer", jo_param, (null != callback) ? callback.callbackId : "");
        }

        /**
         * 获取翻译服务支持的语言。
         * @param callBack 完成的回调，详见 {@link #ValueCallBack()}。
         *
         * \~english
         * Fetches all languages what the translate service supports.
         * @param callBack The result callback，see {@link #ValueCallBack()}.
         */
        public void FetchSupportLanguages(ValueCallBack<List<SupportLanguage>> callback = null)
        {
            Process process = (_cbid, _json) =>
            {
                List<SupportLanguage> list = List.ListFromJson<SupportLanguage>(_json);
                return list;
            };
            callbackManager.AddCallbackAction<List<SupportLanguage>>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "fetchSupportLanguages", null, (null != callback) ? callback.callbackId : "");
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
         * @param languages The list of the target languages.
         * @param callBack The result callback，see {@link #CallBack()}.
         */
        public void TranslateMessage(Message message, List<string> targetLanguages, ValueCallBack<Message> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("message", message.ToJsonObject());
            jo_param.Add("languages", JsonString.JsonStringFromStringList(targetLanguages));

            Process process = (_cbid, _json) =>
            {
                Message msg = new Message(_json);
                return msg;
            };

            callbackManager.AddCallbackAction<Message>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "translateMessage", jo_param, (null != callback) ? callback.callbackId : "");
        }

        /**
         * \~chinese
         * 从服务器获取群组消息回执详情。
         *
         * 分页获取。
         *
         * 参考：
         * 发送群组消息回执，详见 {@link #SendReadAckForGroupMessage}。
         *
         * 异步方法。
         *
         * @param messageId		消息 ID。
		 * @param groupId		群组 ID。
         * @param pageSize		每页获取群消息已读回执的条数。
         * @param startAckId    已读回执的 ID，如果为空，从最新的回执向前开始获取。
         * @param callBack      结果回调，成功执行 {@link ValueCallBack#onSuccess(Object)}，失败执行 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Fetches the ack details for group messages from server.
         *
         * Fetches by page.
         *
         * Reference:
         * If you want to send group message receipt, see {@link #SendReadAckForGroupMessage}.
         *
         * This is an asynchronous method.
         *
         * @param msgId			The message ID.
		 * @param groupId		The group ID。
         * @param pageSize		The number of records per page.
         * @param startAckId    The start ID for fetch receipts, can be null. If you set it as null, the SDK will start from the server's latest receipt.
         * @param callBack		The result callback, if successful, the SDK will execute the method {@link ValueCallBack#onSuccess(Object)},
         *                      if the call failed, the SDK will execute the method {@link ValueCallBack#onError(int, String)}.
         */
        public void FetchGroupReadAcks(string messageId, string groupId, int pageSize = 20, string startAckId = null, ValueCallBack<CursorResult<GroupReadAck>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msg_id", messageId);
            jo_param.Add("pageSize", pageSize);
            jo_param.Add("groupId", groupId);
            jo_param.Add("ack_id", startAckId ?? "");

            Process process = (_cbid, _json) =>
            {
                CursorResult<GroupReadAck>.ItemCallback parse = (JSONObject jo) =>
                {
                    return new GroupReadAck(jo.Value);
                };

                CursorResult<GroupReadAck> cursor_result = new CursorResult<GroupReadAck>(_json, parse);
                return cursor_result;
            };

            callbackManager.AddCallbackAction<CursorResult<GroupReadAck>>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "fetchGroupReadAcks", jo_param, (null != callback) ? callback.callbackId : "");
        }

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
         * @param reason		The report Reason.
         *
         * @param callBack The result callback，see {@link #CallBack()}.
         */
        public void ReportMessage(string messageId, string tag, string reason, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);
            jo_param.Add("tag", tag);
            jo_param.Add("reason", reason);

            callbackManager.AddCallbackAction<object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "reportMessage", jo_param, (null != callback) ? callback.callbackId : "");
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
         * Adds a reaction.
         *
         * This is an asynchronous method.
         *
         * @param messageId The message ID.
         * @param reaction  The message reaction.
         * @param callback  The result callback which contains the error information if the method fails.
         */
        public void AddReaction(string messageId, string reaction, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);
            jo_param.Add("reaction", reaction);

            callbackManager.AddCallbackAction<object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "addReaction", jo_param, (null != callback) ? callback.callbackId : "");
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
         * Deletes a reaction.
         *
         * This is an asynchronous method.
         *
         * @param messageId The message ID.
         * @param reaction  The reaction content.
         * @param callback  The result callback which contains the error information if the method fails.
         */

        public void RemoveReaction(string messageId, string reaction, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);
            jo_param.Add("reaction", reaction);

            callbackManager.AddCallbackAction<object>(callback, null);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "removeReaction", jo_param, (null != callback) ? callback.callbackId : "");
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
        * @param groupId        The group ID, which is invalid only when the chat type is group chat.
        * @param callback       The result callback, which contains the Reaction list under the specified message ID（The user list of EMMessageReaction is the summary data, which only contains the information of the first three users）.
        */
        public void GetReactionList(List<string> messageIdList, MessageType chatType, string groupId, ValueCallBack<Dictionary<string, List<MessageReaction>>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgIds", JsonString.JsonStringFromStringList(messageIdList));
            jo_param.Add("groupId", groupId);

            //TODO: need to check
            if (chatType == MessageType.Group)
            {
                jo_param.Add("type", "groupchat");
            }
            else
            {
                jo_param.Add("type", "chat");
            }

            Process process = (_cbid, _json) =>
            {
                Dictionary<string, List<MessageReaction>> dict = null;
                JSONNode jn = JSON.Parse(_json);
                if (null != jn && jn.IsObject)
                {
                    dict = MessageReaction.DictFromJsonObject(jn.AsObject);
                } 
                else
                {
                    dict = new Dictionary<string, List<MessageReaction>>();
                }
                return dict;
            };

            callbackManager.AddCallbackAction<Dictionary<string, List<MessageReaction>>>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "getReactionList", jo_param, (null != callback) ? callback.callbackId : "");
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
        * Gets the reaction details.
        *
        * This is an asynchronous method.
        *
        * @param messageId    The message ID.
        * @param reaction     The reaction content.
        * @param cursor       The query cursor.
        * @param pageSize     The number of Reactions you expect to get on each page.
        * @param callback     The result callback, which contains the reaction list obtained from the server and the cursor for the next query. Returns null if all the data is fetched.
        */
        public void GetReactionDetail(string messageId, string reaction, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<MessageReaction>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("msgId", messageId);
            jo_param.Add("reaction", reaction);
            jo_param.Add("cursor", cursor ?? "");
            jo_param.Add("pageSize", pageSize);

            Process process = (_cbid, _json) =>
            {
                CursorResult<MessageReaction>.ItemCallback parse = (JSONObject jo) =>
                {
                    return new MessageReaction(jo.Value);
                };

                CursorResult<MessageReaction> cursor_result = new CursorResult<MessageReaction>(_json, parse);
                return cursor_result;
            };

            callbackManager.AddCallbackAction<CursorResult<MessageReaction>>(callback, process);
            CWrapperNative.NativeCall(NAME_CHATMANAGER, "getReactionDetail", jo_param, (null != callback) ? callback.callbackId : "");
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

        internal void NativeEventHandle(string method, string jsonString)
        {
            if (delegater.Count == 0 || null == method || method.Length == 0) return;

            if (method.CompareTo("OnMessagesReceived") == 0)
            {
                List<Message> list = List.ListFromJson<Message>(jsonString);
                if(list.Count > 0)
                {
                    foreach (IChatManagerDelegate it in delegater)
                    {
                        it.OnMessagesReceived(list);
                    }
                }
            }
            else if (method.CompareTo("OnCmdMessagesReceived") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnMessagesRead") == 0)
            {
                List<Message> list = List.ListFromJson<Message>(jsonString);
                if (list.Count > 0)
                {
                    foreach (IChatManagerDelegate it in delegater)
                    {
                        it.OnMessagesRead(list);
                    }
                }
            }
            else if (method.CompareTo("OnMessagesDelivered") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnMessagesRecalled") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnReadAckForGroupMessageUpdated") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnGroupMessageRead") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnConversationsUpdate") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnConversationRead") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("MessageReactionDidChange") == 0)
            {
                //TODO
            }
        }
    }
}