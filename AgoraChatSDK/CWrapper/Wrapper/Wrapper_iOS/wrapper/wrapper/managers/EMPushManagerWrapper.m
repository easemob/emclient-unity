//
//  EMPushManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMPushManagerWrapper.h"
#import "EMPushOptions+Helper.h"
#import "EMConversation+Helper.h"
#import "EMGroup+Helper.h"
#import "EMSilentModeParam+Helper.h"
#import "EMSilentModeResult+Helper.h"
#import "EMHelper.h"
#import "EMUtil.h"

@implementation EMPushManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {
    NSString *ret = nil;
    if ([getImPushConfig isEqualToString:method]) {
        ret = [self getImPushConfig:params callback:callback];
    }
    else if([getImPushConfigFromServer isEqualToString:method]){
        ret = [self getImPushConfigFromServer:params callback:callback];
    }
    else if([updatePushNickname isEqualToString:method]){
        ret = [self updatePushNickname:params callback:callback];
    }
    else if([updateImPushStyle isEqualToString:method]){
        ret = [self updateImPushStyle:params callback:callback];
    }
    else if ([reportPushAction isEqualToString:method]) {
        ret = [self reportPushAction:params callback:callback];
    }
    else if ([setConversationSilentMode isEqualToString:method]) {
        ret = [self setConversationSilentMode:params callback:callback];
    }
    else if ([removeConversationSilentMode isEqualToString:method]) {
        ret = [self removeConversationSilentMode:params callback:callback];
    }
    else if ([fetchConversationSilentMode isEqualToString:method]) {
        ret = [self fetchConversationSilentMode:params callback:callback];
    }
    else if ([setSilentModeForAll isEqualToString:method]) {
        ret = [self setSilentModeForAll:params callback:callback];
    }
    else if ([fetchSilentModeForAll isEqualToString:method]) {
        ret = [self fetchSilentModeForAll:params callback:callback];
    }
    else if ([fetchSilentModeForConversations isEqualToString:method]) {
        ret = [self fetchSilentModeForConversations:params callback:callback];
    }
    else if ([setPreferredNotificationLanguage isEqualToString:method]) {
        ret = [self setPreferredNotificationLanguage:params callback:callback];
    }
    else if ([fetchPreferredNotificationLanguage isEqualToString:method]) {
        ret = [self fetchPreferredNotificationLanguage:params callback:callback];
    }
    else if ([getPushTemplate isEqualToString:method]) {
        ret = [self getPushTemplate:params callback:callback];
    }
    else if ([setPushTemplate isEqualToString:method]) {
        ret = [self setPushTemplate:params callback:callback];
    }else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}


- (NSString *)getImPushConfig:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMPushOptions *options = EMClient.sharedClient.pushManager.pushOptions;
    return [[EMHelper getReturnJsonObject:[options toJson]] toJsonString];
}

- (NSString *)getImPushConfigFromServer:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.pushManager getPushNotificationOptionsFromServerWithCompletion:^(EMPushOptions *aOptions, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aOptions toJson]];
    }];
    return nil;
}

- (NSString *)updatePushNickname:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSString *nickname = params[@"nickname"];
    [EMClient.sharedClient.pushManager updatePushDisplayName:nickname
                                                  completion:^(NSString * _Nonnull aDisplayName, EMError * _Nonnull aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aDisplayName];
    }];
    return nil;
}

- (NSString *)updateImPushStyle:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    EMPushDisplayStyle pushStyle = [params[@"pushStyle"] intValue];
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        EMError *aError = [EMClient.sharedClient.pushManager updatePushDisplayStyle:pushStyle];
        dispatch_async(dispatch_get_main_queue(), ^{
            [weakSelf wrapperCallback:callback error:aError object:nil];
        });
    });
    return nil;
}

- (NSString *)reportPushAction:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    // TODO： no support
    return nil;
}

- (NSString *)setConversationSilentMode:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSString *conversaionId = params[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[params[@"convType"] intValue]];
    EMSilentModeParam *silmentParam = [EMSilentModeParam formJson:params[@"param"]];
    [EMClient.sharedClient.pushManager setSilentModeForConversation:conversaionId
                                                   conversationType:type
                                                             params:silmentParam
                                                         completion:^(EMSilentModeResult * _Nullable aResult, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)removeConversationSilentMode:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSString *conversaionId = params[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[params[@"convType"] intValue]];
    [EMClient.sharedClient.pushManager clearRemindTypeForConversation:conversaionId
                                                     conversationType:type
                                                           completion:^(EMSilentModeResult * _Nullable aResult, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)fetchConversationSilentMode:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSString *conversaionId = params[@"convId"];
    EMConversationType type = [EMConversation typeFromInt:[params[@"convType"] intValue]];
    [EMClient.sharedClient.pushManager getSilentModeForConversation:conversaionId
                                                   conversationType:type
                                                         completion:^(EMSilentModeResult * _Nullable aResult, EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)setSilentModeForAll:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    EMSilentModeParam *silmentParam = [EMSilentModeParam formJson:params[@"param"]];
    [EMClient.sharedClient.pushManager setSilentModeForAll:silmentParam
                                                completion:^(EMSilentModeResult * _Nullable aResult, EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)fetchSilentModeForAll:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.pushManager getSilentModeForAllWithCompletion:^(EMSilentModeResult * _Nullable aResult, EMError * _Nullable aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];
    return nil;
}

- (NSString *)fetchSilentModeForConversations:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSMutableArray *conversations = [NSMutableArray array];
    for (NSString *conversaitonId in params.allKeys) {
        EMConversationType type = [EMConversation typeFromInt:[params[conversaitonId] intValue]];
        EMConversation *conversation = [EMClient.sharedClient.chatManager getConversation:conversaitonId type:type createIfNotExist:YES];
        [conversations addObject:conversation];
    }
    
    
    [EMClient.sharedClient.pushManager getSilentModeForConversations:conversations
                                                          completion:^(NSDictionary<NSString *,EMSilentModeResult *> * _Nullable aResult, EMError * _Nullable aError)
     {
        
        NSMutableDictionary *tmpDict = [NSMutableDictionary dictionary];
        for (NSString *conversationId in aResult.allKeys) {
            tmpDict[conversationId] = [aResult[conversationId] toJson];
        }
        
        [weakSelf wrapperCallback:callback error:aError object:tmpDict];
    }];
    return nil;
}

- (NSString *)setPreferredNotificationLanguage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSString *code = params[@"code"];
    [EMClient.sharedClient.pushManager setPreferredNotificationLanguage:code completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)fetchPreferredNotificationLanguage:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.pushManager getPreferredNotificationLanguageCompletion:^(NSString * _Nullable aLaguangeCode, EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:aLaguangeCode];
    }];
    return nil;
}

- (NSString *)getPushTemplate:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    NSString *pushTemplateName = params[@"pushTemplateName"];
    [EMClient.sharedClient.pushManager setPushTemplate:pushTemplateName completion:^(EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)setPushTemplate:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    __weak EMPushManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.pushManager getPushTemplate:^(NSString * _Nullable aPushTemplateName, EMError * _Nullable aError) {
        [weakSelf wrapperCallback:callback error:aError object:aPushTemplateName];
    }];
    return nil;
}

- (void)registerEaseListener{
    
}

@end
