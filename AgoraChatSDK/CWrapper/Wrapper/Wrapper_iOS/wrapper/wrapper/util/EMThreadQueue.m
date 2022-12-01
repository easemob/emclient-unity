//
//  EMThreadQueue.m
//  UnityFramework
//
//  Created by 杜洁鹏 on 2022/11/22.
//

#import "EMThreadQueue.h"


#define easemob_dispatch_main_async_safe(block)\
    if ([NSThread isMainThread]) {\
        block();\
    } else {\
        dispatch_async(dispatch_get_main_queue(), block);\
    }

@implementation EMThreadQueue

+ (void)mainQueue:(void(^)(void))aAction {
    easemob_dispatch_main_async_safe(^(){
        aAction();
    });
}

@end
