using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class RoomManager_Android : IRoomManager
    {

        private AndroidJavaObject wrapper;

        public RoomManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMChatRoomManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override void AddRoomAdmin(string roomId, string memberId, CallBack handle = null)
        {
            wrapper.Call("addChatRoomAdmin", roomId, memberId, handle?.callbackId);
        }

        public override void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("blockChatRoomMembers", roomId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null)
        {
            wrapper.Call("changeChatRoomOwner", roomId, newOwner, handle?.callbackId);
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null)
        {
            wrapper.Call("changeChatRoomDescription", roomId, newDescription, handle?.callbackId);
        }

        public override void ChangeRoomName(string roomId, string newName, CallBack handle = null)
        {
            wrapper.Call("changeChatRoomSubject", roomId, newName, handle?.callbackId);
        }

        public override void CreateRoom(string subject, string descriptions, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
        {
            wrapper.Call("createChatRoom", subject, descriptions, welcomeMsg, maxUserCount, TransformTool.JsonStringFromStringList(members),handle?.callbackId);
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            wrapper.Call("destroyChatRoom", roomId, handle?.callbackId);
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            wrapper.Call("fetchPublicChatRoomsFromServer", pageNum, pageSize, handle?.callbackId);
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            wrapper.Call("fetchChatRoomAnnouncement", roomId, handle?.callbackId);
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("fetchChatRoomBlockList", roomId, pageNum, pageSize, handle?.callbackId);
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null)
        {
            wrapper.Call("fetchChatRoomInfoFromServer", roomId, handle?.callbackId);
        }

        public override void FetchRoomMembers(string roomId, string cursor = null, int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            wrapper.Call("fetchChatRoomMembers", roomId, cursor, pageSize, handle?.callbackId);
        }

        public override void FetchRoomMuteList(string roomId, int pageSize, int pageNum, ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("fetchChatRoomMuteList", roomId, pageNum, pageSize, handle?.callbackId);
        }

        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
        {
            wrapper.Call("joinChatRoom", roomId, handle?.callbackId);
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            wrapper.Call("leaveChatRoom", roomId, handle?.callbackId);
        }

        public override void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("muteChatRoomMembers", roomId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null)
        {
            wrapper.Call("removeChatRoomAdmin", roomId, adminId, handle?.callbackId);
        }

        public override void RemoveRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("removeChatRoomMembers", roomId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("unBlockChatRoomMembers", roomId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("unMuteChatRoomMembers", roomId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            wrapper.Call("updateChatRoomAnnouncement", roomId, announcement, handle?.callbackId);
        }
    }
}