using System.Collections.Generic;

namespace AgoraChat
{
    internal abstract class IMessageManager
    {
        internal abstract List<MessageReaction> GetReactionList(string MessageId);

        internal abstract int GetGroupAckCount(string messageId);

        // 先不使用，需要再确认
        internal abstract bool GetHasDeliverAck(string messageId);
        // 先不使用，需要再确认
        internal abstract bool GetHasReadAck(string messageId);

        internal abstract ChatThread GetChatThread(string messageId);
    }
}
