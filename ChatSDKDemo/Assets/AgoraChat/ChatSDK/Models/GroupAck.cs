﻿using System;
using SimpleJSON;

namespace AgoraChat {

    /**
     * \~chinese
     * 群组消息的已读回执类。
     * 
     * \~english
     * The class for read receipts of group messages.
     * 
     */
    public class GroupReadAck
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

        internal GroupReadAck()
        {

        }
    }
}

