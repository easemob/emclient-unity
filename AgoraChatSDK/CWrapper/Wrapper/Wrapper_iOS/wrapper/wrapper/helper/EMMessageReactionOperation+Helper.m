//
//  EMMessageReactionOperation+Helper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2023/5/30.
//

#import "EMMessageReactionOperation+Helper.h"

@implementation EMMessageReactionOperation (Helper)
- (NSDictionary *)toJson {
    return @{@"userId": self.userId,
             @"reaction": self.reaction,
             @"operate": @(self.operate)
    };
}
@end
