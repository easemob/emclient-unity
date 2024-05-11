using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 群组消息的已读回执类。
     * 
     * \~english
     * The class for read receipts of group messages.
     * 
     */
    [Preserve]
    public class GroupReadAck : BaseModel
    {
        /**
         * \~chinese
         * 群组消息的已读回执 ID。
         * 
         * \~english
         * The ID of the read receipt of a group message.
         */
        public string AckId { get; internal set; }

        /**
         * \~chinese
         * 群组消息 ID。
         *
         * \~english
         * The ID of the group message.
         */
        public string MsgId { get; internal set; }

        /**
         * \~chinese
         * 发送已读回执的用户 ID。
         *
         * \~english
         * The ID of the user who sends the read receipt.
         */
        public string From { get; internal set; }

        /**
         * \~chinese
         * 已读回执扩展内容。
         * 
         * 已读回执扩展为发送已读回执方法 {@link XXX} 中第三个参数传入的内容。
         *
         * \~english
         * The extension information of a read receipt.
         * 
         * The read receipt extension is passed as the third parameter in ({@link XXX}) that is the method of sending the read receipt.
         */
        public string Content { get; internal set; }

        /**
         * \~chinese
         * 发送的已读回执的数量。
         *
         * \~english
         * The number of read receipts that are sent for group messages.
         */
        public int Count { get; internal set; }

        /**
         * \~chinese
         * 发送已读回执的 Unix 时间戳。
         *
         * \~english
         * The Unix timestamp of sending the read receipt of a group message.
         */
        public long Timestamp { get; internal set; }

        [Preserve]
        internal GroupReadAck() { }

        [Preserve]
        internal GroupReadAck(string jsonString) : base(jsonString) { }

        [Preserve]
        internal GroupReadAck(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            AckId = jsonObject["ackId"];
            MsgId = jsonObject["msgId"];
            From = jsonObject["from"];
            Content = jsonObject["content"];
            Count = jsonObject["count"];
            Timestamp = (long)jsonObject["timestamp"].AsDouble;
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("ackId", AckId);
            jo.AddWithoutNull("msgId", MsgId);
            jo.AddWithoutNull("from", From);
            jo.AddWithoutNull("content", Content);
            jo.AddWithoutNull("count", Count);
            jo.AddWithoutNull("timestamp", Timestamp);
            return jo;
        }
    }
}