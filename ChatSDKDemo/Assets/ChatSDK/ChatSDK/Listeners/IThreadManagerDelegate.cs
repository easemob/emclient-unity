using System.Collections.Generic;

namespace ChatSDK
{
    public interface IThreadManagerDelegate
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
        void OnCreateThread(ThreadEvent threadEvent);

        // 暂时未使用，不会被触发
        //void OnUpdateMyThread(ThreadEvent threadEvent);

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
        void OnThreadNotifyChange(ThreadEvent threadEvent);

        /**
        * \~chinese
        * 用户离开子区回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a user leave the thread.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnLeaveThread(ThreadEvent threadEvent, ThreadLeaveReason reason);

        /**
        * \~chinese
        * 用户加入子区回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a user joined to the thread.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnMemberJoinedThread(ThreadEvent threadEvent);

        /**
        * \~chinese
        * 用户离开子区回调。
        *
        * 子区所属群组的所有成员均可收到该回调。
        *
        * \~english
        * Occurs when a user leave from the thread.
        *
        * Each member of the group to which the message thread belongs can receive the callback.
        */
        void OnMemberLeaveThread(ThreadEvent threadEvent);
    }
}
