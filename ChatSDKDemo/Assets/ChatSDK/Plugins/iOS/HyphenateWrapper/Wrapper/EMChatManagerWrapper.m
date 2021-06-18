//
//  EMChatManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMChatManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>

@interface EMChatManagerWrapper () <EMChatManagerDelegate>

@end

@implementation EMChatManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        [EMClient.sharedClient.chatManager addDelegate:self delegateQueue:nil];
    }
    return self;
}
@end
