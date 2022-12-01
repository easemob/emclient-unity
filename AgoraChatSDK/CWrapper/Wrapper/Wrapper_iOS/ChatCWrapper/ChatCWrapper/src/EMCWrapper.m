//
//  EMCWrapper.m
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/16.
//

#import "EMCWrapper.h"
#import <wrapper/wrapper.h>
#import "EMCWrapperListener.h"

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
    NSString *ret = nil;
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
    
    [self.wrapper callSDKApi:[NSString fromChar:manager]
                                  method:[NSString fromChar:method]
                                  params:[NSString fromChar:jstr].toDict
                                callback:callback];
}
@end
