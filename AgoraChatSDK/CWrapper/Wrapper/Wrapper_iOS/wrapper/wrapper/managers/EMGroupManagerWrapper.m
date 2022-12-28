//
//  EMGroupManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMGroupManagerWrapper.h"
#import "EMHelper.h"
#import "EMGroup+Helper.h"
#import "EMCursorResult+Helper.h"
#import "EMUtil.h"


@interface EMGroupManagerWrapper ()

@end

@implementation EMGroupManagerWrapper

- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback
{
    NSString *ret = nil;
    
    if ([getGroupWithId isEqualToString:method]) {
        ret = [self getGroupWithId:params callback:callback];
    } else if ([getJoinedGroups isEqualToString:method]) {
        ret = [self getJoinedGroups:params callback:callback];
    } else if ([getJoinedGroupsFromServer isEqualToString:method]) {
        ret = [self getJoinedGroupsFromServer:params callback:callback];
    } else if ([getPublicGroupsFromServer isEqualToString:method]) {
        ret = [self getPublicGroupsFromServer:params callback:callback];
    } else if ([createGroup isEqualToString:method]) {
        ret = [self createGroup:params callback:callback];
    } else if ([getGroupSpecificationFromServer isEqualToString:method]) {
        ret = [self getGroupSpecificationFromServer:params callback:callback];
    } else if ([getGroupMemberListFromServer isEqualToString:method]) {
        ret = [self getGroupMemberListFromServer:params callback:callback];
    } else if ([getGroupMuteListFromServer isEqualToString:method]) {
        ret = [self getGroupMuteListFromServer:params callback:callback];
    } else if ([getGroupWhiteListFromServer isEqualToString:method]) {
        ret = [self getGroupWhiteListFromServer:params callback:callback];
    } else if ([isMemberInWhiteListFromServer isEqualToString:method]) {
        ret = [self isMemberInWhiteListFromServer:params callback:callback];
    } else if ([getGroupFileListFromServer isEqualToString:method]) {
        ret = [self getGroupFileListFromServer:params callback:callback];
    } else if ([getGroupAnnouncementFromServer isEqualToString:method]) {
        ret = [self getGroupAnnouncementFromServer:params callback:callback];
    } else if ([getGroupBlockListFromServer isEqualToString:method]) {
        ret = [self getGroupBlockListFromServer:params callback:callback];
    } else if ([addMembers isEqualToString:method]) {
        ret = [self addMembers:params callback:callback];
    } else if ([inviterUser isEqualToString:method]){
        ret = [self inviterUser:params callback:callback];
    } else if ([removeMembers isEqualToString:method]) {
        ret = [self removeMembers:params callback:callback];
    } else if ([blockMembers isEqualToString:method]) {
        ret = [self blockMembers:params callback:callback];
    } else if ([unblockMembers isEqualToString:method]) {
        ret = [self unblockMembers:params callback:callback];
    } else if ([updateGroupSubject isEqualToString:method]) {
        ret = [self updateGroupSubject:params callback:callback];
    } else if ([updateDescription isEqualToString:method]) {
        ret = [self updateDescription:params callback:callback];
    } else if ([leaveGroup isEqualToString:method]) {
        ret = [self leaveGroup:params callback:callback];
    } else if ([destroyGroup isEqualToString:method]) {
        ret = [self destroyGroup:params callback:callback];
    } else if ([blockGroup isEqualToString:method]) {
        ret = [self blockGroup:params callback:callback];
    } else if ([unblockGroup isEqualToString:method]) {
        ret = [self unblockGroup:params callback:callback];
    } else if ([updateGroupOwner isEqualToString:method]) {
        ret = [self updateGroupOwner:params callback:callback];
    } else if ([addAdmin isEqualToString:method]) {
        ret = [self addAdmin:params callback:callback];
    } else if ([removeAdmin isEqualToString:method]) {
        ret = [self removeAdmin:params callback:callback];
    } else if ([muteMembers isEqualToString:method]) {
        ret = [self muteMembers:params callback:callback];
    } else if ([unMuteMembers isEqualToString:method]) {
        ret = [self unMuteMembers:params callback:callback];
    } else if ([muteAllMembers isEqualToString:method]) {
        ret = [self muteAllMembers:params callback:callback];
    } else if ([unMuteAllMembers isEqualToString:method]) {
        ret = [self unMuteAllMembers:params callback:callback];
    } else if ([addWhiteList isEqualToString:method]) {
        ret = [self addWhiteList:params callback:callback];
    } else if ([removeWhiteList isEqualToString:method]) {
        ret = [self removeWhiteList:params callback:callback];
    } else if ([uploadGroupSharedFile isEqualToString:method]) {
        ret = [self uploadGroupSharedFile:params callback:callback];
    } else if ([downloadGroupSharedFile isEqualToString:method]) {
        ret = [self downloadGroupSharedFile:params callback:callback];
    } else if ([removeGroupSharedFile isEqualToString:method]) {
        ret = [self removeGroupSharedFile:params callback:callback];
    } else if ([updateGroupAnnouncement isEqualToString:method]) {
        ret = [self updateGroupAnnouncement:params callback:callback];
    } else if ([updateGroupExt isEqualToString:method]) {
        ret = [self updateGroupExt:params callback:callback];
    } else if ([joinPublicGroup isEqualToString:method]) {
        ret = [self joinPublicGroup:params callback:callback];
    } else if ([requestToJoinGroup isEqualToString:method]) {
        ret = [self requestToJoinPublicGroup:params callback:callback];
    } else if ([acceptJoinApplication isEqualToString:method]) {
        ret = [self acceptJoinApplication:params callback:callback];
    } else if ([declineJoinApplication isEqualToString:method]) {
        ret = [self declineJoinApplication:params callback:callback];
    } else if ([acceptInvitationFromGroup isEqualToString:method]) {
        ret = [self acceptInvitationFromGroup:params callback:callback];
    } else if ([declineInvitationFromGroup isEqualToString:method]) {
        ret = [self declineInvitationFromGroup:params callback:callback];
    } else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    
    return ret;
}

