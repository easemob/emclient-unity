using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    public class MessageReaction
    {
        public string Rection;
        public int Count;
        public List<string> UserList;
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
