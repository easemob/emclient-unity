/**
 *  \~chinese
 *  @header IEMChatManager.h
 *  @abstract 聊天相关操作代理协议。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header IEMChatManager.h
 *  @abstract This protocol defines the operations of chat.
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

#import "EMCommonDefs.h"
#import "EMChatManagerDelegate.h"
#import "EMConversation.h"

#import "EMChatMessage.h"
#import "EMTextMessageBody.h"
#import "EMLocationMessageBody.h"
#import "EMCmdMessageBody.h"
#import "EMFileMessageBody.h"
#import "EMImageMessageBody.h"
#import "EMVoiceMessageBody.h"
#import "EMVideoMessageBody.h"
#import "EMCustomMessageBody.h"
#import "EMCursorResult.h"

#import "EMGroupMessageAck.h"

@class EMError;

/**
 *  \~chinese
 *  聊天相关操作代理协议。
 *  消息都是从本地数据库中加载，不是从服务端加载。
 *
 *  \~english
 *  This protocol defines the operations of chat.
 *  The current messages are loaded from the local database, not from the server.
 */
@protocol IEMChatManager <NSObject>

@required

#pragma mark - Delegate

/**
 *  \~chinese
 *  添加回调代理。
 *
 *  @param aDelegate  实现代理协议的对象。
 *  @param aQueue     执行代理方法的队列。
 *
 *  \~english
 *  Adds a delegate.
 *
 *  @param aDelegate  The object that implements the protocol.
 *  @param aQueue     (optional) The queue of calling delegate methods. If you want to run the app on the main thread, set this parameter as nil.
 */
- (void)addDelegate:(id<EMChatManagerDelegate>)aDelegate
      delegateQueue:(dispatch_queue_t)aQueue;

/**
 *  \~chinese
 *  移除回调代理。
 *
 *  @param aDelegate  要移除的代理。
 *
 *  \~english
 *  Removes a delegate.
 *
 *  @param aDelegate  The delegate to be removed.
 */
- (void)removeDelegate:(id<EMChatManagerDelegate>)aDelegate;

#pragma mark - Conversation

/**
 *  \~chinese
 *  获取所有会话，如果缓存中不存在会从本地数据库中加载。
 *
 *  @result 会话列表。
 *
 *  \~english
 *  Gets all conversations in the local database. The SDK loads the conversations from the cache first. If no conversation is in the cache, the SDK loads from the local database. 
 * 
 *  @result The conversation NSArray.
 */
- (NSArray *)getAllConversations;

/**
 *  \~chinese
 *  从服务器获取所有会话。
 * 
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Gets all conversations from the server.
 * 
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)getConversationsFromServer:(void (^)(NSArray *aCoversations, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  从本地数据库中获取一个已存在的会话。
 *
 *  @param aConversationId  会话 ID。
 *
 *  @result 会话对象。
 *
 *  \~english
 *  Gets a conversation from the local database.
 *
 *  @param aConversationId  The conversation ID.
 *
 *  @result The conversation object.
 */
- (EMConversation *)getConversationWithConvId:(NSString *)aConversationId;

/**
 *  \~chinese
 *  获取一个会话。
 *
 *  @param aConversationId  会话 ID。
 *  @param aType            会话类型。
 *  @param aIfCreate        如果不存在是否创建。
 *
 *  @result 会话对象。
 *
 *  \~english
 *  Gets a conversation from the local database.
 *
 *  @param aConversationId  The conversation ID.
 *  @param aType            The conversation type (Must be specified).
 *  @param aIfCreate        Whether to create the conversation if it does not exist.
 *
 *  @result The conversation.
 */
- (EMConversation *)getConversation:(NSString *)aConversationId
                               type:(EMConversationType)aType
                   createIfNotExist:(BOOL)aIfCreate;

/**
 *  \~chinese
 *  从本地数据库中删除一个会话。
 *
 *  @param aConversationId      会话 ID。
 *  @param aIsDeleteMessages    是否删除会话中的消息。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。

 *
 *  \~english
 *  Deletes a conversation from the local database.
 *
 *  @param aConversationId      The conversation ID.
 *  @param aIsDeleteMessages    Whether to delete the messages in the conversation.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method call fails.
 *
 */
