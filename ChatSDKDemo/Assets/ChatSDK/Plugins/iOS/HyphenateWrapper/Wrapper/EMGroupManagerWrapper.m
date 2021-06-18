//
//  EMGroupManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMGroupManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>

@interface EMGroupManagerWrapper () <EMGroupManagerDelegate>

@end

@implementation EMGroupManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        [EMClient.sharedClient.groupManager addDelegate:self delegateQueue:nil];
    }
    return self;
}
@end
