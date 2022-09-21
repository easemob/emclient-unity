using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class RoomManager_iOS : IRoomManager
    {
        public RoomManager_iOS()
        {
        }

        public override void AddRoomAdmin(string roomId, string memberId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("admin", memberId);
            ChatAPIIOS.RoomManager_HandleMethodCall("addChatRoomAdmin", obj.ToString(), handle?.callbackId);
        }

        public override void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("blockChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("newOwner", newOwner);
            ChatAPIIOS.RoomManager_HandleMethodCall("changeChatRoomOwner", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("desc", newDescription);
            ChatAPIIOS.RoomManager_HandleMethodCall("changeChatRoomDescription", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeRoomName(string roomId, string newName, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("subject", newName);
            ChatAPIIOS.RoomManager_HandleMethodCall("changeChatRoomSubject", obj.ToString(), handle?.callbackId);
        }

        public override void CreateRoom(string subject, string descriptions, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("subject", subject ?? "");
            obj.Add("desc", descriptions ?? "");
            obj.Add("maxUserCount", maxUserCount);
            obj.Add("welcomeMsg", welcomeMsg ?? "");
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("createChatroom", obj.ToString(), handle?.callbackId);
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("destroyChatRoom", obj.ToString(), handle?.callbackId);
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.RoomManager_HandleMethodCall("fetchPublicChatRoomsFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("fetchChatRoomAnnouncement", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.RoomManager_HandleMethodCall("fetchChatRoomBlockList", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("fetchChatRoomInfoFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("cursor", cursor);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.RoomManager_HandleMethodCall("fetchChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.RoomManager_HandleMethodCall("fetchChatRoomMuteList", obj.ToString(), handle?.callbackId);
        }

        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("joinChatRoom", obj.ToString(), handle?.callbackId);
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("leaveChatRoom", obj.ToString(), handle?.callbackId);
        }

        public override void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("muteChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("admin", adminId);
            ChatAPIIOS.RoomManager_HandleMethodCall("removeChatRoomAdmin", obj.ToString(), handle?.callbackId);
        }

        public override void DeleteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("removeChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("unBlockChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("unMuteChatRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("announcement", announcement);
            ChatAPIIOS.RoomManager_HandleMethodCall("updateChatRoomAnnouncement", obj.ToString(), handle?.callbackId);
        }

        public override void MuteAllRoomMembers(string roomId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("muteAllRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnMuteAllRoomMembers(string roomId, ValueCallBack<Room> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            ChatAPIIOS.RoomManager_HandleMethodCall("unMuteAllRoomMembers", obj.ToString(), handle?.callbackId);
        }

        public override void AddWhiteListMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("addWhiteListMembers", obj.ToString(), handle?.callbackId);
        }

        public override void RemoveWhiteListMembers(string roomId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("roomId", roomId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.RoomManager_HandleMethodCall("removeWhiteListMembers", obj.ToString(), handle?.callbackId);
        }

        public override void AddAttributes(string roomId, Dictionary<string, string> kv, bool forced, CallBackResult handle = null)
        {
            //TODO: add code here.
        }

        public override void FetchAttributes(string roomId, List<string> keys, ValueCallBack<Dictionary<string, string>> handle = null)
        {
            //TODO: add code here.
        }

        public override void RemoveAttributes(string roomId, List<string> keys, bool forced, CallBackResult handle = null)
        {
            //TODO: add code here.
        }
    }
}