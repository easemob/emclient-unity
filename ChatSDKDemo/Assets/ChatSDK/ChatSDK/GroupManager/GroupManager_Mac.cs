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
            groupManagerHub = new GroupManagerHub(Delegate);
            //registered listeners
            ChatAPINative.GroupManager_AddListener(client, groupManagerHub.OnInvitationReceived,
                groupManagerHub.OnRequestToJoinReceived, groupManagerHub.OnRequestToJoinAccepted, groupManagerHub.OnRequestToJoinDeclined,
                groupManagerHub.OnInvitationAccepted, groupManagerHub.OnInvitationDeclined, groupManagerHub.OnUserRemoved,
                groupManagerHub.OnGroupDestroyed, groupManagerHub.OnAutoAcceptInvitationFromGroup, groupManagerHub.OnMuteListAdded,
                groupManagerHub.OnMuteListRemoved, groupManagerHub.OnAdminAdded, groupManagerHub.OnAdminRemoved, groupManagerHub.OnOwnerChanged,
                groupManagerHub.OnMemberJoined, groupManagerHub.OnMemberExited, groupManagerHub.OnAnnouncementChanged, groupManagerHub.OnSharedFileAdded,
                groupManagerHub.OnSharedFileDeleted);
        }

        internal OnInvitationReceived OnInvitationReceived;
        internal OnRequestToJoinReceived OnRequestToJoinReceived;
        internal OnRequestToJoinAccepted OnRequestToJoinAccepted;
        internal OnRequestToJoinDeclined OnRequestToJoinDeclined;
        internal OnInvitationAccepted OnInvitationAccepted;
        internal OnInvitationDeclined OnInvitationDeclined;
        internal OnUserRemoved OnUserRemoved;
        internal OnGroupDestroyed OnGroupDestroyed;
        internal OnAutoAcceptInvitationFromGroup OnAutoAcceptInvitationFromGroup;
        internal OnMuteListAdded OnMuteListAdded;
        internal OnMuteListRemoved OnMuteListRemoved;
        internal OnAdminAdded OnAdminAdded;
        internal OnAdminRemoved OnAdminRemoved;
        internal OnOwnerChanged OnOwnerChanged;
        internal OnMemberJoined OnMemberJoined;
        internal OnMemberExited OnMemberExited;
        internal OnAnnouncementChanged OnAnnouncementChanged;
        internal OnSharedFileAdded OnSharedFileAdded;
        internal OnSharedFileDeleted OnSharedFileDeleted;
    
        public override void AcceptInvitationFromGroup(string groupId, string inviter, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AcceptJoinApplication(string groupId, string username, CallBack handle = null)
        {
            throw new System.NotImplementedException();
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
                handle?.OnError);
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
            throw new System.NotImplementedException();
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            ChatAPINative.GroupManager_ChangeGroupName(client, groupId, name,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            throw new System.NotImplementedException();
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
                handle?.OnError);
        }

        public override void DeclineInvitationFromGroup(string groupId, string username, string reason = null, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeclineJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DestroyGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupsWithoutNotice(ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupWithId(string groupId, ValueCallBack<Group> handle = null)
        {
            ChatAPINative.GroupManager_GetGroupWithId(client, groupId,
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
                handle?.OnError); 
        }

        public override void GetJoinedGroups(ValueCallBack<List<Group>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetPublicGroupsFromServer(int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void IgnoreGroupPush(string groupId, bool enable = true, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteAllMembers(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public override void RequestToJoinPublicGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnblockGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnblockMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteAllMembers(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }
    }
}