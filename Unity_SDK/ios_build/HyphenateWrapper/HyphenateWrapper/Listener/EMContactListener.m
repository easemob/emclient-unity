//
//  EMContactListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMContactListener.h"
#import "HyphenateWrapper.h"
#import "Transfrom.h"
#import "EMMethod.h"

@implementation EMContactListener

- (void)friendRequestDidApproveByUser:(NSString *)aUsername {
    UnitySendMessage(ContactListener_Obj, "OnFriendRequestAccepted", [Transfrom DictToCString:@{@"username": aUsername}]);
}

- (void)friendRequestDidDeclineByUser:(NSString *)aUsername {
    UnitySendMessage(ContactListener_Obj, "OnFriendRequestDeclined", [Transfrom DictToCString:@{@"username": aUsername}]);
}

- (void)friendshipDidRemoveByUser:(NSString *)aUsername {
    UnitySendMessage(ContactListener_Obj, "OnFriendRequestDeclined", [Transfrom DictToCString:@{@"OnContactDeleted": aUsername}]);
}

- (void)friendshipDidAddByUser:(NSString *)aUsername {
    UnitySendMessage(ContactListener_Obj, "OnFriendRequestDeclined", [Transfrom DictToCString:@{@"OnContactAdded": aUsername}]);
}

- (void)friendRequestDidReceiveFromUser:(NSString *)aUsername
                                message:(NSString *)aMessage {
    UnitySendMessage(ContactListener_Obj, "OnContactInvited", [Transfrom DictToCString:@{@"OnContactAdded": aUsername, @"reason": aMessage}]);
}
@end
