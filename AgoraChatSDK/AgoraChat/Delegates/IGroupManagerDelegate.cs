using System.Collections.Generic;

namespace AgoraChat
{
    /**
         * \~chinese
	     * 群组管理器回调接口。
		 * 
         * \~english
	     * The group manager callback interface.
		 *
	     */
    public interface IGroupManagerDelegate
    {
        /**
         * \~chinese
	     * 用户收到入群邀请的回调。
		 * 
	     * @param groupId 	群组 ID。
	     * @param groupName 群组名称。
	     * @param inviter 	邀请人 ID。
	     * @param reason 	邀请原因。
         *
         * \~english
	     * Occurs when the user receives a group invitation.
		 * 
	     * @param groupId		The group ID.
	     * @param groupName		The group name.
	     * @param inviter		The user ID of the inviter.
	     * @param reason		The reason for invitation.
	     */
        void OnInvitationReceivedFromGroup(
            string groupId, string groupName, string inviter, string reason);

        /**
         * \~chinese
	     * 用户申请入群回调。
		 * 
	     * @param groupId 	群组 ID。
	     * @param groupName 群组名称。
	     * @param applicant 申请人 ID。
	     * @param reason 	申请加入原因。
         *
         * \~english
         * Occurs when the group owner or admin receives a join request from a user.
	     *
	     * @param groupId		The group ID.
	     * @param groupName		The group name.
	     * @param applicant		The ID of the user requesting to join the group.
	     * @param reason		The reason for requesting to join the group.
	     */
        void OnRequestToJoinReceivedFromGroup(
            string groupId, string groupName, string applicant, string reason);

        /**
         * \~chinese
	     * 接受入群申请回调。
		 * 
	     * @param groupId	群组 ID。
	     * @param groupName 群组名称。
	     * @param accepter 	接受人 ID.
         *
         * \~english
	     * Occurs when a join request is accepted.
	     *
	     * @param groupId 		The group ID.
	     * @param groupName 	The group name.
	     * @param accepter 		The ID of the user that accepts the join request.
	     */
        void OnRequestToJoinAcceptedFromGroup(
            string groupId, string groupName, string accepter);

        /**
         * \~chinese
	     * 拒绝入群申请回调。
		 * 
	     * @param groupId 	群组 ID。
	     * @param reason 	拒绝理由。
	     * @param decliner  拒绝人的用户 ID。
	     * @param applicant 申请人的用户 ID。
         *
         * \~english
         * Occurs when a join request is declined.
	     *
	     * @param groupId 		The group ID.
	     * @param reason 		The reason for declining the join request.
	     * @param decliner 		The user that declines the request.
	     * @param applicant 	The user ID of the applicant.
	     */
        void OnRequestToJoinDeclinedFromGroup(
            string groupId, string reason, string decliner, string applicant);

        /**
         * \~chinese
	     * 接受入群邀请回调。
		 * 
	     * @param groupId		群组 ID。
	     * @param invitee		受邀人 ID。
         *
         * \~english
         * Occurs when a group invitation is accepted.
	     *
	     * @param groupId 		The group ID.
	     * @param invitee 		The user ID of the invitee.
	     */
        void OnInvitationAcceptedFromGroup(string groupId, string invitee);

        /**
         * \~chinese
	     * 拒绝群组邀请回调。
		 * 
	     * @param groupId		群组 ID。
	     * @param invitee		受邀人 ID。
	     * @param reason 		拒绝理由。
         *
         * \~english
         * Occurs when a group invitation is declined.
	     *
	     * @param groupId 		The group ID.
	     * @param invitee		The user ID of the invitee.
	     * @param reason 		The reason for declining the invitation.
	     */
        void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason);

        /**
         * \~chinese
	     * 当前登录用户被群主或群组管理员移出群组时的回调。
		 * 
	     * @param groupId 		群组 ID。
	     * @param groupName		群组名称。
         *
         * \~english
         * Occurs when the current user is removed from the group by the group owner or group admin.
	     *
	     * @param groupId 		The group ID.
	     * @param groupName		The group name.
	     */
        void OnUserRemovedFromGroup(string groupId, string groupName);

        /**
         * \~chinese
         * 群组解散回调。
		 * 
         * SDK 会先删除本地的这个群组，然后通过此回调通知应用此群组被删除。
		 * 
         * @param groupId 	群组 ID。
         * @param groupName 群组名称。
         *
         * \~english
         * Occurs when a group is destroyed.
		 * 
         * The SDK will remove the group from the local database and local memory before notifying the app of the group removal via the callback method.
	     *
	     * @param groupId		The group ID.
	     * @param groupName 	The group name.
         */
        void OnDestroyedFromGroup(string groupId, string groupName);

