//
//  EMRoomListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/9.
//

#import "EMRoomListener.h"
#import "Transfrom.h"
#import "EMMethod.h"
#import "HyphenateWrapper.h"

@implementation EMRoomListener

- (void)userDidJoinChatroom:(EMChatroom *)aChatroom
                       user:(NSString *)aUsername {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"participant":aUsername
    };
    
    UnitySendMessage(RoomListener_Obj, "OnMemberJoined", [Transfrom DictToCString:map]);
}

- (void)userDidLeaveChatroom:(EMChatroom *)aChatroom
                        user:(NSString *)aUsername {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"roomName":aChatroom.subject,
        @"participant":aUsername
    };
    
    UnitySendMessage(RoomListener_Obj, "OnMemberExited", [Transfrom DictToCString:map]);
}

- (void)didDismissFromChatroom:(EMChatroom *)aChatroom
                        reason:(EMChatroomBeKickedReason)aReason {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"roomName":aChatroom.subject
    };
    if (aReason == EMChatroomBeKickedReasonDestroyed) {
        UnitySendMessage(RoomListener_Obj, "OnChatRoomDestroyed", [Transfrom DictToCString:map]);
    } else if (aReason == EMChatroomBeKickedReasonBeRemoved) {
        UnitySendMessage(RoomListener_Obj, "OnRemovedFromChatRoom", [Transfrom DictToCString:map]);
    }
}

- (void)chatroomMuteListDidUpdate:(EMChatroom *)aChatroom
                addedMutedMembers:(NSArray *)aMutes
                       muteExpire:(NSInteger)aMuteExpire {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"mutes":aMutes,
        @"expireTime":[NSString stringWithFormat:@"%ld", aMuteExpire]
    };
    UnitySendMessage(RoomListener_Obj, "OnMuteListAdded", [Transfrom DictToCString:map]);
}

- (void)chatroomMuteListDidUpdate:(EMChatroom *)aChatroom
              removedMutedMembers:(NSArray *)aMutes {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"list":aMutes
    };
    UnitySendMessage(RoomListener_Obj, "OnMuteListRemoved", [Transfrom DictToCString:map]);
}

- (void)chatroomAdminListDidUpdate:(EMChatroom *)aChatroom
                        addedAdmin:(NSString *)aAdmin {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"admin":aAdmin
    };
    UnitySendMessage(RoomListener_Obj, "OnAdminAdded", [Transfrom DictToCString:map]);
}

- (void)chatroomAdminListDidUpdate:(EMChatroom *)aChatroom
                      removedAdmin:(NSString *)aAdmin {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"admin":aAdmin
    };
    UnitySendMessage(RoomListener_Obj, "OnAdminRemoved", [Transfrom DictToCString:map]);
}

- (void)chatroomOwnerDidUpdate:(EMChatroom *)aChatroom
                      newOwner:(NSString *)aNewOwner
                      oldOwner:(NSString *)aOldOwner {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"newOwner":aNewOwner,
        @"oldOwner":aOldOwner
    };
    UnitySendMessage(RoomListener_Obj, "OnOwnerChanged", [Transfrom DictToCString:map]);
}

- (void)chatroomAnnouncementDidUpdate:(EMChatroom *)aChatroom
                         announcement:(NSString *)aAnnouncement {
    NSDictionary *map = @{
        @"roomId":aChatroom.chatroomId,
        @"announcement":aAnnouncement
    };
    UnitySendMessage(RoomListener_Obj, "OnAnnouncementChanged", [Transfrom DictToCString:map]);
}

@end
