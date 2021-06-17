//
//  EMWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMWrapper.h"
#import "HyphenateWrapper.h"
#import "EMMethod.h"
#import "Transfrom.h"

@implementation EMWrapper
{
    
}

- (void)onSuccess:(NSString *)aType
       callbackId:(NSString *)aCallbackId
         userInfo:(NSString *)jsonStr
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    if (!aType && !jsonStr) {
        UnitySendMessage(Callback_Obj, "onSuccess", [Transfrom DictToCString:dict]);
    }else {
        dict[@"type"] = aType;
        dict[@"value"] = jsonStr ?: @"";
        UnitySendMessage(Callback_Obj, "OnSuccessValue", [Transfrom DictToCString:dict]);
    }
}

- (void)onProgress:(int)progress
        callbackId:(NSString *)aCallbackId {
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    dict[@"progress"] = @(progress);
    UnitySendMessage(Callback_Obj, "OnProgress", [Transfrom DictToCString:dict]);
}

- (void)onError:(NSString *)aCallbackId
          error:(EMError *)aError
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    dict[@"code"] = @(aError.code);
    dict[@"desc"] = aError.errorDescription;
    UnitySendMessage(Callback_Obj, "OnError", [Transfrom DictToCString:dict]);
}

@end
