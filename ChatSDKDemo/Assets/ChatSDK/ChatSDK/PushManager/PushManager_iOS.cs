using System.Collections.Generic;
using SimpleJSON;

namespace ChatSDK
{
    internal sealed class PushManager_iOS : IPushManager
    {
        public override List<string> GetNoDisturbGroups()
        {
            string jsonString = ChatAPIIOS.PushManager_GetMethodCall("getNoDisturbGroups");
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return TransformTool.JsonStringToStringList(jsonString);
        }

        public override PushConfig GetPushConfig()
        {
            string jsonString = ChatAPIIOS.PushManager_GetMethodCall("getPushConfig", null, null);
            if(jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return new PushConfig(jsonString);
        }

        public override void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            ChatAPIIOS.PushManager_HandleMethodCall("getPushConfigFromServer", null, handle?.callbackId);
        }

        public override void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("groupId", groupId);
            obj.Add("noDisturb", noDisturb);
            ChatAPIIOS.PushManager_HandleMethodCall("updateGroupPushService", obj.ToString(), handle?.callbackId);
        }

        public override void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("noDisturb", noDisturb);
            obj.Add("startTime", startTime);
            obj.Add("endTime", endTime);
            ChatAPIIOS.PushManager_HandleMethodCall("PushNoDisturb", obj.ToString(), handle?.callbackId);
        }

        public override void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("style", pushStyle == PushStyle.Simple ? 0 : 1 );
            ChatAPIIOS.PushManager_HandleMethodCall("updatePushStyle", obj.ToString(), handle?.callbackId);
        }

        public override void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public override void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public override void UpdateAPNSPushToken(string token, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("deviceToken", token);
            ChatAPIIOS.PushManager_HandleMethodCall("updateDeviceToken", obj.ToString(), handle?.callbackId);
        }

        public override void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("nickname", nickname);
            ChatAPIIOS.PushManager_HandleMethodCall("updatePushNickname", obj.ToString(), handle?.callbackId);
        }

        public override void SetSilentModeForAll(SilentModeParam param, ValueCallBack<SilentModeItem> handle = null)
        {
            //TODO
        }
        public override void GetSilentModeForAll(ValueCallBack<SilentModeItem> handle = null)
        {
            //TODO
        }
        public override void SetSilentModeForConversation(string conversationId, ConversationType type, SilentModeParam param, ValueCallBack<SilentModeItem> handle = null)
        {
            //TODO
        }
        public override void GetSilentModeForConversation(string conversationId, ConversationType type, ValueCallBack<SilentModeItem> handle = null)
        {
            //TODO
        }
        public override void GetSilentModeForConversations(Dictionary<string, string> conversations, ValueCallBack<Dictionary<string, SilentModeItem>> handle = null)
        {
            //TODO
        }
        public override void SetPreferredNotificationLanguage(string languageCode, CallBack handle = null)
        {
            //TODO
        }
        public override void GetPreferredNotificationLanguage(ValueCallBack<string> handle = null)
        {
            //TODO
        }

        internal override void ReportPushAction(string parameters, CallBack handle = null)
        {
            //TODO: add code
        }
    }

}
