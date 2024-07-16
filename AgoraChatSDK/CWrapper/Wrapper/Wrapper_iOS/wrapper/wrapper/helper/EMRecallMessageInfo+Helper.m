//
//  EMRecallMessageInfo+Helper.m
//  wrapper
//
//  Created by yuqiang on 2024/5/31.
//

#include "EMRecallMessageInfo+Helper.h"
#include "EMChatMessage+Helper.h"

@implementation EMRecallMessageInfo (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"recallBy"] = self.recallBy;
    data[@"recallMessageId"] = self.recallMessageId;
    data[@"ext"] = self.ext;
    if(nil != self.recallMessage) {
        data[@"recallMessage"] = [self.recallMessage toJson];
    }
    return data;
}
@end
