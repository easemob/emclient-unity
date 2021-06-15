using System.Collections.Generic;

namespace ChatSDK
{
    internal class GroupManager_Win : IGroupManager
    {
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
            throw new System.NotImplementedException();
        }

        public override void AddMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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