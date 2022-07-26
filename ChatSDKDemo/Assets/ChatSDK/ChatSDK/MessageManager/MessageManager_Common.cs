using System.Collections.Generic;

namespace ChatSDK {
    internal class MessageManager_Common : IMessageManager
    {
        internal MessageManager_Common()
        {
        }

        internal override int GetGroupAckCount(string messageId)
        {
            throw new System.NotImplementedException();
        }

        internal override bool GetHasDeliverAck(string messageId)
        {
            throw new System.NotImplementedException();
        }

        internal override bool GetHasReadAck(string messageId)
        {
            throw new System.NotImplementedException();
        }

        internal override List<MessageReaction> GetReactionList(string MessageId)
        {
            throw new System.NotImplementedException();
        }

        internal override ChatThread GetChatThread(string messageId)
        {
            throw new System.NotImplementedException();
        }
    }

}
