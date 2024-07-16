using System;
using System.Collections.Generic;

namespace AgoraChat
{
	/**
         * \~chinese
	     * 聊天管理器回调接口。
         *
         * \~english
	     * The chat manager callback interface.
	     * 
	     */
	public interface IChatManagerDelegate
	{
		/**
         * \~chinese
	     * 收到消息回调。
		 * 
	     * 在收到文本、图片、视频、语音、地理位置和文件等消息时，通过此回调通知用户。
	     * 
	     * @param messages 收到的消息列表。
         *
         * \~english
	     * Occurs when a messages is received.
		 * 
	     * This callback is triggered to notify the user when a message such as a text, image, video, voice, geographical location, or file is received.
	     * 
	     * @param messages  The received message(s).
	     */
		void OnMessagesReceived(List<Message> messages);
		/**
         * \~chinese
	     * 收到命令消息。
		 * 
	     * 与 {@link #onMessageReceived(List)} 不同, 这个回调只由命令消息触发，命令消息通常不对用户展示。
	     * 
	     * @param messages 收到的命令消息列表。
	     *
         * \~english
	     * Occurs when a command message is received.
		 *
	     * Unlike {@link #onMessageReceived(List)}, this callback is triggered only by the reception of a command message that is usually invisible to users.
	     * 
	     * @param messages  The received command message(s).
	     *
	     */
		void OnCmdMessagesReceived(List<Message> messages);

		/**
         * \~chinese
         * 收到消息的已读回执回调。
         * 
         * @param messages 已读消息列表。
         *
         * \~english
         * Occurs when a read receipt is received for a message. 
         * 
         * @param messages  The read message(s).
         */
		void OnMessagesRead(List<Message> messages);

		/**
         * \~chinese
         * 收到消息的送达回执回调。
         * 
         * @param messages 已送达的消息列表。
         *
         * \~english
         * Occurs when a delivery receipt is received.
         * 
         * @param messages  The delivered message(s).
         */
		void OnMessagesDelivered(List<Message> messages);

        /**
	    * \~chinese
	    * 撤回收到消息的回调。
	    *
	    * @param recallMessagesInfo 被撤回的信息列表。
	    * 如果撤回的是离线期间的消息，`RecallMessageInfo`对象中的`RecallMessage`会变为空对象。
	    *
	    * \~english
	    * Occurs when a received message is recalled.
	    * If the recalled message is offline, the `RecallMessage` in `RecallMessageInfo` object will be an empty object.
	    *
	    * @param recallMessagesInfo  The recalled information list.
	    */
        void OnMessagesRecalled(List<RecallMessageInfo> recallMessagesInfo);

        /**
	     * \~chinese
	     * 收到群组消息的读取状态更新时触发的回调。
	     *
	     * \~english
	     * Occurs when the read status updates of a group message is received.
	     */
        void OnReadAckForGroupMessageUpdated();

		/**
	     * \~chinese
	     * 收到群组消息的已读回执的回调。
	     * 
	     * @param list 群消息已读回执列表。
	     *
	     * \~english
	     * Occurs when a read receipt is received for a group message.
	     * 
	     * @param list The read receipt(s) for group message(s).
	     * 
	     */
		void OnGroupMessageRead(List<GroupReadAck> list);

		/**
        * \~chinese
        *  会话列表数量变化回调。
        *
        * \~english
        * Occurs when the number of conversations changes.
        * 
        */
		void OnConversationsUpdate();

		/**
	     * \~chinese
	     * 收到会话已读回调。
	     *
	     * 回调此方法的场景：
		 *
	     * - 消息被接收方阅读（发送了会话已读回执）。
		 *
	     * SDK 在接收到此事件时，会将本地数据库中该会话中消息的 `isAcked` 属性置为 `true`。
		 *
	     * - 多端多设备登录场景下，一端发送会话已读回执，服务器端会将会话的未读消息数置为 `0`，
		 *
		 * 同时其他端会回调此方法，并将本地数据库中该会话中消息的 `isRead` 属性置为 `true`。
		 *
	     * @param from 已读回执的发送方。
	     * @param to   已读回执的接收方。
	     *
	     * \~english
	     * Occurs when the read receipt is received for a conversation.
	     *
	     * This callback occurs in either of the following scenarios:
		 *
	     * - The message is read by the recipient (The read receipt for the conversation is sent).
		 *
	     *   Upon receiving this event, the SDK sets the `isAcked` attribute of the messages in the conversation to `true` in the local database.
	     * 
		 * - In the multi-device login scenario, when one device sends a read receipt for a conversation, the server will set the number of unread messages of this conversation to `0`.
		 *  In this case, the callback occurs on the other devices where the SDK will set `isRead` attribute of the messages in the conversation to `true` in the local database.
	     * 
		 * @param from The ID of the user who sends the read receipt.
	     * @param to   The ID of the user who receives the read receipt.
	     */
		void OnConversationRead(string from, string to);

		/**
         * \~chinese
         * Reaction 发生变化。
         *
         * @param list 改变的 Reaction 列表。
         *
         *  \~english
         * Occurs when the Reactions changed.
         *
         * @param list The changed Reaction list.
         */
		void MessageReactionDidChange(List<MessageReactionChange> list);

        /**
         * \~chinese
         * 消息内容被修改回调。
         *
         * @param Message       修改的消息对象，其中的 message body 包含消息修改次数、最后一次修改的操作者、最后一次修改时间等信息。
         *               你也可通过 `onMessageContentChanged` 回调获得最后一次修改的操作者和最后一次修改时间等信息。
         * @param operatorId    最后一次修改消息的用户 ID。
         * @param operationTime 消息的最后一次修改时间戳，单位为毫秒。
         *
         *  \~english
         * Occurs when a sent message is modified.
         *
         * @param Message       The modified message object, where the message body contains the information such as the number of message modifications, the operator of the last modification, and the last modification time.
	     * 	Also, you can get the operator of the last message modification and the last modification time via the `onMessageContentChanged` method.
         * @param operatorId    The user ID of the operator that modified the message last time.
         * @param operationTime The last message modification time. It is a UNIX timestamp in milliseconds.
         */
        void OnMessageContentChanged(Message msg, string operatorId, long operationTime);

        /**
         * \~chinese
         * 消息置顶回调。
         *
         * @param messageId      置顶状态发生改变的消息 ID。
         * @param conversationId 消息所属的会话 ID。
         * @param operatorId     进行置顶操作的用户 ID。
         * @param operationTime  消息的最后一次置顶操作的时间戳，单位为毫秒。
         *
         *  \~english
         * Occurs when a message is pinned or unpinned.
         *
         * @param messageId      The message ID whose pinning status has changed.
         * @param conversationId The ID of the conversation to which the message belongs.
         * @param operatorId     The user ID of the operator that pinned or unpinned the message last time.
         * @param operationTime  The time when the message is pinned or unpinned last time. It is a UNIX timestamp in milliseconds.
         */
        void OnMessagePinChanged(string messageId, string conversationId, bool isPinned, string operatorId, long operationTime);
    }
}