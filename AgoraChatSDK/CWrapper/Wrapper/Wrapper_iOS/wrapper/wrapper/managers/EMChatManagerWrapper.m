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
#import "EMFetchServerMessagesOption+Helper.h"
#import "EMRecallMessageInfo+Helper.h"
#import "EMError+Helper.h"
#import "EMUtil.h"
#import "EMHelper.h"
#import "EMWrapperCallback.h"
#import "Mode.h"


@interface EMChatManagerWrapper () 

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
    }else if([method isEqualToString:fetchHistoryMessagesBy]) {
        ret = [self fetchHistoryMessagesBy:params callback:callback];
    }else if([method isEqualToString:searchChatMsgFromDB]) {
        ret = [self searchChatMsgFromDB:params callback:callback];
    }else if([method isEqualToString:searchChatMsgFromDBWithScope]) {
        ret = [self searchChatMsgFromDBWithScope:params callback:callback];
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
    }else if([method isEqualToString:fetchConversationsFromServerWithPage]) {
        ret = [self reportMessage:params callback:callback];
    }else if([method isEqualToString:removeMessagesFromServerWithMsgIds]) {
        ret = [self reportMessage:params callback:callback];
    }else if([method isEqualToString:removeMessagesFromServerWithTs]) {
        ret = [self reportMessage:params callback:callback];
    }else if ([method isEqualToString:getConversationsFromServerWithCursor]) {
        ret = [self getConversationsFromServerWithCursor:params callback:callback];
    }else if ([method isEqualToString:pinConversation]) {
        ret = [self pinConversation:params callback:callback];
    }else if ([method isEqualToString:modifyMessage]) {
        ret = [self modifyMessage:params callback:callback];
    }else if ([method isEqualToString:downloadCombineMessages]) {
        ret = [self downloadCombineMessages:params callback:callback];
    }else if ([method isEqualToString:getConversationsFromServerWithCursorAndMark]) {
        ret = [self getConversationsFromServerWithCursorAndMark:params callback:callback];
    }else if ([method isEqualToString:markConversations]) {
        ret = [self markConversations:params callback:callback];
    }else if ([method isEqualToString:deleteAllMessagesAndConversations]) {
        ret = [self deleteAllMessagesAndConversations:params callback:callback];
    }else if ([method isEqualToString:pinMessage]) {
        ret = [self pinMessage:params callback:callback];
    }else if ([method isEqualToString:getPinnedMessagesFromServer]) {
        ret = [self getPinnedMessagesFromServer:params callback:callback];
    }
    else {
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
    NSString *ext = param[@"ext"];
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (!msg) {
        EMError *error = [EMError errorWithDescription:@"The message was not found" code:EMErrorMessageInvalid];
        [weakSelf wrapperCallback:callback error:error object:nil];
    }
    [EMClient.sharedClient.chatManager recallMessageWithMessageId:msgId
                                                              ext:ext
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
    //NSArray *conList = [EMClient.sharedClient.chatManager getAllConversations];
    //for (EMConversation *con in conList) {
    //    [con markAllMessagesAsRead:nil];
    //}
    
    [EMClient.sharedClient.chatManager markAllConversationsAsRead];
    return [[EMHelper getReturnJsonObject:@(YES)] toJsonString];
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
    NSArray *dictAry = param[@"list"];
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
    EMChatMessage *needDownMsg = [EMClient.sharedClient.chatManager getMessageWithMessageId:param[@"msgId"]];
    [EMClient.sharedClient.chatManager downloadMessageAttachment:needDownMsg
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
    EMChatMessage *needDownMsg = [EMClient.sharedClient.chatManager getMessageWithMessageId:param[@"msgId"]];
    [EMClient.sharedClient.chatManager downloadMessageThumbnail:needDownMsg
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
    
    NSArray *conversations = [EMClient.sharedClient.chatManager getAllConversations:YES];
    NSMutableArray *conList = [NSMutableArray array];
    for (EMConversation *conversation in conversations) {
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
    EMConversationType type = (EMConversationType)[param[@"convType"] intValue];
    int pageSize = [param[@"count"] intValue];
    NSString *startMsgId = param[@"startMsgId"];
    EMMessageFetchHistoryDirection direction = EMMessageFetchHistoryDirectionUp;
    if([param[@"direction"] intValue] == 0) {
        direction = EMMessageFetchHistoryDirectionUp;
    }else {
        direction = EMMessageFetchHistoryDirectionDown;
    }

    [EMClient.sharedClient.chatManager asyncFetchHistoryMessagesFromServer:conversationId
                                                          conversationType:type
                                                            startMessageId:startMsgId
                                                            fetchDirection:direction
                                                                  pageSize:pageSize
                                                                completion:^(EMCursorResult<EMChatMessage *> * _Nullable aResult, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchHistoryMessagesBy:(NSDictionary *)param
                            callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper *weakSelf = self;
    NSString *conversationId = param[@"convId"];
    EMConversationType type = (EMConversationType)[param[@"convType"] intValue];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    EMFetchServerMessagesOption *options = [EMFetchServerMessagesOption fromJson:param[@"options"]];
    [EMClient.sharedClient.chatManager fetchMessagesFromServerBy:conversationId conversationType:type cursor:cursor pageSize:pageSize option:options completion:^(EMCursorResult<EMChatMessage *> * _Nullable aResult, EMError * _Nullable aError) {
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
    int maxCount = [param[@"count"] intValue];
    NSString *from = param[@"from"];
    //EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    EMMessageSearchDirection direction = [param[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
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

- (NSString *)searchChatMsgFromDBWithScope:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *keywords = param[@"keywords"];
    long long timestamp = [param[@"timestamp"] longLongValue];
    int maxCount = [param[@"count"] intValue];
    NSString *from = param[@"from"];
    //EMMessageSearchDirection direction = [self searchDirectionFromString:param[@"direction"]];
    EMMessageSearchDirection direction = [param[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    EMMessageSearchScope scope = [Mode searchScopeFromInt:[param[@"scope"] intValue]];

    [EMClient.sharedClient.chatManager loadMessagesWithKeyword:keywords
                                                     timestamp:timestamp
                                                         count:maxCount
                                                      fromUser:from
                                               searchDirection:direction
                                                         scope:scope
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
    NSString *conversationId = param[@"convId"];
    EMConversationType type = EMConversationTypeChat;
    BOOL isDeleteRemoteMessage = [param[@"isDeleteServerMessages"] boolValue];
    int intType = [param[@"convType"] intValue];
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
    EMChatType type = [param[@"type"] isEqualToString:@"chat"] ? EMChatTypeChat : EMChatTypeGroupChat;
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


- (NSString *)getConversationsFromServerWithCursor:(NSDictionary *)params
                                          callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    
    BOOL pinOnly = [params[@"pinOnly"] boolValue];
    NSString *cursor = params[@"cursor"];
    int limit = [params[@"limit"] intValue];
    
    if(pinOnly) {
        [EMClient.sharedClient.chatManager getPinnedConversationsFromServerWithCursor:cursor pageSize:limit completion:^(EMCursorResult<EMConversation *> * _Nullable result, EMError * _Nullable error) {
            [weakSelf wrapperCallback:callback error:error object:[result toJson]];
        }];
    } else {
        [EMClient.sharedClient.chatManager getConversationsFromServerWithCursor:cursor pageSize:limit completion:^(EMCursorResult<EMConversation *> * _Nullable result, EMError * _Nullable error) {
            [weakSelf wrapperCallback:callback error:error object:[result toJson]];
        }];
    }
    
    
    return nil;
}

- (NSString *)getConversationsFromServerWithCursorAndMark:(NSDictionary *)params
                                                 callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;

    BOOL needMark = [params[@"needMark"] boolValue];
    int mark = [params[@"mark"] intValue];
    NSString *cursor = params[@"cursor"];
    int limit = [params[@"limit"] intValue];

    if(needMark) {
        EMConversationFilter* filter = [[EMConversationFilter alloc] initWithMark:mark pageSize:limit];

        [EMClient.sharedClient.chatManager getConversationsFromServerWithCursor:cursor filter:filter completion:^(EMCursorResult<EMConversation *> * _Nullable result, EMError * _Nullable error) {
            [weakSelf wrapperCallback:callback error:error object:[result toJson]];
        }];
    } else {
        [EMClient.sharedClient.chatManager getConversationsFromServerWithCursor:cursor pageSize:limit completion:^(EMCursorResult<EMConversation *> * _Nullable result, EMError * _Nullable error) {
            [weakSelf wrapperCallback:callback error:error object:[result toJson]];
        }];
    }
    return nil;
}

- (NSString *)pinConversation:(NSDictionary *)params
                     callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *conversationId = params[@"convId"];
    BOOL isPin = [params[@"isPinned"] boolValue];
    
    [EMClient.sharedClient.chatManager pinConversation:conversationId isPinned:isPin completionBlock:^(EMError * _Nullable error) {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    
    return nil;
}

- (NSString *)modifyMessage:(NSDictionary *)params
                     callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *msgId = params[@"msgId"];
    NSDictionary *bodyDict = params[@"body"];
    EMMessageBody *body = [EMTextMessageBody fromJson:bodyDict[@"body"]];
    
    [EMClient.sharedClient.chatManager modifyMessage:msgId
                                                body:body
                                          completion:^(EMError * _Nullable error, EMChatMessage * _Nullable message)
     {
        if (error) {
            [weakSelf wrapperCallback:callback error:error object:nil];
        }else {
            [weakSelf wrapperCallback:callback error:nil object:message.toJson];
        }
        
    }];
    
    return nil;
}

- (NSString *)downloadCombineMessages:(NSDictionary *)params
                     callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    EMChatMessage *msg = [EMChatMessage fromJson:params[@"msg"]];
    
    [EMClient.sharedClient.chatManager downloadAndParseCombineMessage:msg completion:^(NSArray<EMChatMessage *> * _Nullable messages, EMError * _Nullable error) {
        NSMutableArray *jsonMsgs = [NSMutableArray array];
        for (EMChatMessage *msg in messages) {
            [jsonMsgs addObject:[msg toJson]];
        }
        [weakSelf wrapperCallback:callback error:error object:jsonMsgs];
    }];
    
    return nil;
}

- (NSString *)fetchConversationsFromServerWithPage:(NSDictionary *)params
                                          callback:(EMWrapperCallback *)callback {
    int pageSize = [params[@"pageSize"] intValue];
    int pageNum = [params[@"pageNum"] intValue];
    __weak EMChatManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.chatManager getConversationsFromServerByPage:pageNum
                                                               pageSize:pageSize
                                                             completion:^(NSArray<EMConversation *> * _Nullable aConversations, EMError * _Nullable aError)
     {
        NSArray *sortedList = [aConversations sortedArrayUsingComparator:^NSComparisonResult(id  _Nonnull obj1, id  _Nonnull obj2) {
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

- (NSString *)removeMessagesFromServerWithMsgIds:(NSDictionary *)param
                                        callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *convId = param[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"convType"] intValue]];
    NSArray *msgIds = param[@"msgIds"];
    
    if(!EMClient.sharedClient.isLoggedIn) {
        EMError *e = [EMError errorWithDescription:@"Not login" code:EMErrorUserNotLogin];
        [weakSelf wrapperCallback:callback error:e object:nil];
        return nil;
    }
    
    if(convId == nil || convId.length == 0 || msgIds.count == 0 || type == EMConversationTypeChatRoom) {
        EMError *e = [EMError errorWithDescription:@"Invalid parameter" code:EMErrorInvalidParam];
        [weakSelf wrapperCallback:callback error:e object:nil];
        return nil;
    }
    
    EMConversation *conversation = [EMClient.sharedClient.chatManager getConversation:convId type:type createIfNotExist:YES];
    
    [conversation removeMessagesFromServerMessageIds:msgIds completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)removeMessagesFromServerWithTs:(NSDictionary *)param
                                    callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;
    NSString *convId = param[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[param[@"convType"] intValue]];
    long timestamp = [param[@"timestamp"] longValue];
    
    if(!EMClient.sharedClient.isLoggedIn) {
        EMError *e = [EMError errorWithDescription:@"Not login" code:EMErrorUserNotLogin];
        [weakSelf wrapperCallback:callback error:e object:nil];
        return nil;
    }
    
    if(convId == nil || convId.length == 0 || timestamp <= 0 || type == EMConversationTypeChatRoom) {
        EMError *e = [EMError errorWithDescription:@"Invalid parameter" code:EMErrorInvalidParam];
        [weakSelf wrapperCallback:callback error:e object:nil];
        return nil;
    }
    
    EMConversation *conversation = [EMClient.sharedClient.chatManager getConversation:convId type:type createIfNotExist:YES];
    [conversation removeMessagesFromServerWithTimeStamp:timestamp completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)markConversations:(NSDictionary *)param
                                   callback:(EMWrapperCallback *)callback
{
    BOOL isMarked = [param[@"isMarked"] boolValue];
    __weak EMChatManagerWrapper * weakSelf = self;

    if (isMarked){
        [EMClient.sharedClient.chatManager addConversationMark:param[@"convIds"]
                                                          mark:[param[@"mark"] intValue]
                                                    completion:^(EMError *aError)
         {
            [weakSelf wrapperCallback:callback error:aError object:nil];
        }];
    } else {
        [EMClient.sharedClient.chatManager removeConversationMark:param[@"convIds"]
                                                             mark:[param[@"mark"] intValue]
                                                       completion:^(EMError *aError)
         {
            [weakSelf wrapperCallback:callback error:aError object:nil];
        }];
    }

    return nil;
}

- (NSString *)deleteAllMessagesAndConversations:(NSDictionary *)param
                                    callback:(EMWrapperCallback *)callback
{
    BOOL clearServerData = [param[@"clearServerData"] boolValue];
    __weak EMChatManagerWrapper * weakSelf = self;

    [EMClient.sharedClient.chatManager deleteAllMessagesAndConversations:clearServerData
                                                              completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];

    return nil;
}

- (NSString *)pinMessage:(NSDictionary *)param
                callback:(EMWrapperCallback *)callback
{
    BOOL isPinned = [param[@"isPinned"] boolValue];
    __weak EMChatManagerWrapper * weakSelf = self;

    if (isPinned){
        [EMClient.sharedClient.chatManager pinMessage:param[@"msgId"]
                                           completion:^(EMChatMessage* message, EMError *aError)
         {
            [weakSelf wrapperCallback:callback error:aError object:nil];
        }];
    } else {
        [EMClient.sharedClient.chatManager unpinMessage:param[@"msgId"]
                                             completion:^(EMChatMessage* message, EMError *aError)
         {
            [weakSelf wrapperCallback:callback error:aError object:nil];
        }];
    }

    return nil;
}

- (NSString *)getPinnedMessagesFromServer:(NSDictionary *)param
                                 callback:(EMWrapperCallback *)callback {
    __weak EMChatManagerWrapper * weakSelf = self;

    [EMClient.sharedClient.chatManager getPinnedMessagesFromServer:param[@"convId"]
                                                        completion:^(NSArray<EMChatMessage *> * _Nullable messages, EMError * _Nullable error) {
        NSMutableArray *jsonMsgs = [NSMutableArray array];
        for (EMChatMessage *msg in messages) {
            [jsonMsgs addObject:[msg toJson]];
        }
        [weakSelf wrapperCallback:callback error:error object:jsonMsgs];
    }];

    return nil;
}

- (void)registerEaseListener {
    [EMClient.sharedClient.chatManager addDelegate:self delegateQueue:nil];
}

- (void)conversationListDidUpdate:(NSArray<EMConversation *> *)aConversationList {
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onConversationsUpdate info:nil];
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
    //NSMutableArray *msgArray = [NSMutableArray array];
    NSMutableArray *recallMessageInfoArray = [NSMutableArray array];

    for (EMRecallMessageInfo *info in aRecallMessagesInfo) {
        //[msgArray addObject:[info.recallMessage toJson]];
        [recallMessageInfoArray addObject:[info toJson]];
    }
    
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagesRecalledByExt info:[recallMessageInfoArray toJsonString]];

    //[EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagesRecalled info:[msgArray toJsonString]];
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

- (void)onMessageContentChanged:(EMChatMessage *)message operatorId:(NSString *)operatorId operationTime:(NSUInteger)operationTime {
    NSDictionary *info = @{
        @"msg": message.toJson,
        @"operatorId": operatorId,
        @"operationTime": @(operationTime),
    };
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessageContentChanged info:info.toJsonString];
    
}

- (void)onMessagePinChanged:(NSString* _Nonnull)messageId conversationId:(NSString* _Nonnull)conversationId operation:(EMMessagePinOperation)pinOperation pinInfo:(EMMessagePinInfo* _Nonnull)pinInfo {
    NSDictionary *info = @{
        @"msgId": messageId,
        @"convId": conversationId,
        @"isPinned": (pinOperation == EMMessagePin) ? @(YES) : @(NO),
        @"operatorId": pinInfo.operatorId,
        @"ts": @(pinInfo.pinTime)
    };
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onMessagePinChanged info:info.toJsonString];
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
