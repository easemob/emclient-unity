using System.Collections.Generic;
namespace ChatSDK
{
    public interface IGroupManagerDelegate
    {
        // id是[groupId], 名称是[groupName]的群邀请被[inviter]拒绝,理由是[reason]
        void OnInvitationReceived(
            string groupId, string groupName, string inviter, string reason);

        // 收到用户[applicant]申请加入id是[groupId], 名称是[groupName]的群，原因是[reason]
        void OnRequestToJoinReceived(
            string groupId, string groupName, string applicant, string reason);

        // 入群申请被同意
        void OnRequestToJoinAccepted(
            string groupId, string groupName, string accepter);

        // 入群申请被拒绝
        void OnRequestToJoinDeclined(
            string groupId, string groupName, string decliner, string reason);

        // 入群邀请被同意
        void OnInvitationAccepted(string groupId, string invitee, string reason);

        // 入群邀请被拒绝
        void OnInvitationDeclined(string groupId, string invitee, string reason);

        // 被移出群组
        void OnUserRemoved(string groupId, string groupName);

        // 群组解散
        void OnGroupDestroyed(string groupId, string groupName);

        // 自动同意加群
        void OnAutoAcceptInvitationFromGroup(
            string groupId, string inviter, string inviteMessage);

        // 群禁言列表增加
        void OnMuteListAdded(string groupId, List<string> mutes, int muteExpire);

        // 群禁言列表减少
        void OnMuteListRemoved(string groupId, List<string> mutes);

        // 群管理增加
        void OnAdminAdded(string groupId, string administrator);

        // 群管理被移除
        void OnAdminRemoved(string groupId, string administrator);

        // 群所有者变更
        void OnOwnerChanged(string groupId, string newOwner, string oldOwner);

        // 有用户加入群
        void OnMemberJoined(string groupId, string member);

        // 有用户离开群
        void OnMemberExited(string groupId, string member);

        // 群公告变更
        void OnAnnouncementChanged(string groupId, string announcement);

        // 群共享文件增加
        void OnSharedFileAdded(string groupId, GroupSharedFile sharedFile);

        // 群共享文件被删除
        void OnSharedFileDeleted(string groupId, string fileId);
    }
}
