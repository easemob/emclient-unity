namespace AgoraChat
{
    /**
     * \~chinese
     * SDK 客户端类是 Chat SDK 的入口，负责登录、登出及管理 SDK 与 chat 服务器之间的连接。
     *
     * \~english
     * The SDK client class, the entry of the chat SDK, defines how to log in to and log out of the chat app and how to manage the connection between the SDK and the chat server.
     */
    public class SDKClient
    {
        private static SDKClient _instance;
        private IClient _clientImpl;

        public static SDKClient Instance
        {
            get
            {
                return _instance ?? (_instance = new SDKClient());
            }
        }

        private SDKClient()
        {
            _clientImpl = new IClient();
        }


        /**
         * \~chinese
         * 聊天管理器实例。
         *
         * \~english
         * The chat manager instance.
         */
        public ChatManager ChatManager 
        {
            get => _clientImpl.chatManager;
        }

        /**
         * \~chinese
         * 好友管理器实例。
         *
         * \~english
         * The contact manager instance.
         */
        public ContactManager ContactManager 
        {
            get => _clientImpl.contactManager; 
        }

        /**
         * \~chinese
         * 群组管理器实例。
         *
         * \~english
         * The group manager instance.
         */
        public GroupManager GroupManager 
        { 
            get => _clientImpl.groupManager; 
        }

        /**
         * \~chinese
         * 聊天室管理器实例。
         *
         * \~english
         * The chat room manager instance.
         */
        public RoomManager RoomManager
        { 
            get => _clientImpl.roomManager; 
        }

        /**
         * \~chinese
         * 用户信息管理器实例。
         *
         * \~english
         * The user information manager instance.
         */
        public UserInfoManager UserInfoManager 
        { 
            get => _clientImpl.userInfoManager; 
        }

        /**
         * \~chinese
         * 在线状态管理器实例。
         *
         * \~english
         * The presence manager instance.
         */
        public PresenceManager PresenceManager 
        {
            get => _clientImpl.presenceManager;
        }

        /**
         * \~chinese
         * 子区管理器实例。
         *
         * \~english
         * The thread manager instance.
         */
        public ChatThreadManager ThreadManager 
        {
            get => _clientImpl.chatThreadManager; 
        }
        
        internal ConversationManager ConversationManager
        {
            get => _clientImpl.conversationManager;
        }

        internal MessageManager MessageManager
        {
            get => _clientImpl.messageManager;
        }

        /**
         * \~chinese
         * SDK 版本号。
         *
         * \~english
         * The SDK version.
         */
        public string SdkVersion { get => "1.0.8"; }


        /**
         * \~chinese
         * 当前登录用户的 ID。
         *
         * \~english
         * The ID of the current login user.
         */
        public string CurrentUsername { get => _clientImpl.CurrentUsername(); }

        /**
         * \~chinese
         * 是否已经登录。
         * - `true`: 已登录；
         * - `false`：未登录。
         *
         * \~english
         * Whether the current user is logged into the chat app.
         * - `true`: Yes.
         * - `false`: No. The current user is not logged into the chat app yet.
         */
        public bool IsLoggedIn { get => _clientImpl.IsLoggedIn(); }

        /**
         * \~chinese
         * SDK 是否连接到服务器。
         * - `true`: 已连接；
         * - `false`：未连接。
         *
         * \~english
         * Whether the SDK is connected to the server.
         * - `true`: Yes.
         * - `false`: No.
         */
        public bool IsConnected { get => _clientImpl.IsConnected(); }

        /**
         * \~chinese
         * 当前用户的 token。
         *
         * \~english
         * The token of the current user.
         */
        public string AccessToken { get => _clientImpl.AccessToken(); }

        /**
        * \~chinese
        * 初始化 SDK。
        * 
        * 请确保调用其他方法前，完成 SDK 初始化。
        * 
        * @param options  SDK 初始化选项，必填，请参见 {@link Options}。
        *
        * \~english
        * Initializes the SDK.
        * 
        * Make sure that the SDK initialization is complete before you call any methods.
        *
        * @param options The options for SDK initialization. Ensure that you set the options. See {@link Options}.
        */
        public void InitWithOptions(Options options)
        {
            _clientImpl.InitWithOptions(options);
        }

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
        public void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null)
        {
            _clientImpl.Login(username, pwdOrToken, isToken, handle);
        }

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
        public void Logout(bool unbindDeviceToken = true, CallBack handle = null)
        {
            _clientImpl.Logout(unbindDeviceToken, handle);
        }

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
            _clientImpl.AddConnectionDelegate(connectionDelegate);
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
        internal void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            _clientImpl.DeleteConnectionDelegate(connectionDelegate);
        }

        /**
          * \~chinese
          * 注册多设备监听器。
          *
          * @param multiDeviceDelegate 		要注册的多设备监听器，继承自 {@link IMultiDeviceDelegate}。
          *
          * \~english
          * Adds a connection listener.
          *
          * @param multiDeviceDelegate 		The multi-device listener to add. It is inherited from {@link IMultiDeviceDelegate}.
          *
          */

        public void AddMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            _clientImpl.AddMultiDeviceDelegate(multiDeviceDelegate);
        }

        /**
		 * \~chinese
		 * 移除指定的多设备监听器。
		 *
		 * @param multiDeviceDelegate 		要移除的多设备监听器，继承自 {@link IMultiDeviceDelegate}。
		 *
		 * \~english
		 * Removes a connection listener.
		 *
		 * @param multiDeviceDelegate 		The multi-device listener to remove. It is inherited from {@link IMultiDeviceDelegate}.
		 *
		 */
        public void DeleteMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            _clientImpl.DeleteMultiDeviceDelegate(multiDeviceDelegate);
        }

        public void DeInit()
        {
            _clientImpl.CleanUp();
        }
    }
}
