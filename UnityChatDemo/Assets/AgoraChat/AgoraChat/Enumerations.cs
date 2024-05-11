using System;

namespace AgoraChat
{
    /**
    * \~chinese
    * 区域代码。
    *
    * \~english
    * Area Code.
    */
    public enum AreaCode
    {
        CN = 1,

        NA = 2,

        EU = 4,

        AS = 8,

        JP = 16,

        IN = 32,

        GLOB = -1,
    }

    /**
    * \~chinese
    * 链接断开原因。
    *
    * \~english
    * The reason of disconnection.
    */
    public enum DisconnectReason
    {
        /**
        * \~chinese
        * 断开链接, 无具体原因。
        *
        * \~english
        * The SDK is disconnected from the server with no reason.
        */
        Reason_Disconnected,

        /**
        * \~chinese
        * 用户 ID 或者密码认证错误。
        *
        * \~english
        * The user ID or password is wrong.
        */
        Reason_AuthenticationFailed,

        /**
        * \~chinese
        * 用户在另一台设备上登录。
        *
        * \~english
        * The user has logged in to another device.
        */
        Reason_LoginFromOtherDevice,

        /**
        * \~chinese
        * 用户被从 server 上移除。
        * 
        * \~english
        * The user is removed from the server.
        */
        Reason_RemoveFromServer,

        /**
        * \~chinese
        * 用户登录登录设备超限。
        *
        * \~english
        * The user has logged in to too many devices.
        */
        Reason_LoginTooManyDevice,

        /**
        * \~chinese
        * 用户密码变更。
        *
        * \~english
        * The user has changed the password.
        */
        Reason_ChangePassword,

        /**
        * \~chinese
        * 用户被其他设备或者后端控制台踢出。
        *
        * \~english
        * The user is kicked by another device or on the console.
        */
        Reason_KickedByOtherDevice,

        /**
        * \~chinese
        * 服务被禁止。
        *
        * \~english
        * The service is disabled.
        */
        Reason_ForbidByServer,
    }

    public enum ChatThreadOperation
    {
        /**
        * \~chinese
        * 未知操作，缺省值。
        *
        * \~english
        * Unknown operation, default value.
        */
        UnKnown = 0,
        /**
        * \~chinese
        * 创建子区。
        *
        * \~english
        * Create a thread.
        */
        Create,
        /**
        * \~chinese
        * 更新子区。
        *
        * \~english
        * Update a thread.
        */
        Update,
        /**
        * \~chinese
        * 删除子区。
        *
        * \~english
        * Delete a thread.
        */
        Delete,
        /**
        * \~chinese
        * 子区消息更新。
        *
        * \~english
        * Update a thread message.
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
        CUSTOM,

        /**
          * \~chinese
          * 合并消息。
          *
          * \~english
          * The combined message.
          */
        COMBINE
    };

    /**
    * \~chinese
    * 聊天室消息优先级枚举。
    *
    * \~english
    * The chat room message priorities.
    */
    public enum RoomMessagePriority
    {
        /**
        * \~chinese
        * 高优先级。
        *
        * \~english
        * High priority.
        */
        High = 0,

        /**
        * \~chinese
        * 普通优先级。
        *
        * \~english
        * Normal priority.
        */
        Normal,

        /**
        * \~chinese
        * 低优先级。
        *
        * \~english
        * Low priority.
        */
        Low
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
    };

    /**
    * \~chinese
    * 消息搜索范围枚举类型。
    *
    * \~english
    * The message search scopes.
    */
    public enum MessageSearchScope
    {
        /**
        * \~chinese
        * 按消息内容搜索。
        *
        * \~english
        * Search by message content.
        */
        CONTENT,

        /**
        * \~chinese
        * 按消息扩展属性搜索。
        *
        * \~english
        * Search by message extension.
        */
        EXT,

        /**
        * \~chinese
        * 按消息内容和扩展属性搜索。
        *
        * \~english
        * Search by message content and extension.
        */
        ALL,
    };

    /**
    * \~chinese
    * 消息状态枚举。
    *
    *\~english
    * The message status.
    */
    public enum MessageStatus
    {
        /**
        * \~chinese
        * 消息已创建。
        *
        *\~english
        * The message is created.
        */
        CREATE,

        /**
        * \~chinese
        * 消息正在发送。
        *
        *\~english
        * The message is being delivered.
        */
        PROGRESS,

