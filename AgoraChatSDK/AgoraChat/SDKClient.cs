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
         * SDK选项。
         *
         * \~english
         * The SDK options.
         */
        public Options Options { 
            get 
            { //return _Options;
                //TODO
                return null;
             } 
        }

        /**
         * \~chinese
         * SDK 版本号。
         *
         * \~english
         * The SDK version.
         */
        public string SdkVersion { get => "1.0.5"; }


        /**
         * \~chinese
         * 当前登录用户的 ID。
         *
         * \~english
         * The ID of the current login user.
         */
        //TODO
        public string CurrentUsername { get => "test"; }

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
       //TODO
        public bool IsLoggedIn { get => false; }

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
        //TODO
        public bool IsConnected { get => false; }

        /**
         * \~chinese
         * 当前用户的 token。
         *
         * \~english
         * The token of the current user.
         */
        //TODO
        public string AccessToken { get => ""; }

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

        public void DeInit()
        {
            _clientImpl.CleanUp();
        }

        //TO-DO: need to remove, just for testing
        public void TestCallBack(string manager, string method, string jstr)
        {
            _clientImpl.nativeListener.nativeListenerEvent(manager, method, jstr);
        }

    }
}
