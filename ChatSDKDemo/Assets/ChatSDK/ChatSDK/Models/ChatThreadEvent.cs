using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    public class ChatThreadEvent
    {
        public string From { get; internal set; }
        public ChatThreadOperation Operation { get; internal set; }
        public ChatThread ChatThread { get; internal set; }

        static internal ChatThreadEvent FromJsonObject(JSONNode jn)
        {
            if (null == jn) return null;
            if (!jn.IsNull && jn.IsObject)
            {
                ChatThreadEvent thread = new ChatThreadEvent();
                JSONObject jo = jn.AsObject;
                thread.From = jo["from"].Value;
                thread.Operation = (ChatThreadOperation)jo["operation"].AsInt;
                thread.ChatThread = ChatThread.FromJsonObject(jo["chatThread"]);
                return thread;
            }
            else
                return null;
        }

        static internal ChatThreadEvent FromJson(string json)
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
    }
}
