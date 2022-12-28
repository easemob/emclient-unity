//
//  EMRoomManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMRoomManagerWrapper.h"
#import "EMChatroom+Helper.h"
#import "EMPageResult+Helper.h"
#import "EMCursorResult+Helper.h"
#import "EMHelper.h"
#import "EMUtil.h"

@interface EMRoomManagerWrapper ()

@end

@implementation EMRoomManagerWrapper

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
    if ([joinChatRoom isEqualToString:method]) {
        ret = [self joinChatRoom:params callback:callback];
    } else if ([leaveChatRoom isEqualToString:method]) {
        ret = [self leaveChatRoom:params callback:callback];
    } else if ([fetchPublicChatRoomsFromServer isEqualToString:method]) {
        ret = [self fetchPublicChatRoomsFromServer:params callback:callback];
    } else if ([fetchChatRoomInfoFromServer isEqualToString:method]) {
        ret = [self fetchChatRoomInfoFromServer:params callback:callback];
    } else if ([getChatRoom isEqualToString:method]) {
        ret = [self getChatRoom:params callback:callback];
    } else if ([getAllChatRooms isEqualToString:method]) {
        ret = [self getAllChatRooms:params callback:callback];
    } else if ([createChatRoom isEqualToString:method]) {
        ret = [self createChatRoom:params callback:callback];
    } else if ([destroyChatRoom isEqualToString:method]) {
        ret = [self destroyChatRoom:params callback:callback];
    } else if ([changeChatRoomSubject isEqualToString:method]) {
        ret = [self changeChatRoomSubject:params callback:callback];
    } else if ([changeChatRoomDescription isEqualToString:method]) {
        ret = [self changeChatRoomDescription:params callback:callback];
    } else if ([fetchChatRoomMembers isEqualToString:method]) {
        ret = [self fetchChatRoomMembers:params callback:callback];
    } else if ([muteChatRoomMembers isEqualToString:method]) {
        ret = [self muteChatRoomMembers:params callback:callback];
    } else if ([unMuteChatRoomMembers isEqualToString:method]) {
        ret = [self unMuteChatRoomMembers:params callback:callback];
    } else if ([changeChatRoomOwner isEqualToString:method]) {
        ret = [self changeChatRoomOwner:params callback:callback];
    } else if ([addChatRoomAdmin isEqualToString:method]) {
        ret = [self addChatRoomAdmin:params callback:callback];
    } else if ([removeChatRoomAdmin isEqualToString:method]) {
        ret = [self removeChatRoomAdmin:params callback:callback];
    } else if ([fetchChatRoomMuteList isEqualToString:method]) {
        ret = [self fetchChatRoomMuteList:params callback:callback];
    } else if ([removeChatRoomMembers isEqualToString:method]) {
        ret = [self removeChatRoomMembers:params callback:callback];
    } else if ([blockChatRoomMembers isEqualToString:method]) {
        ret = [self blockChatRoomMembers:params callback:callback];
    } else if ([unBlockChatRoomMembers isEqualToString:method]) {
        ret = [self unBlockChatRoomMembers:params callback:callback];
    } else if ([fetchChatRoomBlockList isEqualToString:method]) {
        ret = [self fetchChatRoomBlockList:params callback:callback];
    } else if ([updateChatRoomAnnouncement isEqualToString:method]) {
        ret = [self updateChatRoomAnnouncement:params callback:callback];
    } else if ([fetchChatRoomAnnouncement isEqualToString:method]) {
        ret = [self fetchChatRoomAnnouncement:params callback:callback];
    } else if ([addMembersToChatRoomWhiteList isEqualToString:method]) {
        ret = [self addMembersToChatRoomWhiteList:params callback:callback];
    } else if ([removeMembersFromChatRoomWhiteList isEqualToString:method]) {
        ret = [self removeMembersFromChatRoomWhiteList:params callback:callback];
    } else if ([isMemberInChatRoomWhiteListFromServer isEqualToString:method]) {
        ret = [self isMemberInChatRoomWhiteListFromServer:params callback:callback];
    } else if ([fetchChatRoomWhiteListFromServer isEqualToString:method]) {
        ret = [self fetchChatRoomWhiteListFromServer:params callback:callback];
    } else if ([muteAllChatRoomMembers isEqualToString:method]) {
        ret = [self muteAllChatRoomsMembers:params callback:callback];
    } else if ([unMuteAllChatRoomMembers isEqualToString:method]) {
        ret = [self unMuteAllChatRoomsMembers:params callback:callback];
    } else if ([fetchChatRoomAttributes isEqualToString:method]){
        ret = [self fetchChatRoomAttributes:params callback:callback];
    } else if ([setChatRoomAttributes isEqualToString:method]){
        ret = [self setChatRoomAttributes:params callback:callback];
    } else if ([removeChatRoomAttributes isEqualToString:method]){
        ret = [self removeChatRoomAttributes:params callback:callback];
    } else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    
    return ret;
}

