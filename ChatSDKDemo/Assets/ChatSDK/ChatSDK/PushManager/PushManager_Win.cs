using System.Collections.Generic;

namespace ChatSDK
{
    internal sealed class PushManager_Win : IPushManager
    {
        public List<string> GetNoDisturbGroups()
        {
            return null;
        }

        public PushConfig GetPushConfig()
        {
            return null;
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
            handle?.ClearCallback();
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            handle?.ClearCallback();
        }
    }
}