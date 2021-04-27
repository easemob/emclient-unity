using System;
namespace ChatSDK {
    public interface IContactManagerDelegate 
    {
        // 被[username]添加为好友
        void OnContactAdded(string username);

        // 被[username]从好友中删除
        void OnContactDeleted(string username);

        // 收到[username]的好友申请，原因是[reason]
        void OnContactInvited(string username, string reason);

        // 发出的好友申请被[username]同意
        void OnFriendRequestAccepted(string username);

        // 发出的好友申请被[username]拒绝
        void OnFriendRequestDeclined(string username);
    }
}
