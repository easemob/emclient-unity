using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 在线设备状态类
     *
     * \~english
     * The presence device status class
     */
    [Preserve]
    public class PresenceDeviceStatus : BaseModel
    {
        /**
        * \~chinese
        * 在线设备Id
        *
        * \~english
        * The presence device Id.
        */
        public string DeviceId;


        /**
         * \~chinese
         * 在线设备状态
         *
         * \~english
         * The presence device status
         */
        public int Status;

        [Preserve]
        internal PresenceDeviceStatus() { }

        [Preserve]
        internal PresenceDeviceStatus(string jsonString) : base(jsonString) { }

        [Preserve]
        internal PresenceDeviceStatus(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            DeviceId = jsonObject["device"];
            Status = jsonObject["status"];
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("device", DeviceId);
            jo.AddWithoutNull("status", Status);
            return jo;
        }
    }
}