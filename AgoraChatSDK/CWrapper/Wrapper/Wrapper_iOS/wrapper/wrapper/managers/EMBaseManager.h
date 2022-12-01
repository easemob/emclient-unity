//
//  EMBaseManager.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/11.
//

#import <Foundation/Foundation.h>
#import <HyphenateChat/HyphenateChat.h>
#import "EMSDKMethod.h"
#import "EMWrapperCallback.h"
#import "EMWrapperHelper.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMBaseManager : NSObject
- (NSString *)onMethodCall:(NSString *)method
                    params:(nullable NSDictionary *)params
                  callback:(nullable EMWrapperCallback *)callback;


- (void)wrapperCallback:(EMWrapperCallback *)callback
                  error:(nullable EMError *)aErr object:(nullable NSObject *)aObj;


@end

NS_ASSUME_NONNULL_END
