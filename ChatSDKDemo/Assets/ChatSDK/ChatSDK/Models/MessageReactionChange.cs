using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{

    /**
     * \~chinese
     * 消息 Reaction 变更实体类
     * 
     * \~english
     * The message reaction change entity class
     */
    public class MessageReactionChange
    {
        /**
         * \~chinese
         * Reaction父消息发送方
         * 
         * \~english
         * Reaction's parent message sender
         */
        public string From;

        /**
         * \~chinese
         * Reaction父消息接收方
         * 
         * \~english
         * Reaction's parent message receiver
         */
        public string To;

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

        static internal MessageReactionChange FromJsonObject(JSONNode jn)
        {            
            if (!jn.IsNull && jn.IsObject)
            {
                MessageReactionChange reactionChange = new MessageReactionChange();
                JSONObject jo = jn.AsObject;
                reactionChange.From = jo["from"].Value;
                reactionChange.To = jo["to"].Value;
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
