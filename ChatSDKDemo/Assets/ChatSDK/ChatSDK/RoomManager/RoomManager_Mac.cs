﻿using System;
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
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_AddRoomAdmin(client, callbackId, roomId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.Success();
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
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
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_BlockChatroomMembers(client, callbackId, roomId, membersArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.Success();
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null)
        {
            if (null == roomId || null == newOwner)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_TransferChatroomOwner(client, callbackId, roomId, newOwner,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.Success();
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null)
        {
            if (null == roomId || null == newDescription)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_ChangeChatroomDescription(client, callbackId, roomId, newDescription,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.Success();
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void ChangeRoomName(string roomId, string newName, CallBack handle = null)
        {
            if (null == roomId || null == newName)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_ChangeRoomSubject(client, callbackId, roomId, newName,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.Success();
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
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
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_CreateRoom(client, callbackId, subject, description, welcomeMsg, maxUserCount, membersArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        var room = result.RoomInfo();
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(room);
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void DestroyRoom(string roomId, CallBack handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_DestroyChatroom(client, callbackId, roomId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_FetchChatroomsWithPage(client, callbackId, pageNum, pageSize,
                (IntPtr[] data, DataType dType, int size, int cbId) => {
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
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<PageResult<Room>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(result);
                        });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<PageResult<Room>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_FetchChatroomAnnouncement(client, callbackId, roomId,
                (IntPtr[] data, DataType dType, int size, int cbId) => {
                    Debug.Log($"FetchRoomAnnouncement callback with dType={dType}, size={size}.");
                    if (DataType.String == dType && 1 == size)
                    {
                        string result = Marshal.PtrToStringAnsi(data[0]);
                        string str = new string(result.ToCharArray());
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<string>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(str);
                        });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<string>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_FetchChatroomBans(client, callbackId, roomId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    List<string> banList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var muteItem = Marshal.PtrToStringAnsi(data[i]);
                            banList.Add(muteItem);
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<List<string>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(banList);
                        });

                        
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<List<string>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_FetchChatroomSpecification(client, callbackId, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        var room = result.RoomInfo();
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(room);
                        });
                    }
                    else
                    {
                        Debug.LogError($"Room information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_FetchChatroomMembers(client, callbackId, roomId, cursor, pageSize,
                (IntPtr header, IntPtr[] array, DataType dType, int size, int cbId) => {
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
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                                var myhandle = (ValueCallBack<CursorResult<string>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                                myhandle?.OnSuccessValue(result);
                            });
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
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<CursorResult<string>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_FetchChatroomMutes(client, callbackId, roomId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    List<string> muteList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var muteItem = Marshal.PtrToStringAnsi(data[i]);
                            muteList.Add(muteItem);
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<List<string>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(muteList);
                        });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<List<string>>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }


        public override void JoinRoom(string roomId, ValueCallBack<Room> handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_JoinChatroom(client, callbackId, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType)
                    {
                        if(0 == dSize)
                        {
                            Debug.Log("No room information returned.");
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                                var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                                myhandle?.OnSuccessValue(null);
                            });
                            
                            return;
                        }
                        
                        var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        var room = result.RoomInfo();
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.OnSuccessValue(room);
                        });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (ValueCallBack<Room>)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void LeaveRoom(string roomId, CallBack handle = null)
        {
            if (null == roomId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_LeaveChatroom(client, callbackId, roomId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
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

            int muteDuration = -1; // no this parameter for API?
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_MuteChatroomMembers(client, callbackId, roomId, membersArray, size, muteDuration,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null)
        {
            if (null == roomId || null == adminId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_RemoveChatroomAdmin(client, callbackId, roomId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) => {
                    if (DataType.Room == dType)
                    {
                        if (0 == dSize)
                        {
                            Debug.Log("No room information returned.");
                        }
                        //var result = Marshal.PtrToStructure<RoomTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                            var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                            myhandle?.Success();
                        });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
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
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_RemoveRoomMembers(client, callbackId, roomId, membersArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
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
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_UnblockChatroomMembers(client, callbackId, roomId, membersArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
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
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_UnmuteChatroomMembers(client, callbackId, roomId, membersArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }

        public override void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null)
        {
            if (null == roomId || null == announcement)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.RoomManager_UpdateChatroomAnnouncement(client, callbackId, roomId, announcement,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Success();
                    });
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                        var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                        myhandle?.Error(code, desc);
                    });
                });
        }
    }
}