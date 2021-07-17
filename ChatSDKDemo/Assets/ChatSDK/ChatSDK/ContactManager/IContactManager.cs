using System.Collections.Generic;
using UnityEngine;
namespace ChatSDK
{
    /// <summary>
    /// 联系人管理类
    /// </summary>
    public abstract class IContactManager
    {
        
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="username">对方id</param>
        /// <param name="reason">添加原因</param>
        /// <param name="handle">执行结果</param>
        public abstract void AddContact(string username, string reason = null, CallBack handle = null);

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="username">好友id</param>
        /// <param name="keepConversation"></param>
        /// <param name="handle">执行结果</param>
        public abstract void DeleteContact(string username, bool keepConversation = false, CallBack handle = null);

        /// <summary>
        /// 从服务器获取好友列表
        /// </summary>
        /// <param name="handle">执行结果</param>
        public abstract void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 从本地获取好友列表
        /// </summary>
        /// <returns>返回结果</returns>
        public abstract List<string> GetAllContactsFromDB();

        /// <summary>
        /// 添加用户到黑名单
        /// </summary>
        /// <param name="username">对方id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AddUserToBlockList(string username, CallBack handle = null);

        /// <summary>
        /// 从黑名单中移除
        /// </summary>
        /// <param name="username">黑名单id</param>
        /// <param name="handle">执行结果</param>
        public abstract void RemoveUserFromBlockList(string username, CallBack handle = null);

        /// <summary>
        /// 从服务器获取黑名单列表
        /// </summary>
        /// <param name="handle">执行结果</param>
        public abstract void GetBlockListFromServer(ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 同意好友申请
        /// </summary>
        /// <param name="username">对方id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AcceptInvitation(string username, CallBack handle = null);

        /// <summary>
        /// 拒绝好友申请
        /// </summary>
        /// <param name="username">对方id</param>
        /// <param name="handle">执行结果</param>
        public abstract void DeclineInvitation(string username, CallBack handle = null);

        /// <summary>
        /// 获取自己在其他设备上的信息
        /// </summary>
        /// <param name="handle">执行结果</param>
        public abstract void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 添加通讯录监听
        /// </summary>
        /// <param name="contactManagerDelegate">实现IContactManagerDelegate接口的对象</param>
        public void AddContactManagerDelegate(IContactManagerDelegate contactManagerDelegate)
        {
            CallbackManager.Instance().contactManagerDelegates.Add(contactManagerDelegate);
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().contactManagerDelegates.Clear();
        }
    }

}