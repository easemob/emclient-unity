//
//  EMPageResult+Unity.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/9.
//

#import "EMPageResult+Unity.h"
#import "Transfrom.h"

@implementation EMPageResult (Unity)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    NSMutableArray *dataList = [NSMutableArray array];
    for (id obj in self.list) {
        if ([obj respondsToSelector:@selector(toJson)]) {
            [dataList addObject:[Transfrom NSStringFromJsonObject: [obj toJson]]];
        }else if ([obj isKindOfClass:[NSString class]]){
            [dataList addObject:obj];
        }
    }
   
    data[@"list"] = [Transfrom NSStringFromJsonObject:dataList];
    data[@"count"] = @(self.count);
    
    return data;
}
@end
