//
//  EMClientWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMClientWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMOptions+Helper.h"
#import "EMClientListener.h"

@interface EMClientWrapper ()
{
//    EMChatManagerWrapper *_chatManager;
//    EMContactManagerWrapper *_contactManager;
//    EMGroupManagerWrapper *_groupManager;
//    EMRoomManagerWrapper *_roomManager;
//    EMPushManagerWrapper *_pushManager;
//    EMConversationWrapper *_conversationWrapper;
//    EMUserInfoManagerWrapper *_userInfoManager;
//
    
    EMClientListener *_listener;
}
@end
static EMClientWrapper *_instance;
@implementation EMClientWrapper

+ (EMClientWrapper *)instance {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[EMClientWrapper alloc] init];
    });
    return _instance;
}

- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMClientListener alloc] init];
    }
    return self;
}

- (void)initWithOptions:(NSDictionary *)param {
    if (!param) {
        return;
    }
    EMOptions *options =  [EMOptions fromJson:param];
    [EMClient.sharedClient initializeSDKWithOptions:options];
    [EMClient.sharedClient addDelegate:_listener delegateQueue:nil];
    [EMClient.sharedClient addMultiDevicesDelegate:_listener delegateQueue:nil];
    [self registerManagers];
}

- (void)createAccount:(NSDictionary *)param callbackId:(NSString *)callbackId {
    EMError *error = nil;
    NSString *username = param[@"username"];
    NSString *password = param[@"password"];
    
    if (!username) {
        error = [EMError errorWithDescription:@"username is invalid" code:EMErrorInvalidUsername];
        [self onError:callbackId error:error];
        return;
    }
    
    if (!password) {
        error = [EMError errorWithDescription:@"password is invalid" code:EMErrorInvalidUsername];
        [self onError:callbackId error:error];
        return;
    }
    
    __weak EMClientWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient registerWithUsername:username
                                       password:password
                                     completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)login:(NSDictionary *)param callbackId:(NSString *)callbackId {
    EMError *error = nil;
    NSString *username = param[@"username"];
    NSString *pwdOrToken = param[@"pwdOrToken"];
    BOOL isToken = [param[@"isToken"] boolValue];
    
    if (!username) {
        error = [EMError errorWithDescription:@"username is invalid" code:EMErrorInvalidUsername];
        [self onError:callbackId error:error];
        return;
    }
    
    if (!pwdOrToken) {
        error = [EMError errorWithDescription:@"password or token is invalid" code:EMErrorInvalidUsername];
        [self onError:callbackId error:error];
        return;
    }
    
    __block NSString *callId = callbackId;
    __weak EMClientWrapper * weakSelf = self;
    __block void (^block)(EMError *) = ^(EMError *error) {
        if (!error) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:error];
        }
    };
    
    if (isToken) {
        [EMClient.sharedClient loginWithUsername:username
                                           token:pwdOrToken
                                      completion:^(NSString *aUsername, EMError *aError)
        {
            block(aError);
        }];
    }else {
        [EMClient.sharedClient loginWithUsername:username
                                        password:pwdOrToken
                                      completion:^(NSString *aUsername, EMError *aError)
        {
            block(aError);
        }];
    }
}

- (void)logout:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __weak EMClientWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    BOOL unbindDeviceToken = [param[@"unbindDeviceToken"] boolValue];
    [EMClient.sharedClient logout:unbindDeviceToken completion:^(EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)loginWithAgoraToken:(NSDictionary *)param
                 callbackId:(NSString *)callbackId {
    __weak EMClientWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    NSString *username = param[@"username"];
    NSString *agoraToken = param[@"token"];
    [EMClient.sharedClient loginWithUsername:username
                                  agoraToken:agoraToken
                                  completion:^(NSString *aUsername, EMError *aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}
- (void)renewToken:(NSDictionary *)param
        callbackId:(NSString *)callbackId {
    __weak EMClientWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    NSString *token = param[@"token"];
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        EMError *aError = [EMClient.sharedClient renewToken:token];
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    });
    
}

- (void)registerManagers {
    _chatManager = [[EMChatManagerWrapper alloc] init];
    _contactManager = [[EMContactManagerWrapper alloc] init];
    _groupManager = [[EMGroupManagerWrapper alloc] init];
    _roomManager = [[EMRoomManagerWrapper alloc] init];
    _pushManager = [[EMPushManagerWrapper alloc] init];
    _conversationWrapper = [[EMConversationWrapper alloc] init];
    _userInfoManager = [[EMUserInfoManagerWrapper alloc] init];
    _presenceManager = [[EMPresenceManagerWrapper  alloc] init];
    _chatThreadManager = [[EMThreadManagerWrapper alloc] init];
}


@end
