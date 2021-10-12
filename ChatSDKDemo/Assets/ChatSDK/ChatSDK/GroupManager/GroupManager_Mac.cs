using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class GroupManager_Mac : IGroupManager
    {
        private IntPtr client;
        private GroupManagerHub groupManagerHub;

        //manager level events

        internal GroupManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
            groupManagerHub = new GroupManagerHub();
            //registered listeners
            ChatAPINative.GroupManager_AddListener(client, groupManagerHub.OnInvitationReceived,
                groupManagerHub.OnRequestToJoinReceived, groupManagerHub.OnRequestToJoinAccepted, groupManagerHub.OnRequestToJoinDeclined,
                groupManagerHub.OnInvitationAccepted, groupManagerHub.OnInvitationDeclined, groupManagerHub.OnUserRemoved,
                groupManagerHub.OnGroupDestroyed, groupManagerHub.OnAutoAcceptInvitationFromGroup, groupManagerHub.OnMuteListAdded,
                groupManagerHub.OnMuteListRemoved, groupManagerHub.OnAdminAdded, groupManagerHub.OnAdminRemoved, groupManagerHub.OnOwnerChanged,
                groupManagerHub.OnMemberJoined, groupManagerHub.OnMemberExited, groupManagerHub.OnAnnouncementChanged, groupManagerHub.OnSharedFileAdded,
                groupManagerHub.OnSharedFileDeleted);
        }

        public override void applyJoinToGroup(string groupId, string reason, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_ApplyJoinPublicGroup(client, callbackId, groupId, "", reason,
                onSuccess: (int cbId) =>
                {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void AcceptGroupInvitation(string groupId, ValueCallBack<Group> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_AcceptInvitationFromGroup(client, callbackId, groupId, "",
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        var group = result.GroupInfo();
                        ChatCallbackObject.ValueCallBackOnSuccess<Group>(cbId, group);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<Group>(cbId, code, desc);
                });
        }

        public override void AcceptGroupJoinApplication(string groupId, string username, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_AcceptJoinGroupApplication(client, callbackId, groupId, username,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void AddGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == memberId || 0 == memberId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_AddAdmin(client, callbackId, groupId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void AddGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_AddMembers(client, callbackId, groupId, memberArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void AddGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 ==groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_AddWhiteListMembers(client, callbackId, groupId, memberArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_BlockGroupMessage(client, callbackId, groupId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void BlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_BlockGroupMembers(client, callbackId, groupId, memberArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_ChangeGroupDescription(client, callbackId, groupId, desc,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string errDesc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == name || 0 == name.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_ChangeGroupName(client, callbackId, groupId, name,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == newOwner || 0 == newOwner.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_TransferGroupOwner(client, callbackId, groupId, newOwner,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchIsMemberInWhiteList(client, callbackId, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Bool == dType && 1 == dSize)
                    {
                        int result = (int)data[0];
                        if (result != 0)
                            ChatCallbackObject.ValueCallBackOnSuccess<bool>(cbId, true);
                        else
                            ChatCallbackObject.ValueCallBackOnSuccess<bool>(cbId, false);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<bool>(cbId, code, desc);
                });
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            if (null == groupName || 0 == groupName.Length || null == options)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if (null != inviteMembers && inviteMembers.Count > 0)
            {
                size = inviteMembers.Count;
                membersArray = new string[size];
                int i = 0;
                foreach (string member in inviteMembers)
                {
                    membersArray[i] = member;
                    i++;
                }
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_CreateGroup(client, callbackId, groupName, options, desc, membersArray, size, inviteReason,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        var group = result.GroupInfo();
                        ChatCallbackObject.ValueCallBackOnSuccess<Group>(cbId, group);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                (int code, string errDesc, int cbId) =>
                {
                    ChatCallbackObject.ValueCallBackOnError<Group>(cbId, code, desc);
                }
                );
        }

        public override void DeclineGroupInvitation(string groupId, string reason = null, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_DeclineInvitationFromGroup(client, callbackId, groupId, "", reason,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DeclineGroupJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_DeclineJoinGroupApplication(client, callbackId, groupId, username, reason,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DestroyGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_DestoryGroup(client, callbackId, groupId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null)
        {
            if (null == groupId || null == fileId || null == savePath ||
                0 == groupId.Length || 0 == fileId.Length || 0 == savePath.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_DownloadGroupSharedFile(client, callbackId, groupId, savePath, fileId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupAnnouncement(client, callbackId, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.String == dType && 1 == dSize)
                    {
                        
                        var result = Marshal.PtrToStringAnsi(data[0]);
                        // result maybe release before handle is processed in queue.
                        // so here alloc a string to store the value.
                        string str = new string(result.ToCharArray());
                        ChatCallbackObject.ValueCallBackOnSuccess<string>(cbId, str);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<string>(cbId, code, desc);
                });
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupBans(client, callbackId, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    List<string> banList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var banItem = Marshal.PtrToStringAnsi(data[i]);
                            banList.Add(banItem);
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, banList);
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.Log("No member in BlockList.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
                });
        }

        public override void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupSharedFiles(client, callbackId, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    List<GroupSharedFile> fileList = new List<GroupSharedFile>();
                    if (DataType.ListOfGroupSharedFile == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var file = Marshal.PtrToStructure<GroupSharedFile>(data[i]);
                            fileList.Add(file);
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<GroupSharedFile>>(cbId, fileList);
                    }
                    else
                    {
                        if (0 == dSize)
                            ChatCallbackObject.ValueCallBackOnSuccess<List<GroupSharedFile>>(cbId, fileList);
                            //Debug.Log("No file exist in group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<GroupSharedFile>>(cbId, code, desc);
                });
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupMembers(client, callbackId, groupId, pageSize, cursor,
                (IntPtr header, IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"GetGroupMemberListFromServer callback with dType={dType}, size={size}.");
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
                                var item = Marshal.PtrToStringAnsi(array[i]);
                                result.Data.Add(item);
                            }
                            ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<string>>(cbId, result);
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
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<string>>(cbId, code, desc);
                });
        }

        public override void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupMutes(client, callbackId, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    List<string> muteList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var muteItem = Marshal.PtrToStringAnsi(data[i]);
                            muteList.Add(muteItem);
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, muteList);
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.Log("No member in muteList.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
                });
        }

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupSpecification(client, callbackId, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        var group = result.GroupInfo();
                        ChatCallbackObject.ValueCallBackOnSuccess<Group>(cbId, group);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<Group>(cbId, code, desc);
                });
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchGroupWhiteList(client, callbackId, groupId,
              onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
              {
                  List<string> memberList = new List<string>();
                  if (DataType.String == dType && dSize > 0)
                  {
                      for (int i = 0; i < dSize; i++)
                      {
                          var muteItem = Marshal.PtrToStringAnsi(data[i]);
                          memberList.Add(muteItem);
                      }
                      ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, memberList);
                  }
                  else
                  {
                      if (0 == dSize)
                          Debug.Log("Empty group white List.");
                      else
                          Debug.LogError($"Group information expected.");
                  }
              },
            onError: (int code, string desc, int cbId) => {
                ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
            });
        }

        
        public override Group GetGroupWithId(string groupId)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Group group = null;
            ChatAPINative.GroupManager_GetGroupWithId(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType)
                    {
                        if (1 == dSize)
                        {
                            var groupTO = Marshal.PtrToStructure<GroupTO>(data[0]);
                            group = groupTO.GroupInfo();
                            Debug.Log($"GetGroupWithId return group with id={group.GroupId}");
                        }
                        else
                        {
                            Debug.Log($"Cannot get group with id={groupId}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                }
                );
            return group;
        }

        public override List<Group> GetJoinedGroups()
        {
            var list = new List<Group>();
            ChatAPINative.GroupManager_LoadAllMyGroupsFromDB(client,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.ListOfGroup == dType)
                    {
                        for(int i=0; i< dSize; i++)
                        {
                            GroupTO gto = Marshal.PtrToStructure<GroupTO>(data[i]);
                            list.Add(gto.GroupInfo());
                        }
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                }
                );
            return list;
        }

        public override void FetchJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchAllMyGroupsWithPage(client, callbackId, pageNum, pageSize,
              onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
              {
                  List<Group> groupList = new List<Group>();
                  if (DataType.ListOfGroup == dType && dSize > 0)
                  {
                      for (int i = 0; i < dSize; i++)
                      {
                          var result = Marshal.PtrToStructure<GroupTO>(data[i]);
                          groupList.Add(result.GroupInfo());
                      }
                      ChatCallbackObject.ValueCallBackOnSuccess<List<Group>>(cbId, groupList);
                  }
                  else
                  {
                      Debug.LogError($"Group information expected.");
                  }
              },
              onError: (int code, string desc, int cbId) => {
                  ChatCallbackObject.ValueCallBackOnError<List<Group>>(cbId, code, desc);
              });
        }

        public override void FetchPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_FetchPublicGroupsWithCursor(client, callbackId, pageSize, cursor,
                (IntPtr header, IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"GetPublicGroupsFromServer callback with dType={dType}, size={size}.");
                    if (DataType.CursorResult == dType)
                    {
                        //header
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTOV2>(header);
                        if (DataType.ListOfGroup == cursorResultTO.Type)
                        {
                            var result = new CursorResult<GroupInfo>();
                            result.Data = new List<GroupInfo>();
                            result.Cursor = cursorResultTO.NextPageCursor;
                            Debug.Log($"GetPublicGroupsFromServer next page cursor {result.Cursor}");
                            //items
                            int itemSize = size;
                            for (int i = 0; i < itemSize; i++)
                            {
                                var item = Marshal.PtrToStructure<GroupInfoTO>(array[i]);
                                var groupInfo = new GroupInfo();
                                groupInfo.GroupId = item.GroupId;
                                groupInfo.GroupName = item.GroupName;
                                result.Data.Add(groupInfo);
                            }
                            ChatCallbackObject.ValueCallBackOnSuccess<CursorResult<GroupInfo>>(cbId, result);
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid return type from native GetPublicGroupsFromServer(), please check native c wrapper code.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }

                },
                (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<CursorResult<GroupInfo>>(cbId, code, desc);
                });
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_JoinPublicGroup(client, callbackId, groupId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_LeaveGroup(client, callbackId, groupId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void MuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_MuteAllGroupMembers(client, callbackId, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.LogError($"Empty Group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void MuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            //to-do: need to add this into API function??
            int muteDuration = -1;
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_MuteGroupMembers(client, callbackId, groupId, memberArray, size, muteDuration,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.LogError($"Empty Group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void RemoveGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == memberId || 0 == memberId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_RemoveGroupAdmin(client, callbackId, groupId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DeleteGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == fileId || 0 == fileId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_DeleteGroupSharedFile(client, callbackId, groupId, fileId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DeleteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }

            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_RemoveMembers(client, callbackId, groupId, memberArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void RemoveGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_RemoveWhiteListMembers(client, callbackId, groupId, memberArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UnBlockGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_UnblockGroupMessage(client, callbackId, groupId,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UnBlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_UnblockGroupMembers(client, callbackId, groupId, memberArray, size,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UnMuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_UnMuteAllMembers(client, callbackId, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.LogError($"Empty Group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UnMuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == members || 0 == members.Count)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_UnmuteGroupMembers(client, callbackId, groupId, memberArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == announcement || 0 == announcement.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_UpdateGroupAnnouncement(client, callbackId, groupId, announcement,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UpdateGroupExt(string groupId, string ext, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == ext)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_ChangeGroupExtension(client, callbackId, groupId, ext,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length || null == filePath || 0 == filePath.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.GroupManager_UploadGroupSharedFile(client, callbackId, groupId, filePath,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }



    }
}