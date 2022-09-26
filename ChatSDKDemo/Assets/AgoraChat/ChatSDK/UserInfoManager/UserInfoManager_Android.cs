using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AgoraChat {
    public class UserInfoManager_Android : IUserInfoManager
    {
        private AndroidJavaObject wrapper;

        public UserInfoManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMUserInfoManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        // 暂不提供该方法
        internal void FetchUserInfoByAttribute(List<string> idList, List<UserInfoType> attrs, ValueCallBack<Dictionary<string, UserInfo>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchUserInfoByUserId(List<string> idList, ValueCallBack<Dictionary<string, UserInfo>> handle = null)
        {
            wrapper.Call("fetchUserInfoByUserId", TransformTool.JsonStringFromStringList(idList), handle?.callbackId);
        }

        // 暂不提供该方法
        internal void UpdateOwnByAttribute(UserInfoType userInfoType, string value, ValueCallBack<string> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateOwnInfo(UserInfo userInfo, CallBack handle = null)
        {
            wrapper.Call("updateOwnInfo", userInfo.ToJson().ToString(), handle?.callbackId);
        }
    }

}

