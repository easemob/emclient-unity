using System.Collections.Generic;
using System.Text;

namespace ChatSDK {
    internal class MessageManager_Common : IMessageManager
    {
        internal MessageManager_Common()
        {
        }

        internal override int GetGroupAckCount(string messageId)
        {
            return ChatAPINative.ChatManager_GetGroupAckCount(messageId);
        }

        internal override bool GetHasDeliverAck(string messageId)
        {
            return ChatAPINative.ChatManager_GetHasDeliverAck(messageId);
        }

        internal override bool GetHasReadAck(string messageId)
        {
            return ChatAPINative.ChatManager_GetHasReadAck(messageId);
        }

        internal override List<MessageReaction> GetReactionList(string MessageId)
        {
            int len = 2048;
            StringBuilder sbuilder = new StringBuilder(len);
            ChatAPINative.ChatManager_GetReactionListForMsg(MessageId, sbuilder, len);
            return MessageReaction.ListFromJson(TransformTool.GetUnicodeStringFromUTF8(sbuilder.ToString()));
        }

        internal override ChatThread GetChatThread(string messageId)
        {
            int len = 2048;
            StringBuilder sbuilder = new StringBuilder(len);
            ChatAPINative.ChatManager_GetChatThreadForMsg(messageId, sbuilder, len);
            return ChatThread.FromJson(TransformTool.GetUnicodeStringFromUTF8(sbuilder.ToString()));
        }
    }

}
