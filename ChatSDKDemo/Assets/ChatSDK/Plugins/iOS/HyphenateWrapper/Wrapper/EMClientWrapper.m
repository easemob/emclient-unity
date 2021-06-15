//
//  EMClientWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMClientWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMOptions+Unity.h"

@interface EMClientWrapper ()<EMClientDelegate, EMMultiDevicesDelegate>
{
    EMChatManagerWrapper *_chatManager;
    EMContactManagerWrapper *_contactManager;
    EMGroupManagerWrapper *_groupManager;
    EMRoomManagerWrapper *_roomManager;
    EMPushManagerWrapper *_pushManager;
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
        [EMClient.sharedClient addDelegate:self delegateQueue:nil];
    }
    return self;
}

- (void)initSDKWithDict:(NSDictionary *)param {
    if (!param) {
        return;
    }
    EMOptions *options = [EMOptions fromJson:param];
    [EMClient.sharedClient initializeSDKWithOptions:options];
    [EMClient.sharedClient addDelegate:self delegateQueue:nil];
    [EMClient.sharedClient addMultiDevicesDelegate:self delegateQueue:nil];
    [self registerManagers];
}

- (void)createAccount:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        return;
    }
    __block NSString *callid = callbackId;
    NSString *username = param[@"username"];
    NSString *password = param[@"password"];
    [EMClient.sharedClient registerWithUsername:username
                                       password:password
                                     completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [self onSuccess:nil callbackId:callid userInfo:nil];
        }else {
            [self onError:callid error:aError];
        }
    }];
}

- (void)registerManagers {
    _chatManager = [[EMChatManagerWrapper alloc] init];
    _contactManager = [[EMContactManagerWrapper alloc] init];
    _groupManager = [[EMGroupManagerWrapper alloc] init];
    _roomManager = [[EMRoomManagerWrapper alloc] init];
    _pushManager = [[EMPushManagerWrapper alloc] init];
}


@end
