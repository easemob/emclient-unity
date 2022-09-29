using System.Collections.Generic;

namespace AgoraChat
{
/**
	     * \~chinese
	     * 聊天管理器抽象类。
		 * 
	     * \~english
	     * The abstract class for the chat manager.
		 * 
	     */
    public abstract class IRoomManager
    {
        /**
	     * \~chinese
	     * 添加聊天室管理员。
		  * 
	     * 仅聊天室所有者可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param chatRoomId	聊天室 ID。
	     * @param memberId		要添加的管理员的 ID。
	     * @param handle        操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Adds a chat room admin.
		 * 
	     * Only the chat room owner can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param chatRoomId	The chat room ID.
	     * @param memberId		The user ID of the chat room admin to be added.
	     * @param handle        The operation result callback. See {@link CallBack}.
	     */
        public abstract void AddRoomAdmin(string roomId, string memberId, CallBack handle = null);

        /**
	     * \~chinese
	     * 将成员添加到聊天室黑名单。
		 * 
	     * 仅聊天室所有者和管理员可调用此方法。
		 * 
	     * **注意**
		 * 
	     * - 成员加入黑名单的同时，将被服务器移出聊天室。
	     * - 可通过 {@link IRoomManagerDelegate#OnRemovedFromRoom( String, String, String)} 回调通知。
	     * - 加入黑名单的成员禁止再次加入到聊天室。
	     *
	     * 异步方法。
	     *
	     * @param roomId	    聊天室 ID。
	     * @param members		要加入黑名单的成员列表。
	     * @param handle        操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Adds members to the block list of the chat room.
		 * 
	     * Only the chat room owner or admin can call this method.
		 * 
	     * **Note**
		 * 
	     * - A member, once added to block list of the chat room, will be removed from the chat room by the server.
	     * - After being added to the chat room block list, the member is notified via the {@link IRoomManagerDelegate#OnRemovedFromRoom( String, String, String)} callback.
	     * - Members on the block list cannot rejoin the chat room.
	     *
	     * This is an asynchronous method.
	     *
	     * @param roomId	    The chat room ID.
	     * @param members		The list of members to be added to block list of the chat room.
	     * @param handle        The operation result callback. See {@link CallBack}.
	     */       
        public abstract void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null);

