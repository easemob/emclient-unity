using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    internal class MessageManager : BaseManager
    {
        internal MessageManager(NativeListener listener) : base(listener, SDKMethod.messageManager)
        {
        }

        internal int GetGroupAckCount(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jsonNode = NativeGet(SDKMethod.groupAckCount, jo_param).GetReturnJsonNode();
            return jsonNode.IsNumber ? jsonNode.AsInt : 0;
        }

        internal bool GetHasDeliverAck(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.getHasDeliverAck, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool GetHasReadAck(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.getHasReadAck, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal List<MessageReaction> GetReactionList(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.getReactionList, jo_param).GetReturnJsonNode();
            return List.BaseModelListFromJsonArray<MessageReaction>(jn);
        }

        internal ChatThread GetChatThread(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.getChatThread, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<ChatThread>(jn);
        }

        internal PinnedInfo GetPinnedInfo(string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.getPinnedInfo, jo_param).GetReturnJsonNode();
            PinnedInfo pinnedInfo = ModelHelper.CreateWithJsonObject<PinnedInfo>(jn);
            if (null == pinnedInfo)
            {
                pinnedInfo = new PinnedInfo();
                pinnedInfo.PinnedBy = "";
                pinnedInfo.PinnedAt = 0;
            }
            return pinnedInfo;
        }
    }
}