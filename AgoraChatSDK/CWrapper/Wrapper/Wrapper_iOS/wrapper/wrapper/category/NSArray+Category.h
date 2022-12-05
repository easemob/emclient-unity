//
//  NSArray+Category.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface NSArray (Category)
+ (NSArray *)fromString:(NSString *)aStr;
- (NSString*)toJsonString;

@end

NS_ASSUME_NONNULL_END
