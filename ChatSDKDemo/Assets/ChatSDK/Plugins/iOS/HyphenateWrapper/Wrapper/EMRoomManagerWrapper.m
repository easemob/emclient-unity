//
//  EMRoomManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMRoomManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>

@interface EMRoomManagerWrapper () <EMChatroomManagerDelegate>

@end

@implementation EMRoomManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        [EMClient.sharedClient.roomManager addDelegate:self delegateQueue:nil];
    }
    return self;
}
@end
