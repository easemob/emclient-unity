﻿using System.Collections.Generic;

namespace ChatSDK
{
	 /**
	     * \~chinese
	     * 群组管理器抽象类。
	     *
	     * \~english
	     * The abstract class for the group manager.
	     *
		 */
    public abstract class IGroupManager
    {
        /**
	     * \~chinese
	     * 申请加入群组。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param reason    申请加入的原因。
	     * @param handle    申请结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Requests to join a group.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId	The group ID.
	     * @param reason    The reason for requesting to joining the group.
	     * @param handle    The callback of application. See {@link CallBack}.
	     */
        public abstract void applyJoinToGroup(string groupId, string reason = "", CallBack handle = null);

        /**
	     * \~chinese
	     * 接受入群邀请。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param handle    接受结果回调，返回用户同意邀请的群组，详见 {@link ValueCallBack}。
	     *
	     * \~english
	     * Accepts a group invitation.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param handle    The callback of acceptance. Returns the group instance which the user has accepted the invitation to join. See {@link ValueCallBack}.
	     */
        public abstract void AcceptGroupInvitation(string groupId, ValueCallBack<Group> handle = null);

        /**
	     * \~chinese
	     * 批准入群申请。
		 * 
         * 仅群主和管理员可调用此方法。 
	     *
	     * 异步方法。
	     *
	     * @param groupId  	群组 ID。
	     * @param username 	申请人 ID。
	     * @param handle    批准结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Approves a group request.
		 * 
         * Only the group owner or admin can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId  	The group ID.
	     * @param username 	The ID of the user who sends the request to join the group.
	     * @param handle    The callback of approval. See {@link CallBack}.
	     */
        public abstract void AcceptGroupJoinApplication(string groupId, string username, CallBack handle = null);

        /**
	     * \~chinese
	     * 添加群组管理员。
		 *
	     * 仅群主可调用此方法，admin 无权限。
	     *
	     * 异步方法。
         *
	     * @param groupId	群组 ID。
	     * @param memberId  新增加的管理员 ID。
	     * @param handle    添加结果回调，详见 {@link CallBack}。
	     *
	     * \~english
         * Adds a group admin.
	     * Only the group owner, not the group admin, can call this method.
	     *
	     * This is an asynchronous method.
         *
	     * @param groupId	The group ID.
	     * @param memberId	The new admin ID.
	     * @param handle    The callback of addition. See {@link CallBack}.
	     */
        public abstract void AddGroupAdmin(string groupId, string memberId, CallBack handle = null);

        /**
	     * \~chinese
	     * 向群组中添加新成员。

	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param groupId       群组的 ID。
	     * @param members       要添加的新成员列表。
	     * @param handle        添加结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Adds users to the group.
		 * 
	     * Only the group owner or admin can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId		The group ID.
	     * @param newmembers	The list of new members to add.
	     * @param handle        The operation callback. See {@link CallBack}.
	     */
        public abstract void AddGroupMembers(string groupId, List<string> members, CallBack handle = null);

