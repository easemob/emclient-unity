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
        void onContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext);

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
        void onGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames);

        /**
         * \~chinese
         * 多端多设备免打扰事件回调。
         *
         * @param data     免打扰事件数据。
         *
         * \~english
         * The callback for a multi-device do-not-disturb event.
         * 
         * @param data     The do-not-disturb event data.
         */
        void undisturbMultiDevicesEvent(string data);

        /**
         * \~chinese
         * 多端多设备子区事件回调。
         *
         * @param operation     群组事件，详见 {@link MultiDevicesOperation}。
         * @param target        群组 ID。
         * @param usernames     操作目标 ID 列表。
         *
         * \~english
         * The callback for a multi-device thread event.
         *
         * @param operation     The group event. See {@link MultiDevicesOperation}.
         * @param target        The group ID.
         * @param usernames     The target user ID(s) of the operation.
         */
        void onThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames);
    }

}