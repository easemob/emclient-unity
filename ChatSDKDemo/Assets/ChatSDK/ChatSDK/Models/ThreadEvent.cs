using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    public class ThreadEvent
    {
        public string Tid;
        public string MessageId;
        public string ParentId;
        public string Owner;
        public string Name;
        public string From;
        public string To;
        public string Operation;
        public int MessageCount;
        public int MembersCount;
        public long CreateTimestamp;
        public long UpdateTimestamp;
        public long Timestamp;
        public Message LastMessage;

        internal JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.Add("tId", Tid);
            jo.Add("messageId", MessageId);
            jo.Add("parentId", ParentId);
            jo.Add("owner", Owner);
            jo.Add("name", Name);
            jo.Add("from", From);
            jo.Add("to", To);
            jo.Add("operation", Operation);
            jo.Add("messageCount", MessageCount);
            jo.Add("membersCount", MembersCount);
            jo.Add("createTimestamp", CreateTimestamp.ToString());
            jo.Add("udateTimestamp", UpdateTimestamp.ToString());
            jo.Add("timestamp", Timestamp.ToString());
            if(null != LastMessage)
                jo.Add("lastMessage", LastMessage.ToJson().ToString());

            return jo;
        }

        internal string ToJson()
        {
            return ToJsonObject().ToString();
        }

        static internal ThreadEvent FromJsonObject(JSONNode jn)
        {
            if (null == jn) return null;
            if (!jn.IsNull && jn.IsObject)
            {
                ThreadEvent thread = new ThreadEvent();
                JSONObject jo = jn.AsObject;
                thread.Tid = jo["tId"].Value;
                thread.MessageId = jo["messageId"].Value;
                thread.ParentId = jo["parentId"].Value;
                thread.Owner = jo["owner"].Value;
                thread.Name = jo["name"].Value;
                thread.From = jo["from"].Value;
                thread.To = jo["to"].Value;
                thread.Operation = jo["operation"].Value;
                thread.MessageCount = jo["messageCount"].AsInt;
                thread.MembersCount = jo["membersCount"].AsInt;
                thread.CreateTimestamp = long.Parse(jo["createTimestamp"].Value);
                thread.UpdateTimestamp = long.Parse(jo["updateTimestamp"].Value);
                thread.Timestamp = long.Parse(jo["timestamp"].Value);
                if(!jo["lastMessage"].IsNull && jo["lastMessage"].IsString)
                    thread.LastMessage = new Message(jo["lastMessage"].Value);
                return thread;
            }
            else
                return null;
        }

        static internal ThreadEvent FromJson(string json)
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

        static internal CursorResult<ThreadEvent> CursorThreadFromJson(string json)
        {
            Debug.Log($"CursorThreadFromJson json : {json}");

            CursorResult<ThreadEvent> cursorResult = new CursorResult<ThreadEvent>();
            cursorResult.Data = new List<ThreadEvent>();

            if (null != json && json.Length > 0)
            {
                JSONNode jn = JSON.Parse(json);
                if (null == jn) return cursorResult;

                JSONObject jo = jn.AsObject;

                cursorResult.Cursor = jo["cursor"].Value;

                JSONNode jn_list = JSON.Parse(jo["list"].Value);
                if(null != jn_list)
                {
                    JSONArray jsonArray = jn_list.AsArray;
                    foreach (JSONNode obj in jsonArray)
                    {
                        if (obj.IsObject)
                        {
                            cursorResult.Data.Add(ThreadEvent.FromJsonObject(obj));
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

        static internal int ThreadLeaveReasonToInt(ThreadLeaveReason reason)
        {
            int ret = 0;
            switch (reason)
            {
                case ThreadLeaveReason.LEAVE:
                    {
                        ret = 0;
                        break;
                    }
                case ThreadLeaveReason.BE_KICKED:
                    {
                        ret = 1;
                        break;
                    }
                case ThreadLeaveReason.DESTROYED:
                    {
                        ret = 2;
                        break;
                    }
            }
            return ret;
        }
        static internal ThreadLeaveReason ThreadLeaveReasonFromInt(int i)
        {
            ThreadLeaveReason ret = ThreadLeaveReason.LEAVE;
            switch (i)
            {
                case 0:
                    {
                        ret = ThreadLeaveReason.LEAVE;
                        break;
                    }
                case 1:
                    {
                        ret = ThreadLeaveReason.BE_KICKED;
                        break;
                    }
                case 2:
                    {
                        ret = ThreadLeaveReason.DESTROYED;
                        break;
                    }
            }
            return ret;
        }
    }
}
