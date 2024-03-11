using System.Collections.Generic;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     *  \~chinese
     *  Reaction 操作类。
     *
     *  \~english
     *  The Reaction operation class.
     */
    [Preserve]
    public class MessageReactionOperation : BaseModel
    {
        /**
         *  \~chinese
         *  操作者。
         *
         *  \~english
         *  The user ID of the operator.
         */
        public string UserId;

        /**
         *  \~chinese
         *  发生变化的 Reaction。
         *
         *  \~english
         *  The changed Reaction.
         */
        public string Reaction;

        /**
         *  \~chinese
         *  操作。
         *
         *  \~english
         *  The Reaction operation.
         */
        public MessageReactionOperate operate;

        [Preserve]
        internal MessageReactionOperation() { }

        [Preserve]
        internal MessageReactionOperation(string jsonString) : base(jsonString) { }

        [Preserve]
        internal MessageReactionOperation(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            UserId = jsonObject["userId"];
            Reaction = jsonObject["reaction"];
            operate = jsonObject["operate"].AsInt.ToMessageReactionOperate();
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("userId", UserId);
            jo.AddWithoutNull("reaction", Reaction);
            jo.AddWithoutNull("operate", operate.ToInt());
            return jo;
        }
    };

    /**
    * \~chinese
    * 消息 Reaction 变更实体类。
    *
    * \~english
    * The message Reaction change entity class.
    */
    [Preserve]
    public class MessageReactionChange : BaseModel
    {

        /**
         * \~chinese
         * Reaction 会话 ID。
         *
         * \~english
         * The conversation ID to which the Reaction belongs.
         */
        public string ConversationId;
        /**
         * \~chinese
         * Reaction 父消息 ID。
         *
         * \~english
         * The ID of the parent message of the Reaction.
         */
        public string MessageId;

        /**
         * \~chinese
         * Reaction 列表。
         *
         * \~english
         * The Reaction list.
         */
        public List<MessageReaction> ReactionList;

        /**
         * \~chinese
         * Reaction 操作列表
         *
         * \~english
         * The Reaction operation list.
         */
        public List<MessageReactionOperation> OperationList;

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
            OperationList = List.BaseModelListFromJsonArray<MessageReactionOperation>(jsonObject["operations"]);
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("convId", ConversationId);
            jo.AddWithoutNull("msgId", MessageId);
            jo.AddWithoutNull("reactions", JsonObject.JsonArrayFromList(ReactionList));
            jo.AddWithoutNull("operations", JsonObject.JsonArrayFromList(OperationList));
            return jo;
        }
    }
}