        /**
	     * \~chinese
	     * 添加白名单。

	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。 
	     *
	     * @param groupId 	群组 ID。
	     * @param members 	要添加的成员列表。
	     * @param handle    添加结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Adds members to the allow list.
		 * 
	     * Only the group owner or admin can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param members 	The members to be added to the allow list.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void AddGroupWhiteList(string groupId, List<string> members, CallBack handle = null);

        /**
	     * \~chinese
	     * 屏蔽群消息。
		 * 
	     * 屏蔽群消息的用户仍是群成员，但无法接收群消息。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param handle    屏蔽结果回调，详见 {@link CallBack}。
	     *
	     *\~english
	     * Blocks group messages.
		 * 
         * The user that blocks group messages is still a group member. They just cannot receive group messages.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId	The group ID.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void BlockGroup(string groupId, CallBack handle = null);

        /**
	     * \~chinese
         * 将用户加入群组黑名单。
		 * 
	     * 成功调用该方法后，该用户会先被移除出群组，然后加入群组黑名单。该用户无法接收、发送群消息，也无法申请再次加入群组。
		 * 
	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param groupId 	群组ID。
	     * @param members 	要加入黑名单的用户列表。
	     * @param handle    操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
         * Adds the user to the group block list.
		 * 
	     * After this method call succeeds, the user is first removed from the chat group, and then added to the group block list. Users on the chat group block list cannot send or receive group messages, nor can they re-join the chat group.
	     * 
		 * Only the group owner or admin can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId	The group ID.
	     * @param members 	The users to be added to the block list.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void BlockGroupMembers(string groupId, List<string> members, CallBack handle = null);

        /**
	     * \~chinese
	     * 修改群描述。
		 * 
	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param groupId 	群组 ID。
	     * @param desc 	    修改后的群描述。
         * @param handle    操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Changes the group description.
		 * 
	     * Only the group owner or admin can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param desc 	    The new group description.
         * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void ChangeGroupDescription(string groupId, string desc, CallBack handle = null);

        /**
	     * \~chinese
	     * 修改群组名称。
		 * 
	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param groupId 	需修改名称的群组的 ID。
	     * @param name 	    修改后的群组名称。
	     * @param handle    操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Changes the group name.
		 * 
	     * Only the group owner or admin can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The ID of group whose name is to be changed.
	     * @param name 	    The new group name.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void ChangeGroupName(string groupId, string name, CallBack handle = null);

        /**
	     * \~chinese
	     * 转让群组所有权。
		 * 
	     * 仅群主可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param newOwner	新的群主。
	     * @param handle    操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Transfers the group ownership.
		 * 
	     * Only the group owner can call this method.
	     *
	     * This is an asynchronous method.
         *
	     * @param groupId	The group ID.
	     * @param newOwner	The user ID of the new owner.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void ChangeGroupOwner(string groupId, string newOwner, CallBack handle = null);

        /**
	     * \~chinese
	     * 检查当前用户是否在群组白名单中。
	     *
	     * 异步方法。
	     *
	     * @param groupId 	群组 ID。
	     * @param handle    操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Gets whether the current user is on the allow list of the group.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null);

        /**
	     * \~chinese
	     * 创建群组。
		 * 
         * 群组创建成功后，会更新内存及数据库中的数据，多端多设备会收到相应的通知事件，然后将群组更新到内存及数据库中。
		 * 
	     * 可通过设置 {@link IMultiDeviceDelegate} 监听相关事件，事件回调函数为 {@link onGroupMultiDevicesEvent((MultiDevicesOperation, string, List<string>)}。
	     *
	     * 异步方法。
	     *
	     * @param groupName     群组名称。该参数可选，不设置传 `null`。
	     * @param options       群组创建时需设置的选项。该参数可选，不可为 `null`。详见 {@link GroupOptions}。
	     *                      群组的其他选项如下：
	     *                      - 群组最大成员数，默认值为 200；
	     *                      - 群组类型，详见 {@link GroupStyle}；
	     *                      - 邀请入群是否需要对方同意，默认为 `false`，即邀请后直接入群；
	     *                      - 群详情扩展。
         * @param desc          群组描述。该参数可选，不设置传 `null`。
	     * @param inviteMembers 群成员列表。该参数不可为 `null`。
         * @param inviteReason  成员入群的邀请信息。该参数可选，不设置传 `null`。
         * @param handle        创建结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Creates a group instance.
		 * 
         * After the group is created, the data in the memory and database will be updated and multiple devices will receive the notification event and update the group to the memory and database. 
		 * 
		 * You can set {@link IMultiDeviceDelegate} to listen for the event. 
		 * 
		 * If an event occurs, the callback function {@link onGroupMultiDevicesEvent((MultiDevicesOperation, string, List<string>)} will be triggered.
	     * 
	     * This is an asynchronous method.
	     *
	     * @param groupName     The group name. It is optional. Pass `null` if you do not want to set this parameter.
	     * @param options       The options for creating a group. They are optional and cannot be `null`. See {@link GroupOptions}.
	     *                      The options are as follows:
	     *                      - The maximum number of members allowed in the group. The default value is 200.
	     *                      - The group style. See {@link GroupStyle}. 
	     *                      - Whether to ask for permission when inviting a user to join the group. The default value is `false`, indicating that invitees are automaticall added to the group without their permission.
	     *                      - The extention of group details.* 
         * @param desc          The group description. It is optional. Pass `null` if you do not want to set this parameter.
	     * @param inviteMembers The group member array. The group owner ID is optional. This parameter cannot be `null`.
	     * @param inviteReason  The group joining invitation. It is optional. Pass `null` if you do not want to set this parameter.
         * @param handle        The operation callback. See {@link CallBack}.
         */
        public abstract void CreateGroup(string groupName, GroupOptions options, string desc = null, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null);

