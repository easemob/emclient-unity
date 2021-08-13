using System;
using SimpleJSON;

namespace ChatSDK {

    /// <summary>
    /// 公开群列表返回的群信息
    /// </summary>
    public class GroupInfo
    {
        /// <summary>
        /// 群id
        /// </summary>
        public string GroupId { get; internal set; }

        /// <summary>
        /// 群名称
        /// </summary>
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

        internal GroupInfo()
        {

        }

    }

}

