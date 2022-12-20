//
//  EMChatManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMChatManagerWrapper.h"
#import "EMChatMessage+Helper.h"
#import "EMConversation+Helper.h"
#import "EMMessageReaction+Helper.h"
#import "EMCursorResult+Helper.h"
#import "EMTranslateLanguage+Helper.h"
#import "EMGroupMessageAck+Helper.h"
#import "EMMessageReactionChange+Helper.h"
#import "EMError+Helper.h"
#import "EMUtil.h"
#import "EMHelper.h"



@interface EMChatManagerWrapper () <EMChatManagerDelegate>

@end

@implementation EMChatManagerWrapper

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
    if ([method isEqualToString:sendMessage]) {
        ret = [self sendMessage:params callback:callback];
    }else if([method isEqualToString:resendMessage]) {
        ret = [self resendMessage:params callback:callback];
    }else if([method isEqualToString:ackMessageRead]) {
        ret = [self ackMessageRead:params callback:callback];
    }else if([method isEqualToString:ackGroupMessageRead]) {
        ret = [self ackGroupMessageRead:params callback:callback];
    }else if([method isEqualToString:ackConversationRead]) {
        ret = [self ackConversationRead:params callback:callback];
    }else if([method isEqualToString:recallMessage]) {
        ret = [self recallMessage:params callback:callback];
    }else if([method isEqualToString:getConversation]) {
        ret = [self getConversation:params callback:callback];
    }else if([method isEqualToString:getThreadConversation]) {
        ret = [self getThreadConversation:params callback:callback];
    }else if([method isEqualToString:markAllChatMsgAsRead]) {
        ret = [self markAllMessagesAsRead:params callback:callback];
    }else if([method isEqualToString:getUnreadMessageCount]) {
        ret = [self getUnreadMessageCount:params callback:callback];
    }else if([method isEqualToString:updateChatMessage]) {
        ret = [self updateChatMessage:params callback:callback];
    }else if([method isEqualToString:downloadAttachment]) {
        ret = [self downloadAttachment:params callback:callback];
    }else if([method isEqualToString:downloadThumbnail]) {
        ret = [self downloadThumbnail:params callback:callback];
    }else if([method isEqualToString:importMessages]) {
        ret = [self importMessages:params callback:callback];
    }else if([method isEqualToString:loadAllConversations]) {
        ret = [self loadAllConversations:params callback:callback];
    }else if([method isEqualToString:getConversationsFromServer]) {
        ret = [self getConversationsFromServer:params callback:callback];
    }else if([method isEqualToString:deleteConversation]) {
        ret = [self deleteConversation:params callback:callback];
    }else if([method isEqualToString:fetchHistoryMessages]) {
        ret = [self fetchHistoryMessages:params callback:callback];
    }else if([method isEqualToString:searchChatMsgFromDB]) {
        ret = [self searchChatMsgFromDB:params callback:callback];
    }else if([method isEqualToString:getMessage]) {
        ret = [self getMessage:params callback:callback];
    }else if([method isEqualToString:asyncFetchGroupAcks]) {
        ret = [self asyncFetchGroupMessageAckFromServer:params callback:callback];
    }else if([method isEqualToString:deleteRemoteConversation]) {
        ret = [self deleteRemoteConversation:params callback:callback];
    }else if([method isEqualToString:deleteMessagesBeforeTimestamp]) {
        ret = [self deleteMessagesBeforeTimestamp:params callback:callback];
    }else if([method isEqualToString:translateMessage]) {
        ret = [self translateMessage:params callback:callback];
    }else if([method isEqualToString:fetchSupportedLanguages]) {
        ret = [self fetchSupportLanguages:params callback:callback];
    }else if([method isEqualToString:addReaction]) {
        ret = [self addReaction:params callback:callback];
    }else if([method isEqualToString:removeReaction]) {
        ret = [self removeReaction:params callback:callback];
    }else if([method isEqualToString:fetchReactionList]) {
        ret = [self fetchReactionList:params callback:callback];
    }else if([method isEqualToString:fetchReactionDetail]) {
        ret = [self fetchReactionDetail:params callback:callback];
    }else if([method isEqualToString:reportMessage]) {
        ret = [self reportMessage:params callback:callback];
    }else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}



