//
//  EMWrapperCallback.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMWrapperCallback.h"
#import "EMThreadQueue.h"
#import "EMUtil.h"
#import "EMError+Helper.h"

@implementation EMWrapperCallback

- (void)onSuccess:(NSObject *)aObj {
    __weak EMWrapperCallback *weakSelf = self;
    NSString *str = nil;
    if ([aObj isKindOfClass:[NSDictionary class]]) {
        str = [(NSDictionary *)aObj toJsonString];
    }else if ([aObj isKindOfClass:[NSArray class]]){
        str = [(NSArray *)aObj toJsonString];
    }else {
        str = (NSString *)aObj;
    }
    [weakSelf runInQueue:^{
        weakSelf.onSuccessCallback(str);
    }];
}

- (void)onError:(NSObject *)aError {
    EMError *err = (EMError *)aError;
    __weak EMWrapperCallback *weakSelf = self;
    [weakSelf runInQueue:^{
        weakSelf.onErrorCallback([err toJson]);
    }];
}

- (void)onProgress:(int)aProgress{
    __weak EMWrapperCallback *weakSelf = self;
    [weakSelf runInQueue:^{
        weakSelf.onProgressCallback(aProgress);
    }];
}

- (void)runInQueue:(void(^)(void))aciton {
    [EMThreadQueue mainQueue:aciton];
}

@end
