using SimpleJSON;

namespace AgoraChat
{
    public class ChatThreadEvent
    {
        /**
        * \~chinese
        * 获取子区事件发送方。
        *
        * @return 子区事件发送方。
        *
        * \~english
        * Gets the thread event sender.
        *
        * @return The thread event sender.
        */
        public string From { get; internal set; }

        /**
        * \~chinese
        * 获取子区事件操作。
        *
        * @return 子区事件操作。
        *
        * \~english
        * Gets the operation that triggers the thread event.
        *
        * @return The operation that triggers the thread event.
        */
        public ChatThreadOperation Operation { get; internal set; }

        /**
        * \~chinese
        * 获取子区事件中的子区详情。
        *
        * @return 子区事件中的子区详情。
        *
        * \~english
        * Gets the thread details in the thread event.
        *
        * @return The thread details in the thread event.
        */
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
