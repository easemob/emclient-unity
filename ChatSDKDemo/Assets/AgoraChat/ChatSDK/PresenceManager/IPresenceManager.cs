using System.Collections.Generic;

namespace AgoraChat
{
    /**
     * \~chinese
     * 在线状态管理类，提供在线状态管理功能。
     *
     * \~english
     * The presence manager class that defines methods of managing the presence state.
     */
    public abstract class IPresenceManager
    {

        /**
         * \~chinese
         * 发布自定义在线状态。
         *
         * @param description    在线状态描述信息，可以为空字符串。
         * @param callBack       结果回调。如果该方法调用失败，会包含调用失败的原因。
         *
         * \~english
         * Publishes a custom presence state.
         *
         * @param ext            The description information of the presence state. It can be an empty string.
         * @param callBack       The result callback which contains the error message if this method fails.
         */
        public abstract void PublishPresence(string description, CallBack handle = null);

        /**
         * \~chinese
         * 订阅指定用户的在线状态。订阅成功后，在线状态变更时订阅者会收到回调通知。
         *
         * @param members  要订阅在线状态的用户 ID 数组。
         * @param expiry   订阅时长，单位为秒，最长不超过 2,592,000 (30×24×3600) 秒，即 30 天。
         * @param callBack 结果回调。如果该方法调用成功，会返回被订阅用户的当前状态，调用失败，会包含调用失败的原因。
         *
         * \~english
         * Subscribes to a user's presence state. If the subscription succeeds, the subscriber will receive the onPresenceUpdated callback when the user's presence state changes.
         *
         * @param members  The array of user IDs whose presence states you want to subscribe to.
         * @param expiry   The subscription duration in seconds. The duration cannot exceed 2,592,000 (30×24×3600) seconds, i.e., 30 days.
         * @param callBack The result callback which contains the error message if the method fails. Returns the current presence state of subscribed users if this method executes successfully.
         */
        public abstract void SubscribePresences(List<string> members, long expiry, ValueCallBack<List<Presence>> handle = null);

        /**
         * \~chinese
         * 取消订阅指定用户的在线状态。
         *
         * @param members  要取消订阅在线状态的用户 ID 数组。
         * @param callBack 结果回调。如果该方法调用失败，会包含调用失败的原因。
         *
         *\~english
         * Unsubscribes from the presence state of the unspecified users.
         *
         * @param members  The array of user IDs whose presence state you want to unsubscribe from.
         * @param callBack The result callback, which contains the error message if the method fails.
         */
        public abstract void UnsubscribePresences(List<string> members, CallBack handle = null);

        /**
         * \~chinese
         * 分页查询当前用户订阅了哪些用户的在线状态。
         *
         * @param pageNum  当前页码，从 1 开始。
         * @param pageSize 每页显示的被订阅用户数量。
         * @param callBack 结果回调，返回订阅的在线状态所属的用户 ID。若当前未订阅任何用户的在线状态，返回空列表。
         *
         *\~english
         * Uses pagination to get the list of users whose presence state you have subscribed to.
         *
         * @param pageNum  The current page number, starting from 1.
         * @param pageSize The number of subscribed users displayed on each page.
         * @param callBack The result callback, which contains user IDs whose presence state you have subscribed to. Returns an empty list if you do not subscribe to any user's presence state.
         */
        public abstract void FetchSubscribedMembers(int pageNum, int pageSize, ValueCallBack<List<string>> handle = null);

        /**
         * \~chinese
         * 查询指定用户的当前在线状态。
         *
         * @param members  用户 ID 数组，指定要查询哪些用户的在线状态。
         * @param callBack 结果回调，返回用户的在线状态。
         *
         * \~english
         * Gets the current presence state of the specified users.
         *
         * @param members  The array of user IDs whose current presence state you want to get.
         * @param callBack The result callback, which contains the current presence state of users you have subscribed to.
         */
        public abstract void FetchPresenceStatus(List<string> members, ValueCallBack<List<Presence>> handle = null);

        /**
         * \~chinese
         * 添加在线状态监听器。
         *
         * @param listener {@link IPresenceManagerDelegate} 要添加的在线状态监听器。
         *
         * \~english
         * Adds a presence state listener.
         *
         * @param listener {@link IPresenceManagerDelegate} The presence state listener to add.
         */
        public void AddPresenceManagerDelegate(IPresenceManagerDelegate presenceManagerDelegate)
        {
            if (!CallbackManager.Instance().presenceManagerListener.delegater.Contains(presenceManagerDelegate))
            {
                CallbackManager.Instance().presenceManagerListener.delegater.Add(presenceManagerDelegate);
            }
        }

        /**
         * \~chinese
         * 移除在线状态监听器。
         *
         * @param listener {@link IPresenceManagerDelegate} 要移除的在线状态监听器。
         *
         *\~english
         * Removes a presence listener.
         *
         * @param listener {@link IPresenceManagerDelegate} The presence state listener to remove.
         */
        public void RemovePresenceManagerDelegate(IPresenceManagerDelegate presenceManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().presenceManagerListener.delegater.Contains(presenceManagerDelegate))
            {
                CallbackManager.Instance().presenceManagerListener.delegater.Remove(presenceManagerDelegate);
            }
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().presenceManagerListener.delegater.Clear();
        }
    }
}