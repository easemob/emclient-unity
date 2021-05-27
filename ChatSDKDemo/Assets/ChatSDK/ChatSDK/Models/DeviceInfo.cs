namespace ChatSDK
{

    public class DeviceInfo

    {
        public string Resource { get; private set; }
        public string DeviceUUID { get; private set; }
        public string DeviceName { get; private set; }

        internal DeviceInfo()
        {
        }

        public DeviceInfo FromJsonString(string jsonString)
        {
            throw new System.NotImplementedException();
        }
    }

}