        /**
        * \~chinese
        * 消息发送成功。
        *
        *\~english
        * The message is successfully delivered.
        */
        SUCCESS,

        /**
        * \~chinese
        * 消息发送失败。
        *
        *\~english
        * The message fails to be delivered.
        */
        FAIL,
    };

    /**
    * \~chinese
    * 聊天类型枚举。
    *
    *\~english
    * The chat types.
    */
    public enum MessageType
    {
        /**
        * \~chinese
        * 单聊。
        *
        * \~english
        * The one-to-one chat.
        */
        Chat = 0,

        /**
        * \~chinese
        * 群聊。
        *
        * \~english
        * The group chat.
        */
        Group,

        /**
        * \~chinese
        * 聊天室。
        *
        * \~english
        * The chat room.
        */
        Room,
    };

    /**
    * \~chinese
    * 消息方向枚举。
    * 
    * \~english
    * The message directions.
    */
    public enum MessageDirection
    {
        /**
        \~chinese 
        * 该消息是当前用户发送出去的。
        *
        * \~english 
        * This message is sent from the current user.
        */
        SEND,

        /**
        * \~chinese 
        * 该消息是当前用户接收到的。 
        * 
        * \~english 
        * The message is received by the current user.
        */
        RECEIVE,
    };

    /**
     * \~chinese
     *  消息的扩展属性类型的枚举。
     *
     *  \~english
     *  The extension attribute types of messages.
     */
    public enum AttributeValueType : byte
    {
        /**
         * \~chinese
         *  布尔类型。
         *
         *  \~english
         *  Boolean.
         */
        BOOL = 0,

        /**
         * \~chinese
         *  有符号的 32 位整型。
         *
         *  \~english
         *  Signed 32-bit int.
         */
        INT32,

        /**
         * \~chinese
         *  无符号的 32 位整型。
         *
         *  \~english
         *  Unsigned 32-bit int.
         */
        [Obsolete]
        UINT32,

        /**
         * \~chinese
         *  有符号的 64 位整型。
         *
         *  \~english
         *  Signed 64-bit int.
         */
        INT64,

        /**
         * \~chinese
         *  浮点类型。
         *
         *  \~english
         *  Float.
         */
        FLOAT,

        /**
         * \~chinese
         *  双精度类型。
         *
         *  \~english
         *  Double.
         */
        DOUBLE,

        /**
         * \~chinese
         *  字符串类型。
         *
         *  \~english
         *  String.
         */
        STRING,
        //STRVECTOR,
        /**
         * \~chinese
         *  JSON 字符串类型。
         *
         *  \~english
         *  JSON string.
         */
        JSONSTRING,
        //ATTRIBUTEVALUE,
        NULLOBJ
    };

    namespace MessageBody
    {
        /**
        * \~chinese
        * 消息下载状态枚举。
        *
        *\~english
        * The message download status.
        */
        public enum DownLoadStatus
        {
            /**
            * \~chinese
            * 消息正在下载。
            *
            * \~english
            * The message is being downloaded.
            */
            DOWNLOADING,

            /**
            * \~chinese
            * 消息下载成功。
            *
            * \~english
            * The message is successfully downloaded.
            */
            SUCCESS,

            /**
            * \~chinese
            * 消息下载失败。
            *
            * \~english
            * The message fails to be downloaded.
            */
            FAILED,

            /**
            * \~chinese
            * 消息待下载。
            *
            * \~english
            * The download is pending.
            */
            PENDING
        };
    };

    public enum MultiDevicesOperation
    {
        UNKNOWN = -1,
        /**
         * \~chinese
         * 当前用户在其他设备上删除好友。
         *
         * \~english
         * The current user removed a contact on another device.
         */
        CONTACT_REMOVE = 2,

        /**
         * \~chinese
         * 当前用户在其他设备上接受好友请求。
         *
         * \~english
         * The current user accepted a friend request on another device.
         */
        CONTACT_ACCEPT = 3,

        /**
         * \~chinese
         * 当前用户在其他设备上拒绝好友请求。
         *
         * \~english
         * The current user declined a friend request on another device.
         */
        CONTACT_DECLINE = 4,

        /**
         * \~chinese
         * 当前用户在其他设备上将好友加入黑名单。
         *
         * \~english
         * The current user added a contact to the block list on another device.
         */
        CONTACT_BAN = 5,