        /**
         * \~chinese
         * 自动同意入群申请回调。
		 * 
         * SDK 会先将用户加入群组，并通过此回调通知应用。
		 * 
         * 具体设置，详见 {@link com.hyphenate.chat.EMOptions#setAutoAcceptGroupInvitation(boolean value)}。
         * 
         * @param groupId			群组 ID。
         * @param inviter			邀请者 ID。
         * @param inviteMessage		邀请信息。
         *
         * \~english
         * Occurs when the group invitation is accepted automatically.
		 * 
	     * The SDK will add the user to the group before notifying the app of the acceptance of the group invitation via the callback method.
         * 
		 * For settings, see {@link com.hyphenate.chat.EMOptions#setAutoAcceptGroupInvitation(boolean value)}.
	     *
	     * @param groupId			The group ID.
	     * @param inviter			The user ID of the inviter.
	     * @param inviteMessage		The invitation message.
         */
        void OnAutoAcceptInvitationFromGroup(
            string groupId, string inviter, string inviteMessage);

        /**
	     * \~chinese
	     * 有成员被禁言。
		 * 
	     * **注意**
		 * 
		 * 用户禁言后，将无法在群中发送消息，但可查看群组中的消息，而黑名单中的用户无法查看和发送群组消息。
	     *
	     * @param groupId 	 群组 ID。
	     * @param mutes 	 禁言的成员列表。
	     * @param muteExpire 禁言时长，单位为毫秒。
	     *
	     * \~english
	     * Occurs when one or more group members are muted.
		 * 
	     * **Note**
		 * 
		 *  A user, when muted, can still see group messages, but cannot send messages in the group. However, a user on the block list can neither see nor send group messages.
	     *
	     * @param groupId		The group ID.
	     * @param mutes 		The member(s) added to the mute list.
	     * @param muteExpire    The mute duration in milliseconds.
	     */
        void OnMuteListAddedFromGroup(string groupId, List<string> mutes, long muteExpire);

        /**
	     * \~chinese
	     * 有成员被解除禁言。
		 * 
	     * **注意**
		 * 
		 * 用户禁言后，将无法在群中发送消息，但可查看群组中的消息，而黑名单中的用户无法查看和发送群组消息。
	     *
	     * @param groupId 	群组 ID。
	     * @param mutes 	有成员从群组禁言列表中移除。
	     *
	     * \~english
	     * Occurs when one or more group members are unmuted.
		 * 
	     * **Note**
		 * 
		 * A user, when muted, can still see group messages, but cannot send messages in the group. However, a user on the block list can neither see nor send group messages.
	     *
	     * @param groupId		The group ID.
	     * @param mutes 		The member(s) removed from the mute list.
	     */
        void OnMuteListRemovedFromGroup(string groupId, List<string> mutes);

        /**
	     * \~chinese
	     * 成员设置为管理员的回调。
	     *
	     * @param groupId 		群组 ID。
	     * @param administrator 设置为管理员的成员。
	     *
	     * \~english
	     * Occurs when a member is set as an admin.
	     *
	     * @param groupId		The group ID.
	     * @param administrator The user ID of the member that is set as an admin.
	     */
        void OnAdminAddedFromGroup(string groupId, string administrator);

        /**
		 * \~chinese
		 * 取消成员的管理员权限的回调。
		 * 
		 * @param groupId 		群组 ID。
		 * @param administrator 被取消管理员权限的成员。
		 *
		 * \~english
		 * Occurs when the admin privileges of a member are removed..
		 *
		 * @param groupId 		The group ID.
		 * @param administrator The user ID of the member whose admin privileges are removed.
		 */
        void OnAdminRemovedFromGroup(string groupId, string administrator);

