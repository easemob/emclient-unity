/**
 *  \~chinese
 *  @header EMConversation.h
 *  @abstract 聊天会话类。
 *  @author Hyphenate
 *  @version 3.00
 *
 *  \~english
 *  @header EMConversation.h
 *  @abstract Chat conversation
 *  @author Hyphenate
 *  @version 3.00
 */

#import <Foundation/Foundation.h>

#import "EMMessageBody.h"

/**
 *  \~chinese
 *  会话枚举类型。
 *
 *  \~english
 *  The conversation type.
 */
typedef enum {
    EMConversationTypeChat = 0,    /** \~chinese 单聊会话类型。  \~english The one-to-one chat type. */
    EMConversationTypeGroupChat,    /** \~chinese 群聊会话类型。  \~english The group chat type.*/
    EMConversationTypeChatRoom      /** \~chinese 聊天室会话类型。 \~english The chat room type.*/
} EMConversationType;

/**
 *  \~chinese
 *  消息搜索方向枚举类型。
 *
 *  \~english
 *  The message search direction type.
 */
typedef enum {
    EMMessageSearchDirectionUp  = 0,    /** \~chinese 向上搜索类型。  \~english The search older messages type.*/
    EMMessageSearchDirectionDown        /** \~chinese 向下搜索类型。 \~english The search newer messages type.*/
} EMMessageSearchDirection;

@class EMChatMessage;
@class EMError;

/**
 *  \~chinese
 *  聊天会话类。
 *
 *  \~english
 *  The chat conversation.
 */
@interface EMConversation : NSObject

/**
 *  \~chinese
 *  会话 ID。
 *  对于单聊类型，会话 ID 同时也是对方用户的名称。
 *  对于群聊类型，会话 ID 同时也是对方群组的 ID，并不同于群组的名称。
 *  对于聊天室类型，会话 ID 同时也是聊天室的 ID，并不同于聊天室的名称。
 *  对于 HelpDesk 类型，会话 ID 与单聊类型相同，是对方用户的名称。
 *
 *  \~english
 *  The conversation ID.
 *  For one-to-one chat，conversation ID is to chat user's name.
 *  For group chat, conversation ID is groupID(), different with getGroupName().
 *  For chat room, conversation ID is chatroom ID, different with chat room name().
 *  For help desk, it is same with one-to-one chat, conversation ID is also chat user's name.
 */
@property (nonatomic, copy, readonly) NSString *conversationId;

/**
 *  \~chinese
 *  会话类型。
 *
 *  \~english
 *  The conversation type.
 */
@property (nonatomic, assign, readonly) EMConversationType type;

/**
 *  \~chinese
 *  对话中未读取的消息数量。
 *
 *  \~english
 *  The number of unread messages.
 */
@property (nonatomic, assign, readonly) int unreadMessagesCount;

/**
 *  \~chinese
 *  对话中的消息数量
 *
 *  \~english
 *  Message count
 */
@property (nonatomic, assign, readonly) int messagesCount;

/**
 *  \~chinese
 *  会话扩展属性
 *
 *  \~english
 *  The conversation extension property.
 */
@property (nonatomic, copy) NSDictionary *ext;

/**
 *  \~chinese
 *  会话最新一条消息。
 *
 *  \~english
 *  The latest message in the conversation.
 */
@property (nonatomic, strong, readonly) EMChatMessage *latestMessage;

/**
 *  \~chinese
 *  收到的对方发送的最后一条消息，也是会话里的最新消息。
 *
 *  @result 消息实例。
 *
 *  \~english
 *  Gets the last received message.
 *
 *  @result The message instance.
 */
- (EMChatMessage *)lastReceivedMessage;

