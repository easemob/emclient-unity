/**
 *  \~chinese
 *  @header EMOptions.h
 *  @abstract SDK的设置选项
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMOptions.h
 *  @abstract SDK setting options
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

#import "EMCommonDefs.h"

/**
 *  \~chinese
 *  日志输出级别。
 *
 *  \~english
 *  Log output level types.
 */
typedef enum
{
    EMLogLevelDebug = 0, /** \~chinese 输出所有日志。 \~english Output all logs. */
    EMLogLevelWarning,   /** \~chinese 输出警告及错误。 \~english Output warnings and errors. */
    EMLogLevelError      /** \~chinese 只输出错误。 \~english Output errors only. */
} EMLogLevel;

/**
 *  \~chinese
 *  SDK 的设置选项。
 *
 *  \~english
 *  The SDK setting options.
 */
@interface EMOptions : NSObject

/**
 *  \~chinese
 *  app key，是项目的唯一标识。
 *
 *  \~english
 *  The app key, which is the unique identifier of the project.
 */
@property(nonatomic, copy, readonly) NSString *appkey;

/**
 *  \~chinese
 *  控制台是否输出日志。默认值为 `NO`。
 *
 *  \~english
 *  Whether to print logs on Console. The default value is `NO`.
 */
@property(nonatomic, assign) BOOL enableConsoleLog;

/**
 *  \~chinese
 *  默认值为 `EMLogLevelDebug`，表示所有等级的日志。
 *
 *  \~english
 *  The log output level. The default value is EMLogLevelDebug, which means all log levels.
 */
@property(nonatomic, assign) EMLogLevel logLevel;

/**
 *  \~chinese
 *  是否只使用 HTTPS 协议。默认值为 `NO`。
 *
 *  \~english
 *  Whether to only use the HTTPS protocol. The default value is `NO`.
 */
@property(nonatomic, assign) BOOL usingHttpsOnly;

/**
 *  \~chinese
 *  是否自动登录，默认为 `YES`。
 *
 *  该参数需要在 SDK 初始化前设置，否则不生效。
 *
 *  \~english
 *  Whether to enable automatic login. The default value is `YES`.
 *
 *  You need to set this parameter before the SDK is initialized.
 */
@property(nonatomic, assign) BOOL isAutoLogin;

/**
 *  \~chinese
 *  离开群组时是否删除该群所有消息，默认为 `YES`。
 *
 *  \~english
 *  Whether to delete all the group messages when leaving the group. The default value is YES.
 */
@property(nonatomic, assign) BOOL deleteMessagesOnLeaveGroup;

/**
 *  \~chinese
 *  离开聊天室时是否删除所有消息，默认为 `YES`。
 *
 *  \~english
 *  Whether to delete all the chat room messages when leaving the chat room. The default value is YES.
 */
@property(nonatomic, assign) BOOL deleteMessagesOnLeaveChatroom;

/**
 *  \~chinese
 *  是否允许聊天室所有者离开，默认为 `YES`。
 *
 *  \~english
 *  Whether to allow the chatroom owner leave the room. The default value is YES.
 */
@property(nonatomic, assign) BOOL canChatroomOwnerLeave;

/**
 *  \~chinese
 *  是否自动接受群邀请。默认值为 `YES`。
 *
 *  \~english
 *  Whether to automatically accept group invitation. The default value is `YES`.
 */
@property(nonatomic, assign) BOOL autoAcceptGroupInvitation;

/**
 *  \~chinese
 *  是否自动同意好友邀请。默认值为 `NO`。
 *
 *  \~english
 *  Whether to automatically approve contact request. The default value is `NO`.
 */
@property(nonatomic, assign) BOOL autoAcceptFriendInvitation;

/**
 *  \~chinese
 *  是否自动下载图片和视频缩略图及语音消息，默认为 `YES`。
 *
 *  \~english
 *  Whether to automatically download image or video thumbnails and voice messages. The default value is `YES`.
 */
@property(nonatomic, assign) BOOL autoDownloadThumbnail;

/**
 * \~chinese
 * 是否需要接收信息接收方已读回执。默认值为 `YES`。
 *
 * \~english
 * Whether need to receive the message read receipt. The default value is `YES`.
 */
@property(nonatomic, assign) BOOL enableRequireReadAck;
/**
 *  \~chinese
 *  是否发送消息送达回执，默认为 `NO`，如果设置为 `YES`，SDK 收到单聊消息时会自动发送送达回执。
 *
 *  \~english
 *  Whether to send the message delivery receipt. The default value is `NO`. If you set it to `YES`, the SDK automatically send a delivery receipt when you receive a chat message.
 */
@property(nonatomic, assign) BOOL enableDeliveryAck;

/**
 *  \~chinese
 *  从数据库加载消息时是否按服务器时间排序，默认值为 `YES`，表示按按服务器时间排序。
 *
 *  \~english
 *  Whether to sort messages by server received time when loading message from database. The default value is `YES`.
 */
@property(nonatomic, assign) BOOL sortMessageByServerTime;

