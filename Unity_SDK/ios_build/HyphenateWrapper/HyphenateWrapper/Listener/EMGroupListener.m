//
//  EMGroupListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/9.
//

#import "EMGroupListener.h"
#import "Transfrom.h"
#import "EMMethod.h"
#import "EMGroup+Unity.h"
#import "HyphenateWrapper.h"

@implementation EMGroupListener

- (void)groupInvitationDidReceive:(NSString *)aGroupId
                          inviter:(NSString *)aInviter
                          message:(NSString *)aMessage {
    NSDictionary *map = @{
        @"groupId":aGroupId,
        @"inviter":aInviter,
        @"message":aMessage
    };
    UnitySendMessage(GroupListener_Obj, "OnInvitationReceived", [Transfrom DictToCString:map]);
}

- (void)groupInvitationDidAccept:(EMGroup *)aGroup
                         invitee:(NSString *)aInvitee {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"invitee":aInvitee
    };
    UnitySendMessage(GroupListener_Obj, "OnInvitationAccepted", [Transfrom DictToCString:map]);
    
}

- (void)groupInvitationDidDecline:(EMGroup *)aGroup
                          invitee:(NSString *)aInvitee
                           reason:(NSString *)aReason {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"invitee":aInvitee,
        @"reason":aReason
    };
    UnitySendMessage(GroupListener_Obj, "OnInvitationDeclined", [Transfrom DictToCString:map]);
}

- (void)didJoinGroup:(EMGroup *)aGroup
             inviter:(NSString *)aInviter
             message:(NSString *)aMessage {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"message":aMessage,
        @"inviter":aInviter
    };
    UnitySendMessage(GroupListener_Obj, "OnAutoAcceptInvitationFromGroup", [Transfrom DictToCString:map]);
}

- (void)didLeaveGroup:(EMGroup *)aGroup
               reason:(EMGroupLeaveReason)aReason {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"groupName":aGroup.groupName
    };
    if (aReason == EMGroupLeaveReasonBeRemoved) {
        UnitySendMessage(GroupListener_Obj, "OnUserRemoved", [Transfrom DictToCString:map]);
    } else if (aReason == EMGroupLeaveReasonDestroyed) {
        UnitySendMessage(GroupListener_Obj, "OnGroupDestroyed", [Transfrom DictToCString:map]);
    }
}

- (void)joinGroupRequestDidReceive:(EMGroup *)aGroup
                              user:(NSString *)aUsername
                            reason:(NSString *)aReason {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"groupName":aGroup.groupName,
        @"applicant":aUsername,
        @"reason":aReason
    };
    UnitySendMessage(GroupListener_Obj, "OnRequestToJoinReceived", [Transfrom DictToCString:map]);
}

- (void)joinGroupRequestDidDecline:(NSString *)aGroupId
                            reason:(NSString *)aReason {
    NSDictionary *map = @{
        @"groupId":aGroupId,
        @"reason":aReason
    };
    UnitySendMessage(GroupListener_Obj, "OnRequestToJoinDeclined", [Transfrom DictToCString:map]);
}

- (void)joinGroupRequestDidApprove:(EMGroup *)aGroup {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"groupName":aGroup.groupName,
        @"accepter":aGroup.owner,
    };
    UnitySendMessage(GroupListener_Obj, "OnRequestToJoinAccepted", [Transfrom DictToCString:map]);
}

/*
- (void)groupListDidUpdate:(NSArray *)aGroupList {
    NSDictionary *map = @{
        @"type":@(GROUP_LIST_UPDATE),
        @"groupList":[self groupsArrayWithDictionaryArray:aGroupList]
    };
    [self.channel invokeMethod:EMMethodKeyOnGroupChanged
                     arguments:map];
}
*/

- (void)groupMuteListDidUpdate:(EMGroup *)aGroup
             addedMutedMembers:(NSArray *)aMutedMembers
                    muteExpire:(NSInteger)aMuteExpire {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"list":aMutedMembers,
        @"muteExpire":[NSNumber numberWithInteger:aMuteExpire]
    };
    UnitySendMessage(GroupListener_Obj, "OnMuteListAdded", [Transfrom DictToCString:map]);
}

- (void)groupMuteListDidUpdate:(EMGroup *)aGroup
           removedMutedMembers:(NSArray *)aMutedMembers {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"list":aMutedMembers
    };
    UnitySendMessage(GroupListener_Obj, "OnMuteListRemoved", [Transfrom DictToCString:map]);
}

- (void)groupAdminListDidUpdate:(EMGroup *)aGroup
                     addedAdmin:(NSString *)aAdmin {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"admin":aAdmin
    };
    UnitySendMessage(GroupListener_Obj, "OnAdminAdded", [Transfrom DictToCString:map]);
}

- (void)groupAdminListDidUpdate:(EMGroup *)aGroup
                   removedAdmin:(NSString *)aAdmin {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"admin":aAdmin
    };
    UnitySendMessage(GroupListener_Obj, "OnAdminRemoved", [Transfrom DictToCString:map]);
}

- (void)groupOwnerDidUpdate:(EMGroup *)aGroup
                   newOwner:(NSString *)aNewOwner
                   oldOwner:(NSString *)aOldOwner {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"newOwner":aNewOwner,
        @"oldOwner":aOldOwner
    };
    UnitySendMessage(GroupListener_Obj, "OnOwnerChanged", [Transfrom DictToCString:map]);
}

- (void)userDidJoinGroup:(EMGroup *)aGroup
                    user:(NSString *)aUsername {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"member":aUsername
    };
    UnitySendMessage(GroupListener_Obj, "OnMemberJoined", [Transfrom DictToCString:map]);
}

- (void)userDidLeaveGroup:(EMGroup *)aGroup
                     user:(NSString *)aUsername {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"member":aUsername
    };
    UnitySendMessage(GroupListener_Obj, "OnMemberExited", [Transfrom DictToCString:map]);
}

- (void)groupAnnouncementDidUpdate:(EMGroup *)aGroup
                      announcement:(NSString *)aAnnouncement {
    NSDictionary *map = @{
        @"groupId":aGroup.groupId,
        @"announcement":aAnnouncement
    };
    UnitySendMessage(GroupListener_Obj, "OnAnnouncementChanged", [Transfrom DictToCString:map]);
}

- (void)groupFileListDidUpdate:(EMGroup *)aGroup
               addedSharedFile:(EMGroupSharedFile *)aSharedFile {
    NSDictionary *map = @{
        @"type":@"onSharedFileAdded",
        @"groupId":aGroup.groupId,
        @"sharedFile":[aSharedFile toJson]
    };
    UnitySendMessage(GroupListener_Obj, "OnSharedFileAdded", [Transfrom DictToCString:map]);
}

- (void)groupFileListDidUpdate:(EMGroup *)aGroup
             removedSharedFile:(NSString *)aFileId {
    NSDictionary *map = @{
        @"type":@"onSharedFileDeleted",
        @"groupId":aGroup.groupId,
        @"fileId":aFileId
    };
    UnitySendMessage(GroupListener_Obj, "OnSharedFileDeleted", [Transfrom DictToCString:map]);
}


@end
