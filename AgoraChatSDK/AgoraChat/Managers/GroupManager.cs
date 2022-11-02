using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    /**
    * \~chinese
    * Ⱥ������������ࡣ
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
            listener.GroupManagerEvent += NativeEventHandle;
            delegater = new List<IGroupManagerDelegate>();
        }

		internal void NativeEventHandle(string method, JSONNode jsonNode)
		{

        }

        /**
         * \~chinese
         * �������Ⱥ�顣
         *
         * �첽������
         *
         * @param groupId	Ⱥ�� ID��
         * @param reason    ��������ԭ��
         * @param callback  �������ص������ {@link CallBack}��
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
	     * ������Ⱥ���롣
	     *
	     * �첽������
	     *
	     * @param groupId	Ⱥ�� ID��
	     * @param callback  ���ܽ���ص��������û�ͬ�������Ⱥ�飬��� {@link ValueCallBack}��
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
	     * ��׼��Ⱥ���롣
		 * 
         * ��Ⱥ���͹���Ա�ɵ��ô˷����� 
	     *
	     * �첽������
	     *
	     * @param groupId  	Ⱥ�� ID��
	     * @param username 	������ ID��
	     * @param callback  ��׼����ص������ {@link CallBack}��
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
	     * ���Ⱥ�����Ա��
		 *
	     * ��Ⱥ���ɵ��ô˷�����admin ��Ȩ�ޡ�
	     *
	     * �첽������
         *
	     * @param groupId	Ⱥ�� ID��
	     * @param memberId  �����ӵĹ���Ա ID��
	     * @param callback  ��ӽ���ص������ {@link CallBack}��
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
	     * ��Ⱥ��������³�Ա��

	     * ��Ⱥ���͹���Ա�ɵ��ô˷�����
	     *
	     * �첽������
	     *
	     * @param groupId       Ⱥ��� ID��
	     * @param members       Ҫ��ӵ��³�Ա�б��
	     * @param callback      ��ӽ���ص������ {@link CallBack}��
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
            jo_param.Add("members", JsonObject.JsonArrayFromStringList(members));

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_GROUPMANAGER, "addAdmin", jo_param, (null != callback) ? callback.callbackId : "");
        }

		/**
	     * \~chinese
	     * ��Ӱ�������

	     * ��Ⱥ���͹���Ա�ɵ��ô˷�����
	     *
	     * �첽������ 
	     *
	     * @param groupId 	Ⱥ�� ID��
	     * @param members 	Ҫ��ӵĳ�Ա�б��
	     * @param callback  ��ӽ���ص������ {@link CallBack}��
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
			jo_param.Add("members", JsonObject.JsonArrayFromStringList(members));

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "addWhiteList", jo_param, (null != callback) ? callback.callbackId : "");			
		}

		/**
	     * \~chinese
	     * ����Ⱥ��Ϣ��
		 * 
	     * ����Ⱥ��Ϣ���û�����Ⱥ��Ա�����޷�����Ⱥ��Ϣ��
	     *
	     * �첽������
	     *
	     * @param groupId	Ⱥ�� ID��
	     * @param callback  ���ν���ص������ {@link CallBack}��
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
         * ���û�����Ⱥ���������
		 * 
	     * �ɹ����ø÷����󣬸��û����ȱ��Ƴ���Ⱥ�飬Ȼ�����Ⱥ������������û��޷����ա�����Ⱥ��Ϣ��Ҳ�޷������ٴμ���Ⱥ�顣
		 * 
	     * ��Ⱥ���͹���Ա�ɵ��ô˷�����
	     *
	     * �첽������
	     *
	     * @param groupId 	Ⱥ��ID��
	     * @param members 	Ҫ������������û��б��
	     * @param callback  ��������ص������ {@link CallBack}��
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
			jo_param.Add("members", JsonObject.JsonArrayFromStringList(members));

			callbackManager.AddCallbackAction<Object>(callback, null);
			CWrapperNative.NativeCall(NAME_GROUPMANAGER, "blockMembers", jo_param, (null != callback) ? callback.callbackId : "");
		}

		/**
	     * \~chinese
	     * �޸�Ⱥ������
		 * 
	     * ��Ⱥ���͹���Ա�ɵ��ô˷�����
	     *
	     * �첽������
	     *
	     * @param groupId 	Ⱥ�� ID��
	     * @param desc 	    �޸ĺ��Ⱥ������
         * @param callback  ��������ص������ {@link CallBack}��
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
	     * �޸�Ⱥ�����ơ�
		 * 
	     * ��Ⱥ���͹���Ա�ɵ��ô˷�����
	     *
	     * �첽������
	     *
	     * @param groupId 	���޸����Ƶ�Ⱥ��� ID��
	     * @param name 	    �޸ĺ��Ⱥ�����ơ�
	     * @param callback  ��������ص������ {@link CallBack}��
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
	     * ת��Ⱥ������Ȩ��
		 * 
	     * ��Ⱥ���ɵ��ô˷�����
	     *
	     * �첽������
	     *
	     * @param groupId	Ⱥ�� ID��
	     * @param newOwner	�µ�Ⱥ����
	     * @param callback  ��������ص������ {@link CallBack}��
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
	     * ��鵱ǰ�û��Ƿ���Ⱥ��������С�
	     *
	     * �첽������
	     *
	     * @param groupId 	Ⱥ�� ID��
	     * @param callback  ��������ص������ {@link CallBack}��
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