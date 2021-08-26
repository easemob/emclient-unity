using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    public class GroupManager_Mac : IGroupManager
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
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_ApplyJoinPublicGroup(client, groupId, "", reason,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AcceptGroupInvitation(string groupId, ValueCallBack<Group> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_AcceptInvitationFromGroup(client, groupId, "",
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result.GroupInfo()); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AcceptGroupJoinApplication(string groupId, string username, CallBack handle = null)
        {
            ChatAPINative.GroupManager_AcceptJoinGroupApplication(client, groupId, username,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AddGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            if (null == groupId || null == memberId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_AddAdmin(client, groupId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AddGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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

            ChatAPINative.GroupManager_AddMembers(client, groupId, memberArray, size,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AddGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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

            ChatAPINative.GroupManager_AddWhiteListMembers(client, groupId, memberArray, size,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_BlockGroupMessage(client, groupId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void BlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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

            ChatAPINative.GroupManager_BlockGroupMembers(client, groupId, memberArray, size,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_ChangeGroupDescription(client, groupId, desc,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string errDesc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, errDesc); }); });
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_ChangeGroupName(client, groupId, name,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, CallBack handle = null)
        {
            if (null == groupId || null == newOwner)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_TransferGroupOwner(client, groupId, newOwner,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchIsMemberInWhiteList(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Bool == dType && 1 == dSize)
                    {
                        int result = (int)data[0];
                        if (result != 0)
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(true); });
                        else
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(false); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            if (null == groupName || null == options)
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
            ChatAPINative.GroupManager_CreateGroup(client, groupName, options, desc, membersArray, size, inviteReason,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result.GroupInfo()); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                (int code, string errDesc) =>
                {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, errDesc); });
                }
                );
        }

        public override void DeclineGroupInvitation(string groupId, string reason = null, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_DeclineInvitationFromGroup(client, groupId, "", reason,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void DeclineGroupJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_DeclineJoinGroupApplication(client, groupId, username, reason,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void DestroyGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_DestoryGroup(client, groupId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null)
        {
            if (null == groupId || null == fileId || null == savePath)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_DownloadGroupSharedFile(client, groupId, savePath, fileId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupAnnouncement(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.String == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStringAnsi(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupBans(client, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    List<string> banList = new List<string>();
                    if (DataType.String == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var banItem = Marshal.PtrToStringAnsi(data[i]);
                            banList.Add(banItem);
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(banList); });
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.Log("No member in BlockList.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupSharedFiles(client, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    List<GroupSharedFile> fileList = new List<GroupSharedFile>();
                    if (DataType.ListOfGroupSharedFile == dType && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var file = Marshal.PtrToStructure<GroupSharedFile>(data[i]);
                            fileList.Add(file);
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(fileList); });
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.Log("No file exist in group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupMembers(client, groupId, pageSize, cursor,
                (IntPtr header, IntPtr[] array, DataType dType, int size) =>
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

        public override void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupMutes(client, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
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
                        if (0 == dSize)
                            Debug.Log("No member in muteList.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupSpecification(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result.GroupInfo()); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_FetchGroupWhiteList(client, groupId,
              onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
              {
                  List<string> memberList = new List<string>();
                  if (DataType.String == dType && dSize > 0)
                  {
                      for (int i = 0; i < dSize; i++)
                      {
                          var muteItem = Marshal.PtrToStringAnsi(data[i]);
                          memberList.Add(muteItem);
                      }

                      handle?.OnSuccessValue(memberList);
                  }
                  else
                  {
                      if (0 == dSize)
                          Debug.Log("Empty group white List.");
                      else
                          Debug.LogError($"Group information expected.");
                  }
              },
              handle?.Error);
        }

        
        public override Group GetGroupWithId(string groupId)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return null;
            }
            Group group = null;
            ChatAPINative.GroupManager_GetGroupWithId(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
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
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
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

        public override void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            ChatAPINative.GroupManager_FetchAllMyGroupsWithPage(client, pageNum, pageSize,
              onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
              {
                  List<Group> groupList = new List<Group>();
                  if (DataType.ListOfGroup == dType && dSize > 0)
                  {
                      for (int i = 0; i < dSize; i++)
                      {
                          var result = Marshal.PtrToStructure<GroupTO>(data[i]);
                          groupList.Add(result.GroupInfo());
                      }
                      ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(groupList); });
                  }
                  else
                  {
                      Debug.LogError($"Group information expected.");
                  }
              },
              onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            ChatAPINative.GroupManager_FetchPublicGroupsWithCursor(client, pageSize, cursor,
                (IntPtr header, IntPtr[] array, DataType dType, int size) =>
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
                            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(result); });
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
                (int code, string desc) => ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc);}));
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_JoinPublicGroup(client, groupId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_LeaveGroup(client, groupId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void MuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_MuteAllGroupMembers(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.LogError($"Empty Group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void MuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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
            int muteDuration = 1000;

            ChatAPINative.GroupManager_MuteGroupMembers(client, groupId, memberArray, size, muteDuration,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.LogError($"Empty Group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            if (null == groupId || null == memberId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_RemoveGroupAdmin(client, groupId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            if (null == groupId || null == fileId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_DeleteGroupSharedFile(client, groupId, fileId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            //TODO: C++ sdie remove members return a EMGroupPtr instance, shuold be ValueCallBack?
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.GroupManager_RemoveMembers(client, groupId, memberArray, size,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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

            ChatAPINative.GroupManager_RemoveWhiteListMembers(client, groupId, memberArray, size,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UnBlockGroup(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_UnblockGroupMessage(client, groupId,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UnBlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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

            ChatAPINative.GroupManager_UnblockGroupMembers(client, groupId, memberArray, size,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UnMuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_UnMuteAllMembers(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        if (0 == dSize)
                            Debug.LogError($"Empty Group.");
                        else
                            Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UnMuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            if (null == groupId || null == members || 0 == members.Count)
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

            ChatAPINative.GroupManager_UnmuteGroupMembers(client, groupId, memberArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.Success();
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            if (null == groupId || null == announcement)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_UpdateGroupAnnouncement(client, groupId, announcement,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UpdateGroupExt(string groupId, string ext, CallBack handle = null)
        {
            if (null == groupId || null == ext)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_ChangeGroupExtension(client, groupId, ext,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) =>
                {
                    if (DataType.Group == dType && 1 == dSize)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); });
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            if (null == groupId || null == filePath)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.GroupManager_UploadGroupSharedFile(client, groupId, filePath,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }



    }
}