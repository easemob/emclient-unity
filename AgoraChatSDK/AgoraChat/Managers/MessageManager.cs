using System.Collections.Generic;

namespace AgoraChat
{
    internal class MessageManager
    {
        internal MessageManager()
        {
        }

        internal int GetGroupAckCount(string messageId)
        {
            //TODO
            return 0;
        }

        internal bool GetHasDeliverAck(string messageId)
        {
            //TODO
            return false;
        }

        internal bool GetHasReadAck(string messageId)
        {
            //TODO
            return false;
        }

        internal List<MessageReaction> GetReactionList(string MessageId)
        {
            //TODO
            return null;
        }

        internal ChatThread GetChatThread(string messageId)
        {
            //TODO
            return null;
        }
    }
}