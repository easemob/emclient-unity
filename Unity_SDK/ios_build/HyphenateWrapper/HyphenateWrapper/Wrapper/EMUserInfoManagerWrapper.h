//
//  EMUserInfoManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/12/3.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMUserInfoManagerWrapper : EMWrapper

- (void)updateOwnUserInfo:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)updateOwnUserInfoWithType:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchUserInfoById:(NSDictionary *)param callbackId:(NSString *)callbackId;

@end

NS_ASSUME_NONNULL_END
