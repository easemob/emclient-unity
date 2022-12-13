//
//  EMThreadQueue.h
//  UnityFramework
//
//  Created by 杜洁鹏 on 2022/11/22.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMThreadQueue : NSObject
+ (void)mainQueue:(void(^)(void))aAction;
@end

NS_ASSUME_NONNULL_END
