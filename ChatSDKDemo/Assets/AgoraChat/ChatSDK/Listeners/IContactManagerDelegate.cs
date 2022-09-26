using System;
namespace ChatSDK
{
    /**
         * \~chinese
         * 联系人管理器回调接口。
         * 
         * \~english
         * The contact manager callback interface.
         * 
         */
    public interface IContactManagerDelegate
    {
        /**
         * \~chinese
         * 添加联系人回调。
         *
         * @param username 添加的联系人。
         * 
         * \~english
         * Occurs when a user is added as a contact.
         *
         * @param username   The user ID of the new contact.
         */
        void OnContactAdded(string username);

        /**
         * \~chinese
         * 删除联系人回调。
         * @param username 删除的联系人。

         *
         * \~english
         * Occurs when a user is removed from a contact list.
         * 
         * @param username    The user ID of the removed contact.
         */
        void OnContactDeleted(string username);

        /**
         * \~chinese
         * 收到好友邀请回调。
         *
         * @param username 发起好友请求的用户。
         * @param reason   邀请消息。
         *
         * \~english
         * Occurs when a user receives a friend request.
         *
         * @param username    The ID of the user who initiates the friend request.
         * @param reason      The invitation message. 
         */
        void OnContactInvited(string username, string reason);

        /**
        * \~chinese
        * 好友请求被接受。
        *
        * @param username 发起好友请求的用户。
        *
        * \~english
        * Occurs when a friend request is approved.
        *
        * @param username The ID of the user who initiates the friend request.
        */
        void OnFriendRequestAccepted(string username);

        /**
         * \~chinese
         * 好友请求被拒绝。
         *
         * @param username 发起好友请求的用户。
         *
         * \~english
         * Occurs when a friend request is declined.
         *
         * @param username The ID of the user who initiates the friend request.
         */
        void OnFriendRequestDeclined(string username);
    }
}