- (NSString *)getGroupWithId:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *groupId = params[@"groupId"];
    EMGroup *group = [EMGroup groupWithId:groupId];
    return [[EMHelper getReturnJsonObject:[group toJson]] toJsonString];
}

- (NSString *)getJoinedGroups:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSArray *joinedGroups = [EMClient.sharedClient.groupManager getJoinedGroups];
    NSMutableArray *list = [NSMutableArray array];
    for (EMGroup *group in joinedGroups) {
        [list addObject:[group toJson]];
    }
    return [[EMHelper getReturnJsonObject:list] toJsonString];
}


- (NSString *)getJoinedGroupsFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    
    __weak EMGroupManagerWrapper *weakSelf = self;
    
    int pageSize = [params[@"pageSize"] intValue];
    int pageNum = [params[@"pageNum"] intValue];
    BOOL needMemberCount = [params[@"needMemberCount"] boolValue];
    BOOL needRole = [params[@"needRole"] boolValue];
    [EMClient.sharedClient.groupManager getJoinedGroupsFromServerWithPage:pageNum
                                                                 pageSize:pageSize
                                                          needMemberCount:needMemberCount
                                                                 needRole:needRole
                                                               completion:^(NSArray<EMGroup *> * _Nullable aList, EMError * _Nullable aError) {
        if (aError) {
            [weakSelf wrapperCallback:callback error:aError object:nil];
            return;
        }
        NSMutableArray *list = [NSMutableArray array];
        for (EMGroup *group in aList) {
            [list addObject:[group toJson]];
        }
        
        [weakSelf wrapperCallback:callback error:nil object:list];
    }];
    
    return nil;
}

