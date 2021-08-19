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
            roomManagerHub = new RoomManagerHub(CallbackManager.Instance().roomManagerDelegates);
            //registered listeners
            ChatAPINative.RoomManager_AddListener(client, roomManagerHub.OnChatRoomDestroyed, roomManagerHub.OnMemberJoined,
                roomManagerHub.OnMemberExited, roomManagerHub.OnRemovedFromChatRoom, roomManagerHub.OnMuteListAdded, roomManagerHub.OnMuteListRemoved,
                roomManagerHub.OnAdminAdded, roomManagerHub.OnAdminRemoved, roomManagerHub.OnOwnerChanged, roomManagerHub.OnAnnouncementChanged);
        }

        public override void AddRoomAdmin(string roomId, string memberId, CallBack handle = null)
        {
            ChatAPINative.RoomManager_AddRoomAdmin(client, roomId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.Error);
        }

        public override void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
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
            ChatAPINative.RoomManager_BlockChatroomMembers(client, roomId, membersArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.Error);
        }

        public override void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null)
        {
            ChatAPINative.RoomManager_TransferChatroomOwner(client, roomId, newOwner,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.Error);
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null)
        {
            ChatAPINative.RoomManager_ChangeChatroomDescription(client, roomId, newDescription,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.Error);
        }

        public override void ChangeRoomName(string roomId, string newName, CallBack handle = null)
        {
            ChatAPINative.RoomManager_ChangeRoomSubject(client, roomId, newName,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                handle?.Error);
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
                handle?.Error);
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            ChatAPINative.RoomManager_DestroyChatroom(client, roomId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            if (pageSize > 200) pageSize = 200;
            ChatAPINative.RoomManager_FetchChatroomsWithPage(client, pageNum, pageSize,
                (IntPtr[] data, DataType dType, int size) => {
                    Debug.Log($"FetchPublicRoomsFromServer callback with dType={dType}, size={size}.");
                    if (dType == DataType.Room && size > 0)
                    {
                        var result = new PageResult<Room>();
                        result.Data = new List<Room>();
                        for(int i=0; i<size; i++)
                        {
                            var roomTO = Marshal.PtrToStructure<RoomTO>(data[i]);
                            result.Data.Add(roomTO.RoomInfo());
                        }
                        handle?.OnSuccessValue(result);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                (int code, string desc) => handle?.Error(code, desc));
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            ChatAPINative.RoomManager_FetchChatroomAnnouncement(client, roomId,
                (IntPtr[] data, DataType dType, int size) => {
                    Debug.Log($"FetchRoomAnnouncement callback with dType={dType}, size={size}.");
                    if (dType == DataType.String && size == 1)
                    {
                        string result = Marshal.PtrToStringAnsi(data[0]);
                        handle?.OnSuccessValue(result);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                (int code, string desc) => handle?.Error(code, desc));
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (pageSize > 200) pageSize = 200;
            ChatAPINative.RoomManager_FetchChatroomBans(client, roomId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    List<string> banList = new List<string>();
                    if (dType == DataType.String && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var muteItem = Marshal.PtrToStringAnsi(data[i]);
                            banList.Add(muteItem);
                        }

                        handle?.OnSuccessValue(banList);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null)
        {
            ChatAPINative.RoomManager_FetchChatroomSpecification(client, roomId,
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
                handle?.Error);
        }

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            if (pageSize > 200) pageSize = 200;
            ChatAPINative.RoomManager_FetchChatroomMembers(client, roomId, cursor, pageSize,
                (IntPtr[] cursorResult, DataType dType, int size) => {
                    Debug.Log($"FetchRoomMembers callback with dType={dType}, size={size}.");
                    if (dType == DataType.CursorResult && size == 1)
                    {
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTO>(cursorResult[0]);
                        if (cursorResultTO.Type == DataType.ListOfString)
                        {
                            var result = new CursorResult<String>();
                            result.Data = new List<string>();
                            result.Cursor = cursorResultTO.NextPageCursor;
                            int itemSize = cursorResultTO.Size;

                            for (int i = 0; i < itemSize; i++)
                            {
                                var item = Marshal.PtrToStringAnsi(cursorResultTO.Data[i]);
                                result.Data.Add(item);
                            }
                            handle?.OnSuccessValue(result);
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid return type from native GroupManager_FetchGroupMembers(), please check native c wrapper code.");
                        }

                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                (int code, string desc) => handle?.Error(code, desc));
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null)
        {
            if (pageSize > 200) pageSize = 200;
            ChatAPINative.RoomManager_FetchChatroomMutes(client, roomId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    List<string> muteList = new List<string>();
                    if (dType == DataType.String && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var muteItem = Marshal.PtrToStringAnsi(data[i]);
                            muteList.Add(muteItem);
                        }

                        handle?.OnSuccessValue(muteList);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }


        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
        {
            ChatAPINative.RoomManager_JoinChatroom(client, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room)
                    {
                        if(0 == dSize)
                        {
                            Debug.Log("No room information returned.");
                            handle?.OnSuccessValue(null);
                            return;
                        }
                        
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.OnSuccessValue(result.RoomInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            ChatAPINative.RoomManager_LeaveChatroom(client, roomId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
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

            int muteDuration = 1000; // no this parameter for API?

            ChatAPINative.RoomManager_MuteChatroomMembers(client, roomId, membersArray, size, muteDuration,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null)
        {
            ChatAPINative.RoomManager_RemoveChatroomAdmin(client, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Room)
                    {
                        if (0 == dSize)
                        {
                            Debug.Log("No room information returned.");
                            handle?.Success();
                            return;
                        }

                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
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

            ChatAPINative.RoomManager_UnblockChatroomMembers(client, roomId, membersArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
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

            ChatAPINative.RoomManager_UnmuteChatroomMembers(client, roomId, membersArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            ChatAPINative.RoomManager_UpdateChatroomAnnouncement(client, roomId, announcement, 
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }
    }
}