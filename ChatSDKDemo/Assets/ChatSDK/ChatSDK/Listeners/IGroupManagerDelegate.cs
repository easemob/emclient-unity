using System.Collections.Generic;
namespace ChatSDK
{
    public interface IGroupManagerDelegate
    {
        /// <summary>
        /// 收到群组邀请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="inviter">邀请人id</param>
        /// <param name="reason">邀请原因</param>
        void OnInvitationReceivedFromGroup(
            string groupId, string groupName, string inviter, string reason);

        /// <summary>
        /// 收到加群申请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="applicant">申请人id</param>
        /// <param name="reason">申请原因</param>
        void OnRequestToJoinReceivedFromGroup(
            string groupId, string groupName, string applicant, string reason);

        /// <summary>
        /// 入群申请被同意
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="accepter">操作人</param>
        void OnRequestToJoinAcceptedFromGroup(
            string groupId, string groupName, string accepter);

        /// <summary>
        /// 入群申请被拒绝
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="groupName">群组名称</param>
        /// <param name="accepter">操作人</param>
        /// <param name="reason">拒绝原因</param>
        void OnRequestToJoinDeclinedFromGroup(
            string groupId, string groupName, string decliner, string reason);

        /// <summary>
        /// 加群邀请被同意
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="invitee">被邀请人</param>
        /// <param name="reason">原因</param>
        void OnInvitationAcceptedFromGroup(string groupId, string invitee, string reason);

        /// <summary>
        /// 加群邀请被拒绝
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="invitee">被邀请人</param>
        /// <param name="reason">原因</param>
        void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason);

        /// <summary>
        /// 被移除群组
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="groupName">群组名称</param>
        void OnUserRemovedFromGroup(string groupId, string groupName);

        /// <summary>
        /// 群组解散
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="groupName">群组名称</param>
        void OnDestroyedFromGroup(string groupId, string groupName);

        /// <summary>
        /// 自动同意加群申请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="invitee">邀请人</param>
        /// <param name="inviteMessage">邀请信息</param>
        void OnAutoAcceptInvitationFromGroup(
            string groupId, string inviter, string inviteMessage);

        /// <summary>
        /// 群禁言列表增加
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="mutes">被禁言用户id</param>
        /// <param name="muteExpire">禁言时长(暂不支持)</param>
        void OnMuteListAddedFromGroup(string groupId, List<string> mutes, int muteExpire);

        /// <summary>
        /// 群禁言列表解除
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="mutes">被解除禁言用户id</param>
        void OnMuteListRemovedFromGroup(string groupId, List<string> mutes);

        /// <summary>
        /// 增加管理员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="administrator">新添加的管理员id</param>
        void OnAdminAddedFromGroup(string groupId, string administrator);

        /// <summary>
        /// 移除管理员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="administrator">新添加的管理员id</param>
        void OnAdminRemovedFromGroup(string groupId, string administrator);

        /// <summary>
        /// 群组被转移，只有被转移人才能收到该回调
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="newOwner">新群主</param>
        /// <param name="oldOwner">老群主</param>
        void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner);

        /// <summary>
        /// 用户加入群组
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="member">用户id</param>
        void OnMemberJoinedFromGroup(string groupId, string member);

        /// <summary>
        /// 用户离开群组
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="member">用户id</param>
        void OnMemberExitedFromGroup(string groupId, string member);

        /// <summary>
        /// 群公告变更
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="announcement">新公告</param>
        void OnAnnouncementChangedFromGroup(string groupId, string announcement);

        /// <summary>
        /// 群共享文件增加
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="sharedFile">新增文件</param>
        void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile);

        /// <summary>
        /// 群共享文件被删除
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="fileId">移除文件id</param>
        void OnSharedFileDeletedFromGroup(string groupId, string fileId);
    }
}
