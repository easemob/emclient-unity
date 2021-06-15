//
//  EMPushManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMPushManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMPushOptions+Unity.h"
#import "Transfrom.h"

@implementation EMPushManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        
    }
    return self;
}

- (void)getNoDisturbGroups:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    NSArray *groupIds = [EMClient.sharedClient.pushManager noPushGroups];
    [self onSuccess:@"List<String>" callbackId:callId userInfo:[Transfrom ArrayToNSString:groupIds]];
}

- (void)getPushConfig:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.pushManager getPushNotificationOptionsFromServerWithCompletion:^(EMPushOptions *aOptions,
                                                                                            EMError *aError)
     {
        if (aError) {
            [weakSelf onError:callId error:aError];
        }else {
            [weakSelf onSuccess:@"EMPushConfigs" callbackId:callId userInfo:[Transfrom DictToNSString:[aOptions toJson]]];
        }
    }];
}

- (void)getPushConfigFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId = callbackId;
    EMError *aError = nil;
    EMPushOptions *aOptions = [EMClient.sharedClient.pushManager getPushOptionsFromServerWithError:&aError];
    if (aError) {
        [weakSelf onError:callId error:aError];
    }else {
        [weakSelf onSuccess:@"EMPushConfigs" callbackId:callId userInfo:[Transfrom DictToNSString:[aOptions toJson]]];
    }
}

- (void)updateGroupPushService:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId = callbackId;
    NSString *groupId = param[@"groupId"];
    BOOL noDisturb = [param[@"noDisturb"] boolValue];
    [EMClient.sharedClient.pushManager updatePushServiceForGroups:@[groupId] disablePush:noDisturb
                                                       completion:^(EMError * aError)
     {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)PushNoDisturb:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    __block BOOL noDisturb = [param[@"noDisturb"] boolValue];
    int startTime = [param[@"startTime"] intValue];
    int endTime = [param[@"endTime"] intValue];
    __block EMError *aError = nil;
    __block void(^block)(EMError *) = ^(EMError *aError) {
        easemob_dispatch_main_async_safe(^{
            if (!aError) {
                [self onSuccess:nil callbackId:callId userInfo:nil];
            }else {
                [self onError:callId error:aError];
            }
        });
    };
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        if (noDisturb) {
            aError = [EMClient.sharedClient.pushManager disableOfflinePushStart:startTime end:endTime];
        }else {
            aError = [EMClient.sharedClient.pushManager enableOfflinePush];
        }
        block(aError);
    });
}

- (void)updatePushStyle:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId = callbackId;
    int intStyle = [param[@"style"] intValue];
    EMPushDisplayStyle style = intStyle == 0 ? EMPushDisplayStyleSimpleBanner : EMPushDisplayStyleMessageSummary;
    [EMClient.sharedClient.pushManager updatePushDisplayStyle:style completion:^(EMError * aError) {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)updatePushNickname:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId = callbackId;
    NSString *nickname = param[@"nickname"];
    [EMClient.sharedClient.pushManager updatePushDisplayName:nickname completion:^(NSString *aDisplayName, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

@end
