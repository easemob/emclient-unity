/**
 *  \~chinese
 *  @header     EMContactManagerDelegate.h
 *  @abstract   联系人相关的代理协议
 *  @author     Hyphenate
 *  @version    3.00
 *
 *  \~english
 *  @header     EMContactManagerDelegate.h
 *  @abstract   The protocol of contact callbacks definitions
 *  @author     Hyphenate
 *  @version    3.00
 */

#import <Foundation/Foundation.h>

@class EMError;

/**
 *  \~chinese
 *  联系人相关的代理协议。
 *
 *  \~english
 *  The contact related callbacks.
 */
@protocol EMContactManagerDelegate <NSObject>

@optional

/**
 *  \~chinese
 *  用户 B 同意用户 A 的加好友请求后，用户 A 会收到这个回调
 *
 *  @param aUsername   用户 B 的 user ID
 *
 *  \~english
 *  Occurs when a friend request is approved, user A will receive this callback after user B approved user A's friend request.
 *
 *  @param aUsername    The user ID who approves a friend's request.
 */
- (void)friendRequestDidApproveByUser:(NSString *)aUsername;

/**
 *  \~chinese
 *  用户 B 拒绝用户 A 的加好友请求后，用户 A 会收到这个回调。
 *
 *  @param aUsername   用户 B 的 user ID
 *
 *  \~english
 *  Occurs when a friend request is declined.
 *
 *  User A will receive this callback after user B declined user A's friend request.
 *
 *  @param aUsername   The user ID who declined a friend request.
 */
- (void)friendRequestDidDeclineByUser:(NSString *)aUsername;

/**
 *  \~chinese
 *  用户 B 删除与用户 A 的好友关系后，用户 A，B 会收到这个回调
 *
 *  @param aUsername   用户 B 的 user ID
 *
 *  \~english
 *  Occurs when a user is removed as a contact by another user.
 *
 *  User A and B both will receive this callback after User B unfriended user A.
 *
 *  @param aUsername   The user who unfriended the current user
 */
- (void)friendshipDidRemoveByUser:(NSString *)aUsername;

/**
 *  \~chinese
 *  用户 B 同意用户 A 的好友申请后，用户 A 和用户 B 都会收到这个回调
 *
 *  @param aUsername   用户好友关系的另一方
 *
 *  \~english
 *  Occurs when the user is added as a contact by another user.
 *
 *  Both user A and B will receive this callback after User B agreed user A's add-friend invitation.
 *
 *  @param aUsername   Another user of the user‘s friend relationship.
 */
- (void)friendshipDidAddByUser:(NSString *)aUsername;

/**
 *  \~chinese
 *  用户 B 申请加 A 为好友后，用户 A 会收到这个回调。
 *
 *  @param aUsername   用户 B 的 user ID
 *  @param aMessage    好友邀请信息
 *
 *  \~english
 *  Occurs when a user received a friend request.
 *
 *  User A will receive this callback when received a friend request from user B.
 *
 *  @param aUsername   Friend request sender user ID
 *  @param aMessage    Friend request message
 */
- (void)friendRequestDidReceiveFromUser:(NSString *)aUsername
                                message:(NSString *)aMessage;

#pragma mark - Deprecated methods

/**
 *  \~chinese
 *  用户 B 同意用户 A 的加好友请求后，用户 A 会收到这个回调。
 * 
 *  已废弃，请用 {@link -friendRequestDidApproveByUser:} 代替。
 *
 *  @param aUsername   用户 B 的 user ID
 *
 *  \~english
 *  User A will receive this callback after user B accepted user A's friend request.
 * 
 *  Deprecated, please use  {@link -friendRequestDidApproveByUser:}  instead.
 *
 *  @param aUsername   The user ID of user B. 
 */
- (void)didReceiveAgreedFromUsername:(NSString *)aUsername __deprecated_msg("Use -friendRequestDidApproveByUser: instead");

/**
 *  \~chinese
 *  用户 B 拒绝用户 A 的加好友请求后，用户 A 会收到这个回调。
 * 
 * 已废弃，请用 {@link -friendRequestDidDeclineByUser:} 代替。
 *
 *  @param aUsername   用户 B 的 user ID
 *
 *  \~english
 *  User A will receive this callback after user B declined user A's add-friend invitation.
 * 
 *  Deprecated, please use  {@link -friendRequestDidDeclineByUser:}  instead.
 *
 *  @param aUsername   The user ID of user B.
 */
- (void)didReceiveDeclinedFromUsername:(NSString *)aUsername __deprecated_msg("Use -friendRequestDidDeclineByUser: instead");

/**
 *  \~chinese
 *  用户 B 删除与用户 A 的好友关系后，用户 A 会收到这个回调。
 * 
 *  已废弃，请用 {@link -friendshipDidRemoveByUser:} 代替。
 *
 *  @param aUsername   用户 B 的 user ID。
 *
 *  \~english
 *  User A will receive this callback after user B delete the friend relationship with user A.
 *
 *  Deprecated, please use  {@link -friendshipDidRemoveByUser:}  instead.
 * 
 *  @param aUsername   The user ID of user B.
 */
- (void)didReceiveDeletedFromUsername:(NSString *)aUsername __deprecated_msg("Use -friendshipDidRemoveByUser: instead");

/**
 *  \~chinese
 *  用户 B 同意用户 A 的好友申请后，用户 A 和用户 B 都会收到这个回调。
 * 
 *  已废弃，请用 {@link -friendshipDidAddByUser:} 代替。
 *
 *  @param aUsername   用户好友关系的另一方。
 *
 *  \~english
 *  Both user A and B will receive this callback after user B agreed user A's add-friend invitation.
 * 
 *  Deprecated, please use  {@link -friendshipDidAddByUser:}  instead.
 *
 *  @param aUsername   Another user of user‘s friend relationship.
 */
- (void)didReceiveAddedFromUsername:(NSString *)aUsername __deprecated_msg("Use -friendshipDidAddByUser: instead");

/**
 *  \~chinese
 *  用户 B 申请加 A 为好友后，用户 A 会收到这个回调。
 *  
 *  已废弃，请用 {@link -friendRequestDidReceiveFromUser:message:} 代替。
 *
 *  @param aUsername   用户 B 的 user ID
 *  @param aMessage    好友邀请信息
 *
 *  \~english
 *  User A will receive this callback after user B requested to add user A as a friend.
 * 
 *  Deprecated, please use  {@link -friendRequestDidReceiveFromUser:message:}  instead.
 *
 *  @param aUsername   The user ID of user B.
 *  @param aMessage    Friend invitation message
 */
- (void)didReceiveFriendInvitationFromUsername:(NSString *)aUsername
                                       message:(NSString *)aMessage __deprecated_msg("Use -friendRequestDidReceiveFromUser:message: instead");


@end
