using System;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace AgoraChat
{
    /**
     * \~chinese
     * 聊天设置类，用于定义 SDK 的各种参数和选项，例如，是否自动接受加好友邀请以及是否自动下载缩略图。
     * 
     * \~english
     * The chat setting class that defines parameters and options of the SDK, including whether to automatically accept friend invitations and whether to automatically download the thumbnail.
     */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class Options
    {
        /**
	     * \~chinese
	     * 创建 App 时在 console 后台上注册的 App 唯一识别符，即 App Key。
	     * 
	     * \~english
	     * The App Key you get from the console when creating a chat app. It is the unique identifier of your app.
	     */
        public string AppKey = "";

        /**
	     * \~chinese
	     * DNS 服务器的地址。
	     *
	     * \~english
	     * The URL of the DNS server.
	     */
        public string DNSURL = "";

        /**
	     * \~chinese
	     * IM 消息服务器地址。
         * 
         * 该地址在进行私有部署时实现数据隔离和数据安全时使用。
         * 
         * 如有需求，请联系商务。
         * 
	     *
	     * \~english
	     * The address of the IM server.
         * 
         * This address is used when you implement data isolation and data security during private deployment.
         * 
         * If you need the address, contact our business manager.
         * 
	     */
        public string IMServer = "";

        /**
	     * \~chinese
	     * REST 服务器地址。
         * 
         * 该地址在进行私有部署时实现数据隔离和数据安全时使用。
         * 
         * 如有需求，请联系商务。
	     * 
	     * \~english
	     * The address of the REST server. 
         * 
         * This address is used when you implement data isolation and data security during private deployment.
         * 
         * If you need the address, contact our business manager.
	     */
        public string RestServer = "";

        /**
	     * \~chinese
	     * IM 消息服务器的自定义端口号。
         * 
         * 该端口在进行私有部署时实现数据隔离和数据安全时使用。
         * 
         * 如有需求，请联系商务。
	     *
	     * \~english
	     * The custom port of the IM server.
         * 
         * This port is used when you implement data isolation and data security during private deployment.
         * 
         * If you need the port, contact our business manager.
	     */
        public int IMPort = 0;

        /**
	     * \~chinese
	     * 设置是否开启 DNS。
	     * - （默认） `true`：开启。
	     * - `false`：关闭。私有部署时需要关闭。
	     *
	     * \~english
	     * Whether to enable DNS.
	     * - (Default) `true`: Yes.
	     * - `false`: No. DNS should be disabled for private deployment.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool EnableDNSConfig = true;

        /**
         * \~chinese
         * 是否输出调试信息。
         * - `true`: SDK 会在日志里输出调试信息。
         * - （默认） `false`: SDK 不输出调试信息。
         *
         * \~english
         * Whether to output the debug information. 
         * - `true`: Yes. The debug information will be output as logs.
         * - (Default) `false`: No.
         */
        [MarshalAs(UnmanagedType.U1)]
        public bool DebugMode = false;

        /**
	     * \~chinese
	     * 是否开启自动登录。
	     * -（默认） `true`：开启；
	     * - `false`：关闭。
	     * 
	     * \~english
	     * Whether to enable automatic login.
	     * - (Default) `true`: Yes.
	     * - `false`: No.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool AutoLogin = true;

        /**
	     * \~chinese
	     * 是否自动接受加好友邀请。
         * - `true`：是。
	     * - （默认） `false`：否。
         * 
	     * \~english
	     * Whether to automatically accept friend invitations from other users. 
         * - `true`: Yes.
	     * - (Default) `false`: No.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool AcceptInvitationAlways = false;

        /**
	     * \~chinese
	     * 是否自动接受群组邀请。
         * - （默认） `true`：是。
	     * - `false`：否。
	     *
	     * \~english
	     * Whether to accept group invitations automatically. 
         * - (Default) `true`: Yes.
	     * - `false`: No.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool AutoAcceptGroupInvitation = false;

        /**
	     * \~chinese
	     * 是否需要接收方发送已读回执。
	     * - （默认） `true`：是；
	     * - `false`：否。
	     * 
	     * \~english
	     * Whether to require the read receipt. 
	     * - (Default) `true`: Yes; 
	     * - `false`: No.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool RequireAck = true;

        /**
	     * \~chinese
	     * 是否需要接收方发送送达回执。
	     * -（默认）`true`：是；
	     * - `false`：否。
	     * 
	     * \~english
	     * Whether to require the delivery receipt.
	     * - (Default) `true`: Yes; 
	     * - `false`: No.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool RequireDeliveryAck = false;

        /**
	     * \~chinese
         * 是否在退出（主动或被动）群组时删除该群组中在内存和本地数据库中的历史消息。
	     * - （默认） `true`: 是；
         * - `false`: 否 
         * 
         * \~english
	     * Whether to delete the historical group messages in the memory and local database when leaving the group (either voluntarily or passively). 
	     * - (Default) `true`: Yes;
	     * - `false`: No.
         */
        [MarshalAs(UnmanagedType.U1)]
        public bool DeleteMessagesAsExitGroup = true;

        /**
	     * \~chinese
         * 是否在退出（主动或被动）聊天室时删除该聊天室在内存和本地数据库中的历史消息。
	     * - （默认） `true`: 是；
         * - `false`：否
         * 
         * \~english
	     * Whether to delete the historical messages of the chat room in the memory and local database when leaving the chat room (either voluntarily or passively).
	     * - (Default)` true`: Yes.
	     * - `false`: No.
         */
        [MarshalAs(UnmanagedType.U1)]
        public bool DeleteMessagesAsExitRoom = true;

        /**
	     * \~chinese
         * 是否允许聊天室所有者离开聊天室。
	     * - （默认） `true`: 允许。离开聊天室后，聊天室所有者除了接收不到该聊天室的消息，其他权限不变。
         * - `false`: 不允许。 
         * 
         * \~english
	     * Whether to allow the chat room owner to leave the chat room.
	     * - (Default) `true`: Yes. When leaving the chat room, the chat room owner still has all privileges, except for receive messages in the chat room.
	     * - `false`: No.
         */
        [MarshalAs(UnmanagedType.U1)]
        public bool IsRoomOwnerLeaveAllowed = true;

        /**
	     * \~chinese
	     * 是否按服务器收到消息时间的倒序对消息排序。
	     * - （默认） `true`：是；
	     * - `false`：否。按消息创建时间的倒序排序。
	     * 
	     * \~english
	     * Whether to sort the messages in the reverse chronological order of the time when they are received by the server.
	     * - (Default) `true`: Yes;
	     * - `false`: No. Messages are sorted in the reverse chronological order of the time when they are created.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool SortMessageByServerTime = true;

        /**
	     * \~chinese
	     * 是否只通过 HTTPS 进行 REST 操作。
	     * - （默认） `true`：是；
	     * - `false`：否。支持 HTTPS 和 HTTP。
	     *
	     * \~english
	     * Whether only HTTPS is used for REST operations.
	     * - (Default) `true`: Only HTTPS is supported.
	     * - `false`: Both HTTP and HTTPS are allowed. 
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool UsingHttpsOnly = false;

        /**
	     * \~chinese
	     * 是否自动将消息附件上传到聊天服务器。
	     * -（默认）`true`：是；
	     * - `false`：否。。
	     *
	     * \~english
	     * Whether to upload the message attachments automatically to the chat server.
	     * - (Default)`true`: Yes;
	     * - `false`: No.
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool ServerTransfer = true;

        /**
	     * \~chinese
	     * 是否自动下载缩略图。
	     * - （默认） `true`：是；
	     * - `false`：否。
	     *
	     * \~english
	     * Whether to automatically download the thumbnail.
	     * - (Default) `true`: Yes;
	     * - `false`: No.
	     * 
	     */
        [MarshalAs(UnmanagedType.U1)]
        public bool IsAutoDownload = true;

        /**
        * \~chinese
        * Options 构造方法。
        * 
        * @param appKey  App Key。
        *
        * \~english
        * The options constructor.
        * 
        * @param appKey  The App Key.
        */
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

    }
}
