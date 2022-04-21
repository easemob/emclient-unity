/**
 *  \~chinese
 *  @header EMGroup.h
 *  @abstract 群组模型类。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMGroup.h
 *  @abstract The group model.
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

#import "EMCommonDefs.h"
#import "EMGroupOptions.h"

/**
 *  \~chinese
 *  群组权限类型。
 *
 *  \~english
 *  The group permission type.
 */
typedef enum{
    EMGroupPermissionTypeNone   = -1,    /** \~chinese 未知类型。 \~english The unknown type.*/
    EMGroupPermissionTypeMember = 0,     /** \~chinese 普通成员。  \~english The group member.*/
    EMGroupPermissionTypeAdmin,          /** \~chinese 群组管理员。 \~english The group admin.*/
    EMGroupPermissionTypeOwner,          /** \~chinese 群主。 \~english The group owner.*/
}EMGroupPermissionType;

/**
 *  \~chinese 
 *  群组。
 *
 *  \~english
 *  The group.
 */
@interface EMGroup : NSObject

/**
 *  \~chinese
 *  群组 ID。
 *
 *  \~english
 *  The group ID.
 */
@property (nonatomic, copy, readonly) NSString *groupId;

/**
*  \~chinese
*  群组的名称，需要先通过 `getGroupSpecificationFromServerWithId` 方法获取群详情。
*
*  \~english
*  The subject of the group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` to get the group details first.
*/
@property (nonatomic, copy, readonly) NSString *groupName;

/**
 *  \~chinese
 *  群组的描述，需要先通过 `getGroupSpecificationFromServerWithId` 获取群组详情。
 *
 *  \~english
 *  The description of the group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 */
@property (nonatomic, copy, readonly) NSString *description;

/**
 *  \~chinese
 *  群组的公告，需要先通过 getGroupAnnouncementWithId 方法获取群公告。
 *
 *  \~english
 *  The announcement of the group. To get the value of this member, you need to call `getGroupAnnouncementWithId` first.
 */
@property (nonatomic, copy, readonly) NSString *announcement;

/**
 *  \~chinese
 *  群组属性配置，需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 *
 *  \~english
 *  The setting options of group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 */
@property (nonatomic, strong, readonly) EMGroupOptions *settings;

/**
 *  \~chinese
 *  群组的所有者，拥有群的最高权限，需要先通过 `getGroupSpecificationFromServerWithId` 获取群组详情。
 *
 *  群组的所有者只有一人。
 *
 *  \~english
 *  The owner of the group who has the highest privilege of the group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 *
 *  Each chat group has only one owner.
 */
@property (nonatomic, copy, readonly) NSString *owner;

/**
 *  \~chinese
 *  群组的管理者，拥有群的管理权限，需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 *
 *
 *  \~english
 *  The admins of the group who have the group management authority. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 *
 */
@property (nonatomic, copy, readonly) NSArray *adminList;

/**
 *  \~chinese
 *  群组的成员列表，需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 *
 *  \~english
 *  The member list of the group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 */
@property (nonatomic, copy, readonly) NSArray *memberList;

/**
 *  \~chinese
 *  群组的黑名单，需要先调用 getGroupSpecificationFromServerWithId 方法获取群详情。
 *
 *  该方法只有群主才有权限调用。否则 SDK 返回空值 nil 。
 *
 *  \~english
 *  The blocklist of the chat group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 *
 *  Only the group owner can call this method. Otherwise, the SDK returns nil.
 */
@property (nonatomic, strong, readonly) NSArray *blacklist;

/**
 *  \~chinese
 *  群组的被禁言列表。
 *
 *  该方法只有管理员或者群主才有权限调用。否则 SDK 返回空值 nil 。
 *
 *  \~english
 *  The list of muted members.
 *
 *  Only the group owner or admin can call this method. Otherwise, the SDK returns nil.
 */
@property (nonatomic, strong, readonly) NSArray *muteList;


/**
 *  \~chinese
 *  聊天室的白名单列表。
 *
 *  该方法只有群主才能调用，否则 SDK 返回空值 nil。
 *
 *  \~english
 *  The allowlist of the chat group. 
 *
 *  Only the group owner can call this method. Otherwise, the SDK returns nil.
 */
@property (nonatomic, strong, readonly) NSArray *whiteList;

/**
 *  \~chinese
 *  群共享文件列表。
 *
 *  \~english
 *  The list of group shared files.
 */
@property (nonatomic, strong, readonly) NSArray *sharedFileList;

/**
 *  \~chinese
 *  群组是否接收消息推送通知。
 *
 *  \~english
 *  Whether to enable the push notification service for the group.
 */
@property (nonatomic, readonly) BOOL isPushNotificationEnabled;

/**
 *  \~chinese
 *  此群是否为公开群，需要先通过 `getGroupSpecificationFromServerWithId` 获取群组详情。
 *
 *  \~english
 *  Whether the group is a public group. To get the value of this member, you need to call `getGroupSpecificationFromServerWithId` first.
 */
@property (nonatomic, readonly) BOOL isPublic;

/**
 *  \~chinese
 *  是否屏蔽群消息。
 *
 *  \~english
 *  Whether to block the current group‘s messages.
 */
@property (nonatomic, readonly) BOOL isBlocked;

/**
 *  \~chinese
 *  当前登录账号的群成员类型。
 *
 *  \~english
 *  The group membership type of the current login account.
 */
@property (nonatomic, readonly) EMGroupPermissionType permissionType;

