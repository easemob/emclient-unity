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


- (void)onMessageSendError:(NSString *)aCallbackId
                  userinfo:(id)jsonObject
                     error:(EMError *)aError
{
    if (aCallbackId == nil || aCallbackId.length == 0) {
        return;
    }
    
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    dict[@"type"] = @"OnMessageError";
    if (jsonObject != nil) {
        if (![jsonObject isKindOfClass:[NSString class]]) {
            jsonObject = [Transfrom NSStringFromJsonObject:jsonObject];
        }
        dict[@"value"] = jsonObject;
    }
    
    dict[@"code"] = @(aError.code);
    dict[@"desc"] = aError.errorDescription;
    
    
    UnitySendMessage(Callback_Obj, "OnSuccessValue", [Transfrom JsonObjectToCSString:dict]);
}

- (void)onSuccess:(NSString *)aType
       callbackId:(NSString *)aCallbackId
         userInfo:(id)jsonObject {
    if (aCallbackId == nil || aCallbackId.length == 0) {
        return;
    }
    
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    if (!aType && !jsonObject) {
        UnitySendMessage(Callback_Obj, "OnSuccess", [Transfrom JsonObjectToCSString:dict]);
    }else {
        dict[@"type"] = aType;
        if (jsonObject != nil) {
            if (![jsonObject isKindOfClass:[NSString class]]) {
                jsonObject = [Transfrom NSStringFromJsonObject:jsonObject];
            }
            dict[@"value"] = jsonObject;
        }
        UnitySendMessage(Callback_Obj, "OnSuccessValue", [Transfrom JsonObjectToCSString:dict]);
    }
}

- (void)onProgress:(int)progress
        callbackId:(NSString *)aCallbackId {
    if (aCallbackId == nil || aCallbackId.length == 0) {
        return;
    }
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    dict[@"progress"] = @(progress);
    UnitySendMessage(Callback_Obj, "OnProgress", [Transfrom JsonObjectToCSString:dict]);
}

- (void)onError:(NSString *)aCallbackId
          error:(EMError *)aError
{
    if (aCallbackId == nil || aCallbackId.length == 0) {
        return;
    }
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"callbackId"] = aCallbackId;
    dict[@"code"] = @(aError.code);
    dict[@"desc"] = aError.errorDescription;
    UnitySendMessage(Callback_Obj, "OnError", [Transfrom JsonObjectToCSString:dict]);
}


@end
