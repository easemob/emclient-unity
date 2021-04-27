using System.Collections.Generic;

namespace ChatSDK
{
    public class Group
    {

        internal string _GroupId, _Name, _Description, _Owner, _Annoumcement;
        internal int _memberCount;
        internal List<string> _MemberList, _AdminList, _BlockList, _MuteList;
        internal List<GroupSharedFile> _SharedFileList;
        internal bool _NoticeEnabled, _MessageBlocked, _IsAllMemberMuted;

        internal GroupOptions _Options;
        internal GroupPermissionType _PermissionType;


        public string GroupId { get { return _GroupId; } }
        public string Name { get { return _Name; } }
        public string Description { get { return _Description; } }
        public string Owner { get { return _Owner; } }
        public string Annoumcement { get { return _Annoumcement; } }
        public int MemberCount { get { return _memberCount; } }
        public List<string> MemberList { get { return _MemberList; } }
        public List<string> AdminList { get { return _AdminList; } }
        public List<string> BlockList { get { return _BlockList; } }
        public List<string> MuteList { get { return _MuteList; } }
        public bool NoticeEnabled { get { return _NoticeEnabled; } }
        public bool MessageBlocked { get { return _MessageBlocked; } }
        public bool IsAllMemberMuted { get { return _IsAllMemberMuted; } }
        public GroupOptions Options { get { return _Options; } }
        public GroupPermissionType PermissionType { get { return _PermissionType; } }


        public Group()
        {

        }
    }
}