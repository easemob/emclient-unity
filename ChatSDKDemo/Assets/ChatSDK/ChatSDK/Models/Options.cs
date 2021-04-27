using System;


namespace ChatSDK
{

    public class Options
    {

        private string _AppKey;
        private bool _DebugModel;
        private bool _AutoLogin;
        private bool _AcceptInvitationAlways;
        private bool _AutoAcceptGroupInvitation;
        private bool _RequireAck;
        private bool _RequireDeliveryAck;
        private bool _DeleteMessagesAsExitGroup;
        private bool _DeleteMessagesAsExitRoom;
        private bool _IsRoomOwnerLeaveAllowed;
        private bool _SortMessageByServerTime;
        private bool _UsingHttpsOnly;
        private bool _ServerTransfer;
        private bool _IsAutoDownload;
        private PushConfig _PushConfig;

        public string AppKey { get { return _AppKey; } }

        public bool DebugModel { get { return _DebugModel; } }

        public bool AutoLogin { get { return _AutoLogin; } }

        public bool AcceptInvitationAlways { get { return _AcceptInvitationAlways; } }

        public bool AutoAcceptGroupInvitation { get { return _AutoAcceptGroupInvitation; } }

        public bool RequireAck { get { return _RequireAck; } }

        public bool RequireDeliveryAck { get { return _RequireDeliveryAck; } }

        public bool DeleteMessagesAsExitGroup { get { return _DeleteMessagesAsExitGroup; } }

        public bool DeleteMessagesAsExitRoom { get { return _DeleteMessagesAsExitRoom; } }

        public bool IsRoomOwnerLeaveAllowed { get { return _IsRoomOwnerLeaveAllowed; } }

        public bool SortMessageByServerTime { get { return _SortMessageByServerTime; } }

        public bool UsingHttpsOnly { get { return _UsingHttpsOnly; } }

        public bool ServerTransfer { get { return _ServerTransfer; } }

        public bool IsAutoDownload { get { return _IsAutoDownload; } }

        public PushConfig PushConfig { get { return _PushConfig; } }

        /// <summary>
        /// SDK配置Option
        /// </summary>
        /// <param name="appKey">AppKey,需要到Console中获取</param>
        /// <param name="debugModel">Debug模式</param>
        /// <param name="autoLogin">自定登录，再下次初始化时，SDK会直接登录上次未退出的账号，默认开启</param>
        /// <param name="acceptInvitationAlways">自动同意好友申请，当您在线时，如果有好友申请会自动同意，不在线时，等上线后自动同意, 默认开启</param>
        /// <param name="autoAcceptGroupInvitation">自动同意群组邀请，当您在线时，如果有好友申请会自动同意，不在线时，等上线后自动同意，默认开启</param>
        /// <param name="requireAck">已读回执，允许您发送已读回执，默认开启</param>
        /// <param name="requireDeliveryAck">已送达回执，接收到消息时自动向消息发送方发送已送达回执，只对单聊消息有效，默认关闭</param>
        /// <param name="deleteMessagesAsExitGroup">离开群组时是否删除对应群组消息，默认开启</param>
        /// <param name="deleteMessagesAsExitRoom">离开聊天室时是否删除对应消息，默认开启</param>
        /// <param name="isRoomOwnerLeaveAllowed">是否允许聊天室拥有者离开聊天室，默认开启</param>
        /// <param name="sortMessageByServerTime">获取本地消息时按照服务器时间排序，默认开启</param>
        /// <param name="usingHttpsOnly">只是用Https,默认关闭</param>
        /// <param name="serverTransfer">使用环信附件存储服务，默认开启</param>
        /// <param name="isAutoDownload">自动下载语音和缩略图，默认开启</param>
        /// <param name="pushConfig">推送配置</param>
        public Options
            (
            string appKey,
            bool debugModel = false,
            bool autoLogin = true,
            bool acceptInvitationAlways = true,
            bool autoAcceptGroupInvitation = true,
            bool requireAck = true,
            bool requireDeliveryAck = false,
            bool deleteMessagesAsExitGroup = true,
            bool deleteMessagesAsExitRoom = true,
            bool isRoomOwnerLeaveAllowed = true,
            bool sortMessageByServerTime = true,
            bool usingHttpsOnly = false,
            bool serverTransfer = true,
            bool isAutoDownload = true,
            PushConfig pushConfig = null
            )
        {
            _AppKey = appKey;
            _DebugModel = debugModel;
            _AutoLogin = autoLogin;
            _AcceptInvitationAlways = acceptInvitationAlways;
            _AutoAcceptGroupInvitation = autoAcceptGroupInvitation;
            _RequireAck = requireAck;
            _RequireDeliveryAck = requireDeliveryAck;
            _DeleteMessagesAsExitGroup = deleteMessagesAsExitGroup;
            _DeleteMessagesAsExitRoom = deleteMessagesAsExitRoom;
            _IsRoomOwnerLeaveAllowed = isRoomOwnerLeaveAllowed;
            _SortMessageByServerTime = sortMessageByServerTime;
            _UsingHttpsOnly = usingHttpsOnly;
            _ServerTransfer = serverTransfer;
            _IsAutoDownload = isAutoDownload;
            _PushConfig = pushConfig;
        }

    }
}
