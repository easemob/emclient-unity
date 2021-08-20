using System.Collections.Generic;

namespace ChatSDK
{
    internal class GroupManager_Win : IGroupManager
    {
        public override void AcceptGroupInvitation(string groupId, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AcceptGroupJoinApplication(string groupId, string username, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void applyJoinToGroup(string groupId, string reason = "", CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void BlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
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

        public override void ChangeGroupOwner(string groupId, string newOwner, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateGroup(string groupName, GroupOptions options, string desc = null, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeclineGroupInvitation(string groupId, string reason = null, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeclineGroupJoinApplication(string groupId, string username, string reason = null, CallBack handle = null)
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

        public override void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> handle = null)
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

        public override void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override Group GetGroupWithId(string groupId)
        {
            throw new System.NotImplementedException();
        }

        public override List<Group> GetJoinedGroups()
        {
            throw new System.NotImplementedException();
        }

        public override void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<GroupInfo>> handle = null)
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

        public override void MuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void MuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveGroupAdmin(string groupId, string memberId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveGroupWhiteList(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnBlockGroup(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnBlockGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteGroupAllMembers(string groupId, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UnMuteGroupMembers(string groupId, List<string> members, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateGroupExt(string groupId, string ext, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }
    }
}