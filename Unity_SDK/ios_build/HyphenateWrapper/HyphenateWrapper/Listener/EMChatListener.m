//
//  EMChatListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import "EMChatListener.h"

@implementation EMChatListener

- (void)conversationListDidUpdate:(NSArray *)aConversationList {
    
}

- (void)messagesDidReceive:(NSArray *)aMessages {
    
}

- (void)cmdMessagesDidReceive:(NSArray *)aCmdMessages {
    
}

- (void)messagesDidRead:(NSArray *)aMessages {
    
}

- (void)groupMessageDidRead:(EMMessage *)aMessage
                  groupAcks:(NSArray *)aGroupAcks {
    
}

- (void)groupMessageAckHasChanged {
    
}

- (void)onConversationRead:(NSString *)from to:(NSString *)to {
    
}

- (void)messagesDidDeliver:(NSArray *)aMessages {
    
}

- (void)messagesDidRecall:(NSArray *)aMessages {
    
}

- (void)messageStatusDidChange:(EMMessage *)aMessage
                         error:(EMError *)aError {
    
}

- (void)messageAttachmentStatusDidChange:(EMMessage *)aMessage
                                   error:(EMError *)aError {
    
}


@end
