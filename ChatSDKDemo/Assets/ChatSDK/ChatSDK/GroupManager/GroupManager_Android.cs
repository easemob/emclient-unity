using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    public class GroupManager_Android : IGroupManager
    {

        static string GroupManagerListener_Obj = "unity_chat_emclient_groupmanager_delegate_obj";

        private AndroidJavaObject wrapper;

        GameObject listenerGameObj;

        public GroupManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMGroupManagerWrapper"))
            {
                listenerGameObj = new GameObject(GroupManagerListener_Obj);
                GroupManagerListener listener = listenerGameObj.AddComponent<GroupManagerListener>();
                listener.delegater = Delegate;
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override void AcceptInvitationFromGroup(string groupId, string inviter, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("acceptInvitationFromGroup", groupId, inviter, handle?.callbackId);
        }

        public override void AcceptJoinApplication(string groupId, string username, CallBack handle = null)
        {
            wrapper.Call("acceptJoinApplication", groupId, username, handle?.callbackId);
        }

        public override void AddAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("addAdmin", groupId, memberId, handle?.callbackId);
        }

        public override void AddMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("addMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void AddWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("addWhiteList", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("blockGroup", groupId,  handle?.callbackId);
        }

        public override void BlockMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("blockMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            wrapper.Call("updateDescription", groupId, desc, handle?.callbackId);
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            wrapper.Call("updateGroupSubject", groupId, name, handle?.callbackId);
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("updateGroupOwner", groupId, newOwner, handle?.callbackId);
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            wrapper.Call("checkIfInGroupWhiteList", groupId, handle?.callbackId);
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc = null, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("createGroup", groupName, options.ToJsonString(), desc, TransformTool.JsonStringFromStringList(inviteMembers), inviteReason, handle?.callbackId);
        }

        public override void DeclineInvitationFromGroup(string groupId, string username, string reason = null, CallBack handle = null)
        {
            wrapper.Call("declineInvitationFromGroup", groupId, username, reason, handle?.callbackId);
        }

        public override void DeclineJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
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

        public override void GetGroupsWithoutNotice(ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getGroupsWithoutPushNotification", handle?.callbackId);
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getGroupWhiteListFromServer", groupId, handle?.callbackId);
        }

        public override void GetGroupWithId(string groupId, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("getGroupWithId", groupId, handle?.callbackId);
        }

        public override void GetJoinedGroups(ValueCallBack<List<Group>> handle = null)
        {
            wrapper.Call("getJoinedGroups", handle?.callbackId);
        }

        public override void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            wrapper.Call("getJoinedGroupsFromServer", pageSize, pageNum, handle?.callbackId);
        }

        public override void GetPublicGroupsFromServer(int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            wrapper.Call("getPublicGroupsFromServer", pageSize, cursor, handle?.callbackId);
        }

        public override void IgnoreGroupPush(string groupId, bool enable = true, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("ignoreGroupPush", groupId, enable, handle?.callbackId);
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("joinPublicGroup", groupId, handle?.callbackId);
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("leaveGroup", groupId, handle?.callbackId);
        }

        public override void MuteAllMembers(string groupId, CallBack handle = null)
        {
            wrapper.Call("muteAllMembers", groupId, handle?.callbackId);
        }

        public override void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("muteMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("removeAdmin", groupId, memberId, handle?.callbackId);
        }

        public override void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            wrapper.Call("removeGroupSharedFile", groupId, fileId, handle?.callbackId);
        }

        public override void RemoveMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("removeMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void RemoveWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("removeWhiteList", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void RequestToJoinPublicGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("requestToJoinPublicGroup", groupId, handle?.callbackId);
        }

        public override void UnblockGroup(string groupId, CallBack handle = null)
        {
            wrapper.Call("unblockGroup", groupId, handle?.callbackId);
        }

        public override void UnblockMembers(string groupId, List<string> members, CallBack handle = null)
        {
            wrapper.Call("unblockMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UnMuteAllMembers(string groupId, CallBack handle = null)
        {
            wrapper.Call("unMuteAllMembers", groupId, handle?.callbackId);
        }

        public override void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("unMuteMembers", groupId, TransformTool.JsonStringFromStringList(members), handle?.callbackId);
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            wrapper.Call("updateGroupAnnouncement", groupId, announcement, handle?.callbackId);
        }

        public override void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> handle = null)
        {
            wrapper.Call("updateGroupExt", groupId, ext, handle?.callbackId);
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            wrapper.Call("uploadGroupSharedFile", groupId, filePath, handle?.callbackId);
        }
    }
}