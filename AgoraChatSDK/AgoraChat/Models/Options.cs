using System;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class Options : BaseModel
    {
        //TODO: need to add comments
        public string AppKey = "";
        public string DNSURL = "";
        public string IMServer = "";
        public string RestServer = "";
        public int IMPort = 0;
        public bool EnableDNSConfig = true;
        public bool DebugMode = false;
        public bool AutoLogin = true;
        public bool AcceptInvitationAlways = false;
        public bool AutoAcceptGroupInvitation = false;
        public bool RequireAck = true;
        public bool RequireDeliveryAck = false;
        public bool DeleteMessagesAsExitGroup = true;
        public bool DeleteMessagesAsExitRoom = true;
        public bool IsRoomOwnerLeaveAllowed = true;
        public bool SortMessageByServerTime = true;
        public bool UsingHttpsOnly = true;
        public bool ServerTransfer = true;
        public bool IsAutoDownload = true;

        // MeiZu
        private string mZAppId = "", mZAppKey = "";
        private bool enableMZPush = false;

        // OPPO
        private string oPPOAppKey = "", oPPOAppSecret = "";
        private bool enableOPPOPush = false;

        // XiaoMi
        private string miAppId = "", miAppKey = "";
        private bool enableMiPush = false;

        private bool enableVivoPush = false;

        // Google
        private string fCMId = "";
        private bool enableFCMPush = false;

        // Apple
        private string aPNsCerName = "";
        private bool enableAPNs = false;

        // HuaWei
        private bool enableHWPush = false;


        internal Options() { }
        internal Options(string json) : base(json) { }
        internal Options(JSONObject jo) : base(jo) { }

        internal override void FromJsonObject(JSONObject jo) { }

        internal override JSONObject ToJsonObject()
        {
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

            if (RestServer != null)
            {
                jo.Add("rest_server", RestServer);
            }

            if (IMServer != null)
            {
                jo.Add("im_server", IMServer);
            }

            if (IMPort != 0)
            {
                jo.Add("im_port", IMPort);
            }

            if (DNSURL != null)
            {
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

            return jo;
        }
    }
}

