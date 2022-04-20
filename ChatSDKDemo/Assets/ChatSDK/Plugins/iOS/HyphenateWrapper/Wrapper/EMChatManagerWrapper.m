//
//  EMChatManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMChatManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMConversation+Unity.h"
#import "EMChatMessage+Unity.h"
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

- (id)deleteConversation:(NSDictionary *)param {
    NSString *conversationId = param[@"conversationId"];
    if (!conversationId) {
        return nil;
    }
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
    return @{@"ret":@(ret)};
}


- (void)downloadAttachment:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];
    EMError *error;
    if (!msgId) {
        error = [[EMError alloc] initWithDescription:@"messageId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    EMChatMessage *msg =[EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        error = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager downloadMessageAttachment:msg progress:^(int progress) {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMChatMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)downloadThumbnail:(NSDictionary *)param callbackId:(NSString *)callbackId {

    NSString *msgId = param[@"msgId"];
    EMError *error;
    if (!msgId) {
        error = [[EMError alloc] initWithDescription:@"messageId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    EMChatMessage *msg =[EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        error = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager downloadMessageThumbnail:msg progress:^(int progress) {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMChatMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)fetchHistoryMessages:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *convId = param[@"convId"];
    if (!convId) {
        EMError *error = [[EMError alloc] initWithDescription:@"conversioanId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    __block NSString *callId = callbackId;
    NSString *conversationId = param[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"convType"] intValue]];
    NSString *startMsgId = param[@"startMsgId"];
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
            [self onSuccess:@"EMCursorResult<EMMessage>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:[aResult toJson]]];
        }
    }];
}


- (id)getConversation:(NSDictionary *)param{
    
    NSString *convId = param[@"convId"];
    if (!convId) {
        return nil;
    }
    
    EMConversationType type = [EMConversation typeFromInt:[param[@"convType"] intValue]];
    BOOL createIfNeed = [param[@"createIfNeed"] boolValue];
    EMConversation *con = [EMClient.sharedClient.chatManager getConversation:convId type:type createIfNotExist:createIfNeed];
    if (!con) {
        return nil;
    }
    return [con toJson];
}

- (void)getConversationsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager getConversationsFromServer:^(NSArray *aCoversations, EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            NSMutableArray *jsonList = [NSMutableArray array];
            for (EMConversation *conv in aCoversations) {
                [jsonList addObject:[Transfrom NSStringFromJsonObject:[conv toJson]]];
            }
            [self onSuccess:@"List<EMConversation>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:jsonList]];
        }
    }];
}

- (id)getUnreadMessageCount:(NSDictionary *)param{
    NSArray *lists = [EMClient.sharedClient.chatManager getAllConversations];
    if (!lists && lists.count == 0) {
        return nil;
    }
    int count = 0;
    for (EMConversation *con in lists) {
        count += con.unreadMessagesCount;
    }
    return @{@"ret": @(count)};
}

- (id)importMessages:(NSDictionary *)param {
    
    __block BOOL ret = NO;
    
    NSArray *jsonObjectList = param[@"list"];
    if (!jsonObjectList || jsonObjectList.count == 0) {
        return nil;
    }
    NSMutableArray *msgs = [NSMutableArray array];
    
    for (NSDictionary *jsonObject in jsonObjectList) {
        EMChatMessage *msg = [EMChatMessage fromJson:jsonObject];
        [msgs addObject:msg];
    }
    
    dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
    [EMClient.sharedClient.chatManager importMessages:msgs completion:^(EMError *aError) {
        ret = aError ? YES : NO;
        dispatch_semaphore_signal(semaphore);
    }];
    dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
    return @{@"ret":@(ret)};
}

- (id)loadAllConversations:(NSDictionary *)param{
    NSArray *lists = [EMClient.sharedClient.chatManager getAllConversations];
    if (!lists && lists.count == 0) {
        return nil;
    }
    NSMutableArray *jsonList = [NSMutableArray array];
    for (EMConversation *conv in lists) {
        [jsonList addObject:[Transfrom NSStringFromJsonObject:[conv toJson]]];
    }
    
    return jsonList;
}

