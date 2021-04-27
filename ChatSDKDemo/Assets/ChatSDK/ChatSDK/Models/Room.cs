using System.Collections.Generic;

namespace ChatSDK
{
    public class Room
    {

        internal string _RoomId, _Name, _Description, _Announcement;
        internal int _MemberCount, _MaxUsers;
        internal List<string> _AdminList, _MemberList, _BlockList, _MuteList;
        internal bool _AllMemberMuted;

        internal RoomPermissionType _PermissionType;

        public string RoomId { get { return _RoomId; } }
        public string Name { get { return _Name; } }
        public string Description { get { return _Description; } }
        public string Announcement { get { return _Announcement; } }

        public List<string> AdminList { get { return _AdminList; } }
        public List<string> MemberList { get { return _MemberList; } }
        public List<string> BlockList { get { return _BlockList; } }
        public List<string> MuteList { get { return _MuteList; } }

        public bool AllMemberMuted { get { return _AllMemberMuted; } }

        public RoomPermissionType PermissionType { get { return _PermissionType; } }



        public Room()
        {
        }

    }
}