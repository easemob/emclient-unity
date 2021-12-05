using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IUserInfoManager
    {
        public abstract void UpdateOwnInfo(UserInfo userInfo, CallBack handle = null);
	    public abstract void FetchUserInfoByUserId(List<string> idList, ValueCallBack<Dictionary<string, UserInfo>> handle = null);
    }
}
