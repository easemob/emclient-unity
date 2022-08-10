//
//  EMMessageWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/29.
//

#import "EMMessageWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMMessageReaction+Helper.h"
#import "EMChatThread+Helper.h"

@implementation EMMessageWrapper
- (NSDictionary *)getGroupAckCount:(NSDictionary *)param {
    NSDictionary *ret = nil;
    EMChatMessage *msg = [self getMessage:param];
    ret = @{@"count": @(msg.groupAckCount)};
    return ret;
}
- (NSDictionary *)getHasDeliverAck:(NSDictionary *)param {
    NSDictionary *ret = nil;
    EMChatMessage *msg = [self getMessage:param];
    ret = @{@"hasDeliverAck": @(msg.isDeliverAcked)};
    return ret;
}
- (NSDictionary *)getHasReadAck:(NSDictionary *)param {
    NSDictionary *ret = nil;
    EMChatMessage *msg = [self getMessage:param];
    ret = @{@"hasReadAcked": @(msg.isReadAcked)};
    return ret;
}
- (NSArray *)getReactionList:(NSDictionary *)param {
    NSDictionary *ret = nil;
    EMChatMessage *msg = [self getMessage:param];
    NSMutableArray *ary = [NSMutableArray array];
    for (EMMessageReaction *reaction in msg.reactionList) {
        [ary addObject:[reaction toJson]];
    }
    
    return ret;
}
- (NSDictionary *)getChatThread:(NSDictionary *)param {
    NSDictionary *ret = nil;
    EMChatMessage *msg = [self getMessage:param];
    if (msg.chatThread) {
        ret = @{@"chatThread":[msg.chatThread toJson]};
    }
    
    return ret;
}

- (EMChatMessage *)getMessage:(NSDictionary *)param {

    NSString *msgId = param[@"messageId"];
    
    if (msgId == nil || msgId.length == 0) {
        return nil;
    }
    return [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
}

@end
