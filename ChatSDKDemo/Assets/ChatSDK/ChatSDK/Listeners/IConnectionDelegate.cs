using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public interface IConnectionDelegate
    {
        /// <summary>
        /// 连接回调
        /// </summary>
        void OnConnected();

        /// <summary>
        /// 断开连接回调
        /// </summary>
        /// <param name="i">断开连接错误号</param>
        void OnDisconnected(int i);

        /// <summary>
        /// Token通知回调
        /// </summary>
        /// <param name="i">Token状态id</param>
        /// <param name="desc">Token状态描述</param>
        /// Note: Token状态id可以是以下值：
        /// 108 - TOKEN_EXPIRED
        /// 109 - TOKEN_WILL_EXPIRE
        void OnTokenNotificationed(int i, string desc);
    }

}