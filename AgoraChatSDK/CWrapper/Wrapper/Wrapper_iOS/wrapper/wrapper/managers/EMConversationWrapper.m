//
//  EMConversationWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMConversationWrapper.h"
#import "EMHelper.h"
#import "EMChatMessage+Helper.h"
#import "EMUtil.h"
#import "Mode.h"

@implementation EMConversationWrapper
- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {
    NSString *ret = nil;
    if ([getConversationUnreadMsgCount isEqualToString:method]) {
        ret = [self getUnreadMsgCount:params callback:callback];
    }
    else if ([markAllMessagesAsRead isEqualToString:method]) {
        ret = [self markAllMessagesAsRead:params callback:callback];
    }
    else if ([markMessageAsRead isEqualToString:method]) {
        ret = [self markMessageAsRead:params callback:callback];
    }
    else if ([syncConversationExt isEqualToString:method]){
        ret = [self syncConversationExt:params callback:callback];
    }
    else if ([removeMessage isEqualToString:method])
    {
        ret = [self removeMessage:params callback:callback];
    }
    else if ([getLatestMessage isEqualToString:method]) {
        ret = [self getLatestMessage:params callback:callback];
    }
    else if ([getLatestMessageFromOthers isEqualToString:method]) {
        ret = [self getLatestMessageFromOthers:params callback:callback];
    }
    else if ([clearAllMessages isEqualToString:method]) {
        ret = [self clearAllMessages:params callback:callback];
    }
    else if ([insertMessage isEqualToString:method]) {
        ret = [self insertMessage:params callback:callback];
    }
    else if ([appendMessage isEqualToString:method]) {
        ret = [self appendMessage:params callback:callback];
    }
    else if ([updateConversationMessage isEqualToString:method]) {
        ret = [self updateConversationMessage:params callback:callback];
    }
    else if ([loadMsgWithId isEqualToString:method]) {
        ret = [self loadMsgWithId:params callback:callback];
    }
    else if ([loadMsgWithStartId isEqualToString:method]) {
        ret = [self loadMsgWithStartId:params callback:callback];
    }
    else if ([loadMsgWithKeywords isEqualToString:method]) {
        ret = [self loadMsgWithKeywords:params callback:callback];
    }
    else if ([loadMsgWithMsgType isEqualToString:method]) {
        ret = [self loadMsgWithMsgType:params callback:callback];
    }
    else if ([loadMsgWithTime isEqualToString:method]) {
        ret = [self loadMsgWithTime:params callback:callback];
    }
    else if ([loadMsgWithScope isEqualToString:method]) {
        ret = [self loadMsgWithScope:params callback:callback];
    }
    else if([messageCount isEqualToString:method]) {
        ret = [self messageCount:params callback:callback];
    }
    else if([removeMessages isEqualToString:method]) {
        ret = [self removeMessages:params callback:callback];
    }
    else if([pinnedMessages isEqualToString:method]) {
        ret = [self pinnedMessages:params callback:callback];
    }
    else if([marks isEqualToString:method]) {
        ret = [self marks:params callback:callback];
    }
    else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}


- (NSString *)getUnreadMsgCount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    return [[EMHelper getReturnJsonObject:@(conversation.unreadMessagesCount)] toJsonString];
}

- (NSString *)markAllMessagesAsRead:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    [conversation markAllMessagesAsRead:nil];
    return nil;
}

- (NSString *)markMessageAsRead:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSString *msgId = params[@"msgId"];
    [conversation markMessageAsReadWithId:msgId error:nil];
    return nil;
}

- (NSString *)syncConversationExt:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *ext = params[@"ext"];
    conversation.ext = ext;
    return [[EMHelper getReturnJsonObject:@(YES)] toJsonString];
}

- (NSString *)removeMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSString *msgId = params[@"msgId"];
    EMError *error;
    [conversation deleteMessageWithId:msgId error:&error];
    return [[EMHelper getReturnJsonObject:error == nil ? @(YES) : @(NO)] toJsonString];
}

- (NSString *)getLatestMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    EMChatMessage *msg = [conversation latestMessage];
    return [[EMHelper getReturnJsonObject:[msg toJson]] toJsonString];
}

- (NSString *)getLatestMessageFromOthers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    EMChatMessage *msg = [conversation lastReceivedMessage];
    return [[EMHelper getReturnJsonObject:[msg toJson]] toJsonString];
}

- (NSString *)clearAllMessages:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    EMError *error;
    [conversation deleteAllMessages:&error];
    return [[EMHelper getReturnJsonObject:error == nil ? @(YES) : @(NO)] toJsonString];
}

- (NSString *)insertMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *msgDict = params[@"msg"];
    EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
    EMError *error;
    [conversation insertMessage:msg error:&error];
    return [[EMHelper getReturnJsonObject:error == nil ? @(YES) : @(NO)] toJsonString];
}

- (NSString *)appendMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *msgDict = params[@"msg"];
    EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
    EMError *error;
    [conversation appendMessage:msg error:&error];
    return [[EMHelper getReturnJsonObject:error == nil ? @(YES) : @(NO)] toJsonString];
}

- (NSString *)updateConversationMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *msgDict = params[@"msg"];
    EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
    EMError *error;
    [conversation updateMessageChange:msg error:&error];
    return [[EMHelper getReturnJsonObject:error == nil ? @(YES) : @(NO)] toJsonString];
}

- (NSString *)loadMsgWithId:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSString *msgId = params[@"msgId"];
    EMChatMessage *msg = [conversation loadMessageWithId:msgId error:nil];
    
    return [[EMHelper getReturnJsonObject:[msg toJson]] toJsonString];
}

