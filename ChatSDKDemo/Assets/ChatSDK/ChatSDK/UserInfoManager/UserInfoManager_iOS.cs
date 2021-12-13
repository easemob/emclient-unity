using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK {


    public class UserInfoManager_iOS : IUserInfoManager
    {
        // 暂不提供该方法
        internal void FetchUserInfoByAttribute(List<string> idList, List<UserInfoType> attrs, ValueCallBack<Dictionary<string, UserInfo>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void FetchUserInfoByUserId(List<string> idList, ValueCallBack<Dictionary<string, UserInfo>> handle = null)
        {
            string jsonString = TransformTool.JsonStringFromStringList(idList);
            UserInfoManagerNative.UserInfoManager_MethodCall("fetchUserInfoByUserId", jsonString, callbackId: handle?.callbackId);
        }


        // 暂不提供该方法
        internal void UpdateOwnByAttribute(UserInfoType userInfoType, string value, ValueCallBack<string> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateOwnInfo(UserInfo userInfo, CallBack handle = null)
        {
            string jsonString = TransformTool.JsonStringFromUserInfo(userInfo);
            UserInfoManagerNative.UserInfoManager_MethodCall("updateOwnInfo", jsonString, callbackId:handle?.callbackId);
        }
    }


    class UserInfoManagerNative
    {
        [DllImport("__Internal")]
        internal extern static void UserInfoManager_MethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string UserInfoManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}

