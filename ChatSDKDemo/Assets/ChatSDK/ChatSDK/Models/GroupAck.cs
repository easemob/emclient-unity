using System;
using SimpleJSON;

namespace ChatSDK {
    public class GroupReadAck
    {

        public string AckId { get; internal set; }
        public string MsgId { get; internal set; }
        public string From { get; internal set; }
        public string Content { get; internal set; }
        public int Count { get; internal set; }
        public long Timestamp { get; internal set; }

        internal GroupReadAck(string jsonString) {
            JSONNode jn = JSON.Parse(jsonString);
            if (!jn.IsNull && jn.IsObject) {
                JSONObject jo = jn.AsObject;
                AckId = jo["ackId"].Value;
                MsgId = jo["msgId"].Value;
                From = jo["from"].Value;
                Content = jo["content"].Value;
                Count = jo["count"].AsInt;
                Timestamp = jo["timestamp"].AsInt;
            }
        }

        internal GroupReadAck()
        {

        }
    }
}

