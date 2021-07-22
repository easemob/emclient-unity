//
//  Transfrom.h
//  UnityFramework
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface Transfrom : NSObject

// const char -> NSString
+ (NSString *)NSStringFromCString:(const char*)cStr;

// NSString -> const char
+ (const char*)NSStringToCSString:(NSString *)nsStr;

// const char -> JsonObject
+ (id)JsonObjectFromCSString:(const char*)cStr;

// JsonObject -> const char
+ (const char*)JsonObjectToCSString:(id)jsonObject;

// JsonObject -> NSString
+ (NSString *)NSStringFromJsonObject:(id)jsonObject;

// NSString -> JsonObject
+ (id)NSStringToJsonObject:(NSString *)jsonString;


@end

NS_ASSUME_NONNULL_END
