//
//  DelegateTester.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/12/28.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface DelegateTester : NSObject
+ (DelegateTester *)shared;
- (void)startTest;
@end

NS_ASSUME_NONNULL_END
