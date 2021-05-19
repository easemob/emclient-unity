using System.Collections.Generic;

namespace ChatSDK
{
    public class RoomManager_iOS : IRoomManager
    {
        public override void AddRoomAdmin(string roomId, string memberId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockRoomMembers(string roomId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeOwner(string roomId, string newOwner, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, ValueCallBack<Room> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeRoomSubject(string roomId, string newSubject, ValueCallBack<Room> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRoom(string subject, string descriptionsc, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DestroyRoom(string roomId, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> result = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> callBack)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> result = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<Room>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override List<Room> GetAllRoomsFromLocal()
        {
            throw new System.NotImplementedException();
        }

        public override Room GetChatRoomWithId(string roomId)
        {
            throw new System.NotImplementedException();
        }

        public override void JoinRoom(string roomId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void LeaveRoom(string roomId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteRoomMembers(string roomId, List<string> members, long duration = -1, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveRoomMembers(string roomId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnBlockRoomMembers(string roomId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteRoomMembers(string roomId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }
    }
}