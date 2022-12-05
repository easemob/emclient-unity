//
//  NSString+Category.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/12/5.
//

#import "NSString+Category.h"

@implementation NSString (Category)

+ (NSString *)fromChar:(const char*)charStr {
    NSString *ret = nil;
    if (charStr != NULL) {
        ret = [NSString stringWithUTF8String:charStr];
    }
    return ret;
}

- (const char *)toChar {
    return [self UTF8String];
}

- (NSDictionary *)toDict {
    if (self == nil) {
        return nil;
    }
    NSData *jsonData = [self dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err) {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    return dic;
}

@end
