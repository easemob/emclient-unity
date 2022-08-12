//
//  EMCursorResult+Helper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import "EMCursorResult+Helper.h"
#import "Transfrom.h"

@implementation EMCursorResult (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    NSMutableArray *dataList = [NSMutableArray array];
    
    for (id obj in self.list) {
        if ([obj respondsToSelector:@selector(toJson)]) {
            [dataList addObject:[obj toJson]];
        }else if ([obj isKindOfClass:[NSString class]]){
            [dataList addObject:obj];
        }
    }
    
    data[@"list"] = [Transfrom NSStringFromJsonObject:dataList];
    data[@"cursor"] = self.cursor;
    
    return data;
}

- (NSDictionary *)toJsonString {
    
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    NSMutableArray *dataList = [NSMutableArray array];
    
    for (id obj in self.list) {
        if ([obj respondsToSelector:@selector(toJson)]) {
            [dataList addObject:[Transfrom NSStringFromJsonObject:[obj toJson]]];
        }else if ([obj isKindOfClass:[NSString class]]){
            [dataList addObject:obj];
        }
    }
    
    data[@"list"] = [Transfrom NSStringFromJsonObject:dataList];
    data[@"cursor"] = self.cursor;
    
    return data;
}
@end
