using System.Collections.Generic;

namespace ChatSDK
{
    public class PushManager_Mac : IPushManager
    {
        // Mac 不需要推送，直接返回；
        public void GetNoDisturbGroupsFromServer(ValueCallBack<List<string>> handle = null)
        {
            handle?.ClearCallback();
        }

        public void GetPushConfig(ValueCallBack<PushConfig> handle = null)
        {
            handle?.ClearCallback();
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            handle?.ClearCallback();
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdateAPNsDeviceToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
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
            handle?.ClearCallback();
        }
    }
}