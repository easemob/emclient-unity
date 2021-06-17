//
//  EMWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMWrapper : NSObject
- (void)onSuccess:(NSString *)aType callbackId:(NSString *)aCallbackId userInfo:(NSString *)jsonStr;
- (void)onError:(NSString *)aCallbackId error:(EMError *)aError;
@end

NS_ASSUME_NONNULL_END
