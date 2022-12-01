//
//  ios_gloable.cpp
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/16.
//


#import <wrapper/wrapper.h>

#include <stdio.h>
#include <string>
#include "ios_gloable.h"
#include "common_wrapper_internal.h"


#import "NSString+Category.h"
#import "NSDictionary+Category.h"
#import "EMCWrapper.h"
#import "EMCWrapperListener.h"

using namespace wrapper_ios;

static EMCWrapper *cwrapper;
namespace wrapper_ios {

    void init_common(int sdkType, void* listener) {
        cwrapper = [[EMCWrapper alloc] initWithType:sdkType listener:listener];
    }

    void uninit_common() {
        cwrapper = nil;
    }

    const char* get_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
    {
        EMWrapperCallback *callback = [[EMWrapperCallback alloc] init];
        callback.onSuccessCallback = ^(NSString *valueStr) {
            NSMutableDictionary *dict = [NSMutableDictionary dictionary];
            dict[@"callbackId"] = [NSString fromChar:cbid];
            dict[@"value"] = valueStr;
            [EMWrapperHelper.shared.listener onReceive:@"callback" method:[NSString fromChar:cbid] info:[dict toString]];
        };
        callback.onErrorCallback = ^(NSString *errorStr) {
            NSMutableDictionary *dict = [NSMutableDictionary dictionary];
            dict[@"callbackId"] = [NSString fromChar:cbid];
            dict[@"error"] = errorStr;
            [EMWrapperHelper.shared.listener onReceive:@"callback" method:[NSString fromChar:cbid] info:[dict toString]];
        };
        callback.onProgressCallback = ^(int progress) {
            NSMutableDictionary *dict = [NSMutableDictionary dictionary];
            dict[@"callbackId"] = [NSString fromChar:cbid];
            dict[@"progress"] = @(progress);
            [EMWrapperHelper.shared.listener onReceive:@"callbackProgress" method:[NSString fromChar:cbid] info:[dict toString]];
        };
        
        NSString *str = [cwrapper nativeGet:manager method:method cid:jstr];
        
        if(str == nil) str = @"";
        std::string ret = str.toChar;
        char* buf = new char[ret.size() + 1];
        memcpy(buf, ret.c_str(), ret.size() + 1);
        buf[ret.size()] = '\0';
        return buf;
    }

    void call_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
    {
        EMWrapperCallback *callback = [[EMWrapperCallback alloc] init];
        callback.onSuccessCallback = ^(NSString *valueStr) {};
        callback.onErrorCallback = ^(NSString *errorStr) {};
        callback.onProgressCallback = ^(int progress) {};
        [cwrapper.wrapper callSDKApi:[NSString fromChar:manager]
                              method:[NSString fromChar:method]
                              params:[NSString fromChar:jstr].toDict
                            callback:callback];
        
    }
}
