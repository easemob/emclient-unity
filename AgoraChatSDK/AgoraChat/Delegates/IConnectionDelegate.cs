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
         * @param i 断开连接的错误码。
         *
         * \~english
         * Occurs when the SDK disconnects from the chat server.
         * 
         * The SDK disconnects from the chat server when you log out of the app or a network interruption occurs.
         * 
         * @param reason  The error code for the disconnection.
         * 
         */
        void OnDisconnected(DisconnectReason reason);

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