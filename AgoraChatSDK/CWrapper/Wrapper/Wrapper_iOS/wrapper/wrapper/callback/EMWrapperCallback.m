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

- (void)onSuccess:(NSString *)str {
    __weak EMWrapperCallback *weakSelf = self;
    [weakSelf runInQueue:^{
        weakSelf.onSuccessCallback(str);
    }];
}

- (void)onError:(NSDictionary *)errDict {
    __weak EMWrapperCallback *weakSelf = self;
    [weakSelf runInQueue:^{
        weakSelf.onErrorCallback(errDict);
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
