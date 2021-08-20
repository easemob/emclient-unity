//
//  EMGroupManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMGroupManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMGroup+Unity.h"
#import "EMCursorResult+Unity.h"
#import "Transfrom.h"
#import "EMGroupListener.h"

@interface EMGroupManagerWrapper () <EMGroupManagerDelegate>
{
    EMGroupListener *_listener;
}
@end

@implementation EMGroupManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMGroupListener alloc] init];
        [EMClient.sharedClient.groupManager addDelegate:_listener delegateQueue:nil];
    }
    return self;
}


- (id)getGroupWithId:(NSDictionary *)param {
    if (!param) {
        return nil;
    }
    EMGroup *group = [EMGroup groupWithId:param[@"groupId"]];
    return [group toJson];
}


- (id)getJoinedGroups:(NSDictionary *)param {
    NSArray *joinedGroups = [EMClient.sharedClient.groupManager getJoinedGroups];
    NSMutableArray *list = [NSMutableArray array];
    for (EMGroup *group in joinedGroups) {
        [list addObject:[Transfrom NSStringFromJsonObject:[group toJson]]];
    }
    return list;
}


- (void)getJoinedGroupsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getJoinedGroupsFromServerWithPage:[param[@"pageNum"] intValue]
                                                                 pageSize:[param[@"pageSize"] intValue]
                                                               completion:^(NSArray *aList, EMError *aError)
     {
        if (!aError) {
            NSMutableArray *list = [NSMutableArray array];
            for (EMGroup *group in aList) {
                [list addObject:[Transfrom NSStringFromJsonObject:[group toJson]]];
            }
            [weakSelf onSuccess:@"List<EMGroup>" callbackId:callId userInfo:list];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getPublicGroupsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getPublicGroupsFromServerWithCursor:param[@"cursor"]
                                                                   pageSize:[param[@"pageSize"] integerValue]
                                                                 completion:^(EMCursorResult *aResult, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"EMCursorResult<EMGroupInfo>" callbackId:callId userInfo:[aResult toJson]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)createGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *inviteMembers = [Transfrom NSStringToJsonObject:param[@"inviteMembers"]];
    
    NSDictionary *optionJson = [Transfrom NSStringToJsonObject:param[@"options"]];
    
    EMGroupOptions *options = [EMGroupOptions formJson:optionJson];
    
    [EMClient.sharedClient.groupManager createGroupWithSubject:param[@"groupName"]
                                                   description:param[@"desc"]
                                                      invitees:inviteMembers.count == 0 ? inviteMembers : nil
                                                       message:param[@"inviteReason"]
                                                       setting:options
                                                    completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"EMGroup" callbackId:callbackId userInfo:[aGroup toJson]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getGroupSpecificationFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupSpecificationFromServerWithId:param[@"groupId"]
                                                                   completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"EMGroup" callbackId:callbackId userInfo:[aGroup toJson]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getGroupMemberListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupMemberListFromServerWithId:param[@"groupId"]
                                                                    cursor:param[@"cursor"]
                                                                  pageSize:[param[@"pageSize"] intValue]
                                                                completion:^(EMCursorResult *aResult, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"EMCursorResult<String>" callbackId:callId userInfo:[aResult toJson]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getGroupBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupBlacklistFromServerWithId:param[@"groupId"]
                                                               pageNumber:[param[@"pageNum"] intValue]
                                                                 pageSize:[param[@"pageSize"] intValue]
                                                               completion:^(NSArray *aList, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:aList];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getGroupMuteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupMuteListFromServerWithId:param[@"groupId"]
                                                              pageNumber:[param[@"pageNum"] intValue]
                                                                pageSize:[param[@"pageSize"] intValue]
                                                              completion:^(NSArray *aList, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:aList];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getGroupWhiteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupWhiteListFromServerWithId:param[@"groupId"]
                                                               completion:^(NSArray *aList, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:aList];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)isMemberInWhiteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager isMemberInWhiteListFromServerWithGroupId:param[@"groupId"]
                                                                      completion:^(BOOL inWhiteList, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"bool" callbackId:callId userInfo:@{@"bool": @(inWhiteList)}];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getGroupFileListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupFileListWithId:param[@"groupId"]
                                                    pageNumber:[param[@"pageNum"] intValue]
                                                      pageSize:[param[@"pageSize"] intValue]
                                                    completion:^(NSArray *aList, EMError *aError)
     {
        if (!aError) {
            
            NSMutableArray<NSString*>* list = [NSMutableArray array];
            
            for (EMGroupSharedFile *file in aList) {
                [list addObject:[Transfrom NSStringFromJsonObject:[file toJson]]];
            }
            
            [weakSelf onSuccess:@"List<EMMucSharedFile>" callbackId:callId userInfo:list];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}
- (void)getGroupAnnouncementFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager getGroupAnnouncementWithId:param[@"groupId"]
                                                        completion:^(NSString *aAnnouncement, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"String" callbackId:callId userInfo:aAnnouncement];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)addMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    [EMClient.sharedClient.groupManager addMembers:members
                                           toGroup:param[@"groupId"]
                                           message:param[@"welcome"]
                                        completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)removeMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager removeMembers:members
                                            fromGroup:param[@"groupId"]
                                           completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
    
}

- (void)blockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager blockMembers:members
                                           fromGroup:param[@"groupId"]
                                          completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)unblockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager unblockMembers:members
                                             fromGroup:param[@"groupId"]
                                            completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)updateGroupSubject:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager updateGroupSubject:param[@"name"]
                                                  forGroup:param[@"groupId"]
                                                completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)updateDescription:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager updateDescription:param[@"desc"]
                                                 forGroup:param[@"groupId"]
                                               completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)leaveGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager leaveGroup:param[@"groupId"]
                                        completion:^(EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)destroyGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager destroyGroup:param[@"groupId"]
                                    finishCompletion:^(EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
        
    }];
}

