using System.Collections.Generic;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
    * \~chinese
    * 消息 Reaction 变更实体类
    * 
    * \~english
    * The message Reaction change entity class.
    */
    [Preserve]
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

        [Preserve]
        internal MessageReactionChange() { }

        [Preserve]
        internal MessageReactionChange(string jsonString) : base(jsonString) { }

        [Preserve]
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
            jo.AddWithoutNull("convId", ConversationId);
            jo.AddWithoutNull("msgId", MessageId);
            jo.AddWithoutNull("reactions", JsonObject.JsonArrayFromList(ReactionList));
            return jo;
        }
    }
}