- (NSString *)sendMessage:(NSDictionary *)param
                 callback:(EMWrapperCallback *)callback {
    
    __block EMChatMessage *msg = [EMChatMessage fromJson:param];
    
    [EMClient.sharedClient.chatManager sendMessage:msg
                                          progress:^(int progress) {
        [callback onProgress:progress];
    } completion:^(EMChatMessage *message, EMError *error) {
        if (error) {
            [callback onError:[error toJson]];
        }else {
            [callback onSuccess:[@{@"ret": [message toJson]} toJsonString]];
        }
    }];
    
    return [[EMHelper getReturnJsonObject:[msg toJson]] toJsonString];
}

- (NSString *)resendMessage:(NSDictionary *)param
                   callback:(EMWrapperCallback *)callback {
    
    __block EMChatMessage *msg = [EMChatMessage fromJson:param];
    
    [EMClient.sharedClient.chatManager resendMessage:msg
                                            progress:^(int progress) {
        [callback onProgress:progress];
    } completion:^(EMChatMessage *message, EMError *error) {
        if (error) {
            [callback onError:[error toJson]];
        }else {
            [callback onSuccess:[@{@"ret": [message toJson]} toJsonString]];
        }
    }];
    
    return [[EMHelper getReturnJsonObject:[msg toJson]] toJsonString];
}


- (NSString *)ackMessageRead:(NSDictionary *)param
                    callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *msgId = param[@"msgId"];
    NSString *to = param[@"to"];
    [EMClient.sharedClient.chatManager sendMessageReadAck:msgId
                                                   toUser:to
                                               completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)ackGroupMessageRead:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *msgId = param[@"msgId"];
    NSString *groupId = param[@"groupId"];
    NSString *content = param[@"content"];
    [EMClient.sharedClient.chatManager sendGroupMessageReadAck:msgId
                                                       toGroup:groupId
                                                       content:content
                                                    completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}


- (NSString *)ackConversationRead:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *conversationId = param[@"convId"];
    [EMClient.sharedClient.chatManager ackConversationRead:conversationId
                                                completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)recallMessage:(NSDictionary *)param
                   callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *msgId = param[@"msgId"];
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *error = [EMError errorWithDescription:@"The message was not found" code:EMErrorMessageInvalid];
        [weakSelf wrapperCallback:callback error:error object:nil];
    }
    [EMClient.sharedClient.chatManager recallMessageWithMessageId:msgId
                                                       completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)getMessage:(NSDictionary *)param
                callback:(EMWrapperCallback *)callback {
    NSString *msgId = param[@"msgId"];
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    return [[EMHelper getReturnJsonObject:[msg toJson]] toJsonString];
}


- (NSString *)getConversation:(NSDictionary *)param
                     callback:(EMWrapperCallback *)callback {
    NSString *conId = param[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"type"] intValue]];
    BOOL needCreate = [param[@"createIfNeed"] boolValue];
    EMConversation *conv = [EMClient.sharedClient.chatManager getConversation:conId
                                                                         type:type
                                                             createIfNotExist:needCreate];
    
    return [[EMHelper getReturnJsonObject:[conv toJson]] toJsonString];
}

- (NSString *)getThreadConversation:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    NSString *conId = param[@"convId"];
    EMConversation *conv = [EMClient.sharedClient.chatManager getConversation:conId
                                                                         type:EMConversationTypeGroupChat
                                                             createIfNotExist:YES
                                                                     isThread:YES];
    return [[EMHelper getReturnJsonObject:[conv toJson]] toJsonString];
}

