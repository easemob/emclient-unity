//
//  EMGroupMessageAck+Helper.m
//  Unity-iPhone
//
//  Created by 杜洁鹏 on 2021/7/25.
//

#import "EMGroupMessageAck+Helper.h"

@implementation EMGroupMessageAck (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"count"] = @(self.readCount);
    data[@"content"] = self.content;
    data[@"ackId"] = self.readAckId;
    data[@"msgId"] = self.messageId;
    data[@"from"] = self.from;
    data[@"timestamp"] = @(self.timestamp);
    return data;
}
@end
