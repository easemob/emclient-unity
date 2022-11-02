using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    /**
    * \~chinese
    * 群组管理器抽象类。
    *
    * \~english
    * The abstract class for the group manager.
    *
    */
    public class GroupManager
    {
        internal CallbackManager callbackManager;
        internal List<IGroupManagerDelegate> delegater;

        internal string NAME_GROUPMANAGER = "GroupManager";

        internal GroupManager(NativeListener listener)
        {
            callbackManager = listener.callbackManager;
            listener.groupManagerEvent += NativeEventHandle;
            delegater = new List<IGroupManagerDelegate>();
        }

        void NativeEventHandle(string method, string jsonString)
        {

        }

        /**
         * \~chinese
         * 申请加入群组。
         *
         * 异步方法。
         *
         * @param groupId	群组 ID。
         * @param reason    申请加入的原因。
         * @param callback  申请结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Requests to join a group.
         *
         * This is an asynchronous method.
         *
         * @param groupId	The group ID.
         * @param reason    The reason for requesting to joining the group.
         * @param callback  The callback of application. See {@link CallBack}.
         */
        public void applyJoinToGroup(string groupId, string reason = "", CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("groupId", groupId);
            jo_param.Add("reason", reason ?? "");

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_GROUPMANAGER, "requestToJoinPublicGroup", jo_param, (null != callback) ? callback.callbackId : "");
        }

        /**
	     * \~chinese
	     * 接受入群邀请。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param callback  接受结果回调，返回用户同意邀请的群组，详见 {@link ValueCallBack}。
	     *
	     * \~english
	     * Accepts a group invitation.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param callback  The callback of acceptance. Returns the group instance which the user has accepted the invitation to join. See {@link ValueCallBack}.
	     */
        public void AcceptGroupInvitation(string groupId, ValueCallBack<Group> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("groupId", groupId);

            callbackManager.AddCallbackAction<Group>(callback, null);
            CWrapperNative.NativeCall(NAME_GROUPMANAGER, "acceptInvitationFromGroup", jo_param, (null != callback) ? callback.callbackId : "");
        }

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
	     * @param callback  批准结果回调，详见 {@link CallBack}。
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
	     * @param callback  The callback of approval. See {@link CallBack}.
	     */
        public void AcceptGroupJoinApplication(string groupId, string username, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("groupId", groupId);
            jo_param.Add("username", username);

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_GROUPMANAGER, "acceptJoinApplication", jo_param, (null != callback) ? callback.callbackId : "");
        }

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
	     * @param callback  添加结果回调，详见 {@link CallBack}。
	     *
	     * \~english
         * Adds a group admin.
	     * Only the group owner, not the group admin, can call this method.
	     *
	     * This is an asynchronous method.
         *
	     * @param groupId	The group ID.
	     * @param memberId	The new admin ID.
	     * @param callback  The callback of addition. See {@link CallBack}.
	     */
        public void AddGroupAdmin(string groupId, string memberId, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("groupId", groupId);
            jo_param.Add("memberId", memberId);

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_GROUPMANAGER, "addAdmin", jo_param, (null != callback) ? callback.callbackId : "");
        }

        /**
	     * \~chinese
	     * 向群组中添加新成员。

	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。
	     *
	     * @param groupId       群组的 ID。
	     * @param members       要添加的新成员列表。
	     * @param callback      添加结果回调，详见 {@link CallBack}。
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
	     * @param callback      The operation callback. See {@link CallBack}.
	     */
        public void AddGroupMembers(string groupId, List<string> members, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("groupId", groupId);
            jo_param.Add("members", JsonString.JsonStringFromStringList(members));

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_GROUPMANAGER, "addAdmin", jo_param, (null != callback) ? callback.callbackId : "");
        }

		/**
	     * \~chinese
	     * 添加白名单。

	     * 仅群主和管理员可调用此方法。
	     *
	     * 异步方法。 
	     *
	     * @param groupId 	群组 ID。
	     * @param members 	要添加的成员列表。
	     * @param callback  添加结果回调，详见 {@link CallBack}。
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
	     * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void AddGroupWhiteList(string groupId, List<string> members, CallBack callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);
			jo_param.Add("members", JsonString.JsonStringFromStringList(members));

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "addWhiteList", jo_param, (null != callback) ? callback.callbackId : "");			
		}

		/**
	     * \~chinese
	     * 屏蔽群消息。
		 * 
	     * 屏蔽群消息的用户仍是群成员，但无法接收群消息。
	     *
	     * 异步方法。
	     *
	     * @param groupId	群组 ID。
	     * @param callback  屏蔽结果回调，详见 {@link CallBack}。
	     *
	     *\~english
	     * Blocks group messages.
		 * 
         * The user that blocks group messages is still a group member. They just cannot receive group messages.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId	The group ID.
	     * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void BlockGroup(string groupId, CallBack callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "blockGroup", jo_param, (null != callback) ? callback.callbackId : "");
		}

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
	     * @param callback  操作结果回调，详见 {@link CallBack}。
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
	     * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void BlockGroupMembers(string groupId, List<string> members, CallBack callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);
			jo_param.Add("members", JsonString.JsonStringFromStringList(members));

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "blockMembers", jo_param, (null != callback) ? callback.callbackId : "");
		}

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
         * @param callback  操作结果回调，详见 {@link CallBack}。
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
         * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void ChangeGroupDescription(string groupId, string desc, CallBack callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);
			jo_param.Add("desc", desc);

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "updateDescription", jo_param, (null != callback) ? callback.callbackId : "");
		}

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
	     * @param callback  操作结果回调，详见 {@link CallBack}。
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
	     * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void ChangeGroupName(string groupId, string name, CallBack callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);
			jo_param.Add("name", name);

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "updateGroupSubject", jo_param, (null != callback) ? callback.callbackId : "");
		}

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
	     * @param callback  操作结果回调，详见 {@link CallBack}。
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
	     * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void ChangeGroupOwner(string groupId, string newOwner, CallBack callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);
			jo_param.Add("owner", newOwner);

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "updateGroupOwner", jo_param, (null != callback) ? callback.callbackId : "");
		}

		/**
	     * \~chinese
	     * 检查当前用户是否在群组白名单中。
	     *
	     * 异步方法。
	     *
	     * @param groupId 	群组 ID。
	     * @param callback  操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Gets whether the current user is on the allow list of the group.
	     *
	     * This is an asynchronous method.
	     *
	     * @param groupId 	The group ID.
	     * @param callback  The operation callback. See {@link CallBack}.
	     */
		public void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> callback = null)
        {
			JSONObject jo_param = new JSONObject();
			jo_param.Add("groupId", groupId);

			callbackManager.AddCallbackAction<bool>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "checkIfInGroupWhiteList", jo_param, (null != callback) ? callback.callbackId : "");
		}
	}
}