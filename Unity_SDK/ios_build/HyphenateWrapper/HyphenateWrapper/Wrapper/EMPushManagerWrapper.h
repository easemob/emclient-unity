//
//  EMPushManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMPushManagerWrapper : EMWrapper
- (id)getNoDisturbGroups:(NSDictionary *)param;
- (id)getPushConfig:(NSDictionary *)param;
- (void)getPushConfigFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)updateGroupPushService:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)PushNoDisturb:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)updatePushStyle:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)updatePushNickname:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

NS_ASSUME_NONNULL_END
