using System.Collections.Generic;

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

        public RoomPermissionType PermissionType { get; internal set; }



        public Room()
        {
        }

    }
}