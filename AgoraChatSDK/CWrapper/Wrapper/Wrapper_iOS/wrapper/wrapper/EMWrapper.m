//
//  EMWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/11.
//

#import "EMWrapper.h"
#import "EMClientWrapper.h"
#import "EMSDKMethod.h"

@interface EMWrapper ()
@property (nonnull, strong) EMClientWrapper *clientWrapper;
@end

@implementation EMWrapper

- (instancetype)init {
    if (self = [super init]) {
        _clientWrapper = [EMClientWrapper shared];
    }
    return self;
}

- (NSString *)callSDKApi:(NSString *)manager method:(NSString *)method params:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *str = nil;
    if ([manager isEqualToString:client]) {
        str = [_clientWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:chatManager]) {
        str = [_clientWrapper.chatManager onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:contactManager]) {
        str = [_clientWrapper.contactManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:roomManager]) {
        str = [_clientWrapper.roomManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:groupManager]) {
        str = [_clientWrapper.groupManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:userInfoManager]) {
        str = [_clientWrapper.userInfoManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:presenceManager]) {
        str = [_clientWrapper.presenceManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:threadManager]) {
        str = [_clientWrapper.chatThreadManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:pushManager]) {
        str = [_clientWrapper.pushManagerWrapper onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:messageManager]) {
        str = [_clientWrapper.messageManager onMethodCall:method params:params callback:callback];
    }else if ([manager isEqualToString:conversationManager]) {
        str = [_clientWrapper.conversationWrapper onMethodCall:method params:params callback:callback];
    }
    return str;
}
@end
