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
     * 消息 Reaction 实体类，有如下属性：
     *
     *   Reaction：消息 Reaction。
     *   UserCount：添加了指定 Reaction 的用户数量。
     *   UserList：添加了指定 Reaction 的用户列表。
     *   State 当前用户是否添加了该 Reaction。
     *
     * \~english
     * The message Reaction instance class, which has the following attributes:
     *
     *   Reaction: The message Reaction.
     *   UserCount: The count of users that added the Reaction.
     *   UserList: The list of users that added the Reaction.
     *   State: Whether the current user added this Reaction.
     */
    public class MessageReaction
    {
        /**
         * \~chinese
         * 获取 Reaction 内容。
         *
         * \~english
         * Gets the Reaction.
         */
        public string Rection;

        /**
         * \~chinese
         * 获取添加了指定 Reaction 的用户数量。
         *
         * \~english
         * Gets the count of users that added this Reaction.
         */
        public int Count;

        /**
         * \~chinese
         * 获取添加了指定 Reaction 的用户列表。
         *
         * **Note**
         *  只有通过 {@link IChatManager#GetReactionDetail} 接口获取的是全部用户的分页数据；其他相关接口如{@link IChatManager#GetReactionList}等都只包含前三个用户。
         *
         * @return 用户列表。
         *
         * \~english
         * Gets the list of users that added this Reaction.
         *
         * **Note**
         * {@link IChatManager#GetReactionDetail} can return the entire list of users that added this Reaction with pagination, whereas other methods such as {@link IChatManager#GetReactionList} can only return the first three users.
         * @return  The list of users that added this Reaction.
         */
        public List<string> UserList;

        /**
         * \~chinese
         * 获取当前用户是否添加过该 Reaction。
         *
         *  - `true`：是；
         *  - `false`：否。
         *
         * \~english
         * Gets whether the current user has added the Reaction.
         *
         *  - `true`: Yes.
         *  - `false`: No.
         */
        public bool State;
        //public UInt64 Ts; // not used yet

        internal JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.Add("reaction", Rection);
            jo.Add("count", Count);
            jo.Add("userList", TransformTool.JsonObjectFromStringList(UserList));
            jo.Add("state", State);
            return jo;
        }

        static internal string ListToJson(List<MessageReaction> list)
        {
            if (null == list || list.Count == 0) return "";

            JSONArray ja = new JSONArray();
            foreach (var it in list)
            {
                ja.Add(it.ToJsonObject());
            }
            return ja.ToString();
        }

        static internal MessageReaction FromJsonObject(JSONNode jn)
        {
            MessageReaction ret = null;
            if (!jn.IsNull && jn.IsObject)
            {
                MessageReaction reaction = new MessageReaction();
                JSONObject jo = jn.AsObject;
                if (!jo["reaction"].IsNull) reaction.Rection = jo["reaction"].Value;
                if (!jo["count"].IsNull)    reaction.Count = jo["count"].AsInt;
                if (!jo["userList"].IsNull) reaction.UserList = TransformTool.JsonArrayToStringList(jo["userList"]);
                if (!jo["state"].IsNull)    reaction.State = jo["state"].AsBool;
                ret = reaction;
            }
            return ret;
        }

        static internal MessageReaction FromJson(string json)
        {            
            MessageReaction ret = null;
            if (null != json && json.Length > 0)
            {
                Debug.Log($"FromJson json : {json}");
                JSONNode jn = JSON.Parse(json);
                ret = FromJsonObject(jn);
            }
            return ret;
        }

        static internal List<MessageReaction> ListFromJsonObject(JSONNode jn)
        {
            List<MessageReaction> list = new List<MessageReaction>();
            if (null != jn && jn.IsArray)
            {
                foreach (JSONNode v in jn.AsArray)
                {
                    if (v.IsObject)
                    {
                        MessageReaction reaction = MessageReaction.FromJsonObject(v);
                        if (null != reaction)
                            list.Add(reaction);
                    }
                }
            }
            return list;
        }

        static internal List<MessageReaction> ListFromJson(string json)
        {
            if (null == json || json.Length == 0) 
                return new List<MessageReaction>();

            Debug.Log($"ListFromJson json : {json}");

            JSONNode jsonArray = JSON.Parse(json);
            return ListFromJsonObject(jsonArray);
        }

        static internal Dictionary<string, List<MessageReaction>> MapFromJson(string json)
        {
            Dictionary<string, List<MessageReaction>> dict = new Dictionary<string, List<MessageReaction>>();
            if (null == json || json.Length == 0)
                return dict;

            Debug.Log($"MapFromJson json : {json}");

            JSONNode jn = JSON.Parse(json);
            if (null == jn) return dict;

            JSONObject jo = jn.AsObject;

            foreach (string s in jo.Keys)
            {
                dict.Add(s, ListFromJsonObject(jo[s]));
            }

            return dict;
        }
    }
}
