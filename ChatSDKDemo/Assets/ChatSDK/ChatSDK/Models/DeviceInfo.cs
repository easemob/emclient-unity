using SimpleJSON;

namespace ChatSDK
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo

    {
        /// <summary>
        /// 设备Resource
        /// </summary>
        public string Resource { get; private set; }

        /// <summary>
        /// 设备UUId
        /// </summary>
        public string DeviceUUID { get; private set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; private set; }


        internal DeviceInfo(string jsonString)
        {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    Resource = jo["resource"];
                    DeviceUUID = jo["deviceUUID"];
                    DeviceName = jo["deviceName"];
                }
            }
        }
    }
}