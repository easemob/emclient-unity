using System.Collections.Generic;

namespace ChatSDK
{
    /**
		 * \~chinese
		 * 推送管理器抽象类。
		 *
		 * \~english
		 * The abstract class for the push manager.
		 * 
		 */
    public abstract class IPushManager
    {
        /**
		 * \~chinese
		 * 获取免打扰模式下的群组列表。
		 *
		 * @return  免打扰模式下的群组 ID 列表。
		 * @deprecated 已废弃，请用 {@link getSilentModeForConversation替换，该方法用于获取会话的免打扰模式。
		 *
		 * \~english
		 * Gets the list of groups in do-not-disturb mode.
		 *
		 * @return The list of group IDs in do-not-disturb mode.
		 * @deprecated Deprecated. Use {@link getSilentModeForConversation instead, which gets the do-not-disturb settings for a conversation.
		 * 
		 */
        public abstract List<string> GetNoDisturbGroups();

        /**
         * \~chinese
         * 从内存中获取推送配置信息。
         * 
         * @return 推送配置信息。
         *
         * \~english
         * Gets the push configurations from the memory.
         * 
         * @return The push configurations.
         */
        public abstract PushConfig GetPushConfig();

        /**
         * \~chinese
         * 从服务器获取推送配置信息。
         *
         * 异步方法。
         *
         * @return          推送配置。
         * @param handle    操作结果回调，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets the push configurations from the server.
         *
         * This is an asynchronous method.
         *
         * @return The push configurations.
         * @param handle    The operation result callback. See {@link ValueCallBack}.
         */
        public abstract void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null);

        /**
	     * \~chinese
	     * 更新当前用户的推送昵称。
         * 
         * 异步方法。
         * 
         * 离线消息推送时显示推送称，而非用户信息中的昵称。
         * 
         * 建议这两种昵称的设置保持一致。因此，修改其中一个昵称时，也需调用相应方法对另一个进行更新，确保设置一致。
         *  
         * 更新用户信息中的昵称的方法，请参见 {@link IUserInfoManager#UpdateOwnInfo(UserInfo, CallBack)}。
         * 
	     *
	     * @param nickname 推送昵称。
	     * @param handle   操作结果回调，详见 {@link CallBack}。
	     *
	     * \~english
	     * Updates the push nickname of the current user.
         * 
         * This is an asynchronous method.
         * 
         * The push nickname, instead of the nickname in the user profile, is displayed during message push. 
         * 
         * We recommend that you use the same name for both nicknames. Therefore, if either nickname is updated, the other should be changed to the same name. 
         * 
         * To update the nickname in the user profile, you can call {@link IUserInfoManager#UpdateOwnInfo(UserInfo, CallBack)}.
         * 
         * 
	     * @param nickname  The push nickname.
	     * @param handle    The operation callback. See {@link CallBack}.
	     */        
        public abstract void UpdatePushNickName(string nickname, CallBack handle = null);

        /**
         * \~chinese
         * 更新华为推送 token。
         *
         * 该方法只支持华为设备。
         *
         * @param token    华为推送 token。
         * @param handle   操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Updates HuaWei push token.
         * 
         * This method applies to Huawei devices only.
         *
         * @param token     Huawei push token.
         * @param handle    The operation result callback. See {@link CallBack}.
         */
        public abstract void UpdateHMSPushToken(string token, CallBack handle = null);

        /**
         * \~chinese
         * 更新 Firebase 云信息传递服务（FCM）的 token。
         *
         * 该方法只支持谷歌服务设备。
         *
         * @param token     Firebase 云信息传递服务的 token。
         * @param handle    操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Updates the token for Firebase Cloud Messaging (FCM).
         * 
         * This method only applies to devices that support Google Play.
         *
         * @param token     The FCM token.
         * @param handle    The operation result callback. See {@link CallBack}.
         */
        public abstract void UpdateFCMPushToken(string token, CallBack handle = null);

        /**
         * \~chinese
         * 更新苹果推送通知服务（APNs）的 token。
         *
         * 该方法只支持 iOS 设备。
         *
         * @param token   APNs 的 token。
         * @param handle  操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Updates the token for the Apple Push Notification service (APNs).
         * 
         * This method applies to iOS devices only.
         *
         * @param token     The APNs token.
         * @param handle    The operation result callback. See {@link CallBack}.
         */       
        public abstract void UpdateAPNSPushToken(string token, CallBack handle = null);

