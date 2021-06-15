//
//  EMChatManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMChatManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMConversation+Unity.h"
#import "EMMessage+Unity.h"
#import "EMCursorResult+Unity.h"
#import "Transfrom.h"
#import "EMChatListener.h"

@interface EMChatManagerWrapper () <EMChatManagerDelegate>
{
    EMChatListener *_listener;
}
@end

@implementation EMChatManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMChatListener alloc] init];
        [EMClient.sharedClient.chatManager addDelegate:_listener delegateQueue:nil];
    }
    return self;
}

- (void)downloadAttachment:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSString *msgId = param[@"messageId"];
    EMMessage *msg =[EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager downloadMessageAttachment:msg progress:^(int progress) {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)downloadThumbnail:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSString *msgId = param[@"messageId"];
    EMMessage *msg =[EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager downloadMessageThumbnail:msg progress:^(int progress) {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)fetchHistoryMessages:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    NSString *conversationId = param[@"conversationId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"type"] intValue]];
    NSString *startMsgId = param[@"startMessageId"];
    int count = [param[@"count"] intValue];
    [EMClient.sharedClient.chatManager asyncFetchHistoryMessagesFromServer:conversationId
                                                          conversationType:type
                                                            startMessageId:startMsgId
                                                                  pageSize:count
                                                                completion:^(EMCursorResult *aResult, EMError *aError)
     {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:@"EMCursorResult<EMMessage>" callbackId:callId userInfo:[Transfrom DictToNSString:[aResult toJson]]];
        }
    }];
}

- (void)getConversationsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager getConversationsFromServer:^(NSArray *aCoversations, EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            NSMutableArray *ary = [NSMutableArray array];
            for (EMConversation *conv in aCoversations) {
                [ary addObject:[conv toJson]];
            }
            [self onSuccess:@"List<Conversation>" callbackId:callId userInfo:[Transfrom ArrayToNSString:ary]];
        }
    }];
}

- (void)recallMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSString *messageId = param[@"messageId"];
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager recallMessageWithMessageId:messageId
                                                       completion:^(EMError *aError)
     {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)ackConversationRead:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSString *conversationId = param[@"conversationId"];
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager ackConversationRead:conversationId completion:^(EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)ackMessageRead:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSString *messageId = param[@"messageId"];
    __block NSString *callId = callbackId;
    EMMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:messageId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return;
    }
    [EMClient.sharedClient.chatManager sendMessageReadAck:messageId toUser:msg.from completion:^(EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)updateChatMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    EMMessage *msg = [EMMessage fromJson:param];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager updateMessage:msg completion:^(EMMessage *aMessage, EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (NSDictionary *)deleteConversation:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *conversationId = param[@"conversationId"];
    BOOL deleteMessages = [param[@"deleteMessages"] boolValue];
    dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
    __block BOOL ret = NO;
    [EMClient.sharedClient.chatManager deleteConversation:conversationId
                                         isDeleteMessages:deleteMessages
                                               completion:^(NSString *aConversationId, EMError *aError)
     {
        ret = aError ? YES:NO;
        dispatch_semaphore_signal(semaphore);
    }];
    dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
    return @{@"bool":@(ret)};
}

- (NSDictionary *)getConversation:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *conversationId = param[@"conversationId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"type"] intValue]];
    BOOL createIfNeed = [param[@"createIfNeed"] boolValue];
    EMConversation *con = [EMClient.sharedClient.chatManager getConversation:conversationId type:type createIfNotExist:createIfNeed];
    if (!con) {
        return nil;
    }
    return @{@"EMConversation":[con toJson]};
}

- (NSDictionary *)getUnreadMessageCount:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *lists = [EMClient.sharedClient.chatManager getAllConversations];
    if (!lists && lists.count == 0) {
        return nil;
    }
    int count = 0;
    for (EMConversation *con in lists) {
        count += con.unreadMessagesCount;
    }
    return @{@"int": @(count)};
}

- (NSDictionary *)importMessages:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *listString = param[@"list"];
    NSMutableArray *msgs = [NSMutableArray array];
    __block BOOL ret = NO;
    for (NSString *str in listString) {
        EMMessage *msg = [EMMessage fromJson:[Transfrom DictFromNSString:str]];
        [msgs addObject:msg];
    }
    if (msgs.count == 0) {
        return @{@"bool":@(ret)};
    }
    dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
    [EMClient.sharedClient.chatManager importMessages:msgs completion:^(EMError *aError) {
        ret = aError ? YES:NO;
        dispatch_semaphore_signal(semaphore);
    }];
    dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
    return @{@"bool":@(ret)};
}

- (NSDictionary *)loadAllConversations:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *lists = [EMClient.sharedClient.chatManager getAllConversations];
    if (!lists && lists.count == 0) {
        return nil;
    }
    NSMutableArray *jsonList = [NSMutableArray array];
    for (EMConversation *conv in lists) {
        [jsonList addObject:[conv toJson]];
    }
    return @{@"List<EMConversation>":jsonList};
}

- (NSDictionary *)getMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"messageId"];
    EMMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        return nil;
    }
    return @{@"EMMessage": [msg toJson]};
}

- (NSDictionary *)markAllChatMsgAsRead:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *lists = [EMClient.sharedClient.chatManager getAllConversations];
    if (!lists && lists.count == 0) {
        return nil;
    }
    for (EMConversation *con in lists) {
        [con markAllMessagesAsRead:nil];
    }
    return @{@"bool":@(YES)};
}

- (NSDictionary *)resendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"messageId"];
    __block NSString *callId = callbackId;
    EMMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code:EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return nil;
    }
    [EMClient.sharedClient.chatManager resendMessage:msg
                                            progress:^(int progress)
     {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
    return @{@"EMMessage": [msg toJson]};
}

- (NSDictionary *)searchChatMsgFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return nil;
    }
    NSString *keywards = param[@"keywards"];
    long long timestamp = [param[@"timestamp"] longLongValue];
    int count = [param[@"maxCount"] intValue];
    NSString *from = param[@"from"];
    EMMessageSearchDirection direction = [param[@"direction"] isEqualToString:@"up"] ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    __block NSArray *msgs = nil;
    __block EMError *err = nil;
    
    dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
    [EMClient.sharedClient.chatManager loadMessagesWithKeyword:keywards
                                                     timestamp:timestamp
                                                         count:count
                                                      fromUser:from
                                               searchDirection:direction
                                                    completion:^(NSArray *aMessages, EMError *aError)
     {
        msgs = aMessages;
        err = aError;
        dispatch_semaphore_signal(semaphore);
    }];
    dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
    NSString *listStr = @"";
    if (!err) {
        NSMutableArray *ary = [NSMutableArray array];
        for (EMMessage *msg in msgs) {
            [ary addObject:[msg toJson]];
        }
        listStr = [Transfrom ArrayToNSString:ary];
    }
    return @{@"List<EMMessage>": listStr};
}

- (NSDictionary *)sendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return nil;
    }
    __block NSString *callId = callbackId;
    EMMessage *msg = [EMMessage fromJson:param];
    [EMClient.sharedClient.chatManager sendMessage:msg progress:^(int progress){
        [self onProgress:progress callbackId:callId];
    } completion:^(EMMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
    return @{@"EMMessage": [msg toJson]};
}
@end
