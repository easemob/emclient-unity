//
//  EMClientWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMClientWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMOptions+Helper.h"
#import "EMWrapperHelper.h"
#import "EMDeviceConfig+Helper.h"
#import "EMHelper.h"
#import "EMUtil.h"


@interface EMClientWrapper () <EMClientDelegate, EMMultiDevicesDelegate>

@end

@implementation EMClientWrapper

- (instancetype)init {
    if (self = [super init]) {
        
    }
    
    return self;
}

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback
{
    NSString *ret = nil;
    if ([method isEqualToString:init]) {
        return [self sdkInit:params callback:callback];
    }else if ([method isEqualToString:createAccount]) {
        return [self createAccount:params callback:callback];
    }else if ([method isEqualToString:login]) {
        return [self login:params callback:callback];
    }else if ([method isEqualToString:logout]) {
        return [self logout:params callback:callback];
    }else if ([method isEqualToString:changeAppKey]) {
        return [self changeAppKey:params callback:callback];
    }else if ([method isEqualToString:uploadLog]) {
        return [self uploadLog:params callback:callback];
    }else if ([method isEqualToString:compressLogs]) {
        return [self compressLogs:params callback:callback];
    }else if ([method isEqualToString:getLoggedInDevicesFromServer]) {
        return [self getLoggedInDevicesFromServer:params callback:callback];
    }else if ([method isEqualToString:kickDevice]) {
        return [self kickDevice:params callback:callback];
    }else if ([method isEqualToString:kickAllDevices]) {
        return [self kickAllDevices:params callback:callback];
    }else if ([method isEqualToString:isLoggedInBefore]) {
        return [self isLoggedInBefore:params callback:callback];
    }else if ([method isEqualToString:getCurrentUser]) {
        return [self getCurrentUser:params callback:callback];
    }else if ([method isEqualToString:loginWithAgoraToken]) {
        return [self loginWithAgoraToken:params callback:callback];
    }else if ([method isEqualToString:getToken]) {
        return [self getToken:params callback:callback];
    }else if ([method isEqualToString:isConnected]) {
        return [self isConnected:params callback:callback];
    }else if ([method isEqualToString:renewToken]) {
        return [self renewToken:params callback:callback];
    }else if ([method isEqualToString:startCallback]) {
//        return [self startCallback:params callback:callback];
    }else {
        [super onMethodCall:method params:params callback:callback];
    }
    
    return ret;
}

- (NSString *)sdkInit:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSDictionary *jo = params[@"options"];
    EMOptions *options = [EMOptions fromJson:jo];
    [EMClient.sharedClient initializeSDKWithOptions:options];
    [self bindingManagers];
    [self registerEaseListener];
    [self wrapperCallback:callback error:nil object:nil];
    return nil;
}

- (NSString *)createAccount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *username = params[@"userId"];
    NSString *pwd = params[@"password"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient loginWithUsername:username
                                    password:pwd
                                  completion:^(NSString * _Nonnull aUsername, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)login:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    Boolean isToken = [params[@"isToken"] boolValue];
    NSString *userId = params[@"userId"];
    NSString *pwdOrToken = params[@"pwdOrToken"];
    
    __weak EMBaseManager *weakSelf = self;
    
    void(^result)(NSString *, EMError *) = ^(NSString *username, EMError *err) {
        [weakSelf wrapperCallback:callback error:err object:username];
    };
    
    if (isToken) {
        [EMClient.sharedClient loginWithUsername:userId
                                           token:pwdOrToken
                                      completion:^(NSString * _Nonnull aUsername, EMError * _Nullable aError)
         {
            result(aUsername, aError);
        }];
    }else {
        [EMClient.sharedClient loginWithUsername:userId
                                        password:pwdOrToken
                                      completion:^(NSString * _Nonnull aUsername, EMError * _Nullable aError)
         {
            result(aUsername, aError);
        }];
    }
    
    return nil;
}

- (NSString *)logout:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    
    Boolean unbind = [params[@"unbindDeviceToken"] boolValue];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient logout:unbind completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)changeAppKey:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *appkey = params[@"appKey"];
    [EMClient.sharedClient changeAppkey:appkey];
    [self wrapperCallback:callback error:nil object:nil];
    return nil;
}

- (NSString *)getCurrentUser:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [EMHelper getReturnJsonObject:EMClient.sharedClient.currentUsername];
}

- (NSString *)loginWithAgoraToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *username = params[@"userId"];
    NSString *token = params[@"token"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient loginWithUsername:username
                                       token:token
                                  completion:^(NSString * _Nonnull aUsername, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    return nil;
}

- (NSString *)getToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [EMHelper getReturnJsonObject:EMClient.sharedClient.accessUserToken];
}

- (NSString *)isLoggedInBefore:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [EMHelper getReturnJsonObject:@(EMClient.sharedClient.isLoggedIn)];
}

- (NSString *)isConnected:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [EMHelper getReturnJsonObject:@(EMClient.sharedClient.isConnected)];
}

