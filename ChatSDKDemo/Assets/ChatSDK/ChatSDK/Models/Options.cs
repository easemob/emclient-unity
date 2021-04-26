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

        public Options
            (
            string appKey,
            bool debugModel = false,
            bool autoLogin = true,
            bool acceptInvitationAlways = true,
            bool autoAcceptGroupInvitation = true,
            bool requireAck = true,
            bool requireDeliveryAck = true,
            bool deleteMessagesAsExitGroup = true,
            bool deleteMessagesAsExitRoom = false,
            bool isRoomOwnerLeaveAllowed = true,
            bool sortMessageByServerTime = true,
            bool usingHttpsOnly = true,
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
