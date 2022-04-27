using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public class PresenceDeviceStatus
    {
        public string DeviceId;
        public string Status;

        internal PresenceDeviceStatus()
        {
            // Default constructor, No need to add any code
        }
    }

    public class Presence
    {
        public string Publisher { get; internal set; }
        public List<PresenceDeviceStatus> StatusList { get; internal set; }
        public string Ext { get; internal set; }
        public long LatestTime { get; internal set; }
        public long ExpiryTime { get; internal set; }

        internal Presence()
        {
            // Default constructor, No need to add any code
        }

        internal Presence(string jsonString)
        {
            //TODO: Add code for using jsonString to create Presence object
        }

    }
}
