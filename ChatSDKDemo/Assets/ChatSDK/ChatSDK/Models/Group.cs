using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    public class Group
    {

        internal Group(JSONNode jn)
        {
            if (!jn.IsNull && jn.IsObject)
            {
                JSONObject jo = jn.AsObject;
                this.GroupId = jo["groupId"].Value;
                this.Name = jo["name"].Value;
                this.Description = jo["desc"].Value;
                this.Owner = jo["owner"].Value;
                this.Annoumcement = jo["announcement"].Value;
                this.MemberCount = jo["memberCount"].AsInt;
                this.MemberList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                this.AdminList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                this.BlockList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                this.MuteList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                this.NoticeEnabled = jo["noticeEnable"].AsBool;
                this.MessageBlocked = jo["messageBlocked"].AsBool;
                this.IsAllMemberMuted = jo["isAllMemberMuted"].AsBool;
                JSONObject options = jo["options"].AsObject;
                if (options != null)
                {
                    this.Options = new GroupOptions(options);
                }

                if (jo["permissionType"].AsInt == -1)
                {
                    this.PermissionType = GroupPermissionType.None;
                }
                else if (jo["permissionType"].AsInt == 0)
                {
                    this.PermissionType = GroupPermissionType.Member;
                }
                else if (jo["permissionType"].AsInt == 1)
                {
                    this.PermissionType = GroupPermissionType.Admin;
                }
                else if (jo["permissionType"].AsInt == 2)
                {
                    this.PermissionType = GroupPermissionType.Owner;
                }
            }
        }

        public string GroupId { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string Owner { get; internal set; }
        public string Annoumcement { get; internal set; }
        public int MemberCount { get; internal set; }
        public List<string> MemberList { get; internal set; }
        public List<string> AdminList { get; internal set; }
        public List<string> BlockList { get; internal set; }
        public List<string> MuteList { get; internal set; }
        public bool NoticeEnabled { get; internal set; }
        public bool MessageBlocked { get; internal set; }
        public bool IsAllMemberMuted { get; internal set; }
        public GroupOptions Options { get; internal set; }
        public GroupPermissionType PermissionType { get; internal set; }

    }
}