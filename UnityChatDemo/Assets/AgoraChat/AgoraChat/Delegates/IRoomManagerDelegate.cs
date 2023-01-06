using System.Collections.Generic;

namespace AgoraChat
{
    /**
         * \~chinese
         * 聊天管理器回调接口。
         * 
         * \~english
         * 
         * The chat manager callback interface.
         */
    public interface IRoomManagerDelegate
    {
        /**
         * \~chinese
         * 聊天室被解散。
         * 
         * @param roomId        聊天室 ID。
         * @param roomName      聊天室名称。
         *
         * \~english
         * Occurs when the chat room is destroyed.
         * 
         * @param roomId        The chat room ID.
         * @param roomName      The chat room name.
         */
        void OnDestroyedFromRoom(string roomId, string roomName);


        /**
        * \~chinese
        * 聊天室加入新成员事件。
        * 
        * @param roomId        聊天室 ID。
        * @param participant   新成员的 ID。
        *
        * \~english
        * Occurs when a user joins the chat room.
        * 
        * @param roomId        The chat room ID.
        * @param participant   The user ID of the new member.
        */
        void OnMemberJoinedFromRoom(string roomId, string participant);

        /**
         * \~chinese
         * 聊天室成员主动退出事件。
         * 
         * @param roomId        聊天室 ID。
         * @param roomName      聊天室名称。
         * @param participant   退出的成员 ID。
         * 
         * \~english
         * Occurs when a member voluntarily leaves the chat room.
         * 
         * @param roomId       The chat room ID.
         * @param roomName     The name of the chat room.
         * @param participant  The user ID of the member who leaves the chat room.
         */
        void OnMemberExitedFromRoom(string roomId, string roomName, string participant);


        /**
         * \~chinese
         * 聊天室成员被移除。
         *
         * @param roomId        聊天室 ID。
         * @param roomName      聊天室名称。
         * @param participant   被移除人员 ID。
         *
         * \~english
         * Occurs when a member is removed from a chat room.
         *
         * @param roomId        The chat room ID.
         * @param roomName      The name of the chat room.
         * @param participant   The user ID of the member that is removed from a chat room.
         */
        void OnRemovedFromRoom(string roomId, string roomName, string participant);



        /**
         * \~chinese
         * 聊天室成员因为离线被移除。
         *
         * @param roomId        聊天室 ID。
         * @param roomName      聊天室名称。
         *
         * \~english
         * Occurs when a member is removed from a chat room because he or she gets offline.
         *
         * @param roomId        The chat room ID.
         * @param roomName      The name of the chat room.
         */
        void OnRemoveFromRoomByOffline(string roomId, string roomName);
        /**
         * \~chinese
         * 有成员被禁言。
         *
         * 禁言期间成员不能发送消息。
         *
         * @param chatRoomId    聊天室 ID。
         * @param mutes         禁言的成员。
         * @param expireTime    禁言有效期，单位为毫秒。
         *
         * \~english
         * Occurs when a chat room member is added to the mute list.
         *
         * The muted members cannot send messages during the mute duration.
         *
         * @param chatRoomId    The chat room ID.
         * @param mutes         The user ID(s) of the muted member(s).
         * @param expireTime    The mute duration in milliseconds.
         */
        void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime);

        /**
         * \~chinese
         * 有成员从禁言列表中移除。
         *
         * @param chatRoomId    聊天室 ID。
         * @param mutes         从禁言列表中移除的成员名单。
         *
         * \~english
         * Occurs when a chat room member is removed from the mute list.
         *
         * @param chatRoomId    The chat room ID.
         * @param mutes         The user ID(s) of member(s) that is removed from the mute list of the chat room.
         */
        void OnMuteListRemovedFromRoom(string roomId, List<string> mutes);

        /**
         * \~chinese
         * 有成员设置为管理员权限。
         *
         * @param roomId        聊天室 ID。
         * @param admin         设置为管理员的成员。
         *
         * \~english
         * Occurs when a regular chat room member is set as an admin.
         *
         * @param  roomId       The chat room ID.
         * @param  admin        The user ID of the member who is set as an admin.
         */
        void OnAdminAddedFromRoom(string roomId, string admin);

        /**
         * \~chinese
         * 移除管理员权限。
         *
         * @param  roomId       聊天室 ID。
         * @param  admin        被移除的管理员。
         *
         * \~english
         * Occurs when an admin is demoted to a regular member.
         *
         * @param  roomId       The chat room ID.
         * @param  admin        The user ID of the admin whose administrative permissions are removed.
         */
        void OnAdminRemovedFromRoom(string roomId, string admin);

