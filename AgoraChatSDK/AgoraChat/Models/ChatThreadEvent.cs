using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    [Preserve]
    public class ChatThreadEvent : BaseModel
    {
        /**
        * \~chinese
        * 获取子区事件发送方。
        *
        * @return 子区事件发送方。
        *
        * \~english
        * Gets thread event sender.
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
        * Gets thread event operation.
        *
        * @return The thread event operation.
        */
        public ChatThreadOperation Operation { get; internal set; }

        /**
        * \~chinese
        * 获取子区事件中的子区内容。
        *
        * @return 子区事件中的子区内容。
        *
        * \~english
        * Gets the thread in thread event.
        *
        * @return The thread in thread event.
        */
        public ChatThread ChatThread { get; internal set; }

        [Preserve]
        internal ChatThreadEvent() { }

        [Preserve]
        internal ChatThreadEvent(string jsonString) : base(jsonString) { }

        [Preserve]
        internal ChatThreadEvent(SimpleJSON.JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            From = jsonObject["from"];
            Operation = jsonObject["type"].AsInt.ToChatThreadOperation();
            ChatThread = ModelHelper.CreateWithJsonObject<ChatThread>(jsonObject["thread"].AsObject);
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("from", From);
            jo.AddWithoutNull("type", Operation.ToInt());
            jo.AddWithoutNull("thread", ChatThread.ToJsonObject());

            return jo;
        }
    }
}