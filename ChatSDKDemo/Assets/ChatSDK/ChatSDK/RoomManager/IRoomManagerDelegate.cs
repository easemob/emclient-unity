using System.Collections.Generic;
namespace ChatSDK
{
    public interface IRoomManagerDelegate
    {
        // id是[roomId],名称是[roomName]的聊天室被销毁
        void OnChatRoomDestroyed(string roomId, string roomName);

        // 有用户[participant]加入id是[roomId]的聊天室
        void OnMemberJoined(string roomId, string participant);

        // 有用户[participant]离开id是[roomId]，名字是[roomName]的聊天室
        void OnMemberExited(string roomId, string roomName, string participant);

        // 用户[participant]被id是[roomId],名称[roomName]的聊天室删除
        void OnRemovedFromChatRoom(string roomId, string roomName, string participant);

        // id是[roomId]的聊天室禁言列表[mutes]有增加
        void OnMuteListAdded(string roomId, List<string> mutes, string expireTime);

        // id是[roomId]的聊天室禁言列表[mutes]有减少
        void OnMuteListRemoved(string roomId, List<string> mutes);

        // id是[roomId]的聊天室增加id是[admin]管理员
        void OnAdminAdded(string roomId, string admin);

        // id是[roomId]的聊天室移除id是[admin]管理员
        void OnAdminRemoved(string roomId, string admin);

        // id是[roomId]的聊天室所有者由[oldOwner]变更为[newOwner]
        void OnOwnerChanged(string roomId, string newOwner, string oldOwner);

        // id是[roomId]的聊天室公告变为[announcement]
        void OnAnnouncementChanged(string roomId, string announcement);
    }
}