- (NSString *)getPublicGroupsFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    
    [EMClient.sharedClient.groupManager getPublicGroupsFromServerWithCursor:params[@"cursor"]
                                                                   pageSize:[params[@"pageSize"] integerValue]
                                                                 completion:^(EMCursorResult *aResult, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)createGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager createGroupWithSubject:params[@"name"]
                                                   description:params[@"desc"]
                                                      invitees:params[@"userIds"]
                                                       message:params[@"msg"]
                                                       setting:[EMGroupOptions formJson:params[@"options"]]
                                                    completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)getGroupSpecificationFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    NSString *groupId = params[@"groupId"];
    BOOL fetchMembers = [params[@"fetchMembers"] boolValue];
    [EMClient.sharedClient.groupManager getGroupSpecificationFromServerWithId:groupId
                                                                 fetchMembers:fetchMembers
                                                                   completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)getGroupMemberListFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager getGroupMemberListFromServerWithId:params[@"groupId"]
                                                                    cursor:params[@"cursor"]
                                                                  pageSize:[params[@"pageSize"] intValue]
                                                                completion:^(EMCursorResult *aResult, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)getGroupBlockListFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager getGroupBlacklistFromServerWithId:params[@"groupId"]
                                                               pageNumber:[params[@"pageNum"] intValue]
                                                                 pageSize:[params[@"pageSize"] intValue]
                                                               completion:^(NSArray *aList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    return nil;
}

- (NSString *)getGroupMuteListFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager fetchGroupMuteListFromServerWithId:params[@"groupId"]
                                                                pageNumber:[params[@"pageNum"] intValue]
                                                                  pageSize:[params[@"pageSize"] intValue]
                                                                completion:^(NSDictionary<NSString *,NSNumber *> *aDict, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aDict];
    }];
    return nil;
}

- (NSString *)getGroupWhiteListFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager getGroupWhiteListFromServerWithId:params[@"groupId"]
                                                               completion:^(NSArray *aList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    return nil;
}

- (NSString *)isMemberInWhiteListFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager isMemberInWhiteListFromServerWithGroupId:params[@"groupId"]
                                                                      completion:^(BOOL inWhiteList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:@{@"ret":@(inWhiteList)}];
    }];
    
    return nil;
}

- (NSString *)getGroupFileListFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager getGroupFileListWithId:params[@"groupId"]
                                                    pageNumber:[params[@"pageNum"] intValue]
                                                      pageSize:[params[@"pageSize"] intValue]
                                                    completion:^(NSArray *aList, EMError *aError)
     {
        
        if (aError) {
            [weakSelf wrapperCallback:callback error:aError object:nil];
            return;
        }
        
        NSMutableArray *array = [NSMutableArray array];
        for (EMGroupSharedFile *file in aList) {
            [array addObject:[file toJson]];
        }
        
        [weakSelf wrapperCallback:callback error:aError object:array];
    }];
    return nil;
}

- (NSString *)getGroupAnnouncementFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager getGroupAnnouncementWithId:params[@"groupId"]
                                                        completion:^(NSString *aAnnouncement, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aAnnouncement];
    }];
    return nil;
}

- (NSString *)inviterUser:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager addMembers:params[@"userIds"]
                                            toGroup:params[@"groupId"]
                                            message:params[@"msg"]
                                         completion:^(EMGroup *aGroup, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)addMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager addMembers:params[@"userIds"]
                                           toGroup:params[@"groupId"]
                                           message:params[@"msg"]
                                        completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    
    return nil;
}

- (NSString *)removeMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager removeMembers:params[@"userIds"]
                                            fromGroup:params[@"groupId"]
                                           completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    
    return nil;
}

- (NSString *)blockMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager blockMembers:params[@"userIds"]
                                           fromGroup:params[@"groupId"]
                                          completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)unblockMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager unblockMembers:params[@"userIds"]
                                             fromGroup:params[@"groupId"]
                                            completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)updateGroupSubject:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager updateGroupSubject:params[@"name"]
                                                  forGroup:params[@"groupId"]
                                                completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)updateDescription:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager updateDescription:params[@"desc"]
                                                 forGroup:params[@"groupId"]
                                               completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)leaveGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager leaveGroup:params[@"groupId"]
                                        completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)destroyGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager destroyGroup:params[@"groupId"]
                                    finishCompletion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)blockGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager blockGroup:params[@"groupId"]
                                        completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
        
    }];
    return nil;
}

