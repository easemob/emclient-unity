using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IGroupManager
    {
        internal WeakDelegater<IGroupManagerDelegate> Delegate = new WeakDelegater<IGroupManagerDelegate>();

        public abstract void GetGroupWithId(string groupId, ValueCallBack<Group> callBack = null);

        public abstract void GetJoinedGroups(ValueCallBack<Group> callBack = null);

        public abstract void GetJoinedGroupsFromServer(int pageSize = 200, int pageNum = 1, ValueCallBack<Group> callBack = null);

        public abstract void GetPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<Group>> callBack = null);

        public abstract void GetGroupsWithoutNotice(ValueCallBack<string> callBack = null);

        public abstract void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = "", ValueCallBack<CursorResult<Group>> callBack = null);

        public abstract void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> callBack = null);

        public abstract void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> callBack = null);

        public abstract void GetGroupBlockListFromServer(string groupId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> callBack = null);

        public abstract void GetGroupMuteListFromServer(string groupId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> callBack = null);

        public abstract void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> callBack = null);

        public abstract void CheckIfInGroupWhiteList(ValueCallBack<bool> callBack = null);

        public abstract void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> callBack = null);

        public abstract void AddMembers(string groupId, List<string> members, string welcome = "", CallBack callBack = null);

        public abstract void RemoveMembers(string groupId, List<string> members, CallBack callBack = null);

        public abstract void BlockMembers(string groupId, List<string> members, CallBack callBack = null);
         
        public abstract void UnblockMembers(string groupId, List<string> members, CallBack callBack = null);

        public abstract void ChangeGroupName(string groupId, string name, ValueCallBack<Group> callBack = null);

        public abstract void ChangeGroupDescription(string groupId, string desc, ValueCallBack<Group> callBack = null);

        public abstract void LeaveGroup(string groupId, CallBack callBack = null);

        public abstract void DestroyGroup(string groupId, CallBack callBack = null);

        public abstract void BlockGroup(string groupId, CallBack callBack = null);

        public abstract void UnblockGroup(string groupId, CallBack callBack = null);

        public abstract void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> callBack = null);

        public abstract void AddAdmin(string groupId, string memberId, ValueCallBack<Group> callBack = null);

        public abstract void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> callBack = null);

        public abstract void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> callBack = null);

        public abstract void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> callBack = null);

        public abstract void MuteAllMembers(string groupId, CallBack callBack = null);

        public abstract void UnMuteAllMembers(string groupId, CallBack callBack = null);

        public abstract void AddWhiteList(string groupId, List<string> members, ValueCallBack<Group> callBack = null);

        public abstract void RemoveWhiteList(string groupId, List<string> members, ValueCallBack<Group> callBack = null);

        public abstract void GetGroupFileListFromServer(string groupId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<GroupSharedFile>> callBack = null);

        public abstract void UploadGroupSharedFile(string groupId, string filePath, ValueCallBack<bool> callBack = null);

        public abstract void DownloadGroupSharedFile(string groupId, string fileId, string savePath, ValueCallBack<bool> callBack = null);

        public abstract void RemoveGroupSharedFile(string groupId, string fileId, ValueCallBack<Group> callBack = null);

        public abstract void UpdateGroupAnnouncement(string groupId, string announcement, ValueCallBack<Group> callBack = null);

        public abstract void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> callBack = null);

        public abstract void JoinPublicGroup(string groupId, ValueCallBack<Group> callBack = null);

        public abstract void RequestToJoinPublicGroup(string groupId, string reason = "", ValueCallBack<Group> callBack = null);

        public abstract void AcceptJoinApplication(string groupId, string username, ValueCallBack<Group> callBack = null);

        public abstract void DeclineJoinApplication(string groupId, string username, string reason = "", ValueCallBack<Group> callBack = null);

        public abstract void AcceptInvitationFromGroup(string groupId, string inviter, ValueCallBack<Group> callBack = null);

        public abstract void DeclineInvitationFromGroup(string groupId, string username, string reason = "", ValueCallBack<Group> callBack = null);

        public abstract void IgnoreGroupPush(string groupId, bool enable = true, ValueCallBack<Group> callBack = null);

        public void AddGroupManagerDelegate(IGroupManagerDelegate groupManagerDelegate)
        {
            Delegate.Add(groupManagerDelegate);
        }

        internal void ClearDelegates()
        {
            Delegate.Clear();
        }

    }

}