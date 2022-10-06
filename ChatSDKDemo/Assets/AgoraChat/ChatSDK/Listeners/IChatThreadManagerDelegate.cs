namespace AgoraChat
{
    public interface IChatThreadManagerDelegate
    {
        /**
        * \~chinese
        * 子区创建回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a message thread is created.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnChatThreadCreate(ChatThreadEvent threadEvent);

        /**
        * \~chinese
        * 子区更新回调。
	    *
	    * 修改子区名称或子区中添加或撤销回复消息时会触发该回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a message thread is updated.
		*
	    * This callback is triggered when the message thread name is changed or a threaded reply is added or recalled.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnChatThreadUpdate(ChatThreadEvent threadEvent);

        /**
        * \~chinese
        * 子区解散回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a message thread is destoryed.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnChatThreadDestroy(ChatThreadEvent threadEvent);

        /**
        * \~chinese
        * 当前登录用户被群主或群管理员移出子区。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
	    * Occurs when the current user is removed from the message thread by the group owner or a group admin to which the message thread belongs.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent);
    }
}