        /**
         * \~chinese
         * 当前用户在其他设备上将好友移出黑名单。
         *
         * \~english
         * The current user removed a contact from the block list on another device.
         */
        CONTACT_ALLOW = 6,

        /**
         * \~chinese
         * 当前用户在其他设备上创建群组。
         *
         * \~english
         * The current user created a group on another device.
         */
        GROUP_CREATE = 10,

        /**
         * \~chinese
         * 当前用户在其他设备上解散群组。
         *
         * \~english
         * The current user destroyed a group on another device.
         */
        GROUP_DESTROY = 11,

        /**
         * \~chinese
         * 当前用户在其他设备上加入群组。
         *
         * \~english
         * The current user joined a group on another device.
         */
        GROUP_JOIN = 12,

        /**
         * \~chinese
         * 当前用户在其他设备离开群组。
         *
         * \~english
         * The current user left a group on another device.
         */
        GROUP_LEAVE = 13,

        /**
         * \~chinese
         * 当前用户在其他设备上申请加入群组。
         *
         * \~english
         * The current user requested to join a group on another device.
         */
        GROUP_APPLY = 14,

        /**
         * \~chinese
         * 当前用户在其他设备接受入群申请。
         *
         * \~english
         * The current user accepted a group request on another device.
         */
        GROUP_APPLY_ACCEPT = 15,

        /**
         * \~chinese
         * 当前用户在其他设备上拒绝入群申请。
         *
         * \~english
         * The current user declined a group request on another device.
         */
        GROUP_APPLY_DECLINE = 16,

        /**
         * \~chinese
         * 当前用户在其他设备上邀请用户入群。
         *
         * \~english
         * The current user invited a user to join the group on another device.
         */
        GROUP_INVITE = 17,

        /**
         * \~chinese
         * 当前用户在其他设备上接受了入群邀请。
         *
         * \~english
         * The current user accepted a group invitation on another device.
         */
        GROUP_INVITE_ACCEPT = 18,

        /**
         * \~chinese
         * 当前用户在其他设备上拒绝了入群邀请。
         *
         * \~english
         * The current user declined a group invitation on another device.
         */
        GROUP_INVITE_DECLINE = 19,

        /**
         * \~chinese
         * 当前用户在其他设备上将成员踢出群。
         *
         * \~english
         * The current user kicked a member out of a group on another device.
         */
        GROUP_KICK = 20,

        /**
         * \~chinese
         * 当前用户在其他设备上将成员加入群组黑名单。
         *
         * \~english
         * The current user added a member to a group block list on another device.
         */
        GROUP_BAN = 21,

        /**
         * \~chinese
         * 当前用户在其他设备上将成员移除群组黑名单。
         *
         * \~english
         * The current user removed a member from a group block list on another device.
         */
        GROUP_ALLOW = 22,

        /**
         * \~chinese
         * 当前用户在其他设备上屏蔽群组。
         *
         * \~english
         * The current user blocked a group on another device.
         */
        GROUP_BLOCK = 23,

        /**
         * \~chinese
         * 当前用户在其他设备上取消群组屏蔽。
         *
         * \~english
         * The current user unblocked a group on another device.
         */
        GROUP_UNBLOCK = 24,

        /**
         * \~chinese
         * 当前用户在其他设备上转让群组所有权。
         *
         * \~english
         * The current user transferred the group ownership on another device.
         */
        GROUP_ASSIGN_OWNER = 25,

        /**
         * \~chinese
         * 当前用户在其他设备上添加管理员。
         *
         * \~english
         * The current user added an admin on another device.
         */
        GROUP_ADD_ADMIN = 26,

        /**
         * \~chinese
         * 当前用户在其他设备上移除管理员。
         *
         * \~english
         * The current user removed an admin on another device.
         */
        GROUP_REMOVE_ADMIN = 27,

        /**
         * \~chinese
         * 当前用户在其他设备上禁言成员。
         *
         * \~english
         * The current user muted a member on another device.
         */
        GROUP_ADD_MUTE = 28,

        /**
         * \~chinese
         * 当前用户在其他设备上解除禁言。
         *
         * \~english
         * The current user unmuted a member on another device.
         */
        GROUP_REMOVE_MUTE = 29,

        /**
         * \~chinese
         * 当前用户在其他设备上将群组成员添加至白名单。
         *
         * \~english
         * The current user added a group member to the allow list on another device.
         */
        GROUP_ADD_USER_WHITE_LIST = 30,

