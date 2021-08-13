//
//  EMWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import <HyphenateChat/HyphenateChat.h>

#define easemob_dispatch_main_async_safe(block)\
if ([NSThread isMainThread]) {\
    block();\
} else {\
    dispatch_async(dispatch_get_main_queue(), block);\
}

NS_ASSUME_NONNULL_BEGIN

@interface EMWrapper : NSObject
- (void)onSuccess:(nullable NSString *)aType callbackId:(nullable NSString *)aCallbackId userInfo:(nullable id)jsonObject;
- (void)onProgress:(int)progress callbackId:(nullable NSString *)aCallbackId;
- (void)onError:(nullable NSString *)aCallbackId error:(nullable EMError *)aError;
@end

NS_ASSUME_NONNULL_END

