//
//  EMHelper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMHelper.h"
#import "EMUtil.h"

@implementation EMHelper
+ (NSString *)getReturnJsonObject:(NSObject *)aStr {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    if (aStr) {
        ret[@"ret"] = aStr;
    }
    
    return ret.toJsonString;
}

@end
