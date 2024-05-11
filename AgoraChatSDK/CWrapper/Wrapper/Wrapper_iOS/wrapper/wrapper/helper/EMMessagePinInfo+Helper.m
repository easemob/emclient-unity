//
//  EMMessagePinInfo+Helper.m
//  wrapper
//
//  Created by yuqiang on 2024/4/7.
//

#include "EMMessagePinInfo+Helper.h"

@implementation EMMessagePinInfo (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"pinnedBy"] = self.operatorId;
    data[@"pinnedAt"] = @(self.pinTime);
    return data;
}
@end
