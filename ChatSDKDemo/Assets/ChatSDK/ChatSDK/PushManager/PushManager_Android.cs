using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    public class PushManager_Android : IPushManager
    {

        private AndroidJavaObject wrapper;

        public PushManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMPushManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public void GetNoDisturbGroupsFromServer(ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getNoDisturbGroups", handle?.callbackId);
        }

        public void GetPushConfig(ValueCallBack<PushConfig> handle = null)
        {
            wrapper.Call("getImPushConfig", handle?.callbackId);
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            wrapper.Call("getImPushConfigFromServer", handle?.callbackId);
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            wrapper.Call("updateGroupPushService", groupId, noDisturb, handle?.callbackId);
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            wrapper.Call("imPushNoDisturb", noDisturb, startTime, endTime, handle?.callbackId);
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            wrapper.Call("updateImPushStyle", pushStyle == PushStyle.Simple ? 0 : 1 , handle?.callbackId);
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            wrapper.Call("updateFCMPushToken", token, handle?.callbackId);
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            wrapper.Call("updateHMSPushToken", token, handle?.callbackId);
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            wrapper.Call("updatePushNickname", nickname, handle?.callbackId);
        }
    }
}