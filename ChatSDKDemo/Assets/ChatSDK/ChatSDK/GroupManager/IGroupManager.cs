using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IGroupManager
    {

        public abstract void AcceptInvitationFromGroup(string groupId, string inviter, ValueCallBack<Group> handle = null);

        public abstract void AcceptJoinApplication(string groupId, string username, CallBack handle = null);

        public abstract void AddAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null);

        public abstract void AddMembers(string groupId, List<string> members, CallBack handle = null);

        public abstract void AddWhiteList(string groupId, List<string> members, CallBack handle = null);

        public abstract void BlockGroup(string groupId, CallBack handle = null);

        public abstract void BlockMembers(string groupId, List<string> members, CallBack handle = null);

        public abstract void ChangeGroupDescription(string groupId, string desc, CallBack handle = null);

        public abstract void ChangeGroupName(string groupId, string name, CallBack handle = null);

        public abstract void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> handle = null);

        public abstract void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null);

        public abstract void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null);

        public abstract void DeclineInvitationFromGroup(string groupId, string username, string reason = null, CallBack handle = null);

        public abstract void DeclineJoinApplication(string groupId, string username, string reason = null, CallBack handle = null);

        public abstract void DestroyGroup(string groupId, CallBack handle = null);

        public abstract void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null);

        public abstract void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null);

        public abstract void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

        public abstract void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null);

        public abstract void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<string>> handle = null);

        public abstract void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

        public abstract void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null);

        public abstract void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null);

        public abstract Group GetGroupWithId(string groupId);

        public abstract List<Group> GetJoinedGroups();

        public abstract void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200,  ValueCallBack<List<Group>> handle = null);

        public abstract void GetPublicGroupsFromServer(int pageSize = 200, string cursor = null, ValueCallBack<CursorResult<GroupInfo>> handle = null);

        public abstract void JoinPublicGroup(string groupId, CallBack handle = null);

        public abstract void LeaveGroup(string groupId, CallBack handle = null);

        public abstract void MuteAllMembers(string groupId, ValueCallBack<Group> handle = null);

        public abstract void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null);

        public abstract void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null);

        public abstract void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null);

        public abstract void RemoveMembers(string groupId, List<string> members, CallBack handle = null);

        public abstract void RemoveWhiteList(string groupId, List<string> members, CallBack handle = null);

        public abstract void UnBlockGroup(string groupId, CallBack handle = null);

        public abstract void UnBlockMembers(string groupId, List<string> members, CallBack handle = null);

        public abstract void UnMuteAllMembers(string groupId, ValueCallBack<Group> handle = null);

        public abstract void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null);

        public abstract void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null);
        
        public abstract void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> handle = null);

        public abstract void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null);

        public void AddGroupManagerDelegate(IGroupManagerDelegate groupManagerDelegate)
        {
            CallbackManager.Instance().groupManagerDelegates.Add(groupManagerDelegate);
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().groupManagerDelegates.Clear();
        }

    }

}