        /**
         * \~chinese
         * 转移聊天室的所有权。
         *
         * @param roomId        聊天室 ID。
         * @param newOwner      新的聊天室所有者。
         * @param oldOwner      原聊天室所有者。
         *
         * \~english
         * Occurs when the chat room ownership is transferred.
         *
         * @param roomId        The chat room ID.
         * @param newOwner      The user ID of the new chat room owner.
         * @param oldOwner      The user ID of the previous chat room owner.
         */
        void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner);

        /**
         * \~chinese
         * 聊天室公告更新事件。
         *
         * @param roomId        聊天室 ID。
         * @param announcement  更新的聊天室公告。
         *
         * \~english
         * Occurs when the chat room announcement is updated.
         *
         * @param roomId        The chat room ID.
         * @param announcement  The updated announcement.
         */
        void OnAnnouncementChangedFromRoom(string roomId, string announcement);

        /**
         * \~chinese
         * 聊天室自定义属性（key-value）有更新。
         *
         * 聊天室所有成员会收到该事件。
         *
         * @param chatRoomId   聊天室 ID。
         * @param kv           更新的聊天室自定义属性。
         * @param from         操作者的用户 ID。
         *
         * \~english
         * The custom chat room attribute(s) is/are updated.
         *
         * All chat room members receive this event.
         *
         * @param chatRoomId   The chat room ID.
         * @param kv           The map of custom chat room attributes that are updated.
         * @param from         The user ID of the operator.
         */
        void OnChatroomAttributesChanged(string roomId, Dictionary<string, string> kv, string from);

        /**
         * \~chinese
         * 聊天室自定义属性被移除。
         *
         * 聊天室所有成员会收到该事件。
         *
         * @param chatRoomId   聊天室 ID。
         * @param keys         被移除的聊天室自定义属性 key 列表。
         * @param from         操作者用户 ID。
         *
         * \~english
         * The custom chat room attribute(s) is/are removed.
         *
         * All chat room members receive this event.
         *
         * @param chatRoomId   The chat room ID.
         * @param keys         The list of keys of custom chat room attributes that are removed.
         * @param from         The user ID of the operator.
         */
        void OnChatroomAttributesRemoved(string roomId, List<string> keys, string from);

        /**
        * \~chinese
        * 聊天室信息有更新。
        *
        * 聊天室的所有成员会收到该事件。
        *
        * @param room 聊天室对象。
        *
        * \~english
        * Occurs when the chat room specifications are changed.
        *
        * All chat room members receive this event.
        *
        * @param room The chat room instance.
        */
        void OnSpecificationChangedFromRoom(Room room);

        /**
         * \~chinese
         * 有成员加入白名单。
         *
         * 被添加的成员收到该事件。
         *
         * @param roomId 聊天室 ID。
         * @param members  白名单中增加的成员名单。
         *
         * \~english
         * Occurs when the chat room member(s) is/are added to the allow list.
         *
         * The members added to the allow list receive this event.
         *
         * @param roomId The chat room ID.
         * @param members  The member(s) added to the allow list.
         */
        void OnAddAllowListMembersFromChatroom(string roomId, List<string> members);

        /**
         * \~chinese
         * 有成员被移出白名单。
         *
         * 被移出白名单的成员会收到该事件。
         *
         * @param roomId 聊天室 ID。
         * @param members  移出白名单的成员。
         *
         * \~english
         * Occurs when the chat room member(s) is/are removed from the allow list.
         *
         * The members that are removed from the allow list receive this event.
         *
         * @param roomId The chat room ID.
         * @param members  The member(s) removed from the allow list.
         */
        void OnRemoveAllowListMembersFromChatroom(string roomId, List<string> members);

        /**
         * \~chinese
         * 全员禁言状态有变更。
         *
         * 聊天室所有成员会收到该事件。
         *
         * @param roomId 聊天室 ID。
         * @param isAllMuted    是否开启了全员禁言。
         *
         * \~english
         * Occurs when all members in the chat room are muted or unmuted.
         *
         * All chat room members receive this event.
         *
         * @param roomId The chat room ID.
         * @param isAllMuted    Whether all chat room members are muted or unmuted.
         */
        void OnAllMemberMuteChangedFromChatroom(string roomId, bool isAllMuted);
    }
}