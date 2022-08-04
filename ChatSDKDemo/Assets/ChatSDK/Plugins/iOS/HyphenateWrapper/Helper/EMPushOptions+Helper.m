//
//  EMPushOptions+Helper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import "EMPushOptions+Helper.h"

@implementation EMPushOptions (Unity)
- (NSDictionary *)toJson{
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"noDisturb"] = @(self.isNoDisturbEnable);
    data[@"pushStyle"] = @(self.displayStyle != EMPushDisplayStyleSimpleBanner);
    data[@"noDisturbStartHour"] = @(self.noDisturbingStartH);
    data[@"noDisturbEndHour"] = @(self.noDisturbingEndH);
    return data;
}
@end
