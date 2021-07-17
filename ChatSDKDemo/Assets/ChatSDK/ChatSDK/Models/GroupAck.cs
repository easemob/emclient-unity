using System;
using SimpleJSON;

namespace ChatSDK {

    /// <summary>
    /// 群已读
    /// </summary>
    public class GroupReadAck
    {

        /// <summary>
        /// 已读id
        /// </summary>
        public string AckId { get; internal set; }

        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgId { get; internal set; }

        /// <summary>
        /// 读消息用户id
        /// </summary>
        public string From { get; internal set; }

        /// <summary>
        /// 已读追加信息
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// 已读数量
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        public long Timestamp { get; internal set; }

        internal GroupReadAck(string jsonString) {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    AckId = jo["ackId"].Value;
                    MsgId = jo["msgId"].Value;
                    From = jo["from"].Value;
                    Content = jo["content"].Value;
                    Count = jo["count"].AsInt;
                    Timestamp = jo["timestamp"].AsInt;
                }
            }
        }
    }
}

