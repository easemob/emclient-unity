//
//  EMContact+Helper.m
//  wrapper
//
//  Created by yuqiang on 2024/2/22.
//

#include "EMContact+Helper.h"

@implementation EMContact (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"userId"] = self.userId;
    data[@"remark"] = self.remark;
    return data;
}
@end
