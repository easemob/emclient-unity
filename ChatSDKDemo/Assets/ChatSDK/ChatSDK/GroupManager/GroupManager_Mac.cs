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
            groupManagerHub = new GroupManagerHub(CallbackManager.Instance().groupManagerDelegates);
            //registered listeners
            ChatAPINative.GroupManager_AddListener(client, groupManagerHub.OnInvitationReceived,
                groupManagerHub.OnRequestToJoinReceived, groupManagerHub.OnRequestToJoinAccepted, groupManagerHub.OnRequestToJoinDeclined,
                groupManagerHub.OnInvitationAccepted, groupManagerHub.OnInvitationDeclined, groupManagerHub.OnUserRemoved,
                groupManagerHub.OnGroupDestroyed, groupManagerHub.OnAutoAcceptInvitationFromGroup, groupManagerHub.OnMuteListAdded,
                groupManagerHub.OnMuteListRemoved, groupManagerHub.OnAdminAdded, groupManagerHub.OnAdminRemoved, groupManagerHub.OnOwnerChanged,
                groupManagerHub.OnMemberJoined, groupManagerHub.OnMemberExited, groupManagerHub.OnAnnouncementChanged, groupManagerHub.OnSharedFileAdded,
                groupManagerHub.OnSharedFileDeleted);
        }

        // TODO:
        public override void applyJoinToGroup(string groupId, string reason, CallBack handle = null) {
        }

        public override void AcceptInvitationFromGroup(string groupId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_AcceptInvitationFromGroup(client, groupId, "",
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void AcceptJoinApplication(string groupId, string username, CallBack handle = null)
        {
            ChatAPINative.GroupManager_AcceptJoinGroupApplication(client, groupId, username,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void AddAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_AddAdmin(client, groupId, memberId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void AddMembers(string groupId, List<string> members, CallBack handle = null)
        {
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach(string member in members)
            {
                memberArray[i] = member;
                i++;
            }
            
            ChatAPINative.GroupManager_AddMembers(client, groupId, memberArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void AddWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.GroupManager_AddWhiteListMembers(client, groupId, memberArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            ChatAPINative.GroupManager_BlockGroupMessage(client, groupId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void BlockMembers(string groupId, List<string> members, CallBack handle = null)
        {
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.GroupManager_BlockGroupMembers(client, groupId, memberArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            ChatAPINative.GroupManager_ChangeGroupDescription(client, groupId, desc,
                onSuccess: () => handle?.Success(),
                onError: (int code, string errDesc) => handle?.Error(code, errDesc));
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            ChatAPINative.GroupManager_ChangeGroupName(client, groupId, name,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_TransferGroupOwner(client, groupId, newOwner,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            ChatAPINative.GroupManager_FetchIsMemberInWhiteList(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Bool && dSize == 1)
                    {
                        int result = (int)data[0];
                        if(result != 0)
                            handle?.OnSuccessValue(true);
                        else
                            handle?.OnSuccessValue(false);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            //turn List<string> into array
            int size = 0;
            var membersArray = new string[0];
            if(inviteMembers != null && inviteMembers.Count >0)
            {
                size = inviteMembers.Count;
                membersArray = new string[size];
                int i = 0;
                foreach(string member in inviteMembers)
                {
                    membersArray[i] = member;
                    i++;
                }
            }
            ChatAPINative.GroupManager_CreateGroup(client, groupName, options, desc, membersArray, size, inviteReason,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if(dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void DeclineInvitationFromGroup(string groupId, string reason = null, CallBack handle = null)
        {
            ChatAPINative.GroupManager_DeclineInvitationFromGroup(client, groupId, "", reason,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void DeclineJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
        {
            ChatAPINative.GroupManager_DeclineJoinGroupApplication(client, groupId, username, reason,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void DestroyGroup(string groupId, CallBack handle = null)
        {
            ChatAPINative.GroupManager_DestoryGroup(client, groupId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null)
        {
            ChatAPINative.GroupManager_DownloadGroupSharedFile(client, groupId, savePath, fileId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupAnnouncement(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.String && dSize == 1)
                    {
                        var result = Marshal.PtrToStringAnsi(data[0]);
                        handle?.OnSuccessValue(result);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupBans(client, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    List<string> banList = new List<string>();
                    if (dType == DataType.String && dSize > 0)
                    {
                        for(int i=0; i<dSize; i++)
                        {
                            var banItem = Marshal.PtrToStringAnsi(data[i]);
                            banList.Add(banItem);
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

        public override void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupSharedFiles(client, groupId, pageNum, pageSize,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    List<GroupSharedFile> fileList = new List<GroupSharedFile>();
                    if (dType == DataType.ListOfGroupSharedFile && dSize > 0)
                    {
                        for (int i = 0; i < dSize; i++)
                        {
                            var file = Marshal.PtrToStructure<GroupSharedFile>(data[i]);
                            fileList.Add(file);
                        }

                        handle?.OnSuccessValue(fileList);
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<string>> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupMembers(client, groupId, pageSize, cursor,
                (IntPtr[] cursorResult, DataType dType, int size) => {
                    Debug.Log($"GetGroupMemberListFromServer callback with dType={dType}, size={size}.");
                    if (dType == DataType.CursorResult && size == 1)
                    {
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTO>(cursorResult[0]);
                        if (cursorResultTO.Type == DataType.ListOfString)
                        {
                            var result = new CursorResult<String>();
                            result.Cursor = cursorResultTO.NextPageCursor;
                            int itemSize = cursorResultTO.Size;

                            for(int i=0; i<itemSize; i++)
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

        public override void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupMutes(client, groupId, pageNum, pageSize,
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

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupSpecification(client, groupId,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            ChatAPINative.GroupManager_FetchGroupWhiteList(client, groupId, 
              onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                  List<string> memberList = new List<string>();
                  if (dType == DataType.String && dSize > 0)
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
                      Debug.LogError($"Group information expected.");
                  }
              },
              handle?.Error);
        }

        // TODO:
        public override Group GetGroupWithId(string groupId)
        {
            return null;
            //ChatAPINative.GroupManager_GetGroupWithId(client, groupId,
            //    onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
            //        if (dType == DataType.Group && dSize == 1)
            //        {
            //            var result = Marshal.PtrToStructure<GroupTO>(data[0]);
            //            handle?.OnSuccessValue(result.GroupInfo());
            //        }
            //        else
            //        {
            //            Debug.LogError($"Group information expected.");
            //        }
            //    },
            //    handle?.OnError); 
        }

        // TODO:
        public override List<Group> GetJoinedGroups()
        {

            return null;
            //ChatAPINative.GroupManager_LoadAllMyGroupsFromDB(client,
            //  onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
            //      List<Group> groupList = new List<Group>();
            //      if (dType == DataType.ListOfGroup && dSize > 0)
            //      {
            //          for (int i = 0; i < dSize; i++)
            //          {
            //              var result = Marshal.PtrToStructure<GroupTO>(data[i]);
            //              groupList.Add(result.GroupInfo());
            //          }
            //          handle?.OnSuccessValue(groupList);
            //      }
            //      else
            //      {
            //          Debug.LogError($"Group information expected.");
            //      }
            //  },
            //  handle?.OnError);
        }

        public override void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            ChatAPINative.GroupManager_FetchAllMyGroupsWithPage(client, pageNum, pageSize,
              onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                  List<Group> groupList = new List<Group>();
                  if (dType == DataType.ListOfGroup && dSize > 0)
                  {
                      for (int i = 0; i < dSize; i++)
                      {
                          var result = Marshal.PtrToStructure<GroupTO>(data[i]);
                          groupList.Add(result.GroupInfo());
                      }
                      handle?.OnSuccessValue(groupList);
                  }
                  else
                  {
                      Debug.LogError($"Group information expected.");
                  }
              },
              handle?.Error);
        }

        public override void GetPublicGroupsFromServer(int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            ChatAPINative.GroupManager_FetchPublicGroupsWithCursor(client, pageSize, cursor,
                (IntPtr[] cursorResult, DataType dType, int size) => {
                    Debug.Log($"GetPublicGroupsFromServer callback with dType={dType}, size={size}.");
                    if (dType == DataType.CursorResult && size == 1)
                    {
                        var cursorResultTO = Marshal.PtrToStructure<CursorResultTO>(cursorResult[0]);
                        if (cursorResultTO.Type == DataType.ListOfString)
                        {
                            var result = new CursorResult<GroupInfo>();
                            result.Cursor = cursorResultTO.NextPageCursor;
                            int itemSize = cursorResultTO.Size;

                            for (int i = 0; i < itemSize; i++)
                            {
                                var item = Marshal.PtrToStructure<GroupTO>(cursorResultTO.Data[i]);
                                var groupInfo = new GroupInfo();
                                groupInfo.GroupId = item.GroupId;
                                groupInfo.GroupName = item.Name;
                                result.Data.Add(groupInfo);
                            }
                            handle?.OnSuccessValue(result);
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
                (int code, string desc) => handle?.Error(code, desc));
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            ChatAPINative.GroupManager_JoinPublicGroup(client, groupId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            ChatAPINative.GroupManager_LeaveGroup(client, groupId,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void MuteAllMembers(string groupId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_MuteAllGroupMembers(client, groupId,
               onSuccess: () => handle?.Success(),
               onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null)
        {
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
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);

        }

        public override void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_RemoveGroupAdmin(client, groupId, memberId, 
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            ChatAPINative.GroupManager_DeleteGroupSharedFile(client, groupId, fileId,
               onSuccess: () => handle?.Success(),
               onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void RemoveMembers(string groupId, List<string> members, CallBack handle = null)
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
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void RemoveWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.GroupManager_RemoveWhiteListMembers(client, groupId, memberArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UnBlockGroup(string groupId, CallBack handle = null)
        {
            ChatAPINative.GroupManager_UnblockGroupMessage(client, groupId, 
               onSuccess: () => handle?.Success(),
               onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UnBlockMembers(string groupId, List<string> members, CallBack handle = null)
        {
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.GroupManager_UnblockGroupMembers(client, groupId, memberArray, size,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UnMuteAllMembers(string groupId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_UnMuteAllMembers(client, groupId,
               onSuccess: () => handle?.Success(),
               onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null)
        {
            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.GroupManager_UnmuteGroupMembers(client, groupId, memberArray, size,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
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
            ChatAPINative.GroupManager_UpdateGroupAnnouncement(client, groupId, announcement,
               onSuccess: () => handle?.Success(),
               onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_ChangeGroupExtension(client, groupId, ext,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
                    if (dType == DataType.Group && dSize == 1)
                    {
                        var result = Marshal.PtrToStructure<GroupTO>(data[0]);
                        handle?.OnSuccessValue(result.GroupInfo());
                    }
                    else
                    {
                        Debug.LogError($"Group information expected.");
                    }
                },
                handle?.Error);
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            ChatAPINative.GroupManager_UploadGroupSharedFile(client, groupId, filePath,
               onSuccess: () => handle?.Success(),
               onError: (int code, string desc) => handle?.Error(code, desc));
        }



    }
}