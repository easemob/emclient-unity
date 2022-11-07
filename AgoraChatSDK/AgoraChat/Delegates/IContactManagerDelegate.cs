namespace AgoraChat
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
         * @param userId 添加的联系人。
         * 
         * \~english
         * Occurs when a user is added as a contact.
         *
         * @param userId   The user ID of the new contact.
         */
        void OnContactAdded(string userId);

        /**
         * \~chinese
         * 删除联系人回调。
         * @param userId 删除的联系人。
         *
         * \~english
         * Occurs when a user is removed from a contact list.
         * 
         * @param userId    The user ID of the removed contact.
         */
        void OnContactDeleted(string userId);

        /**
         * \~chinese
         * 收到好友邀请回调。
         *
         * @param userId 发起好友请求的用户。
         * @param reason   邀请消息。
         *
         * \~english
         * Occurs when a user receives a friend request.
         *
         * @param userId    The ID of the user who initiates the friend request.
         * @param reason      The invitation message. 
         */
        void OnContactInvited(string userId, string reason);

        /**
        * \~chinese
        * 好友请求被接受。
        *
        * @param userId 发起好友请求的用户。
        *
        * \~english
        * Occurs when a friend request is approved.
        *
        * @param userId The ID of the user who initiates the friend request.
        */
        void OnFriendRequestAccepted(string userId);

        /**
         * \~chinese
         * 好友请求被拒绝。
         *
         * @param userId 发起好友请求的用户。
         *
         * \~english
         * Occurs when a friend request is declined.
         *
         * @param userId The ID of the user who initiates the friend request.
         */
        void OnFriendRequestDeclined(string userId);
    }
}