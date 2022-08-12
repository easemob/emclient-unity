using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    internal class MessageManager_iOS : IMessageManager
    {

        internal override int GetGroupAckCount(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("messageId", messageId);
            string jsonString = ChatAPIIOS.MessageManager_GetMethodCall("getGroupAckCount", obj.ToString());
            Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(jsonString);
            string countString = dict["count"];
            return int.Parse(countString);
        }

        internal override bool GetHasDeliverAck(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("messageId", messageId);
            string jsonString = ChatAPIIOS.MessageManager_GetMethodCall("getHasDeliverAck", obj.ToString());
            JSONNode jn = JSON.Parse(jsonString);
            return jn["hasDeliverAck"].AsBool;
        }

        internal override bool GetHasReadAck(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("messageId", messageId);
            string jsonString = ChatAPIIOS.MessageManager_GetMethodCall("getHasReadAck", obj.ToString());
            JSONNode jn = JSON.Parse(jsonString);
            return jn["hasReadAcked"].AsBool;
        }

        internal override List<MessageReaction> GetReactionList(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("messageId", messageId);
            string jsonString = ChatAPIIOS.MessageManager_GetMethodCall("getReactionList", obj.ToString());
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            JSONNode jn = JSON.Parse(jsonString);
            return MessageReaction.ListFromJsonObject(jn["reactionList"]);
        }

        internal override ChatThread GetChatThread(string messageId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("messageId", messageId);
            string jsonString = ChatAPIIOS.MessageManager_GetMethodCall("getChatThread", obj.ToString());
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            JSONNode jn = JSON.Parse(jsonString);
            return ChatThread.FromJsonObject(jn["chatThread"]);
        }
    }

}