/**
 *  \~chinese
 * 是否自动上传或者下载消息中的附件，默认值为 `YES`。
 *
 *  \~english
 *  Whether to automatically upload or download the attachment in the message. The default value is `YES`.
 */
@property(nonatomic, assign) BOOL isAutoTransferMessageAttachments;

/**
 *  \~chinese
 *  iOS 特有属性，推送证书的名称。
 *
 *  消息推送的证书名称。该参数只能在调用 `initializeSDKWithOptions` 时设置，且 app 运行过程中不可以修改。
 *
 *  \~english
 *  The certificate name of Apple Push Notification Service.
 *
 *  Ensure that you set this parameter when calling `initializeSDKWithOptions`. During the app runtime, you can not change the settings.
 */
@property(nonatomic, copy) NSString *apnsCertName;

/**
 *  \~chinese
 *  iOS 特有属性，PushKit 的证书名称。
 *
 *  该参数只能在调用 `initializeSDKWithOptions` 时设置，且 app 运行过程中不可以修改。
 *
 *  \~english
 *  The certificate name of Apple PushKit Service.
 *
 *  Ensure that you set this parameter when calling `initializeSDKWithOptions`. During the app runtime, you can not change the settings.
 */
@property(nonatomic, copy) NSString *pushKitCertName;

/**
 *  \~chinese
 *  获取实例。
 *
 *  @param aAppkey  app key。
 *
 *  @result SDK 设置项实例。
 *
 *  \~english
 *  Gets a SDK setting options instance.
 *
 *  @param aAppkey  The app key.
 *
 *  @result  The SDK setting options instance.
 */
+ (instancetype)optionsWithAppkey:(NSString *)aAppkey;

#pragma mark - EM_DEPRECATED_IOS 3.8.8
/**
 *  \~chinese
 *  离开群组时是否删除该群所有消息, 默认为 YES。
 *
 *  \~english
 *  Whether to delete all the group messages when leaving the group. The default value is YES.
 */
@property(nonatomic, assign) BOOL isDeleteMessagesWhenExitGroup __deprecated_msg("Use deleteMessagesOnLeaveGroup instead");

/**
 *  \~chinese
 *  离开聊天室时是否删除所有消息, 默认为 YES。
 *
 *  \~english
 *  Whether to delete all the chat room messages when leaving the chat room. The default value is YES.
 */
@property(nonatomic, assign) BOOL isDeleteMessagesWhenExitChatRoom
    __deprecated_msg("Use deleteMessagesOnLeaveChatroom instead");

/**
 *  \~chinese
 *  是否允许聊天室所有者离开, 默认为 YES。
 *
 *  \~english
 *  if allow chat room's owner can leave the chat room. The default value is YES.
 */
@property(nonatomic, assign) BOOL isChatroomOwnerLeaveAllowed
    __deprecated_msg("Use canChatroomOwnerLeave instead");

/**
 *  \~chinese
 *  用户自动同意群邀请, 默认为 YES。
 *
 *  \~english
 *  Whether to automatically accept group invitation. The default value is YES.
 */
@property(nonatomic, assign) BOOL isAutoAcceptGroupInvitation
    __deprecated_msg("Use autoAcceptGroupInvitation instead");

/**
 *  \~chinese
 *  自动同意好友申请, 默认为 NO。
 *
 *  \~english
 *  Whether to automatically approve friend request. The default value is NO.
 */
@property(nonatomic, assign) BOOL isAutoAcceptFriendInvitation
    __deprecated_msg("Use autoAcceptFriendInvitation instead");

/**
 *  \~chinese
 *  是否自动下载图片和视频缩略图及语音消息, 默认为 YES。
 *
 *  \~english
 *  Whether to automatically download thumbnail of image&video and audio. The default value is YES.
 */
@property(nonatomic, assign) BOOL isAutoDownloadThumbnail
    __deprecated_msg("Use autoDownloadThumbnail instead");

#pragma mark - EM_DEPRECATED_IOS 3.2.3

/**
 *  \~chinese
 *  是否使用开发环境，默认为 NO。
 *
 *  已废弃。
 *
 *  只能在[EMClient initializeSDKWithOptions:]时设置，不能在程序运行过程中动态修改。
 *
 *  \~english
 *  Whether using development environment. The default value is NO.
 *
 *  Deprecated.
 *
 *  Can only be set when initializing the sdk with [EMClient initializeSDKWithOptions:]. Can't be altered in runtime.
 */
@property(nonatomic, assign) BOOL isSandboxMode EM_DEPRECATED_IOS(3_0_0, 3_2_2);

#pragma mark - EM_DEPRECATED_IOS 3.2.2

/**
 *  \~chinese
 *  是否使用 https，默认为 YES。
 *
 *  已废弃。
 *
 *  \~english
 *  Whether using https. The default value is YES.
 *
 *  Deprecated.
 */
@property(nonatomic, assign) BOOL usingHttps EM_DEPRECATED_IOS(3_0_0, 3_2_1);

@end