- (NSString *)unblockGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager unblockGroup:params[@"groupId"]
                                          completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)updateGroupOwner:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager updateGroupOwner:params[@"groupId"]
                                                newOwner:params[@"userId"]
                                              completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)addAdmin:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager addAdmin:params[@"userId"]
                                         toGroup:params[@"groupId"]
                                      completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)removeAdmin:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager removeAdmin:params[@"userId"]
                                          fromGroup:params[@"groupId"]
                                         completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)muteMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    
    NSArray *userIds = params[@"userIds"];
    NSInteger muteMilliseconds = [params[@"expireTime"] integerValue];
    if (muteMilliseconds == 0) {
        muteMilliseconds = -1;
    }
    NSString *groupId = params[@"groupId"];
    
    [EMClient.sharedClient.groupManager muteMembers:userIds
                                   muteMilliseconds:muteMilliseconds
                                          fromGroup:groupId
                                         completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)unMuteMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager unmuteMembers:params[@"userIds"]
                                            fromGroup:params[@"groupId"]
                                           completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    
    return nil;
}

- (NSString *)muteAllMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager muteAllMembersFromGroup:params[@"groupId"]
                                                     completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    
    return nil;
}

- (NSString *)unMuteAllMembers:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager unmuteAllMembersFromGroup:params[@"groupId"]
                                                       completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)addWhiteList:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager addWhiteListMembers:params[@"userIds"]
                                                  fromGroup:params[@"groupId"]
                                                 completion:^(EMGroup *aGroup, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)removeWhiteList:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager removeWhiteListMembers:params[@"userIds"]
                                                     fromGroup:params[@"groupId"]
                                                    completion:^(EMGroup *aGroup, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)uploadGroupSharedFile:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager uploadGroupSharedFileWithId:params[@"groupId"]
                                                           filePath:params[@"filePath"]
                                                           progress:^(int progress)
     {
        callback.onProgressCallback(progress);
    } completion:^(EMGroupSharedFile *aSharedFile, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aSharedFile toJson]];
    }];
    return nil;
}

- (NSString *)downloadGroupSharedFile:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __block NSString *fileId = params[@"fileId"];
    __block NSString *savePath = params[@"savePath"];
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager downloadGroupSharedFileWithId:params[@"groupId"]
                                                             filePath:savePath
                                                         sharedFileId:fileId
                                                             progress:^(int progress)
     {
        callback.onProgressCallback(progress);
    } completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];

    return nil;
}

- (NSString *)removeGroupSharedFile:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager removeGroupSharedFileWithId:params[@"groupId"]
                                                       sharedFileId:params[@"fileId"]
                                                         completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];

    return nil;
}

- (NSString *)updateGroupAnnouncement:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager updateGroupAnnouncementWithId:params[@"groupId"]
                                                         announcement:params[@"announcement"]
                                                           completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)updateGroupExt:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager updateGroupExtWithId:params[@"groupId"]
                                                         ext:params[@"ext"]
                                                  completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)joinPublicGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager joinPublicGroup:params[@"groupId"]
                                             completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)requestToJoinPublicGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager requestToJoinPublicGroup:params[@"groupId"]
                                                         message:params[@"msg"]
                                                      completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)acceptJoinApplication:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager approveJoinGroupRequest:params[@"groupId"]
                                                         sender:params[@"userId"]
                                                     completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)declineJoinApplication:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager declineJoinGroupRequest:params[@"groupId"]
                                                         sender:params[@"userId"]
                                                         reason:params[@"msg"]
                                                     completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)acceptInvitationFromGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager acceptInvitationFromGroup:params[@"groupId"]
                                                          inviter:params[@"userId"]
                                                       completion:^(EMGroup *aGroup, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aGroup toJson]];
    }];
    return nil;
}

