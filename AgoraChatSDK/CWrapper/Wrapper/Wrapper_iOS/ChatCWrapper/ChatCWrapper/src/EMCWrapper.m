//
//  EMCWrapper.m
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/16.
//

#import "EMCWrapper.h"
#import <wrapper/wrapper.h>
#import "EMCWrapperListener.h"
#import "NSString+Category.h"

@interface EMCWrapper ()
@property (nonatomic, strong) EMWrapper *wrapper;
@end

@implementation EMCWrapper
{
    void* callback;
    int iType;
}

- (instancetype)initWithType:(int)iType
                    listener:(void*)listener {
    if (self = [super init]) {
        self.wrapper = [[EMWrapper alloc] init];
        EMWrapperHelper.shared.listener = [[EMCWrapperListener alloc] init:listener];
    }
    return self;
}

- (NSString *)nativeGet:(const char *)manager
                 method:(const char *)method
                 params:(const char*)jstr
                    cid:(const char *)cbid {
    EMWrapperCallback *callback = [[EMWrapperCallback alloc] init];
    __block NSString *callbackId = [NSString fromChar:cbid];
    callback.onSuccessCallback = ^(NSObject *valueObj) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        if (valueObj != nil) {
            dict[@"value"] = valueObj;
        }
        dict[@"callbackId"] = callbackId;
        [EMWrapperHelper.shared.listener onReceive:@"callback" method:callbackId info:[dict toJsonString]];
    };
    callback.onErrorCallback = ^(NSDictionary *errorDict) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        dict[@"callbackId"] = [NSString fromChar:cbid];
        dict[@"error"] = errorDict;
        [EMWrapperHelper.shared.listener onReceive:@"callback" method:callbackId info:[dict toJsonString]];
    };
    callback.onProgressCallback = ^(int progress) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        dict[@"callbackId"] = [NSString fromChar:cbid];
        dict[@"progress"] = @(progress);
        [EMWrapperHelper.shared.listener onReceive:@"callbackProgress" method:callbackId info:[dict toJsonString]];
    };
    
    NSString *ret = [self.wrapper callSDKApi:[NSString fromChar:manager]
                                      method:[NSString fromChar:method]
                                      params:[NSString fromChar:jstr].toDict
                                    callback:callback];
    return ret;
}

- (void)nativeCall:(const char *)manager
            method:(const char *)method
            params:(const char*)jstr
               cid:(const char *)cbid
{
    __block NSString *callbackId = [NSString fromChar:cbid];
    EMWrapperCallback *callback = [[EMWrapperCallback alloc] init];
    callback.onSuccessCallback = ^(NSObject *valueObj) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        if (valueObj != nil) {
            if ([valueObj isKindOfClass:[NSDictionary class]]) {
                dict[@"value"] = [(NSDictionary *)valueObj toJsonString];
            }else if ([valueObj isKindOfClass:[NSArray class]]) {
                dict[@"value"] = [(NSArray *)valueObj toJsonString];
            }
        }
        dict[@"callbackId"] = callbackId;
        [EMWrapperHelper.shared.listener onReceive:@"callback" method:callbackId info:[dict toJsonString]];
    };
    callback.onErrorCallback = ^(NSDictionary *errorDict) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        dict[@"callbackId"] = [NSString fromChar:cbid];
        dict[@"error"] = errorDict;
        [EMWrapperHelper.shared.listener onReceive:@"callback" method:callbackId info:[dict toJsonString]];
    };
    callback.onProgressCallback = ^(int progress) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        dict[@"callbackId"] = [NSString fromChar:cbid];
        dict[@"progress"] = @(progress);
        [EMWrapperHelper.shared.listener onReceive:@"callbackProgress" method:callbackId info:[dict toJsonString]];
    };
    
    [self.wrapper callSDKApi:[NSString fromChar:manager]
                                  method:[NSString fromChar:method]
                                  params:[NSString fromChar:jstr].toDict
                                callback:callback];
}
@end
