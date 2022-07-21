using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{

    /**
     *  \~chinese
     *  子区对象类。
     *
     *  \~english
     *  The message thread object class.
     */

    public class ThreadEvent
    {
        /**
         * \~chinese
         * 获取子区 ID。
         *
         * @return 子区 ID。
         *
         * \~english
         * Gets the message thread ID.
         *
         * @return The message thread ID.
         */
        public string Tid;

        /**
         * \~chinese
         * 获取父消息 ID。
         *
         * @return 父消息 ID。
         *
         * \~english
         * Gets the ID of the parent message.
         *
         * @return The ID of the parent message.
         */
        public string MessageId;

        /**
         * \~chinese
         * 获取子区所在的群组 ID。
         *
         * @return  群组 ID。
         *
         * \~english
         * Gets the group ID to which the message thread belongs.
         *
         * @return The group ID.
         */
        public string ParentId;

        /**
         * \~chinese
         * 获取子区创建者。
         *
         * 获取子区详情和子区列表的时候返回此属性值。
         *
         * @return 子区创建者的用户 ID。
         *
         * \~english
         * Gets the message thread creator.
         *
         * This message thread creator is returned when you get the message thread details and message thread list.
         *
         * @return The user ID of the message thread creator.
         */

        public string Owner;

        /**
         * \~chinese
         * 获取子区名称。
         *
         * @return 子区名称。
         *
         * \~english
         * Gets the message thread name.
         *
         * @return The message thread name.
         */
        public string Name;

        /**
        * \~chinese
        * 获取子区发送方。
        *
        * @return 子区发送方。
        *
        * \~english
        * Gets thread sender.
        *
        * @return The thread sender.
        */
        public string From;

        /**
        * \~chinese
        * 获取子区接收方。
        *
        * @return 子区接收方。
        *
        * \~english
        * Gets thread sender.
        *
        * @return The thread sender.
        */
        public string To;

        /**
        * \~chinese
        * 获取子区操作内容。
        *
        * @return 子区操作内容。
        *
        * \~english
        * Gets thread operation.
        *
        * @return The thread operation.
        */
        public string Operation;

        /**
         * \~chinese
         * 获取子区的消息数。
         *
         * 获取子区的消息数之前，需先调用 {@link IThreadManager#GetThreadDetail} 方法获取子区详情。
         *
         * @return 消息数。
         *
         * \~english
         * Get the number of messages in a message thread.
         *
         * To get the number of messages in a message thread, you need to first call {@link IThreadManager#GetThreadDetail} to get details of the message thread.
         *
         * @return The message count.
         */
        public int MessageCount;

        /**
         * \~chinese
         * 获取子区成员数量。
         *
         * 获取子区成员数量之前，需先调用 {@link IThreadManager#GetThreadDetail} 方法获取子区详情。
         *
         * @return 子区成员数量。
         *
         * \~english
         * Gets the number of members in the message thread.
         *
         * To get the member count, you need to first call {@link IThreadManager#GetThreadDetail} to get details of the message thread.
         *
         * @return  The number of members in the message thread.
         */
        public int MembersCount;

        /**
         * \~chinese
         * 获取子区创建的 Unix 时间戳，单位为毫秒。
         *
         * @return  子区创建的 Unix 时间戳。
         *
         * \~english
         * Gets the Unix timestamp when the message thread is created. The unit is millisecond.
         *
         * @return The Unix timestamp when the message thread is created.
         */
        public long CreateTimestamp;

        /**
         * \~chinese
         * 获取子区更新的 Unix 时间戳，单位为毫秒。
         *
         * @return  子区更新的 Unix 时间戳。
         *
         * \~english
         * Gets the Unix timestamp when the message thread is updated. The unit is millisecond.
         *
         * @return The Unix timestamp when the message thread is updated.
         */
        public long UpdateTimestamp;

        /**
         * \~chinese
         * 获取子区事件的 Unix 时间戳，单位为毫秒。
         *
         * @return  子区事件的 Unix 时间戳。
         *
         * \~english
         * Gets the Unix timestamp of event on the message thread. The unit is millisecond.
         *
         * @return The Unix timestamp of event on thread.
         */
        public long Timestamp;

        /**
         * \~chinese
         * 获取最近的子区消息。
         *
         * 获取该属性的值之前，需先调用 {@link IThreadManager#GetThreadDetail} 方法获取子区详情。
         *
         * @return 最近的子区消息。
         *
         * \~english
         * Get the last reply in the message thread.
         *
         * To get the last reply in the message thread, you need to first call {@link IThreadManager#GetThreadDetail} to get details of the message thread.
         *
         * @return The last reply in the message thread.
         */
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