- (NSString *)declineInvitationFromGroup:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMGroupManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.groupManager declineGroupInvitation:params[@"groupId"]
                                                       inviter:params[@"userId"]
                                                        reason:params[@"msg"]
                                                    completion:^(EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (void)registerEaseListener{
    [EMClient.sharedClient.groupManager addDelegate:self delegateQueue:nil];
}


- (void)groupInvitationDidReceive:(NSString *_Nonnull)aGroupId
                        groupName:(NSString *_Nonnull)aGroupName
                          inviter:(NSString *_Nonnull)aInviter
                          message:(NSString *_Nullable)aMessage
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroupId;
    dictionary[@"name"] = aGroupName;
    dictionary[@"userId"] = aInviter;
    dictionary[@"msg"] = aMessage;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onInvitationReceivedFromGroup info: [dictionary toJsonString]];
}

- (void)groupInvitationDidAccept:(EMGroup *_Nonnull)aGroup
                         invitee:(NSString *_Nonnull)aInvitee
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aInvitee;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onInvitationAcceptedFromGroup info: [dictionary toJsonString]];
}

- (void)groupInvitationDidDecline:(EMGroup *_Nonnull)aGroup
                          invitee:(NSString *_Nonnull)aInvitee
                           reason:(NSString *_Nullable)aReason
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aInvitee;
    dictionary[@"msg"] = aReason;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onInvitationDeclinedFromGroup info: [dictionary toJsonString]];
}

- (void)didJoinGroup:(EMGroup *_Nonnull)aGroup
             inviter:(NSString *_Nonnull)aInviter
             message:(NSString *_Nullable)aMessage
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aInviter;
    dictionary[@"msg"] = aMessage;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onAutoAcceptInvitationFromGroup info: [dictionary toJsonString]];
}

- (void)didLeaveGroup:(EMGroup *_Nonnull)aGroup
               reason:(EMGroupLeaveReason)aReason
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"name"] = aGroup.groupName;
    if (aReason == EMGroupLeaveReasonBeRemoved) {
        [EMWrapperHelper.shared.listener onReceive:groupListener method:onUserRemovedFromGroup info: [dictionary toJsonString]];
    }else if (aReason == EMGroupLeaveReasonDestroyed) {
        [EMWrapperHelper.shared.listener onReceive:groupListener method:onDestroyedFromGroup info: [dictionary toJsonString]];
    }
}

- (void)joinGroupRequestDidReceive:(EMGroup *_Nonnull)aGroup
                              user:(NSString *_Nonnull)aUsername
                            reason:(NSString *_Nullable)aReason
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"name"] = aGroup.groupName;
    dictionary[@"userId"] = aUsername;
    dictionary[@"msg"] = aReason;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onRequestToJoinReceivedFromGroup info: [dictionary toJsonString]];
}

- (void)joinGroupRequestDidDecline:(NSString *_Nonnull)aGroupId
                            reason:(NSString *_Nullable)aReason
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroupId;
    dictionary[@"msg"] = aReason;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onRequestToJoinDeclinedFromGroup info: [dictionary toJsonString]];
}

- (void)joinGroupRequestDidApprove:(EMGroup *_Nonnull)aGroup
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"name"] = aGroup.groupName;
    dictionary[@"userId"] = aGroup.owner;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onRequestToJoinAcceptedFromGroup info: [dictionary toJsonString]];
}

- (void)groupListDidUpdate:(NSArray<EMGroup *> *_Nonnull)aGroupList
{
    // TODO: 安卓没有提供
}

- (void)groupMuteListDidUpdate:(EMGroup *_Nonnull)aGroup
             addedMutedMembers:(NSArray<NSString *> *_Nonnull)aMutedMembers
                    muteExpire:(NSInteger)aMuteExpire
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userIds"] = aMutedMembers;
    dictionary[@"expireTime"] = @(aMuteExpire);
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onMuteListAddedFromGroup info: [dictionary toJsonString]];
}