- (void)deleteConversation:(NSString *)aConversationId
          isDeleteMessages:(BOOL)aIsDeleteMessages
                completion:(void (^)(NSString *aConversationId, EMError *aError))aCompletionBlock;

/*!
  *  \~chinese
  *  删除服务器会话
  *
  *  @param aConversationId      会话ID
  *  @param aConversationType    会话类型
  *  @param aIsDeleteMessages    是否删除会话中的消息
  *  @param aCompletionBlock     完成的回调
  *
  *  \~english
  *  Delete a conversation from server
  *
  *  @param aConversationId      conversation id
  *  @param aConversationType    conversation type
  *  @param aIsDeleteMessages    if delete messages
  *  @param aCompletionBlock     The callback of completion block
  *
  */
 - (void)deleteServerConversation:(NSString *)aConversationId
                 conversationType:(EMConversationType)aConversationType
           isDeleteServerMessages:(BOOL)aIsDeleteServerMessages
                       completion:(void (^)(NSString *aConversationId, EMError *aError))aCompletionBlock;

/*!
 *  \~chinese
 *  删除一组会话。
 *
 *  @param aConversations       会话列表。
 *  @param aIsDeleteMessages    是否删除会话中的消息。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Deletes multiple conversations.
 *
 *  @param aConversations       The conversation list.
 *  @param aIsDeleteMessages    Whether to delete the messages.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)deleteConversations:(NSArray *)aConversations
           isDeleteMessages:(BOOL)aIsDeleteMessages
                 completion:(void (^)(EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  导入一组会话到本地数据库。
 *
 *  @param aConversations   会话列表。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Imports multiple conversations to the local database.
 *
 *  @param aConversations       The conversation list. 
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)importConversations:(NSArray *)aConversations
                 completion:(void (^)(EMError *aError))aCompletionBlock;

#pragma mark - Message

/**
 *  \~chinese
 *  获取指定的消息。
 * 
 *  @param  aMessageId   消息 ID。
 * 
 *  @result   获取到的消息。
 *
 *  \~english
 *  Gets the specified message.
 *
 *  @param aMessageId    The message ID.
 * @result EMMessage     The message content.
 */
- (EMChatMessage *)getMessageWithMessageId:(NSString *)aMessageId;

/**
 *  \~chinese
 *  获取一个会话中消息附件的本地路径。
 *  
 *  删除会话时，会话中的消息附件也会被删除。
 *
 *  @param aConversationId  会话 ID。
 *
 *  @result 附件路径。
 *
 *  \~english
 *  Gets the local path of message attachments in a conversation. Delete the conversation will also delete the files under the file path.
 *
 *  @param aConversationId  The conversation ID.
 *
 *  @result The attachment path.
 */
- (NSString *)getMessageAttachmentPath:(NSString *)aConversationId;

