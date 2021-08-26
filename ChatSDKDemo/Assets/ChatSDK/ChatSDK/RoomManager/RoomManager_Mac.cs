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
            roomManagerHub = new RoomManagerHub();
            //registered listeners
            ChatAPINative.RoomManager_AddListener(client, roomManagerHub.OnChatRoomDestroyed, roomManagerHub.OnMemberJoined,
                roomManagerHub.OnMemberExited, roomManagerHub.OnRemovedFromChatRoom, roomManagerHub.OnMuteListAdded, roomManagerHub.OnMuteListRemoved,
                roomManagerHub.OnAdminAdded, roomManagerHub.OnAdminRemoved, roomManagerHub.OnOwnerChanged, roomManagerHub.OnAnnouncementChanged);
        }

        public override void AddRoomAdmin(string roomId, string memberId, CallBack handle = null)
        {
            if (null == roomId || null == memberId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_AddRoomAdmin(client, roomId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            if (null == roomId || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (null != members && members.Count > 0)
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
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null)
        {
            if (null == roomId || null == newOwner)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_TransferChatroomOwner(client, roomId, newOwner,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null)
        {
            if (null == roomId || null == newDescription)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_ChangeChatroomDescription(client, roomId, newDescription,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void ChangeRoomName(string roomId, string newName, CallBack handle = null)
        {
            if (null == roomId || null == newName)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_ChangeRoomSubject(client, roomId, newName,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void CreateRoom(string subject, string description, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null)
        {
            if (null == subject || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
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
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result.RoomInfo()); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_DestroyChatroom(client, roomId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            ChatAPINative.RoomManager_FetchChatroomsWithPage(client, pageNum, pageSize,
                (IntPtr[] data, DataType dType, int size) => {
                    Debug.Log($"FetchPublicRoomsFromServer callback with dType={dType}, size={size}.");
                    if (DataType.Room == dType && size > 0)
                    {
                        var result = new PageResult<Room>();
                        result.Data = new List<Room>();
                        for(int i=0; i<size; i++)
                        {
                            var roomTO = Marshal.PtrToStructure<RoomTO>(data[i]);
                            result.Data.Add(roomTO.RoomInfo());
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result); });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_FetchChatroomAnnouncement(client, roomId,
                (IntPtr[] data, DataType dType, int size) => {
                    Debug.Log($"FetchRoomAnnouncement callback with dType={dType}, size={size}.");
                    if (DataType.String == dType && 1 == size)
                    {
                        string result = Marshal.PtrToStringAnsi(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result); });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_FetchChatroomBans(client, roomId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    List<string> banList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
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
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_FetchChatroomSpecification(client, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result.RoomInfo()); });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_FetchChatroomMembers(client, roomId, cursor, pageSize,
                (IntPtr header, IntPtr[] array, DataType dType, int size) => {
                    Debug.Log($"FetchRoomMembers callback with dType={dType}, size={size}.");
                    if (DataType.CursorResult == dType)
                    {
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTOV2>(header);
                        if (DataType.ListOfString == cursorResultTO.Type)
                        {
                            var result = new CursorResult<String>();
                            result.Data = new List<string>();
                            result.Cursor = cursorResultTO.NextPageCursor;
                            for (int i = 0; i < size; i++)
                            {
                                result.Data.Add(Marshal.PtrToStringAnsi(array[i]));
                            }
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result); });
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
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_FetchChatroomMutes(client, roomId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    List<string> muteList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var muteItem = Marshal.PtrToStringAnsi(data[i]);
                            muteList.Add(muteItem);
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(muteList); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }


        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_JoinChatroom(client, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType)
                    {
                        if(0 == dSize)
                        {
                            Debug.Log("No room information returned.");
                            handle?.OnSuccessValue(null);
                            return;
                        }
                        
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result.RoomInfo()); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_LeaveChatroom(client, roomId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            if (null == roomId || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (null != members && members.Count > 0)
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
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null)
        {
            if (null == roomId || null == adminId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_RemoveChatroomAdmin(client, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (DataType.Room == dType)
                    {
                        if (0 == dSize)
                        {
                            Debug.Log("No room information returned.");
                            handle?.Success();
                            return;
                        }

                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            if (null == roomId || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (null != members && members.Count > 0)
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
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null)
        {
            if (null == roomId || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (null != members && members.Count > 0)
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
            if (null == roomId || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (null != members && members.Count > 0)
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
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            if (null == roomId || null == announcement)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.RoomManager_UpdateChatroomAnnouncement(client, roomId, announcement,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }
    }
}