//
//  Transfrom.h
//  UnityFramework
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface Transfrom : NSObject

+ (NSString *)NSStringFromCString:(const char*)cStr;
+ (const char*)NSStringToCSString:(NSString *)nsStr;

+ (NSDictionary *)DictFromCString:(const char*)cStr;
+ (const char*)DictToCString:(NSDictionary *)aDict;

+ (NSDictionary *)DictFromNSString:(NSString*)nsStr;
+ (NSString*)DictToNSString:(NSDictionary *)aDict;
@end

NS_ASSUME_NONNULL_END