        /**
	     * \~chinese
	     * 拒绝入群邀请。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param reason	拒绝理由。
	     * @param handle    操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Declines a group invitation.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param reason	The reason for declining the group invitation.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */
        public abstract void DeclineGroupInvitation(string groupId, string reason = null, CallBack handle = null);

		/**
		 * \~chinese
		 * 拒绝入群申请。
		 * 
		 * 仅群组创建者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId  	群组 ID。
		 * @param username 	申请人的用户 ID。
		 * @param reason   	拒绝理由。
		 * @param handle    操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Declines a group request.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId  	The group ID.
		 * @param username 	The ID of the user who sends the request to join the group.
		 * @param reason   	The reason for declining the group request.
		 * @param handle    The operation callback. See {@link CallBack}.
		 */
		public abstract void DeclineGroupJoinApplication(string groupId, string username, string reason = null, CallBack handle = null);

		/**
		 * \~chinese
		 * 解散群组。
		 * 
		 * 仅群主可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param handle    操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Destroys the group instance.
		 * 
		 * Only the group owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param handle    The operation callback. See {@link CallBack}.
		 */
		public abstract void DestroyGroup(string groupId, CallBack handle = null);

		/**
		 * \~chinese
		 * 下载群组中指定的共享文件。
		 *
		 * 异步方法。
		 *
		 * @param groupId   群组 ID。
		 * @param fileId    共享文件 ID。
		 * @param savePath  文件保存路径。
		 * @param handle    操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Downloads the shared file of the group.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId   The group ID.
		 * @param fileId    The ID of the shared file.
		 * @param savePath  The path to save the downloaded file.
		 * @param handle    The operation callback. See {@link CallBack}.
		 */
		public abstract void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null);

