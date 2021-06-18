//
//  EMClientListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

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

- (void)onDisconnected:(int)code {
    UnitySendMessage(ContactListener_Obj, "OnDisconnected", [Transfrom NSStringToCSString:[NSString stringWithFormat:@"%d",code]]);
}

- (void)onConnected {
    UnitySendMessage(ContactListener_Obj, "OnConnected", "");
}

@end