- (NSString *)markAllMessagesAsRead:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    NSArray *conList = [EMClient.sharedClient.chatManager getAllConversations];
    for (EMConversation *con in conList) {
        [con markAllMessagesAsRead:nil];
    }
    
    return nil;
}

- (NSString *)getUnreadMessageCount:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    NSArray *conList = [EMClient.sharedClient.chatManager getAllConversations];
    int unreadCount = 0;
    for (EMConversation *con in conList) {
        unreadCount += con.unreadMessagesCount;
    }
    
    return [[EMHelper getReturnJsonObject:@(unreadCount)] toJsonString];
}

- (NSString *)updateChatMessage:(NSDictionary *)param
                       callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    EMChatMessage *msg = [EMChatMessage fromJson:param[@"message"]];
    [EMClient.sharedClient.chatManager updateMessage:msg
                                          completion:^(EMChatMessage *aMessage, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:@(aError == nil)];
    }];
    
    return nil;
}

- (NSString *)importMessages:(NSDictionary *)param
                    callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSArray *dictAry = param[@"messages"];
    NSMutableArray *messages = [NSMutableArray array];
    for (NSDictionary *dict in dictAry) {
        [messages addObject:[EMChatMessage fromJson:dict]];
    }
    [[EMClient sharedClient].chatManager importMessages:messages
                                             completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:@(aError == nil)];
    }];
    return nil;
}


- (NSString *)downloadAttachment:(NSDictionary *)param
                        callback:(EMWrapperCallback *)callback {
    __block EMChatMessage *msg = [EMChatMessage fromJson:param[@"message"]];
    EMChatMessage *needDownMSg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msg.messageId];
    [EMClient.sharedClient.chatManager downloadMessageAttachment:needDownMSg
                                                        progress:^(int progress)
     {
        [callback onProgress:progress];
    } completion:^(EMChatMessage *message, EMError *error)
     {
        if (error) {
            [callback onError:[error toJson]];
        }else {
            [callback onSuccess:[@{@"ret": [message toJson]} toJsonString]];
        }
    }];
    
    return nil;
}

- (NSString *)downloadThumbnail:(NSDictionary *)param
                       callback:(EMWrapperCallback *)callback {
    __block EMChatMessage *msg = [EMChatMessage fromJson:param[@"message"]];
    EMChatMessage *needDownMSg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msg.messageId];
    [EMClient.sharedClient.chatManager downloadMessageThumbnail:needDownMSg
                                                       progress:^(int progress)
     {
        [callback onProgress:progress];
    } completion:^(EMChatMessage *message, EMError *error)
     {
        if (error) {
            [callback onError:[error toJson]];
        }else {
            [callback onSuccess:[@{@"ret": [message toJson]} toJsonString]];
        }
    }];
    
    return nil;
}

- (NSString *)loadAllConversations:(NSDictionary *)param
                          callback:(EMWrapperCallback *)callback {
    NSArray *conversations = [EMClient.sharedClient.chatManager getAllConversations];
    NSArray *sortedList = [conversations sortedArrayUsingComparator:^NSComparisonResult(id  _Nonnull obj1, id  _Nonnull obj2) {
        if (((EMConversation *)obj1).latestMessage.timestamp > ((EMConversation *)obj2).latestMessage.timestamp) {
            return NSOrderedAscending;
        }else {
            return NSOrderedDescending;
        }
    }];
    NSMutableArray *conList = [NSMutableArray array];
    for (EMConversation *conversation in sortedList) {
        [conList addObject:[conversation toJson]];
    }
    
    return [[EMHelper getReturnJsonObject:conList] toJsonString];
}

