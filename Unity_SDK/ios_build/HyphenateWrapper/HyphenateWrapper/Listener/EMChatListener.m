//
//  EMChatListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import "EMChatListener.h"
#import "EMMethod.h"
#import "Transfrom.h"
#import "EMChatMessage+Helper.h"
#import "HyphenateWrapper.h"
#import "EMGroupMessageAck+Helper.h"
#import "EMMessageReactionChange+Helper.h"
#import "NSArray+Helper.h"

@implementation EMChatListener

- (void)conversationListDidUpdate:(NSArray *)aConversationList {
    UnitySendMessage(ChatListener_Obj, "OnConversationUpdate", "");
}

- (void)messagesDidReceive:(NSArray *)aMessages {
    NSMutableArray *array = [NSMutableArray array];
    for(EMChatMessage *msg in aMessages) {
        NSString *jsonString = [Transfrom NSStringFromJsonObject:[msg toJson]];
        [array addObject:jsonString];
    }
    UnitySendMessage(ChatListener_Obj, "OnMessageReceived", [Transfrom JsonObjectToCSString:array]);
}

- (void)cmdMessagesDidReceive:(NSArray *)aCmdMessages {
    NSMutableArray *array = [NSMutableArray array];
    for(EMChatMessage *msg in aCmdMessages) {
        NSString *jsonString = [Transfrom NSStringFromJsonObject:[msg toJson]];
        [array addObject:jsonString];
    }
    UnitySendMessage(ChatListener_Obj, "OnCmdMessageReceived", [Transfrom JsonObjectToCSString:array]);
}

- (void)messagesDidRead:(NSArray *)aMessages {
    NSMutableArray *array = [NSMutableArray array];
    for(EMChatMessage *msg in aMessages) {
        NSString *jsonString = [Transfrom NSStringFromJsonObject:[msg toJson]];
        [array addObject:jsonString];
    }
    UnitySendMessage(ChatListener_Obj, "OnMessageRead", [Transfrom JsonObjectToCSString:array]);
}

- (void)groupMessageDidRead:(EMChatMessage *)aMessage
                  groupAcks:(NSArray *)aGroupAcks {
    NSMutableArray *array = [NSMutableArray array];
    for(EMGroupMessageAck *ack in aGroupAcks) {
        NSString *jsonString = [Transfrom NSStringFromJsonObject:[ack toJson]];
        [array addObject:jsonString];
    }
    UnitySendMessage(ChatListener_Obj, "OnGroupMessageRead", [Transfrom JsonObjectToCSString:array]);
}

- (void)groupMessageAckHasChanged {
    UnitySendMessage(ChatListener_Obj, "OnReadAckForGroupMessageUpdated", NULL);
}

- (void)onConversationRead:(NSString *)from to:(NSString *)to {
    NSDictionary *dict = @{@"from":from, @"to": to};
    UnitySendMessage(ChatListener_Obj, "OnConversationRead", [Transfrom JsonObjectToCSString:dict]);
}

- (void)messagesDidDeliver:(NSArray *)aMessages {
    NSMutableArray *array = [NSMutableArray array];
    for(EMChatMessage *msg in aMessages) {
        NSString *jsonString = [Transfrom NSStringFromJsonObject:[msg toJson]];
        [array addObject:jsonString];
    }
    UnitySendMessage(ChatListener_Obj, "OnMessageDelivered", [Transfrom JsonObjectToCSString:array]);
}

- (void)messagesDidRecall:(NSArray *)aMessages {
    NSMutableArray *array = [NSMutableArray array];
    for(EMChatMessage *msg in aMessages) {
        NSString *jsonString = [Transfrom NSStringFromJsonObject:[msg toJson]];
        [array addObject:jsonString];
    }
    UnitySendMessage(ChatListener_Obj, "OnMessageRecalled", [Transfrom JsonObjectToCSString:array]);
}

- (void)messageStatusDidChange:(EMChatMessage *)aMessage
                         error:(EMError *)aError {
    
}

- (void)messageAttachmentStatusDidChange:(EMChatMessage *)aMessage
                                   error:(EMError *)aError {
    
}

- (void)messageReactionDidChange:(NSArray<EMMessageReactionChange *> *)changes {
    UnitySendMessage(ChatListener_Obj, "MessageReactionDidChange", [Transfrom JsonObjectToCSString:[changes toJsonArray]]);
}



@end
