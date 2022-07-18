//
//  EMGroupMessageAck+Unity.m
//  Unity-iPhone
//
//  Created by 杜洁鹏 on 2021/7/25.
//

#import "EMGroupMessageAck+Unity.h"

@implementation EMGroupMessageAck (Unity)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"count"] = @(self.readCount);
    data[@"content"] = self.content;
    data[@"msgId"] = self.messageId;
    data[@"from"] = self.from;
    data[@"timestamp"] = @(self.timestamp);
    return data;
}
@end
