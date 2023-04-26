using System;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine;
#endif

namespace AgoraChat
{
    /**
     * \~chinese
     * 聊天设置类，用于定义 SDK 的各种参数和选项，例如，是否自动接受加好友邀请以及是否自动下载缩略图。
     *
     * \~english
     * The chat setting class that defines parameters and options of the SDK, including whether to automatically accept friend invitations and whether to automatically download the thumbnail.
     */
    public class Options : BaseModel
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
        public bool DebugMode = false;

        /**
	     * \~chinese
	     * 是否开启自动登录。
	     * - `true`：开启；
	     * - （默认）`false`：关闭。
	     *
	     * \~english
	     * Whether to enable automatic login.
	     * -  `true`: Yes.
	     * -  (Default)`false`: No.
	     */
        public bool AutoLogin = false;

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
        public bool UsingHttpsOnly = true;

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
        public bool IsAutoDownload = true;


        /**
	     * \~chinese
	     * 设置区域代号，使用边缘节点时遵循区域限制
	     * -（默认）`GLOB`: 不限制区域。
	     *
	     * \~english
	     * Sets area code, will follow the area when using edge node.
	     * - (Default)`GLOB`: glob.
	     */
        public AreaCode AreaCode = AreaCode.GLOB;

        public string MyUUID = "";

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

        /*
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
        */

        internal Options() { }
        internal Options(bool is_json, string json) : base(json) { }
        internal Options(JSONObject jo) : base(jo) { }

        internal override void FromJsonObject(JSONObject jo) { }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("appKey", AppKey);
            jo.AddWithoutNull("debugMode", DebugMode);
            jo.AddWithoutNull("autoLogin", AutoLogin);
            jo.AddWithoutNull("acceptInvitationAlways", AcceptInvitationAlways);
            jo.AddWithoutNull("autoAcceptGroupInvitation", AutoAcceptGroupInvitation);
            jo.AddWithoutNull("requireAck", RequireAck);
            jo.AddWithoutNull("requireDeliveryAck", RequireDeliveryAck);
            jo.AddWithoutNull("deleteMessagesAsExitGroup", DeleteMessagesAsExitGroup);
            jo.AddWithoutNull("deleteMessagesAsExitRoom", DeleteMessagesAsExitRoom);
            jo.AddWithoutNull("isRoomOwnerLeaveAllowed", IsRoomOwnerLeaveAllowed);
            jo.AddWithoutNull("sortMessageByServerTime", SortMessageByServerTime);
            jo.AddWithoutNull("usingHttpsOnly", UsingHttpsOnly);
            jo.AddWithoutNull("serverTransfer", ServerTransfer);
            jo.AddWithoutNull("isAutoDownload", IsAutoDownload);
            jo.AddWithoutNull("areaCode", (int)AreaCode);
            jo.AddWithoutNull("enableDnsConfig", EnableDNSConfig);
            jo.AddWithoutNull("myUUID", MyUUID);

#if !_WIN32
            jo.AddWithoutNull("sdkDataPath", Application.persistentDataPath);
#endif

            if (RestServer != null)
            {
                jo.AddWithoutNull("restServer", RestServer);
            }

            if (IMServer != null)
            {
                jo.AddWithoutNull("imServer", IMServer);
            }

            if (IMPort != 0)
            {
                jo.AddWithoutNull("imPort", IMPort);
            }

            if (DNSURL != null)
            {
                jo.AddWithoutNull("dnsUrl", DNSURL);
            }


            /* 暂不支持推送
            JSONObject pushConfig = new JSONObject();
            pushConfig.Add("mzAppId", mZAppId);
            pushConfig.Add("mzAppKey", mZAppKey);
            pushConfig.Add("enableMzPush", enableMZPush);

            pushConfig.Add("oppoAppKey", oPPOAppKey);
            pushConfig.Add("oppoAppSecret", oPPOAppSecret);
            pushConfig.Add("enableOppoPush", enableOPPOPush);

            pushConfig.Add("miAppId", miAppId);
            pushConfig.Add("miAppKey", miAppKey);
            pushConfig.Add("enableMiPush", enableMiPush);

            pushConfig.Add("fcmId", fCMId);
            pushConfig.Add("enableFcmPush", enableFCMPush);

            pushConfig.Add("apnsCerName", aPNsCerName);
            pushConfig.Add("enableApns", enableAPNs);

            pushConfig.Add("enableHwPush", enableHWPush);

            pushConfig.Add("enableVivoPush", enableVivoPush);

            jo.AddWithoutNull("pushConfig", pushConfig);
            */

            return jo;
        }
    }
}

