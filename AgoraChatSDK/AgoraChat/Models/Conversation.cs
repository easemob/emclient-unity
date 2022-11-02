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
                Id = jo["con_id"].Value;
                Type = ConversationTypeFromInt(jo["type"].AsInt);
                IsThread = jo["isThread"].AsBool;
            }
        }

        internal override JSONObject ToJsonObject()
        {
            return null;
        }

        internal static List<Conversation> ListFromJson(string json)
        {
            List<Conversation> list = new List<Conversation>();

            if (null == json || json.Length == 0) return list;

            JSONNode jsonArray = JSON.Parse(json);
            if (jsonArray != null && jsonArray.IsArray)
            {
                foreach (JSONNode v in jsonArray.AsArray)
                {
                    if (v.IsString)
                    {
                        Conversation conv = new Conversation(v.Value);
                        list.Add(conv);
                    }
                }
            }
            return list;
        }

        internal static int ConversationTypeToInt(ConversationType type)
        {
            int intType = 0;
            switch (type)
            {
                case ConversationType.Chat: intType = 0; break;
                case ConversationType.Group: intType = 1; break;
                case ConversationType.Room: intType = 2; break;
            }
            return intType;
        }

        internal static ConversationType ConversationTypeFromInt(int intType)
        {
            ConversationType type = ConversationType.Chat;
            switch (intType)
            {
                case 0: type = ConversationType.Chat; break;
                case 1: type = ConversationType.Group; break;
                case 2: type = ConversationType.Room; break;
            }
            return type;
        }
    }
}


