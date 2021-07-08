//
//  Transfrom.m
//  UnityFramework
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "Transfrom.h"
#import "EMConversation+Unity.h"


@implementation Transfrom

+ (NSString *)NSStringFromCString:(const char*)cStr {
    NSString *ret = [NSString stringWithUTF8String:cStr];
    return ret ? ret : @"";
}

+ (const char*)NSStringToCSString:(NSString *)nsStr {
    id value = nsStr;
    if (value == [NSNull null] || nsStr == nil) {
        nsStr = @"";
    }
    
    return [nsStr UTF8String];
}

+ (NSDictionary *)JsonObjectFromCSString:(const char*)cStr
{
    NSString *str = [self NSStringFromCString:cStr];
    return [self NSStringToJsonObject:str];
}

+ (const char*)JsonObjectToCSString:(NSDictionary *)aDict {
    if (aDict == nil) {
        return NULL;
    }
    NSString *str = [self NSStringFromJsonObject:aDict];
    return [self NSStringToCSString:str];
}

+ (NSString *)NSStringFromJsonObject:(id)jsonObject
{
    if (!jsonObject) {
        return nil;
    }
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonObject
                                                       options:NSJSONWritingPrettyPrinted
                                                         error:&error];
    NSString *jsonString;
    if (!jsonData || error) {
        NSLog(@"%@",error);
        return nil;
    }else{
        jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
    }

    NSMutableString *mutStr = [NSMutableString stringWithString:jsonString];
    NSRange range2 = {0,mutStr.length};
    [mutStr replaceOccurrencesOfString:@"\n" withString:@"" options:NSLiteralSearch range:range2];
    return mutStr;
}


+ (id)NSStringToJsonObject:(NSString *)jsonString
{
    if (jsonString == nil) {
        return nil;
    }
    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    id jsonObject = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err)
    {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    
    
    if ([jsonObject isKindOfClass:[NSDictionary class]]) {
        NSMutableDictionary *tmp = [[NSMutableDictionary alloc] initWithDictionary:jsonObject];
        for (NSString *str in ((NSDictionary *)jsonObject).allKeys) {
            if ([jsonObject[str] isKindOfClass:[NSNull class]]) {
                [tmp removeObjectForKey:str];
            }
        }
        jsonObject = tmp;
    }
    
    if ([jsonObject isKindOfClass:[NSArray class]]) {
        NSMutableArray *tmp = [[NSMutableArray alloc] initWithArray:jsonObject];
        for (id obj in ((NSArray *)jsonObject)) {
            if ([obj isKindOfClass:[NSArray class]]) {
                [tmp removeObject:obj];
            }
        }
        jsonObject = tmp;
    }
    
    return jsonObject;
}

@end
