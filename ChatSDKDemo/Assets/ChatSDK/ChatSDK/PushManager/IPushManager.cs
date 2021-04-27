using System.Collections.Generic;

namespace ChatSDK
{
    public interface IPushManager {

        PushConfig GetPushConfig();

        void GetPushConfigFromServer(ValueCallBack<PushConfig> callBack = null);

        void UpdatePushNickName(string nickname, ValueCallBack<bool> callBack = null);

        void UpdateHMSPushToken(string token, ValueCallBack<bool> callBack = null);

        void UpdateFCMPushToken(string token, ValueCallBack<bool> callBack = null);

        void UpdateAPNsDeviceToken(string token, ValueCallBack<bool> callBack = null);

        void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, ValueCallBack<bool> callBack = null);

        void SetPushStyle(PushStyle pushStyle, ValueCallBack<bool> callBack = null);

        void SetGroupToDisturb(string groupId, bool noDisturb, ValueCallBack<bool> callBack = null);

        void GetNoDisturbGroupsFromServer(ValueCallBack<List<string>> callBack = null);

    }    

}