- (void)groupMuteListDidUpdate:(EMGroup *_Nonnull)aGroup
           removedMutedMembers:(NSArray<NSString *> *_Nonnull)aMutedMembers
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userIds"] = aMutedMembers;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onMuteListRemovedFromGroup info: [dictionary toJsonString]];
}

- (void)groupWhiteListDidUpdate:(EMGroup *_Nonnull)aGroup
          addedWhiteListMembers:(NSArray<NSString *> *_Nonnull)aMembers
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userIds"] = aMembers;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onAddAllowListMembersFromGroup info: [dictionary toJsonString]];
}

- (void)groupWhiteListDidUpdate:(EMGroup *_Nonnull)aGroup
        removedWhiteListMembers:(NSArray<NSString *> *_Nonnull)aMembers
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userIds"] = aMembers;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onRemoveAllowListMembersFromGroup info: [dictionary toJsonString]];
}

- (void)groupAllMemberMuteChanged:(EMGroup *_Nonnull)aGroup
                 isAllMemberMuted:(BOOL)aMuted
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"isMuteAll"] = @(aMuted);
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onAllMemberMuteChangedFromGroup info: [dictionary toJsonString]];
}

- (void)groupAdminListDidUpdate:(EMGroup *_Nonnull)aGroup
                     addedAdmin:(NSString *_Nonnull)aAdmin
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aAdmin;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onAdminAddedFromGroup info: [dictionary toJsonString]];
}

- (void)groupAdminListDidUpdate:(EMGroup *_Nonnull)aGroup
                   removedAdmin:(NSString *_Nonnull)aAdmin
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aAdmin;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onAdminRemovedFromGroup info: [dictionary toJsonString]];
}

- (void)groupOwnerDidUpdate:(EMGroup *_Nonnull)aGroup
                   newOwner:(NSString *_Nonnull)aNewOwner
                   oldOwner:(NSString *_Nonnull)aOldOwner
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"newOwner"] = aNewOwner;
    dictionary[@"oldOwner"] = aOldOwner;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onOwnerChangedFromGroup info: [dictionary toJsonString]];
}

- (void)userDidJoinGroup:(EMGroup *_Nonnull)aGroup
                    user:(NSString *_Nonnull)aUsername
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onMemberJoinedFromGroup info: [dictionary toJsonString]];
}

- (void)userDidLeaveGroup:(EMGroup *_Nonnull)aGroup
                     user:(NSString *_Nonnull)aUsername
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onMemberExitedFromGroup info: [dictionary toJsonString]];
}

- (void)groupAnnouncementDidUpdate:(EMGroup *_Nonnull)aGroup
                      announcement:(NSString *_Nullable)aAnnouncement
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"announcement"] = aAnnouncement;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onAnnouncementChangedFromGroup info: [dictionary toJsonString]];
}

- (void)groupFileListDidUpdate:(EMGroup *_Nonnull)aGroup
               addedSharedFile:(EMGroupSharedFile *_Nonnull)aSharedFile
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"file"] = [aSharedFile toJson];
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onSharedFileAddedFromGroup info: [dictionary toJsonString]];
}

- (void)groupFileListDidUpdate:(EMGroup *_Nonnull)aGroup
             removedSharedFile:(NSString *_Nonnull)aFileId
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"fileId"] = aFileId;
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onSharedFileDeletedFromGroup info: [dictionary toJsonString]];
}

- (void)groupStateChanged:(EMGroup *)aGroup
               isDisabled:(BOOL)aDisabled
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"groupId"] = aGroup.groupId;
    dictionary[@"isDisabled"] = @(aDisabled);
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onStateChangedFromGroup info: [dictionary toJsonString]];
}

- (void)groupSpecificationDidUpdate:(EMGroup *)aGroup
{
    NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
    dictionary[@"group"] = [aGroup toJson];
    [EMWrapperHelper.shared.listener onReceive:groupListener method:onSpecificationChangedFromGroup info: [dictionary toJsonString]];
}

@end
