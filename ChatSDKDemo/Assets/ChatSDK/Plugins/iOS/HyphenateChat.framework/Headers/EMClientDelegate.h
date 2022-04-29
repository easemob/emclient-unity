/**
 *  \~chinese
 *  @header EMClientDelegate.h
 *  @abstract 协议提供了与账号登录状态相关的回调。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMClientDelegate.h
 *  @abstract The protocol provides callbacks related to account login status
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

/**
 *  \~chinese 
 *  网络连接状态。
 *
 *  \~english 
 *  The connection state.
 */
typedef enum {
    EMConnectionConnected = 0,  /**
                                 * \~chinese 服务器已连接。
                                 * \~english The SDK is connected to the chat server.
                                 */
    EMConnectionDisconnected,   /**
                                 * \~chinese 服务器未连接。
                                 * \~english The SDK is disconnected from the chat server.
                                 */
} EMConnectionState;

@class EMError;

/**
 *  \~chinese 
 *  @abstract EMClientDelegate 提供与账号登录状态相关的回调。
 *
 *  \~english 
 *  @abstract This protocol provides a number of utility classes callback
 */
@protocol EMClientDelegate <NSObject>

@optional

/**
 *  \~chinese
 *  SDK 连接服务器的状态变化时会接收到该回调。
 *
 *  SDK 会在以下情况下触发该回调：
 *  1.登录成功后，设备无法上网时。
 *  2.登录成功后，连接状态发生变化时。
 *
 *  @param aConnectionState 当前的连接状态。
 *
 *  \~english
 *  Occurs when the connection state between the SDK and the server changes.
 * 
 *  The SDK triggers this callback in any of the following situations:
 *   - After login, the device is disconnected from the internet.
 *   - After login, the network status changes.
 *
 *  @param aConnectionState  The current connection state.
 */
- (void)connectionStateDidChange:(EMConnectionState)aConnectionState;

/**
 *  \~chinese
 *  自动登录完成时的回调。
 *
 *  @param aError 错误信息，包含失败原因。
 *
 *  \~english
 *  Occurs when the auto login is completed.
 *
 *  @param aError Error   A description of the issue that caused this call to fail.
 */
- (void)autoLoginDidCompleteWithError:(EMError *)aError;

/**
 *  \~chinese
 *  当前登录账号在其它设备登录时会接收到此回调。
 *
 *  \~english
 *  Occurs when the current user account is logged in to another device.
 */
- (void)userAccountDidLoginFromOtherDevice;

/**
 *  \~chinese
 *  当前登录账号已经被从服务器端删除时会收到该回调。
 *
 *  \~english
 *  Occurs when the current chat user is removed from the server.
 */
- (void)userAccountDidRemoveFromServer;

/**
 *  \~chinese
 *  当前用户账号被禁用时会收到该回调。
 *
 *  \~english
 *  The delegate method will be invoked when the User account is forbidden.
 */
- (void)userDidForbidByServer;

/**
 *  \~chinese
 *  当前登录账号被强制退出时会收到该回调，有以下原因：
 *    - 密码被修改；
 *    - 登录设备数过多；
 *    - 服务被封禁;
 *    - 被强制下线;
 *
 *  \~english
 *  The delegate method will be invoked when current IM account is forced to logout with the following reasons:
 *    1. The password is modified;
 *    2. Logged in too many devices;
 *    3. User for forbidden;
 *    4. Forced offline.
 */
- (void)userAccountDidForcedToLogout:(EMError *)aError;

/**
 *  \~chinese
 *  token 即将过期 （使用声网 token 即 Agora Chat user token 登陆）。
 *
 *  \~english
 *  token will expire (log in using agoraToken)
 */
- (void)tokenWillExpire:(int)aErrorCode;

/**
 *  \~chinese
 *  token已经过期 （使用声网token agoraToken 登陆）
 *
 *  \~english
 *  token did expire (log in using agoraToken)
 */
- (void)tokenDidExpire:(int)aErrorCode;

#pragma mark - Deprecated methods

/**
 *  \~chinese
 *  SDK 连接服务器的状态变化时会接收到该回调。
 *
 *  有以下几种情况, 会引起该方法的调用:
 *  1. 登录成功后, 手机无法上网时, 会调用该回调；
 *  2. 登录成功后, 网络状态变化时, 会调用该回调。
 *
 *  已废弃，请用 {@link connectionStateDidChange:} 代替。
 * 
 *  @param aConnectionState 当前状态。
 *
 *  \~english
 *  Occurs when the connection to the server status changes.
 * 
 *  Deprecated. Please use  {@link connectionStateDidChange:}  instead.
 *  
 *  For example:
 *  1. After successful login, the phone can not access to the internet.
 *  2. After a successful login, the network status changes.
 *  
 *  @param aConnectionState The current connection state.
 */
- (void)didConnectionStateChanged:(EMConnectionState)aConnectionState __deprecated_msg("Use -connectionStateDidChange:");

/**
 *  \~chinese
 *  自动登录完成时的回调。
 * 
 *  已废弃，请用 {@link autoLoginDidCompleteWithError:} 代替。
 *
 *  @param aError 错误信息，包含方法失败的原因。
 *
 *  \~english
 *  Occurs when the Automatic login fails.
 * 
 *  Deprecated. Please use  {@link autoLoginDidCompleteWithError:}  instead.
 *
 *  @param aError Error  A description of the issue that caused this call to fail.
 */
- (void)didAutoLoginWithError:(EMError *)aError __deprecated_msg("Use -autoLoginDidCompleteWithError:");

/**
 *  \~chinese
 *  当前登录账号在其它设备登录时会接收到此回调。
 * 
 *  已废弃，请用 {@link userAccountDidLoginFromOtherDevice} 代替。
 *
 *  \~english
 *  Occurs when the current account login from other device.
 * 
 *  Deprecated, please use  {@link userAccountDidLoginFromOtherDevice}  instead.
 */
- (void)didLoginFromOtherDevice __deprecated_msg("Use -userAccountDidLoginFromOtherDevice");

/**
 *  \~chinese
 *  当前登录账号已经被从服务器端删除时会收到该回调。
 * 
 *  已废弃，请用 {@link userAccountDidRemoveFromServe} 代替。
 *
 *  \~english
 *  Occurs when the current login account is deleted from the server.
 * 
 *  Deprecated, please use  {@link userAccountDidRemoveFromServe}  instead.
 */
- (void)didRemovedFromServer __deprecated_msg("Use -userAccountDidRemoveFromServer");

@end
