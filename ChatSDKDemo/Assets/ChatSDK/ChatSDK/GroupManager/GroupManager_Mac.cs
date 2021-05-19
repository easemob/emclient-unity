using System.Collections.Generic;

namespace ChatSDK
{
    public class GroupManager_Mac : IGroupManager
    {
        public override void AcceptInvitationFromGroup(string groupId, string inviter, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AcceptJoinApplication(string groupId, string username, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddAdmin(string groupId, string memberId, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddMembers(string groupId, List<string> members, string welcome = "", CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddWhiteList(string groupId, List<string> members, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockGroup(string groupId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockMembers(string groupId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeGroupDescription(string groupId, string desc, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeGroupName(string groupId, string name, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CheckIfInGroupWhiteList(ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = "", ValueCallBack<CursorResult<Group>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeclineInvitationFromGroup(string groupId, string username, string reason = "", ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeclineJoinApplication(string groupId, string username, string reason = "", ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DestroyGroup(string groupId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DownloadGroupSharedFile(string groupId, string fileId, string savePath, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupBlockListFromServer(string groupId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupFileListFromServer(string groupId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<GroupSharedFile>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupMuteListFromServer(string groupId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupsWithoutNotice(ValueCallBack<string> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetGroupWithId(string groupId, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetJoinedGroups(ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetJoinedGroupsFromServer(int pageSize = 200, int pageNum = 1, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<Group>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void IgnoreGroupPush(string groupId, bool enable = true, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void JoinPublicGroup(string groupId, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void LeaveGroup(string groupId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteAllMembers(string groupId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveGroupSharedFile(string groupId, string fileId, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveMembers(string groupId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveWhiteList(string groupId, List<string> members, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RequestToJoinPublicGroup(string groupId, string reason = "", ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnblockGroup(string groupId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnblockMembers(string groupId, List<string> members, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteAllMembers(string groupId, CallBack callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }
    }
}