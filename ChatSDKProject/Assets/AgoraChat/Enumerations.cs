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