- (NSString *)joinChatRoom:(NSDictionary *)param
                  callback:(EMWrapperCallback *)callback {
    NSString *roomId = param[@"roomId"];
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.roomManager joinChatroom:roomId
                                         completion:^(EMChatroom * _Nullable aChatroom, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    return nil;
}

- (NSString *)leaveChatRoom:(NSDictionary *)param
                   callback:(EMWrapperCallback *)callback {
    NSString *roomId = param[@"roomId"];
    __weak EMRoomManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.roomManager leaveChatroom:roomId completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)fetchPublicChatRoomsFromServer:(NSDictionary *)param
                                    callback:(EMWrapperCallback *)callback {
    NSInteger page = [param[@"pageNum"] integerValue];
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    [EMClient.sharedClient.roomManager getChatroomsFromServerWithPage:page
                                                             pageSize:pageSize
                                                           completion:^(EMPageResult *aResult, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchChatRoomInfoFromServer:(NSDictionary *)param
                                 callback:(EMWrapperCallback *)callback {
    NSString *chatroomId = param[@"roomId"];
    BOOL fetchMembers = [param[@"fetchMembers"] boolValue];
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    [EMClient.sharedClient.roomManager getChatroomSpecificationFromServerWithId:chatroomId fetchMembers:fetchMembers completion:^(EMChatroom *aChatroom, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)getChatRoom:(NSDictionary *)param
                 callback:(EMWrapperCallback *)callback {
    EMChatroom *chatroom = [EMChatroom chatroomWithId:param[@"roomId"]];
    
    return [[EMHelper getReturnJsonObject:[chatroom toJson]] toJsonString];
}

- (NSString *)getAllChatRooms:(NSDictionary *)param
                     callback:(EMWrapperCallback *)callback {
    
    // TODO: 安卓有这个定义，iOS没有对应方法，不需要实现
    return nil;
}

- (NSString *)createChatRoom:(NSDictionary *)param
                    callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *subject = param[@"name"];
    NSString *description = param[@"desc"];
    NSArray *invitees = param[@"userIds"];
    NSString *message = param[@"msg"];
    NSInteger maxMembersCount = [param[@"count"] integerValue];
    [EMClient.sharedClient.roomManager createChatroomWithSubject:subject
                                                     description:description
                                                        invitees:invitees
                                                         message:message
                                                 maxMembersCount:maxMembersCount
                                                      completion:^(EMChatroom *aChatroom, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)destroyChatRoom:(NSDictionary *)param
                     callback:(EMWrapperCallback *)callback {
    
    NSString *roomId = param[@"roomId"];
    __weak EMRoomManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.roomManager destroyChatroom:roomId completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)changeChatRoomSubject:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    
    NSString *roomId = param[@"roomId"];
    NSString *name = param[@"name"];
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    [EMClient.sharedClient.roomManager updateSubject:name
                                         forChatroom:roomId
                                          completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    
    return nil;
}

- (NSString *)changeChatRoomDescription:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    
    NSString *roomId = param[@"roomId"];
    NSString *desc = param[@"desc"];
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.roomManager updateDescription:desc
                                             forChatroom:roomId
                                              completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    return nil;
}

- (NSString *)fetchChatRoomMembers:(NSDictionary *)param
                          callback:(EMWrapperCallback *)callback {
    
    
    NSString *chatroomId = param[@"roomId"];
    NSString *cursor = param[@"cursor"];
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    __weak EMRoomManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.roomManager getChatroomMemberListFromServerWithId:chatroomId
                                                                      cursor:cursor
                                                                    pageSize:pageSize
                                                                  completion:^(EMCursorResult *aResult, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];

    }];
    
    return nil;
}

- (NSString *)muteChatRoomMembers:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSArray *userIds = param[@"userIds"];
    NSInteger muteMilliseconds = [param[@"expireTime"] integerValue];
    if (muteMilliseconds == 0) {
        muteMilliseconds = -1;
    }
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager muteMembers:userIds
                                  muteMilliseconds:muteMilliseconds
                                      fromChatroom:chatroomId
                                        completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)unMuteChatRoomMembers:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSArray *unMuteMembers = param[@"userIds"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager unmuteMembers:unMuteMembers
                                        fromChatroom:chatroomId
                                          completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    return nil;
}

- (NSString *)changeChatRoomOwner:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *newOwner = param[@"userId"];
    [EMClient.sharedClient.roomManager updateChatroomOwner:chatroomId
                                                  newOwner:newOwner
                                                completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)addChatRoomAdmin:(NSDictionary *)param
                      callback:(EMWrapperCallback *)callback {
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *admin = param[@"userId"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager addAdmin:admin
                                     toChatroom:chatroomId
                                     completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    return nil;
}

- (NSString *)removeChatRoomAdmin:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *admin = param[@"userId"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager removeAdmin:admin
                                      fromChatroom:chatroomId
                                        completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchChatRoomMuteList:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *chatroomId = param[@"roomId"];
    NSInteger pageNumber = [param[@"pageNum"] integerValue];
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    [EMClient.sharedClient.roomManager getChatroomMuteListFromServerWithId:chatroomId
                                                                pageNumber:pageNumber
                                                                  pageSize:pageSize
                                                                completion:^(NSArray *aList, EMError *aError)
    {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        for (NSString *userId in aList) {
            dict[userId] = @(0);
        }
        [weakSelf wrapperCallback:callback error:aError object:dict];
    }];
    
    return nil;
}

- (NSString *)removeChatRoomMembers:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSArray *members = param[@"userIds"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager removeMembers:members
                                        fromChatroom:chatroomId
                                          completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    return nil;
}

- (NSString *)blockChatRoomMembers:(NSDictionary *)param
                          callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSArray *members = param[@"userIds"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager blockMembers:members
                                       fromChatroom:chatroomId
                                         completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)unBlockChatRoomMembers:(NSDictionary *)param
                            callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSArray *members = param[@"userIds"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager unblockMembers:members
                                         fromChatroom:chatroomId
                                           completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchChatRoomBlockList:(NSDictionary *)param
                            callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *chatroomId = param[@"roomId"];
    NSInteger pageNumber = [param[@"pageNum"] integerValue];;
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    [EMClient.sharedClient.roomManager getChatroomBlacklistFromServerWithId:chatroomId
                                                                 pageNumber:pageNumber
                                                                   pageSize:pageSize
                                                                 completion:^(NSArray *aList, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    
    return nil;
}

- (NSString *)updateChatRoomAnnouncement:(NSDictionary *)param
                                callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *announcement = param[@"announcement"];
    [EMClient.sharedClient.roomManager updateChatroomAnnouncementWithId:chatroomId
                                                           announcement:announcement
                                                             completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchChatRoomAnnouncement:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager getChatroomAnnouncementWithId:chatroomId
                                                          completion:^(NSString *aAnnouncement, EMError *aError)
    {
        [weakSelf wrapperCallback:callback error:aError object:aAnnouncement];
    }];
    
    return nil;
}

- (NSString *)addMembersToChatRoomWhiteList:(NSDictionary *)param
                                   callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    NSArray *ary = param[@"userIds"];
    [EMClient.sharedClient.roomManager addWhiteListMembers:ary
                                               fromChatroom:roomId
                                                 completion:^(EMChatroom *aChatroom, EMError *aError)
      {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }] ;
    
    return nil;
}

- (NSString *)removeMembersFromChatRoomWhiteList:(NSDictionary *)param
                                        callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    NSArray *ary = param[@"userIds"];
    
    [EMClient.sharedClient.roomManager removeWhiteListMembers:ary fromChatroom:roomId completion:^(EMChatroom *aChatroom, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)isMemberInChatRoomWhiteListFromServer:(NSDictionary *)param
                                           callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager isMemberInWhiteListFromServerWithChatroomId:roomId
                                                                        completion:^(BOOL inWhiteList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:@(inWhiteList)];
    }];
    
    return nil;
}

- (NSString *)fetchChatRoomWhiteListFromServer:(NSDictionary *)param
                                      callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    NSString *roomId = param[@"roomId"];
    
    [EMClient.sharedClient.roomManager getChatroomWhiteListFromServerWithId:roomId
                                                                 completion:^(NSArray *aList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    return nil;
}

- (NSString *)muteAllChatRoomsMembers:(NSDictionary *)param
                             callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager muteAllMembersFromChatroom:roomId
                                                       completion:^(EMChatroom *aChatroom, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    
    return nil;
}

- (NSString *)unMuteAllChatRoomsMembers:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager unmuteAllMembersFromChatroom:roomId
                                                       completion:^(EMChatroom *aChatroom, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aChatroom toJson]];
    }];
    return nil;
}