- (id)getMessage:(NSDictionary *)param{

    NSString *msgId = param[@"msgId"];
    if (!msgId) {
        return nil;
    }
    
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        return nil;
    }
    return [msg toJson];
}

- (id)markAllChatMsgAsRead:(NSDictionary *)param{
    NSArray *lists = [EMClient.sharedClient.chatManager getAllConversations];
    if (!lists && lists.count == 0) {
        return nil;
    }
    for (EMConversation *con in lists) {
        [con markAllMessagesAsRead:nil];
    }
    return @{@"ret":@(YES)};
}

- (void)recallMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];
    if (!msgId) {
        EMError *error = [[EMError alloc] initWithDescription:@"messageId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }

    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager recallMessageWithMessageId:msgId
                                                       completion:^(EMError *aError)
     {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (id)resendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    
    NSString *msgId = param[@"msgId"];
    if (!msgId) {
        EMError *error = [[EMError alloc] initWithDescription:@"messageId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return nil;
    }
    
    __block NSString *callId = callbackId;
    
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"message not found" code:EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return nil;
    }
    [EMClient.sharedClient.chatManager resendMessage:msg
                                            progress:^(int progress)
     {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMChatMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
    return [msg toJson];
}

- (void)searchChatMsgFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId {

    __block NSString *callId = callbackId;
    NSString *keywards = param[@"keywords"];
    long long timestamp = [param[@"timestamp"] longLongValue];
    int count = [param[@"count"] intValue];
    NSString *from = param[@"from"];
    EMMessageSearchDirection direction = [param[@"direction"] isEqualToString:@"up"] ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    
    [EMClient.sharedClient.chatManager loadMessagesWithKeyword:keywards
                                                     timestamp:timestamp
                                                         count:count
                                                      fromUser:from
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

- (void)ackConversationRead:(NSDictionary *)param callbackId:(NSString *)callbackId {
    
    NSString *convId = param[@"convId"];
    if (!convId) {
        EMError *error = [[EMError alloc] initWithDescription:@"conversioanId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager ackConversationRead:convId completion:^(EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (id)sendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (param == nil || param.count == 0) {
        EMError *error = [[EMError alloc] initWithDescription:@"Message contains invalid content"
                                                         code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:error];
        return nil;
    }
    __block NSString *callId = callbackId;
    EMChatMessage *msg = [EMChatMessage fromJson:param] ;
    [EMClient.sharedClient.chatManager sendMessage:msg
                                          progress:^(int progress)
    {
        [self onProgress:progress callbackId:callId];
    } completion:^(EMChatMessage *message, EMError *error) {
        if (error) {
            [self onError:callId error:error];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
    return [msg toJson];
}

- (void)ackMessageRead:(NSDictionary *)param callbackId:(NSString *)callbackId {

    NSString *msgId = param[@"msgId"];
    if (!msgId) {
        EMError *error = [[EMError alloc] initWithDescription:@"messageId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    __block NSString *callId = callbackId;
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return;
    }
    [EMClient.sharedClient.chatManager sendMessageReadAck:msgId toUser:msg.from completion:^(EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (id)updateChatMessage:(NSDictionary *)param {
    
    __block BOOL ret = NO;
    do {
        NSString *msgId = param[@"msgId"];
        if (!msgId) {
            break;
        }
        EMChatMessage *msg = [EMChatMessage fromJson:param];
        dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);
        [EMClient.sharedClient.chatManager updateMessage:msg completion:^(EMChatMessage *aMessage, EMError *aError) {
            ret = aError ? YES : NO;
            dispatch_semaphore_signal(semaphore);
        }];
        dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);
        
    } while (0);
    
    return @{@"ret":@(ret)};
}

@end
