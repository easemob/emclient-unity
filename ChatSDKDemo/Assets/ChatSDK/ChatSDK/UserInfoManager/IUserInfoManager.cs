using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IUserInfoManager
    {
        /// <summary>
        /// 设置自己的用户属性
        /// </summary>
        /// <param name="userInfo">用户属性对象</param>
        /// <param name="handle">结果回调</param>
        public abstract void UpdateOwnInfo(UserInfo userInfo, CallBack handle = null);

        /// <summary>
        /// 根据id列表获取用户属性
        /// </summary>
        /// <param name="idList">用户id列表</param>
        /// <param name="handle">返回结果</param>
	    public abstract void FetchUserInfoByUserId(List<string> idList, ValueCallBack<Dictionary<string, UserInfo>> handle = null);
    }
}
