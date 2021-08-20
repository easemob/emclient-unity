using System.Collections.Generic;
namespace ChatSDK
{
    public interface IRoomManagerDelegate
    {
        /// <summary>
        /// 聊天室被解散
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="roomName">聊天室名称</param>
        void OnDestroyedFromRoom(string roomId, string roomName);

        /// <summary>
        /// 用户加入聊天室，需要环信后台开通才能收到
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="participant">加入用户的环信id</param>
        void OnMemberJoinedFromRoom(string roomId, string participant);

        /// <summary>
        /// 用户离开聊天室
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="roomName">聊天室名称</param>
        /// <param name="participant">离开用户环信id</param>
        void OnMemberExitedFromRoom(string roomId, string roomName, string participant);

        /// <summary>
        /// 被聊天室移除，被移除人才可以收到
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="roomName">聊天室名称</param>
        /// <param name="participant">被移除人环信id</param>
        void OnRemovedFromRoom(string roomId, string roomName, string participant);

        /// <summary>
        /// 用户被加入禁言列表
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="mutes">被禁言用户环信id</param>
        /// <param name="expireTime">禁言时长(暂不可用)</param>
        void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime);

        /// <summary>
        /// 用户从禁言列表中被移除
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="mutes">被移除用户id</param>
        void OnMuteListRemovedFromRoom(string roomId, List<string> mutes);

        /// <summary>
        /// 用户被设置为聊天室管理员
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="admin">管理员id</param>
        void OnAdminAddedFromRoom(string roomId, string admin);

        /// <summary>
        /// 管理员被移除
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="admin">被移除管理员id</param>
        void OnAdminRemovedFromRoom(string roomId, string admin);

        /// <summary>
        /// 聊天室创建人将聊天室转给其他人
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="newOwner">聊天室新拥有者</param>
        /// <param name="oldOwner">聊天室旧拥有者</param>
        void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner);

        /// <summary>
        /// 聊天室描述变更
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="announcement">新的描述</param>
        void OnAnnouncementChangedFromRoom(string roomId, string announcement);
    }
}
