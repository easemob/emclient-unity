//
//  EMWrapperHelper.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/14.
//

#import <Foundation/Foundation.h>
#import "EMWrapperListener.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMWrapperHelper : NSObject

+ (EMWrapperHelper *)shared;

@property (nonatomic, strong) id<EMWrapperListener> listener;
@end

NS_ASSUME_NONNULL_END
