//
//  EMConversationWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/15.
//

#import "EMConversationWrapper.h"
#import "EMConversation+Unity.h"
#import "Transfrom.h"
#import "EMMessage+Unity.h"

@implementation EMConversationWrapper
- (void)getConversationWithParam:(NSDictionary *)param
                      completion:(void(^)(EMConversation *conversation))aCompletion
{
    __weak NSString *conversationId = param[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"convType"] intValue]];
    EMConversation *conversation = [EMClient.sharedClient.chatManager getConversation:conversationId
                                                                                 type:type
                                                                     createIfNotExist:YES];
    if (aCompletion) {
        aCompletion(conversation);
    }
}

#pragma mark - Actions
- (NSDictionary *)getUnreadMsgCount:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation) {
        ret = @{@"count": @(conversation.unreadMessagesCount)};
    }];
    
    return ret;
}

- (NSDictionary *)getLatestMessage:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        EMMessage *msg =conversation.latestMessage;
        ret = msg.toJson;
    }];
    return ret;
}

- (NSDictionary *)getLatestMessageFromOthers:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        EMMessage *msg =conversation.lastReceivedMessage;
        ret = msg.toJson;
    }];
    return ret;
}

- (void)markMessageAsRead:(NSDictionary *)param
{
    __block NSString *msgId = param[@"msgId"];
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {  
        EMError *error = nil;
        [conversation markMessageAsReadWithId:msgId error:&error];
    }];
}

- (void)syncConversationExt:(NSDictionary *)param
{
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        NSDictionary *ext = param[@"ext"];
        conversation.ext = ext;
    }];
}

- (NSDictionary *)conversationExt:(NSDictionary *)param {
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        ret = conversation.ext;
    }];
    return ret;
}

- (void)markAllMessagesAsRead:(NSDictionary *)param
{
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation) {
        [conversation markAllMessagesAsRead:nil];
    }];
}

- (NSDictionary *)insertMessage:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        NSDictionary *msgDict = [Transfrom NSStringToJsonObject:param[@"msg"]];
        EMMessage *msg = [EMMessage fromJson:msgDict];
        
        EMError *error = nil;
        [conversation insertMessage:msg error:&error];
        ret = @{@"ret": @(error == nil)};
    }];
    return ret;
}

- (NSDictionary *)appendMessage:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        NSDictionary *msgDict = [Transfrom NSStringToJsonObject:param[@"msg"]];
        EMMessage *msg = [EMMessage fromJson:msgDict];
        
        EMError *error = nil;
        [conversation appendMessage:msg error:&error];
        ret = @{@"ret": @(error == nil)};
    }];
    return ret;
}

- (NSDictionary *)updateConversationMessage:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        NSDictionary *msgDict = [Transfrom NSStringToJsonObject:param[@"msg"]];
        EMMessage *msg = [EMMessage fromJson:msgDict];
        
        EMError *error = nil;
        [conversation updateMessageChange:msg error:&error];
        ret = @{@"ret": @(error == nil)};
    }];
    return ret;
}

- (NSDictionary *)removeMessage:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    __block NSString *msgId = param[@"msgId"];
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        EMError *error = nil;
        [conversation deleteMessageWithId:msgId error:&error];
        ret = @{@"ret": @(error == nil)};
    }];
    return ret;
}

- (NSDictionary *)clearAllMessages:(NSDictionary *)param
{
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        EMError *error = nil;
        [conversation deleteAllMessages:&error];
        ret = @{@"ret": @(error == nil)};
    }];
    return ret;
}

#pragma mark - load messages
- (NSDictionary *)loadMsgWithId:(NSDictionary *)param
{
    __block NSString *msgId = param[@"msgId"];
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        EMError *error = nil;
        EMMessage *msg = [conversation loadMessageWithId:msgId error:&error];
        ret = msg.toJson;
    }];
    return ret;
}

- (NSArray *)loadMsgWithMsgType:(NSDictionary *)param
{
    EMMessageBodyType type = [EMMessageBody typeFromString:param[@"type"]];
    long long timeStamp = [param[@"timeStamp"] longLongValue];
    int count = [param[@"count"] intValue];
    NSString *sender = param[@"sender"];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    
    __block NSMutableArray *jsonMsgs = [NSMutableArray array];
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        if (conversation) {
            dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                [conversation loadMessagesWithType:type
                                         timestamp:timeStamp
                                             count:count
                                          fromUser:sender
                                   searchDirection:direction
                                        completion:^(NSArray *aMessages, EMError *aError)
                 {
                    for (EMMessage *msg in aMessages) {
                        [jsonMsgs addObject:[msg toJson]];
                    }
                    dispatch_semaphore_signal(semaphore);
                }];
            });
            dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
        }
    }];

    return jsonMsgs;
}

- (NSArray *)loadMsgWithStartId:(NSDictionary *)param
{
    __block NSString *startId = param[@"startId"];
    int count = [param[@"count"] intValue];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    
    __block NSMutableArray *jsonMsgs = [NSMutableArray array];
    [self getConversationWithParam:param completion:^(EMConversation *conversation)
    {
        if (conversation) {
            dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                [conversation loadMessagesStartFromId:startId
                                                count:count
                                      searchDirection:direction
                                           completion:^(NSArray *aMessages, EMError *aError)
                 {
                    
                    for (EMMessage *msg in aMessages) {
                        [jsonMsgs addObject:[msg toJson]];
                    }
                    dispatch_semaphore_signal(semaphore);
                }];
            });
            dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
        }
    }];
    return jsonMsgs;
}

- (NSArray *)loadMsgWithKeywords:(NSDictionary *)param
{
    __block  NSString * keywords = param[@"keywords"];
    long long timestamp = [param[@"timestamp"] longLongValue];
    int count = [param[@"count"] intValue];
    __block  NSString *sender = param[@"sender"];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    
    __block NSMutableArray *jsonMsgs = [NSMutableArray array];
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        if (conversation) {
            dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                [conversation loadMessagesWithKeyword:keywords
                                            timestamp:timestamp
                                                count:count
                                             fromUser:sender
                                      searchDirection:direction
                                           completion:^(NSArray *aMessages, EMError *aError)
                 {
                    for (EMMessage *msg in aMessages) {
                        [jsonMsgs addObject:[msg toJson]];
                    }
                    dispatch_semaphore_signal(semaphore);
                }];
            });
            dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
        }
    }];
    return jsonMsgs;
}

- (NSArray *)loadMsgWithTime:(NSDictionary *)param
{
    long long startTime = [param[@"startTime"] longLongValue];
    long long entTime = [param[@"endTime"] longLongValue];
    int count = [param[@"count"] intValue];
    
    __block NSMutableArray *jsonMsgs = [NSMutableArray array];
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        if (conversation) {
            dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                [conversation loadMessagesFrom:startTime
                                            to:entTime
                                         count:count
                                    completion:^(NSArray *aMessages, EMError *aError)
                 {
                    for (EMMessage *msg in aMessages) {
                        [jsonMsgs addObject:[msg toJson]];
                    }
                    dispatch_semaphore_signal(semaphore);
                }];
            });
            dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
        }
    }];
    return jsonMsgs;
}

- (EMMessageSearchDirection)searchDirectionFromString:(NSString *)aDirection {
    return [aDirection isEqualToString:@"up"] ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
}
@end
