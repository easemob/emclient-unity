using System.Collections.Generic;

namespace ChatSDK
{
    public class Group
    {

        internal Group()
        {

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