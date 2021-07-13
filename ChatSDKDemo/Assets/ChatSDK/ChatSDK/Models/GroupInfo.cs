using System;
using SimpleJSON;

namespace ChatSDK {
    public class GroupInfo
    {

        public string GroupId { get; internal set; }
        public string GroupName { get; internal set; }

        internal GroupInfo(string jsonString) {
            if (jsonString != null) {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull)
                {
                    GroupId = jn["groupId"].Value;
                    GroupName = jn["groupName"].Value;
                }
            }
        }
    }

}

