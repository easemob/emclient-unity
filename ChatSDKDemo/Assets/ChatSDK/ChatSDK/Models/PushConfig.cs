namespace ChatSDK
{

    public class PushConfig
    {

        // MeiZu
        internal string MZAppId, MZAppKey;
        internal bool _EnableMZPush;

        // OPPO
        internal string OPPOAppKey, OPPOAppSecret;
        internal bool _EnableOPPOPush;

        // XiaoMi
        internal string MiAppId, MiAppKey;
        internal bool _EnableMiPush;

        // Google
        internal string FCMId;
        internal bool _EnableFCMPush;

        // Apple
        internal string APNsCerName;
        internal bool _EnableAPNs;

        // HuaWei
        internal bool _EnableHWPush;


        public void EnableMeiZuPush(string appId, string appKey)
        {
            _EnableMZPush = true;
            this.MZAppId = appId;
            this.MZAppKey = appKey;
        }

        public void EnableOPPOPush(string key, string secret)
        {
            _EnableOPPOPush = true;
            this.OPPOAppKey = key;
            this.OPPOAppSecret = secret;
        }

        public void EnableXiaoMiPush(string miAppId, string miAppKey)
        {
            _EnableMiPush = true;
            this.MiAppId = miAppId;
            this.MiAppKey = miAppKey;
        }

        public void EnableFCMPush(string fcmId)
        {
            _EnableFCMPush = true;
            this.FCMId = fcmId;
        }

        public void EnableApplePush(string apnsCerName)
        {
            _EnableAPNs = true;
            this.APNsCerName = apnsCerName;
        }

        public void EnableHuaWeiPush(bool enable)
        {
            _EnableHWPush = enable;
        }
    }
}
