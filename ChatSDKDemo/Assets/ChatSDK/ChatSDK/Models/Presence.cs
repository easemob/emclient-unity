using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public class PresenceDeviceStatus
    {
        public string DeviceId;
        public int Status;

        internal PresenceDeviceStatus()
        {
           
        }

        internal PresenceDeviceStatus(JSONObject json) {
            foreach (string key in json.Keys) {
                DeviceId = key;
                Status = json[key].AsInt;
            }
        }
    }

    public class Presence
    {
        public string Publisher { get; internal set; }
        public List<PresenceDeviceStatus> StatusList { get; internal set; }
        public string statusDescription { get; internal set; }
        public long LatestTime { get; internal set; }
        public long ExpiryTime { get; internal set; }

        internal Presence()
        {
            // Default constructor, No need to add any code
        }

        internal Presence(JSONObject json)
        {
            Publisher = json["publisher"].Value;
            statusDescription = json["statusDescription"].Value;
            LatestTime = json["lastTime"].AsInt;
            ExpiryTime = json["expiryTime"].AsInt;
            StatusList = new List<PresenceDeviceStatus>();
            if (json["statusDetails"].IsArray) {
                JSONArray ary = json["statusDetails"].AsArray;
                foreach (JSONObject jo in ary) {
                    StatusList.Add(new PresenceDeviceStatus(jo));
                }
            }
        }

    }
}
