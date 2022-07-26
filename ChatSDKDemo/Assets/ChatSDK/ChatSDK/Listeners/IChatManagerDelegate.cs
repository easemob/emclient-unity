using System.Collections.Generic;
namespace ChatSDK
{
    public interface IChatManagerDelegate
    {
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="messages">消息列表</param>
        void OnMessagesReceived(List<Message> messages);

        /// <summary>
        /// 收到cmd消息
        /// </summary>
        /// <param name="messages">cmd消息列表</param>
        void OnCmdMessagesReceived(List<Message> messages);

        /// <summary>
        /// 收到消息已读回执
        /// </summary>
        /// <param name="messages">已读消息列表param>
        void OnMessagesRead(List<Message> messages);

        /// <summary>
        /// 消息已送达回执
        /// </summary>
        /// <param name="messages">已送达消息列表</param>
        void OnMessagesDelivered(List<Message> messages);

        /// <summary>
        /// 收到消息被撤回
        /// </summary>
        /// <param name="messages">被撤回消息列表</param>
        void OnMessagesRecalled(List<Message> messages);

        /// <summary>
        /// 群消息已读数量变化
        /// </summary>
        void OnReadAckForGroupMessageUpdated();

        /// <summary>
        /// 群消息已读列表
        /// </summary>
        /// <param name="list"></param>
        void OnGroupMessageRead(List<GroupReadAck> list);

        /// <summary>
        /// 会话列表数量变化
        /// </summary>
        void OnConversationsUpdate();

        /// <summary>
        /// 收到会话已读回调
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        void OnConversationRead(string from, string to);

        void MessageReactionDidChange(List<MessageReactionChange> list);
    }
}
