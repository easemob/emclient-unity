using System.Collections.Generic;

namespace ChatSDK
{
    internal sealed class RoomManager_Win : IRoomManager
    {
        public override void AddRoomAdmin(string roomId, string memberId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomName(string roomId, string newName, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRoom(string subject, string descriptions = null, string welcomeMsg = null, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
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

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
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