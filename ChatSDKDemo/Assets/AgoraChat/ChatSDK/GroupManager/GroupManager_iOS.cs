using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace AgoraChat
{
    internal sealed class GroupManager_iOS : IGroupManager
    {

        public GroupManager_iOS()
        {
        }

        public override void applyJoinToGroup(string groupId, string reason, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("reason", reason ?? "");
            ChatAPIIOS.GroupManager_HandleMethodCall("requestToJoinPublicGroup", obj.ToString(), handle?.callbackId);
        }

        public override void AcceptGroupInvitation(string groupId, ValueCallBack<Group> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("acceptInvitationFromGroup", obj.ToString(), handle?.callbackId);
        }

        public override void AcceptGroupJoinApplication(string groupId, string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("username", username);
            ChatAPIIOS.GroupManager_HandleMethodCall("acceptJoinApplication", obj.ToString(), handle?.callbackId);
        }

        public override void AddGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("memberId", memberId);
            ChatAPIIOS.GroupManager_HandleMethodCall("addAdmin", obj.ToString(), handle?.callbackId);
        }

        public override void AddGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("addMembers", obj.ToString(), handle?.callbackId);
        }

        public override void AddGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("addWhiteList", obj.ToString(), handle?.callbackId);
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("blockGroup", obj.ToString(), handle?.callbackId);
        }

        public override void BlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("blockMembers", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeGroupDescription(string groupId, string desc, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("desc", desc);
            ChatAPIIOS.GroupManager_HandleMethodCall("updateDescription", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeGroupName(string groupId, string name, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("name", name);
            ChatAPIIOS.GroupManager_HandleMethodCall("updateGroupSubject", obj.ToString(), handle?.callbackId);
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("owner", newOwner);
            ChatAPIIOS.GroupManager_HandleMethodCall("updateGroupOwner", obj.ToString(), handle?.callbackId);
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("checkIfInGroupWhiteList", obj.ToString(), handle?.callbackId);
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupName", groupName ?? "");
            obj.Add("desc", desc ?? "");
            obj.Add("inviteMembers", TransformTool.JsonStringFromStringList(inviteMembers));
            obj.Add("inviteReason", inviteReason ?? "");
            obj.Add("options", options.ToJsonString());
            ChatAPIIOS.GroupManager_HandleMethodCall("createGroup", obj.ToString(), handle?.callbackId);
        }

        public override void DeclineGroupInvitation(string groupId, string reason = null, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("reason", reason ?? "");
            ChatAPIIOS.GroupManager_HandleMethodCall("declineInvitationFromGroup", obj.ToString(), handle?.callbackId);
        }

        public override void DeclineGroupJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("username", username);
            obj.Add("reason", reason ?? "");
            ChatAPIIOS.GroupManager_HandleMethodCall("declineJoinApplication", obj.ToString(), handle?.callbackId);
        }

        public override void DestroyGroup(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("destroyGroup", obj.ToString(), handle?.callbackId);
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("fileId", fileId);
            obj.Add("savePath", savePath);
            ChatAPIIOS.GroupManager_HandleMethodCall("downloadGroupSharedFile", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupAnnouncementFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupBlockListFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupFileListFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("cursor", cursor ?? "");
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupMemberListFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupMuteListFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupSpecificationFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("getGroupWhiteListFromServer", obj.ToString(), handle?.callbackId);
        }

        public override Group GetGroupWithId(string groupId)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            string jsonString = ChatAPIIOS.GroupManager_GetMethodCall("getGroupWithId", obj.ToString());
            if (jsonString == null || jsonString.Length == 0) {
                return null;
            }
            return new Group(jsonString);
        }

        public override List<Group> GetJoinedGroups()
        {

            string jsonString = ChatAPIIOS.GroupManager_GetMethodCall("getJoinedGroups");
            List<Group> list = new List<Group>();
            if (jsonString == null || jsonString.Length == 0)
            {
                return list;
            }

            list = TransformTool.JsonStringToGroupList(jsonString);

            return list;
        }

        public override void FetchJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("pageNum", pageNum);
            obj.Add("pageSize", pageSize);
            ChatAPIIOS.GroupManager_HandleMethodCall("getJoinedGroupsFromServer", obj.ToString(), handle?.callbackId);
        }

        public override void FetchPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<GroupInfo>> handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("cursor", cursor ?? "");
            obj.Add("pageSize", pageSize);
            string jsonString = obj.ToString();
            ChatAPIIOS.GroupManager_HandleMethodCall("getPublicGroupsFromServer", jsonString, handle?.callbackId);
        }

        public override void JoinPublicGroup(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("joinPublicGroup", obj.ToString(), handle?.callbackId);
        }

        public override void LeaveGroup(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("leaveGroup", obj.ToString(), handle?.callbackId);
        }

        public override void MuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("muteAllMembers", obj.ToString(), handle?.callbackId);
        }

        public override void MuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("muteMembers", obj.ToString(), handle?.callbackId);
        }

        public override void RemoveGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("admin", memberId);
            ChatAPIIOS.GroupManager_HandleMethodCall("removeAdmin", obj.ToString(), handle?.callbackId);
        }

        public override void DeleteGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("fileId", fileId);
            ChatAPIIOS.GroupManager_HandleMethodCall("removeGroupSharedFile", obj.ToString(), handle?.callbackId);
        }

        public override void DeleteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("removeMembers", obj.ToString(), handle?.callbackId);
        }

        public override void RemoveGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("removeWhiteList", obj.ToString(), handle?.callbackId);
        }

        public override void UnBlockGroup(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("unblockGroup", obj.ToString(), handle?.callbackId);
        }

        public override void UnBlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("unblockMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnMuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            ChatAPIIOS.GroupManager_HandleMethodCall("unMuteAllMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UnMuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("members", TransformTool.JsonStringFromStringList(members));
            ChatAPIIOS.GroupManager_HandleMethodCall("unMuteMembers", obj.ToString(), handle?.callbackId);
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("announcement", announcement ?? "");
            ChatAPIIOS.GroupManager_HandleMethodCall("updateGroupAnnouncement", obj.ToString(), handle?.callbackId);
        }

        public override void UpdateGroupExt(string groupId, string ext, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("ext", ext ?? "");
            ChatAPIIOS.GroupManager_HandleMethodCall("updateGroupExt", obj.ToString(), handle?.callbackId);
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("filePath", filePath);
            ChatAPIIOS.GroupManager_HandleMethodCall("uploadGroupSharedFile", obj.ToString(), handle?.callbackId);
        }
    }
}