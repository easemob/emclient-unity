using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    public class Conversation : BaseModel
    {
        private ConversationManager manager { get => SDKClient.Instance.ConversationManager; }

        /**
         * \~chinese
         * 会话 ID。
         * 
         * \~english
         * The conversation ID.
         * 
        */
        public string Id { get; internal set; }

        /**
         * \~chinese
         * 会话类型。
         *
         * \~english
         * The conversation type.
         */
        public ConversationType Type { get; internal set; }


        /**
         * \~chinese
         * 判断该会话是否为子区会话。
         * @return 是否为子区会话。
         *
         * \~english
         * Check a conversation is thread or not.
         * @return a conversation is thread or not.
         */
        public bool IsThread { get; internal set; }

        //TODO:Add other APIs for conversation

        public Conversation(string id, ConversationType type, bool isThread)
        {
            Id = id;
            Type = type;
            IsThread = isThread;
        }

        internal Conversation(string json) : base(json) { }
        internal Conversation(JSONObject jo) : base(jo) { }

        internal override void FromJsonObject(JSONObject jo)
        {
            if (!jo.IsNull)
            {
                Id = jo["con_id"];
                Type = jo["type"].AsInt.ToConversationType();
                IsThread = jo["isThread"];
            }
        }

        internal override JSONObject ToJsonObject()
        {
            return null;
        }

    }
}


