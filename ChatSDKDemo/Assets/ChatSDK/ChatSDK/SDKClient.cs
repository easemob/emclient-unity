using System.Threading.Tasks;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
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
        private Options _Options;
        private static SDKClient _instance;
        private IClient _Sdk;

        public static SDKClient Instance
        {
            get
            {
                return _instance ?? (_instance = new SDKClient());
            }
        }

        /**
         * \~chinese
         * 聊天管理器实例。
         *
         * \~english
         * The chat manager instance.
         */
        public IChatManager ChatManager { get => _Sdk.ChatManager(); }

        /**
         * \~chinese
         * 好友管理器实例。
         *
         * \~english
         * The contact manager instance.
         */
        public IContactManager ContactManager { get => _Sdk.ContactManager(); }

        /**
         * \~chinese
         * 群组管理器实例。
         *
         * \~english
         * The group manager instance.
         */
        public IGroupManager GroupManager { get => _Sdk.GroupManager(); }

        /**
         * \~chinese
         * 聊天室管理器实例。
         *
         * \~english
         * The chat room manager instance.
         */
        public IRoomManager RoomManager { get => _Sdk.RoomManager(); }

        /**
         * \~chinese
         * 推送管理器实例。
         *
         * \~english
         * The push manager instance.
         */
        public IPushManager PushManager { get => _Sdk.PushManager(); }

        /**
         * \~chinese
         * 会话管理器实例。
         *
         * \~english
         * The conversation manager instance.
         */
        internal IConversationManager ConversationManager { get => _Sdk.ConversationManager(); }

        internal IMessageManager MessageManager { get => _Sdk.MessageManager();  }

        /**
         * \~chinese
         * 用户信息管理器实例。
         *
         * \~english
         * The user information manager instance.
         */
        public IUserInfoManager UserInfoManager { get => _Sdk.UserInfoManager(); }

        /**
         * \~chinese
         * 在线状态管理器实例。
         *
         * \~english
         * The presence manager instance.
         */
        public IPresenceManager PresenceManager { get => _Sdk.PresenceManager(); }

        /**
         * \~chinese
         * 子区管理器实例。
         *
         * \~english
         * The thread manager instance.
         */	
		public IChatThreadManager ThreadManager { get => _Sdk.ThreadManager(); }
        /**
         * \~chinese
         * SDK选项。
         *
         * \~english
         * The SDK options.
         */
        public Options Options { get { return _Options; } }

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
        public string CurrentUsername { get => _Sdk.CurrentUsername(); }

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
        public bool IsLoggedIn { get => _Sdk.IsLoggedIn(); }

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
        public bool IsConnected { get => _Sdk.IsConnected(); }

        /**
         * \~chinese
         * 当前用户的 token。
         *
         * \~english
         * The token of the current user.
         */
        public string AccessToken { get => _Sdk.AccessToken(); }

        /**
		 * \~chinese
		 * 注册连接监听器。
		 *
		 * @param connectionDelegate 		要注册的连接监听器，继承自 {@link IConnectionDelegate}。
		 *
		 * \~english
		 * Adds a connection listener.
		 *
		 * @param connectionDelegate 		The connection listener to add. It is inherited from {@link IConnectionDelegate}.
		 * 
		 */
        public void AddConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (!CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate)) {
                CallbackManager.Instance().connectionListener.delegater.Add(connectionDelegate);
            }
        }

        /**
		 * \~chinese
		 * 移除指定的连接监听器。
		 *
		 * @param connectionDelegate 		要移除的连接监听器，继承自 {@link IConnectionDelegate}。
		 *
		 * \~english
		 * Removes a connection listener.
		 *
		 * @param connectionDelegate 		The connection listener to remove. It is inherited from {@link IConnectionDelegate}.
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
		 * Adds a connection listener.
		 *
		 * @param multiDeviceDelegate 		The multi-device listener to add. It is inherited from {@link IMultiDeviceDelegate}.
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
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Remove(multiDeviceDelegate);
            }
        }
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
            CallbackManager.Instance();
            _Options = options;
            _Sdk.InitWithOptions(options);
            registerManagers();
        }

        /**
         * \~chinese
         * 创建账号。
         * 
         * 异步方法。
         *
         * @param username  用户 ID。该参数必填。用户 ID 不能超过 64 个字符，支持以下类型的字符：
         * - 26 个小写英文字母 a-z
         * - 26 个大写英文字母 A-Z
         * - 10 个数字 0-9
         * - "_", "-", "."
         * 
         * 用户 ID 不区分大小写，大写字母会自动转换为小写字母。
         * 
         * 用户的电子邮件地址和 UUID 不能作为用户 ID。
         * 
         * 可通过以下格式的正则表达式设置用户 ID：^[a-zA-Z0-9_-]+$。
         * 
         * @param password  密码，长度不超过 64 个字符。该参数必填。
         * @param handle    创建结果回调，详见 {@link CallBack}。                             
         *
         * \~english
         * Creates a new user.
         *
         * This is an asynchronous method.
         *
         * @param username The user ID. Ensure that you set this parameter. 
         * 
         * The user ID can contain a maximum of 64 characters of the following types:
         * - 26 lowercase English letters (a-z);
         * - 26 uppercase English letters (A-Z);
         * - 10 numbers (0-9);
         * - "_", "-", "."
         * 
         * The user ID is case-insensitive, so Aa and aa are the same user ID. 
         * 
         * The email address or the UUID of the user cannot be used as the user ID.
         * 
         * You can also set the user ID using a regular expression in the format of ^[a-zA-Z0-9_-]+$. 
         * 
         * @param password The password. The password can contain a maximum of 64 characters. Ensure that you set this parameter.
         * @param handle  The creation result callback. See {@link CallBack}.          
         *             
         */
        public void CreateAccount(string username, string password, CallBack handle = null)
        {
            _Sdk.CreateAccount(username, password, handle);
        }

        /**
         * \~chinese
         * 利用密码或环信 token 登录 chat 服务器。
         *
         * 异步方法。
         *
         * @param username      用户 ID，必填。
         * @param pwdOrToken    用户密码或者 token，必填。
         * @param isToken       是否通过 token 登录。
         *                      - `true`：通过 token 登录。
         *                      - （默认） `false`：通过密码登录。
         * @param handle        登录结果回调，详见 {@link CallBack}。 
         *
         * \~english
         * Logs in to the chat server with a password or an Easemob token.
         * 
         * This is an asynchronous method.
         *
         * @param username 		The user ID. Ensure that you set this parameter.
         * @param pwdOrToken 	The password or token. Ensure that you set this parameter.
         * @param isToken       Whether to log in with a token or a password. 
         *                      - `true`：A token is used.
         *                      - (Default) `false`：A password is used.
         * @param handle 	    The login result callback. See {@link CallBack}.
         *
         */
        public void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null)
        {
            _Sdk.Login(username, pwdOrToken, isToken, handle);
        }

        /**
         * \~chinese
         * 通过用户 ID 和声网 token 登录 chat 服务器。

         * 通过用户 ID 和密码登录 chat 服务器，详见 {@link #Login(string, string, bool, CallBack))}。
         *
         * 异步方法。
         *
         * @param username      用户 ID，必填。
         * @param token         声网 token，必填。
         * @param handle        登录结果回调，详见 {@link CallBack}。 
         *
         * \~english
         * Logs in to the chat server with the user ID and an Agora token.
         * 
         * You can also log in to the chat server with the user ID and a password. See {@link #Login(string, string, bool, CallBack)}.
         *
         * This an asynchronous method.
         *
         * @param username      The user ID. Ensure that you set this parameter.
         * @param token         The Agora token. Ensure that you set this parameter.
         * @param handle        The login result callback. See {@link CallBack}.
         *
         */
        public void LoginWithAgoraToken(string username, string token, CallBack handle = null)
        {
            _Sdk.LoginWithAgoraToken(username, token, handle);
        }

        /**
         * \~chinese
         * 更新声网 token。
         * 
         * 当用户通过声网 token 登录时，在 {@link IConnectionDelegate} 回调中收到 token 即将过期的通知时可更新 token，避免因 token 失效产生未知问题。
         *
         * @param token 新的声网 token。
         *
         * \~english
         * Renews the Agora token.
         * 
         * If you log in with an Agora token and are notified by a callback method {@link IConnectionDelegate} that the token is to be expired, you can call this method to update the token to avoid unknown issues caused by an invalid token.
         *
         * @param token The new Agora token.
         */
        public void RenewAgoraToken(string token)
        {
            _Sdk.RenewAgoraToken(token);
        }

        /**
         * \~chinese
         * 退出登录。
         *
         * 异步方法。
         *
         * @param unbindDeviceToken 登出时是否解绑 token。该参数仅对移动平台有效。
         *                           - `true`：是。 
         *                           - `false`：否。
         *                    
         * @param handle            退出结果回调，详见 {@link CallBack}。 
         *
         * \~english
         * Logs out of the chat app.
         *
         * This is an asynchronous method.
         *
         * @param unbindToken Whether to unbind the token upon logout. This parameter is available only to mobile platforms.
         *                    - `true`: Yes.
         *                    - `false`: No.
         *                    
         * @param handle 	    The logout result callback. see {@link CallBack}.
         * 
         */
        public void Logout(bool unbindDeviceToken, CallBack handle = null)
        {
            _Sdk.Logout(unbindDeviceToken, new CallBack(
                onSuccess: () => {
                    CallbackManager.Instance().CleanAllCallback();
                    handle?.Success?.Invoke();
                },
                onError:(code, desc) => {
                    handle?.Error?.Invoke(code, desc);
                }
                ));
        }

        private SDKClient()
        {
            _Sdk = IClient.Instance;
        }

        private void registerManagers() {
            _Sdk.ContactManager();
            _Sdk.ChatManager();
            _Sdk.GroupManager();
            _Sdk.RoomManager();
            _Sdk.ConversationManager();
            _Sdk.UserInfoManager();
            _Sdk.PushManager();
            _Sdk.PresenceManager();
            _Sdk.ThreadManager();
        }
    }
}





