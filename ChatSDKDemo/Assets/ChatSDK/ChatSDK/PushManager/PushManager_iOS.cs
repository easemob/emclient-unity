using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;

namespace ChatSDK
{
    public class PushManager_iOS : IPushManager
    {
        public List<string> GetNoDisturbGroups()
        {
            //PushManagerNative.PushManager_HandleMethodCall("getNoDisturbGroups", handle?.callbackId);
            return null;
        }

        public void GetPushConfig(ValueCallBack<PushConfig> handle = null)
        {
            PushManagerNative.PushManager_HandleMethodCall("getPushConfig", handle?.callbackId);
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            PushManagerNative.PushManager_HandleMethodCall("getPushConfigFromServer", handle?.callbackId);
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("noDisturb", noDisturb);
            PushManagerNative.PushManager_HandleMethodCall("updateGroupPushService", obj.ToString(), handle?.callbackId);
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("noDisturb", noDisturb);
            obj.Add("startTime", startTime);
            obj.Add("endTime", endTime);
            PushManagerNative.PushManager_HandleMethodCall("PushNoDisturb", obj.ToString(), handle?.callbackId);
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("style", pushStyle == PushStyle.Simple ? 0 : 1 );
            PushManagerNative.PushManager_HandleMethodCall("updatePushStyle", obj.ToString(), handle?.callbackId);
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("nickname", nickname);
            PushManagerNative.PushManager_HandleMethodCall("updatePushNickname", obj.ToString(), handle?.callbackId);
        }
    }

    class PushManagerNative
    {
        [DllImport("__Internal")]
        internal extern static void PushManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string PushManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}
