using SimpleJSON;

namespace ChatSDK
{

    public class DeviceInfo

    {
        public string Resource { get; private set; }
        public string DeviceUUID { get; private set; }
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