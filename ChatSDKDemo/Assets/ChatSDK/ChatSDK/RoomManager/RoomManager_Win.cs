using System.Collections.Generic;

namespace ChatSDK
{
    public class RoomManager_Win : IRoomManager
    {
        public override void AddRoomAdmin(string roomId, string memberId, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockRoomMembers(string roomId, List<string> members, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeOwner(string roomId, string newOwner, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomSubject(string roomId, string newSubject, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRoom(string subject, string descriptionsc, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomMembers(string roomId, string cursor = null, int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetAllRoomsFromLocal(ValueCallBack<List<Room>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetChatRoomWithId(string roomId, ValueCallBack<Room> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void JoinRoom(string roomId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }
    }
}