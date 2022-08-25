namespace AgoraChat
{
    public enum ChatThreadOperation
    {
        /**
        * \～chinese
        * 未知操作，缺省值。
        *
        \~english
        * Unkonw operation, default value.
        */
        UnKnown = 0,
        /**
        * \～chinese
        * 创建子区。
        *
        \~english
        * Create thread.
        */
        Create,
        /**
        * \～chinese
        * 更新子区。
        *
        \~english
        * Update thread.
        */
        Update,
        /**
        * \～chinese
        * 删除子区。
        *
        \~english
        * Delete thread.
        */
        Delete,
        /**
        * \～chinese
        * 子区消息更新。
        *
        \~english
        * Thread message updated.
        */
        Update_Msg,
    }

    /**
         * \~chinese
         * 群组成员角色枚举。
         *
         * \~english
         * The group member roles.
         *
         */
    public enum GroupPermissionType
    {
        /**
         * \~chinese
         * 普通成员。
         *
         * \~english
         * The regular group member.
         */
        Member,

        /**
         * \~chinese
         * 群组管理员。
         *
         * \~english
         * The group admin.
         */
        Admin,

        /**
         * \~chinese
         * 群主。
         *
         * \~english
         * The group owner.
         */
        Owner,

        /**
         * \~chinese
         * 未知。
         *
         * \~english
         * Unknown.
         */
        Unknown = -1,
        Default = Unknown,
        None = Unknown
    }

    /**
         * \~chinese
         * 群组类型枚举。
         *
         * \~english
         * The group styles.
         */
    public enum GroupStyle
    {
        /**
         * \~chinese
         * 私有群组，只允许群主邀请用户加入。
         *
         * \~english
         * Private groups where only the group owner can invite users to join.
         */
        PrivateOnlyOwnerInvite,

        /**
         * \~chinese
         * 私有群组，群成员可邀请用户加入。
         *
         * \~english
         * Private groups where each group member can invite users to join.
         */
        PrivateMemberCanInvite,

        /**
         * \~chinese
         * 公开群组，只允许群主邀请用户加入; 非群成员用户需发送入群申请，群主或群组管理员同意后才能入群。
         *
         * \~english
         * Public groups where only the owner can invite users to join. 
         * 
         * A user can join a group only after getting approval from the group owner or admins.
         */
        PublicJoinNeedApproval,

        /**
         * \~chinese
         * 公开群组，允许非群组成员加入，无需群主或管理员同意.
         *
         * \~english
         * Public groups where a user can join a group without the approval from the group owner or admins.
         */
        PublicOpenJoin,
    }

    /**
         * \~chinese
         *  聊天室成员枚举。
         *
         *  \~english
         *  The chat room member roles.
         */
    public enum RoomPermissionType
    {
        /**
         * \~chinese
         * 普通成员。
         *
         * \~english
         * The regular chat room member.
         */
        Member,

        /**
         * \~chinese
         * 聊天室管理员。
         *
         * \~english
         * The chat room admin.
         */
        Admin,

        /**
         * \~chinese
         * 聊天室所有者。
         *
         * \~english
         * The chat room owner.
         */
        Owner,

        /**
         * \~chinese
         * 未知。
         *
         * \~english
         * Unknown.
         */
        Unknown = -1,
        Default = Unknown,
        None = Unknown
    }

    /**
        * \~chinese
        * 会话类型枚举。
        *
        *\~english
        * The chat types.
        */
    public enum ConversationType
    {
        /**
        * \~chinese
        * 单聊会话。
        *
        * \~english
        * The one-to-one chat.
        */
        Chat,

        /**
        * \~chinese
        * 群聊会话。
        *
        * \~english
        * The group chat.
        */
        Group,

        /**
        * \~chinese
        * 聊天室会话。
        *
        * \~english
        * The chat room.
        */
        Room,
    }

    /**
    * \~chinese
    * 消息类型枚举。
    * 
    * \~english
    * The message types.
    */
    public enum MessageBodyType
    {
        /**
          * \~chinese
          * 文本消息。
          * 
          * \~english
          * The text message.
          */
        TXT,

        /**
          * \~chinese
          * 图片消息。
          * 
          * \~english
          * The image message.
          */
        IMAGE,

        /**
          * \~chinese
          * 视频消息。
          * 
          * \~english
          * The video message.
          */
        VIDEO,

        /**
          * \~chinese
          * 位置消息。
          * 
          * \~english
          * The location message.
          */
        LOCATION,

        /**
          * \~chinese
          * 语音消息。
          * 
          * \~english
          * The voice message.
          */
        VOICE,

        /**
         * \~chinese
         * 文件消息。
         * 
         * \~english
         * The file message.
         */
        FILE,

        /**
          * \~chinese
          * 命令消息（透传消息）。
          * 
          * \~english
          * The command message.
          */
        CMD,

        /**
          * \~chinese
          * 用户自定义消息。
          * 
          * \~english
          * The custom message.
          */
        CUSTOM
    };

    /**
    * \~chinese
    * 消息查询方向枚举。
    *
    * \~english
    * The message query directions. 
    */
    public enum MessageSearchDirection
    {
        /**
        * \~chinese 
        * 按消息中的时间戳 ({@link SortMessageByServerTime}) 的倒序加载。
        * 
        * \~english 
        * Messages are retrieved in the reverse chronological order of their Unix timestamp ({@link SortMessageByServerTime}). 
        */
        UP,

        /**
        * \~chinese 
        * 按消息中的时间戳 ({@link SortMessageByServerTime}) 的顺序加载。
        * 
        * \~english 
        * Messages are retrieved in the chronological order of their Unix timestamp ({@link SortMessageByServerTime}). 
        */
        DOWN
    }
}