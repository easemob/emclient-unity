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

@interface EMWrapper : NSObject
- (void)onSuccess:(NSString *)aType callbackId:(NSString *)aCallbackId userInfo:(NSString *)jsonObject;
- (void)onProgress:(int)progress callbackId:(NSString *)aCallbackId;
- (void)onError:(NSString *)aCallbackId error:(EMError *)aError;
@end