		/**
		 * \~chinese
		 * 从服务器获取群公告。
		 * 
		 * 群成员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param handle    操作结果回调，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets the group announcement from the server.
		 * Group members can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param handle    The operation callback. See {@link ValueCallBack}.
		 */
		public abstract void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null);

		/**
		 * \~chinese
		 * 以分页方式获取群组的黑名单。
		 * 
		 * 仅群主和管理员可调用此方法。		 
		 *
		 * 异步方法。
		 *
		 * @param groupId 				群组 ID。
		 * @param pageNum 				当前页码，从 1 开始。
		 * @param pageSize 				每页期望返回的黑名单成员数量。
		 * @param handle				操作结果回调，返回黑名单列表或错误信息，详见 {@link ValueCallBack}。
		 * 
		 * \~english
		 * Gets the group block list from the server with pagination.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId               The group ID.
		 * @param pageNum            	The page number, starting from 1.
		 * @param pageSize              The number of members on the block list that you expect to get on each page.
		 * @param handle				The operation callback. If success, the obtained block list will be returned; otherwise, an error will be returned. See {@link ValueCallBack}.
		 */		
		public abstract void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

		/**
		 * \~chinese
		 * 从服务器获取群共享文件列表。
         * 
		 * 若数据量未知且很大，可分页获取，服务器会根据每次传入的 `pageSize` 和 `pageNum` 的值返回数据。 
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param pageNum 	当前页码，从 1 开始。
		 * @param pageSize 	每页期望返回的共享文件数。
		 *                  查询最后一页时，返回的数量小于 `pageSize` 的值。                                 
		 * @param handle	操作结果回调，成功返回共享文件列表，失败则返回错误信息，详见 {@link ValueCallBack}。
		
		 *    
		 * \~english
		 * Gets the shared files of the group from the server.
         *
         * For a large but unknown quantity of data, the server will return data with pagination as specified by `pageSize` and `pageNum`.
		 * 
		 * This is an asynchronous method.
		 *
		 * @param groupId 	The group ID.
		 * @param pageNum 	The page number, starting from 1.
		 *                  For the last page, the actual number of returned shared files is less than the value of `pageSize`.     
		 * @param pageSize 	The number of shared files that you expect to get on each page.
		 * @param handle	The operation callback. If success, the SDK returns the list of shared files; otherwise, an error will be returned. See {@link ValueCallBack}.
		 *
		 */
		public abstract void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null);

		/**
		 * \~chinese
		 * 从服务器获取群成员列表。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param pageSize 	每页期望返回的群成员数。
		 * @param cursor	从该游标位置开始获取数据，首次获取数据时传 `null` 会从最新一条数据开始获取。
		 * @param handle	操作结果回调，成功返回成员列表及用于下次获取数据的cursor，失败返回错误信息，详见 {@link ValueCallBack}。
		 *    
		 * \~english
		 * Gets the group member list from the server.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId 	The group ID.
		 * @param pageSize 	The number of members that you expect to get on each page.
		   @param cursor    The position from which to start getting data. At the first method call, if you set `cursor` as `null`, the SDK gets the data in the reverse chronological order of when users joined the group.
		 * @param handle	The operation callback. If success, the SDK returns the obtained group member list and the cursor for the next query; otherwise, an error will be returned. See {@link ValueCallBack}.
		 *
		 */
		public abstract void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> handle = null);

		/**
		 * \~chinese
		 * 获取群组的禁言列表。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param pageNum	当前页码，从 1 开始。
		 * @param pageSize	每页期望返回的禁言成员数。
		 * @param handle	操作结果回调，成功返回禁言列表，失败返回错误信息，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets the mute list of the group from the server.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param pageNum	The page number, starting from 1.
		 * @param pageSize	The number of muted members that you expect to get on each page.
		 * @param handle	The operation callback. If success, the SDK returns the obtained mute list; otherwise, an error will be returned. See {@link ValueCallBack}.
		 *
		 */
		public abstract void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

		/**
		 * \~chinese
		 * 获取群详情。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param handle	操作结果回调，成功返群组实例，失败返回错误信息，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets group details.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param handle	The operation callback. If success, the SDK returns the group instance; otherwise, an error will be returned. See {@link ValueCallBack}.
		 *
		 */
		public abstract void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null);

		/**
		 * \~chinese
		 * 获取群组白名单列表。
		 * 
		 * 仅聊天室创建者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param handle	操作结果回调，成功返回白名单列表，失败返回错误信息，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets the allow list of the group from the server.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId 	The group ID.
		 * @param handle	The operation callback. If success, the SDK returns the obtained allow list; otherwise, an error will be returned. See {@link ValueCallBack}.
		 */
		public abstract void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null);

		/**
		 * \~chinese
		 * 根据群组 ID，从内存中获得群组对象。
		 *
		 * @param groupId	群组 ID。
		 * @return 			返回群组对象。如果群组不存在，返回 `null`。
		 *
		 * \~english
		 * Gets the group instance from the memory by group ID.
		 *
		 * @param groupId	The group ID.
		 * @return 			The group instance. Returns `null` if the group does not exist.
		 */
		public abstract Group GetGroupWithId(string groupId);

		/**
		 * \~chinese
		 * 从本地内存和数据库获取加入的群组列表。
		 *
		 * 异步方法。
		 * 
		 * @return 			返回群组列表。
		 *
		 * \~english
		 * Gets the list of groups that the user has joined.
		 * 
		 * This method gets the groups from the local memory and database.
		 * 
		 * This is an asynchronous method.
		 *
		 * @return 			The group list. 
		 */
		public abstract List<Group> GetJoinedGroups();

		/**
		 * \~chinese
		 * 以分页方式从服务器获取当前用户加入的群组。
		 * 
		 * 此操作只返回群组列表，不包含群组的所有成员信息。
		 *
		 * 异步方法，会阻塞当前线程。
		 *
		 * @param pageNum 		当前页码，从 1 开始。
		 * @param pageSize		每页期望返回的群组数。
		 * @param handle		操作结果回调，成功群组列表，失败返回错误信息，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets the list of groups with pagination.
		 * 
		 * This method gets a group list from the server, which does not contain member information. 
		 * 
		 * This is an asynchronous method and blocks the current thread.
		 *
		 * @param pageNum 		The page number, starting from 1.
		 * @param pageSize		The number of groups that you expect to get on each page.
		 * @param handle		The operation callback. If success, the SDK returns the obtained group list; otherwise, an error will be returned. See {@link ValueCallBack}. 
		 */
		public abstract void FetchJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<List<Group>> handle = null);

		/**
		 * \~chinese
		 * 以分页方式从服务器获取公开群组。
		 *
		 * 异步方法。
		 *
		 * @param pageSize		每页期望返回的公开群组数量。
		 * @param cursor		从该游标位置开始取数据，首次获取数据时传 `null` 从最新数据开始获取。
		 * @param handle		操作结果回调，成功返回获取的成员列表及用于下次获取数据的cursor，失败则返回错误信息，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets public groups from the server with pagination.
		 *
		 * This is an asynchronous method.
		 *
		 * @param pageSize  	The number of public groups that you expect to get on each page.
		 * @param cursor    	The position from which to start getting data. During the first call to this method, if `null` is passed to `cursor`, the SDK will get data in the reverse chronological order.
		 * @param handle		The operation callback. If success, the SDK returns the obtained public group list and the cursor used for the next query; otherwise, an error will be returned. See {@link ValueCallBack}.
		 */
		public abstract void FetchPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<GroupInfo>> handle = null);

		/**
		 * \~chinese
		 * 加入公开群组。
		 *
		 * 异步方法。
		 *
		 * @param groupId		公开群组 ID。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Joins a public group.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	  	The ID of the public group.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */
		public abstract void JoinPublicGroup(string groupId, CallBack handle = null);

		/**
		 * \~chinese
		 * 当前登录用户退出群组。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param handle    操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Leaves a group.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param handle    The operation callback. See {@link CallBack}.
		 */
		public abstract void LeaveGroup(string groupId, CallBack handle = null);

		/**
		 * \~chinese
		 * 对群组成员全部禁言。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId		群组 ID。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Mutes all members in the group.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId		The group ID.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */
		public abstract void MuteGroupAllMembers(string groupId, CallBack handle = null);

		/**
		 * \~chinese
		 * 将多个成员禁言。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId		群组 ID。
		 * @param members		要禁言的用户列表。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Mutes the specified group members.

		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId		The group ID.
		 * @param members 		The list of members to be muted.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */
		public abstract void MuteGroupMembers(string groupId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 删除群组管理员。
		 * 
		 * 仅群主可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param memberId	删除的管理员 ID。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes a group admin.
		 * Only the group owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param memberId	The ID of the admin to remove.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */		
		public abstract void RemoveGroupAdmin(string groupId, string memberId, CallBack handle = null);

		/**
		 * \~chinese
		 * 删除群组指定的共享文件。
		 * 
		 * 群组成员可以删除自己上传的文件，群主或者群组管理员可以删除所有的共享文件。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param fileId 	共享文件 ID。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes the shared file of the group.
		 * 
		 * Group members can only delete the shared files uploaded by themselves. The group owner and admins can delete all the shared files.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId 	The group ID.
		 * @param fileId 	The ID of the shared file.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */		
		public abstract void DeleteGroupSharedFile(string groupId, string fileId, CallBack handle = null);

		/**
		 * \~chinese
		 * 从群组中删除成员。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId   群组 ID。
		 * @param members   要删除的成员。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes members from the group.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param members   The members to remove.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void DeleteGroupMembers(string groupId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 将成员移除群组白名单。
		 * 
		 * 仅群组创建者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param members 	需从白名单中移除的成员列表。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes members from the group allow list.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId 	The group ID.
		 * @param members 	The list of members to be removed from the group allow list.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */		
		public abstract void RemoveGroupWhiteList(string groupId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 取消屏蔽群消息。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Unblocks group messages.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void UnBlockGroup(string groupId, CallBack handle = null);

		/**
		 * \~chinese
		 * 将用户移除群组黑名单。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param members 	要从黑名单中移除的用户列表。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes users from the group block list.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param members	The list of users to be removed from the block list.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void UnBlockGroupMembers(string groupId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 解除全员禁言。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Unmutes all members in the group.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void UnMuteGroupAllMembers(string groupId, CallBack handle = null);

		/**
		 * \~chinese
		 * 解除禁言。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId	群组 ID。
		 * @param members	要解除禁言的用户列表。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Unmutes the specified group members.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId	The group ID.
		 * @param members	The list of members to be unmuted.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void UnMuteGroupMembers(string groupId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 更新群公告。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId 		群组 ID。
		 * @param announcement 	公告内容。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Updates the group announcement.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId 		The group ID.
		 * @param announcement 	The group announcement.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null);

		/**
		 * \~chinese
		 * 更新群组扩展字段。
		 * 
		 * 仅群主和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param groupId       群组 ID。
		 * @param ext			群组扩展字段。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Updates the group extension field.
		 * 
		 * Only the group owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId       The group ID.
		 * @param ext     		The group extension field.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */
		public abstract void UpdateGroupExt(string groupId, string ext, CallBack handle = null);

		/**
		 * \~chinese
		 * 上传共享文件至群组。
		 *
		 * 异步方法。
		 *
		 * @param groupId 	群组 ID。
		 * @param filePath 	共享文件的本地路径。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 *
		 * \~english
		 * Uploads the shared file to the group.
		 *
		 * This is an asynchronous method.
		 *
		 * @param groupId 	The group ID.
		 * @param filePath 	The local path of the shared file.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */
		public abstract void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null);

		/**
		 * \~chinese
		 * 注册群组管理器的监听器。
		 *
		 * @param groupManagerDelegate 		 要注册的群组管理器的监听器，继承自 {@link IGroupManagerDelegate}。
		 *
		 * \~english
		 * Adds a group manager listener.
		 *
		 * @param groupManagerDelegate 		The group manager listener to add. It is inherited from {@link IGroupManagerDelegate}.
		 * 
		 */
		public void AddGroupManagerDelegate(IGroupManagerDelegate groupManagerDelegate)
        {
            if (!CallbackManager.Instance().groupManagerListener.delegater.Contains(groupManagerDelegate))
            {
                CallbackManager.Instance().groupManagerListener.delegater.Add(groupManagerDelegate);
            }
        }

		/**
		 * \~chinese
		 * 移除群组管理器的监听器。
		 *
		 * @param groupManagerDelegate 		要移除的群组管理器的监听器，继承自 {@link IGroupManagerDelegate}。
		 *
		 * \~english
		 * Removes a group manager listener.
		 *
		 * @param groupManagerDelegate 		The group manager listener to remove. It is inherited from {@link IGroupManagerDelegate}.
		 * 
		 */
		public void RemoveGroupManagerDelegate(IGroupManagerDelegate groupManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().groupManagerListener.delegater.Contains(groupManagerDelegate))
            {
                CallbackManager.Instance().groupManagerListener.delegater.Remove(groupManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().groupManagerListener.delegater.Clear();
        }

    }

}