        /**
         * \~chinese
         * 设置推送免打扰。
         *
         * @param noDisturb     是否开启免打扰模式。
         *                      - `true`: 是；
         *                      - `false`：否。
         * @param startTime     免打扰开始时间，精确到小时。
         *                      该时间为 24 小时制，取值范围为 [0,24]。
         *                      若全天免打扰，可将开始时间和结束时间分别设置为 `0` 和 `24`。
         * @param endTime       免打扰结束时间，精确到小时。
         *                      该时间为 24 小时制，取值范围为 [0,24]。
         *                      若全天免打扰，可将开始时间和结束时间分别设置为 `0` 和 `24`。
         * @return handle        操作结果回调，详见 {@link CallBack}。
		 * @deprecated 已废弃，请用 {@link SetSilentModeForAll} 替换。
         *
         * \~english
         * Sets the do-not-disturb mode for push.
         * 
         *
         * @param token         Whether to enable the do-not-disturb mode.
         *                      - `true`: Yes.
         *                      - `false`：No.
         * @param startTime     The start hour of the do-not-disturb mode.
         *                      The time is based on a 24-hour clock. The value range is [0,24].
         *                      For the all-day do-not-disturb mode, set the start hour (`startTime`) and end hour (`silentModeEnd`) of this mode as `0` and `24`respectively.
         * @param endTime       The end hour of the do-not-disturb mode. 
         *                      The time is based on a 24-hour clock. The value range is [0,24].
         *                      For the all-day do-not-disturb mode, set the start hour (`startTime`) and end hour (`silentModeEnd`) of this mode as `0` and `24`respectively.
         * @return handle        The operation result callback. See {@link CallBack}.
		 * @deprecated Deprecated. Use {@link SetSilentModeForAll} instead.
         */        
        public abstract void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null);

        /**
         * \~chinese
         * 设置推送通知的展示方式。
         *
         * @param pushStyle     推送通知的展示方式，详见 {@link PushStyle}。
         * @param handle        操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Sets the push style.
         *
         * @param pushStyle     The display style of push notifications. See {@link PushStyle}.
         * @param handle        The operation result callback. See {@link CallBack}.
         */        
        public abstract void SetPushStyle(PushStyle pushStyle, CallBack handle = null);

        /**
         * \~chinese
         * 设置指定群组的免打扰模式。
         *
         * @param groupId       群组 ID。
         * @param noDisturb     是否开启该群组的免打扰模式。
         *                      - `true`: 是；
         *                      - `false`：否。
         * @param handle        操作结果回调，详见 {@link CallBack}。
         * @deprecated 已废弃，请用 {@link SetSilentModeForAll} 替换
         *
         * \~english
         * Sets the do-not-disturb mode for the group.
         *
         * @param groupId       The group ID.
         * @param noDisturb     Whether to enable the do-not-disturb mode for the group.
         *                      - `true`: Yes.
         *                      - `false`：No.
         * @param handle        The operation result callback. See {@link CallBack}.
		 * @deprecated Deprecated. Use {@link SetSilentModeForAll} instead.
         */        
        public abstract void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null);

        /**
         * \~chinese
         * 设置 app 的免打扰模式。
         *
         * @param param 离线推送免打扰参数。详见 {@link SilentModeParam}.
         * @param handle 完成回调。
         *
         * \~english
         * Sets the do-not-disturb mode of the app.
         *
         * @param param The parameters of the do-not-disturb mode. See {@link SilentModeParam}.
         * @param handle The completion callback.
         */
        public abstract void SetSilentModeForAll(SilentModeParam param, ValueCallBack<SilentModeItem> handle = null);

        /**
         * \~chinese
         * 获取 app 的免打扰设置。
         *
         * @param handle 完成回调。
         *
         * \~english
         * Gets the do-not-disturb settings of the app.
         *
         * @param handle The completion callback.
         */
        public abstract void GetSilentModeForAll(ValueCallBack<SilentModeItem> handle = null);

        /**
         * \~chinese
         * 设置指定会话的免打扰模式。
         *
         * @param conversationId 会话 ID。
         * @param type 会话类型。详见 {@link ConversationType}。
         * @param param 离线推送免打扰参数。
         * @param handle 完成回调。
         *
         * \~english
         * Sets the do-not-disturb mode for the conversation.
         *
         * @param conversationId The conversation ID.
         * @param type The conversation type. See {@link ConversationType}.
         * @param param The parameters of the do-not-disturb mode for the offline message push.
         * @param handle The completion callback.
         */
        public abstract void SetSilentModeForConversation(string conversationId, ConversationType type, SilentModeParam param, ValueCallBack<SilentModeItem> handle = null);

        /**
         * \~chinese
         * 获取指定会话的免打扰设置。
         *
         * @param conversationId 会话 ID。
         * @param type 会话类型。详见 {@ConversationType}。
         * @param handle 完成回调。
         *
         * \~english
         * Gets the do-not-disturb settings of the conversation.
         *
         * @param conversationId The conversation ID.
         * @param type The conversation type. See {@ConversationType}.
         * @param haneld  The completion callback.
         */

        public abstract void GetSilentModeForConversation(string conversationId, ConversationType type, ValueCallBack<SilentModeItem> handle = null);

        /**
         * \~chinese
         * 批量获取指定会话的免打扰设置。
         *
         * @param conversations 会话列表。 格式：{"user":"user1,user2","group":"group1,group2"}
         * @param handle 完成回调。
         *
         * \~english
         * Gets the do-not-disturb settings of the specified conversations.
         *
         * @param conversations The conversation dictionary. Format: {"user":"user1,user2","group":"group1,group2"}
         * @param callBack The completion handle.
         */
        public abstract void GetSilentModeForConversations(Dictionary<string, string> conversations, ValueCallBack<Dictionary<string, SilentModeItem>> handle = null);

        /**
         * \~chinese
         * 设置用户推送翻译语言。
         *
         * @param languageCode 语言代码。
         * @param handle 完成回调。
         *
         * \~english
         * Sets the target translation language of offline push notifications.
         *
         * @param languageCode The language code.
         * @param handle The completion callback.
         */
        public abstract void SetPreferredNotificationLanguage(string languageCode, CallBack handle = null);

        /**
         * \~chinese
         * 获取设置的推送翻译语言。
         *
         * @param handle 完成回调。
         *
         * \~english
         * Gets the configured push translation language.
         *
         * @param handle The completion callback.
         */
        public abstract void GetPreferredNotificationLanguage(ValueCallBack<string> handle = null);

        /// <summary>
        /// 将push结果发送给服务器
        /// </summary>
        /// <param name="parameters">json格式的push结果</param>
        /// <param name="handle">返回结果</param>
        internal abstract void ReportPushAction(string parameters, CallBack handle = null);

    }

}