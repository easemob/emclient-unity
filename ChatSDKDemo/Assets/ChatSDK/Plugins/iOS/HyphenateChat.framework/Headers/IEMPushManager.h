//
//  IEMPushManager.h
//  HyphenateSDK
//
//  Created by 杜洁鹏 on 2020/10/26.
//  Copyright © 2020 easemob.com. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "EMCommonDefs.h"
#import "EMPushOptions.h"
#import "EMError.h"

NS_ASSUME_NONNULL_BEGIN
/**
 *  \~chinese
 *  @header IEMPushManager.h
 *  @abstract 推送相关的管理协议类。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header IEMPushManager.h
 *  @abstract The push related management protocol class.
 *  @author Hyphenate
 *  @version 3.00
 */

@protocol IEMPushManager <NSObject>

/**
 *  \~chinese
 *  消息推送配置选项。
 *
 *  \~english
 *  The message push configuration options.
 *
 */
@property (nonatomic, strong, readonly) EMPushOptions *pushOptions;

/**
 *  \~chinese
 *  从内存中获取屏蔽了推送的用户 ID 列表。
 *
 *
 *  \~english
 *  Gets the list of user ID which have blocked the push notification.
 */
@property (nonatomic, strong, readonly) NSArray *noPushUIds;

/**
 *  \~chinese
 *  从内存中获取屏蔽了推送的群组 ID 列表。
 *
 *
 *  \~english
 *  Gets the list of groups which have blocked the push notification.
 */
@property (nonatomic, strong, readonly) NSArray *noPushGroups;

/**
 *  \~chinese
 *  开启离线推送。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @result  错误信息，详见 EMError。
 *
 *  \~english
 *  Turns on the push notification.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @result   The error information if the method fails: Error.
 */
- (EMError *)enableOfflinePush;


/**
 *  \~chinese
 *  关闭离线推送。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aStartHour    开始时间。
 *  @param aEndHour      结束时间。
 *
 *  @result  错误信息，详见 EMError。
 *
 *  \~english
 *  Turns off the push notification.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aStartHour    The begin time.
 *  @param aEndHour      The end time.
 *
 *  @result     The error information if the method fails: Error.
 */
- (EMError *)disableOfflinePushStart:(int)aStartHour end:(int)aEndHour;

/**
 *  \~chinese
 *  设置群组是否接收推送。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aGroupIds    群组 ID。
 *  @param disable      是否接收推送。
 *
 *  @result      错误信息，详见 EMError。
 *
 *  \~english
 *  Sets wether to turn on or turn off the push notification.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aGroupIds    The group IDs.
 *  @param disable      Turn off.
 *
 *  @result    The error information if the method fails: Error.
 */
- (EMError *)updatePushServiceForGroups:(NSArray *)aGroupIds
                            disablePush:(BOOL)disable;


/**
 *  \~chinese
 *  设置群组是否接收推送。
 * 
 *  异步方法。
 *
 *  @param aGroupIds            群组 ID。
 *  @param disable              是否接收推送。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sets the display style for the push notification.
 * 
 *  This is an asynchronous method.
 *
 *  @param aGroupIds            The group IDs.
 *  @param disable              Turn off.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails..
 */
- (void)updatePushServiceForGroups:(NSArray *)aGroupIds
                       disablePush:(BOOL)disable
                        completion:(nonnull void (^)(EMError * aError))aCompletionBlock;

/**
  *  \~chinese
  *  设置是否接收联系人消息推送。
  *
  *  同步方法，会阻塞当前线程。
  *
  *  @param aUIds        用户 ID。
  *  @param disable      是否不接收推送。默认值是 NO，表示接收推送；设置 YES，表示不接收推送。
  *
  *  @result             错误信息。
  *
  *  \~english
  *  Sets whether to receive push notification of the specific contacts.
  *
  *  This is a synchronous method and blocks the current thread.
  *
  *  @param aUIds        The user IDs of the contacts.
  *  @param disable      Whether to receive the push notification.// to do
  *
  *  @result    The error information if the method fails: Error.
  */
- (EMError *)updatePushServiceForUsers:(NSArray *)aUIds
                            disablePush:(BOOL)disable;

 /**
  *  \~chinese
  *  设置是否接收联系人消息推送。
  * 
  *  异步方法。
  *
  *  @param aUIds                用户 ID。
  *  @param disable              是否不接收推送。默认值是 NO，表示接收推送；设置 YES，表示不接收推送。
  *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
  *
  *  \~english
  *  Sets whether to receive the push notification of the contacts.
  * 
  *  This is an asynchronous method.
  *
  *  @param aUIds                The user IDs of the contacts.
  *  @param disable              Whether to receive the push notification.
  *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
  */
- (void)updatePushServiceForUsers:(NSArray *)aUIds
                        disablePush:(BOOL)disable
                        completion:(nonnull void (^)(EMError * aError))aCompletionBlock;

/**
 *  \~chinese
 *  设置推送消息显示的样式。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param pushDisplayStyle  要设置的推送样式。
 *
 *  @result  错误信息，详见 EMError。
 *
 *  \~english
 *  Sets the display style for the push notification.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param pushDisplayStyle  The display style of the push notification.
 *
 *  @result    The error information if the method fails: Error.
 */
- (EMError *)updatePushDisplayStyle:(EMPushDisplayStyle)pushDisplayStyle;


/**
 *  \~chinese
 *  设置推送的显示名。
 * 
 *  异步方法。
 *
 *  @param pushDisplayStyle     推送显示样式。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sets the display style for the push notification.
 * 
 *  This is an asynchronous method.
 *
 *  @param pushDisplayStyle     The display style of the push notification.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)updatePushDisplayStyle:(EMPushDisplayStyle)pushDisplayStyle
                    completion:(nonnull void (^)(EMError * aError))aCompletionBlock;


/**
 *  \~chinese
 *  设置推送消息显示的昵称。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aNickname  要设置的昵称。
 *
 *  @result  错误信息，详见 EMError。
 *
 *  \~english
 *  Sets the display name of the push notification.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aNickname  The display name of the push notification.
 *
 *  @result    The error information if the method fails: Error.
 */
- (EMError *)updatePushDisplayName:(NSString *)aDisplayName;

/**
 *  \~chinese
 *  设置推送的显示的昵称。
 * 
 *  异步方法。
 *
 *  @param aDisplayName     推送显示的昵称。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sets the display name of the push notification.
 * 
 *  This is an asynchronous method.
 *
 *  @param aDisplayName     The display name of the push notification.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)updatePushDisplayName:(NSString * _Nonnull)aDisplayName
                   completion:(void (^)(NSString * _Nonnull aDisplayName, EMError *aError))aCompletionBlock;



/**
 *  \~chinese
 *  从服务器获取推送属性。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param pError  错误信息。
 *
 *  @result   推送属性，详见 EMPushOptions。
 *
 *  \~english
 *  Gets the push options from the server.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param pError  The error information if the method fails: Error.
 *
 *  @result    The push options. See EMPushOptions.
 */
- (EMPushOptions *)getPushOptionsFromServerWithError:(EMError *_Nullable *_Nullable)pError;

/**
 *  \~chinese
 *  从服务器获取推送属性。
 * 
 *  异步方法。
 *
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Gets the push options from the server.
 * 
 *  This is an asynchronous method.
 *
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 */
- (void)getPushNotificationOptionsFromServerWithCompletion:(void (^)(EMPushOptions *aOptions, EMError *aError))aCompletionBlock;



@end

NS_ASSUME_NONNULL_END
