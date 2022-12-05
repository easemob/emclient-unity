//
//  EMCWrapperListener.m
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/14.
//

#import "EMCWrapperListener.h"
#import <wrapper/wrapper.h>
#import "NSString+Category.h"

@interface EMCWrapperListener ()
{
    NativeListenerEvent _listener;
}

@end

@implementation EMCWrapperListener

- (instancetype)init:(void *)event {
    if (self = [super init]) {
        _listener = event;
    }
    return self;
}

- (void)onReceive:(nonnull NSString *)listener method:(nullable NSString *)method info:(nullable NSString *)jsonInfo {
    if (jsonInfo == nil) {
        _listener(listener.toChar, method.toChar, NULL);
    }else {
        _listener(listener.toChar, method.toChar, jsonInfo.toChar);
    }
}

@end
