namespace ChatSDK
{
    public enum MessageBodyType
    {
        TXT,        // 文本
        IMAGE,      // 图片
        VIDEO,      // 视频
        LOCATION,   // 位置
        VOICE,      // 语音
        FILE,       // 文件
        CMD,        // cmd
        CUSTOM      // custom
    };

    public enum MessageType
    {
        Chat, Group, Room,
    }


    // 消息状态
    public enum MessageStatus
    {
        CREATE, // 创建
        PROGRESS, // 发送中
        SUCCESS, // 发送成功
        FAIL, // 发送失败
    }

    // 消息方向
    public enum MessageDirection
    {
        SEND, // 发送的消息
        RECEIVE, // 接收的消息
    }


    public enum ConversationType
    {
        Chat, Group, Room,
    }


    public enum MessageSearchDirection
    {
        UP, DOWN
    }

    namespace MessageBody
    {
        public enum DownLoadStatus
        {
            PENDING,    // 准备下载
            DOWNLOADING,// 下载中
            SUCCESS,    // 下载成功
            FAILED,     // 下载失败
        };
    }

    public enum GroupStyle
    {
        PrivateOnlyOwnerInvite, // 私有群，只有群主能邀请他人进群，被邀请人会收到邀请信息，同意后可入群；
        PrivateMemberCanInvite, // 私有群，所有人都可以邀请他人进群，被邀请人会收到邀请信息，同意后可入群；
        PublicJoinNeedApproval, // 公开群，可以通过获取公开群列表api取的，申请加入时需要管理员以上权限用户同意；
        PublicOpenJoin, // 公开群，可以通过获取公开群列表api取的，可以直接进入；
    }

    public enum GroupPermissionType
    {
        None,
        Member,
        Admin,
        Owner,
    }

    public enum RoomPermissionType
    {
        None,
        Member,
        Admin,
        Owner,
    }

    public enum PushStyle : byte { Simple, Summary, }

    public enum COMMON_ERR_CODE
    {
        ERROR_NULL_PTR = -7
    }
}
