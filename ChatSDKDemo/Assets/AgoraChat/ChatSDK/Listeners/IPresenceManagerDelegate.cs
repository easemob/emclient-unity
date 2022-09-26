using System.Collections.Generic;

namespace AgoraChat
{
    /**
     *
     * \~chinese
     * 在线状态监听器。
     *
     * \~english
     * The presence state listener.
     */
    public interface IPresenceManagerDelegate
    {
        /**
         * \~chinese
         * 被订阅用户的在线状态发生变化。
         *
         * @param presences 被订阅用户新的在线状态。
         *
         *  \~english
         * Occurs when the presence state of a subscribed user changes.
         *
         * @param presences The new presence state of a subscribed user.
         */
        void OnPresenceUpdated(List<Presence> presences);
    }
}