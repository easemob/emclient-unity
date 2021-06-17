using System;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Options
    {
        /// <summary>
        /// AppKey,需要到Console中获取
        /// </summary>
        public string AppKey = "";
        public string DNSURL = "";
        public string IMServer = "";
        public string RestServer = "";
        public int IMPort = 0;

        [MarshalAs(UnmanagedType.U1)]
        public bool EnableDNSConfig = true;

        /// <summary>
        /// Debug模式，会输出日志
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool DebugMode = false;

        /// <summary>
        /// 自定登录，再下次初始化时，SDK会直接登录上次未退出的账号，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool AutoLogin = true;

        /// <summary>
        /// 自动同意好友申请，当您在线时，如果有好友申请会自动同意，不在线时，等上线后自动同意, 默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool AcceptInvitationAlways = true;

        /// <summary>
        /// 自动同意群组邀请，当您在线时，如果有好友申请会自动同意，不在线时，等上线后自动同意，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool AutoAcceptGroupInvitation = true;



        /// <summary>
        /// 已读回执，允许您发送已读回执，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool RequireAck = true;

        /// <summary>
        /// 已送达回执，接收到消息时自动向消息发送方发送已送达回执，只对单聊消息有效，默认关闭
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool RequireDeliveryAck = false;

        /// <summary>
        /// 离开群组时是否删除对应群组消息，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool DeleteMessagesAsExitGroup = true;

        /// <summary>
        /// 离开聊天室时是否删除对应消息，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool DeleteMessagesAsExitRoom = true;

        /// <summary>
        /// 是否允许聊天室拥有者离开聊天室，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool IsRoomOwnerLeaveAllowed = true;

        /// <summary>
        /// 获取本地消息时按照服务器时间排序，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool SortMessageByServerTime = true;

        /// <summary>
        /// 只是用Https,默认关闭
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool UsingHttpsOnly = false;

        /// <summary>
        /// 使用环信附件存储服务，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool ServerTransfer = true;

        /// <summary>
        /// 自动下载语音和缩略图，默认开启
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool IsAutoDownload = true;

        /// <summary>
        /// 初始化Options
        /// </summary>
        /// <param name="appKey">AppKey,需要到Console中获取</param>
        public Options(string appKey)
        {
            AppKey = appKey;
        }

        internal string ToJsonString() {

            JSONObject jo = new JSONObject();
            jo["app_key"] = AppKey;
            jo["debug_mode"] = DebugMode;
            jo["auto_login"] = AutoLogin;
            jo["accept_invitation_always"] = AcceptInvitationAlways;
            jo["auto_accept_group_invitation"] = AutoAcceptGroupInvitation;
            jo["require_ack"] = RequireAck;
            jo["require_delivery_ack"] = RequireDeliveryAck;
            jo["delete_messages_as_exit_group"] = DeleteMessagesAsExitGroup;
            jo["delete_messages_as_exit_room"] = DeleteMessagesAsExitRoom;
            jo["is_room_owner_leave_allowed"] = IsRoomOwnerLeaveAllowed;
            jo["sort_message_by_server_time"] = SortMessageByServerTime;
            jo["using_https_only"] = UsingHttpsOnly;
            //jo["server_transfer"] = ServerTransfer;
            jo["is_auto_download"] = IsAutoDownload;


            jo.Add("enable_dns_config", EnableDNSConfig);

            if (RestServer != null) {
                jo.Add("rest_server", RestServer);
            }

            if (IMServer != null) {
                jo.Add("im_server", IMServer);
            }

            if (IMPort != 0) {
                jo.Add("im_port", IMPort);
            }


            if (DNSURL != null) {
                jo.Add("dns_url", DNSURL);
            }

            jo.Add("mz_app_id", mZAppId);
            jo.Add("mz_app_key", mZAppKey);
            jo.Add("enable_mz_push", enableMZPush);

            jo.Add("oppo_app_key", oPPOAppKey);
            jo.Add("oppo_app_secret", oPPOAppSecret);
            jo.Add("enable_oppo_push", enableOPPOPush);

            jo.Add("mi_app_id", miAppId);
            jo.Add("mi_app_key", miAppKey);
            jo.Add("enable_mi_push", enableMiPush);

            jo.Add("fcm_id", fCMId);
            jo.Add("enable_fcm_push", enableFCMPush);

            jo.Add("apns_cer_name", aPNsCerName);
            jo.Add("enable_apns", enableAPNs);

            jo.Add("enable_hw_push", enableHWPush);

            jo.Add("enable_vivo_push", enableVivoPush);

            return jo.ToString();
        }


        // MeiZu
        internal string mZAppId = "", mZAppKey = "";
        internal bool enableMZPush = false;

        // OPPO
        internal string oPPOAppKey = "", oPPOAppSecret = "";
        internal bool enableOPPOPush = false;

        // XiaoMi
        internal string miAppId = "", miAppKey = "";
        internal bool enableMiPush = false;

        internal bool enableVivoPush = false;

        // Google
        internal string fCMId = "";
        internal bool enableFCMPush = false;

        // Apple
        internal string aPNsCerName = "";
        internal bool enableAPNs = false;

        // HuaWei
        internal bool enableHWPush = false;


        /// <summary>
        /// 开启魅族推送
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        public void EnableMeiZuPush(string appId, string appKey)
        {
            enableMZPush = true;
            this.mZAppId = appId;
            this.mZAppKey = appKey;
        }

        /// <summary>
        /// 开启OPPO推送
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        public void EnableOPPOPush(string key, string secret)
        {
            enableOPPOPush = true;
            this.oPPOAppKey = key;
            this.oPPOAppSecret = secret;
        }

        /// <summary>
        /// 开启VIVO推送
        /// </summary>
        public void EnableVivoPush()
        {
            enableVivoPush = true;
        }

        /// <summary>
        /// 开启小米推送
        /// </summary>
        /// <param name="miAppId"></param>
        /// <param name="miAppKey"></param>
        public void EnableMiPush(string miAppId, string miAppKey)
        {
            enableMiPush = true;
            this.miAppId = miAppId;
            this.miAppKey = miAppKey;
        }

        /// <summary>
        /// 开启FCM推送
        /// </summary>
        /// <param name="fcmId"></param>
        public void EnableFCMPush(string fcmId)
        {
            enableFCMPush = true;
            this.fCMId = fcmId;
        }

        /// <summary>
        /// 开启APNs推送
        /// </summary>
        /// <param name="apnsCerName"></param>
        public void EnableApplePush(string apnsCerName)
        {
            enableAPNs = true;
            this.aPNsCerName = apnsCerName;
        }

        /// <summary>
        /// 开启华为推送
        /// </summary>
        /// <param name="enable"></param>
        public void EnableHuaWeiPush(bool enable)
        {
            enableHWPush = enable;
        }
    }
}