        /**
         * \~chinese
         * 当前用户在其他设备上将群组成员移除白名单。
         *
         * \~english
         * The current user removed a group member from the allow list on another device.
         */
        GROUP_REMOVE_USER_WHITE_LIST = 31,

        /**
         * \~chinese
         * 当前用户在其他设备上将所有其他群组成员禁言。
         *
         * \~english
         * The current user added all other group members to the group mute list on another device.
         */
        GROUP_ALL_BAN = 32,

        /**
         * \~chinese
         * 当前用户在其他设备上将所有其他群组成员解除禁言。
         *
         * \~english
         * The current user removed all other group members from the group mute list on another device.
         */
        GROUP_REMOVE_ALL_BAN = 33,

        /**
         * \~chinese
         * 子区在其他设备上被创建。
         *
         * \~english
         * A thread was created on another device.
         */
        THREAD_CREATE = 40,

        /**
        * \~chinese
        * 子区在其他设备上被销毁。
        *
        * \~english
        * A thread was destroyed on another device.
        */
        THREAD_DESTROY = 41,

        /**
        * \~chinese
        * 在其他设备上加入子区。
        *
        * \~english
        * The user joined a thread on another device.
        */
        THREAD_JOIN = 42,

        /**
        * \~chinese
        * 在其他设备上加退出子区。
        *
        * \~english
        * The user left a thread on another device.
        */
        THREAD_LEAVE = 43,

        /**
        * \~chinese
        * 子区在其他设备上有更新。
        *
        * \~english
        * The thread was updated on another device.
        */
        THREAD_UPDATE = 44,

        /**
        * \~chinese
        * 在其他设备上被踢出子区。
        *
        * \~english
        * The user was kicked from a thread on another device.
        */
        THREAD_KICK = 45,

        /**
        * \~chinese
        * 在其他设备上设置了群组成员自定义属性。
        *
        * \~english
        * The custom attribute(s) of a group member(s) is/are set on other devices.
        */
        SET_METADATA = 50,

        /**
        * \~chinese
        * 在其他设备上删除了群组成员自定义属性。
        *
        * \~english
        * The custom attribute(s) of a group member(s) is/are deleted on other devices.
        */
        DELETE_METADATA = 51,

        /**
        * \~chinese
        * 群组成员自定义属性发生改变。
        *
        * \~english
        * The custom attribute(s) of a group member(s) is/are changed.
        */
        GROUP_MEMBER_METADATA_CHANGED = 52,

        /**
        * \~chinese
        * 会话被置顶。
        *
        * \~english
        * A conversation is pinned.
        */
        CONVERSATION_PINNED = 60,

        /**
        * \~chinese
        * 会话被取消置顶。
        *
        * \~english
        * A conversation is unpinned.
        */
        CONVERSATION_UNPINNED = 61,

        /**
        * \~chinese
        * 会话被删除。
        *
        * \~english
        * A conversation is deleted.
        */
        CONVERSATION_DELETED = 62,

        /**
        * \~chinese
        * 会话被标记或取消标记。
        *
        * \~english
        * A conversation is marked or unmarked.
        */
        CONVERSATION_MARK = 63
    };

    public enum MessageReactionOperate
    {
        /**
        * \~chinese
        * Reaction 删除操作。
        *
        * \~english
        * A Reaction is deleted.
        */
        MessageReactionOperateRemove = 0,

        /**
        * \~chinese
        * Reaction 添加操作。
        *
        * \~english
        * A Reaction is added.
        */
        MessageReactionOperateAdd = 1,
    }

    /**
    * \~chinese
    * 会话标记。
    *
    * \~english
    * The conversation marks.
    */
    public enum MarkType
    {
        MarkType0 = 0,
        MarkType1 = 1,
        MarkType2 = 2,
        MarkType3 = 3,
        MarkType4 = 4,
        MarkType5 = 5,
        MarkType6 = 6,
        MarkType7 = 7,
        MarkType8 = 8,
        MarkType9 = 9,
        MarkType10 = 10,
        MarkType11 = 11,
        MarkType12 = 12,
        MarkType13 = 13,
        MarkType14 = 14,
        MarkType15 = 15,
        MarkType16 = 16,
        MarkType17 = 17,
        MarkType18 = 18,
        MarkType19 = 19,
    }
}