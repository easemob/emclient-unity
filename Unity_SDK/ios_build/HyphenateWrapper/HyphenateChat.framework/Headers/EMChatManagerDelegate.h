/**
 *  \~chinese
 *  @header EMChatManagerDelegate.h
 *  @abstract 聊天相关代理协议类。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMChatManagerDelegate.h
 *  @abstract This protocol defines chat related callbacks.
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>
#import "EMRecallMessageInfo.h"

@class EMChatMessage;
@class EMError;

/**
 *  \~chinese
 *  聊天相关回调的协议类。
 *
 *  \~english
 *  The chat related callbacks.
 */
@protocol EMChatManagerDelegate <NSObject>

@optional

#pragma mark - Conversation

/**
 *  \~chinese
 *  会话列表发生变化的回调。
 *
 *  @param aConversationList  会话列表。 <EMConversation>
 *
 *  \~english
 *  Occurs when the conversation list changes.
 *
 *  @param aConversationList  The conversation NSArray. <EMConversation>
 */
- (void)conversationListDidUpdate:(NSArray *)aConversationList;

#pragma mark - Message

/**
 *  \~chinese
 *  收到消息的回调。
 *
 *  @param aMessages  消息列表。
 *
 *  \~english
 *  Occurs when the SDK receives new messages.
 *
 *  @param aMessages  The received messages. An NSArray of the <EMMessage> objects.
 */
- (void)messagesDidReceive:(NSArray *)aMessages;

/**
 *  \~chinese
 *  收到 CMD 消息代理。
 *
 *  @param aCmdMessages  CMD 消息列表。 
 *
 *  \~english
 *  Occurs when receiving command messages.
 *
 *  @param aCmdMessages  The command message NSArray. 
 */
- (void)cmdMessagesDidReceive:(NSArray *)aCmdMessages;

/**
 *  \~chinese
 *  收到已读回执代理。
 *
 *  @param aMessages  已读消息列表。 
 *
 *  \~english
 *  Occurs when receiving read acknowledgement in message list.
 *
 *  @param aMessages  The read messages.
 */
- (void)messagesDidRead:(NSArray *)aMessages;

/**
 *  \~chinese
 *  收到群消息已读回执代理。
 *
 *  @param aMessages  已读消息列表。
 *
 *  \~english
 *  Occurs when the SDK receives read receipts for group messages.
 *
 *  @param aMessages  The acknowledged message NSArray.
 * 
 *
 */
- (void)groupMessageDidRead:(EMChatMessage *)aMessage
                  groupAcks:(NSArray *)aGroupAcks;

/**
 *  \~chinese
 *  当前用户所在群已读消息数量发生变化的回调。
 *
 *  \~english
 *  Occurs when the current group read messages count changed.
 *
 */
- (void)groupMessageAckHasChanged;

/**
 * \~chinese
 * 收到会话已读回调代理。
 *
 * @param from  会话已读回执的发送方。
 * @param to    CHANNEL_ACK 接收方。
 *
 *  发送会话已读的是我方多设备：
 *     则 from 参数值是“我方登录” ID，to 参数值是“会话方”会话 ID，此会话“会话方”发送的消息会全部置为已读 isRead 为 YES。
 *  发送会话已读的是会话方：
 *     则 from 参数值是“会话方”会话 ID，to 参数值是“我方登录” ID，此会话“我方”发送的消息的 isReadAck 会全部置为 YES。
 *  注：此会话既会话方 ID 所代表的会话。
 *
 * \~english
 * Occurs when receiving the conversation read receipt.
 * @param from  The username who send channel_ack.
 * @param to    The username who receive channel_ack.
 *
 *  If the conversaion readack is from the current login ID's multiple devices:
 *       The value of the "FROM" parameter is current login ID, and the value of the "to" parameter is the conversation ID. All the messages sent by the conversation are set to read： "isRead" is set to YES.
 *  If the send conversation readack is from the conversation ID's device:
 *       The value of the "FROM" parameter is the conversation ID, and the value of the "to" parameter is current login ID. The "isReaAck" of messages sent by login ID in this session will all be set to YES.
 *  Note: This conversation is the conversation represented by the conversation ID.
 *
 *
 */
- (void)onConversationRead:(NSString *)from to:(NSString *)to;

/**
 *  \~chinese
 *  发送方收到消息已送达的回调。
 *
 *  @param aMessages  送达消息列表。 
 * 
 *  \~english
 *  Occurs when receiving delivered acknowledgement in message list.
 *
 *  @param aMessages  The acknowledged message NSArray. 
 */
- (void)messagesDidDeliver:(NSArray *)aMessages;

/**
 *  \~chinese
 *  收到消息撤回代理。
 *
 *  @param aMessages  撤回消息列表。 
 *
 *  \~english
 * Occurs when receiving recall for message list.
 *
 *  @param aMessages  The recall message NSArray. 
 */
- (void)messagesInfoDidRecall:(NSArray<EMRecallMessageInfo *> *)aRecallMessagesInfo;

/**
 *  \~chinese
 *  消息状态发生变化的回调。消息状态包括消息创建，发送，发送成功，发送失败。
 *
 *  需要给发送消息的 callback 参数传入 nil，此回调才会生效。
 *
 *  @param aMessage  状态发生变化的消息。
 *  @param aError    出错信息。
 *
 *  \~english
 *  Occurs when message status has changed. You need to set the parameter as nil.
 *
 *  @param aMessage  The message whose status has changed.
 *  @param aError    The error information.
 */
