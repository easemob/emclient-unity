using System.Collections.Generic;
using UnityEngine;

namespace AgoraChat
{
    internal sealed class GroupManager_Android : IGroupManager
    {

        private AndroidJavaObject wrapper;

        public GroupManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMGroupManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override void applyJoinToGroup(string groupId, string reason, CallBack handle = null) {
            wrapper.Call("", groupId, reason, handle?.callbackId);
        }

        public override void AcceptGroupInvitation(string groupId, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("acceptInvitationFromGroup", groupId, handle?.callbackId);
        }

        public override void AcceptGroupJoinApplication(string groupId, string username, CallBack handle = null)
        {
            wrapper.Call("acceptJoinApplication", groupId, username, handle?.callbackId);
        }

        public override void AddGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            wrapper.Call("addAdmin", groupId, memberId, handle?.callbackId);
        }

        public override void AddGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("addMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void AddGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("addWhiteList", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("blockGroup", groupId,  handle?.callbackId);
        }

        public override void BlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("blockMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            wrapper.Call("changeGroupDescription", groupId, desc, handle?.callbackId);
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            wrapper.Call("changeGroupSubject", groupId, name, handle?.callbackId);
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, CallBack handle = null)
        {
            wrapper.Call("changeGroupOwner", groupId, newOwner, handle?.callbackId);
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            wrapper.Call("checkIfInGroupWhiteList", groupId, handle?.callbackId);
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc = null, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("createGroup", groupName, options.ToJsonString(), desc, TransformTool.JsonStringFromStringList(inviteMembers), inviteReason, handle?.callbackId);
        }

        public override void DeclineGroupInvitation(string groupId,string reason = null, CallBack handle = null)
        {
            wrapper.Call("declineInvitationFromGroup", groupId, reason, handle?.callbackId);
        }

        public override void DeclineGroupJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
        {
            
            wrapper.Call("declineJoinApplication", groupId, username, reason, handle?.callbackId);
        }

        public override void DestroyGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("destroyGroup", groupId, handle?.callbackId);
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null)
        {
            wrapper.Call("downloadGroupSharedFile", groupId, groupId, fileId, savePath, handle?.callbackId);
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null)
        {
            wrapper.Call("getGroupAnnouncementFromServer", groupId, handle?.callbackId);
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200,  ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getGroupBlockListFromServer", groupId, pageSize, pageNum, handle?.callbackId);
        }

        public override void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null)
        {
            wrapper.Call("getGroupFileListFromServer", groupId, pageSize, pageNum, handle?.callbackId);
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<string>> handle = null)
        {
            wrapper.Call("getGroupMemberListFromServer", groupId, pageSize, cursor, handle?.callbackId);
        }

        public override void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getGroupMuteListFromServer", groupId, pageSize, pageNum, handle?.callbackId);
        }

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("getGroupSpecificationFromServer", groupId, handle?.callbackId);
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getGroupWhiteListFromServer", groupId, handle?.callbackId);
        }

        public override Group GetGroupWithId(string groupId)
        {
            string jsonString = wrapper.Call<string>("getGroupWithId", groupId);
            if (jsonString == null || jsonString.Length == 0) {
                return null;
            }

            return new Group(jsonString);

        }

        public override List<Group> GetJoinedGroups()
        {
            string jsonString = wrapper.Call<string>("getJoinedGroups");
            return TransformTool.JsonStringToGroupList(jsonString);
        }

        public override void FetchJoinedGroupsFromServer(int pageNum, int pageSize , bool needAffiliations = false, bool needRole = false, ValueCallBack<List<Group>> handle = null)
        {
            wrapper.Call("getJoinedGroupsFromServer", pageSize, pageNum, handle?.callbackId);
        }

        public override void FetchPublicGroupsFromServer(int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            wrapper.Call("getPublicGroupsFromServer", pageSize, cursor, handle?.callbackId);
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("joinPublicGroup", groupId, handle?.callbackId);
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("leaveGroup", groupId, handle?.callbackId);
        }

        public override void MuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            wrapper.Call("muteAllMembers", groupId, handle?.callbackId);
        }

        public override void MuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("muteMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void RemoveGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            wrapper.Call("removeAdmin", groupId, memberId, handle?.callbackId);
        }

        public override void DeleteGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            wrapper.Call("removeGroupSharedFile", groupId, fileId, handle?.callbackId);
        }

        public override void DeleteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("removeMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void RemoveGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("removeWhiteList", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UnBlockGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("unblockGroup", groupId, handle?.callbackId);
        }

        public override void UnBlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("unblockMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UnMuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            wrapper.Call("unMuteAllMembers", groupId, handle?.callbackId);
        }

        public override void UnMuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("unMuteMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            wrapper.Call("updateGroupAnnouncement", groupId, announcement, handle?.callbackId);
        }

        public override void UpdateGroupExt(string groupId, string ext, CallBack handle = null)
        {
            wrapper.Call("updateGroupExt", groupId, ext, handle?.callbackId);
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            wrapper.Call("uploadGroupSharedFile", groupId, filePath, handle?.callbackId);
        }
    }
}