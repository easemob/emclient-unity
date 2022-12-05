//
//  EMBaseManager.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/11.
//

#import "EMBaseManager.h"
#import "EMError+Helper.h"
#import "EMThreadQueue.h"

@implementation EMBaseManager

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {

    return nil;
}

- (void)wrapperCallback:(EMWrapperCallback *)callback
                  error:(EMError *)aErr
                 object:(NSObject *)aObj {
    [EMThreadQueue mainQueue:^{
            if (aErr) {
                callback.onErrorCallback([aErr toJson]);
            }else {
                callback.onSuccessCallback(aObj);
            }
    }];
}

@end
