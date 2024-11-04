using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 多设备登录信息类。
     *
     * \~english
     * The multi-device information class.
     */
    [Preserve]
    public class DeviceInfo : BaseModel

    {
        /**
         * \~chinese
         * 其他登录的设备信息。
         * 
         * 可以通过设备信息区分设备类型。
         *
         * \~english
         * The information of other login devices.
         * 
         * With the device information, you can distinguish different types of devices.
         */
        public string Resource { get; private set; }

        /**
         * \~chinese
         * 设备的 UUID（唯一标识码）。
         *
         * \~english
         * The UUID of the device.
         * 
         */
        public string DeviceUUID { get; private set; }

        /**
         * \~chinese
         * 设备名称。
         *
         * \~english
         * The device name.
         */
        public string DeviceName { get; private set; }

        [Preserve]
        internal DeviceInfo() { }

        [Preserve]
        internal DeviceInfo(string jsonString) : base(jsonString) { }

        [Preserve]
        internal DeviceInfo(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            Resource = jsonObject["resource"];
            DeviceUUID = jsonObject["deviceUUID"];
            DeviceName = jsonObject["deviceName"];
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("resource", Resource);
            jo.AddWithoutNull("deviceUUID", DeviceUUID);
            jo.AddWithoutNull("deviceName", DeviceName);
            return jo;
        }
    }
}