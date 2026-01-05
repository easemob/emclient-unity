using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 群组成员信息。
     *
     * \~english
     * The group member information.
     */
    [Preserve]
    public class GroupMemberInfo : BaseModel
    {
        /**
         * \~chinese
         * 群成员的用户 ID。
         *
         * \~english
         * The user ID of the group member.
         */
        public string MemberId;

        /**
         * \~chinese
         * 群成员的加群时间。Unix 时间戳，单位为毫秒。
         *
         * \~english
         * The time when the group member joined the group. Unix timestamp in milliseconds.
         */
        public long JoinedTimestamp;

        /**
         * \~chinese
         * 群成员的角色。
         *
         * \~english
         * The role of the group member.
         */
        public GroupPermissionType Role;

        [Preserve]
        public GroupMemberInfo()
        {
            MemberId = "";
            JoinedTimestamp = 0;
            Role = GroupPermissionType.None;
        }

        [Preserve]
        internal GroupMemberInfo(string jsonString) : base(jsonString) { }

        [Preserve]
        internal GroupMemberInfo(JSONObject jsonObject) : base(jsonObject) { }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jsonObject = new JSONObject();
            jsonObject.AddWithoutNull("memberId", MemberId);
            jsonObject.AddWithoutNull("joinedTimestamp", JoinedTimestamp);
            jsonObject.AddWithoutNull("role", (int)Role);
            return jsonObject;
        }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            if (!jsonObject.IsNull)
            {
                if (!jsonObject["memberId"].IsNull)
                {
                    MemberId = jsonObject["memberId"];
                }

                if (!jsonObject["joinedTimestamp"].IsNull)
                {
                    JoinedTimestamp = (long)jsonObject["joinedTimestamp"].AsDouble;
                }

                if (!jsonObject["role"].IsNull)
                {
                    Role = (GroupPermissionType)jsonObject["role"].AsInt;
                }
            }
        }
    }
}