        /**
		 * \~chinese
		 * 转移群主权限的回调。
		 * 
		 * @param groupId 	群组 ID。
		 * @param newOwner 	新群主。
		 * @param oldOwner 	原群主。
		 *
		 * \~english
		 * Occurs when the group ownership is transferred.
		 * 
		 * @param groupId 		The group ID.
		 * @param newOwner 		The new group owner.
		 * @param oldOwner 		The previous group owner.
		 */
        void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner);

        /**
		 * \~chinese
		 * 新成员加入群组的回调。
		 * 
		 * @param groupId 	群组 ID。
		 * @param member 	新成员 ID。
		 *
		 * \~english
		 * Occurs when a user joins a group.
		 * 
		 * @param groupId  The group ID.
		 * @param member   The ID of the new member.
		 */
        void OnMemberJoinedFromGroup(string groupId, string member);

        /**
		 * \~chinese
		 * 群组成员主动退出回调。
		 * 
		 * @param groupId 	群组 ID。
		 * @param member 	退群的成员 ID。
		 * 
		 * \~english
		 * Occurs when a member voluntarily leaves the group.
		 * 
		 * @param groupId   The group ID.
		 * @param member  	The user ID of the member who has left the group.
		 */
        void OnMemberExitedFromGroup(string groupId, string member);

        /**
		 * \~chinese
		 * 群公告更新回调。
		 * 
		 * @param groupId      群组 ID。
		 * @param announcement 更新后的公告内容。
		 *
		 * \~english
		 * Occurs when the group announcement is updated.
		 * 
		 * @param groupId  The group ID.
		 * @param announcement  The updated announcement content.
		 */
        void OnAnnouncementChangedFromGroup(string groupId, string announcement);

        /**
		 * \~chinese
		 * 群组添加共享文件回调。
		 * 
		 * @param groupId    群组 ID。
		 * @param sharedFile 添加的共享文件。
		 *
		 * \~english
		 * Occurs when a shared file is added to a group.
		 * 
		 * @param groupId The group ID.
		 * @param sharedFile The new shared file.
		 */
        void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile);

        /**
		 * \~chinese
		 * 群组删除共享文件回调。
		 * 
		 * @param groupId 群组 ID。
		 * @param fileId  删除的共享文件的 ID。
		 *
		 * \~english
		 * Occurs when a shared file is removed from a group.
		 * 
		 * @param groupId The group ID.
		 * @param fileId  The ID of the removed shared file.
		 */
        void OnSharedFileDeletedFromGroup(string groupId, string fileId);

        /**
		 * \~chinese
		 * 有成员被加入群组白名单的回调。
		 *
		 * @param groupId       群组 ID。
		 * @param allowList     要添加的成员列表。
		 *
		 * \~english
		 * Occurs when one or more group members are added to the group allow list.
		 *
		 * @param groupId       The group ID.
		 * @param allowList     The member(s) added to the group allow list.
		 */
        void OnAddAllowListMembersFromGroup(string groupId, List<string> allowList);

        /**
		 * \~chinese
		 * 有成员被移出群组白名单回调。
		 *
		 * @param groupId       群组 ID。
		 * @param allowList     从白名单中移除的成员列表。
		 *
		 * \~english
		 * Occurs when one or more members are removed from the group allow list.
		 *
		 * @param groupId       The group ID.
		 * @param allowList     Member(s) removed from the group allow list.
		 */
        void OnRemoveAllowListMembersFromGroup(string groupId, List<string> allowList);

        /**
		 * \~chinese
		 * 全员禁言状态变化回调。
		 *
		 * @param groupId       群组 ID。
		 * @param isAllMuted    是否开启了全员禁言。
		 * - `true`：是；
		 * - `false`：否。
		 *
		 * \~english
		 * Occurs when all group members are muted or unmuted.
		 *
		 * @param groupId       The group ID.
		 * @param isAllMuted    Whether all group members are muted or unmuted. 
		 * - `true`: All group members are muted; 
		 * - `false`: All group members are unmuted.
		 */

        void OnAllMemberMuteChangedFromGroup(string groupId, bool isAllMuted);


        /**
		 *  \~chinese
		 *  群组禁用状态发生变化回调。
		 *
		 *  @param groupId           群组 ID。
		 *  @param isDisable           是否禁用。
		 *
		 *  \~english
		 *  Occurs when the disabled state of group changes.
		 *
		 *  @param groupId          The group ID.
		 *  @param isDisable        Whether the group is disabled.
		 */
        void OnStateChangedFromGroup(string groupId, bool isDisable);

        /**
		 *  \~chinese
		 *  群详情发生了变更。
		 *
		 * 需调用获取群详情方法 {@link GetGroupSpecificationFromServer(string, ValueCallBack<Group>)} 从服务器获得最新的群组详情。
		 *
		 *  @param group       群组实例。
		 *
		 *  \~english
		 *  Occurs when the group specification(s) is/are changed.
		 *
		 *  Once the group specifications are changed, you need to call {@link GetGroupSpecificationFromServer(string, ValueCallBack<Group>)} to get the latest group information from the server.
		 *
		 *  @param group      The group instance.
		 */
        void OnSpecificationChangedFromGroup(Group group);

        /**
		 *  \~chinese
		 *  群成员自定义属性发生了变更。
		 *
		 *  @param groupId           群组 ID。
         *  @param userId            自定义属性变更的群成员的用户 ID。
         *  @param attributes        修改后的自定义属性。
         *  @param from              操作者的用户 ID。
		 *
		 *  \~english
		 *  Occurs when a custom attribute(s) of a group member is/are changed.
		 *
         *  @param groupId           The group ID.
         *  @param userId            The user ID of the group member whose custom attribute(s) is/are changed.
         *  @param attributes        The modified custom attribute(s).
         *  @param from              The user ID of the operator.
		 */
        void OnUpdateMemberAttributesFromGroup(string groupId, string userId, Dictionary<string, string> attributes, string from);

    }
}