- (void)messageStatusDidChange:(EMChatMessage *)aMessage
                         error:(EMError *)aError;

/**
 *  \~chinese
 *  消息附件状态发生改变代理。
 *
 *  @param aMessage  附件状态发生变化的消息。
 *  @param aError    错误信息。
 *
 *  \~english
 *  Occurs when message attachment status changed.
 *
 *  @param aMessage  The message attachment status has changed.
 *  @param aError    The error information.
 */
- (void)messageAttachmentStatusDidChange:(EMChatMessage *)aMessage
                                   error:(EMError *)aError;


#pragma mark - Deprecated methods

/**
 *  \~chinese
 *  收到消息撤回代理。
 *
 *  @param aMessages  撤回消息列表<EMChatMessage>
 *
 *  \~english
 *  Delegate method will be invoked when receiving recall for message list.
 *
 *  @param aMessages  Recall message NSArray<EMChatMessage>
 */
- (void)messagesDidRecall:(NSArray *)aMessages __deprecated_msg("Use -messagesInfoDidRecall: instead");

/*!
 *  \~chinese
 *  会话列表发生变化代理。
 *  
 *  已废弃，请用 {@link conversationListDidUpdate:} 代替。
 *
 *  @param aConversationList  会话列表。
 *
 *  \~english
 *  Occurs when the conversation list changed.
 * 
 *  Deprecated. Please use  {@link conversationListDidUpdate:}  instead.
 *
 *  @param aConversationList  The conversation NSArray. 
 */
- (void)didUpdateConversationList:(NSArray *)aConversationList __deprecated_msg("Use -conversationListDidUpdate: instead");

/**
 *  \~chinese
 *  收到消息代理。
 * 
 *  已废弃，请用 {@link messagesDidReceive:} 代替。
 *
 *  @param aMessages  消息列表。
 *
 *  \~english
 *  Occurs when received messages.
 * 
 *  Deprecated. Please use  {@link messagesDidReceive:}  instead.
 *
 *  @param aMessages The message NSArray. 
 */
- (void)didReceiveMessages:(NSArray *)aMessages __deprecated_msg("Use -messagesDidReceive: instead");

/**
 *  \~chinese
 *  收到 CMD 消息代理。
 * 
 *  已废弃，请用 {@link cmdMessagesDidReceive:} 代替。
 *
 *  @param aCmdMessages  CMD 消息列表。
 *
 *  \~english
 *  Occurs when received cmd messages.
 * 
 *  Deprecated. Please use  {@link cmdMessagesDidReceive:}  instead.
 *
 *  @param aCmdMessages  Cmd message NSArray. 
 */
- (void)didReceiveCmdMessages:(NSArray *)aCmdMessages __deprecated_msg("Use -cmdMessagesDidReceive: instead");

/**
 *  \~chinese
 *  收到已读回执代理。
 * 
 *  已废弃，请用 {@link messagesDidRead:} 代替。
 *
 *  @param aMessages  已读消息列表。
 *
 *  \~english
 *  Occurs when receives read acks.
 * 
 *  Deprecated. Please use  {@link messagesDidRead:}  instead.
 *
 *  @param aMessages  Read acked message NSArray. 
 */
- (void)didReceiveHasReadAcks:(NSArray *)aMessages __deprecated_msg("Use -messagesDidRead: instead");

/**
 *  \~chinese
 *  收到消息送达回执代理。
 * 
 *  已废弃，请用 {@link messagesDidDeliver:} 代替。
 *
 *  @param aMessages  送达消息列表。
 *
 *  \~english
 *  Occurs when receives deliver acks.
 * 
 *  Deprecated. Please use  {@link messagesDidDeliver:}  instead.
 *
 *  @param aMessages  The deliver acked message NSArray. 
 */
- (void)didReceiveHasDeliveredAcks:(NSArray *)aMessages __deprecated_msg("Use -messagesDidDeliver: instead");

/**
 *  \~chinese
 *  消息状态发生变化代理。
 * 
 *  已废弃，请用 {@link messageStatusDidChange:error:} 代替。
 *
 *  @param aMessage  状态发生变化的消息。
 *  @param aError    出错信息。
 *
 *  \~english
 *  Occurs when message status changed.
 * 
 *  Deprecated. Please use  {@link messageStatusDidChange:error:}  instead.
 *
 *  @param aMessage  Message whose status changed.
 *  @param aError    The error information.
 */
- (void)didMessageStatusChanged:(EMChatMessage *)aMessage
                          error:(EMError *)aError __deprecated_msg("Use -messageStatusDidChange:error: instead");

/**
 *  \~chinese
 *  消息附件状态发生改变代理。
 * 
 *  已废弃，请用 {@link messageAttachmentStatusDidChange:error:} 代替。
 *  
 *  @param aMessage  附件状态发生变化的消息。
 *  @param aError    错误信息。
 *
 *  \~english
 *  Occurs when the attachment status has changed.
 * 
 *  Deprecated. Please use  {@link messageAttachmentStatusDidChange:error: }  instead.
 *
 *  @param aMessage  Message whose attachment status changed.
 *  @param aError    The error information.
 */
- (void)didMessageAttachmentsStatusChanged:(EMChatMessage *)aMessage
                                     error:(EMError *)aError __deprecated_msg("Use -messageAttachmentStatusDidChange:error: instead");
@end
