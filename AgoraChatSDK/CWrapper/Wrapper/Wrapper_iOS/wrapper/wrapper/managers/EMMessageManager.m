//
//  EMMessageManager.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMMessageManager.h"
#import "EMMessageReaction+Helper.h"
#import "EMChatThread+Helper.h"
#import "EMMessagePinInfo+Helper.h"
#import "EMHelper.h"
#import "EMUtil.h"

@implementation EMMessageManager

- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {
    
    NSString *ret = nil;
    if ([getReactionList isEqualToString:method]) {
        ret = [self getReactionList:params callback:callback];
    } else if ([groupAckCount isEqualToString:method]) {
        ret = [self groupAckCount:params callback:callback];
    } else if ([getChatThread isEqualToString:method]) {
        ret = [self getChatThread:params callback:callback];
    } else if ([getPinnedInfo isEqualToString:method]) {
        ret = [self getPinnedInfo:params callback:callback];
    } else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}

- (NSString *)getReactionList:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMChatMessage *msg = [self getMessage:params];
    NSMutableArray *list = [NSMutableArray array];
    for (EMMessageReaction *reaction in msg.reactionList) {
        [list addObject:[reaction toJson]];
    }
    
    return [[EMHelper getReturnJsonObject:list] toJsonString];
}

- (NSString *)groupAckCount:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMChatMessage *msg = [self getMessage:params];
    return [[EMHelper getReturnJsonObject:@(msg.groupAckCount)] toJsonString];
}

- (NSString *)getChatThread:(NSDictionary *)params callback:(EMWrapperCallback *)callback {
    EMChatMessage *msg = [self getMessage:params];
    return [[EMHelper getReturnJsonObject:[msg.chatThread toJson]] toJsonString];
}

- (EMChatMessage *)getMessage:(NSDictionary *)params {
    NSString *msgId = params[@"msgId"];
    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    return msg;
}

- (NSString *)getPinnedInfo:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    NSString *msgId = param[@"msgId"];

    EMChatMessage *msg = [EMClient.sharedClient.chatManager getMessageWithMessageId:msgId];
    if (msg) {
        if (msg.pinnedInfo) {
            return [[EMHelper getReturnJsonObject:[msg.pinnedInfo toJson]] toJsonString];
        }
    }

    return nil;
}

- (void)registerEaseListener{
    
}
@end
