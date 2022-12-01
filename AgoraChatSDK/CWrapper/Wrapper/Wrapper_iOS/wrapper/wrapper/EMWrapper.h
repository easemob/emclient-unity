//
//  EMWrapper.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/11.
//

#import <Foundation/Foundation.h>
#import <wrapper/EMWrapperCallback.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMWrapper : NSObject
- (NSString *)callSDKApi:(NSString *)manager
                  method:(NSString *)method
                  params:(NSDictionary *)params
                callback:(EMWrapperCallback *)callback;
@end

NS_ASSUME_NONNULL_END
