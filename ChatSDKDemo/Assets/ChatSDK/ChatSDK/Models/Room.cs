using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public class Room
    {

        public string RoomId { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string Announcement { get; internal set; }

        public List<string> AdminList { get; internal set; }
        public List<string> MemberList { get; internal set; }
        public List<string> BlockList { get; internal set; }
        public List<string> MuteList { get; internal set; }

        public bool AllMemberMuted { get; internal set; }

        public int MaxUsers { get; internal set; }
        public string Owner { get; internal set; }
        public bool IsAllMemberMuted { get; internal set; }

        public RoomPermissionType PermissionType { get; internal set; }



        internal Room(string jsonString)
        {
            JSONNode jn = JSON.Parse(jsonString);
            if (!jn.IsNull && jn.IsObject) {
                JSONObject jo = jn.AsObject;
                RoomId = jo["roomId"].Value;
                Name = jo["name"].Value;
                Description = jo["desc"].Value;
                Announcement = jo["announcement"].Value;
                AdminList = TransformTool.JsonStringToStringList(jo["adminList"].Value);
                MemberList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                BlockList = TransformTool.JsonStringToStringList(jo["blockList"].Value);
                MuteList = TransformTool.JsonStringToStringList(jo["muteList"].Value);
                MaxUsers = jo["maxUsers"].AsInt;
                Owner = jo["owner"].Value;
                IsAllMemberMuted = jo["isAllMemberMuted"].AsBool;
                if (jo["permissionType"].AsInt == -1)
                {
                    PermissionType = RoomPermissionType.None;
                }
                else if (jo["permissionType"].AsInt == 0)
                {
                    PermissionType = RoomPermissionType.Member;
                }
                else if (jo["permissionType"].AsInt == 1)
                {
                    PermissionType = RoomPermissionType.Admin;
                }
                else if (jo["permissionType"].AsInt == 2)
                {
                    PermissionType = RoomPermissionType.Owner;
                }
            }
        }

    }
}