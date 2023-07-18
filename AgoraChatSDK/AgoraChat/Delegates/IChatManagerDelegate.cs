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
 	     * @param messages 被撤回的消息列表。
 	     *
 	     * \~english
 	     * Occurs when a received message is recalled.
 	     * 
 	     * @param messages  The recalled message(s).
 	     */
		void OnMessagesRecalled(List<Message> messages);

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
         * Occurs when the reactions changed.
         *
         * @param list The changed reaction list.
         */
		void MessageReactionDidChange(List<MessageReactionChange> list);

        /**
         * \~chinese
         * 消息内容被修改回调。
         *
         * @param Message       被改变的消息。
         * @param operatorId    修改消息的操作人Id。
         * @param operationTime 修改消息的时间点。
         *
         *  \~english
         * Occurs when user modify message content.
         *
         * @param Message       The modified message.
         * @param operatorId    The operator Id who modified the message.
         * @param operationTime The time when the message was modified.
         */
        void OnMessageContentChanged(Message msg, string operatorId, long operationTime);
    }
}