- (NSString *)loadMsgWithStartId:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    
    NSString *startId = params[@"startMessageId"];
    int count = [params[@"count"] intValue];
    EMMessageSearchDirection direction = [params[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    EMConversation *conversation = [self conversationWithParam: params];
    NSMutableArray *jsonMsgs = [NSMutableArray array];
    NSArray *aMessages = [conversation loadMessagesStartFromId:startId count:count searchDirection:direction];
    for (EMChatMessage *msg in aMessages) {
        [jsonMsgs addObject:[msg toJson]];
    }
    
    [self wrapperCallback:callback error:nil object:jsonMsgs];
    return nil;
}

- (NSString *)loadMsgWithKeywords:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSString * keywords = params[@"keywords"];
    long long timestamp = [params[@"timestamp"] longLongValue];
    int count = [params[@"count"] intValue];
    NSString *sender = params[@"sender"];
    EMMessageSearchDirection direction = [params[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    NSArray *aMessages = [conversation loadMessagesWithKeyword:keywords timestamp:timestamp count:count fromUser:sender searchDirection:direction];
    NSMutableArray *jsonMsgs = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [jsonMsgs addObject:[msg toJson]];
    }
    
    [self wrapperCallback:callback error:nil object:jsonMsgs];
    return nil;
}


- (NSString *)loadMsgWithMsgType:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    
    EMMessageBodyType type = EMMessageBodyTypeText;
    int iType = [params[@"bodyType"] intValue];
    switch (iType) {
        case 0:type = EMMessageBodyTypeText; break;
        case 1:type = EMMessageBodyTypeImage; break;
        case 2:type = EMMessageBodyTypeVideo; break;
        case 3:type = EMMessageBodyTypeLocation; break;
        case 4:type = EMMessageBodyTypeVoice; break;
        case 5:type = EMMessageBodyTypeFile; break;
        case 6:type = EMMessageBodyTypeCmd; break;
        case 7:type = EMMessageBodyTypeCustom; break;
        case 8:type = EMMessageBodyTypeCombine; break;
        default:break;
    }
    long long timestamp = [params[@"timestamp"] longLongValue];
    int count = [params[@"count"] intValue];
    NSString *sender = params[@"sender"];
    EMMessageSearchDirection direction = [params[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    
    EMConversation *conversation = [self conversationWithParam: params];
    NSArray *aMessages = [conversation loadMessagesWithType:type timestamp:timestamp count:count fromUser:sender searchDirection:direction];
    NSMutableArray *jsonMsgs = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [jsonMsgs addObject:[msg toJson]];
    }
    [self wrapperCallback:callback error:nil object:jsonMsgs];
    return nil;
}

- (NSString *)loadMsgWithTime:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    long long startTime = [params[@"startTime"] longLongValue];
    long long entTime = [params[@"endTime"] longLongValue];
    int count = [params[@"count"] intValue];
    EMConversation *conversation = [self conversationWithParam: params];
    NSArray *aMessages = [conversation loadMessagesFrom:startTime to:entTime count:count];
    NSMutableArray *jsonMsgs = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [jsonMsgs addObject:[msg toJson]];
    }
    
    [self wrapperCallback:callback error:nil object:jsonMsgs];
    return nil;
}

- (NSString *)loadMsgWithScope:(NSDictionary *)params callback:(EMWrapperCallback *)callback {

    __weak EMConversationWrapper * weakSelf = self;
    EMConversation *conversation = [self conversationWithParam: params];
    NSString * keywords = params[@"keywords"];
    long long timestamp = [params[@"timestamp"] longLongValue];
    int count = [params[@"count"] intValue];
    NSString *sender = params[@"from"];
    EMMessageSearchDirection direction = [params[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    EMMessageSearchScope scope = [Mode searchScopeFromInt:[params[@"scope"] intValue]];

    [conversation loadMessagesWithKeyword:keywords
                                timestamp:timestamp
                                    count:count
                                 fromUser:sender
                          searchDirection:direction
                                   scope:scope
                               completion:^(NSArray<EMChatMessage *> * _Nullable aMessages, EMError * _Nullable aError)
    {
        NSMutableArray *jsonMsgs = [NSMutableArray array];
        for (EMChatMessage *msg in aMessages) {
            [jsonMsgs addObject:[msg toJson]];
        }
        [weakSelf wrapperCallback:callback error:aError object:jsonMsgs];
    }];

    return nil;
}

- (NSString *)messageCount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    return [[EMHelper getReturnJsonObject:@(conversation.messagesCount)] toJsonString];
}

- (NSString *)removeMessages:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSInteger startTs = [params[@"startTime"] intValue];
    NSInteger endTs = [params[@"endTime"] intValue];
    EMError *error = [conversation removeMessagesStart:startTs to:endTs];
    return [[EMHelper getReturnJsonObject:error == nil ? @(YES) : @(NO)] toJsonString];
}

- (NSString *)pinnedMessages:(NSDictionary *)params
                    callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSArray *aMessages = [conversation pinnedMessages];
    NSMutableArray *jsonMsgs = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [jsonMsgs addObject:[msg toJson]];
    }
    return [[EMHelper getReturnJsonObject:jsonMsgs] toJsonString];
}

- (NSString *)marks:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSArray *mks = [conversation marks];
    return [[EMHelper getReturnJsonObject:mks] toJsonString];
}

- (EMConversation *)conversationWithParam:(NSDictionary *)param
{
    NSString *conversationId = param[@"convId"];
    EMConversationType type = [param[@"convType"] intValue];
    EMConversation *conversation = [EMClient.sharedClient.chatManager getConversation:conversationId
                                                                                 type:type
                                                                     createIfNotExist:YES];
    return conversation;
}


- (void)registerEaseListener {
    
}

@end