/**
 *  \~chinese
 *  插入一条消息在 SDK 本地数据库，消息的 conversation ID 应该和会话的 conversation ID 一致，消息会根据消息里的时间戳被插入 SDK 本地数据库，并且更新会话的 latestMessage 等属性。
 * 
 *  insertMessage 会更新对应 Conversation 里的 latestMessage。
 * 
 *  Method EMConversation insertMessage:error: = EMChatManager importMsessage:completion: + update conversation latest message
 *
 *  @param aMessage 消息实例。
 *  @param pError   错误信息。
 *
 *  \~english
 *  Inserts a message to a conversation in local database and SDK will update the last message automatically.
 * 
 *  The conversation ID of the message should be the same as conversation ID of the conversation in order to insert the message into the conversation correctly. The inserting message will be inserted based on timestamp.
 * 
 *  Method EMConversation insertMessage:error: = EMChatManager importMsessage:completion: + update conversation latest message
 *
 *  @param aMessage The message instance.
 *  @param pError   The error information if the method fails: Error.
 */
- (void)insertMessage:(EMChatMessage *)aMessage
                error:(EMError **)pError;

/**
 *  \~chinese
 *  插入一条消息到 SDK 本地数据库会话尾部，消息的 conversationId 应该和会话的 conversationId 一致，消息会被插入 SDK 本地数据库，并且更新会话的 latestMessage 等属性。
 *
 *  @param aMessage 消息实例。
 *  @param pError   错误信息。
 *
 *  \~english
 *  Inserts a message to the end of a conversation in local database. The conversationId of the message should be the same as the conversationId of the conversation in order to insert the message into the conversation correctly.
 *
 *  @param aMessage The message instance.
 *  @param pError   The error information if the method fails: Error.
 *
 */
- (void)appendMessage:(EMChatMessage *)aMessage
                error:(EMError **)pError;

/**
 *  \~chinese
 *  从 SDK 本地数据库删除一条消息。
 *
 *  @param aMessageId   要删除消失的 ID。
 *  @param pError       错误信息。
 *
 *  \~english
 *  Deletes a message.
 *
 *  @param aMessageId   The ID of the message to be deleted.
 *  @param pError       The error information if the method fails: Error.
 *
 */
- (void)deleteMessageWithId:(NSString *)aMessageId
                      error:(EMError **)pError;

/**
 *  \~chinese
 *  删除 SDK 本地数据库中该会话所有消息，清除 SDK 本地数据库中的消息。
 *
 *  @param pError       错误信息。
 *
 *  \~english
 *  Deletes all messages of the conversation from memory cache and local database.
 *
 *  @param pError       The error information if the method fails: Error.
 */
- (void)deleteAllMessages:(EMError **)pError;

/**
 *  \~chinese
 *  更新 SDK 本地数据库的消息。
 * 
 *  不能更新消息 ID，消息更新后，会话的 latestMessage 等属性进行相应更新。
 *
 *  @param aMessage 要更新的消息。
 *  @param pError   错误信息。
 *
 *  \~english
 *  Uses this method to update a message in local database. Changing properties will affect data in database.
 * 
 *  The latestMessage of the conversation and other properties will be updated accordingly. The messageID of the message cannot be updated.
 *
 *  @param aMessage The message to be updated.
 *  @param pError   The error information if the method fails: Error.
 *
 */
- (void)updateMessageChange:(EMChatMessage *)aMessage
                      error:(EMError **)pError;

/**
 *  \~chinese
 *  将 SDK 本地数据库消息设置为已读。
 *
 *  @param aMessageId   要设置消息的 ID。
 *  @param pError       错误信息。
 *
 *  \~english
 *  Marks a message as read.
 *
 *  @param aMessageId   The message ID.
 *  @param pError       The error information if the method fails: Error.
 *
 */
- (void)markMessageAsReadWithId:(NSString *)aMessageId
                          error:(EMError **)pError;

/**
 *  \~chinese
 *  将 SDK 本地数据库所有未读消息设置为已读。
 *
 *  @param pError   错误信息。
 *
 *  \~english
 *  Marks all messages as read.
 *
 *  @param pError   The error information if the method fails: Error.
 *
 */
- (void)markAllMessagesAsRead:(EMError **)pError;


