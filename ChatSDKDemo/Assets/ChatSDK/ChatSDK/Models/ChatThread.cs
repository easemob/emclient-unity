using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    public class ChatThread
    {
        public string Tid;
        public string MessageId;
        public string ParentId;
        public string Owner;
        public string Name;
        public int MessageCount;
        public int MembersCount;
        public long CreateAt;
        public Message LastMessage;

        // TODO: 不需要TOJSON吧。
        internal JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.Add("tId", Tid);
            jo.Add("messageId", MessageId);
            jo.Add("parentId", ParentId);
            jo.Add("owner", Owner);
            jo.Add("name", Name);
            jo.Add("messageCount", MessageCount);
            jo.Add("membersCount", MembersCount);
            jo.Add("createTimestamp", CreateAt.ToString());
            if (null != LastMessage)
                jo.Add("lastMessage", LastMessage.ToJson().ToString());

            return jo;
        }

        internal string ToJson()
        {
            return ToJsonObject().ToString();
        }

        static internal ChatThread FromJsonObject(JSONNode jn)
        {
            if (null == jn) return null;
            if (!jn.IsNull && jn.IsObject)
            {
                ChatThread thread = new ChatThread();
                JSONObject jo = jn.AsObject;
                thread.Tid = jo["tId"].Value;
                thread.MessageId = jo["messageId"].Value;
                thread.ParentId = jo["parentId"].Value;
                thread.Owner = jo["owner"].Value;
                thread.Name = jo["name"].Value;
                thread.MessageCount = jo["messageCount"].AsInt;
                thread.MembersCount = jo["membersCount"].AsInt;
                thread.CreateAt = long.Parse(jo["createTimestamp"].Value);
                if (!jo["lastMessage"].IsNull && jo["lastMessage"].IsString)
                    thread.LastMessage = new Message(jo["lastMessage"].Value);
                return thread;
            }
            else
                return null;
        }

        static internal ChatThread FromJson(string json)
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

        static internal CursorResult<ChatThread> CursorThreadFromJson(string json)
        {
            Debug.Log($"CursorThreadFromJson json : {json}");

            CursorResult<ChatThread> cursorResult = new CursorResult<ChatThread>();
            cursorResult.Data = new List<ChatThread>();

            if (null != json && json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                if (null == jn) return cursorResult;

                JSONObject jo = jn.AsObject;

                cursorResult.Cursor = jo["cursor"].Value;

                JSONNode jn_list = JSON.Parse(jo["list"].Value);
                if (null != jn_list)
                {
                    JSONArray jsonArray = jn_list.AsArray;
                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsObject)
                        {
                            cursorResult.Data.Add(ChatThread.FromJsonObject(obj));
                        }
                    }
                }
            }
            return cursorResult;
        }

        static internal Dictionary<string, Message> DictFromJson(string json)
        {
            Debug.Log($"DictFromJson json : {json}");
            Dictionary<string, Message> dict = new Dictionary<string, Message>();

            if (null != json && json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                if (null == jn) return dict;

                JSONObject jo = jn.AsObject;

                foreach (string s in jo.Keys)
                {
                    Message msg = new Message(jo[s].Value);
                    dict.Add(s, msg);
                }
            }
            return dict;
        }

    }
}