- (NSString *)fetchChatRoomAttributes:(NSDictionary *)param
                             callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    NSArray *keys = param[@"list"];
    
    [EMClient.sharedClient.roomManager fetchChatroomAttributes:roomId
                                                          keys:keys
                                                    completion:^(EMError * _Nullable aError, NSDictionary<NSString *,NSString *> * _Nullable properties) {
        [weakSelf wrapperCallback:callback error:aError object:properties];
    }];
    
    return nil;
}

- (NSString *)setChatRoomAttributes:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    NSDictionary *attributes = param[@"kv"];
    BOOL autoDelete = [param[@"autoDelete"] boolValue];
    BOOL forced = [param[@"forced"] boolValue];
    
    
    void (^block)(EMError *, NSDictionary <NSString *, EMError *>*) = ^(EMError *error, NSDictionary <NSString *, EMError *> *failureKeys) {
        NSMutableDictionary *tmp = [NSMutableDictionary dictionary];
        for (NSString *key in failureKeys) {
            tmp[key] = @(failureKeys[key].code);
        }
        [weakSelf wrapperCallback:callback error:error object:tmp];
    };
    
    if (forced) {
        [EMClient.sharedClient.roomManager setChatroomAttributesForced:roomId attributes:attributes autoDelete:autoDelete completionBlock:^(EMError * _Nullable aError, NSDictionary<NSString *,EMError *> * _Nullable failureKeys) {
            block(aError, failureKeys);
        }];
    }else {
        [EMClient.sharedClient.roomManager setChatroomAttributes:roomId attributes:attributes autoDelete:autoDelete completionBlock:^(EMError * _Nullable aError, NSDictionary<NSString *,EMError *> * _Nullable failureKeys) {
            block(aError, failureKeys);
        }];
    }
    
    return nil;
}

