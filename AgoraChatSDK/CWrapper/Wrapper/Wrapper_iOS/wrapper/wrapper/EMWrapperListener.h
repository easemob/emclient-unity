//
//  EMWrapperListener.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/14.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol EMWrapperListener <NSObject>

- (void)onReceive:(NSString *)listener method:(nullable NSString *)method info:(nullable NSString *)jsonInfo;

@end

NS_ASSUME_NONNULL_END
