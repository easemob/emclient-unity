using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK
{
    public class RoomManager_iOS : IRoomManager
    {
        GameObject listenerGameObj;

        public RoomManager_iOS()
        {
        }

        public override void AddRoomAdmin(string roomId, string memberId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("admin", memberId);
            RoomManagerNative.RoomManager_HandleMethodCall("addChatRoomAdmin", obj.ToString(), handle?.callbackId);
        }

        public override void BlockRoomMembers(string roomId, List<string> members, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            RoomManagerNative.RoomManager_HandleMethodCall("blockChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeOwner(string roomId, string newOwner, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("newOwner", newOwner);
            RoomManagerNative.RoomManager_HandleMethodCall("changeChatRoomOwner", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("desc", newDescription);
            RoomManagerNative.RoomManager_HandleMethodCall("changeChatRoomDescription", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeRoomName(string roomId, string newName, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("subject", newName);
            RoomManagerNative.RoomManager_HandleMethodCall("changeChatRoomSubject", obj.ToString(), handle?.callbackId);
        }

        public override void CreateRoom(string subject, string descriptions, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("subject", subject ?? "");
            obj.Add("desc", descriptions ?? "");
            obj.Add("maxUserCount", maxUserCount);
            obj.Add("welcomeMsg", welcomeMsg ?? "");
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            RoomManagerNative.RoomManager_HandleMethodCall("createChatroom", obj.ToString(), handle?.callbackId);
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            RoomManagerNative.RoomManager_HandleMethodCall("destroyChatRoom", obj.ToString(), handle?.callbackId);
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            RoomManagerNative.RoomManager_HandleMethodCall("fetchPublicChatRoomsFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            RoomManagerNative.RoomManager_HandleMethodCall("fetchChatRoomAnnouncement", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            RoomManagerNative.RoomManager_HandleMethodCall("fetchChatRoomBlockList", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            RoomManagerNative.RoomManager_HandleMethodCall("fetchChatRoomInfoFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("cursor", cursor);
            obj.Add("pageSize", pageSize);
            RoomManagerNative.RoomManager_HandleMethodCall("fetchChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            RoomManagerNative.RoomManager_HandleMethodCall("fetchChatRoomMuteList", obj.ToString(), handle?.callbackId);
        }

        public override void GetAllRoomsFromLocal(ValueCallBack<List<Room>> handle = null)
        {
            JSONObject obj = new JSONObject();
            RoomManagerNative.RoomManager_HandleMethodCall("getAllChatRooms", obj.ToString(), handle?.callbackId);
        }

        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            RoomManagerNative.RoomManager_HandleMethodCall("joinChatRoom", obj.ToString(), handle?.callbackId);
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            RoomManagerNative.RoomManager_HandleMethodCall("leaveChatRoom", obj.ToString(), handle?.callbackId);
        }

        public override void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            RoomManagerNative.RoomManager_HandleMethodCall("muteChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("admin", adminId);
            RoomManagerNative.RoomManager_HandleMethodCall("removeChatRoomAdmin", obj.ToString(), handle?.callbackId);
        }

        public override void RemoveRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            RoomManagerNative.RoomManager_HandleMethodCall("removeChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            RoomManagerNative.RoomManager_HandleMethodCall("unBlockChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            RoomManagerNative.RoomManager_HandleMethodCall("unMuteChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("announcement", announcement);
            RoomManagerNative.RoomManager_HandleMethodCall("updateChatRoomAnnouncement", obj.ToString(), handle?.callbackId);
        }
    }
    

    class RoomManagerNative
    {
        [DllImport("__Internal")]
        internal extern static void RoomManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string RoomManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}