/**
 *  \~chinese
 *  群组的所有成员(包含群主、管理员和普通成员）。
 *
 *  \~english
 *  All occupants of the group, including the group owner, admins, and all other group members.
 */
@property (nonatomic, strong, readonly) NSArray *users;

/**
 *  \~chinese
 *  群组当前的成员数量，包括群主、群管理员和普通成员。该方法需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 *
 *  \~english
 *  The total number of group occupants, including the owner, admins, and all other group members. To get the value of this member, you need to call getGroupSpecificationFromServerWithId first. 
 */
@property (nonatomic, readonly) NSInteger occupantsCount;

/**
 *  \~chinese
 *  群组成员是否全部被禁言。
 *
 *  \~english
 *  Whether all the group members are muted.
 */
@property (nonatomic, readonly) BOOL isMuteAllMembers;

/**
 *  \~chinese
 *  获取群组实例，如果不存在则创建。
 *
 *  @param aGroupId    群组 ID。
 *
 *  @result 群组实例。
 *
 *  \~english
 *  Gets the group instance. Creates an instance if it does not exist.
 *
 *  @param aGroupId  The group ID.
 *
 *  @result The group instance.
 */
+ (instancetype)groupWithId:(NSString *)aGroupId;

#pragma mark - EM_DEPRECATED_IOS 3.8.8
/**
 *  \~chinese
 *  群组属性配置，需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 * 
 *  已废弃，请用 {@link settings} 代替。
 *
 *  \~english
 *  Setting options of group, require fetch group's detail first.
 * 
 *  Deprecated. Please use  {@link settings}  instead.
 */
@property (nonatomic, strong, readonly) EMGroupOptions *setting __deprecated_msg("Use settings instead");

/**
 *  \~chinese
 *  群组的所有成员(包含owner、admins和members)。
 * 
 *  已废弃，请用 {@link users} 代替。
 *
 *  \~english
 *  All occupants of the group, includes the group owner and admins and all other group members.
 * 
 *  Deprecated. Please use  {@link users}  instead.
 */
@property (nonatomic, strong, readonly) NSArray *occupants
__deprecated_msg("Use users instead");



#pragma mark - EM_DEPRECATED_IOS 3.3.0

/**
 *  \~chinese
 *  群组的成员列表，需要先通过 `getGroupSpecificationFromServerWithId` 获取群组详情。
 * 
 *  已废弃，请用 {@link memberList} 代替。
 *
 *  \~english
 *  The member list of the group. Needs to fetch the group's detail first.
 * 
 *  Deprecated. Please use  {@link memberList}  instead.
 */
@property (nonatomic, copy, readonly) NSArray *members EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use -memberList instead");

/**
 *  \~chinese
 *  群组的黑名单，需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 *
 *  该方法只有群主才能调用，否则 SDK 返回空值 nil。
 * 
 *  已废弃，请用 {@link blacklist} 代替。
 *
 *  \~english
 *  Group‘s blocklist of blocked users. Needs to fetch the blockList method first.
 *
 *  Needs the owner's authority to access. Returns nil if user is not the group owner.
 * 
 *  Deprecated. Please use  {@link blacklist}  instead.
 */
@property (nonatomic, strong, readonly) NSArray *blackList EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use -blacklist instead");

/**
 *  \~chinese
 *  群组当前的成员数量，需要获取群详情, 包括群主、管理员和普通成员。
 * 
 *  已废弃，请用 {@link occupantsCount} 代替。
 *
 *  \~english
 *  The total number of group members, including the owner, admins, members. Needs to fetch the group's detail first.
 * 
 *  Deprecated. Please use  {@link occupantsCount}  instead.
 */
@property (nonatomic, readonly) NSInteger membersCount EM_DEPRECATED_IOS(3_1_0, 3_3_0, "Use -occupantsCount instead");

/**
 *  \~chinese
 *  群组的主题，需要先通过 getGroupSpecificationFromServerWithId 获取群组详情。
 * 
 *  已废弃，请用 {@link groupName} 代替。
 *
 *  \~english
 *  The subject of the group. To get the value of this member, you need to call getGroupSpecificationFromServerWithId first.
 * 
 *  Deprecated. Please use  {@link groupName}  instead.
 */
@property (nonatomic, copy, readonly) NSString *subject EM_DEPRECATED_IOS(3_1_0, 3_6_2, "Use -groupName instead");

#pragma mark - EM_DEPRECATED_IOS < 3.2.3

/**
 *  \~chinese
 *  初始化群组实例。
 *
 *  已废弃，请使用  {@link groupWithId:}  方法代替。
 *
 *  @result 空值
 *
 *  \~english
 *  Initializes a group instance.
 *
 *  Deprecated. Please use  {@link groupWithId:}  instead.
 *
 *  @result nil.
 */
- (instancetype)init __deprecated_msg("Use +groupWithId: instead");


/**
 *  \~chinese
 *  群组的黑名单，需要先调用获取群黑名单方法。
 * 
 *  已废弃，请使用  {@link blackList}  方法代替。
 *
 *  该方法只有群主才能调用，否则 SDK 返回空值 nil。
 *
 *  \~english
 *  The group‘s blocklist of blocked users. To get the value of this member, you need to call getGroupSpecificationFromServerWithId first.
 * 
 *  Deprecated. Please use  {@link blackList}  instead.
 *
 *  Needs owner's authority. Returns nil if the user is not the group owner.
 */
@property (nonatomic, strong, readonly) NSArray *bans __deprecated_msg("Use - blackList instead");

@end