- (NSString *)getConversationsFromServer:(NSDictionary *)param
                                callback:(EMWrapperCallback *)callback {
    
    __weak EMChatManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.chatManager getConversationsFromServer:^(NSArray *aCoversations, EMError *aError) {
        if (aError) {
            [weakSelf wrapperCallback:callback error:aError object:nil];
            return;
        }
        NSArray *sortedList = [aCoversations sortedArrayUsingComparator:^NSComparisonResult(id  _Nonnull obj1, id  _Nonnull obj2) {
            if (((EMConversation *)obj1).latestMessage.timestamp > ((EMConversation *)obj2).latestMessage.timestamp) {
                return NSOrderedAscending;
            }else {
                return NSOrderedDescending;
            }
        }];
        NSMutableArray *conList = [NSMutableArray array];
        for (EMConversation *conversation in sortedList) {
            [conList addObject:[conversation toJson]];
        }
        
        [weakSelf wrapperCallback:callback error:aError object: conList];
    }];
    
    return nil;
}

- (NSString *)deleteConversation:(NSDictionary *)param
                        callback:(EMWrapperCallback *)callback {
    NSString *conversationId = param[@"convId"];
    BOOL isDeleteMsgs = [param[@"deleteMessages"] boolValue];
    [EMClient.sharedClient.chatManager deleteConversation:conversationId
                                         isDeleteMessages:isDeleteMsgs
                                               completion:nil];
    
    return nil;
}

