using System.Collections.Generic;
namespace ChatSDK
{
    /**
    * \~chinese
    * 联系人管理类，用于添加、查询和删除联系人。
    * 
    * \~english
    * The contact manager class, which manages chat contacts such as adding, retrieving, and deleting contacts.
    */
    public abstract class IContactManager
    {

        /**
        * \~chinese
        * 添加好友。
        * 
        * 异步方法。
        *
        * @param username		要添加为好友的用户 ID。
        * @param reason		    添加好友的原因，即邀请消息。该参数可选，可设置为 `null` 或 ""。
        * @param handle     	结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Adds a new contact.
        *
        * This is an asynchronous method.
        * 
        * @param username		The user ID of the contact to add.
        * @param reason     	The invitation message. This parameter is optional and can be set to `null` or "".
        * @param handle	        The result callback. See {@link CallBack}.
        */
        public abstract void AddContact(string username, string reason = null, CallBack handle = null);

        /**
         * \~chinese
         * 删除联系人及其相关的会话。
         *
         * 异步方法。
         *
         * @param username          要删除的联系人的用户 ID。
         * @param keepConversation  是否保留要删除的联系人的会话。
   *                                - `true`：是；
   *                                - （默认）`false`：否。
         * @param handle     	    结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Deletes a contact and all the related conversations.
         *
         * This is an asynchronous method.
         *
         * @param username          The user ID of the contact to delete.
         * @param keepConversation  Whether to retain conversations of the contact to delete.
         *                          - `true`: Yes.
   *                                - (Default) `false`: No.
         * @param handle	        The result callback. See {@link CallBack}.
         */
        public abstract void DeleteContact(string username, bool keepConversation = false, CallBack handle = null);

        /**
        * \~chinese
        * 从服务器获取联系人列表。
        * 
        * 异步方法。
        *
        * @param handle   结果回调。成功则返回联系人列表，失败返回错误信息，详见 {@link ValueCallBack}。
        *
        * \~english
        * Gets the contact list from the server.
        * 
        * This is an asynchronous method.
        *
        * @param handle The result callback. If success, the SDK returns the list of contacts; if a failure occurs, the SDK returns the error information. See {@link ValueCallBack}.
        */
        public abstract void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null);

        /**
         * \~chinese
         * 从本地数据库获取联系人列表。
         *
         * @return 调用成功会返回好友列表，失败返回空列表。
         *
         * \~english
         * Gets the contact list from the local database.
         *
         * @return  If success, the SDK returns the list of contacts; if a failure occurs, the SDK returns an empty list.
         
         */
        public abstract List<string> GetAllContactsFromDB();

        /**
         * \~chinese
         * 将指定用户加入黑名单。

         * 你可以向黑名单中用户发消息，但是接收不到对方发送的消息。
         * 
         * 异步方法。
         *
         * @param username  要加入黑名单的用户的用户 ID。
         * @param handle    结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Adds a contact to the block list.

         * You can send messages to the users on the block list, but cannot receive messages from them.
         * 
         * This is an asynchronous method.
         *
         * @param username  The user ID of the contact to be added to the block list.
         * @param handle	The result callback. See {@link CallBack}.
         */
        public abstract void AddUserToBlockList(string username, CallBack handle = null);

        /**
         * \~chinese
         * 将指定用户移除黑名单。
         * 
         * 异步方法。
         *
         * @param username  要在黑名单中移除的用户 ID。
         * @param handle    结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Removes the contact from the block list.
         * 
         * This is an asynchronous method.
         *
         * @param username  The user ID of the contact to be removed from the block list.
         * @param handle	The result callback. See {@link CallBack}.
         */
        public abstract void RemoveUserFromBlockList(string username, CallBack handle = null);

        /**
         * \~chinese
         * 从服务器获取黑名单列表。
         * 
         * 异步方法。
         *
         * @param handle   结果回调。调用成功则返回黑名单列表，失败返回错误信息。详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets the block list from the server.
         * 
         * This is an asynchronous method.
         *
         * @param handle    The result callback. If success, the SDK returns the block list; if a failure occurs, the SDK returns the error information. See {@link ValueCallBack}.
         */
        public abstract void GetBlockListFromServer(ValueCallBack<List<string>> handle = null);

        /**
         * \~chinese
         * 接受加好友的邀请。
         *      
         * 异步方法。
         * 
         * @param username  发起好友邀请的用户 ID。
         * @param handle    结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Accepts a friend invitation.
         *
         * This an asynchronous method.
         *
         * @param username  The user who initiates the friend invitation.
         * @param handle    The result callback. See {@link CallBack}.
         */
        public abstract void AcceptInvitation(string username, CallBack handle = null);

        /**
         * \~chinese
         * 拒绝加好友的邀请。
         *      
         * 异步方法。
         *
         * @param username  发起好友邀请的用户 ID。
         * @param handle    该方法完成的回调，详见 {@link CallBack}。
         *
         * \~english
         * Declines a friend invitation.
         *      
         * This an asynchronous method.
         *
         * @param username  The user who initiates the friend invitation.
         * @param handle    The result callback. See {@link CallBack}.
         */
        public abstract void DeclineInvitation(string username, CallBack handle = null);

        /**
         * \~chinese
         * 获取登录用户在其他登录设备上唯一 ID。
         
         * 该 ID 由 user ID + "/" + resource 组成。
         * 
         * 异步方法。
         *
         * @param handle  该方法完成的回调。调用成功会返回用户在其他登录设备上的 ID；失败则返回错误信息。详见 {@link ValueCallBack}
         *
         * \~english
         * Gets the unique IDs of the current user on the other devices. 
         *
         * The ID is in the format of user ID + "/" + resource.
         *      
         * This is an asynchronous method.
         *
         * @param handle   The result callback. If success, the SDK returns the unique IDs of the current user on the other devices; if a failure occurs, the SDK returns the error information. See {@link ValueCallBack}.
                            
         * 
         */
        public abstract void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null);

        /**
		 * \~chinese
		 * 注册联系人监听器。
		 *
		 * @param contactManagerDelegate 		要注册的联系人监听器，继承自 {@link IContactManagerDelegate}。
		 *
		 * \~english
		 * Adds a contact listener.
		 *
		 * @param contactManagerDelegate 		The contact listener to add. It is inherited from {@link IContactManagerDelegate}.
		 * 
		 */
        public void AddContactManagerDelegate(IContactManagerDelegate contactManagerDelegate)
        {
            if (!CallbackManager.Instance().contactManagerListener.delegater.Contains(contactManagerDelegate))
            {
                CallbackManager.Instance().contactManagerListener.delegater.Add(contactManagerDelegate);
            }
        }

        /**
		 * \~chinese
		 * 移除联系人监听器。
		 *
		 * @param contactManagerDelegate 		要移除的联系人监听器，继承自 {@link IContactManagerDelegate}。
		 *
		 * \~english
		 * Removes a contact listener.
		 *
		 * @param contactManagerDelegate 		The contact listener to remove. It is inherited from {@link IContactManagerDelegate}.
		 * 
		 */
        public void RemoveContactManagerDelegate(IContactManagerDelegate contactManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().contactManagerListener.delegater.Contains(contactManagerDelegate))
            {
                CallbackManager.Instance().contactManagerListener.delegater.Remove(contactManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().contactManagerListener.delegater.Clear();
        }
    }

}