using System;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine;
using UnityEngine.Scripting;
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
    [Preserve]
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
         * Whether to output the debug information as logs.
         * - `true`: Yes.
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
	     * - (Default) `true`: Yes.
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
	     * - (Default) `true`: Yes;
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
	     * 从数据库加载会话时，是否允许加载空会话。
	     * - `true`：是；
	     * - （默认）`false`：否。
	     *
	     * \~english
	     * Whether to include empty conversations while loading conversations from the database.
	     * -`true`: Yes;
	     * -(Default) `false`: No.
	     *
	     */
        public bool EnableEmptyConversation = false;

        /**
         *  \~chinese
         *  自定义系统类型。
         *
         *  \~english
         *  The custom system type.
         */
        public int CustomOSType = -1;

        /**
         *  \~chinese
         *  自定义设备名称。
         *
         *  \~english
         *  The custom device name.
         *
         */
        public string CustomDeviceName = "";

        /**
	     * \~chinese
	     * 设置区域代号，使用边缘节点时遵循区域限制。
	     *
	     * （默认）`GLOB`: 不限制区域。
	     *
	     * \~english
	     * The area code.
         *
         * Restrictions of the area should be followed when edge nodes are used.
         *
	     * (Default) `GLOB`: No restrictions will be applied when you try to access the server node.
	     */
        public AreaCode AreaCode = AreaCode.GLOB;

        /**
        * \~chinese
        * 当前设备的 UUID。
        *
        * \~english
        * The UUID for the current device.
        */
        public string MyUUID = "";

        /**
        * \~chinese
        * 设置 SDK 底层数据存储路径。仅用于 MacOS 和 Windows 平台端。
        *
        * 如果未设置，则由 SDK 设置为缺省路径。
        *
        * 注意：
        * 对于 Unity SDK，在 1.1.2 版本之前，SDKDataPath 为当前路径，即 "."，从 1.1.2 开始变更为持久化目录 Application.persistentDataPath，
        * 若从 1.1.2 之前的 SDK 版本进行升级，而且需要保留本地历史消息，可使用以下两种方式：
        * - 方式一：将 SDKDataPath 设置为当前路径，即 "."；
        * - 方式二：将原来当前路径下的 sdkdata 文件夹拷贝到 Application.persistentDataPath 中。
        * 对于 Windows SDK，SDKDataPath 仍使用缺省的当前路径，即 "."。
        *
        *
        * 举例如下:
        * MacOS: /Users/UserName/Library/Application Support/DefaultCompany/xxx
        * Windows: C:/Users/UserName/AppData/LocalLow/DefaultCompany/xxx
        *
        * 若数据存储路径以文件夹结尾，无需添加 “/”。
        *
        * 注意: 在 MacOS 下，如果使用相对路径设置 `SDKDatapath`，必须使用"."开头，例如: "./sdkdatapath"。
        *
        * \~english
        * The underlying storage path for SDK data. The storage path is used only for MacOS and Windows platforms.
        *
        * If this parameter is not set, the SDK will set the default value.
        *
        * Note:
        * For the Unity SDK, prior to v1.1.2, SDKDataPath was set to the current path ".".
        * Starting from v1.1.2, this path has been changed to the persistent directory Application.persistentDataPath.
        * If you are upgrading from an SDK version earlier than v1.1.2 and need to retain the local historical messages, there are two methods:
        * - Method 1: Set SDKDataPath to the current path ".".
        * - Method 2: Copy the sdkdata folder in the original current path to the persistent directory Application.persistentDataPath.
        * For the Windows SDK, SDKDataPath still uses the current path ".".
        *
        * For example:
        * MacOS: /Users/UserName/Library/Application Support/DefaultCompany/xxx
        * Windows: C:/Users/UserName/AppData/LocalLow/DefaultCompany/xxx
        *
        * If the data storage path ends with a folder name, it is unnecessary to appended "/" to the end of the path.
        *
        * Note: For MacOS, if you set `SDKDatapath` to a relative path, the path must start with ".", for example "./sdkdatapath".
        */
        public string SDKDataPath = "";

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
        [Preserve]
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
        [Preserve]
        internal Options() { }

        [Preserve]
        internal Options(bool is_json, string json) : base(json) { }

        [Preserve]
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
            jo.AddWithoutNull("enableEmptyConversation", EnableEmptyConversation);
            jo.AddWithoutNull("customOSType", CustomOSType);
            jo.AddWithoutNull("customDeviceName", CustomDeviceName);


            if (SDKDataPath.Length == 0)
            {
#if !_WIN32
                jo.AddWithoutNull("sdkDataPath", Application.persistentDataPath);
#endif
            }
            else
            {
                jo.AddWithoutNull("sdkDataPath", SDKDataPath);
            }

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