#pragma mark - Load Messages Methods

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定 ID 的消息。
 *
 *  @param aMessageId       消息 ID。
 *  @param pError           错误信息。
 *
 *  \~english
 *  Gets a message with the ID.
 *
 *  @param aMessageId       The message ID.
 *  @param pError           The error information if the method fails: Error.
 *
 */
- (EMChatMessage *)loadMessageWithId:(NSString *)aMessageId
                           error:(EMError **)pError;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定数量的消息。
 * 
 *  取到的消息按时间排序，并且不包含参考的消息，如果传入消息的 ID 为空，则从最新消息取。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aMessageId       传入消息的 ID。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Loads messages starting from the specified message id from local database. 
 * 
 *  This method returns messages in the sequence of the timestamp when they are received.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aMessageId       The specified message ID.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aDirection       The message search direction: EMMessageSearchDirection.
                            EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
                            EMMessageSearchDirectionDown: get aCount of messages after aMessageId
 *
 *  @result EMChatMessage  The message instance.
 *
 */
- (NSArray<EMChatMessage *> *)loadMessagesStartFromId:(NSString *)aMessageId
                          count:(int)aCount
                searchDirection:(EMMessageSearchDirection)aDirection;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定数量的消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 *
 *  @param aMessageId       参考消息的 ID。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads messages starting from the specified message ID from local database.
 * 
 *  Returning messages are sorted by receiving timestamp based on EMMessageSearchDirection. If the aMessageId is nil, will return starting from the latest message.
 *
 *  @param aMessageId       The specified message ID loading messages start from.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aDirection       The message search direction: EMMessageSearchDirection. 
                            EMMessageSearchDirectionUp: get aCount of messages before the timestamp of the specified message ID; 
                            EMMessageSearchDirectionDown: get aCount of messages after the timestamp of the specified message ID.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)loadMessagesStartFromId:(NSString *)aMessageId
                          count:(int)aCount
                searchDirection:(EMMessageSearchDirection)aDirection
                     completion:(void (^)(NSArray *aMessages, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定类型的消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aType            消息类型，txt:文本消息，img：图片消息，loc：位置消息，audio：语音消息，video：视频消息，file：文件消息，cmd: 透传消息。
 *  @param aTimestamp       当前传入消息的设备时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aUsername        消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Loads messages with specified message type from local database. Returning messages are sorted by receiving timestamp based on EMMessageSearchDirection.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aType            The message type to load. Type includes txt : text msg, img : image msg, loc: location msg, audio: audio msg, video: video msg, file: file msg, cmd:  command msg.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aUsername        The user that sends the message. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aDirection       The message search direction: EMMessageSearchDirection.
                            EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
                            EMMessageSearchDirectionDown: get aCount of messages after aMessageId
 *
 *  @result EMChatMessage  The message instance.
 *
 */
- (NSArray<EMChatMessage *> *)loadMessagesWithType:(EMMessageBodyType)aType
                   timestamp:(long long)aTimestamp
                       count:(int)aCount
                    fromUser:(NSString*)aUsername
             searchDirection:(EMMessageSearchDirection)aDirection;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定类型的消息，取到的消息按时间排序，如果参考的时间戳为负数，则从最新消息取，如果 aCount 小于等于 0 当作 1 处理。
 *
 *  @param aType            消息类型，txt:文本消息，img：图片消息，loc：位置消息，audio：语音消息，video：视频消息，file：文件消息，cmd: 透传消息。
 *  @param aTimestamp       当前传入消息的设备时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aUsername        消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads messages with specified message type from local database. Returning messages are sorted by receiving timestamp based on EMMessageSearchDirection.
 *
 *  @param aType            The message type to load. Type includes txt : text msg, img : image msg, loc: location msg, audio: audio msg, video: video msg, file: file msg, cmd:  command msg.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aUsername        The message sender (optional). If the parameter is nil, ignore.
 *  @param aDirection       The message search direction.
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
 *  从 SDK 本地数据库获取包含指定内容的全部类型的消息，取到的消息按时间排序，如果参考的时间戳为负数，则从最新消息向前取，如果 aCount 小于等于 0 当作 1 处理。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aKeywords        搜索关键词，设为 NIL 表示忽略该参数。
 *  @param aTimestamp       传入的时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender          消息发送方，设为 NIL 表示忽略该参数。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Loads messages with specified keyword from local database, returning messages are sorted by receiving timestamp based on EMMessageSearchDirection. If reference timestamp is negative, load from the latest messages; if message count is negative, will be handled as count=1
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aKeyword         The keyword for searching the messages. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aTimestamp       The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aSender          The message sender (optional). If the parameter is nil, ignore.
 *  @param aDirection       The message search direction: EMMessageSearchDirection.
                            EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
                            EMMessageSearchDirectionDown: get aCount of messages after aMessageId *  ----
 *
 *  @result EMChatMessage  The message list.
 *
 */
- (NSArray<EMChatMessage *> *)loadMessagesWithKeyword:(NSString*)aKeyword
                      timestamp:(long long)aTimestamp
                          count:(int)aCount
                       fromUser:(NSString*)aSender
                searchDirection:(EMMessageSearchDirection)aDirection;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取包含指定内容的全部类型的消息，取到的消息按时间排序，如果参考的时间戳为负数，则从最新消息向前取，如果 aCount 小于等于 0 当作 1 处理。
 *
 *  @param aKeywords        关键词。设为 NIL 表示忽略该参数。
 *  @param aTimestamp       传入的 Unix 时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender          消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads messages with specified keyword from local database. Returning messages are sorted by receiving timestamp based on EMMessageSearchDirection. If reference timestamp is negative, load from the latest messages; if message count is negative, will be handled as count=1
 *
 *  @param aKeyword         The search keyword. If aKeyword=nil, ignore.
 *  @param aTimestamp       The load based on reference timestamp. If aTimestamp=-1, will load from the most recent (the latest) message
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aSender          The message sender (optional). If the parameter is nil, ignore.
 *  @param aDirection       The message search direction.
                            EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
                            EMMessageSearchDirectionDown: get aCount of messages after aMessageId *  ----
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)loadMessagesWithKeyword:(NSString*)aKeyword
                      timestamp:(long long)aTimestamp
                          count:(int)aCount
                       fromUser:(NSString*)aSender
                searchDirection:(EMMessageSearchDirection)aDirection
                     completion:(void (^)(NSArray *aMessages, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取包含指定内容的自定义类型消息，如果参考的时间戳为负数，则从最新消息向前取，如果 aCount 小于等于 0 当作 1 处理。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aKeywords        关键词。设为 NIL 表示忽略该参数。
 *  @param aTimestamp       传入的时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender          消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *
 *  \~english
 *  Loads custom messages with specified keyword from local database. The returning messages are sorted by receiving timestamp based on EMMessageSearchDirection. If the reference timestamp is negative, load from the latest messages; if message count is negative, will be handled as count=1.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aKeyword         The search keyword. If aKeyword=nil, ignore.
 *  @param aTimestamp       The load based on reference timestamp. If aTimestamp=-1, will load from the most recent (the latest) message
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aSender          The user that sends the message. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aDirection       The message search direction.
                            EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
                            EMMessageSearchDirectionDown: get aCount of messages after aMessageId *  ----
 *
 *  @result EMChatMessage  The message list.
 *
 */
- (NSArray<EMChatMessage *> *)loadCustomMsgWithKeyword:(NSString*)aKeyword
                       timestamp:(long long)aTimestamp
                           count:(int)aCount
                        fromUser:(NSString*)aSender
                 searchDirection:(EMMessageSearchDirection)aDirection;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取包含指定内容的自定义类型消息，如果参考的时间戳为负数，则从最新消息向前取，如果 aCount 小于等于 0 当作 1 处理。
 *
 *  @param aKeywords        关键词。设为 NIL 表示忽略该参数。
 *  @param aTimestamp       传入的 Unix 时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender          消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection       消息搜索方向，详见 EMMessageSearchDirection。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads custom messages with specified keyword from local database. returning messages are sorted by receiving timestamp based on EMMessageSearchDirection. If reference timestamp is negative, load from the latest messages; if message count is negative, will be handled as count=1
 *
 *  @param aKeyword         The keyword for searching the messages. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aTimestamp       The reference Unix timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aSender          The user that sends the message. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aDirection       The message search direction: EMMessageSearchDirection.
                            EMMessageSearchDirectionUp: get aCount of messages before aMessageId;
                            EMMessageSearchDirectionDown: get aCount of messages after aMessageId *  ----
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)loadCustomMsgWithKeyword:(NSString*)aKeyword
                       timestamp:(long long)aTimestamp
                           count:(int)aCount
                        fromUser:(NSString*)aSender
                 searchDirection:(EMMessageSearchDirection)aDirection
                      completion:(void (^)(NSArray *aMessages, EMError *aError))aCompletionBlock;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定时间段内的消息，取到的消息按时间排序，为了防止占用太多内存，建议你指定加载消息的最大数。
 * 
 *  该方法返回的消息按时间顺序排列。
 *
 *  同步方法，会阻塞当前线程。
 *
 *  @param aStartTimestamp  毫秒级开始时间。
 *  @param aEndTimestamp    结束时间。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *
 *  @result 消息列表。
 *
 *
 *  \~english
 *  Loads messages within specified time range from local database. Returning messages are sorted by sending timestamp.
 *
 *  This is a synchronous method and blocks the current thread.
 *
 *  @param aStartTimestamp  The starting timestamp in miliseconds.
 *  @param aEndTimestamp    The ending timestamp in miliseconds.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *
 *  @result EMChatMessage       The message list.
 *
 */
- (NSArray<EMChatMessage *> *)loadMessagesFrom:(long long)aStartTimestamp
                      to:(long long)aEndTimestamp
                   count:(int)aCount;

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定时间段内的消息，取到的消息按时间排序，为了防止占用太多内存，建议你指定加载消息的最大数。
 *
 *  @param aStartTimestamp  毫秒级开始时间。
 *  @param aEndTimestamp    结束时间。
 *  @param aCount           获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aCompletionBlock 该方法完成调用的回调。如果该方法调用失败，会包含调用失败的原因。
 *
 *  \~english
 *  Loads messages within specified time range from local database. Returning messages are sorted by sending timestamp.
 *
 *  @param aStartTimestamp  The starting timestamp in miliseconds.
 *  @param aEndTimestamp    The ending timestamp in miliseconds.
 *  @param aCount           The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aCompletionBlock The completion block, which contains the error message if the method fails.
 *
 */
- (void)loadMessagesFrom:(long long)aStartTimestamp
                      to:(long long)aEndTimestamp
                   count:(int)aCount
              completion:(void (^)(NSArray *aMessages, EMError *aError))aCompletionBlock;

#pragma mark - Deprecated methods

/**
 *  \~chinese
 *  在 SDK 本地数据库插入一条消息，消息的 conversation ID 应该和会话的 conversation ID 一致，消息会被插入 SDK 本地数据库，并且更新会话的 latestMessage 等属性。
 *
 *  已废弃，请用 {@link insertMessage:error:} 替代。
 *  
 *  @param aMessage  消息实例。
 *
 *  @result 是否成功： YES: 插入成功； NO: 插入失败。
 *
 *  \~english
 *  Inserts a message to a conversation. 
 * 
 *  Deprecated. Please use  {@link insertMessage:error:}  instead.
 * 
 *  The conversation ID of the message should be the same as the conversation ID of the conversation in order to insert the message into the conversation correctly.
 *
 *  @param aMessage  The message instance.
 *
 *  @result The result. YES means success, NO means failure.
 */
- (BOOL)insertMessage:(EMChatMessage *)aMessage __deprecated_msg("Use -insertMessage:error: instead");

/**
 *  \~chinese
 *  插入一条消息到 SDK 本地数据库会话尾部。
 * 
 *  该方法已废弃，请用 {@link appendMessage:error:} 代替。
 * 
 *  消息的 conversationId 应该和会话的 conversationId 一致，消息会被插入 SDK 本地数据库，并且更新会话的 latestMessage 等属性。
 *
 *  @param aMessage  消息实例
 *
 *  @result 是否成功  YES: 插入成功  NO: 插入失败
 *
 *  \~english
 *  Inserts a message to the tail of conversation.
 *
 *  Deprecated. Please use  {@link appendMessage:error:}  instead.
 * 
 *  The message's conversationId should be the same as the conversation's conversationId, message will be inserted to DB, and update conversation's property.
 *
 *  @param aMessage  The message instance.
 *
 *  @result  The result. YES means success, NO means failure.
 */
- (BOOL)appendMessage:(EMChatMessage *)aMessage __deprecated_msg("Use -appendMessage:error: instead");

/**
 *  \~chinese
 *  删除 SDK 本地数据库中一条消息。
 *
 *  已废弃，请用 {@link deleteMessageWithId:error:} 替代。
 * 
 *  @param aMessageId  要删除消息的 ID。
 *
 *  @result 是否成功  YES: 删除成功  NO: 删除失败。
 *
 *  \~english
 *  Deletes a message.
 * 
 *  Deprecated. Please use  {@link deleteMessageWithId:error:}  instead.
 *
 *  @param aMessageId  The ID of the message to be deleted.
 *
 *  @result The result. YES means success, NO means failure.
 */
- (BOOL)deleteMessageWithId:(NSString *)aMessageId __deprecated_msg("Use -deleteMessageWithId:error: instead");

/**
 *  \~chinese
 *  删除 SDK 本地数据库该会话所有消息。
 * 
 *  已废弃，请用 {@link deleteAllMessages:} 替代。
 *
 *  @result 是否成功 YES: 删除成功  NO: 删除失败
 *
 *  \~english
 *  Deletes all message of the conversation.
 * 
 *  Deprecated. Please use  {@link deleteAllMessages:}  instead.
 *
 *  @result Delete result, YES:  mean success,  NO:  mean failure.
 */
- (BOOL)deleteAllMessages __deprecated_msg("Use -deleteAllMessages: instead");

/**
 *  \~chinese
 *  更新 SDK 本地数据库的一条消息。
 * 
 *  不能更新消息 ID，消息更新后，会话的 latestMessage 等属性进行相应更新。
 * 
 *  已废弃，请用 {@link updateMessageChange:error:} 替代。
 *
 *  @param aMessage  要更新的消息。
 *
 *  @result 是否成功   YES: 更新成功  NO: 更新失败。
 *
 *  \~english
 *  Updates a message.
 * 
 *  The message's ID can't be updated. The conversation's latestMessage and other properties will be updated after the message been updated.
 *
 *  Deprecated. Please use  {@link updateMessageChange:error:}  instead.
 * 
 *  @param aMessage  The message to be updated.
 *
 *  @result The result. YES means success, NO means failure.
 */
- (BOOL)updateMessage:(EMChatMessage *)aMessage __deprecated_msg("Use -updateMessageChange:error: instead");

/**
 *  \~chinese
 *  将 SDK 本地数据库消息设置为已读。
 * 
 *  已废弃，请用 {@link markMessageAsReadWithId:error:} 替代。
 *
 *  @param aMessageId  要设置消息的 ID。
 *
 *  @result 是否成功。 YES: 更新成功  NO: 更新失败。
 *
 *  \~english
 *  Marks a message as read.
 * 
 *  Deprecated. Please use  {@link markMessageAsReadWithId:error:}  instead.
 *
 *  @param aMessageId  Message ID who will be set read status.
 *
 *  @result The result of mark message as read. YES means success, NO means failure.
 */
- (BOOL)markMessageAsReadWithId:(NSString *)aMessageId __deprecated_msg("Use -markMessageAsReadWithId:error: instead");

/**
 *  \~chinese
 *  将 SDK 本地数据库所有未读消息设置为已读。
 * 
 *  已废弃，请用 {@link markAllMessagesAsRead:} 替代。
 *
 *  @result 是否成功  YES: 更新成功  NO: 更新失败
 *
 *  \~english
 *  Marks all message as read.
 * 
 *  Deprecated. Please use  {@link markAllMessagesAsRead:}  instead.
 *
 *  @result The result of mark all message as read. YES means success, NO means failure.
 */
- (BOOL)markAllMessagesAsRead __deprecated_msg("Use -markAllMessagesAsRead: instead");

/**
 *  \~chinese
 *  更新会话扩展属性到 SDK 本地数据库。
 * 
 *  已废弃。
 *
 *  @result 是否成功。YES: 更新成功；NO: 更新失败。
 *
 *
 *  \~english
 *  Updates the conversation's extend properties to DB.
 * 
 *  Deprecated.
 *
 *  @result Extend properties update result, YES:  mean success,  NO:  mean failure.
 */
- (BOOL)updateConversationExtToDB __deprecated_msg("setExt: will update extend properties to DB");

/**
 *  \~chinese
 *  从  SDK 本地数据库获取指定 ID 的消息。
 * 
 *  已废弃，请用 {@link loadMessageWithId:error:} 代替。
 *
 *  @param aMessageId  消息 ID。
 *
 *  @result 消息实例。
 *
 *  \~english
 *  Gets a message with the ID.
 * 
 *  Deprecated. Please use  {@link loadMessageWithId:error:}  instead.
 *
 *  @param aMessageId  The message ID.
 *
 *  @result The message instance.
 */
- (EMChatMessage *)loadMessageWithId:(NSString *)aMessageId __deprecated_msg("Use -loadMessageWithId:error: instead");

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定数量的消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 * 
 *  已废弃，请用 {@link loadMessagesStartFromId:count:searchDirection:completion:} 代替。
 *
 *  @param aMessageId  获取的消息的 ID。
 *  @param aLimit      获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aDirection  消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Gets more messages from DB. 
 * 
 *  The result messages are sorted by receive time, and NOT include the reference message, if reference messag's ID is nil, will fetch message from latest message.
 * 
 *  Deprecated. Please use  {@link loadMessagesStartFromId:count:searchDirection:completion:}  instead.
 *
 *  @param aMessageId  The message ID.
 *  @param aLimit      The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aDirection  The message search direction: EMMessageSearchDirection.
 *
 *  @result The message NSArray. See <EMChatMessage>.
 */
- (NSArray *)loadMoreMessagesFromId:(NSString *)aMessageId
                              limit:(int)aLimit
                          direction:(EMMessageSearchDirection)aDirection __deprecated_msg("Use -loadMessagesStartFromId:count:searchDirection:completion: instead");

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定类型的消息，取到的消息按时间排序，如果参考的时间戳为负数，则从最新消息向前取，如果aLimit是负数，则获取所有符合条件的消息。
 *
 *  已废弃，请用 {@link loadMessagesWithType:timestamp:count:fromUser:searchDirection:completion:} 代替。
 * 
 *  @param aType        消息类型。
 *  @param aTimestamp   传入的时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aLimit       获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender      消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection   消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Gets more messages with specified type from DB. The result messages are sorted by received time, if reference timestamp is negative, will fetch message from latest message, andd will fetch all messages that meet the condition if aLimit is negative.
 *
 *  Deprecated. Please use  {@link loadMessagesWithType:timestamp:count:fromUser:searchDirection:completion:}  instead.
 * 
 *  @param aType        The message type to load.
 *  @param aTimestamp   Reference timestamp.
 *  @param aLimit       The maximum number of messages to load.
 *  @param aSender      The message sender, will ignore it if it's empty.
 *  @param aDirection   The message search direction.
 *
 *  @result The message NSArray. See <EMChatMessage>.
 */
- (NSArray *)loadMoreMessagesWithType:(EMMessageBodyType)aType
                               before:(long long)aTimestamp
                                limit:(int)aLimit
                                 from:(NSString*)aSender
                            direction:(EMMessageSearchDirection)aDirection __deprecated_msg("Use -loadMessagesWithType:timestamp:count:fromUser:searchDirection:completion: instead");

/**
 *  \~chinese
 *  从数据库获取包含指定内容的消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 * 
 *  已废弃，请用 {@link loadMessagesContainKeywords:timestamp:count:fromUser:searchDirection:completion:} 代替。
 *
 *  @param aKeywords    关键词。设为 NIL 表示忽略该参数。
 *  @param aTimestamp   传入的时间戳，单位为毫秒。如果该参数设置的时间戳为负数，则从最新消息向前获取。
 *  @param aLimit       获取的消息条数。如果设为小于等于 0，SDK 会将该参数作 1 处理。
 *  @param aSender      消息发送方。设为 NIL 表示忽略该参数。
 *  @param aDirection   消息搜索方向，详见 EMMessageSearchDirection。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Loads messages with the specified keyword from the local database. 
 *  
 *  This method returns messages in the sequence of the timestamp when they are received.
 * 
 *  Deprecated. Please use  {@link loadMessagesContainKeywords:timestamp:count:fromUser:searchDirection:completion:}  instead.
 * 
 *  @param aKeywords    The keyword for searching the messages. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aTimestamp   The reference timestamp for the messages to be loaded. If you set this parameter as a negative value, the SDK loads messages from the latest.
 *  @param aLimit       The number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *  @param aSender      The user that sends the message. Setting it as NIL means that the SDK ignores this parameter.
 *  @param aDirection   The message search direction: EMMessageSearchDirection.
 *
 *  @result The message NSArray. See <EMChatMessage>.
 */
- (NSArray *)loadMoreMessagesContain:(NSString*)aKeywords
                              before:(long long)aTimestamp
                               limit:(int)aLimit
                                from:(NSString*)aSender
                           direction:(EMMessageSearchDirection)aDirection __deprecated_msg("Use -loadMessagesContainKeywords:timestamp:count:fromUser:searchDirection:completion: instead");

/**
 *  \~chinese
 *  从 SDK 本地数据库获取指定时间段内的消息。
 * 
 *  该方法返回的消息按时间顺序排列。
 * 
 *  为了防止占用太多内存，用户应当制定加载消息的最大数。
 * 
 *  已废弃，请用 {@link loadMessagesFrom:to:count:completion:} 代替。
 *
 *  @param aStartTimestamp  毫秒级开始时间。
 *  @param aEndTimestamp    结束时间。
 *  @param aMaxCount        加载消息最大数。
 *
 *  @result 消息列表。
 *
 *  \~english
 *  Loads messages from DB in duration, result messages are sorted by receive time, user should limit the max count to load to avoid memory issue
 *
 *  Deprecated. Please use  {@link loadMessagesFrom:to:count:completion:}  instead.
 * 
 *  @param aStartTimestamp  Start time's timestamp in miliseconds
 *  @param aEndTimestamp    End time's timestamp in miliseconds
 *  @param aMaxCount        The maximum number of messages to load. If you set this parameter less than 1, it will be handled as count=1.
 *
 *  @result The message NSArray. See <EMChatMessage>.
 */
- (NSArray *)loadMoreMessagesFrom:(long long)aStartTimestamp
                               to:(long long)aEndTimestamp
                         maxCount:(int)aMaxCount __deprecated_msg("Use -loadMessagesFrom:to:count:completion: instead");

/**
 *  \~chinese
 *  收到的对方发送的最后一条消息。
 * 
 *  已废弃，请用 {@link lastReceivedMessage} 代替。
 *
 *  @result 消息实例
 *
 *  \~english
 *  Gets the latest message received from others.
 * 
 *  Deprecated. Please use  {@link lastReceivedMessage}  instead.
 *
 *  @result The message instance.
 */
- (EMChatMessage *)latestMessageFromOthers __deprecated_msg("Use -lastReceivedMessage instead");

@end
