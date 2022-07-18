//
//  EMRoomManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMRoomManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMPageResult+Unity.h"
#import "Transfrom.h"
#import "EMChatroom+Unity.h"
#import "EMCursorResult+Unity.h"
#import "EMRoomListener.h"

@interface EMRoomManagerWrapper () <EMChatroomManagerDelegate>
{
    EMRoomListener *_listener;
}
@end

@implementation EMRoomManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMRoomListener alloc] init];
        [EMClient.sharedClient.roomManager addDelegate:_listener delegateQueue:nil];
    }
    return self;
}

- (void)getChatroomsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSInteger page = [param[@"pageNum"] integerValue];
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    [EMClient.sharedClient.roomManager getChatroomsFromServerWithPage:page
                                                             pageSize:pageSize
                                                           completion:^(EMPageResult *aResult, EMError *aError)
     {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"EMPageResult<EMChatRoom>" callbackId:callId userInfo:[aResult toJson]];
        }
    }];
}

- (void)createChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *subject = param[@"subject"];
    NSString *description = param[@"desc"];
    NSArray *invitees = [Transfrom NSStringToJsonObject:param[@"members"]];
    NSString *message = param[@"welcomeMsg"];
    NSInteger maxMembersCount = [param[@"maxUserCount"] integerValue];
    [EMClient.sharedClient.roomManager createChatroomWithSubject:subject
                                                     description:description
                                                        invitees:invitees
                                                         message:message
                                                 maxMembersCount:maxMembersCount
                                                      completion:^(EMChatroom *aChatroom, EMError *aError)
     {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"EMChatRoom" callbackId:callId userInfo:[aChatroom toJson]];
        }
    }];
}

- (void)joinChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager joinChatroom:chatroomId
                                         completion:^(EMChatroom *aChatroom, EMError *aError)
     {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"EMChatRoom" callbackId:callId userInfo:[aChatroom toJson]];
        }
    }];
}

- (void)leaveChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager leaveChatroom:chatroomId
                                          completion:^(EMError *aError)
     {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)destroyChatRoom:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager destroyChatroom:chatroomId completion:^(EMError *aError) {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)fetchChatroomInfoFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper *weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager getChatroomSpecificationFromServerWithId:chatroomId
                                                                     completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"EMChatRoom" callbackId:callId userInfo:[aChatroom toJson]];
        }
        
    }];
}

- (void)getChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    EMChatroom *chatroom = [EMChatroom chatroomWithId:param[@"roomId"]];
    if (chatroom) {
        [self onSuccess:@"EMChatRoom" callbackId:callbackId userInfo:[chatroom toJson]];
    }else {
        EMError *aError = [EMError errorWithDescription:@"Room not found" code:EMErrorMessageInvalid];
        [self onError:callbackId error:aError];
    }
}

- (void)getChatroomMemberListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *cursor = param[@"cursor"];
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    [EMClient.sharedClient.roomManager getChatroomMemberListFromServerWithId:chatroomId
                                                                      cursor:cursor
                                                                    pageSize:pageSize
                                                                  completion:^(EMCursorResult *aResult, EMError *aError)
     {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"EMCursorResult<String>" callbackId:callId userInfo:[aResult toJson]];
        }
    }];
}

- (void)fetchChatroomBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSInteger pageNumber = [param[@"pageNum"] integerValue];;
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    [EMClient.sharedClient.roomManager getChatroomBlacklistFromServerWithId:chatroomId
                                                                 pageNumber:pageNumber
                                                                   pageSize:pageSize
                                                                 completion:^(NSArray *aList, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:[Transfrom  NSStringFromJsonObject:aList]];
        }
    }];
}

- (void)getChatroomMuteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSInteger pageNumber = [param[@"pageNum"] integerValue];
    NSInteger pageSize = [param[@"pageSize"] integerValue];
    [EMClient.sharedClient.roomManager getChatroomMuteListFromServerWithId:chatroomId
                                                                pageNumber:pageNumber
                                                                  pageSize:pageSize
                                                                completion:^(NSArray *aList, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:aList]];
        }
    }];
}

- (void)fetchChatroomAnnouncement:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager getChatroomAnnouncementWithId:chatroomId
                                                          completion:^(NSString *aAnnouncement, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"String" callbackId:callId userInfo:aAnnouncement];
        }
    }];
}

- (void)chatRoomUpdateSubject:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *subject = param[@"subject"];
    [EMClient.sharedClient.roomManager updateSubject:subject
                                         forChatroom:chatroomId
                                          completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomUpdateDescription:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *desc = param[@"desc"];
    [EMClient.sharedClient.roomManager updateDescription:desc
                                             forChatroom:chatroomId
                                              completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomRemoveMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager removeMembers:members
                                        fromChatroom:chatroomId
                                          completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomBlockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager blockMembers:members
                                       fromChatroom:chatroomId
                                         completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomUnblockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager unblockMembers:members
                                         fromChatroom:chatroomId
                                           completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomChangeOwner:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *newOwner = param[@"newOwner"];
    [EMClient.sharedClient.roomManager updateChatroomOwner:chatroomId
                                                  newOwner:newOwner
                                                completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomAddAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *admin = param[@"admin"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager addAdmin:admin
                                     toChatroom:chatroomId
                                     completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomRemoveAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *admin = param[@"admin"];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager removeAdmin:admin
                                      fromChatroom:chatroomId
                                        completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomMuteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
//    NSInteger muteMilliseconds = [param[@"duration"] integerValue];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager muteMembers:members
                                  muteMilliseconds:-1
                                      fromChatroom:chatroomId
                                        completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)chatRoomUnmuteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    NSString *chatroomId = param[@"roomId"];
    [EMClient.sharedClient.roomManager unmuteMembers:members
                                        fromChatroom:chatroomId
                                          completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

- (void)updateChatroomAnnouncement:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    __weak EMRoomManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSString *chatroomId = param[@"roomId"];
    NSString *announcement = param[@"announcement"];
    [EMClient.sharedClient.roomManager updateChatroomAnnouncementWithId:chatroomId
                                                           announcement:announcement
                                                             completion:^(EMChatroom *aChatroom, EMError *aError)
    {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }
    }];
}

@end
