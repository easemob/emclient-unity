//
//  EMChatManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMChatManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMConversation+Helper.h"
#import "EMChatMessage+Helper.h"
#import "EMCursorResult+Helper.h"
#import "Transfrom.h"
#import "EMChatListener.h"
#import "NSArray+Helper.h"
#import "EMMessageReaction+Helper.h"
#import "EMChatMessage+Helper.h"

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
    NSString *conversationId = param[@"convId"];
    if (!conversationId) {
        return nil;
    }
    BOOL deleteMessages = [param[@"deleteMessages"] boolValue];
    BOOL ret = [EMClient.sharedClient.chatManager deleteConversation:conversationId deleteMessages:deleteMessages];
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
    
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
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
    int iDirection = [param[@"direction"] intValue];
    EMMessageSearchDirection direction = iDirection == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    
   
    
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
    
    BOOL ret = NO;
    
    NSArray *jsonObjectList = param[@"list"];
    if (!jsonObjectList || jsonObjectList.count == 0) {
        return nil;
    }
    NSMutableArray *msgs = [NSMutableArray array];
    
    for (NSDictionary *jsonObject in jsonObjectList) {
        EMChatMessage *msg = [EMChatMessage fromJson:jsonObject];
        [msgs addObject:msg];
    }
    
    ret = [EMClient.sharedClient.chatManager importMessages:msgs];
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

- (void)sendReadAckForGroupMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];

    if (!msgId) {
        EMError *error = [[EMError alloc] initWithDescription:@"messageId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    NSString *content = param[@"content"];
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Message not found" code: EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.chatManager sendGroupMessageReadAck:msgId toGroup:msg.conversationId content:content completion:^(EMError *aError) {
        if (aError) {
            [self onError:callId error:aError];
        }else {
            [self onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (id)updateChatMessage:(NSDictionary *)param {
    
    BOOL ret = NO;
    EMChatMessage *msg = [EMChatMessage fromJson:param];
    ret = [EMClient.sharedClient.chatManager updateMessage:msg];
    return @{@"ret":@(ret)};
}

- (void)removeMessagesBeforeTimestamp:(NSDictionary *)param
                         callbackId:(NSString *)callbackId {
    NSNumber *nTimestamp = param[@"timestamp"];
    NSInteger timestamp = [nTimestamp integerValue];
    [EMClient.sharedClient.chatManager deleteMessagesBefore:timestamp completion:^(EMError *error) {
        if (error) {
            [self onError:callbackId error:error];
        }else {
            [self onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)deleteConversationFromServer:(NSDictionary *)param
                        callbackId:(NSString *)callbackId {
    
    NSString *convId = param[@"convId"];
    if (!convId) {
        EMError *error = [[EMError alloc] initWithDescription:@"conversationId is invalid" code: EMErrorMessageInvalid];
        [self onError:callbackId error:error];
        return;
    }
    
    EMConversationType type = [EMConversation typeFromInt:[param[@"convType"] intValue]];
    BOOL deleteMessages = [param[@"isDeleteServerMessages"] boolValue];
    [EMClient.sharedClient.chatManager deleteServerConversation:convId
                                               conversationType:type
                                         isDeleteServerMessages:deleteMessages
                                                     completion:^(NSString *aConversationId, EMError *aError)
     {
        if (aError) {
            [self onError:callbackId error:aError];
        }else {
            [self onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}

- (void)fetchSupportLanguages:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager fetchSupportedLangurages:^(NSArray<EMTranslateLanguage *> * _Nullable languages, EMError * _Nullable aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"List<SupportLanguage>" callbackId:callbackId userInfo:[languages toJsonArray]];
        }
    }];
}

- (void)translateMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    EMChatMessage *msg = [EMChatMessage fromJson:param[@"message"]];
    NSArray *languages = param[@"languages"];
    
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager translateMessage:msg
                                        targetLanguages:languages
                                             completion:^(EMChatMessage *message, EMError *error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:@"EMMessage" callbackId:callbackId userInfo:[message toJson]];
        }
    }];
}
- (void)fetchGroupReadAcks:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msg_id"];
    int pageSize = [param[@"pageSize"] intValue];
    NSString *ackId = param[@"ack_id"];
    __weak typeof(self) weakSelf = self;
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    EMError *e = nil;
    do {
        e = [EMError errorWithDescription:@"Invalid message" code:EMErrorMessageInvalid];
        if (msg == nil) {
            break;
        }
        if (msg.chatType != EMChatTypeGroupChat || !msg.isNeedGroupAck) {
            break;
        }
        e = nil;
    } while (false);
    if (e != nil) {
        [self onError:callbackId error:e];
        return;
    }
    [EMClient.sharedClient.chatManager asyncFetchGroupMessageAcksFromServer:msgId
                                                                    groupId:msg.conversationId
                                                            startGroupAckId:ackId
                                                                   pageSize:pageSize
                                                                 completion:^(EMCursorResult *aResult, EMError *aError, int totalCount)
     {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"CursorResult<GroupReadAck>"
                     callbackId:callbackId
                       userInfo:[aResult toJson]];
        }
    }];
}

- (void)reportMessage:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];
    NSString *tag = param[@"tag"];
    NSString *reason = param[@"reason"];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager reportMessageWithId:msgId
                                                       tag:tag
                                                    reason:reason
                                                completion:^(EMError *error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}

- (void)addReaction:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *reaction = param[@"reaction"];
    NSString *msgId = param[@"msgId"];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager addReaction:reaction
                                         toMessage:msgId
                                        completion:^(EMError * error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)removeReaction:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *reaction = param[@"reaction"];
    NSString *msgId = param[@"msgId"];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager removeReaction:reaction
                                          fromMessage:msgId
                                        completion:^(EMError * error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}

- (void)getReactionList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *msgIds = param[@"msgIds"];
    NSString *groupId = param[@"groupId"];
    EMChatType type = [EMChatMessage chatTypeFromInt:[param[@"chatType"] intValue]];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager getReactionList:msgIds
                                                groupId:groupId
                                               chatType:type
                                             completion:^(NSDictionary<NSString *,NSArray *> * dic, EMError * aError)
      {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
            for (NSString *key in dic.allKeys) {
                NSArray *ary = dic[key];
                NSMutableArray *list = [NSMutableArray array];
                for (EMMessageReaction *reaction in ary) {
                    [list addObject:[reaction toJson]];
                }
                dictionary[key] = list;
            }
            [weakSelf onSuccess:@"Dictionary<string, List<MessageReaction>>"
                     callbackId:callbackId
                       userInfo:dictionary];
        }
    }];
}
- (void)getReactionDetail:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];
    NSString *reaction = param[@"reaction"];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.chatManager getReactionDetail:msgId
                                                reaction:reaction
                                                  cursor:cursor
                                                pageSize:pageSize
                                              completion:^(EMMessageReaction * reaction, NSString * _Nullable cursor, EMError * error)
     {
        EMCursorResult *cursorResult = nil;
        if (error == nil) {
            cursorResult = [EMCursorResult cursorResultWithList:@[reaction] andCursor:cursor];
        }
        
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:@"CursorResult<MessageReaction>"
                     callbackId:callbackId
                       userInfo:[cursorResult toJson]];
        }
    }];
}
@end
