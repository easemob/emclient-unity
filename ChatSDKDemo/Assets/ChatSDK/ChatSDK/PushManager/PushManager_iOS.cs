using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;

namespace ChatSDK
{
    internal sealed class PushManager_iOS : IPushManager
    {
        public List<string> GetNoDisturbGroups()
        {
            string jsonString = ChatAPIIOS.PushManager_GetMethodCall("getNoDisturbGroups");
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return TransformTool.JsonStringToStringList(jsonString);
        }

        public PushConfig GetPushConfig()
        {
            string jsonString = ChatAPIIOS.PushManager_GetMethodCall("getPushConfig", null, null);
            if(jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return new PushConfig(jsonString);
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            ChatAPIIOS.PushManager_HandleMethodCall("getPushConfigFromServer", null, handle?.callbackId);
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("noDisturb", noDisturb);
            ChatAPIIOS.PushManager_HandleMethodCall("updateGroupPushService", obj.ToString(), handle?.callbackId);
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("noDisturb", noDisturb);
            obj.Add("startTime", startTime);
            obj.Add("endTime", endTime);
            ChatAPIIOS.PushManager_HandleMethodCall("PushNoDisturb", obj.ToString(), handle?.callbackId);
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("style", pushStyle == PushStyle.Simple ? 0 : 1 );
            ChatAPIIOS.PushManager_HandleMethodCall("updatePushStyle", obj.ToString(), handle?.callbackId);
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdateAPNSPuthToken(string token, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("deviceToken", token);
            ChatAPIIOS.PushManager_HandleMethodCall("updateDeviceToken", obj.ToString(), handle?.callbackId);
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("nickname", nickname);
            ChatAPIIOS.PushManager_HandleMethodCall("updatePushNickname", obj.ToString(), handle?.callbackId);
        }

        public void ReportPushAction(string parameters, CallBack handle = null)
        {
            //TODO: add code
        }
    }

}
