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
#import "EMConversation+Helper.h"
#import "EMHelper.h"
#import "EMUtil.h"

#import "DelegateTester.h"


@interface EMClientWrapper () 

@end

@implementation EMClientWrapper

+ (EMClientWrapper *)shared {
    static EMClientWrapper *wrapper_;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        wrapper_ = [[EMClientWrapper alloc] init];
    });
    
    return wrapper_;
}


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
    if ([method isEqualToString:runDelegateTester]) {
        return [self runDelegateTester];
    }
    else if ([method isEqualToString:init]) {
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
    }else if ([method isEqualToString:kickDeviceWithToken]) {
        return [self kickDeviceWithToken:params callback:callback];
    }else if ([method isEqualToString:kickAllDevicesWithToken]) {
        return [self kickAllDevicesWithToken:params callback:callback];
    }else if ([method isEqualToString:getLoggedInDevicesFromServerWithToken]) {
        return [self getLoggedInDevicesFromServerWithToken:params callback:callback];
    }else {
        [super onMethodCall:method params:params callback:callback];
    }
    
    return ret;
}




- (NSString *)runDelegateTester {
    [DelegateTester.shared startTest];
    return nil;
}

- (NSString *)sdkInit:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSDictionary *jo = params[@"options"];
    EMOptions *options = [EMOptions fromJson:jo];
    EMError *err = [EMClient.sharedClient initializeSDKWithOptions:options];
    if(err != nil) {
        return [[EMHelper getReturnJsonObject:@(err.code)] toJsonString];
    }
    [self bindingManagers];
    [self registerEaseListener];
    [self wrapperCallback:callback error:nil object:nil];
    return [[EMHelper getReturnJsonObject:@(0)] toJsonString];
}

- (NSString *)createAccount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *username = params[@"userId"];
    NSString *pwd = params[@"password"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient registerWithUsername:username
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
        [weakSelf wrapperCallback:callback error:err object:nil];
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
    return [[EMHelper getReturnJsonObject:EMClient.sharedClient.currentUsername] toJsonString];
}

- (NSString *)loginWithAgoraToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *username = params[@"userId"];
    NSString *token = params[@"token"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient loginWithUsername:username
                                  agoraToken:token
                                  completion:^(NSString * _Nonnull aUsername, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    return nil;
}

- (NSString *)getToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [[EMHelper getReturnJsonObject:EMClient.sharedClient.accessUserToken] toJsonString];
}

- (NSString *)isLoggedInBefore:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [[EMHelper getReturnJsonObject:@(EMClient.sharedClient.isLoggedIn)] toJsonString];
}

- (NSString *)isConnected:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    return [[EMHelper getReturnJsonObject:@(EMClient.sharedClient.isConnected)] toJsonString];
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
    NSString *userId = params[@"userId"];
    NSString *password = params[@"password"];
    NSString *resource = params[@"resource"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient kickDeviceWithUsername:userId
                                         password:password
                                         resource:resource
                                       completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)kickAllDevices:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *userId = params[@"userId"];
    NSString *password = params[@"password"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient kickAllDevicesWithUsername:userId
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

- (NSString *)kickDeviceWithToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *userId = params[@"userId"];
    NSString *token = params[@"token"];
    NSString *resource = params[@"resource"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient kickDeviceWithUserId:userId
                                         token:token
                                         resource:resource
                                       completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)kickAllDevicesWithToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *userId = params[@"userId"];
    NSString *token = params[@"token"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient kickAllDevicesWithUserId:userId
                                             token:token
                                           completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    
    return nil;
}

- (NSString *)getLoggedInDevicesFromServerWithToken:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    NSString *userId = params[@"userId"];
    NSString *token = params[@"token"];
    __weak EMBaseManager *weakSelf = self;
    [EMClient.sharedClient getLoggedInDevicesFromServerWithUserId:userId
                                                            token:token
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

- (void)autoLoginDidCompleteWithError:(EMError *)aError {
    if(aError.code == EMAppActiveNumbersReachLimitation) {
        [self onAppActiveNumberReachLimitation];
    }else if (aError.code == EMErrorUserAuthenticationFailed) {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onAuthFailed info:nil];
    }else if (aError.code == EMErrorUserNotFound) {
        [self userAccountDidRemoveFromServer];
    }
}

- (void)connectionStateDidChange:(EMConnectionState)aConnectionState {
    if (aConnectionState == EMConnectionConnected) {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onConnected info:nil];
    }else {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onDisconnected info:nil];
    }
}

- (void)onAppActiveNumberReachLimitation {
    [EMWrapperHelper.shared.listener onReceive:connectionListener method:onAppActiveNumberReachLimitation info:nil];
}

- (void)userAccountDidLoginFromOtherDevice:(NSString *)aDeviceName {
    [EMWrapperHelper.shared.listener onReceive:connectionListener method:onLoggedOtherDevice
                                          info:@{@"deviceName": aDeviceName}.toJsonString];
}

- (void)userAccountDidRemoveFromServer {
    [EMWrapperHelper.shared.listener onReceive:connectionListener method:onRemovedFromServer info:nil];
}

- (void)userDidForbidByServer {
    [EMWrapperHelper.shared.listener onReceive:connectionListener method:onForbidByServer info:nil];
}

- (void)userAccountDidForcedToLogout:(EMError *)aError {
    if (aError.code == 216) {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onChangedImPwd info:nil];
    }else if(aError.code == 214) {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onLoginTooManyDevice info:nil];
    }else if(aError.code == 217) {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onKickedByOtherDevice info:nil];
    }else if(aError.code == 202) {
        [EMWrapperHelper.shared.listener onReceive:connectionListener method:onAuthFailed info:nil];
    }
}

- (void)tokenWillExpire:(EMErrorCode)aErrorCode {
    [EMWrapperHelper.shared.listener onReceive:connectionListener method:onTokenWillExpire info:nil];
}

- (void)tokenDidExpire:(EMErrorCode)aErrorCode {
    [EMWrapperHelper.shared.listener onReceive:connectionListener method:onTokenExpired info:nil];
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
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"operation"] = @(-1);
    dict[@"convId"] = conversationId;
    dict[@"deviceId"] = deviceId;
    [EMWrapperHelper.shared.listener onReceive:multiDeviceListener method:onRoamDeleteMultiDevicesEvent info:dict.toJsonString];
}

- (void)multiDevicesConversationEvent:(EMMultiDevicesEvent)event
                       conversationId:(NSString *)conversationId
                     conversationType:(EMConversationType)conversationType
{
    NSDictionary *info = @{
        @"operation": [NSNumber numberWithInt:(int)event],
        @"convId": conversationId,
        @"type": @([EMConversation typeToInt:conversationType])
    };
    [EMWrapperHelper.shared.listener onReceive:multiDeviceListener method:onConversationMultiDevicesEvent info:info.toJsonString];
    
}

@end
