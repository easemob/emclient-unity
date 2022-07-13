using System;
using System.Collections.Generic;

namespace ChatSDK
{
    public interface IMultiDeviceDelegate
    {
        /// <summary>
        /// 多端多设备联系人事件
        /// </summary>
        /// <param name="operation">联系人事件类型</param>
        /// /// <param name="target">联系人ID</param>
        /// /// <param name="ext">联系人扩展信息</param>
        void onContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext);

        /// <summary>
        /// 多端多设备群组人事件
        /// </summary>
        /// <param name="operation">群组事件类型</param>
        /// <param name="target">群组ID号</param>
        /// <param name="usernames">操作目标 ID 列表</param>
        void onGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames);

        /// <summary>
        /// 多端多设备非打扰事件
        /// </summary>
        /// <param name="data">非打扰事件数据</param>
        void undisturbMultiDevicesEvent(string data);

        void onThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames);
    }

}