        /**
	     * \~chinese
	     * 转让聊天室的所有权。
		 * 
	     * 仅聊天室所有者可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param roomId	    聊天室 ID。
	     * @param newOwner		新的聊天室所有者 ID。
	     * @param handle        操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Transfers the chat room ownership.
		 * 
	     * Only the chat room owner can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param chatRoomId	The chat room ID.
	     * @param newOwner		The user ID of the new chat room owner.
	     * @param handle        The operation result callback. See {@link CallBack}.
	     */
        public abstract void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null);

        /**
	     * \~chinese
	     * 修改聊天室描述信息。
		 * 
	     * 仅聊天室所有者可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param roomId		    聊天室 ID。
	     * @param newDescription	新的聊天室描述。
	     * @param handle            操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Modifies the chat room description.
		 * 
	     * Only the chat room owner can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param roomId		    The chat room ID.
	     * @param newDescription	The new description of the chat room.
	     * @param handle            The operation result callback. See {@link CallBack}.
	     */
        public abstract void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null);

        /**
	     * \~chinese
	     * 修改聊天室名称。
		 * 
	     * 仅聊天室所有者可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param roomId	    聊天室 ID。
	     * @param newName	    聊天室新名称。
	     * @param handle        操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Changes the chat room name.
		 * 
	     * Only the chat room owner can call this method.
	     *
	     * This is an asynchronous method.
	     *
	     * @param roomId	    The chat room ID.
	     * @param newName	    The new name of the chat room.
	     * @param handle        The operation result callback. See {@link CallBack}.
	     */
        public abstract void ChangeRoomName(string roomId, string newName, CallBack handle = null);

        /**
	     * \~chinese
	     * 创建聊天室。
	     *
	     * 异步方法。
	     *
	     * @param name              聊天室名称。
	     * @param description       聊天室描述。
	     * @param welcomeMsg        邀请成员加入聊天室的消息。
	     * @param maxUserCount      允许加入聊天室的最大成员数。
	     * @param members           邀请加入聊天室的成员列表。
	     * @param handle            操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Creates a chat room.
	     *
	     * This is an asynchronous method.
	     *
	     * @param name              The chat room name.
	     * @param description       The chat room description.
	     * @param welcomeMsg        A welcome message inviting members to join the chat room.
	     * @param maxUserCount      The maximum number of members allowed to join the chat room.
	     * @param members           The list of members invited to join the chat room.
	     * @param handle			The operation result callback. See {@link CallBack}.
	     */        
        public abstract void CreateRoom(string name, string descriptions = null, string welcomeMsg = null, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null);

		/**
		 * \~chinese
		 * 销毁聊天室。
		 * 
		 * 仅聊天室所有者可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param handle        操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Destroys a chat room.
		 * 
		 * Only the chat room owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param handle		The operation result callback. See {@link CallBack}.
		 */
		public abstract void DestroyRoom(string roomId, CallBack handle = null);

		/**
		 * \~chinese
		 * 以分页方式从服务器获取聊天室。
		 * 
		 * 对于数据量未知且很大的情况，你可以设置 `pageNum` 和 `pageSize` 分页获取数据。
		 *
		 * 异步方法。
		 *
		 * @param pageNum 	当前页数，从 1 开始。
		 * @param pageSize 	每页期望返回的记录数。如当前在最后一页，返回的数量小于该参数的值。
		 * @param handle    操作结果回调，详见 {@link CallBack}。
		 * 
		 *
		 * \~english
		 * Gets chat room data from the server with pagination.
		 * 
		 * For a large but unknown quantity of data, you can get data with pagination by specifying `pageNum` and `pageSize`.
		 *
		 * This is an asynchronous method.
		 *
		 * @param pageNum 	The page number, starting from 1.
		 * @param pageSize 	The number of records that you expect to get on each page. For the last page, the number of returned records is less than the parameter value.
		 * @param handle	The operation result callback. See {@link CallBack}.
		 * 
		 */		
		public abstract void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null);

		/**
		 * \~chinese
		 * 从服务器获取聊天室公告内容。
		 *
		 * 异步方法。
		 *
		 * @param roomId 		聊天室 ID。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Gets the chat room announcement from the server.
		 * 
		 * This is an asynchronous method.
		 * 
		 * @param roomId 		The chat room ID.
		 * @param handle		The operation result callback. See {@link CallBack}.
		 */
		public abstract void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null);

		/**
		 * \~chinese
		 * 以分页的形式获取聊天室黑名单列表。
		 * 
		 * 对于数据量未知且很大的情况，你可以设置 `pageSize` 和 `cursor` 分页获取数据。
		 * 
		 * 仅聊天室所有者或管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param pageNum		当前页码，从 1 开始。
		 * @param pageSize		每页期望返回的黑名单上的用户数。如果当前在最后一页，返回的数量小于该参数的值。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 * 
		 *
		 * \~english
		 * Gets the block list of the chat room with pagination.
         *
         * For a large but unknown quantity of data, you can get data with pagination by specifying `pageSize` and `cursor`.
		 *
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param pageNum		The page number, starting from 1. 
		 * @param pageSize		The number of users on the block list that you expect to get on each page. For the last page, the number of returned users is less than the parameter value.
		 * @param handle		The operation result callback. See {@link CallBack}.
		 *
		 */
		public abstract void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

		/**
		 * \~chinese
		 * 从服务器获取聊天室详情，默认不取成员列表。
		 *
		 * 异步方法。
		 *
		 * @param roomId	聊天室 ID。
		 * @param handle	操作结果回调，返回聊天室信息或错误描述，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets details of a chat room from the server, excluding the member list by default.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId	The chat room ID.
		 * @param handle	The operation callback. If success, the chat room instance is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 */
		public abstract void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null);

		/**
		 * \~chinese
		 * 以分页方式获取聊天室成员列表。
		 * 
		 * 对于数据量未知且很大的情况，你可以设置 `pageSize` 和 `cursor` 分页获取数据。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param cursor		从该游标位置开始取数据。首次调用 `cursor` 传空值，SDK 按照用户加入聊天室时间的倒序获取数据，即从最新数据开始获取。服务器返回的数据中包含 `cursor` 字段，该字段保存在本地，下次调用接口时，可以将更新的 `cursor` 传入作为开始获取数据的位置。
		 * @param pageSize		每页期望返回的成员数。如果当前为最后一页，返回的数据量小于该参数的值；
		 * @param handle		操作结果回调，成功则返回聊天室成员列表，失败则返回错误描述，详见 {@link ValueCallBack}。
		 * 
		 *  
		 * \~english
		 * Gets the chat room member list with pagination.

		 * For a large but unknown quantity of data, you can get data with pagination by specifying `pageSize` and `cursor`.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param cursor		The cursor position from which to start to get data.
		 *                      At the first method call, if you set `cursor` as `null`, the SDK gets the data in the reverse chronological order of when users joined the chat room. 
		 *                      Amid the returned data (CursorResult), `cursor` is a field saved locally and the updated cursor can be passed as the position from which to start to get data for the next query.
		 * @param pageSize		The number of members that you expect to get on each page. For the last page, the number of returned members is less than the parameter value.
		 * @param handle		The operation callback. If success, the chat room member list is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 * 
		 */		
		public abstract void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null);

		/**
		 * \~chinese
		 * 以分页方式获取聊天室禁言列表。
         * 
         * 对于数据量未知且很大的情况，你可以设置 `pageSize` 和 `cursor` 分页获取数据。
		 * 
		 * 仅聊天室所有者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param pageNum		当前页码，从 1 开始。
		 * @param pageSize		每页返回的禁言成员数。如果当前为最后一页，返回的数量小于该参数的值。
		 * @param handle		操作结果回调，成功则返回聊天室禁言列表，失败返回错误描述，详见 {@link ValueCallBack}。
         * 
		 *
		 * \~english
		 * Gets the list of members who are muted in the chat room.
		 *
         * For a large but unknown quantity of data, you can get data with pagination by specifying `pageSize` and `cursor`.
         * 
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param pageNum		The page number, starting from 1.
		 * @param pageSize		The number of muted members that you expect to get on each page. For the last page, the actual number of returned members is less than the parameter value.
		 * @param handle		The operation callback. If success, the chat room mute list is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 * 
		 */		
		public abstract void FetchRoomMuteList(string roomId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> handle = null);

		/**
		 * \~chinese
		 * 加入聊天室。
		 * 
		 * 退出聊天室调用 {@link #LeaveRoom(String, CallBack)}。
		 *
		 * 异步方法。
		 *
		 * @param roomId 	聊天室 ID。
		 * @param handle	操作结果回调，成功则返回加入的聊天室对象，失败则返回错误信息，详见 {@link ValueCallBack}。
		 * 
		 * \~english
		 * Joins the chat room.
		 * 
		 * To exit the chat room, you can call {@link #LeaveRoom(String, CallBack)}.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId 	The ID of the chat room to join.
		 * @param handle	The operation callback. If success, the chat room instance is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 */		
		public abstract void JoinRoom(string roomId, ValueCallBack<Room> handle = null);

		/**
		 * \~chinese
		 * 离开聊天室。
		 * 
		 * 利用 {@link #JoinRoom(String, ValueCallBack)} 加入聊天室后，离开时调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId 	聊天室 ID。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Leaves a chat room.
		 * 
		 * After joining a chat room via {@link #JoinRoom(String, ValueCallBack)}, the member can call `LeaveRoom` to leave the chat room.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId 	The ID of the chat room to leave.
		 * @param handle	The operation callback. See {@link CallBack}.
		 * 
		 */		
		public abstract void LeaveRoom(string roomId, CallBack handle = null);

		/**
		 * \~chinese
		 * 禁止聊天室成员发言。
		 * 
		 * 仅聊天室所有者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId	聊天室 ID。
		 * @param members   要禁言的用户列表。
		 * @param handle	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Mutes members in a chat room.
		 * 
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId	The chat room ID.
		 * @param members 	The list of members to be muted.
		 * @param handle	The operation callback. See {@link CallBack}.
		 */		
		public abstract void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 移除聊天室管理员权限。
		 * 
		 * 仅聊天室所有者可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param adminId		要移除管理员权限的 ID。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes the administrative privileges of a chat room admin.
		 * 
		 * Only the chat room owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param adminId		The user ID of the admin whose administrative privileges are to be removed.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */		
		public abstract void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null);

		/**
		 * \~chinese
		 * 将成员移出聊天室。
		 * 
		 * 仅聊天室所有者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param members		要移出聊天室的用户列表。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes members from a chat room.
		 * 
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param members		The list of members to be removed from the chat room.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */
		public abstract void DeleteRoomMembers(string roomId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 从聊天室黑名单中移除成员。
		 * 
		 * 仅聊天室所有者或管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param members		要移除黑名单的成员列表。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Removes members from the block list of the chat room.
		 * 
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param members		The list of members to be removed from the block list of the chat room.
		 * @param handle		The operation callback. See {@link CallBack}.
		 */
		public abstract void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 解除禁言。
		 * 
		 * 仅聊天室所有者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param members		要解除禁言的用户列表。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Unmutes members in a chat room.
		 * 
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param members		The list of members to be unmuted.
		 * @param handle		The operation callback. See {@link CallBack}.
		 * 
		 */		
		public abstract void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null);

		/**
		 * \~chinese
		 * 更新聊天室公告。
		 * 
		 * 仅聊天室所有者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId 		聊天室 ID。
		 * @param announcement 	公告内容。
		 * @param handle		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Updates the chat room announcement.
		 * 
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId 		The chat room ID.
		 * @param announcement 	The announcement content.
		 * @param handle		The operation callback. See {@link CallBack}.
		 * 
		 */
		public abstract void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null);

        /**
         * \~chinese
         * 设置全员禁言。
         * 
         * 仅聊天室所有者和管理员可调用此方法。
         * 
         * 聊天室拥有者、管理员及加入白名单的用户不受影响。
         *
         * 异步方法。
         *
         * @param roomId      聊天室 ID。
         * @param handle      结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
         *                    失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Mutes all members.
         * Only the chat room owner or admin can call this method.
         * This method does not work for the chat room owner, admin, and members added to the allow list.
         *
         * This is an asynchronous method.
         *
         * @param roomId    The chat room ID.
         * @param handle    The completion callback. 
		 *                  - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
         *                  - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
        public abstract void MuteAllRoomMembers(string roomId, ValueCallBack<Room> handle = null);

        /**
         * \~chinese
         * 解除所有成员的禁言状态。
         * 仅聊天室所有者和管理员可调用此方法。
         *
         * 异步方法。
         *
         * @param roomId    聊天室 ID。
         * @param handle    结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
         *                  失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Unmutes all members.
         * Only the chat room owner or admin can call this method.
         *
         * This is an asynchronous method.
         *
         * @param roomId    The chat room ID.
         * @param handle    The completion callback. 
		 *                  - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
         *                  - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
        public abstract void UnMuteAllRoomMembers(string roomId, ValueCallBack<Room> handle = null);

        /**
         * \~chinese
         * 将成员添加到白名单。
		 * 
         * 仅聊天室所有者或管理员可调用此方法。
		 * 
         * 聊天室所有者或者管理员执行 {@link #MuteAllMembers} 时，加入白名单的成员不受影响。
         *
         * 异步方法。
         *
         * @param roomId      聊天室 ID。
         * @param members     加入白名单的成员列表。
         * @param handle      结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
         *                    失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Adds members to the allow list.
		 * 
         * Only the chat room owner or admin can call this method.
		 * 
         * A call to the {@link #MuteAllMembers} method by the chat room owner or admin does not affect members on the allow list.
         *
         * This is an asynchronous method.
         *
         * @param roomId       The chat room ID.
         * @param members      The list of members to be added to the allow list.
         * @param handle       The completion callback. 
		 *                     - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
         *                     - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
        public abstract void AddWhiteListMembers(string roomId, List<string> members, CallBack handle = null);

        /**
         * \~chinese
         * 将成员从白名单移除。
		 * 
         * 仅聊天室所有者和管理员可调用此方法。
		 * 
         * 成员从白名单移除后，将受到 {@link #MuteAllMembers} 功能的影响。
         *
         * 异步方法。
         *
         * @param roomId         聊天室 ID。
         * @param members        移除白名单的用户列表。
         * @param handle         结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
         *                       失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Removes members from the block list.
		 * 
         * Only the chat room owner or admin can call this method.
		 * 
         * When members are removed from the block list, a call to the method {@link #MuteAllMembers(String, EMValueCallBack)} will also mute them.
         *
         * This is an asynchronous method.
         *
         * @param roomId        The chat room ID.
         * @param members       The list of members to be removed from the block list.
         * @param handle        The completion callback. 
		 *                      - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
         *                      - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
        public abstract void RemoveWhiteListMembers(string roomId, List<string> members, CallBack handle = null);

		/**
         * \~chinese
         * 添加聊天室属性。
         * 聊天室成员均可调用此方法。
         *
         * 异步方法。
         *
         * @param roomId         聊天室 ID。
         * @param kv			 新增的属性。
         *                        其中属性中的键值用于指定属性名。属性名不能超过 128 字符。每个聊天室最多可有 100 个属性。Key 支持以下字符集：
         *							• 26 个小写英文字母 a-z；
         *							• 26 个大写英文字母 A-Z；
         *							• 10 个数字 0-9；
         *							• “_”, “-”, “.”。
         *						 聊天室属性值不超过 4096 字符，每个应用的聊天室属性总大小不能超过 10 GB。
         * @param deleteWhenExit 当前成员退出聊天室是否自动删除该聊天室中其设置的所有聊天室自定义属性。
         * 							- （默认）`true`:是。
		 *							- `false`:否。
         * @forced               是否覆盖其他成员设置的同名属性。
         * @param handle         结果回调，成功时回调 {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)}，
         *                       失败时回调 {@link CallBackResult#onError(int, String)}。
         *
         * \~english
         * Add chat room properties.
         * All members in the chatroom owner can call this API.
         *
         * This is an asynchronous method.
         *
         * @param roomId        The chat room ID.
         * @param kv            The added chatroom attributes.
         *                        The chat room attribute key that specifies the attribute name. The attribute name can contain 128 characters at most.
         *                          A chat room can have a maximum of 100 custom attributes. The following character sets are supported:
         *								- 26 lowercase English letters (a-z)
         *								- 26 uppercase English letters (A-Z)
         *								- 10 numbers (0-9)
         *								- "_", "-", "."
         *							The chat room attribute value. The attribute value can contain a maximum of 4096 characters. The total length of custom chat room attributes cannot exceed 10 GB for each app.
         * @deleteWhenExit      Whether to delete the chat room attributes set by the member when he or she exits the chat room.
         * 							- (Default)`true`:Yes.
		 *							- `false`: No.
         * @forced              Whether to overwrite the attributes with same key name set by others.
         * @param handle        The completion callback. If this call succeeds, calls {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)};
         *                      if this call fails, calls {@link CallBackResult#onError(int, String)}.
         */
		public abstract void AddAttributes(string roomId, Dictionary<string, string> kv, bool deleteWhenExit = true, bool forced = false, CallBackResult handle = null);

		/**
         * \~chinese
         * 根据聊天室属性 key 列表获取属性列表。
         * 聊天室成员均可调用此方法。
         *
         * 异步方法。
         *
         * @param roomId         聊天室 ID。
         * @param keys			 待获取属性的键值。如果未指定任何key值，则表示获取所有属性。
         * @param handle         结果回调，成功时回调 {@link ValueCallBack#OnSuccessValue(Dictionary<string, string>)}，
         *                       失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Gets the list of custom chat room attributes based on the attribute key list.
         * All members in the chatroom owner can call this API.
         *
         * This is an asynchronous method.
         *
         * @param roomId        The chat room ID.
         * @param keys			The keys used to fetch properties. If not set any special keys, then will fetch all properties.
         * @param handle        The completion callback. If this call succeeds, calls {@link ValueCallBack#OnSuccessValue(Dictionary<string, string>)};
         *                      if this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
		public abstract void FetchAttributes(string roomId, List<string> keys, ValueCallBack<Dictionary<string, string>> handle = null);

		/**
         * \~chinese
         * 根据聊天室 ID 和属性 key 列表删除聊天室自定义属性。。
         * 聊天室成员均可调用此方法。
         *
         * 异步方法。
         *
         * @param roomId         聊天室 ID。
         * @param keys           待删除属性的键值。
         * @forced               是否强制删除其他用户所设置的同名属性。
         * @param handle         结果回调，成功时回调 {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)}，
         *                       失败时回调 {@link CallBackResult#onError(int, String)}。
         *
         * \~english
         * Removes custom chat room attributes by chat room ID and attribute key list.
         * All members in the chatroom owner can call this API.
         *
         * This is an asynchronous method.
         *
         * @param roomId        The chat room ID.
         * @param keys			The keys used to remove properties.
         * @forced              Whether force to remove attributes with same key name set by others.
         * @param handle        The completion callback. If this call succeeds, calls {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)};
         *                      if this call fails, calls {@link CallBackResult#onError(int, String)}.
         */
		public abstract void RemoveAttributes(string roomId, List<string> keys, bool forced = false, CallBackResult handle = null);

		/**
		 * \~chinese
		 * 注册聊天室监听器。
		 *
		 * @param roomManagerDelegate 		要注册的聊天室监听器，继承自 {@link IRoomManagerDelegate}。
		 *
		 * \~english
		 * Adds a chat room listener.
		 *
		 * @param roomManagerDelegate 		The chat room listener to add. It is inherited from {@link IRoomManagerDelegate}.
		 * 
		 */
		public void AddRoomManagerDelegate(IRoomManagerDelegate roomManagerDelegate)
        {
            if (!CallbackManager.Instance().roomManagerListener.delegater.Contains(roomManagerDelegate))
            {
                CallbackManager.Instance().roomManagerListener.delegater.Add(roomManagerDelegate);
            }
        }

		/**
		 * \~chinese
		 * 移除聊天室监听器。
		 *
		 * @param roomManagerDelegate 		要移除的聊天室监听器，继承自 {@link IRoomManagerDelegate}。
		 *
		 * \~english
		 * Removes a chat room listener.
		 *
		 * @param roomManagerDelegate 		The chat room listener to remove. It is inherited from {@link IRoomManagerDelegate}.
		 * 
		 */
		public void RemoveRoomManagerDelegate(IRoomManagerDelegate roomManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().roomManagerListener.delegater.Contains(roomManagerDelegate))
            {
                CallbackManager.Instance().roomManagerListener.delegater.Remove(roomManagerDelegate);
            }            
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().roomManagerListener.delegater.Clear();
        }
    }
}