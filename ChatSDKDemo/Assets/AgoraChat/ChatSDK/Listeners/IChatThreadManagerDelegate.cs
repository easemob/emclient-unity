using System.Collections.Generic;

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
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a message thread is updated.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnChatThreadUpdate(ChatThreadEvent threadEvent);

        /**
        * \~chinese
        * 子区删除回调。
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
        * 用户被移除回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a user is kicked from thread.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent);
    }
}
