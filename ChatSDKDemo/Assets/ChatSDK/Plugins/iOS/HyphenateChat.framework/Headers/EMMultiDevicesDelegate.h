/**
 *  \~chinese
 *  @header EMMultiDevicesDelegate.h
 *  @abstract 多设备代理协议
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMMultiDevicesDelegate.h
 *  @abstract This protocol defined the callbacks of Multi-device
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

/**
 *  \~chinese
 *  多设备登录事件类型。
 * 
 *  本枚举类以用户 A 同时登录设备 A1 和 设备 A2 为例，描述多设备登录各事件的触发时机。
 *
 *  \~english
 *  Multi-device event types.
 * 
 *  This enumeration takes user A logged into both DeviceA1 and DeviceA2 as an example to illustrate the various multi-device event types and when these events are triggered.
 */
typedef NS_ENUM(NSInteger, EMMultiDevicesEvent) {
    EMMultiDevicesEventUnknow = -1,         /** \~chinese 默认。 \~english Default. */
    EMMultiDevicesEventContactRemove = 2,    /** \~chinese 用户 A 在设备 A1 上删除了好友，则设备 A2 上会收到该事件。 \~english If user A deletes a contact on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventContactAccept = 3,    /** \~chinese 用户 A 在设备 A1 上同意了其他用户的好友请求，则设备 A2 上会收到该事件。 \~english If user A accepts a contact invitation on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventContactDecline = 4,   /** \~chinese 用户 A 在设备 A1 上拒绝了其他用户的好友请求，则设备 A2 上会收到该事件。 \~english If user A declines a contact invitation on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventContactBan = 5,       /** \~chinese 用户 A 在设备 A1 上将其他用户加入黑名单，则设备 A2 上会收到该事件。 \~english If user A adds another user into the blocklist on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventContactAllow = 6,     /** \~chinese 用户 A 在设备 A1 上将某用户移出黑名单，则设备 A2 上会收到该事件。 \~english If user A removes a user from the blocklist on DeviceA1, this event is triggered on DeviceA2. */
    
    EMMultiDevicesEventGroupCreate = 10,     /** \~chinese 用户 A 在设备 A1 上创建了群组，则设备 A2 上会收到该事件。\~english If user A creates a chat group on Device A1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupDestroy = 11,    /** \~chinese 用户 A 在设备 A1 上销毁了群组，则设备 A2 上会收到该事件。 \~english If user A destroys a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupJoin = 12,       /** \~chinese 用户 A 在设备 A1 上加入了群组，则设备 A2 会收到该事件。 \~english If user A joins a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupLeave = 13,      /** \~chinese 用户 A 在设备 A1 上退出群组，则设备 A2 会收到该事件。 \~english If user A leaves a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupApply = 14,      /** \~chinese 用户 A 在设备 A1 上申请加入群组，则设备 A2 会收到该事件。 \~english If user A requests to join a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupApplyAccept = 15,    /** \~chinese 用户 A 在设备 A1 上收到了其他用户的入群申请，则设备 A2 会收到该事件。 \~english If user A receives another user's request to join the chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupApplyDecline = 16,   /** \~chinese 用户 A 在设备 A1 上拒绝了其他用户的入群申请，设备 A2 上会收到该事件。 \~english If user A declines another user's request to join the chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupInvite = 17,     /** \~chinese 用户 A 在设备 A1 上邀请了其他用户进入群组，则设备 A2 上会收到该事件。 \~english If user A invites other users to join the chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupInviteAccept = 18,   /** \~chinese 用户 A 在设备 A1 上同意了其他用户的群组邀请，则设备 A2 上会收到该事件。 \~english If user A accepts another user's group invitation on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupInviteDecline = 19,  /** \~chinese 用户 A 在设备 A1 上拒绝了其他用户的群组邀请，则设备 A2 上会收到该事件。 \~english If user A declines another user's group invitation on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupKick = 20,       /** \~chinese 用户 A 在设备 A1 上将其他用户踢出群组，则设备 A2 上会收到该事件。 \~english If user A removes other users from a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupBan = 21,        /** \~chinese 用户 A 在设备 A1 上被加入黑名单，则设备 A2 上会收到该事件。 \~english If user A is added to the blocklist on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupAllow = 22,      /** \~chinese 用户 A 在设备 A1 上将其他用户移出群组，则设备 A2 上会收到该事件。 \~english If user A removes other users from a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupBlock = 23,      /** \~chinese 用户 A 在设备 A1 上屏蔽了某个群组的消息，设备 A2 上会收到该事件。 \~english If user A blocks messages from a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupUnBlock = 24,    /** \~chinese 用户 A 在设备 A1 上取消屏蔽了某个群组的消息，设备 A2 上会收到该事件。 \~english If user A unblocks messages from a chat group on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupAssignOwner = 25,    /** \~chinese 用户 A 在设备 A1 上更新了群组的群主，则设备 A2 上会收到该事件。 \~english If user A assigns a group owner on DeviceA1, this event is triggered on DeviceA2.*/
    EMMultiDevicesEventGroupAddAdmin = 26,   /** \~chinese 用户 A 在设备 A1 上添加了群组管理员，则设备 A2 上会收到该事件。 \~english If user A adds a group admin on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupRemoveAdmin = 27,    /** \~chinese 用户 A 在设备 A1 上移除了群组管理员，则设备 A2 上会收到该事件。 \~english If user A removes a group admin on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupAddMute = 28,    /** \~chinese 用户 A 在设备 A1 上禁言了群成员，则设备 A2 上会收到该事件。 \~english If user A mutes other group members on DeviceA1, this event is triggered on DeviceA2. */
    EMMultiDevicesEventGroupRemoveMute = 29,     /** \~chinese 用户 A 在设备 A1 上取消禁言了群成员，则设备 A2 上会收到该事件。 \~english If user A unmutes other group members in DeviceA1, this event is triggered on DeviceA2. */
};

@protocol EMMultiDevicesDelegate <NSObject>

@optional

/**
 *  \~chinese
 *  开通多设备同步后，好友操作的多设备事件回调。
 *
 *  @param aEvent       多设备事件类型。
 *  @param aUsername    用户名。
 *  @param aExt         扩展信息。
 *
 *  \~english
 *  The multi-device event callback of contact.
 *
 *  @param aEvent       The event type.
 *  @param aUsername    The username.
 *  @param aExt         The extended Information.
 */
- (void)multiDevicesContactEventDidReceive:(EMMultiDevicesEvent)aEvent
                                  username:(NSString *)aUsername
                                       ext:(NSString *)aExt;

/**
 *  \~chinese
 *  群组多设备事件回调。
 *
 *  @param aEvent       多设备事件类型。
 *  @param aGroupId     群组 ID。
 *  @param aExt         扩展信息。
 *
 *  \~english
 *  The multi-device event callback of group.
 *
 *  @param aEvent       The event type.
 *  @param aGroupId     The group ID.
 *  @param aExt         The extended Information.
 */
- (void)multiDevicesGroupEventDidReceive:(EMMultiDevicesEvent)aEvent
                                 groupId:(NSString *)aGroupId
                                     ext:(id)aExt;

/*!
 *  \~chinese
 *  开启多设备后对单个会话设置免打扰后对其他设备的回调
 *
 *  @param undisturbData         扩展信息
 *
 *  \~english
 *  Callback to other devices after setting Do Not Disturb for a single session after enabling multiple devices
 *
 *  @param aEvent       Event type
 *  @param undisturbData         Extended Information
 */
- (void)multiDevicesUndisturbEventNotifyFormOtherDeviceData:(NSString *)undisturbData;

@end
