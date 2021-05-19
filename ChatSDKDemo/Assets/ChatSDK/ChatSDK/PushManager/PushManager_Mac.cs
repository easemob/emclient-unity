using System.Collections.Generic;

namespace ChatSDK
{
    public class PushManager_Mac : IPushManager
    {
        public void GetNoDisturbGroupsFromServer(ValueCallBack<List<string>> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public PushConfig GetPushConfig()
        {
            throw new System.NotImplementedException();
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetPushStyle(PushStyle pushStyle, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAPNsDeviceToken(string token, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateFCMPushToken(string token, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateHMSPushToken(string token, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePushNickName(string nickname, ValueCallBack<bool> callBack = null)
        {
            throw new System.NotImplementedException();
        }
    }
}