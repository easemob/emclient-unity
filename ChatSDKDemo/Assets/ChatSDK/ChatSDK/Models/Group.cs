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
        internal EMGroupPermissionType _PermissionType;

        public Group()
        {

        }
    }
}