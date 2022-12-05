//
//  EMConversationWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMConversationWrapper.h"
#import "EMHelper.h"
#import "EMChatMessage+Helper.h"

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
    else if([messageCount isEqualToString:method]) {
        ret = [self messageCount:params callback:callback];
    }else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}


- (NSString *)getUnreadMsgCount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    return [EMHelper getReturnJsonObject:@(conversation.unreadMessagesCount)];
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
    return nil;
}

- (NSString *)removeMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSString *msgId = params[@"msgId"];
    [conversation deleteMessageWithId:msgId error:nil];
    return nil;
}

- (NSString *)getLatestMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    EMChatMessage *msg = [conversation latestMessage];
    return [EMHelper getReturnJsonObject:[msg toJson]];
}

- (NSString *)getLatestMessageFromOthers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    EMChatMessage *msg = [conversation lastReceivedMessage];
    return [EMHelper getReturnJsonObject:[msg toJson]];
}

- (NSString *)clearAllMessages:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    [conversation deleteAllMessages:nil];
    return nil;
}

- (NSString *)insertMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *msgDict = params[@"msg"];
    EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
    [conversation insertMessage:msg error:nil];
    return nil;
}

- (NSString *)appendMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *msgDict = params[@"msg"];
    EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
    [conversation appendMessage:msg error:nil];
    return nil;
}

- (NSString *)updateConversationMessage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSDictionary *msgDict = params[@"msg"];
    EMChatMessage *msg = [EMChatMessage fromJson:msgDict];
    [conversation updateMessageChange:msg error:nil];
    return nil;
}

- (NSString *)loadMsgWithId:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    NSString *msgId = params[@"msgId"];
    EMChatMessage *msg = [conversation loadMessageWithId:msgId error:nil];
    
    return [EMHelper getReturnJsonObject:[msg toJson]];
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
    
    return [EMHelper getReturnJsonObject:jsonMsgs];
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
    
    return [EMHelper getReturnJsonObject:jsonMsgs];
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
    
    return [EMHelper getReturnJsonObject:jsonMsgs];
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
    
    return [EMHelper getReturnJsonObject:jsonMsgs];
}

- (NSString *)messageCount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMConversation *conversation = [self conversationWithParam: params];
    return [EMHelper getReturnJsonObject:@(conversation.messagesCount)];
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