//
//  EMConversationWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/15.
//

#import "EMConversationWrapper.h"
#import "EMConversation+Unity.h"
#import "Transfrom.h"
#import "EMChatMessage+Unity.h"

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
        EMChatMessage *msg = conversation.latestMessage;
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
        EMChatMessage *msg =conversation.lastReceivedMessage;
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
        NSDictionary *ext = [Transfrom NSStringToJsonObject: param[@"ext"]];
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
        EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
        
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
        EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
        
        EMError *error = nil;
        [conversation appendMessage:msg error:&error];
        ret = @{@"ret": @(error == nil)};
    }];
    return ret;
}

- (NSDictionary *)messageCount:(NSDictionary *)param {
    __block NSDictionary *ret = nil;
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation) {
        ret = @{@"count": @(conversation.messagesCount)};
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
        EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
        
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
        EMChatMessage *msg = [conversation loadMessageWithId:msgId error:&error];
        ret = msg.toJson;
    }];
    return ret;
}

- (void)loadMsgWithMsgType:(NSDictionary *)param callbackId:(NSString *)callbackId
{
    __block NSString *callId = callbackId;
    EMMessageBodyType type = [EMMessageBody typeFromString:param[@"type"]];
    long long timeStamp = [param[@"timeStamp"] longLongValue];
    int count = [param[@"count"] intValue];
    NSString *sender = param[@"sender"];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        if (conversation) {
            [conversation loadMessagesWithType:type
                                     timestamp:timeStamp
                                         count:count
                                      fromUser:sender
                               searchDirection:direction
                                    completion:^(NSArray *aMessages, EMError *aError)
             {
                if (aError) {
                    [self onError:callId error:aError];
                }else {
                    NSMutableArray *ary = [NSMutableArray array];
                    for (EMChatMessage *msg in aMessages) {
                        [ary addObject:[Transfrom NSStringFromJsonObject:[msg toJson]]];
                    }
                    [self onSuccess:@"List<EMMessage>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:ary]];
                }
            }];
        }
    }];

}

- (void)loadMsgWithStartId:(NSDictionary *)param callbackId:(NSString *)callbackId
{
    __block NSString *startId = param[@"startId"];
    __block NSString *callId = callbackId;
    int count = [param[@"count"] intValue];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];

    [self getConversationWithParam:param completion:^(EMConversation *conversation)
    {
        if (conversation) {
            [conversation loadMessagesStartFromId:startId
                                            count:count
                                  searchDirection:direction
                                       completion:^(NSArray *aMessages, EMError *aError)
             {
                if (aError) {
                    [self onError:callId error:aError];
                }else {
                    NSMutableArray *ary = [NSMutableArray array];
                    for (EMChatMessage *msg in aMessages) {
                        [ary addObject:[Transfrom NSStringFromJsonObject:[msg toJson]]];
                    }
                    [self onSuccess:@"List<EMMessage>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:ary]];
                }
            }];
        }
    }];
}

- (void)loadMsgWithKeywords:(NSDictionary *)param callbackId:(NSString *)callbackId
{
    __block  NSString * callId = callbackId;
    __block  NSString * keywords = param[@"keywords"];
    long long timestamp = [param[@"timestamp"] longLongValue];
    int count = [param[@"count"] intValue];
    __block  NSString *sender = param[@"sender"];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        if (conversation) {
            [conversation loadMessagesWithKeyword:keywords
                                        timestamp:timestamp
                                            count:count
                                         fromUser:sender
                                  searchDirection:direction
                                       completion:^(NSArray *aMessages, EMError *aError)
             {
                if (aError) {
                    [self onError:callId error:aError];
                }else {
                    NSMutableArray *ary = [NSMutableArray array];
                    for (EMChatMessage *msg in aMessages) {
                        [ary addObject:[Transfrom NSStringFromJsonObject:[msg toJson]]];
                    }
                    [self onSuccess:@"List<EMMessage>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:ary]];
                }
            }];
        }
    }];
}

- (void)loadMsgWithTime:(NSDictionary *)param callbackId:(NSString *)callbackId
{
    __block NSString * callId = callbackId;
    long long startTime = [param[@"startTime"] longLongValue];
    long long entTime = [param[@"endTime"] longLongValue];
    int count = [param[@"count"] intValue];
    
    [self getConversationWithParam:param
                        completion:^(EMConversation *conversation)
     {
        if (conversation) {
            [conversation loadMessagesFrom:startTime
                                        to:entTime
                                     count:count
                                completion:^(NSArray *aMessages, EMError *aError)
             {
                if (aError) {
                    [self onError:callId error:aError];
                }else {
                    NSMutableArray *ary = [NSMutableArray array];
                    for (EMChatMessage *msg in aMessages) {
                        [ary addObject:[Transfrom NSStringFromJsonObject:[msg toJson]]];
                    }
                    [self onSuccess:@"List<EMMessage>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:ary]];
                }
            }];
        }
    }];
}



- (EMMessageSearchDirection)searchDirectionFromString:(NSString *)aDirection {
    return [aDirection isEqualToString:@"up"] ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
}
@end
