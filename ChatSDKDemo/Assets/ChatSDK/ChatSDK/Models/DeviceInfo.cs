namespace ChatSDK
{

    public class DeviceInfo

    {
        internal string _Resource;
        internal string _DeviceUUID;
        internal string _DeviceName;


        public string Resource { get { return _Resource; } }
        public string DeviceUUID { get { return _DeviceUUID; } }
        public string DeviceName { get { return _DeviceName; } }

        public DeviceInfo() {
        }
    }

}