//
//  NSArray+Category.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "NSArray+Category.h"

@implementation NSArray (Category)
+ (NSArray *)fromString:(NSString *)aStr {
    if (aStr == nil) {
        return nil;
    }
    
    NSData *data = [aStr dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err = nil;
    NSArray *ary = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&err];
    if(err) {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    
    return ary;
}

- (NSString *)toJsonString {
    NSError *err = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:self options:NSJSONWritingPrettyPrinted error:&err];
    if(err) {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    NSString *jsonTemp = [jsonString stringByReplacingOccurrencesOfString:@"\n" withString:@""];
    return jsonTemp;
}
@end