- (void)blockGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager blockGroup:param[@"groupId"]
                                        completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)unblockGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager unblockGroup:param[@"groupId"]
                                          completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)updateGroupOwner:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager updateGroupOwner:param[@"groupId"]
                                                newOwner:param[@"owner"]
                                              completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)addAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager addAdmin:param[@"memberId"]
                                         toGroup:param[@"groupId"]
                                      completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)removeAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager removeAdmin:param[@"admin"]
                                          fromGroup:param[@"groupId"]
                                         completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)muteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager muteMembers:members
                                   muteMilliseconds:-1
                                          fromGroup:param[@"groupId"]
                                         completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)unMuteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager unmuteMembers:members
                                            fromGroup:param[@"groupId"]
                                           completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)muteAllMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager muteAllMembersFromGroup:param[@"groupId"]
                                                     completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)unMuteAllMembers:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager unmuteAllMembersFromGroup:param[@"groupId"]
                                                       completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)addWhiteList:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    
    [EMClient.sharedClient.groupManager addWhiteListMembers:members
                                                  fromGroup:param[@"groupId"]
                                                 completion:^(EMGroup *aGroup, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)removeWhiteList:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    NSArray *members = [Transfrom NSStringToJsonObject:param[@"members"]];
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager removeWhiteListMembers:members
                                                     fromGroup:param[@"groupId"]
                                                    completion:^(EMGroup *aGroup, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)uploadGroupSharedFile:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager uploadGroupSharedFileWithId:param[@"groupId"]
                                                           filePath:param[@"filePath"]
                                                           progress:^(int progress)
     {
        
    } completion:^(EMGroupSharedFile *aSharedFile, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)downloadGroupSharedFile:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager downloadGroupSharedFileWithId:param[@"groupId"]
                                                             filePath:param[@"savePath"]
                                                         sharedFileId:param[@"fileId"]
                                                             progress:^(int progress)
     {
        
    } completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)removeGroupSharedFile:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager removeGroupSharedFileWithId:param[@"groupId"]
                                                       sharedFileId:param[@"fileId"]
                                                         completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)updateGroupAnnouncement:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager updateGroupAnnouncementWithId:param[@"groupId"]
                                                         announcement:param[@"announcement"]
                                                           completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
    
}

- (void)updateGroupExt:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager updateGroupExtWithId:param[@"groupId"]
                                                         ext:param[@"ext"]
                                                  completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)joinPublicGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager joinPublicGroup:param[@"groupId"]
                                             completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)requestToJoinPublicGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager requestToJoinPublicGroup:param[@"groupId"]
                                                         message:param[@"reason"]
                                                      completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)acceptJoinApplication:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager approveJoinGroupRequest:param[@"groupId"]
                                                         sender:param[@"username"]
                                                     completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)declineJoinApplication:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager declineJoinGroupRequest:param[@"groupId"]
                                                         sender:param[@"username"]
                                                         reason:param[@"reason"]
                                                     completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)acceptInvitationFromGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager acceptInvitationFromGroup:param[@"groupId"]
                                                          inviter:param[@"inviter"]
                                                       completion:^(EMGroup *aGroup, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:@"EMGroup" callbackId:callbackId userInfo:[aGroup toJson]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)declineInvitationFromGroup:(NSDictionary *)param callbackId:(NSString *)callbackId  {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMGroupManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.groupManager declineGroupInvitation:param[@"groupId"]
                                                       inviter:param[@"inviter"]
                                                        reason:param[@"reason"]
                                                    completion:^(EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}


@end
