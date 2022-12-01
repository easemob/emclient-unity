//
//  NSDictionary+Category.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "NSDictionary+Category.h"

@implementation NSDictionary (Category)
+ (NSDictionary *)fromString:(NSString *)aStr {
    if (aStr == nil) {
        return nil;
    }
    NSData *jsonData = [aStr dataUsingEncoding:NSUTF8StringEncoding];
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

- (NSString *)toJsonString {
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:self options:0 error:0];
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

@end