/**
 *  \~chinese
 *  导入一组消息到本地数据库。
 *
 *  @param aMessages        消息列表。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Imports multiple messages to the local database.
 *
 *  @param aMessages            The message NSArray.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)importMessages:(NSArray *)aMessages
            completion:(void (^)(EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  更新消息到本地数据库，会话中最新的消息会先更新，消息 ID 不会更新。
 *
 *  @param aMessage         消息。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Updates a message in the local database. Latest Message of the conversation and other properties will be updated accordingly. MessageId of the message cannot be updated.
 *
 *  @param aMessage             The message instance.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)updateMessage:(EMChatMessage *)aMessage
           completion:(void (^)(EMChatMessage *aMessage, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  发送消息已读回执。
 *
 *  异步方法。
 *
 *  @param aMessage             消息 ID。
 *  @param aUsername            已读接收方。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sends the read receipt for a message.
 * 
 *  This is an asynchronous method.
 *
 *  @param aMessageId           The message ID.
 *  @param aUsername            The receiver of the read receipt.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)sendMessageReadAck:(NSString *)aMessageId
                    toUser:(NSString *)aUsername
                completion:(void (^)(EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  发送群消息已读回执。
 *
 *  异步方法。
 *
 *  @param aMessageId           消息 ID。
 *  @param aGroupId             群 ID。
 *  @param aContent             消息内容。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sends the read receipt for a group message.
 * 
 *  This is an asynchronous method.
 *
 *  @param aMessageId           The message ID.
 *  @param aGroupId             The group receiver ID.
 *  @param aContent             The message content.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)sendGroupMessageReadAck:(NSString *)aMessageId
                        toGroup:(NSString *)aGroupId
                        content:(NSString *)aContent
                     completion:(void (^)(EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  发送会话已读回执。
 *  
 *  该方法仅适用于单聊会话。
 *  
 *  发送会话已读回执会通知服务器将指定的会话未读消息数置为 0。调用该方法后对方会收到 onConversationRead 回调。
 *  对话方（包含多端多设备）将会在回调方法 EMChatManagerDelegate onConversationRead(String, String) 中接收到回调。
 *
 *  为了减少调用次数，我们建议在进入聊天页面有大量未读消息时调用该方法，在聊天过程中调用 sendMessageReadAck 方法发送消息已读回执。
 *  
 *
 *  异步方法。
 *
 *  @param conversationId         会话 ID。
 *  @param aCompletionBlock       该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sends the conversation read receipt to the server.
 * 
 *  This method applies to one-to-one chats only.
 * 
 *  This method call notifies the server to set the number of unread messages of the specified conversation as 0, and triggers the onConversationRead callback on the receiver's client.
 *
 *  To reduce the number of method calls, we recommend that you call this method when the user enters a conversation with many unread messages, and call sendMessageReadAck during a conversation to send the message read receipts.
 * 
 *  This is an asynchronous method.
 * 
 *  @param conversationId          The conversation ID.
 *  @param aCompletionBlock        The completion block, which contains the error message if the method fails.
 * 
 */
