using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class PushManager_Android : IPushManager
    {

        private AndroidJavaObject wrapper;

        public PushManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMPushManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public List<string> GetNoDisturbGroups()
        {
            string jsonString = wrapper.Call<string>("getNoDisturbGroups");
            return TransformTool.JsonStringToStringList(jsonString);
        }

        public PushConfig GetPushConfig()
        {
            string jsonString = wrapper.Call<string>("getPushConfig");
            if (jsonString == null) {
                return null;
            }
            return new PushConfig(jsonString);
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            wrapper.Call("getPushConfigFromServer", handle?.callbackId);
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            wrapper.Call("setGroupToDisturb", groupId, noDisturb, handle?.callbackId);
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            wrapper.Call("setNoDisturb", noDisturb, startTime, endTime, handle?.callbackId);
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            wrapper.Call("setPushStyle", pushStyle == PushStyle.Simple ? 0 : 1 , handle?.callbackId);
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            wrapper.Call("updateFCMPushToken", token, handle?.callbackId);
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            wrapper.Call("updateHMSPushToken", token, handle?.callbackId);
        }

        public void UpdateAPNSPuthToken(string token, CallBack handle = null) {
            handle?.ClearCallback();
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            wrapper.Call("updatePushNickname", nickname, handle?.callbackId);
        }

        public void ReportPushAction(string parameters, CallBack handle = null)
        {
            //TODO: add code
        }
    }
}