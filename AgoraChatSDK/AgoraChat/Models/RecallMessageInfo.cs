using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 消息撤回详情类。
     *
     * \~english
     * The message recall information object.
     */
    [Preserve]
    public class RecallMessageInfo : BaseModel

    {
        /**
         * \~chinese
         * 消息撤回者。
         *
         * \~english
         * The user that recalls the message.
         * 
         */
        public string RecallBy { get; private set; }

        /**
         * \~chinese
         * 被撤回的消息的消息 ID。
         *
         * \~english
         * The ID of the recalled message.
         * 
         */
        public string RecallMessageId { get; private set; }

        /**
         * \~chinese
         * 撤回消息时要透传的信息。
         *
         * \~english
         * The information to be transparently transmitted during message recall.
         */
        public string Ext { get; private set; }

        /**
         * \~chinese
         * 撤回的消息
         * 离线撤回会为空。
         *
         * \~english
         * The recalled message.
         * The value of this property is nil if the recipient is offline during message recall.
         */
        public Message RecallMessage;

        [Preserve]
        internal RecallMessageInfo() { }

        [Preserve]
        internal RecallMessageInfo(string jsonString) : base(jsonString) { }

        [Preserve]
        internal RecallMessageInfo(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            RecallBy = jsonObject["recallBy"];
            RecallMessageId = jsonObject["recallMessageId"];
            Ext = jsonObject["ext"];

            if (null != jsonObject["recallMessage"] && jsonObject["recallMessage"].IsObject)
            {
                RecallMessage = new Message(jsonObject["recallMessage"].AsObject);
            }
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("recallBy", RecallBy);
            jo.AddWithoutNull("recallMessageId", RecallMessageId);
            jo.AddWithoutNull("ext", Ext);
            if (null != RecallMessage)
            {
                jo.AddWithoutNull("recallMessage", RecallMessage.ToJsonObject());
            }
            return jo;
        }
    }
}