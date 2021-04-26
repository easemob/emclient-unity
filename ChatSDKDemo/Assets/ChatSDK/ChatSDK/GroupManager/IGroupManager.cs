using System.Collections.Generic;

namespace ChatSDK
{
    public interface IGroupManager
    {
        void GetGroupWithId(string groupId, SDKValueCallBack<Group> callBack = null);

        void GetJoinedGroups(SDKValueCallBack<Group> callBack = null);

        void GetJoinedGroupsFromServer(int pageSize = 200, int pageNum = 1, SDKValueCallBack<Group> callBack = null);

        void GetPublicGroupsFromServer(int pageSize = 200, string cursor = "", SDKValueCallBack<CursorResult<Group>> callBack = null);

        void GetGroupsWithoutNotice(SDKValueCallBack<string> callBack = null);

        void CreateGroup(string groupName, GroupOptions options, string desc, List<string> inviteMembers = null, string inviteReason = "", SDKValueCallBack<CursorResult<Group>> callBack = null);

        void GetGroupSpecificationFromServer(string groupId, SDKValueCallBack<Group> callBack = null);

        void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", SDKValueCallBack<CursorResult<string>> callBack = null);

        void GetGroupBlockListFromServer(string groupId, int pageSize = 200, int pageNum = 1, SDKValueCallBack<List<string>> callBack = null);

        void GetGroupMuteListFromServer(string groupId, int pageSize = 200, int pageNum = 1, SDKValueCallBack<List<string>> callBack = null);

        void GetGroupWhiteListFromServer(string groupId, SDKValueCallBack<List<string>> callBack = null);

        void CheckIfInGroupWhiteList(SDKValueCallBack<bool> callBack = null);

        void GetGroupAnnouncementFromServer(string groupId, SDKValueCallBack<string> callBack = null);

        void AddMembers(string groupId, List<string> members, string welcome = "", SDKCallBack callBack = null);

        void RemoveMembers(string groupId, List<string> members, SDKCallBack callBack = null);

        void BlockMembers(string groupId, List<string> members, SDKCallBack callBack = null);

        void UnblockMembers(string groupId, List<string> members, SDKCallBack callBack = null);

        void ChangeGroupName(string groupId, string name, SDKValueCallBack<Group> callBack = null);

        void ChangeGroupDescription(string groupId, string desc, SDKValueCallBack<Group> callBack = null);

        void LeaveGroup(string groupId, SDKCallBack callBack = null);

        void DestroyGroup(string groupId, SDKCallBack callBack = null);

        void BlockGroup(string groupId, SDKCallBack callBack = null);

        void UnblockGroup(string groupId, SDKCallBack callBack = null);

        void ChangeGroupOwner(string groupId, string newOwner, SDKValueCallBack<Group> callBack = null);

        void AddAdmin(string groupId, string memberId, SDKValueCallBack<Group> callBack = null);

        void RemoveAdmin(string groupId, string memberId, SDKValueCallBack<Group> callBack = null);

        void MuteMembers(string groupId, List<string> members, SDKValueCallBack<Group> callBack = null);

        void UnMuteMembers(string groupId, List<string> members, SDKValueCallBack<Group> callBack = null);

        void MuteAllMembers(string groupId, SDKCallBack callBack = null);

        void UnMuteAllMembers(string groupId, SDKCallBack callBack = null);

        void AddWhiteList(string groupId, List<string> members, SDKValueCallBack<Group> callBack = null);

        void RemoveWhiteList(string groupId, List<string> members, SDKValueCallBack<Group> callBack = null);

        void GetGroupFileListFromServer(string groupId, int pageSize = 200, int pageNum = 1, SDKValueCallBack<List<GroupSharedFile>> callBack = null);

        void UploadGroupSharedFile(string groupId, string filePath, SDKValueCallBack<bool> callBack = null);

        void DownloadGroupSharedFile(string groupId, string fileId, string savePath, SDKValueCallBack<bool> callBack = null);

        void RemoveGroupSharedFile(string groupId, string fileId, SDKValueCallBack<Group> callBack = null);

        void UpdateGroupAnnouncement(string groupId, string announcement, SDKValueCallBack<Group> callBack = null);

        void UpdateGroupExt(string groupId, string ext, SDKValueCallBack<Group> callBack = null);

        void JoinPublicGroup(string groupId, SDKValueCallBack<Group> callBack = null);

        void RequestToJoinPublicGroup(string groupId, string reason = "", SDKValueCallBack<Group> callBack = null);

        void AcceptJoinApplication(string groupId, string username, SDKValueCallBack<Group> callBack = null);

        void DeclineJoinApplication(string groupId, string username, string reason = "", SDKValueCallBack<Group> callBack = null);

        void AcceptInvitationFromGroup(string groupId, string inviter, SDKValueCallBack<Group> callBack = null);

        void DeclineInvitationFromGroup(string groupId, string username, string reason = "", SDKValueCallBack<Group> callBack = null);

        void IgnoreGroupPush(string groupId, bool enable = true, SDKValueCallBack<Group> callBack = null);

    }

}