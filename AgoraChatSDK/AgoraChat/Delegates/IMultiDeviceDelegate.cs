using System.Collections.Generic;

namespace AgoraChat
{
    /**
        * \~chinese
        * 多设备回调接口。
        *
        * \~english
        * The multi-device callback interface.
        * 
        */
    public interface IMultiDeviceDelegate
    {
        /**
         * \~chinese
         * 多端多设备联系人事件回调。
         *
         * @param operation 联系人事件，详见 {@link MultiDevicesOperation}。
         * @param target    联系人 ID。
         * @param ext       扩展信息。
         *
         * \~english
         * The callback for a multi-device contact event.
         *
         * @param operation The contact event. See {@link MultiDevicesOperation}.
         * @param target    The user ID of the contact.
         * @param ext       The extension information.
         */
        void OnContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext);

        /**
         * \~chinese
         * 多端多设备群组事件回调。
         *
         * @param operation     群组事件，详见 {@link MultiDevicesOperation}。
         * @param target        群组 ID。
         * @param usernames     操作目标 ID 列表。
         *
         * \~english
         * The callback for a multi-device group event.
         *
         * @param operation     The group event. See {@link MultiDevicesOperation}.
         * @param target        The group ID.
         * @param usernames     The target user ID(s) of the operation.
         */
        void OnGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames);

        /**
         * \~chinese
         * 多端多设备子区事件回调。
         *
         * @param operation     子区事件，详见 {@link MultiDevicesOperation}。
         * @param target        子区 ID。
         * @param usernames     操作目标 ID 列表。
         *
         * \~english
         * The callback for a multi-device thread event.
         *
         * @param operation     The thread event. See {@link MultiDevicesOperation}.
         * @param target        The thread ID.
         * @param usernames     The target user ID(s) of the operation.
         */
        void OnThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames);

        /**
         * \~chinese
         * 多端多设备单个会话删除漫游消息事件回调。
         *
         * @param conversationId    会话 ID。
         * @param deviceId          设备 ID。
         *
         * \~english
         * The callback for a multi-device event for deletion of historical messages in a conversation from the server.
         *
         * @param conversationId    The conversation ID.
         * @param deviceId          The device ID.
         */
        void OnRoamDeleteMultiDevicesEvent(string conversationId, string deviceId);

        /**
        * \~chinese
        * 多端多设备会话操作事件回调。
        *
        * @param operation         会话事件，详见 {@link MultiDevicesOperation}。
        * @param conversationId    会话 ID。
        * @param type              会话类型。
        *
        * \~english
        * The callback for a multi-device conversation event.
        *
        * @param operation         The conversation event. See {@link MultiDevicesOperation}.
        * @param conversationId    The conversation ID.
        * @param type              The conversation type.
        */
        void OnConversationMultiDevicesEvent(MultiDevicesOperation operation, string conversationId, ConversationType type);
    }

}