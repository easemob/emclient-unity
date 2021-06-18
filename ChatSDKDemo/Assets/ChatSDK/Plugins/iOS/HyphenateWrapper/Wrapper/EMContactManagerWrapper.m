//
//  EMContactManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMContactManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>

@interface EMContactManagerWrapper ()<EMContactManagerDelegate>

@end

@implementation EMContactManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        [EMClient.sharedClient.contactManager addDelegate:self delegateQueue:nil];
    }
    return self;
}
@end
