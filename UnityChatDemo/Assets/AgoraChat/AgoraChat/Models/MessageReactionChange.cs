using System.Collections.Generic;
using AgoraChat.SimpleJSON;
namespace AgoraChat
{
    /**
    * \~chinese
    * 消息 Reaction 变更实体类
    * 
    * \~english
    * The message reaction change entity class
    */
    public class MessageReactionChange : BaseModel
    {

        /**
         * \~chinese
         * Reaction 会话id
         * 
         * \~english
         * Reaction conversationId
         */
        public string ConversationId;
        /**
         * \~chinese
         * Reaction父消息ID
         * 
         * \~english
         * Reaction parent message ID
         */
        public string MessageId;

        /**
         * \~chinese
         * Reaction 列表
         * 
         * \~english
         * Reaction list
         */
        public List<MessageReaction> ReactionList;

        internal MessageReactionChange() { }

        internal MessageReactionChange(string jsonString) : base(jsonString) { }

        internal MessageReactionChange(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            ConversationId = jsonObject["convId"];
            MessageId = jsonObject["msgId"];
            ReactionList = List.BaseModelListFromJsonArray<MessageReaction>(jsonObject["reactions"]);
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.Add("convId", ConversationId);
            jo.Add("msgId", MessageId);
            jo.Add("reactions", JsonObject.JsonArrayFromList(ReactionList));
            return jo;
        }
    }
}