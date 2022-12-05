//
//  NSString+Category.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/12/5.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface NSString (Category)

+ (NSString *)fromChar:(const char*)charStr;
- (const char *)toChar;
- (NSDictionary *)toDict;
@end

NS_ASSUME_NONNULL_END
