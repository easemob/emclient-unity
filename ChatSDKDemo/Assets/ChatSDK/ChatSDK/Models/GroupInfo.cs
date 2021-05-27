using System;
using SimpleJSON;

namespace ChatSDK {
    public class GroupInfo
    {

        public string GroupId { get; internal set; }
        public string GroupName { get; internal set; }

        internal GroupInfo(JSONNode jo) {
            if (jo != null) {
                GroupId = jo["gruopId"].Value;
                GroupName = jo["groupName"].Value;
            }
        }
    }

}

