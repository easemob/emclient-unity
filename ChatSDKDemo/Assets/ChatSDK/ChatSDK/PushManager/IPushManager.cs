using System.Collections.Generic;

namespace ChatSDK
{
    /// <summary>
    /// 推送管理类
    /// </summary>
    public interface IPushManager
    {

        /// <summary>
        /// 获取免打扰群组列表
        /// </summary>
        /// <returns></returns>
        List<string> GetNoDisturbGroups();

        /// <summary>
        /// 从内存获取推送配置项,需要先从服务器获取后才会有值
        /// </summary>
        /// <returns></returns>
        PushConfig GetPushConfig();

        /// <summary>
        /// 从服务器获取推送配置项
        /// </summary>
        /// <param name="handle">返回结果</param>
        void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null);

        /// <summary>
        /// 设置推送昵称
        /// </summary>
        /// <param name="nickname">昵称</param>
        /// <param name="handle">返回结果</param>
        void UpdatePushNickName(string nickname, CallBack handle = null);

        /// <summary>
        /// 绑定华为deviceToken
        /// </summary>
        /// <param name="token">华为deviceToken</param>
        /// <param name="handle">返回结果</param>
        void UpdateHMSPushToken(string token, CallBack handle = null);

        /// <summary>
        /// 绑定谷歌deviceToken
        /// </summary>
        /// <param name="token">谷歌deviceToken</param>
        /// <param name="handle">返回结果</param>
        void UpdateFCMPushToken(string token, CallBack handle = null);


        /// <summary>
        /// 绑定苹果推送deviceToken
        /// </summary>
        /// <param name="token">苹果deviceToken</param>
        /// <param name="handle">返回结果</param>
        void UpdateAPNSPushToken(string token, CallBack handle = null);

        /// <summary>
        /// 设置推送免打扰
        /// </summary>
        /// <param name="noDisturb">是否免打扰</param>
        /// <param name="startTime">免打扰开始时间，如果是全天免打扰可以设置为0</param>
        /// <param name="endTime">免打扰结束时间，如果是全天免打扰可设置为24</param>
        /// <param name="handle">返回结果</param>
        void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null);

        /// <summary>
        /// 设置推送显示样式
        /// </summary>
        /// <param name="pushStyle">推送显示样式</param>
        /// <param name="handle">返回结果</param>
        void SetPushStyle(PushStyle pushStyle, CallBack handle = null);

        /// <summary>
        /// 设置群组免打扰
        /// </summary>
        /// <param name="groupId">免打扰群组id</param>
        /// <param name="noDisturb">是否免打扰</param>
        /// <param name="handle">返回结果</param>
        void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null);

        /// <summary>
        /// 将push结果发送给服务器
        /// </summary>
        /// <param name="parameters">json格式的push结果</param>
        /// <param name="handle">返回结果</param>
        void ReportPushAction(string parameters, CallBack handle = null);
    }

}