using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 联系人信息。
     *
     * \~english
     * The contactor information class.
     */
    [Preserve]
    public class Contact : BaseModel

    {
        /**
         * \~chinese
         * 联系人ID。
         *
         * \~english
         * The contactor ID.
         *
         */
        public string UserId { get; private set; }

        /**
         * \~chinese
         * 联系人备注。
         *
         * \~english
         * The remark or contactor.
         * 
         */
        public string Remark { get; private set; }

        [Preserve]
        internal Contact() { }

        [Preserve]
        internal Contact(string jsonString) : base(jsonString) { }

        [Preserve]
        internal Contact(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            UserId = jsonObject["userId"];
            Remark = jsonObject["remark"];
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("userId", UserId);
            jo.AddWithoutNull("remark", Remark);
            return jo;
        }
    }
}