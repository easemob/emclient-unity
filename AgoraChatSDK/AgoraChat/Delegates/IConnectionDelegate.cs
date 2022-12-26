namespace AgoraChat
{
    /**
     * \~chinese
     * 连接回调接口。
     *
     * \~english
     * The connection callback interface.
     */
    public interface IConnectionDelegate
    {
        /**
         * \~chinese
         * SDK 成功连接到 chat 服务器时触发的回调。
         *
         * \~english
         * Occurs when the SDK successfully connects to the chat server.
         */
        void OnConnected();

        /**
         * \~chinese
         * SDK 与 chat 服务器断开连接时触发的回调。
         *
         * 断开连接的原因可能是你主动退出登录或网络中断。
         * 
         * \~english
         * Occurs when the SDK disconnects from the chat server.
         * 
         * The SDK disconnects from the chat server when you log out of the app or a network interruption occurs.
         * 
         */
        void OnDisconnected();

        /**
         *  \~chinese
         *  当前登录账号在其它设备登录时会接收到此回调。
         *
         *  \~english
         *  Occurs when the current user account is logged in to another device.
         */
        void OnLoggedOtherDevice();

        /**
         *  \~chinese
         *  当前登录账号已经被从服务器端删除时会收到该回调。
         *
         *  \~english
         *  Occurs when the current chat user is removed from the server.
         */
        void OnRemovedFromServer();

        /**
         *  \~chinese
         *  当前用户账号被禁用时会收到该回调。
         *
         *  \~english
         *  The delegate method will be invoked when the User account is forbidden.
         */
        void OnForbidByServer();

        /**
         *  \~chinese
         *  当前登录账号被强制退出时会收到该回调，有以下原因：
         *    - 密码被修改；
         *    - 登录设备数过多；
         *    - 服务被封禁;
         *    - 被强制下线;
         *
         *  \~english
         *  The delegate method will be invoked when current IM account is forced to logout with the following reasons:
         *    1. The password is modified;
         *    2. Logged in too many devices;
         *    3. User for forbidden;
         *    4. Forced offline.
         */
        void OnForcedToLogout();


        /**
         * \~chinese
         * Token 过期回调。
         *  
         * \~english
         * Occurs when the token has expired.
         */
        void OnTokenExpired();

        /**
         * \~chinese
         * Token 即将过期回调。
         *  
         * \~english
         * Occurs when the token is about to expire.
         */
        void OnTokenWillExpire();

    }

}