- (void)ackConversationRead:(NSString *)conversationId
                 completion:(void (^)(EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  撤回一条消息。
 *
 *  异步方法。
 *
 *  @param aMessageId           消息 ID。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Recalls a message.
 *
 *  This is an asynchronous method.
 *
 *  @param aMessageId           The message ID
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)recallMessageWithMessageId:(NSString *)aMessageId
                        completion:(void (^)(EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  发送消息。
 * 
 *  异步方法。
 *
 *  @param aMessage         消息。
 *  @param aProgressBlock   附件上传进度回调。如果该方法调用失败，会包含调用失败的原因。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sends a message.
 * 
 *  This is an asynchronous method.
 *
 *  @param aMessage             The message instance.
 *  @param aProgressBlock       The block of attachment upload progress in percentage, 0~100.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)sendMessage:(EMChatMessage *)aMessage
           progress:(void (^)(int progress))aProgressBlock
         completion:(void (^)(EMChatMessage *message, EMError *error))aCompletionBlock;

/**
 *  \~chinese
 *  重新发送消息。
 *
 *  @param aMessage         消息对象。
 *  @param aProgressBlock   附件上传进度回调 block。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Resends a message.
 *
 *  @param aMessage             The message object.
 *  @param aProgressBlock       The callback block of attachment upload progress.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)resendMessage:(EMChatMessage *)aMessage
             progress:(void (^)(int progress))aProgressBlock
           completion:(void (^)(EMChatMessage *message, EMError *error))aCompletionBlock;

/**
 *  \~chinese
 *  下载缩略图（图片消息的缩略图或视频消息的第一帧图片）。
 * 
 *  SDK 会自动下载缩略图。如果自动下载失败，你可以调用该方法下载缩略图。
 *
 *  @param aMessage            消息对象。
 *  @param aProgressBlock      附件下载进度回调 block。
 *  @param aCompletionBlock    该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Downloads the message thumbnail (the thumbnail of an image message or the first frame of a video message).
 * 
 *  The SDK automatically downloads the thumbnail. If the auto-download fails, you can call this method to manually download the thumbnail.
 *
 *  @param aMessage             The message object.
 *  @param aProgressBlock       The callback block of attachment download progress.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)downloadMessageThumbnail:(EMChatMessage *)aMessage
                        progress:(void (^)(int progress))aProgressBlock
                      completion:(void (^)(EMChatMessage *message, EMError *error))aCompletionBlock;

/**
 *  \~chinese
 *  下载消息附件（语音、视频、图片原图、文件）。
 * 
 *  SDK 会自动下载语音消息。如果自动下载失败，你可以调用该方法。
 *
 *  异步方法。
 *
 *  @param aMessage            消息。
 *  @param aProgressBlock      附件下载进度回调 block。
 *  @param aCompletionBlock    该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Downloads message attachment (voice, video, image or file). 
 *  
 *  SDK handles attachment downloading automatically. If automatic download failed, you can download attachment manually.
 *
 *  This is an asynchronous method.
 * 
 *  @param aMessage             The message object.
 *  @param aProgressBlock       The callback block of attachment download progress.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)downloadMessageAttachment:(EMChatMessage *)aMessage
                         progress:(void (^)(int progress))aProgressBlock
                       completion:(void (^)(EMChatMessage *message, EMError *error))aCompletionBlock;



/**
 *  \~chinese
 *  从服务器获取指定会话的消息。
 *
 *  @param  aConversationId     要获取消息的 Conversation ID。
 *  @param  aConversationType   要获取消息的 Conversation type。
 *  @param  aStartMessageId     起始消息的 ID。
 *  @param  aPageSize           获取消息条数。
 *  @param  pError              错误信息。
 *
 *  @result     获取的消息内容列表。
 *
 *
 *  \~english
 *  Fetches conversation messages from server.
 
 *  @param aConversationId      The conversation id which select to fetch message.
 *  @param aConversationType    The conversation type which select to fetch message.
 *  @param aStartMessageId      The start message id, if empty, start from the server‘s last message.
 *  @param aPageSize            The page size.
 *  @param pError               The error information if the method fails: Error.
 *
 *  @result    The messages list.
 */
- (EMCursorResult *)fetchHistoryMessagesFromServer:(NSString *)aConversationId
                                  conversationType:(EMConversationType)aConversationType
                                    startMessageId:(NSString *)aStartMessageId
                                          pageSize:(int)aPageSize
                                             error:(EMError **)pError;


/**
 *  \~chinese
 *  从服务器获取指定会话的消息。
 *
 *  异步方法。
 *
 *  @param  aConversationId     要获取消息的 Conversation ID。
 *  @param  aConversationType   要获取消息的 Conversation type。
 *  @param  aStartMessageId     起始消息的 ID。
 *  @param  aPageSize           获取消息条数。
 *  @param  aCompletionBlock    获取消息结束的 callback。
 *
 *
 *  \~english
 *  Fetches conversation messages from server.
 * 
 *  This is an asynchronous method.
 * 
 *  @param aConversationId      The conversation id which select to fetch message.
 *  @param aConversationType    The conversation type which select to fetch message.
 *  @param aStartMessageId      The start message id, if empty, start from the server last message.
 *  @param aPageSize            The page size.
 *  @param aCompletionBlock     The callback block of fetch complete, which contains the error message if the method fails.
 */
- (void)asyncFetchHistoryMessagesFromServer:(NSString *)aConversationId
                           conversationType:(EMConversationType)aConversationType
                             startMessageId:(NSString *)aStartMessageId
                                   pageSize:(int)aPageSize
                                 completion:(void (^)(EMCursorResult *aResult, EMError *aError))aCompletionBlock;


/**
 *  \~chinese
 *  从服务器获取指定群消息的已读回执，即指定的群消息有多少人已读。
 *
 *  异步方法。
 *
 *  @param  aMessageId           要获取的消息 ID。
 *  @param  aGroupId             要获取回执对应的群 ID。
 *  @param  aGroupAckId          要获取的群回执 ID。
 *  @param  aPageSize            获取消息条数。
 *  @param  aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *
 *  \~english
 *  Fetches the read back receipt of specified group messages from the server.
 * 
 *  This is an asynchronous method.
 * 
 *  @param  aMessageId           The message ID which select to fetch.
 *  @param  aGroupId             The group ID which select to fetch.
 *  @param  aGroupAckId          The group ack ID which select to fetch.
 *  @param  aPageSize            The page size.
 *  @param  aCompletionBlock     The callback block of fetch complete, which contains the error message if the method fails.
 */
- (void)asyncFetchGroupMessageAcksFromServer:(NSString *)aMessageId
                                     groupId:(NSString *)aGroupId
                             startGroupAckId:(NSString *)aGroupAckId
                                    pageSize:(int)aPageSize
                                  completion:(void (^)(EMCursorResult *aResult, EMError *error, int totalCount))aCompletionBlock;


#pragma mark - EM_DEPRECATED_IOS 3.6.1

/**
 *  \~chinese
 *  发送消息已读回执。
 *  已废弃，请用 {@link IEMChatManager sendMessageReadAck:toUser:completion:} 代替。
 *  异步方法。
 *
 *  @param aMessage             消息。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Sends read acknowledgement for a message.
 *  Deprecated. Please use {@link IEMChatManager sendMessageReadAck:toUser:completion:} instead.
 *  This is an asynchronous method.
 *
 *  @param aMessage             The message instance.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 *
 */
- (void)sendMessageReadAck:(EMChatMessage *)aMessage
                completion:(void (^)(EMChatMessage *aMessage, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_3_0, 3_6_1, "Use -[IEMChatManager sendMessageReadAck:toUser:completion:] instead");


/**
 *  \~chinese
 *  撤回消息。
 *  已废弃，请用 {@link IEMChatManager recallMessageWithMessageId:completion:} 代替。
 *  异步方法。
 *
 *  @param aMessage             消息。
 *  @param aCompletionBlock     该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Recalls a message.
 *  Deprecated. Please use {@link IEMChatManager recallMessageWithMessageId:completion:} instead.
 *  This is an asynchronous method.
 *
 *  @param aMessage             The message instance.
 *  @param aCompletionBlock     The completion block, which contains the error message if the method fails.
 */
- (void)recallMessage:(EMChatMessage *)aMessage
           completion:(void (^)(EMChatMessage *aMessage, EMError *aError))aCompletionBlock EM_DEPRECATED_IOS(3_3_0, 3_6_1, "Use -[IEMChatManager recallMessageWithMessageId:completion:] instead");

/*!
 *  \~chinese
 *  删除某个时间点之前的消息
 *
 *  异步方法
 *
 *  @param aTimestamp             要删除时间点
 *  @param aCompletion           完成回调
 *
 *  \~english
 *  Delete messages which before a special timestamp
 *
 *
 *  @param aTimestamp             The timestamp to delete
 *  @param aCompletion           The callback block of completion
 *
 */
- (void)deleteMessagesBefore:(NSUInteger)aTimestamp
                  completion:(void(^)(EMError*error))aCompletion;

#pragma mark - EM_DEPRECATED_IOS 3.2.3

/**
 *  \~chinese
 *  添加回调代理。
 *  已废弃，请用 {@link addDelegate:delegateQueue: } 代替。

 *  @param aDelegate  要添加的代理。
 *
 *  \~english
 *  Adds delegate.
 *  Deprecated. Please use {@link addDelegate:delegateQueue: }  instead.
 *
 *  @param aDelegate  The delegate.
 */
- (void)addDelegate:(id<EMChatManagerDelegate>)aDelegate EM_DEPRECATED_IOS(3_1_0, 3_2_2, "Use -IEMChatManager addDelegate:delegateQueue: instead");

#pragma mark - EM_DEPRECATED_IOS < 3.2.3

/**
 *  \~chinese
 *  从数据库中获取所有的会话，执行后会更新内存中的会话列表。
 *  
 *  同步方法，会阻塞当前线程。
 * 
 *  已废弃，请用 {@link getAllConversations} 代替。
 *
 *  @result 会话列表。 
 *
 *  \~english
 *  Loads all conversations from DB, will update conversation list in memory after this method is called.
 *
 *  This is a synchronous method and blocks the current thread.
 * 
 *  Deprecated. Please use  {@link getAllConversations}  instead.
 *
 *  @result The conversation list. 
 */
- (NSArray *)loadAllConversationsFromDB __deprecated_msg("Use -getAllConversations instead");

/**
 *  \~chinese
 *  删除会话。
 * 
 *  已废弃，请用 {@link deleteConversation:isDeleteMessages:completion:} 代替。
 *
 *  @param aConversationId  会话 ID。
 *  @param aDeleteMessage   是否删除会话中的消息。
 *
 *  @result 是否成功。
 *
 *  \~english
 *  Deletes a conversation.
 * 
 *  Deprecated. Please use  {@link deleteConversation:isDeleteMessages:completion:}  instead.
 *
 *  @param aConversationId  The conversation ID.
 *  @param aDeleteMessage   Whether to delete the messages.
 *
 *  @result Whether the messages are deleted successfully.
 */
- (BOOL)deleteConversation:(NSString *)aConversationId
            deleteMessages:(BOOL)aDeleteMessage __deprecated_msg("Use -deleteConversation:isDeleteMessages:completion: instead");

/**
 *  \~chinese
 *  删除一组会话。
 * 
 *  已废弃，请用 {@link deleteConversations:isDeleteMessages:completion:} 代替。
 *
 *  @param aConversations  会话列表。
 *  @param aDeleteMessage  是否删除会话中的消息。
 *
 *  @result 是否成功。
 *
 *  \~english
 *  Deletes multiple conversations.
 * 
 *  Deprecated. Please use  {@link deleteConversations:isDeleteMessages:completion:}  instead.
 *
 *  @param aConversations  The conversation list.
 *  @param aDeleteMessage  Whether delete messages.
 *
 *  @result Whether the messages are deleted successfully.
 */
- (BOOL)deleteConversations:(NSArray *)aConversations
             deleteMessages:(BOOL)aDeleteMessage __deprecated_msg("Use -deleteConversations:isDeleteMessages:completion: instead");

/**
 *  \~chinese
 *  导入一组会话到本地数据库。
 * 
 *  已废弃，请用 {@link importConversations:completion:} 代替。
 *
 *  @param aConversations  会话列表。
 *
 *  @result 是否成功。
 *
 *  \~english
 *  Imports multiple conversations to DB.
 * 
 *  Deprecated. Please use  {@link importConversations:completion:}  instead.
 *
 *  @param aConversations  The conversation list. 
 *
 *  @result Whether the conversations are imported successfully.
 */
- (BOOL)importConversations:(NSArray *)aConversations __deprecated_msg("Use -importConversations:completion: instead");

/**
 *  \~chinese
 *  导入一组消息到本地数据库。
 * 
 *  已废弃，请用 {@link importMessages:completion:} 代替。
 *
 *  @param aMessages  消息列表。
 *
 *  @result 是否成功。
 *
 *  \~english
 *  Imports multiple messages.
 * 
 *  Deprecated. Please use  {@link importMessages:completion:}  instead.
 *
 *  @param aMessages  The message list.
 *
 *  @result Whether the messages are imported successfully.
 */
- (BOOL)importMessages:(NSArray *)aMessages __deprecated_msg("Use -importMessages:completion: instead");

/**
 *  \~chinese
 *  更新消息到本地数据库。
 * 
 *  已废弃，请用 {@link updateMessage:completion:} 代替。
 *
 *  @param aMessage  消息。
 *
 *  @result 是否成功。
 *
 *  \~english
 *  Updates message to local data base.
 * 
 *  Deprecated. Please use  {@link updateMessage:completion:}  instead.
 *
 *  @param aMessage  The message instance.
 *
 *  @result Whether updated successfully.
 */
- (BOOL)updateMessage:(EMChatMessage *)aMessage __deprecated_msg("Use -updateMessage:completion: instead");

/**
 *  \~chinese
 *  发送消息已读回执。
 * 
 *  已废弃，请用 {@link sendMessageReadAck:completion:} 代替。
 *
 *  异步方法。
 *
 *  @param aMessage  消息。
 *
 *  \~english
 *  Sends read ack for a message.
 * 
 *  Deprecated. Please use  {@link sendMessageReadAck:completion:}  instead.
 *
 *  This is an asynchronous method.
 *
 *  @param aMessage  The message instance.
 */
- (void)asyncSendReadAckForMessage:(EMChatMessage *)aMessage __deprecated_msg("Use -sendMessageReadAck:completion: instead");

/**
 *  \~chinese
 *  发送消息。
 * 
 *  已废弃，请用 {@link sendMessage:progress:completion:} 代替。
 *  
 *  异步方法。
 *
 *  @param aMessage            消息。
 *  @param aProgressCompletion 附件上传进度回调 block。
 *  @param aCompletion         发送完成回调 block。
 *
 *  \~english
 *  Sends a message.
 * 
 *  Deprecated. Please use  {@link sendMessage:progress:completion:}  instead.
 *
 *  This is an asynchronous method.
 *
 *  @param aMessage            The message instance.
 *  @param aProgressCompletion The block of attachment upload progress.
 *
 *  @param aCompletion         The block of send complete.
 */
- (void)asyncSendMessage:(EMChatMessage *)aMessage
                progress:(void (^)(int progress))aProgressCompletion
              completion:(void (^)(EMChatMessage *message, EMError *error))aCompletion __deprecated_msg("Use -sendMessage:progress:completion: instead");

/**
 *  \~chinese
 *  重发送消息。
 * 
 *  已废弃，请用 {@link resendMessage:progress:completion:} 代替。
 *  
 *  异步方法。
 *
 *  @param aMessage            消息对象。
 *  @param aProgressCompletion 附件上传进度回调 block。
 *  @param aCompletion         发送完成回调 block。
 *
 *  \~english
 *  Resends a message.
 * 
 *  Deprecated. Please use  {@link resendMessage:progress:completion:}  instead.
 *
 *  This is an asynchronous method.
 *
 *  @param aMessage            The message object.
 *  @param aProgressCompletion The callback block of attachment upload progress.
 *  @param aCompletion         The callback block of send complete.
 */
- (void)asyncResendMessage:(EMChatMessage *)aMessage
                  progress:(void (^)(int progress))aProgressCompletion
                completion:(void (^)(EMChatMessage *message, EMError *error))aCompletion __deprecated_msg("Use -resendMessage:progress:completion: instead");

/**
 *  \~chinese
 *  下载缩略图（图片消息的缩略图或视频消息的第一帧图片）。
 * 
 *  SDK 会自动下载缩略图。如果自动下载失败，你可以调用该方法下载缩略图。
 * 
 *  已废弃，请用 {@link downloadMessageThumbnail:progress:completion:} 代替。
 * 
 *  异步方法。
 *
 *  @param aMessage            消息对象。
 *  @param aProgressCompletion 附件下载进度回调 block。
 *  @param aCompletion          该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Downloads message thumbnail attachments (thumbnails of image message or first frame of video image), SDK can download thumbail automatically, so user should NOT download thumbail manually except automatic download failed
 *
 *  Deprecated. Please use  {@link downloadMessageThumbnail:progress:completion:}  instead.
 * 
 *  This is an asynchronous method.
 *
 *  @param aMessage            The message object.
 *  @param aProgressCompletion The callback block of attachment download progress.
 *  @param aCompletion         The callback block of download complete.
 */
- (void)asyncDownloadMessageThumbnail:(EMChatMessage *)aMessage
                             progress:(void (^)(int progress))aProgressCompletion
                           completion:(void (^)(EMChatMessage * message, EMError *error))aCompletion __deprecated_msg("Use -downloadMessageThumbnail:progress:completion: instead");

/**
 *  \~chinese
 *  下载消息附件（语音，视频，图片原图，文件）。
 * 
 *  SDK 会自动下载语音消息，所以除非自动下载语音失败，用户不需要自动下载语音附件。
 * 
 *  已废弃，请用 {@link downloadMessageAttachment:progress:completion:} 代替。
 *  
 *  异步方法。
 *
 *  @param aMessage            消息对象。
 *  @param aProgressCompletion 附件下载进度回调 block。
 *  @param aCompletion         下载完成回调 block。
 *
 *  \~english
 *  Downloads message attachment(voice, video, image or file), SDK can download voice automatically, so user don't need to download voice manually except automatic download failed.
 *
 *  Deprecated. Please use  {@link downloadMessageAttachment:progress:completion:}  instead.
 * 
 *  This is an asynchronous method.
 *
 *  @param aMessage            The message object.
 *  @param aProgressCompletion The callback block of attachment download progress.
 *  @param aCompletion         The callback block of download complete.
 */
- (void)asyncDownloadMessageAttachments:(EMChatMessage *)aMessage
                               progress:(void (^)(int progress))aProgressCompletion
                             completion:(void (^)(EMChatMessage *message, EMError *error))aCompletion __deprecated_msg("Use -downloadMessageAttachment:progress:completion: instead");

/**
 *  \~chinese
 *  通过关键词从数据库获取消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 * 
 *  同步方法，会阻塞当前线程。
 *
 *  @param aType            消息类型。
 *  @param aTimestamp       参考时间戳。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aUsername        消息发送方。设为 nil 表示忽略该参数。
 *  @param aDirection       消息搜索方向。
 *
 *  @result 消息列表。 <EMChatMessage>
 *
 *  \~english
 *  Loads messages with the specified keyword from the local database.
 * 
 *  This method returns messages in the sequence of the timestamp when they are received.
 * 
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aType            The message type to load.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aUsername        Message sender (optional). Use aUsername=nil to ignore.
 *  @param aDirection       Message search direction
 *                          EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
 *                          EMMessageSearchDirectionDown: get aCount of messages after aMessageId.
 *
 *  @result EMChatMessage NSArray.
 *
 */
- (NSArray<EMChatMessage *> *)loadMessagesWithType:(EMMessageBodyType)aType
                   timestamp:(long long)aTimestamp
                       count:(int)aCount
                    fromUser:(NSString*)aUsername
             searchDirection:(EMMessageSearchDirection)aDirection;

/**
 *  \~chinese
 *  通过关键词从数据库获取消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 *
 *  @param aType            消息类型。
 *  @param aTimestamp       参考时间戳。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aUsername        消息发送方。设为 nil 表示忽略该参数。
 *  @param aDirection       消息搜索方向。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads messages with the specified keyword from the local database.
 * 
 *  This method returns messages in the sequence of the timestamp when they are received.
 *
 *  @param aType            The message type to load.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1
 *  @param aUsername        The user that sends the message. Setting it as nil means that the SDK ignores this parameter.
 *  @param aDirection       The message search direction: EMMessageSearchDirection.
 EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
 EMMessageSearchDirectionDown: get aCount of messages after aMessageId
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)loadMessagesWithType:(EMMessageBodyType)aType
                   timestamp:(long long)aTimestamp
                       count:(int)aCount
                    fromUser:(NSString*)aUsername
             searchDirection:(EMMessageSearchDirection)aDirection
                  completion:(void (^)(NSArray *aMessages, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  通过关键词从数据库获取消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 * 
 *  同步方法，会阻塞当前线程。
 *
 *  @param aKeywords        关键词。设为 nil 表示忽略该参数。
 *  @param aTimestamp       参考时间戳。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender          消息发送方。设为 nil 表示忽略该参数。
 *  @param aDirection       消息搜索方向。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Loads messages with the specified keyword from the local database. 
 *  
 *  This method returns messages in the sequence of the timestamp when they are received. 
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aKeyword         The keyword for searching the messages. Setting it as nil means that the SDK ignores this parameter.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aSender          The user that sends the message.
 *  @param aDirection       The message search direction: EMMessageSearchDirection.
 *
 *  @result EMChatMessage NSArray. <EMChatMessage>
 *
 */
- (NSArray<EMChatMessage *> *)loadMessagesWithKeyword:(NSString*)aKeywords
                      timestamp:(long long)aTimestamp
                          count:(int)aCount
                       fromUser:(NSString*)aSender
                searchDirection:(EMMessageSearchDirection)aDirection;

/**
 *  \~chinese
 *  通过关键词从数据库获取消息。
 * 
 *  该方法返回的消息按时间逆序返回排列。
 *
 *  @param aKeywords        搜索关键词，设为 nil 表示忽略该参数。
 *  @param aTimestamp       参考时间戳。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender          消息发送方。设为 nil 表示忽略该参数。
 *  @param aDirection       消息搜索方向。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads messages with the specified keyword from the local database.
 * 
 *  This method returns messages in the sequence of the timestamp when they are received.
 *
 *  @param aKeyword         The keyword for searching the messages. Setting it as nil means that the SDK ignores this parameter.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 EMMessageSearchDirectionDown: get aCount of messages after aMessageId *  ----
 *
 */
- (void)loadMessagesWithKeyword:(NSString*)aKeywords
                      timestamp:(long long)aTimestamp
                          count:(int)aCount
                       fromUser:(NSString*)aSender
                searchDirection:(EMMessageSearchDirection)aDirection
                     completion:(void (^)(NSArray *aMessages, EMError *aError))aCompletionBlock;

@end
