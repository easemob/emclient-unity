namespace ChatSDK
{
    public enum DataType
    {
        Bool,
        String,
        Group,
        Room,
        CursorResult,
        ListOfString,
        ListOfMessage,
        ListOfConversation,
        ListOfGroup,
        ListOfGroupSharedFile,
    };

    /// <summary>
    /// 消息体类型
    /// </summary>
    public enum MessageBodyType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        TXT,

        /// <summary>
        /// 图片消息
        /// </summary>
        IMAGE,

        /// <summary>
        /// 视频消息
        /// </summary>
        VIDEO,

        /// <summary>
        /// 位置消息
        /// </summary>
        LOCATION,

        /// <summary>
        /// 语音消息
        /// </summary>
        VOICE,

        /// <summary>
        /// 文件消息
        /// </summary>
        FILE,

        /// <summary>
        /// Cmd消息
        /// </summary>
        CMD,

        /// <summary>
        /// 自定义消息
        /// </summary>
        CUSTOM    
    };


    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 单聊消息
        /// </summary>
        Chat,

        /// <summary>
        /// 群聊消息
        /// </summary>
        Group,

        /// <summary>
        /// 聊天室消息
        /// </summary>
        Room,
    }


    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MessageStatus
    {
        /// <summary>
        /// 创建
        /// </summary>
        CREATE,

        /// <summary>
        /// 发送中
        /// </summary>
        PROGRESS,

        /// <summary>
        /// 发送成功
        /// </summary>
        SUCCESS,

        /// <summary>
        /// 发送失败
        /// </summary>
        FAIL, 
    }

    /// <summary>
    /// 消息方向
    /// </summary>
    public enum MessageDirection
    {
        /// <summary>
        /// 发送的消息
        /// </summary>
        SEND,

        /// <summary>
        /// 接收的消息
        /// </summary>
        RECEIVE, 
    }


    /// <summary>
    /// 会话类型
    /// </summary>
    public enum ConversationType
    {
        /// <summary>
        /// 单聊会话
        /// </summary>
        Chat,

        /// <summary>
        /// 群聊会话
        /// </summary>
        Group,

        /// <summary>
        /// 聊天室会话
        /// </summary>
        Room,
    }

    /// <summary>
    /// 消息搜索方向
    /// </summary>
    public enum MessageSearchDirection
    {
        /// <summary>
        /// 更早的消息
        /// </summary>
        UP,

        /// <summary>
        /// 更晚的消息
        /// </summary>
        DOWN
    }

    namespace MessageBody
    {
        public enum DownLoadStatus
        {
            /// <summary>
            /// 下载中
            /// </summary>
            DOWNLOADING,

            /// <summary>
            /// 下载成功
            /// </summary>
            SUCCESS,

            /// <summary>
            /// 下载失败
            /// </summary>
            FAILED,

            /// <summary>
            /// 准备
            /// </summary>
            PENDING
        };
    }

    /// <summary>
    /// 群类型
    /// </summary>
    public enum GroupStyle
    {
        /// <summary>
        /// 私有群，只有创建者可以邀请他人进群
        /// </summary>
        PrivateOnlyOwnerInvite,

        /// <summary>
        /// 私有群，创建者和成员都可以邀请他人进群
        /// </summary>
        PrivateMemberCanInvite,

        /// <summary>
        /// 公开群，申请后需要群主或管理员同意才能加群
        /// </summary>
        PublicJoinNeedApproval,

        /// <summary>
        /// 公开群，任何人可以直接进群，不需要管理员同意
        /// </summary>
        PublicOpenJoin,
    }

    public enum GroupPermissionType
    {
        /// <summary>
        /// 群成员
        /// </summary>
        Member,
        /// <summary>
        /// 群管理员
        /// </summary>
        Admin,
        /// <summary>
        /// 群主
        /// </summary>
        Owner,
        Unknown = -1,
        Default=Unknown,
        None=Unknown
    }

    /// <summary>
    /// 聊天室权限
    /// </summary>
    public enum RoomPermissionType
    {
        /// <summary>
        /// 聊天室成员
        /// </summary>
        Member,
        /// <summary>
        /// 聊天室管理员
        /// </summary>
        Admin,
        /// <summary>
        /// 聊天室创建者
        /// </summary>
        Owner,
        Unknown = -1,
        Default = Unknown,
        None = Unknown
    }

    /// <summary>
    /// 推送样式
    /// </summary>
    public enum PushStyle : byte {
        /// <summary>
        /// 显示详情
        /// </summary>
        Simple,
        /// <summary>
        /// 显示概要
        /// </summary>
        Summary, 
    }

    /// <summary>
    /// Message中属性类型
    /// STRVECTOR 对应 List<string>类型
    /// </summary>
    public enum AttributeValueType : byte
    {
        BOOL = 0,
        INT32,
        UINT32,
        INT64,
        FLOAT,
        DOUBLE,
        STRING,
        //STRVECTOR,
        JSONSTRING,
        //ATTRIBUTEVALUE,
        NULLOBJ
    }

    /// <summary>
    /// 多设备操作类型
    /// </summary>
    public enum MultiDevicesOperation
    {
        UNKNOW                          = -1,
        CONTACT_REMOVE                  = 2,
        CONTACT_ACCEPT                  = 3,
        CONTACT_DECLINE                 = 4,
        CONTACT_BAN                     = 5, 
        CONTACT_ALLOW                   = 6, 

        GROUP_CREATE                    = 10,
        GROUP_DESTROY                   = 11,
        GROUP_JOIN                      = 12,
        GROUP_LEAVE                     = 13,
        GROUP_APPLY                     = 14,
        GROUP_APPLY_ACCEPT              = 15, 
        GROUP_APPLY_DECLINE             = 16,
        GROUP_INVITE                    = 17,
        GROUP_INVITE_ACCEPT             = 18,
        GROUP_INVITE_DECLINE            = 19,
        GROUP_KICK                      = 20,
        GROUP_BAN                       = 21,
        GROUP_ALLOW                     = 22,
        GROUP_BLOCK                     = 23,
        GROUP_UNBLOCK                   = 24,
        GROUP_ASSIGN_OWNER              = 25,
        GROUP_ADD_ADMIN                 = 26,
        GROUP_REMOVE_ADMIN              = 27,
        GROUP_ADD_MUTE                  = 28,
        GROUP_REMOVE_MUTE               = 29,
        GROUP_ADD_USER_WHITE_LIST       = 30,
        GROUP_REMOVE_USER_WHITE_LIST    = 31,
        GROUP_ALL_BAN                   = 32,
        GROUP_REMOVE_ALL_BAN            = 33,
    }

    public enum SilentModeParamType
    {
        RemindType = 0,
        Duration,
        Interval,
    }

    public enum PushRemindType
    {
        Default = 0,
        All,
        MentionOnly,
        None,
    }

    public enum ChatThreadOperation
    {
        UnKnown = 0, Create, Update, Delete, Update_Msg,
    }
}
