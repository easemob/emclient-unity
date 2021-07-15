using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace ChatSDK
{
    public class RoomManager_Mac : IRoomManager
    {
        private IntPtr client;
        private RoomManagerHub roomManagerHub;

        //manager level events

        internal RoomManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
            roomManagerHub = new RoomManagerHub(Delegate);
            //registered listeners
            ChatAPINative.RoomManager_AddListener(client, roomManagerHub.OnChatRoomDestroyed, roomManagerHub.OnMemberJoined,
                roomManagerHub.OnMemberExited, roomManagerHub.OnRemovedFromChatRoom, roomManagerHub.OnMuteListAdded, roomManagerHub.OnMuteListRemoved,
                roomManagerHub.OnAdminAdded, roomManagerHub.OnAdminRemoved, roomManagerHub.OnOwnerChanged, roomManagerHub.OnAnnouncementChanged);
        }

        public override void AddRoomAdmin(string roomId, string memberId, ValueCallBack<Room> handle = null)
        {
            ChatAPINative.RoomManager_AddRoomAdmin(client, roomId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.OnSuccessValue(result.RoomInfo());
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.OnError);
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
            ChatAPINative.RoomManager_ChangeRoomSubject(client, roomId, newSubject,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.OnSuccessValue(result.RoomInfo());
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.OnError);
        }

        public override void CreateRoom(string subject, string description, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
        {
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (members != null && members.Count > 0)
            {
                size = members.Count;
                membersArray = new string[size];
                int i = 0;
                foreach (string member in members)
                {
                    membersArray[i] = member;
                    i++;
                }
            }
            ChatAPINative.RoomManager_CreateRoom(client, subject, description, welcomeMsg, maxUserCount, membersArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.OnSuccessValue(result.RoomInfo());
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.OnError);
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
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (members != null && members.Count > 0)
            {
                size = members.Count;
                membersArray = new string[size];
                int i = 0;
                foreach (string member in members)
                {
                    membersArray[i] = member;
                    i++;
                }
            }
            ChatAPINative.RoomManager_RemoveRoomMembers(client, roomId, membersArray, size,
                handle?.Success,
                handle?.Error);
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