using System.Collections.Generic;
namespace ChatSDK
{
    public interface IChatManagerDelegate
    {
        /// 收到消息[messages]
        void OnMessagesReceived(List<Message> messages);

        /// 收到cmd消息[messages]
        void OnCmdMessagesReceived(List<Message> messages);

        /// 收到[messages]消息已读
        void OnMessagesRead(List<Message> messages);

        /// 收到[messages]消息已送达
        void OnMessagesDelivered(List<Message> messages);

        /// 收到[messages]消息被撤回
        void OnMessagesRecalled(List<Message> messages);

        /// 会话列表变化
        void OnConversationsUpdate();

        /// 会话已读`from`是已读的发送方, `to`是已读的接收方
        void OnConversationRead(string from, string to);
    }
}
