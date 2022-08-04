//
//  EMChatThread+Helper.m
//  im_flutter_sdk
//
//  Created by 杜洁鹏 on 2022/5/29.
//

#import "EMChatThread+Helper.h"
#import "EMChatMessage+Helper.h"

@implementation EMChatThread (Helper)
- (nonnull NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    ret[@"tId"] = self.threadId;
    ret[@"name"] = self.threadName;
    ret[@"owner"] = self.owner;
    ret[@"messageId"] = self.messageId;
    ret[@"parentId"] = self.parentId;
    ret[@"membersCount"] = @(self.membersCount);
    ret[@"messageCount"] = @(self.messageCount);
    ret[@"createTimestamp"] = @(self.createAt);
    if (self.lastMessage) {
        ret[@"lastMessage"] = [self.lastMessage toJson];
    }
    return ret;
}

@end