- (NSString *)uploadLog:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient uploadDebugLogToServerWithCompletion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)compressLogs:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient getLogFilesPathWithCompletion:^(NSString * _Nullable aPath, EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:aPath];
    }];
    return nil;
}

- (NSString *)kickDevice:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *username = params[@"userId"];
    NSString *password = params[@"password"];
    NSString *resource = params[@"resource"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient kickDeviceWithUsername:username
                                         password:password
                                         resource:resource
                                       completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)kickAllDevices:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *username = params[@"userId"];
    NSString *password = params[@"password"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient kickAllDevicesWithUsername:username
                                             password:password
                                           completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)renewToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *token = params[@"token"];
    __weak EMBaseManager *weakSelf = self;
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        EMError *error = [EMClient.sharedClient renewToken:token];
        [weakSelf wrapperCallback:callback error:error object:nil];
    });
    
    return nil;
}

- (NSString *)getLoggedInDevicesFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *userId = params[@"userId"];
    NSString *password = params[@"password"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient getLoggedInDevicesFromServerWithUsername:userId
                                                           password:password
                                                         completion:^(NSArray<EMDeviceConfig *> * _Nullable aList, EMError * _Nullable aError) {
        NSMutableArray *list = [NSMutableArray array];
        for (EMDeviceConfig *deviceInfo in aList) {
            [list addObject:[deviceInfo toJson]];
        }
        
        [weakSelf wrapperCallback:callback error:aError object:list];
    }];
    
    return nil;
}

- (void)bindingManagers {
    self.chatManager = [[EMChatManagerWrapper alloc] init];
    self.contactManagerWrapper = [[EMContactManagerWrapper alloc] init];
    self.roomManagerWrapper = [[EMRoomManagerWrapper alloc] init];
    self.groupManagerWrapper = [[EMGroupManagerWrapper alloc] init];
    self.userInfoManagerWrapper = [[EMUserInfoManagerWrapper alloc] init];
    self.presenceManagerWrapper = [[EMPresenceManagerWrapper alloc] init];
    self.chatThreadManagerWrapper = [[EMChatThreadManagerWrapper alloc] init];
    self.pushManagerWrapper = [[EMPushManagerWrapper alloc] init];
    self.conversationWrapper = [[EMConversationWrapper alloc] init];
    self.messageManager = [[EMMessageManager alloc] init];
}

- (void)registerEaseListener {
    [EMClient.sharedClient addDelegate:self delegateQueue:nil];
    [EMClient.sharedClient addMultiDevicesDelegate:self delegateQueue:nil];
}

- (void)connectionStateDidChange:(EMConnectionState)aConnectionState {
//    EMWrapperHelper.shared.listener(connectionListener, onConnected, )
}

- (void)autoLoginDidCompleteWithError:(EMError *)aError {
    
}

- (void)userAccountDidLoginFromOtherDevice {
    
}

- (void)userAccountDidRemoveFromServer {
    
}

- (void)userDidForbidByServer {
    
}

- (void)userAccountDidForcedToLogout:(EMError *)aError {
    
}

- (void)tokenWillExpire:(EMErrorCode)aErrorCode {
    
}

- (void)tokenDidExpire:(EMErrorCode)aErrorCode {
    
}

- (void)multiDevicesContactEventDidReceive:(EMMultiDevicesEvent)aEvent username:(NSString *)aUsername ext:(NSString *)aExt {
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"operation"] = [NSNumber numberWithInt:(int)aEvent];
    dict[@"target"] = aUsername;
    dict[@"ext"] = aExt;
    [EMWrapperHelper.shared.listener onReceive:multiDeviceListener method:onContactMultiDevicesEvent info:dict.toJsonString];
}

- (void)multiDevicesGroupEventDidReceive:(EMMultiDevicesEvent)aEvent groupId:(NSString *)aGroupId ext:(id)aExt {
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"operation"] = [NSNumber numberWithInt:(int)aEvent];
    dict[@"target"] = aGroupId;
    dict[@"userIds"] = (NSArray *)aExt;
    [EMWrapperHelper.shared.listener onReceive:multiDeviceListener method:onGroupMultiDevicesEvent info:dict.toJsonString];
}

- (void)multiDevicesChatThreadEventDidReceive:(EMMultiDevicesEvent)aEvent threadId:(NSString *)aThreadId ext:(id)aExt {
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"operation"] = [NSNumber numberWithInt:(int)aEvent];
    dict[@"target"] = aThreadId;
    dict[@"userIds"] = (NSArray *)aExt;
    [EMWrapperHelper.shared.listener onReceive:multiDeviceListener method:onThreadMultiDevicesEvent info:dict.toJsonString];
}

- (void)multiDevicesUndisturbEventNotifyFormOtherDeviceData:(NSString *)undisturbData {
    
}

- (void)multiDevicesMessageBeRemoved:(NSString *)conversationId deviceId:(NSString *)deviceId {
    
}

@end
