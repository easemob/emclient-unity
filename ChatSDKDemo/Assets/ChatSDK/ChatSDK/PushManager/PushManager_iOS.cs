using System.Collections.Generic;

namespace ChatSDK
{
    public class PushManager_iOS : IPushManager
    {
        public void GetNoDisturbGroupsFromServer(ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void GetPushConfig(ValueCallBack<PushConfig> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAPNsDeviceToken(string token, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
