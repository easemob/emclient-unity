using System;
namespace ChatSDK {
    public interface IContactManagerDelegate 
    {
        /// <summary>
        /// 添加好友回调
        /// </summary>
        /// <param name="username">好友id</param>
        void OnContactAdded(string username);

        /// <summary>
        /// 删除好友回调
        /// </summary>
        /// <param name="username">好友id</param>
        void OnContactDeleted(string username);

        /// <summary>
        /// 收到好友申请
        /// </summary>
        /// <param name="username">申请id</param>
        /// <param name="reason">申请原因</param>
        void OnContactInvited(string username, string reason);

        /// <summary>
        /// 发出的好友申请被对方同意
        /// </summary>
        /// <param name="username">对方id</param>
        void OnFriendRequestAccepted(string username);

        /// <summary>
        /// 发出的好友申请被对方拒绝
        /// </summary>
        /// <param name="username">对方id</param>
        void OnFriendRequestDeclined(string username);
    }
}
