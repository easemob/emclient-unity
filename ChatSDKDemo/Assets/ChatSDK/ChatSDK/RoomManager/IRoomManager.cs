using System.Collections.Generic;

namespace ChatSDK
{

    public abstract class IRoomManager {
        internal WeakDelegater<IRoomManagerDelegate> Delegate = new WeakDelegater<IRoomManagerDelegate>();

        public abstract void AddRoomAdmin(string roomId, string memberId, ValueCallBack<Room> handle = null);

        public abstract void BlockRoomMembers(string roomId, List<string> members, ValueCallBack<Room> handle = null);

        public abstract void ChangeOwner(string roomId, string newOwner, ValueCallBack<Room> handle = null);

        public abstract void ChangeRoomDescription(string roomId, string newDescription, ValueCallBack<Room> handle = null);

        public abstract void ChangeRoomSubject(string roomId, string newSubject, ValueCallBack<Room> handle = null);

        public abstract void CreateRoom(string subject, string descriptionsc, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null);

        public abstract void DestroyRoom(string roomId, CallBack handle = null);

        public abstract void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null);

        public abstract void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null);

        public abstract void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

        public abstract void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null);

        public abstract void FetchRoomMembers(string roomId, string cursor = null, int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null);

        public abstract void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null);

        public abstract void GetAllRoomsFromLocal(ValueCallBack<List<Room>> handle = null);

        public abstract void GetChatRoomWithId(string roomId, ValueCallBack<Room> handle = null);

        public abstract void JoinRoom(string roomId, CallBack handle = null);

        public abstract void LeaveRoom(string roomId, CallBack handle = null);

        public abstract void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null);

        public abstract void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null);

        public abstract void RemoveRoomMembers(string roomId, List<string> members, CallBack handle = null);

        public abstract void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null);

        public abstract void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null);

        public abstract void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null);

        public void AddRoomManagerDelegate(IRoomManagerDelegate roomManagerDelegate)

        {
            Delegate.Add(roomManagerDelegate);
        }


        internal void ClearDelegates()
        {
            Delegate.Clear();
        }




    }


}