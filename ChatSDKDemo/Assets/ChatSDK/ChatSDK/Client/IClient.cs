#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    internal abstract class IClient
    {
        static private bool _initialized = false;

        private static IClient instance;

        /**
        * \~chinese
        * 获取客户端实例。
        * 
        * @return 客户端实例对象。
        *
        * \~english
        * Gets a client instance.
        *
        * @return The client instance object.
        */
        public static IClient Instance
        {
            get
            {
                if (instance == null)
                {
                    CallbackManager.Instance();
#if UNITY_ANDROID && !UNITY_EDITOR
                    instance = new Client_Android();
#elif UNITY_IOS && !UNITY_EDITOR
                    instance = new Client_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
                    instance = new Client_Common();
#else
                    instance = new Client_Common();
#endif
                    _initialized = true;
                }

                return instance;
            }
        }

        private IChatManager chatImp = null;
        private IContactManager contactImp = null;
        private IGroupManager groupImp = null;
        private IRoomManager roomImp = null;
        private IConversationManager conversationImp = null;
        private IMessageManager messageManagerImp = null;
        private IUserInfoManager userInfoImp = null;
        private IPresenceManager presenceImp = null;
        private IChatThreadManager threadImp = null;

        /**
        * \~chinese
        * 获取聊天管理器实例。
        * 
        * @return 聊天管理器实例对象。
        *
        * \~english
        * Gets a chat manager instance.
        *
        * @return The chat manager instance object.
        */
        public IChatManager ChatManager()
        {
            if (_initialized == false) return null;
            if (chatImp != null) { return chatImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            chatImp = new ChatManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            chatImp = new ChatManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            chatImp = new ChatManager_Common(instance);
#else
            chatImp = new ChatManager_Common(instance);
#endif
            return chatImp;
        }

        /**
        * \~chinese
        * 获取联系人管理器实例。
        * 
        * @return 联系人管理器实例对象。
        *
        * \~english
        * Gets a contact manager instance.
        *
        * @return The contact manager instance object.
        */
        public IContactManager ContactManager()
        {
            if (_initialized == false) return null;
            if (contactImp != null) { return contactImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            contactImp = new ContactManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            contactImp = new ContactManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            contactImp = new ContactManager_Common(instance);
#else
            contactImp = new ContactManager_Common(instance);
#endif
            return contactImp;
        }

        /**
        * \~chinese
        * 获取群组管理器实例。
        * 
        * @return 群组管理器实例对象。
        *
        * \~english
        * Gets a group manager instance.
        *
        * @return The group manager instance object.
        */
        public IGroupManager GroupManager()
        {
            if (_initialized == false) return null;
            if (groupImp != null) { return groupImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            groupImp = new GroupManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            groupImp = new GroupManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            groupImp = new GroupManager_Common(instance);
#else
            groupImp = new GroupManager_Common(instance);
#endif
            return groupImp;
        }

        /**
        * \~chinese
        * 获取聊天室管理器实例。
        * 
        * @return 聊天室管理器实例对象。
        *
        * \~english
        * Gets a chat room manager instance.
        *
        * @return The chat room manager instance object.
        */
        public IRoomManager RoomManager()
        {
            if (_initialized == false) return null;
            if (roomImp != null) { return roomImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            roomImp = new RoomManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            roomImp = new RoomManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            roomImp = new RoomManager_Common(instance);
#else
            roomImp = new RoomManager_Common(instance);
#endif
            return roomImp;
        }



        /**
        * \~chinese
        * 获取会话管理器实例。
        * 
        * @return   会话管理器实例对象。
        *
        * \~english
        * Gets a conversation manager instance.
        *
        * @return   The conversation manager instance object.
        */
        internal IConversationManager ConversationManager()
        {
            if (conversationImp != null) { return conversationImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            conversationImp = new ConversationManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            conversationImp = new ConversationManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            conversationImp = new ConversationManager_Common(instance);
#else
            conversationImp = new ConversationManager_Common(instance);
#endif
            return conversationImp;
        }


        /**
        * \~chinese
        * 获取用户信息管理器实例。
        * 
        * @return   用户信息管理器实例对象。
        *
        * \~english
        * Gets a user information manager instance.
        *
        * @return   The user information manager instance object.
        */
        internal IUserInfoManager UserInfoManager()
        {
            if (userInfoImp != null) { return userInfoImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            userInfoImp = new UserInfoManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            userInfoImp = new UserInfoManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            userInfoImp = new UserInfoManager_Common(instance);
#else
            userInfoImp = new UserInfoManager_Common(instance);
#endif
            return userInfoImp;
        }
		
        /**
        * \~chinese
        * 获取消息管理器实例。
        * 
        * @return   消息管理器实例对象。
        *
        * \~english
        * Gets message manager instance.
        *
        * @return   The message manager instance object.
        */		
        internal IMessageManager MessageManager() {
            if (messageManagerImp != null) { return messageManagerImp; }
            
#if UNITY_ANDROID && !UNITY_EDITOR
            messageManagerImp = new MessageManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            messageManagerImp = new MessageManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            messageManagerImp = new MessageManager_Common();
#else
            messageManagerImp = new MessageManager_Common();
#endif
            
            return messageManagerImp;
        }		

        /**
        * \~chinese
        * 获取在线状态管理器实例。
        * 
        * @return   在线状态管理器实例对象。
        *
        * \~english
        * Gets a presence information manager instance.
        *
        * @return   The presence information manager instance object.
        */
        internal IPresenceManager PresenceManager()
        {
            if (presenceImp != null) { return presenceImp; }
#if UNITY_ANDROID && !UNITY_EDITOR
            presenceImp = new PresenceManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            presenceImp = new PresenceManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            presenceImp = new PresenceManager_Common(instance);
#else
            presenceImp = new PresenceManager_Common(instance);
#endif
            return presenceImp;
        }

        /**
        * \~chinese
        * 获取子区管理器实例。
        * 
        * @return   子区管理器实例对象。
        *
        * \~english
        * Gets a thread information manager instance.
        *
        * @return   The thread information manager instance object.
        */

        internal IChatThreadManager ThreadManager()
        {
            if (threadImp != null) { return threadImp; }

#if UNITY_ANDROID && !UNITY_EDITOR
            threadImp = new ChatThreadManager_Android();
#elif UNITY_IOS && !UNITY_EDITOR
            threadImp = new ChatThreadManager_iOS();
#elif UNITY_STANDALONE || UNITY_EDITOR
            threadImp = new ChatThreadManager_Common(instance);
#else
            threadImp = new ChatThreadManager_Common(instance);
#endif

            return threadImp;
        }
        /**
        * \~chinese
        * 初始化 SDK。
        * 
        * 调用任何其他方法前，请确保先调用该方法初始化 SDK。。
        * 
        * @param options SDK 初始化相关的配置项，必填。详见 {@link Options}。
        *
        * \~english
        * Initializes the SDK.
        * 
        * Before calling any other methods, ensure that you call this method to initialize the SDK first.
        *
        * @param options The options to be set for SDK initialization. Ensure that you set the options. See {@link Options}.
        */
        public abstract void InitWithOptions(Options options);

        /**
         * \~chinese
         * 创建账号。
         *
         * 异步方法。
         *
         * @param username  新账号的用户 ID，必填。
         *
         * 用户 ID 长度不超过 64 个字符，支持的字符集如下：
         * - 26 个小写英文字母 a-z
         * - 26 个大写英文字母 A-Z
         * - 10 个数字 0-9
         * - "_", "-", "."
         *
         * 该参数不区分大小写，大写字母会被自动转为小写字母。
         *
         * 如果使用正则表达式设置该参数，则可以将表达式写为：^[a-zA-Z0-9_-]+$。
         *
         * @param password  新账号的密码，必填。密码长度不超过 64 个字符。
         * @param handle    创建结果回调，详见 {@link CallBack}。                             
         *
         * \~english
         * Creates a user account.
         *
         * This is an asynchronous method.
         *
         * @param username The user ID of the new account. Ensure that you set this parameter. 
         *
         * The user ID can contain a maximum of 64 characters of the following types:
         * - 26 lowercase English letters (a-z)
         * - 26 uppercase English letters (A-Z)
         * - 10 numbers (0-9)
         * - "_", "-", "."
         *
         * The user ID is case-insensitive, so Aa and aa are the same user ID.
         *
         * You can also set this parameter with the regular expression ^[a-zA-Z0-9_-]+$. 
         *
         * @param password The password of the new account. It cannot exceed 64 characters. Ensure that you set this parameter.
         * @param handle  The creation result callback. See {@link CallBack}.          
         *             
         */
        public abstract void CreateAccount(string username, string password, CallBack handle = null);

        /**
         * \~chinese
         * 使用密码或 token 登录服务器。
         *
         * 异步方法。
         *
         * @param username      用户 ID，必填。
         * @param pwdOrToken    用户密码或者 token。 该参数必填。
         * @param isToken       是否通过 token 登录。
         *                      - `true`：通过 token 登录。
         *                      - （默认） `false`：通过密码登录。
         * @param handle        登录结果回调，详见 {@link CallBack}。 
         *
         * \~english
         * Logs in to the chat server with a password or token.
         * 
         * This is an asynchronous method.
         *
         * @param username 		The user ID. Ensure that you set this parameter.
         * @param pwdOrToken 	The password or token of the user. Ensure that you set this parameter.
         * @param isToken       Whether to log in with a token or a password. 
         *                      - `true`：Log in with a token.
         *                      - (Default) `false`：Log in with a password.
         * @param handle 	    The login result callback. See {@link CallBack}.
         *
         */    
        public abstract void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null);


        /**
         * \~chinese
         * 退出登录。
         *
         * 异步方法。
         *
         * @param unbindDeviceToken 退出时是否将设备与 token 解绑。该参数仅对移动平台有效。 
         *                           - `true`：是。 
         *                           - `false`：否。
         *                    
         * @param handle            退出结果回调，详见 {@link CallBack}。 
         *
         * \~english
         * Logs you out of the chat service.
         *
         * This is an asynchronous method.
         *
         * @param unbindToken Whether to unbind the device with the token upon logout. This parameter is valid only for mobile platforms.
         * - `true`: Yes.
         * - `false`: No.
         *                    
         * @param handle 	    The logout result callback. See {@link CallBack}.
         * 
         */
        public abstract void Logout(bool unbindDeviceToken = true, CallBack handle = null);

        /**
         * \~chinese
         * 获取当前登录用户 ID。
         *                
         * @return  返回当前登录用户 ID。
         *
         * \~english
         * Gets the ID of the logged-in user.
         *            
         * @return The ID of the logged-in user.
         * 
         */
        public abstract string CurrentUsername();

        /**
         * \~chinese
         * 检查 SDK 是否连接到 Chat 服务器。
         *
         * @return SDK 是否连接到 Chat 服务器。
         *         - `true`：是；
         *         - `false`：否。
         *
         * \~english
         * Checks whether the SDK is connected to the chat server.
         *
         * @return Whether the SDK is connected to the chat server.
         *         - `true`: Yes.
         *         - `false`: No.
         */
        public abstract bool IsConnected();

        /**
         *\~chinese
         * 检查用户是否已登录 Chat 服务。
         *
         * @return 用户是否已登录 Chat 服务。
         *         - `true`：是；
         *         - `false`：否。
         * \~english
         * Checks whether the user is logged in to the chat service.
         *
         * @return Whether the user is logged in to the chat service.
         *         - `true`: Yes.
         *         - `false`: No. 
         */
        public abstract bool IsLoggedIn();

        /**
         * \~chinese
         * 从内存中获取身份认证 token。
         *
         * @return 身份认证 token。
         *
         * \~english
         * Gets the access token from the memory.
         *
         * @return  The access token.
         */
        public abstract string AccessToken();

        /**
         * \~chinese
         * 通过用户 ID 和声网 token 登录 chat 服务器。
         * 
         * 通过用户 ID 和密码登录 chat 服务器，详见 {@link #Login(string, string, bool, CallBack)}。
         *
         * 异步方法。
         *
         * @param username      用户 ID，必填。
         * @param token         声网 token，必填。
         * @param handle        登录结果回调，详见 {@link CallBack}。 
         *
         * \~english
         * Logs in to the chat server with a user ID and an Agora token. 
         *
         * You can also use your user ID and password to log in to the chat server. See {@link #Login(string, string, bool, CallBack)}.
         *
         * This an asynchronous method.
         *
         * @param username      The user ID. Ensure that you set this parameter.
         * @param token         The Agora token. Ensure that you set this parameter.
         * @param handle        The login result callback. See {@link CallBack}.
         *
         */
        public abstract void LoginWithAgoraToken(string username, string token, CallBack handle = null);

        /**
         * \~chinese
         * 更新 token。
         *
         * 当用户利用声网 token 登录的情况下在 {@link IConnectionDelegate} 类中收到 token 即将过期事件的回调通知时，可调用该方法更新 token，避免因 token 失效产生的未知问题。
         *
         * @param token 声网 token。
         *
         * \~english
         * Renews the Agora token.
         *
         * If you log in with an Agora token and are notified by a callback method in {@link IConnectionDelegate} that the token is to expire, you can call this method to renew the token to avoid unknown issues caused by an invalid token.
         *
         * @param token The new Agora token.
         */
        public abstract void RenewAgoraToken(string token);

        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void ClearResource();

        /**
		 * \~chinese
		 * 注册连接状态监听器。
		 *
		 * @param connectionDelegate 		要注册的连接状态监听器，继承自 {@link IConnectionDelegate}。
		 *
		 * \~english
		 * Adds a connection status listener.
		 *
		 * @param connectionDelegate 		The connection status listener to add. It is inherited from {@link IConnectionDelegate}.
		 * 
		 */
        public void AddConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (!CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate))
            {
                CallbackManager.Instance().connectionListener.delegater.Add(connectionDelegate);
            }
        }

        /**
		 * \~chinese
		 * 移除添加连接状态监听器。
		 *
		 * @param connectionDelegate 		要移除的连接状态监听器，继承自 {@link IConnectionDelegate}。
		 *
		 * \~english
		 * Removes a connection status listener.
		 *
		 * @param connectionDelegate 		The connection status listener to remove. It is inherited from {@link IConnectionDelegate}.
		 * 
		 */
        public void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate))
            {
                CallbackManager.Instance().connectionListener.delegater.Remove(connectionDelegate);
            }
        }

        /**
		 * \~chinese
		 * 注册多设备监听器。
		 *
		 * @param multiDeviceDelegate 		要注册的多设备监听器，继承自 {@link IMultiDeviceDelegate}。
		 *
		 * \~english
		 * Adds a multi-device listener.
		 *
		 * @param multiDeviceDelegate 		The multi-device listenser to add. It is inherited from {@link IMultiDeviceDelegate}.
		 * 
		 */
        public void AddMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            if (!CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Add(multiDeviceDelegate);
            }
        }

        /**
		 * \~chinese
		 * 移除多设备监听器。
		 *
		 * @param multiDeviceDelegate 		要移除的多设备监听器，继承自 {@link IMultiDeviceDelegate}。
		 *
		 * \~english
		 * Removes a multi-device listener.
		 *
		 * @param multiDeviceDelegate 		The multi-device listenser to remove. It is inherited from {@link IMultiDeviceDelegate}.
		 * 
		 */
        public void DeleteMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Remove(multiDeviceDelegate);
            }
        }

    }
}
