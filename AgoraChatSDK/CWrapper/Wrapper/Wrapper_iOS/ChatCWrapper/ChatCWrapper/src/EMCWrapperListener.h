//
//  EMCWrapperListener.h
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/14.
//

#include "common_wrapper_internal.h"

#import <Foundation/Foundation.h>
#import <wrapper/wrapper.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMCWrapperListener : NSObject <EMWrapperListener>
- (instancetype)init:(void*)event;
@end

NS_ASSUME_NONNULL_END
