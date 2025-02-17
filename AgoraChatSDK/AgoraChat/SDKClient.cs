using System;
using System.Collections.Generic;

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
        internal IClient _clientImpl;

        public static SDKClient Instance
        {
            get
            {
                return _instance = _instance ?? new SDKClient();
            }
        }

        private SDKClient()
        {

            _clientImpl = new IClient(new NativeListener());
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
        public string SdkVersion { get => "1.3.1"; }


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
        * @param options  SDK 初始化选项，必填，详见 {@link Options}。
        * @return 返回初始化结果：
        * - `0`：成功；
        * - `100`：App Key 不合法。
        *
        * \~english
        * Initializes the SDK.
        * 
        * Make sure that the SDK initialization is complete before you call any methods.
        *
        * @param options The options for SDK initialization. Ensure that you set the options. See {@link Options}.
        * @return The return result:
        * - `0`: Success;
        * - `100`: The App key is invalid.
        */
        public int InitWithOptions(Options options)
        {
            return _clientImpl.InitWithOptions(options);
        }

        /**
         * \~chinese
         * 创建账号。
         *
         * 该方法不推荐使用，建议调用相应的 RESTful 方法。
         *
         * 异步方法。
         *
         * @param userId  用户 ID。该参数必填。用户 ID 不能超过 64 个字符，支持以下类型的字符：
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
         * @param callback    创建结果回调，详见 {@link CallBack}。                             
         *
         * \~english
         * Creates a new user.
         *
         * This method is not recommended and you are advised to call the RESTful API.
         *
         * This is an asynchronous method.
         *
         * @param userId The user ID. Ensure that you set this parameter.
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
         * @param callback  The creation result callback. See {@link CallBack}.          
         *             
         */
        public void CreateAccount(string userId, string password, CallBack callback = null)
        {
            _clientImpl.CreateAccount(userId, password, callback);
        }

        /**
         * \~chinese
         * 使用密码或 token 登录服务器。
         *
         * 异步方法。
         *
         * @param userId        用户 ID，必填。
         * @param pwdOrToken    用户密码或者 token。 该参数必填。
         * @param isToken       是否通过 token 登录。
         *                      - `true`：通过 token 登录。
         *                      - （默认） `false`：通过密码登录。
         * @param callback        登录结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Logs in to the chat server with a password or token.
         *
         * This is an asynchronous method.
         *
         * @param userId 		The user ID. Ensure that you set this parameter.
         * @param pwdOrToken 	The password or token of the user. Ensure that you set this parameter.
         * @param isToken       Whether to log in with a token or a password.
         *                      - `true`：Log in with a token.
         *                      - (Default) `false`：Log in with a password.
         * @param callback 	    The login result callback. See {@link CallBack}.
         *
         */
        [Obsolete]
        public void Login(string userId, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            _clientImpl.Login(userId, pwdOrToken, isToken, callback);
        }

        /**
         * \~chinese
         * 使用密码或 token 登录服务器。
         *
         * 异步方法。
         *
         * @param userId        用户 ID，必填。
         * @param token         token。 该参数必填。
         * @param callback        登录结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Logs in to the chat server with a password or token.
         *
         * This is an asynchronous method.
         *
         * @param userId 		The user ID. Ensure that you set this parameter.
         * @param token     	The token of the user. Ensure that you set this parameter.
         * @param callback 	    The login result callback. See {@link CallBack}.
         *
         */
        public void LoginWithToken(string userId, string token, CallBack callback = null)
        {
            _clientImpl.Login(userId, token, true, callback);
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
          * @param callback            退出结果回调，详见 {@link CallBack}。
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
          * @param callback 	    The logout result callback. See {@link CallBack}.
          *
          */
        public void Logout(bool unbindDeviceToken = true, CallBack callback = null)
        {
            _clientImpl.Logout(unbindDeviceToken, callback);
        }

        /**
        * \~chinese
        * 通过用户 ID 和声网 token 登录 chat 服务器。

        * 通过用户 ID 和密码登录 chat 服务器，详见 {@link #Login(string, string, bool, CallBack))}。
        *
        * 异步方法。
        *
        * 此方法已过时，建议使用Login方法
        *
        * @param userId        用户 ID，必填。
        * @param token         声网 token，必填。
        * @param callback      登录结果回调，详见 {@link CallBack}。
        *
        * \~english
        * Logs in to the chat server with the user ID and an Agora token.
        * 
        * You can also log in to the chat server with the user ID and a password. See {@link #Login(string, string, bool, CallBack)}.
        *
        * This an asynchronous method.
        *
        * This method is obsolete; it is recommended to use the 'Login' method.
        *
        * @param userId        The user ID. Ensure that you set this parameter.
        * @param token         The Agora token. Ensure that you set this parameter.
        * @param callback      The login result callback. See {@link CallBack}.
        *
        */
        [Obsolete]
        public void LoginWithAgoraToken(string userId, string token, CallBack callback = null)
        {
            _clientImpl.LoginWithAgoraToken(userId, token, callback);
        }

        /**
         * \~chinese
         * 更新声网 token。
         *
         * 当用户通过声网 token 登录时，在 {@link IConnectionDelegate} 回调中收到 token 即将过期的通知时可更新 token，避免因 token 失效产生未知问题。
         *
         * 此方法已过时，建议使用 RenewToken 方法
         *
         * @param token 新的声网 token。
         *
         * \~english
         * Renews the Agora token.
         *
         * If you log in with an Agora token and are notified by a callback method {@link IConnectionDelegate} that the token is to be expired, you can call this method to update the token to avoid unknown issues caused by an invalid token.
         *
         * This method is deprecated. Use the 'RenewToken' method instead.
         *
         * @param token The new Agora token.
         */
        [Obsolete]
        public void RenewAgoraToken(string token)
        {
            _clientImpl.RenewToken(token);
        }

        /**
         * \~chinese
         * 更新token。
         *
         * 当用户通过token 登录时，在 {@link IConnectionDelegate} 回调中收到 token 即将过期的通知时可更新 token，避免因 token 失效产生未知问题。
         *
         * @param token 新的 token。
         *
         * \~english
         * Renews the token.
         *
         * If you log in with a token and are notified by a callback method {@link IConnectionDelegate} that the token is to be expired, you can call this method to update the token to avoid unknown issues caused by an invalid token.
         *
         * @param token The new token.
         */
        public void RenewToken(string token)
        {
            _clientImpl.RenewToken(token);
        }

        /**
         * \~chinese
         * 获取指定账号下登录的在线设备列表。
         *
         * 异步方法。
         *
         * @param userId        用户 ID。
         * @param password      密码。
         * @param callBack 		结果回调，成功时回调 {@link ValueCallBack#OnSuccessValue(Object)}，返回设备信息列表；
         * 						失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Gets the list of currently logged-in devices of a specified account.
         *
         * This is an asynchronous method.
         *
         * @param userId        The user ID.
         * @param password      The password.
         * @param callBack		The completion callback. If this call succeeds, calls {@link ValueCallBack#OnSuccessValue(Object)} to show device information list;
         * 						if this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
        public void GetLoggedInDevicesFromServer(string userId, string password, ValueCallBack<List<DeviceInfo>> callback = null)
        {
            _clientImpl.GetLoggedInDevicesFromServer(userId, password, callback);
        }

        /**
         * \~chinese
         * 获取指定账号下登录的在线设备列表。
         *
         * 异步方法。
         *
         * @param userId        用户 ID。
         * @param token         Token。
         * @param callBack 		结果回调，成功时回调 {@link ValueCallBack#OnSuccessValue(Object)}，返回设备信息列表；
         * 						失败时回调 {@link ValueCallBack#onError(int, String)}。
         *
         * \~english
         * Gets the list of currently logged-in devices of a specified account.
         *
         * This is an asynchronous method.
         *
         * @param userId        The user ID.
         * @param token         The token.
         * @param callBack		The completion callback. If this call succeeds, calls {@link ValueCallBack#OnSuccessValue(Object)} to show device information list;
         * 						if this call fails, calls {@link ValueCallBack#onError(int, String)}.
         */
        public void GetLoggedInDevicesFromServerWithToken(string userId, string token, ValueCallBack<List<DeviceInfo>> callback = null)
        {
            _clientImpl.GetLoggedInDevicesFromServerWithToken(userId, token, callback);
        }

        /**
         * \~chinese
         * 将指定账号登录的指定设备踢下线。
         *
         * 可通过 {@link #GetLoggedInDevicesFromServer()} 方法获取设备信息 {@link DeviceInfo}。
         *
         * 异步方法。
         *
         * @param userId    用户 ID。
         * @param password  用户的密码。
         * @param resource  设备 ID, 见 {@link DeviceInfo#Resource}。
         *
         * \~english
         * Forces the specified account to log out from the specified device.
         *
         * You can call {@link GetLoggedInDevicesFromServer()} to get the device information {@link DeviceInfo}.
         *
         * This is an asynchronous method.
         *
         * @param userId   The user ID.
         * @param password The password.
         * @param resource The device ID. See {@link DeviceInfo#Resource}.
         */
        public void KickDevice(string userId, string password, string resource, CallBack callback = null)
        {
            _clientImpl.KickDevice(userId, password, resource, callback);
        }

        /**
        * \~chinese
        * 将指定账号登录的指定设备踢下线。
        *
        * 可通过 {@link #GetLoggedInDevicesFromServer()} 方法获取设备信息 {@link DeviceInfo}。
        *
        * 异步方法。
        *
        * @param userId    用户 ID。
        * @param token     Token。
        * @param resource  设备 ID, 见 {@link DeviceInfo#Resource}。
        *
        * \~english
        * Forces the specified account to log out from the specified device.
        *
        * You can call {@link GetLoggedInDevicesFromServer()} to get the device information {@link DeviceInfo}.
        *
        * This is an asynchronous method.
        *
        * @param userId   The user ID.
        * @param token    The token.
        * @param resource The device ID. See {@link DeviceInfo#Resource}.
        */
        public void KickDeviceWithToken(string userId, string token, string resource, CallBack callback = null)
        {
            _clientImpl.KickDeviceWithToken(userId, token, resource, callback);
        }

        /**
         * \~chinese
         * 将指定账号登录的所有设备都踢下线。
         *
         * 异步方法。
         *
         * @param userId   用户 ID。
         * @param password 密码。
         * @param callback	操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Forces the specified account to log out from all devices.
         *
         * This is an asynchronous method.
         *
         * @param userId   The user ID.
         * @param password The password.
         * @param callback The operation callback. See {@link CallBack}.
         */
        public void KickAllDevices(string userId, string password, CallBack callback = null)
        {
            _clientImpl.KickAllDevices(userId, password, callback);
        }

        /**
         * \~chinese
         * 将指定账号登录的所有设备都踢下线。
         *
         * 异步方法。
         *
         * @param userId    用户 ID。
         * @param token     Token。
         * @param callback	操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Forces the specified account to log out from all devices.
         *
         * This is an asynchronous method.
         *
         * @param userId   The user ID.
         * @param token    The token.
         * @param callback The operation callback. See {@link CallBack}.
         */
        public void KickAllDevicesWithToken(string userId, string token, CallBack callback = null)
        {
            _clientImpl.KickAllDevicesWithToken(userId, token, callback);
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
        public void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
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

        [Obsolete]
        public void DeInit()
        {
            _clientImpl.CleanUp();
        }

        internal void ClearResource()
        {
            _clientImpl.ClearResource();
        }

        internal void DelegateTester()
        {
            _clientImpl.DelegateTesterRun();
        }
    }
}