- (NSString *)removeChatRoomAttributes:(NSDictionary *)param
                              callback:(EMWrapperCallback *)callback {
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    
    NSString *roomId = param[@"roomId"];
    NSArray *keys = param[@"list"];
    BOOL forced = [param[@"forced"] boolValue];
    
    void (^block)(EMError *, NSDictionary<NSString *, EMError*> *) = ^(EMError *error, NSDictionary <NSString * ,EMError *> *failureKeys) {
        NSMutableDictionary *tmp = [NSMutableDictionary dictionary];
        for (NSString *key in failureKeys.allKeys) {
            tmp[key] = @(failureKeys[key].code);
        }
        [weakSelf wrapperCallback:callback error:error object:tmp];
    };
    
    if (forced) {
        [EMClient.sharedClient.roomManager removeChatroomAttributesForced:roomId
                                                               attributes:keys
                                                          completionBlock:^(EMError * _Nullable aError, NSDictionary<NSString *,EMError *> * _Nullable failureKeys) {
            block(aError, failureKeys);
        }];
    } else {
        [EMClient.sharedClient.roomManager removeChatroomAttributes:roomId
                                                         attributes:keys
                                                    completionBlock:^(EMError * _Nullable aError, NSDictionary<NSString *,EMError *> * _Nullable failureKeys) {
            block(aError, failureKeys);
        }];
    }
    
    return nil;
}


- (void)registerEaseListener {
    [EMClient.sharedClient.roomManager addDelegate:self delegateQueue:nil];
}


