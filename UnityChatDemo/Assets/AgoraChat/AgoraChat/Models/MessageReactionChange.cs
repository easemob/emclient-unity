using System.Collections.Generic;
using AgoraChat.SimpleJSON;
namespace AgoraChat
{
    /**
     *  \~chinese
     *  reaction 操作类。
     *
     *  \~english
     *  Operation class of reaction.
     */
    public class MessageReactionOperation : BaseModel
    {
        /**
         *  \~chinese
         *  操作者。
         *
         *  \~english
         *  Operator.
         */
        public string UserId;

        /**
         *  \~chinese
         *  发生变化的 reaction。
         *
         *  \~english
         *  Changed reaction.
         */
        public string Reaction;

        /**
         *  \~chinese
         *  操作。
         *
         *  \~english
         *  Operate.
         */
        public MessageReactionOperate operate;

        internal MessageReactionOperation() { }

        internal MessageReactionOperation(string jsonString) : base(jsonString) { }

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

        /**
         * \~chinese
         * Reaction 操作列表
         *
         * \~english
         * Reaction operation list
         */
        public List<MessageReactionOperation> OperationList;

        internal MessageReactionChange() { }

        internal MessageReactionChange(string jsonString) : base(jsonString) { }

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