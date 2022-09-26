﻿namespace ChatSDK
{
     /**
    * \~chinese
    * 数据类型枚举。
    * 
    * \~english
    * The data types.
    */
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
    }

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
    }

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
        Default=Unknown,
        None=Unknown
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
     *  推送通知的显示样式枚举。
     *
     *  \~english
     *  The display styles of push notifications.
     */
    public enum PushStyle : byte {

        /**
         * \~chinese
         *  显示“你有一条新消息”。
         *
         *  \~english
         *  Displays "You have a new message".
         */
        Simple,

        /**
         * \~chinese
         *  显示离线消息的内容。
         *
         *  \~english
         *  Displays the content of the offline message.
         */
        Summary, 
    }

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
         *  Bool.
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
    }

    /**
     * \~chinese
     *  多设备操作类型枚举。
     *
     *  \~english
     *  The multi-device operation types.
     */
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
        THREAD_CREATE  = 40,

		 /**
         * \~chinese
         * 子区在其他设备上被销毁。
         *
         * \~english
         * A thread was destoryed on another device.
         */
        THREAD_DESTROY = 41,

		 /**
         * \~chinese
         * 在其他设备上加入子区。
         *
         * \~english
         * Joined thread on another device.
         */
        THREAD_JOIN = 42,

		 /**
         * \~chinese
         * 在其他设备上加退出子区。
         *
         * \~english
         * Left thread on another device.
         */
        THREAD_LEAVE = 43,

		 /**
         * \~chinese
         * 子区在其他设备上有更新。
         *
         * \~english
         * Thread updated on another device.
         */
        THREAD_UPDATE = 44,

		 /**
         * \~chinese
         * 在其他设备上被提出子区。
         *
         * \~english
         * Kicked from thread on another device.
         */
        THREAD_KICK = 45,
    }

    public enum SilentModeParamType
    {
        /**
        * \~chinese
        * 离线消息推送方式。
        *
        * \~english
        * The push notification mode.
        */
        RemindType = 0,
        /**
        * \~chinese
        * 免打扰时长，单位为分钟。
        *
        * \~english
        * The duration of the do-not-disturb mode, in minutes.
        */
        Duration,
        /**
        * \~chinese
        * 免打扰时间段。仅 app 全局设置有效，对单个会话无效。
        *
        * \~english
        *  The time frame of the do-not-disturb mode. This parameter type is valid only at the app level, but not for conversations.
        */
        Interval,
    }

    public enum PushRemindType
    {
         /**
         * \～chinese
         * 缺省接收全部离线消息的推送通知。
         *
         * \~english
         * Default: Receives push notifications for all offline messages.
         * */
        Default = 0,
        /**
         * \～chinese
         * 接收全部离线消息的推送通知。
         *
         * \~english
         * Receives push notifications for all offline messages.
         * */
        All,
        /** \～chinese
         * 只接收提及当前用户的离线消息的推送通知。
         *
         * \~english
         * Only receives push notifications for mentioned messages.
         */
        MentionOnly,
       /**
        * \～chinese
        * 不接收离线消息的推送通知。
        *
        \~english
        * Receives no push notification for offline messages.
        */
        None,
    }

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
}
