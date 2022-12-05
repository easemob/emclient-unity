//
//  NSDictionary+Category.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface NSDictionary (Category)
+ (NSDictionary *)fromString:(NSString *)aStr;
- (NSString *)toJsonString;
@end

NS_ASSUME_NONNULL_END
