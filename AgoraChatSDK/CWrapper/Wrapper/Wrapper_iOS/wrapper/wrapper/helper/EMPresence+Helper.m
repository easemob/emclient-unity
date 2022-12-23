//
//  EMPresence+Helper.m
//  im_flutter_sdk
//
//  Created by 杜洁鹏 on 2022/4/27.
//

#import "EMPresence+Helper.h"

@implementation EMPresence (Helper)

- (nonnull NSDictionary *)toJson {
    NSMutableArray *details = [NSMutableArray array];
    for (EMPresenceStatusDetail *detail in self.statusDetails) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        dict[@"device"] = detail.device;
        dict[@"status"] = @(detail.status);
        [details addObject:dict];
    }
    return @{
        @"publisher": self.publisher,
        @"detail": details,
        @"desc": self.statusDescription,
        @"lastTime": @(self.lastTime),
        @"expiryTime": @(self.expirytime)
    };
}

@end

