//
//  EMClientListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <HyphenateChat/HyphenateChat.h>
#import "EMClientListener.h"
#import "EMMethod.h"
#import "Transfrom.h"
#import "HyphenateWrapper.h"

@interface EMClientListener () 

@end

@implementation EMClientListener

- (void)connectionStateDidChange:(EMConnectionState)aConnectionState {
    BOOL isConnected = aConnectionState == EMConnectionConnected;
    if (isConnected) {
        [self onConnected];
    }else {
        [self onDisconnected:2]; // 需要明确具体的code
    }}

- (void)autoLoginDidCompleteWithError:(EMError *)aError {
    if (aError) {
        [self onDisconnected:1];  // 需要明确具体的code
    }else {
        [self onConnected];
    }
}

- (void)userAccountDidLoginFromOtherDevice {
    [self onDisconnected:206];
}

- (void)userAccountDidRemoveFromServer {
    [self onDisconnected:207];
}

- (void)userDidForbidByServer {
    [self onDisconnected:305];
}

- (void)userAccountDidForcedToLogout:(EMError *)aError {
    [self onDisconnected:1];
}


- (void)tokenWillExpire:(int)aErrorCode {
    UnitySendMessage(Connection_Obj,"OnTokenWillExpire", "");
}


- (void)tokenDidExpire:(int)aErrorCode {
    UnitySendMessage(Connection_Obj, "OnTokenExpired", "");
}


- (void)onDisconnected:(int)code {
    UnitySendMessage(Connection_Obj, "OnDisconnected", [Transfrom NSStringToCSString:[NSString stringWithFormat:@"%d",code]]);
}

- (void)onConnected {
    UnitySendMessage(Connection_Obj, "OnConnected", "");
}


#pragma mark - EMMultiDevicesDelegate
- (void)multiDevicesContactEventDidReceive:(EMMultiDevicesEvent)aEvent
                                  username:(NSString *)aUsername
                                       ext:(NSString *)aExt {
    NSDictionary *info = @{@"event": @(aEvent), @"username":aUsername, @"ext":aExt};
    UnitySendMessage(MultiDevicesListener_Obj,
                     "OnContactMultiDevicesEvent",
                     [Transfrom JsonObjectToCSString:info]);
}


- (void)multiDevicesGroupEventDidReceive:(EMMultiDevicesEvent)aEvent
                                 groupId:(NSString *)aGroupId
                                     ext:(id)aExt {
    NSArray *usernames = aExt;
    NSDictionary *info = @{@"event": @(aEvent), @"groupId":aGroupId, @"usernames":[Transfrom NSStringFromJsonObject:usernames]};
    UnitySendMessage(MultiDevicesListener_Obj,
                     "OnGroupMultiDevicesEvent",
                     [Transfrom JsonObjectToCSString:info]);
}

- (void)multiDevicesUndisturbEventNotifyFormOtherDeviceData:(NSString *)undisturbData {
    UnitySendMessage(MultiDevicesListener_Obj,
                     "OndisturbMultiDevicesEvent",
                     [Transfrom NSStringToCSString:undisturbData]);
}

@end