- (void)userDidJoinChatroom:(EMChatroom *)aChatroom
                       user:(NSString *)aUsername
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onMemberJoinedFromRoom info: [dictionary toJsonString]];
}


- (void)userDidLeaveChatroom:(EMChatroom *)aChatroom
                        user:(NSString *)aUsername
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"name"] = aChatroom.subject;
    dictionary[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onMemberExitedFromRoom info: [dictionary toJsonString]];
}

- (void)didDismissFromChatroom:(EMChatroom *)aChatroom
                        reason:(EMChatroomBeKickedReason)aReason
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"name"] = aChatroom.subject;
    switch (aReason) {
        case EMChatroomBeKickedReasonBeRemoved:
        {
            [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onRemovedFromRoom info: [dictionary toJsonString]];
        }
            break;
        case EMChatroomBeKickedReasonDestroyed:
        {
            [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onDestroyedFromRoom info: [dictionary toJsonString]];
        }
            break;
        case EMChatroomBeKickedReasonOffline:
        {
            [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onRemoveFromRoomByOffline info: [dictionary toJsonString]];
        }
            break;
        default:
            break;
    }
}

- (void)chatroomSpecificationDidUpdate:(EMChatroom *)aChatroom
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"room"] = [aChatroom toJson];
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onSpecificationChangedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomMuteListDidUpdate:(EMChatroom *)aChatroom
                addedMutedMembers:(NSArray<NSString *> *)aMutes
                       muteExpire:(NSInteger)aMuteExpire
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userIds"] = aMutes;
    dictionary[@"expireTime"] = @(aMuteExpire);
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onMuteListAddedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomMuteListDidUpdate:(EMChatroom *)aChatroom
              removedMutedMembers:(NSArray<NSString *> *)aMutes
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userIds"] = aMutes;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onMuteListRemovedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomWhiteListDidUpdate:(EMChatroom *)aChatroom
             addedWhiteListMembers:(NSArray<NSString *> *)aMembers
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userIds"] = aMembers;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAddAllowListMembersFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomWhiteListDidUpdate:(EMChatroom *)aChatroom
           removedWhiteListMembers:(NSArray<NSString *> *)aMembers
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userIds"] = aMembers;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onRemoveAllowListMembersFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomAllMemberMuteChanged:(EMChatroom *)aChatroom
                    isAllMemberMuted:(BOOL)aMuted
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"isAllMuted"] = @(aMuted);
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAllMemberMuteChangedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomAdminListDidUpdate:(EMChatroom *)aChatroom
                        addedAdmin:(NSString *)aAdmin
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userId"] = aAdmin;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAdminAddedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomAdminListDidUpdate:(EMChatroom *)aChatroom
                      removedAdmin:(NSString *)aAdmin
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"userId"] = aAdmin;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAdminRemovedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomOwnerDidUpdate:(EMChatroom *)aChatroom
                      newOwner:(NSString *)aNewOwner
                      oldOwner:(NSString *)aOldOwner
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"newOwner"] = aNewOwner;
    dictionary[@"oldOwner"] = aOldOwner;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onOwnerChangedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomAnnouncementDidUpdate:(EMChatroom *)aChatroom
                         announcement:(NSString * _Nullable )aAnnouncement
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = aChatroom.chatroomId;
    dictionary[@"announcement"] = aAnnouncement;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAnnouncementChangedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomAttributesDidUpdated:( NSString * _Nonnull )roomId
                        attributeMap:(NSDictionary<NSString*,NSString*> * _Nonnull)attributeMap
                                from:(NSString * _Nonnull)fromId
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = roomId;
    dictionary[@"kv"] = attributeMap;
    dictionary[@"userId"] = fromId;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAttributesChangedFromRoom info: [dictionary toJsonString]];
}

- (void)chatroomAttributesDidRemoved:( NSString * _Nonnull )roomId
                          attributes:(NSArray<__kindof NSString*> * _Nonnull)attributes
                                from:(NSString * _Nonnull)fromId
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"roomId"] = roomId;
    dictionary[@"list"] = attributes;
    dictionary[@"userId"] = fromId;
    [EMWrapperHelper.shared.listener onReceive:chatRoomListener method:onAttributesRemovedFromRoom info: [dictionary toJsonString]];
}


@end
