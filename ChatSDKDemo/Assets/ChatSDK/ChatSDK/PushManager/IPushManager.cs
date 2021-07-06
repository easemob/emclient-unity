using System.Collections.Generic;

namespace ChatSDK
{
    public interface IPushManager {

        List<string> GetNoDisturbGroups();

        PushConfig GetPushConfig();

        void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null);

        void UpdatePushNickName(string nickname, CallBack handle = null);

        void UpdateHMSPushToken(string token, CallBack handle = null);

        void UpdateFCMPushToken(string token, CallBack handle = null);

        void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null);

        void SetPushStyle(PushStyle pushStyle, CallBack handle = null);

        void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null);
    }    

}