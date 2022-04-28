/**
 *  \~chinese
 *  @header EMChatMessage.h
 *  @abstract 聊天消息
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMChatMessage.h
 *  @abstract Chat message
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

#import "EMMessageBody.h"

/**
 *  \~chinese
 *  聊天类型。
 *
 *  \~english
 *  Chat types.
 */
typedef enum {
    EMChatTypeChat   = 0,   /** \~chinese 单聊。  \~english One-to-one chat. */
    EMChatTypeGroupChat,    /** \~chinese 群聊。  \~english Group chat. */
    EMChatTypeChatRoom,     /** \~chinese 聊天室。  \~english Chatroom. */
} EMChatType;

/**
 *  \~chinese
 *  消息发送状态。
 *
 *  \~english
 *  The message delivery status types.
 */
typedef enum {
    EMMessageStatusPending  = 0,    /** \~chinese 发送未开始。 \~english Message delivery is pending.*/
    EMMessageStatusDelivering,      /** \~chinese 正在发送。 \~english The message is being delivered.*/
    EMMessageStatusSucceed,         /** \~chinese 发送成功。 \~english The message is successfully delivered.*/
    EMMessageStatusFailed,          /** \~chinese 发送失败。 \~english The message fails to be delivered.*/
} EMMessageStatus;

/**
 *  \~chinese
 *  消息方向类型。
 *
 *  \~english
 *  The message direction types.
 */
typedef enum {
    EMMessageDirectionSend = 0,    /** \~chinese 该消息是当前用户发送出去的。\~english This message is sent from the local client.*/
    EMMessageDirectionReceive,     /** \~chinese 该消息是当前用户接收到的。 \~english The message is received by the local client.*/
} EMMessageDirection;

/**
 *  \~chinese
 *  聊天消息类。
 *
 *  \~english
 *  The chat message class.
 */
@interface EMChatMessage : NSObject

/**
 *  \~chinese
 *  消息 ID，是消息的唯一标识。
 *
 *  \~english
 *  The message ID, which is the unique identifier of the message.
 */
@property (nonatomic, copy) NSString *messageId;

/**
 *  \~chinese
 *  会话 ID，是会话的唯一标识。
 *
 *  \~english
 *  The conversation ID, which is the unique identifier of the conversation.
 */
@property (nonatomic, copy) NSString *conversationId;

/**
 *  \~chinese
 *  消息的方向。
 *
 *  \~english
 *  The message delivery direction.
 */
@property (nonatomic) EMMessageDirection direction;

/**
 *  \~chinese
 *  消息的发送方。
 *
 *  \~english
 *  The user sending the message.
 */
@property (nonatomic, copy) NSString *from;

/**
 *  \~chinese
 *  消息的接收方。
 *
 *  \~english
 *  The user receiving the message.
 */
@property (nonatomic, copy) NSString *to;

/**
 *  \~chinese
 *  服务器收到该消息的 Unix 时间戳，单位为毫秒。
 *
 *  \~english
 *  The Unix timestamp for the chat server receiving the message.
 */
@property (nonatomic) long long timestamp;

/**
 *  \~chinese
 *  客户端发送/收到此消息的时间。
 *
 *  \~english
 *  The Unix timestamp for the local client sending or receiving the message.
 */
@property (nonatomic) long long localTime;

/**
 *  \~chinese
 *  消息类型。
 *
 *  \~english
 *  The chat type.
 */
@property (nonatomic) EMChatType chatType;

/**
 *  \~chinese
 *  消息状态类型。
 *
 *  \~english
 *  The message delivery status type.
 */
@property (nonatomic) EMMessageStatus status;

/**
 *  \~chinese
 *  是否已发送（消息接收方）或收到（消息发送方）消息已读回执。
 *
 *  \~english
 *  Whether the message receipt is sent (for the clients that send the message) or received (for the clients that receive the message).
 */
@property (nonatomic) BOOL isReadAcked;

/**
 *  \~chinese
 *  是否需要发送群组已读消息回执。
 *
 *  \~english
 *  Whether needs to send the message receipt of the group.
 */
@property (nonatomic) BOOL isNeedGroupAck;

/**
 *  \~chinese
 *  收到的群组已读消息回执数量。
 *
 *  \~english
 *  The number of message receipt of the group.
 */
@property (nonatomic, readonly) int groupAckCount;

/**
 *  \~chinese
 *  是否已发送或收到消息送达回执。
 *  
 *      - 如果本地用户为信息的发送方，则该成员表示是否已收到送达回执。
 *      - 如果本地用户为信息的接收方，则该成员表示是否已发送送达回执。
 *  
 *  如果你将 EMOptions 中的 enableDeliveryAck 设为 YES，则 SDK 在收到消息后会自动发送送法回执。
 *
 *  \~english
 *  Whether the message delivery receipt is sent or received.
 *  
 *      If the message is sent by the local user, this member indicates whether the delivery receipt is received.
 *      If the message is received by the local user, this member indicates whether the delivery receipt is sent.
 *  
 *      If you set enableDeliveryAck in EMOptions as YES, the SDK automatically sends the delivery receipt after receiving a message.
 */
@property (nonatomic) BOOL isDeliverAcked;

/**
 *  \~chinese
 *  信息是否已读。
 *
 *  \~english
 *  Whether the message is read.
 */
@property (nonatomic) BOOL isRead;

/**
 *  \~chinese
 *  语音消息是否已播放。
 *
 *  \~english
 *  Whether the voice message is played out.
 */
@property (nonatomic) BOOL isListened;

/**
 *  \~chinese
 *  消息体。
 *
 *  \~english
 *  The message body.
 */
@property (nonatomic, strong) EMMessageBody *body;

/**
 *  \~chinese
 *  自定义消息扩展。
 *
 *  该参数数据形式是一个 Key-Value 的键值对，其中 Key 为 NSString 型，Value 为 NSString、NSNumber 类型的 Bool、Int、Unsigned int、long long 或 double.
 *
 *  \~english
 *  The message extension.
 *
 *  This member is in the Key-Value format, where the Key must be an NSString, and Value NSString or an NSNumber object, which includes int, unsigned int, long long, double, and NSNumber (@yES or @no instead of BOOL).
 */
@property (nonatomic, copy) NSDictionary *ext;

/**
 *  \~chinese
 *  初始化消息实例。
 *
 *  @param aConversationId  会话 ID。
 *  @param aFrom            消息发送方。
 *  @param aTo              消息接收方。
 *  @param aBody            消息体实例。
 *  @param aExt             扩展信息。
 *
 *  @result    消息实例。
 *
 *  \~english
 *  Initializes a message instance.
 *
 *  @param aConversationId   The conversation ID.
 *  @param aFrom           The user that sends the message.
 *  @param aTo         The user that receives the message.
 *  @param aBody             The message body.
 *  @param aExt              The message extention.
 *
 *  @result    The message instance.
 */
- (id)initWithConversationID:(NSString *)aConversationId
                        from:(NSString *)aFrom
                          to:(NSString *)aTo
                        body:(EMMessageBody *)aBody
                         ext:(NSDictionary *)aExt;


@end
