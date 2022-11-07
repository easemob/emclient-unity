using System.Collections.Generic;
using AgoraChat.SimpleJSON;

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
    public class RoomManager : BaseManager
    {
        internal List<IRoomManagerDelegate> delegater;

        internal RoomManager(NativeListener listener) : base(listener, SDKMethod.groupManager)
        {
            listener.GroupManagerEvent += NativeEventcallback;
            delegater = new List<IRoomManagerDelegate>();
        }

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
	     * @param callback        操作结果回调，详见 {@link CallBack}。
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
	     * @param callback        The operation result callback. See {@link CallBack}.
	     */
        public void AddRoomAdmin(string roomId, string memberId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userId", memberId);
            NativeCall(SDKMethod.addChatRoomAdmin, jo_param, callback);
        }

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
		 * @param callback        操作结果回调，详见 {@link CallBack}。
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
		 * @param callback        The operation result callback. See {@link CallBack}.
		 */
        public void BlockRoomMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall(SDKMethod.blockChatRoomMembers, jo_param, callback);
        }

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
		 * @param callback        操作结果回调，详见 {@link CallBack}。
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
		 * @param callback        The operation result callback. See {@link CallBack}.
		 */
        public void ChangeRoomOwner(string roomId, string newOwner, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userId", newOwner);
            NativeCall(SDKMethod.changeChatRoomOwner, jo_param, callback);
        }

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
		 * @param callback            操作结果回调，详见 {@link CallBack}。
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
		 * @param callback            The operation result callback. See {@link CallBack}.
		 */
        public void ChangeRoomDescription(string roomId, string newDescription, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("desc", newDescription);
            NativeCall(SDKMethod.changeChatRoomDescription, jo_param, callback);
        }

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
		 * @param callback        操作结果回调，详见 {@link CallBack}。
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
		 * @param callback        The operation result callback. See {@link CallBack}.
		 */
        public void ChangeRoomName(string roomId, string newName, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("name", newName);
            NativeCall(SDKMethod.changeChatRoomSubject, jo_param, callback);
        }

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
		 * @param callback            操作结果回调，详见 {@link CallBack}。
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
		 * @param callback			The operation result callback. See {@link CallBack}.
		 */
        public void CreateRoom(string name, string descriptions = null, string welcomeMsg = null, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("name", name);
            jo_param.Add("desc", descriptions);
            jo_param.Add("msg", welcomeMsg);
            jo_param.Add("count", maxUserCount);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));

            Process process = (_, jsonNode) =>
            {
                return ModelHelper.CreateWithJsonObject<Room>(jsonNode);
            };

            NativeCall<Room>(SDKMethod.changeChatRoomSubject, jo_param, callback, process);
        }

        /**
		 * \~chinese
		 * 销毁聊天室。
		 * 
		 * 仅聊天室所有者可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId		聊天室 ID。
		 * @param callback        操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Destroys a chat room.
		 * 
		 * Only the chat room owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId		The chat room ID.
		 * @param callback		The operation result callback. See {@link CallBack}.
		 */
        public void DestroyRoom(string roomId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            NativeCall(SDKMethod.destroyChatRoom, jo_param, callback);
        }

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
		 * @param callback    操作结果回调，详见 {@link CallBack}。
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
		 * @param callback	The operation result callback. See {@link CallBack}.
		 * 
		 */
        public void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("pageNum", pageNum);
            jo_param.Add("pageSize", pageSize);

            Process process = (_, jsonNode) =>
            {
                return new PageResult<Room>(jsonNode, (jn) =>
                {
                    return ModelHelper.CreateWithJsonObject<Room>(jn);
                });
            };

            NativeCall<PageResult<Room>>(SDKMethod.fetchPublicChatRoomsFromServer, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 从服务器获取聊天室公告内容。
		 *
		 * 异步方法。
		 *
		 * @param roomId 		聊天室 ID。
		 * @param callback		操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Gets the chat room announcement from the server.
		 * 
		 * This is an asynchronous method.
		 * 
		 * @param roomId 		The chat room ID.
		 * @param callback		The operation result callback. See {@link CallBack}.
		 */
        public void FetchRoomAnnouncement(string roomId, ValueCallBack<string> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            Process process = (_, jsonNode) =>
            {
                return jsonNode.IsString ? jsonNode.Value : null;
            };

            NativeCall<string>(SDKMethod.fetchChatRoomAnnouncement, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，详见 {@link CallBack}。
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
		 * @param callback		The operation result callback. See {@link CallBack}.
		 *
		 */
        public void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("pageNum", pageNum);
            jo_param.Add("pageSize", pageSize);
            Process process = (_, jsonNode) =>
            {
                return List.StringListFromJsonArray(jsonNode);
            };

            NativeCall<List<string>>(SDKMethod.fetchChatRoomBlockList, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 从服务器获取聊天室详情，默认不取成员列表。
		 *
		 * 异步方法。
		 *
		 * @param roomId	聊天室 ID。
		 * @param callback	操作结果回调，返回聊天室信息或错误描述，详见 {@link ValueCallBack}。
		 *
		 * \~english
		 * Gets details of a chat room from the server, excluding the member list by default.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId	The chat room ID.
		 * @param callback	The operation callback. If success, the chat room instance is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 */
        public void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            NativeCall(SDKMethod.fetchChatRoomInfoFromServer, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，成功则返回聊天室成员列表，失败则返回错误描述，详见 {@link ValueCallBack}。
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
		 * @param callback		The operation callback. If success, the chat room member list is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 * 
		 */
        public void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("cursor", cursor);
            jo_param.Add("pageSize", pageSize);

            Process process = (_, jsonNode) =>
            {
                return new CursorResult<string>(jsonNode, (jn) =>
                {
                    return jn.IsString ? jn.Value : null;
                });
            };

            NativeCall<CursorResult<string>>(SDKMethod.fetchChatRoomMembers, jo_param, callback, process);
        }

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
		 * @param callback		操作结果回调，成功则返回聊天室禁言列表，失败返回错误描述，详见 {@link ValueCallBack}。
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
		 * @param callback		The operation callback. If success, the chat room mute list is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 * 
		 */
        public void FetchRoomMuteList(string roomId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("pageNum", pageNum);
            jo_param.Add("pageSize", pageSize);

            Process process = (_, jsonNode) =>
            {
                return List.StringListFromJsonArray(jsonNode);
            };

            NativeCall<List<string>>(SDKMethod.fetchChatRoomMuteList, jo_param, callback, process);
        }

        /**
		 * \~chinese
		 * 加入聊天室。
		 * 
		 * 退出聊天室调用 {@link #LeaveRoom(String, CallBack)}。
		 *
		 * 异步方法。
		 *
		 * @param roomId 	聊天室 ID。
		 * @param callback	操作结果回调，成功则返回加入的聊天室对象，失败则返回错误信息，详见 {@link ValueCallBack}。
		 * 
		 * \~english
		 * Joins the chat room.
		 * 
		 * To exit the chat room, you can call {@link #LeaveRoom(String, CallBack)}.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId 	The ID of the chat room to join.
		 * @param callback	The operation callback. If success, the chat room instance is returned; otherwise, an error is returned. See {@link ValueCallBack}.
		 */
        public void JoinRoom(string roomId, ValueCallBack<Room> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            NativeCall(SDKMethod.joinChatRoom, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 离开聊天室。
		 * 
		 * 利用 {@link #JoinRoom(String, ValueCallBack)} 加入聊天室后，离开时调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId 	聊天室 ID。
		 * @param callback	操作结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Leaves a chat room.
		 * 
		 * After joining a chat room via {@link #JoinRoom(String, ValueCallBack)}, the member can call `LeaveRoom` to leave the chat room.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId 	The ID of the chat room to leave.
		 * @param callback	The operation callback. See {@link CallBack}.
		 * 
		 */
        public void LeaveRoom(string roomId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            NativeCall(SDKMethod.leaveChatRoom, jo_param, callback);
        }

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
		 * @param callback	操作结果回调，详见 {@link CallBack}。
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
		 * @param callback	The operation callback. See {@link CallBack}.
		 */
        public void MuteRoomMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall(SDKMethod.muteChatRoomMembers, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，详见 {@link CallBack}。
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
		 * @param callback		The operation callback. See {@link CallBack}.
		 */
        public void RemoveRoomAdmin(string roomId, string adminId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userId", adminId);
            NativeCall(SDKMethod.removeChatRoomAdmin, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，详见 {@link CallBack}。
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
		 * @param callback		The operation callback. See {@link CallBack}.
		 */
        public void DeleteRoomMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall(SDKMethod.removeChatRoomMembers, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，详见 {@link CallBack}。
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
		 * @param callback		The operation callback. See {@link CallBack}.
		 */
        public void UnBlockRoomMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall(SDKMethod.unBlockChatRoomMembers, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，详见 {@link CallBack}。
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
		 * @param callback		The operation callback. See {@link CallBack}.
		 * 
		 */
        public void UnMuteRoomMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall(SDKMethod.unMuteChatRoomMembers, jo_param, callback);
        }

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
		 * @param callback		操作结果回调，详见 {@link CallBack}。
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
		 * @param callback		The operation callback. See {@link CallBack}.
		 * 
		 */
        public void UpdateRoomAnnouncement(string roomId, string announcement, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("announcement", announcement);
            NativeCall(SDKMethod.updateChatRoomAnnouncement, jo_param, callback);
        }

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
		 * @param callback      结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
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
		 * @param callback    The completion callback. 
		 *                  - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
		 *                  - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
		 */
        public void MuteAllRoomMembers(string roomId, ValueCallBack<Room> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);

            Process process = (_, jsonNode) =>
            {
                return ModelHelper.CreateWithJsonObject<Room>(jsonNode);
            };

            NativeCall<Room>(SDKMethod.muteAllChatRoomMembers, jo_param, callback, process);
        }

        /**
		 * \~chinese
		 * 解除所有成员的禁言状态。
		 * 
		 * 仅聊天室所有者和管理员可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId    聊天室 ID。
		 * @param callback    结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
		 *                  失败时回调 {@link ValueCallBack#onError(int, String)}。
		 *
		 * \~english
		 * Unmutes all members.
		 * Only the chat room owner or admin can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId    The chat room ID.
		 * @param callback    The completion callback. 
		 *                  - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
		 *                  - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
		 */
        public void UnMuteAllRoomMembers(string roomId, ValueCallBack<Room> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);

            Process process = (_, jsonNode) =>
            {
                return ModelHelper.CreateWithJsonObject<Room>(jsonNode);
            };

            NativeCall<Room>(SDKMethod.unMuteAllChatRoomMembers, jo_param, callback, process);
        }

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
		 * @param callback      结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
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
		 * @param callback       The completion callback. 
		 *                     - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
		 *                     - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
		 */
        public void AddWhiteListMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall<Room>(SDKMethod.addMembersToChatRoomWhiteList, jo_param, callback);
        }

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
		 * @param callback         结果回调，成功时回调 {@link ValueCallBack#onSuccess(Object)}，
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
		 * @param callback        The completion callback. 
		 *                      - If this call succeeds, calls {@link ValueCallBack#onSuccess(Object)};
		 *                      - If this call fails, calls {@link ValueCallBack#onError(int, String)}.
		 */
        public void RemoveWhiteListMembers(string roomId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(members));
            NativeCall<Room>(SDKMethod.removeMembersFromChatRoomWhiteList, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 设置聊天室属性。
		  * 
		 * 聊天室成员均可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId         聊天室 ID。
		 * @param kv			 新增的属性，为键值对（key-value）结构。在键值对中，key 为属性名，不超过 128 字符，value 为属性值不超过 4096 字符。
		 *                       每个聊天室最多可有 100 个属性。每个应用的聊天室属性总大小不能超过 10 GB。Key 支持以下字符集：
		 *						 - 26 个小写英文字母 a-z；
		 *						 - 26 个大写英文字母 A-Z；
		 *						 - 10 个数字 0-9；
		 *						 - “_”, “-”, “.”。
		 *						，
		 * @param deleteWhenExit 当前成员退出聊天室时是否自动删除其设置的该聊天室的所有自定义属性。
		 * 							- （默认）`true`：是。
		 *							- `false`：否。
		 * @forced               是否覆盖其他成员设置的 key 相同的属性。
		 * 							- `true`：是。
		 *							- （默认）`false`：否。
		 * @param callback         结果回调，成功时回调 {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)}，
		 *                       失败时回调 {@link CallBackResult#onError(int, String)}。
		 *
		 * \~english
		 * Sets custom chat room attributes.
		 * All members in the chat room owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId        The chat room ID.
		 * @param kv            The chat room attributes to add. The attributes are in key-value format. 
		 *                      In a key-value pair, the key is the attribute name that can contain 128 characters at most; the value is the attribute value that cannot exceed 4096 characters. 
		 *                      A chat room can have a maximum of 100 custom attributes and the total length of custom chat room attributes cannot exceed 10 GB for each app. Attribute keys support the following character sets:
		 *						 - 26 lowercase English letters (a-z)
		 *						 - 26 uppercase English letters (A-Z)
		 *						 - 10 numbers (0-9)
		 *						 - "_", "-", "."
		 * @deleteWhenExit      Whether to delete the chat room attributes set by the member when he or she exits the chat room.
		 * 						- (Default)`true`: Yes.
		 *						- `false`: No.
		 * @forced              Whether to overwrite the attributes with same key set by others.
		 * 						- `true`: Yes.
		 *						- (Default)`false`: No.
		 * @param callback        The completion callback. If this call succeeds, calls {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)};
		 *                      if this call fails, calls {@link CallBackResult#onError(int, String)}.
		 */
        public void AddAttributes(string roomId, Dictionary<string, string> kv, bool deleteWhenExit = true, bool forced = false, CallBackResult callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("kv", JsonObject.JsonObjectFromDictionary(kv));
            jo_param.Add("deleteWhenExit", deleteWhenExit);
            jo_param.Add("forced", forced);
            // TODO: add back result?
            //NativeCall<Room>(SDKMethod.setChatRoomAttributes, jo_param, callback);
        }

        /**
		 * \~chinese
		 * 根据聊天室属性 key 列表获取属性列表。
		 * 聊天室成员均可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId         聊天室 ID。
		 * @param keys			 待获取属性的键值。如果未指定任何 key 值，则表示获取所有属性。
		 * @param callback         结果回调，成功时回调 {@link ValueCallBack#OnSuccessValue(Dictionary<string, string>)}，
		 *                       失败时回调 {@link ValueCallBack#onError(int, String)}。
		 *
		 * \~english
		 * Gets the list of custom chat room attributes based on the attribute key list.
		 * 
		 * All members in the chat room owner can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId        The chat room ID.
		 * @param keys			The key list of attributes to get. If you set it as `null` or leave it empty, this method retrieves all custom attributes.
		 * @param callback        The completion callback. If this call succeeds, calls {@link ValueCallBack#OnSuccessValue(Dictionary<string, string>)};
		 *                      if this call fails, calls {@link ValueCallBack#onError(int, String)}.
		 */
        public void FetchAttributes(string roomId, List<string> keys, ValueCallBack<Dictionary<string, string>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("list", JsonObject.JsonArrayFromStringList(keys));

            Process process = (_, jsonNode) =>
            {
                return Dictionary.StringDictionaryFromJsonObject(jsonNode);
            };

            NativeCall<Dictionary<string, string>>(SDKMethod.fetchChatRoomAttributes, jo_param, callback, process);
        }

        /**
		 * \~chinese
		 * 根据聊天室 ID 和属性 key 列表删除聊天室自定义属性。
		 * 
		 * 聊天室成员均可调用此方法。
		 *
		 * 异步方法。
		 *
		 * @param roomId         聊天室 ID。
		 * @param keys           待删除属性的键值。
		 * @forced               是否强制删除其他用户所设置的相同 key 的属性。
		 * @param callback         结果回调，成功时回调 {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)}，
		 *                       失败时回调 {@link CallBackResult#onError(int, String)}。
		 *
		 * \~english
		 * Removes custom chat room attributes by chat room ID and attribute key list.
		 * 
		 * All members in the chat room can call this method.
		 *
		 * This is an asynchronous method.
		 *
		 * @param roomId        The chat room ID.
		 * @param keys			The keys of custom chat room attributes to remove.
		 * @forced              Whether to remove attributes with same key set by others.
		 * @param callback        The completion callback. If this call succeeds, calls {@link CallBackResult#OnSuccessResult(Dictionary<string, int>)};
		 *                      if this call fails, calls {@link CallBackResult#onError(int, String)}.
		 */
        public void RemoveAttributes(string roomId, List<string> keys, bool forced = false, CallBackResult callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("roomId", roomId);
            jo_param.Add("list", JsonObject.JsonArrayFromStringList(keys));
            // TODO: add back result?
            //NativeCall<Room>(SDKMethod.setChatRoomAttributes, jo_param, callback);

        }

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
            if (!delegater.Contains(roomManagerDelegate))
            {
                delegater.Add(roomManagerDelegate);
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
            delegater.Remove(roomManagerDelegate);
        }

        internal void ClearDelegates()
        {
            delegater.Clear();
        }


        internal void NativeEventcallback(string method, JSONNode jsonNode)
        {
            if (delegater.Count == 0) return;

            string roomId = jsonNode["roomId"];
            string roomName = jsonNode["name"];

            foreach (IRoomManagerDelegate it in delegater)
            {
                switch (method)
                {
                    case SDKMethod.onDestroyedFromRoom:
                        {
                            it.OnDestroyedFromRoom(roomId, roomName);
                        }
                        break;
                    case SDKMethod.onMemberJoinedFromRoom:
                        {
                            string userId = jsonNode["userId"];
                            it.OnMemberJoinedFromRoom(roomId, userId);
                        }
                        break;
                    case SDKMethod.onMemberExitedFromRoom:
                        {
                            string userId = jsonNode["userId"];
                            it.OnMemberExitedFromRoom(roomId, roomName, userId);
                        }
                        break;
                    case SDKMethod.onRemovedFromRoom:
                        {
                            string userId = jsonNode["userId"];
                            it.OnRemovedFromRoom(roomId, roomName, userId);
                        }
                        break;
                    case SDKMethod.onMuteListAddedFromRoom:
                        {
                            List<string> list = List.StringListFromJsonArray(jsonNode["userId"]);
                            int muteExpire = jsonNode["expireTime"];
                            it.OnMuteListAddedFromRoom(roomId, list, muteExpire);
                        }
                        break;
                    case SDKMethod.onMuteListRemovedFromRoom:
                        {
                            List<string> list = List.StringListFromJsonArray(jsonNode["userId"]);
                            it.OnMuteListRemovedFromRoom(roomId, list);
                        }
                        break;
                    case SDKMethod.onAdminAddedFromRoom:
                        {
                            string userId = jsonNode["userId"];
                            it.OnAdminAddedFromRoom(roomId, userId);
                        }
                        break;
                    case SDKMethod.onAdminRemovedFromRoom:
                        {
                            string userId = jsonNode["userId"];
                            it.OnAdminRemovedFromRoom(roomId, userId);
                        }
                        break;
                    case SDKMethod.onOwnerChangedFromRoom:
                        {
                            string newOwner = jsonNode["newOwner"];
                            string oldOwner = jsonNode["oldOwner"];
                            it.OnOwnerChangedFromRoom(roomId, newOwner, oldOwner);
                        }
                        break;
                    case SDKMethod.onAnnouncementChangedFromRoom:
                        {
                            string announcement = jsonNode["announcement"];
                            it.OnAnnouncementChangedFromRoom(roomId, announcement);
                        }
                        break;
                    case SDKMethod.onChatroomAttributesChanged:
                        {
                            Dictionary<string, string> kv = Dictionary.StringDictionaryFromJsonObject(jsonNode["kv"]);
                            string userId = jsonNode["userId"];
                            it.OnChatroomAttributesChanged(roomId, kv, userId);
                        }
                        break;
                    case SDKMethod.onChatroomAttributesRemoved:
                        {
                            List<string> list = List.StringListFromJsonArray(jsonNode["list"]);
                            string userId = jsonNode["userId"];
                            it.OnChatroomAttributesRemoved(roomId, list, userId);
                        }
                        break;
                }
            }
        }
    }



}