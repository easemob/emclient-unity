using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{

    /**
     * \~chinese
     * 消息 Reaction 变更实体类。
     * 
     * \~english
     * The message Reaction change entity class.
     */
    public class MessageReactionChange
    {

        /**
         * \~chinese
         * Reaction 会话 ID。
         * 
         * \~english
         * The conversation ID.
         */
        public string ConversationId;
        /**
         * \~chinese
         * Reaction 父消息 ID。
         * 
         * \~english
         * The parent message ID of the Reaction.
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

        static internal MessageReactionChange FromJsonObject(JSONNode jn)
        {            
            if (!jn.IsNull && jn.IsObject)
            {
                MessageReactionChange reactionChange = new MessageReactionChange();
                JSONObject jo = jn.AsObject;
                reactionChange.ConversationId = jo["conversationId"].Value;
                reactionChange.MessageId = jo["messageId"].Value;
                reactionChange.ReactionList = MessageReaction.ListFromJsonObject(jo["reactionList"]);
                return reactionChange;
            }
            else
                return null;
        }

        static internal MessageReactionChange FromJson(string json)
        {
            Debug.Log($"FromJson json : {json}");
            if (null != json && json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                return FromJsonObject(jn);
            }
            else
                return null;
        }

        static internal List<MessageReactionChange> ListFromJson(string json)
        {
            Debug.Log($"ListFromJson json : {json}");

            List<MessageReactionChange> list = new List<MessageReactionChange>();
            if (null == json || json.Length == 0) return list;

            JSONNode jsonArray = JSON.Parse(json);
            if (null != jsonArray && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    if (v.IsObject)
                    {
                        MessageReactionChange rectionChange = FromJsonObject(v);
                        if (null != rectionChange)
                            list.Add(rectionChange);
                    }
                }
            }
            return list;
        }
    }
}