- (NSString *)fetchHistoryMessages:(NSDictionary *)param
                          callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper *weakSelf = self;
    NSString *conversationId = param[@"convId"];
    EMConversationType type = (EMConversationType)[param[@"type"] intValue];
    int pageSize = [param[@"count"] intValue];
    NSString *startMsgId = param[@"startMsgId"];
    [EMClient.sharedClient.chatManager asyncFetchHistoryMessagesFromServer:conversationId
                                                          conversationType:type
                                                            startMessageId:startMsgId
                                                                  pageSize:pageSize
                                                                completion:^(EMCursorResult *aResult, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    
    return nil;
}

- (NSString *)asyncFetchGroupMessageAckFromServer:(NSDictionary *)param
                                         callback:(EMWrapperCallback *)callback {
    NSString *msgId = param[@"msgId"];
    int pageSize = [param[@"pageSize"] intValue];
    NSString *ackId = param[@"ackId"];
    __weak EMChatManagerWrapper * weakSelf = self;
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
        [weakSelf wrapperCallback:callback error:e object:nil];
        return nil;
    }
    [EMClient.sharedClient.chatManager asyncFetchGroupMessageAcksFromServer:msgId
                                                                    groupId:msg.conversationId
                                                            startGroupAckId:ackId
                                                                   pageSize:pageSize
                                                                 completion:^(EMCursorResult *aResult, EMError *aError, int totalCount)
     {
        if (aError) {
            [weakSelf wrapperCallback:callback error:aError object:nil];
            return;
        }
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    
    return nil;
}

- (NSString *)searchChatMsgFromDB:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *keywords = param[@"keywords"];
    long long timestamp = [param[@"timestamp"] longLongValue];
    int maxCount = [param[@"maxCount"] intValue];
    NSString *from = param[@"from"];
    EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    [EMClient.sharedClient.chatManager loadMessagesWithKeyword:keywords
                                                     timestamp:timestamp
                                                         count:maxCount
                                                      fromUser:from
                                               searchDirection:direction
                                                    completion:^(NSArray *aMessages, EMError *aError)
     {
        if (aError) {
            [weakSelf wrapperCallback:callback error:aError object:nil];
            return;
        }
        NSMutableArray *msgList = [NSMutableArray array];
        for (EMChatMessage *msg in aMessages) {
            [msgList addObject:[msg toJson]];
        }
        
        [weakSelf wrapperCallback:callback error:nil object:msgList];
    }];
    
    return nil;
}


- (NSString *)deleteRemoteConversation:(NSDictionary *)param
                              callback:(EMWrapperCallback *)callback
{
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *conversationId = param[@"conversationId"];
    EMConversationType type = EMConversationTypeChat;
    BOOL isDeleteRemoteMessage = [param[@"isDeleteRemoteMessage"] boolValue];
    int intType = [param[@"conversationType"] intValue];
    if (intType == 0) {
        type = EMConversationTypeChat;
    }else if (intType == 1) {
        type = EMConversationTypeGroupChat;
    }else {
        type = EMConversationTypeChatRoom;
    }
    
    [EMClient.sharedClient.chatManager deleteServerConversation:conversationId
                                               conversationType:type
                                         isDeleteServerMessages:isDeleteRemoteMessage
                                                     completion:^(NSString *aConversationId, EMError *aError)
     {
        if (aError) {
            [weakSelf wrapperCallback:callback error:aError object:nil];
            return;
        }
        
        [weakSelf wrapperCallback:callback error:nil object:nil];
        
    }];
    
    return nil;
}

- (NSString *)deleteMessagesBeforeTimestamp:(NSDictionary *)param
                                   callback:(EMWrapperCallback *)callback
{
    NSUInteger timestamp = [param[@"timestamp"] unsignedIntValue];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager deleteMessagesBefore:timestamp completion:^(EMError *error) {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    
    return nil;
}

- (NSString *)translateMessage:(NSDictionary *)param
                      callback:(EMWrapperCallback *)callback {
    EMChatMessage *msg = [EMChatMessage fromJson:param[@"message"]];
    NSArray *languages = param[@"languages"];
    
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager translateMessage:msg
                                        targetLanguages:languages completion:^(EMChatMessage *message, EMError *error)
     {
        [weakSelf wrapperCallback:callback error:error object:[message toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchSupportLanguages:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager fetchSupportedLanguages:^(NSArray<EMTranslateLanguage *> * _Nullable languages, EMError * _Nullable error) {
        if (error) {
            [weakSelf wrapperCallback:callback error:error object:nil];
            return;
        }
        
        NSMutableArray *list = [NSMutableArray array];
        
        for (EMTranslateLanguage * language in languages) {
            [list addObject:[language toJson]];
        }
        
        [weakSelf wrapperCallback:callback error:error object:list];
    }];
    
    return nil;
}

- (NSString *)addReaction:(NSDictionary *)param
                 callback:(EMWrapperCallback *)callback {
    NSString *reaction = param[@"reaction"];
    NSString *msgId = param[@"msgId"];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager addReaction:reaction
                                         toMessage:msgId
                                        completion:^(EMError * error)
     {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    
    return nil;
}

- (NSString *)removeReaction:(NSDictionary *)param
                    callback:(EMWrapperCallback *)callback {
    NSString *reaction = param[@"reaction"];
    NSString *msgId = param[@"msgId"];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager removeReaction:reaction fromMessage:msgId completion:^(EMError * error) {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    return nil;
}

- (NSString *)fetchReactionList:(NSDictionary *)param
                       callback:(EMWrapperCallback *)callback {
    NSArray *msgIds = param[@"msgIds"];
    NSString *groupId = param[@"groupId"];
    EMChatType type = [EMChatMessage chatTypeFromInt:[param[@"chatType"] intValue]];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager getReactionList:msgIds
                                               groupId:groupId
                                              chatType:type
                                            completion:^(NSDictionary<NSString *,NSArray *> * dic, EMError * error)
     {
        if (error) {
            [weakSelf wrapperCallback:callback error:error object:nil];
            return;
        }
        NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
        for (NSString *key in dic.allKeys) {
            NSArray *ary = dic[key];
            NSMutableArray *list = [NSMutableArray array];
            for (EMMessageReaction *reaction in ary) {
                [list addObject:[reaction toJson]];
            }
            dictionary[key] = list;
        }
        
        [weakSelf wrapperCallback:callback error:nil object:dictionary];
    }];
    
    return nil;
}

- (NSString *)fetchReactionDetail:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    NSString *msgId = param[@"msgId"];
    NSString *reaction = param[@"reaction"];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager getReactionDetail:msgId
                                                reaction:reaction
                                                  cursor:cursor
                                                pageSize:pageSize
                                              completion:^(EMMessageReaction * reaction, NSString * _Nullable cursor, EMError * error)
     {
        if (error) {
            [weakSelf wrapperCallback:callback error:error object:nil];
            return;
        }
        EMCursorResult *cursorResult = nil;
        if (error == nil) {
            cursorResult = [EMCursorResult cursorResultWithList:@[reaction] andCursor:cursor];
        }
        
        [weakSelf wrapperCallback:callback error:nil object:[cursorResult toJson]];
    }];
    
    return nil;
}

- (NSString *)reportMessage:(NSDictionary *)param
                   callback:(EMWrapperCallback *)callback {
    NSString *msgId = param[@"msgId"];
    NSString *tag = param[@"tag"];
    NSString *reason = param[@"reason"];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager reportMessageWithId:msgId
                                                       tag:tag
                                                    reason:reason
                                                completion:^(EMError *error)
     {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    
    return nil;
}


- (void)registerEaseListener {
    [EMClient.sharedClient.chatManager addDelegate:self delegateQueue:nil];
}

- (void)conversationListDidUpdate:(NSArray<EMConversation *> *)aConversationList {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMConversation *conv in aConversationList) {
        [ary addObject:[conv toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onConversationsUpdate info:[ary toJsonString]];
}

- (void)messagesDidReceive:(NSArray<EMChatMessage *> *)aMessages {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [ary addObject:[msg toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagesReceived info:[ary toJsonString]];
}

- (void)cmdMessagesDidReceive:(NSArray<EMChatMessage *> *)aCmdMessages {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMChatMessage *msg in aCmdMessages) {
        [ary addObject:[msg toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onCmdMessagesReceived info:[ary toJsonString]];
}

- (void)messagesDidRead:(NSArray<EMChatMessage *> *)aMessages {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [ary addObject:[msg toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagesRead info:[ary toJsonString]];
}

- (void)groupMessageDidRead:(EMChatMessage *)aMessage groupAcks:(NSArray<EMGroupMessageAck *> *)aGroupAcks {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMGroupMessageAck *ack in aGroupAcks) {
        [ary addObject:[ack toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onGroupMessageRead info:[ary toJsonString]];
}

- (void)groupMessageAckHasChanged {
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onReadAckForGroupMessageUpdated info:nil];
}

- (void)onConversationRead:(NSString *)from to:(NSString *)to {
    NSMutableDictionary *jo = [NSMutableDictionary dictionary];
    jo[@"from"] = from;
    jo[@"to"] = to;
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onConversationRead info:[jo toJsonString]];
}

- (void)messagesDidDeliver:(NSArray<EMChatMessage *> *)aMessages {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMChatMessage *msg in aMessages) {
        [ary addObject:[msg toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagesDelivered info:[ary toJsonString]];
}

- (void)messagesInfoDidRecall:(NSArray<EMRecallMessageInfo *> *)aRecallMessagesInfo {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMRecallMessageInfo *info in aRecallMessagesInfo) {
        [ary addObject:[info.recallMessage toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagesRecalled info:[ary toJsonString]];
}

- (void)messageStatusDidChange:(EMChatMessage *)aMessage error:(EMError *)aError {
    
}

- (void)messageAttachmentStatusDidChange:(EMChatMessage *)aMessage error:(EMError *)aError {
    
}

- (void)messageReactionDidChange:(NSArray<EMMessageReactionChange *>* _Nonnull)changes {
    NSMutableArray *ary = [NSMutableArray array];
    for (EMMessageReactionChange *change in changes) {
        [ary addObject:[change toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessageReactionDidChange info:[ary toJsonString]];
}

#pragma mark - action

- (EMMessageSearchDirection)searchDirectionFromString:(NSString *)str {
    if ([str isEqualToString:@"up"]) {
        return EMMessageSearchDirectionUp;
    }else {
        return EMMessageSearchDirectionDown;
    }
}


@end
