//
//  Transfrom.m
//  UnityFramework
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "Transfrom.h"

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

+ (NSDictionary *)DictFromCString:(const char*)cStr
{
    NSString *str = [self NSStringFromCString:cStr];
    return [self DictFromNSString:str];
}

+ (const char*)DictToCString:(NSDictionary *)aDict {
    NSString *str = [self DictToNSString:aDict];
    return [self NSStringToCSString:str];
}

+ (NSDictionary *)DictFromNSString:(NSString*)nsStr {
    if (nsStr == nil) {
        return nil;
    }
    NSData *jsonData = [nsStr dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err)
    {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    return dic;
}

+ (NSString*)DictToNSString:(NSDictionary *)aDict {
    if (!aDict) {
        return nil;
    }
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:aDict
                                                       options:NSJSONWritingPrettyPrinted
                                                         error:&error];
    NSString *jsonString;
    if (!jsonData) {
        NSLog(@"%@",error);
    }else{
        jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
    }

    NSMutableString *mutStr = [NSMutableString stringWithString:jsonString];
//    NSRange range = {0,jsonString.length};
//    [mutStr replaceOccurrencesOfString:@" " withString:@"" options:NSLiteralSearch range:range];
    NSRange range2 = {0,mutStr.length};
    [mutStr replaceOccurrencesOfString:@"\n" withString:@"" options:NSLiteralSearch range:range2];
    return mutStr;
}


+ (NSArray *)ArrayFromNSString:(NSString *)nsStr {
    if (nsStr == nil) {
        return nil;
    }
    NSData *jsonData = [nsStr dataUsingEncoding:NSUTF8StringEncoding];
    NSError *err;
    NSArray<NSString*>* jsonAry = [NSJSONSerialization JSONObjectWithData:jsonData
                                                        options:NSJSONReadingMutableContainers
                                                          error:&err];
    if(err)
    {
        NSLog(@"json解析失败：%@",err);
        return nil;
    }
    return jsonAry;
}

+ (NSString *)ArrayToNSString:(NSArray<NSString*>*)strAry {
    if (!strAry) {
        return nil;
    }
    NSError *error = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:strAry options:NSJSONWritingPrettyPrinted error:&error];
    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    NSString *jsonTemp = [jsonString stringByReplacingOccurrencesOfString:@"\n" withString:@""];
    //    NSString *jsonResult = [jsonTemp stringByReplacingOccurrencesOfString:@" " withString:@""];
    return jsonTemp;
}

+ (NSArray *)ArrayFromCString:(const char*)cStr {
    return [self ArrayFromNSString: [self NSStringFromCString:cStr]];
}

+ (const char*)ArrayToCString:(NSArray<NSString*>*)strAry {
    return [self NSStringToCSString: [self ArrayToNSString:strAry]];
}

@end
