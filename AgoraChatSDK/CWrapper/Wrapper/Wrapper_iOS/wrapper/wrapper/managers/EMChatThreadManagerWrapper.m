//
//  EMChatThreadManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMChatThreadManagerWrapper.h"
#import "EMChatThread+Helper.h"
#import "EMCursorResult+Helper.h"
#import "EMChatMessage+Helper.h"
#import "EMChatThreadEvent+Helper.h"
#import "EMUtil.h"

@interface EMChatThreadManagerWrapper () <EMThreadManagerDelegate>

@end

@implementation EMChatThreadManagerWrapper

- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}

- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {
    NSString * ret = nil;
    if([fetchChatThreadDetail isEqualToString:method]) {
        ret = [self fetchChatThreadDetail:params callback:callback];
    } else if([fetchJoinedChatThreads isEqualToString:method]) {
        ret = [self fetchJoinedChatThreads:params callback:callback];
    } else if([fetchChatThreadsWithParentId isEqualToString:method]) {
        ret = [self fetchChatThreadsWithParentId:params callback:callback];
    } else if([fetchChatThreadMember isEqualToString:method]) {
        ret = [self fetchChatThreadMember:params callback:callback];
    } else if([fetchLastMessageWithChatThreads isEqualToString:method]) {
        ret = [self fetchLastMessageWithChatThreads:params callback:callback];
    } else if([removeMemberFromChatThread isEqualToString:method]) {
        ret = [self removeMemberFromChatThread:params callback:callback];
    } else if([updateChatThreadSubject isEqualToString:method]) {
        ret = [self updateChatThreadSubject:params callback:callback];
    } else if([createChatThread isEqualToString:method]) {
        ret = [self createChatThread:params callback:callback];
    } else if([joinChatThread isEqualToString:method]) {
        ret = [self joinChatThread:params callback:callback];
    } else if([leaveChatThread isEqualToString:method]) {
        ret = [self leaveChatThread:params callback:callback];
    } else if([destroyChatThread isEqualToString:method]) {
        ret = [self destroyChatThread:params callback:callback];
    } else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}

- (NSString *)fetchChatThreadDetail:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback{
    NSString *threadId = param[@"threadId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager getChatThreadFromSever:threadId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:[thread toJson]];
    }];
    return nil;
}

- (NSString *)fetchJoinedChatThreads:(NSDictionary *)param
                            callback:(EMWrapperCallback *)callback{
    int pageSize = [param[@"pageSize"] intValue];
    NSString *cursor = param[@"cursor"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager getJoinedChatThreadsFromServerWithCursor:cursor pageSize:pageSize completion:^(EMCursorResult * _Nonnull cursorResult, EMError * _Nonnull aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[cursorResult toJson]];
    }];
    return nil;
}

- (NSString *)fetchChatThreadsWithParentId:(NSDictionary *)param
                                  callback:(EMWrapperCallback *)callback{
    BOOL joined = [param[@"joined"] boolValue];
    
    if (joined) {
        return [self fetchJoinedChatThreads:param callback:callback];
    }
    
    int pageSize = [param[@"pageSize"] intValue];
    NSString *cursor = param[@"cursor"];
    NSString *parentId = param[@"parentId"];
    
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager getChatThreadsFromServerWithParentId:parentId cursor:cursor pageSize:pageSize completion:^(EMCursorResult * _Nonnull cursorResult, EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:[cursorResult toJson]];
    }];
    return nil;
}

- (NSString *)fetchChatThreadMember:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback{
    int pageSize = [param[@"pageSize"] intValue];
    NSString *cursor = param[@"cursor"];
    NSString *threadId = param[@"threadId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager getChatThreadMemberListFromServerWithId:threadId cursor:cursor pageSize:pageSize completion:^(EMCursorResult * _Nonnull cursorResult, EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:[cursorResult toJson]];
    }];
    return nil;
}

- (NSString *)fetchLastMessageWithChatThreads:(NSDictionary *)param
                                     callback:(EMWrapperCallback *)callback{
    NSArray *threadIds = param[@"threadIds"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager getLastMessageFromSeverWithChatThreads:threadIds completion:^(NSDictionary<NSString *,EMChatMessage *> * _Nonnull messageMap, EMError * _Nonnull aError) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        for (NSString *key in messageMap.allKeys) {
            dict[key] = [messageMap[key] toJson];
        }
        
        [weakSelf wrapperCallback:callback error:aError object:dict];
    }];
    return nil;
}

- (NSString *)removeMemberFromChatThread:(NSDictionary *)param
                                callback:(EMWrapperCallback *)callback{
    NSString *threadId = param[@"threadId"];
    NSString *memberId = param[@"userId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager removeMemberFromChatThread:memberId threadId:threadId completion:^(EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)updateChatThreadSubject:(NSDictionary *)param
                             callback:(EMWrapperCallback *)callback{
    NSString *threadId = param[@"threadId"];
    NSString *name = param[@"name"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager updateChatThreadName:name threadId:threadId completion:^(EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)createChatThread:(NSDictionary *)param
                      callback:(EMWrapperCallback *)callback{
    NSString *messageId = param[@"messageId"];
    NSString *name = param[@"name"];
    NSString *parentId = param[@"parentId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager createChatThread:name messageId:messageId parentId:parentId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:[thread toJson]];
    }];
    return nil;
}

- (NSString *)joinChatThread:(NSDictionary *)param
                    callback:(EMWrapperCallback *)callback{
    NSString *threadId = param[@"threadId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager joinChatThread:threadId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:[thread toJson]];
    }];
    return nil;
}

- (NSString *)leaveChatThread:(NSDictionary *)param
                     callback:(EMWrapperCallback *)callback{
    NSString *threadId = param[@"threadId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.threadManager leaveChatThread:threadId completion:^(EMError * _Nonnull aError) {
       [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (NSString *)destroyChatThread:(NSDictionary *)param
                       callback:(EMWrapperCallback *)callback{
    NSString *threadId = param[@"threadId"];
    __weak EMChatThreadManagerWrapper *weakSelf = self;
    
    [EMClient.sharedClient.threadManager destroyChatThread:threadId completion:^(EMError * _Nonnull aError) {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];
    return nil;
}

- (void)registerEaseListener
{
    [EMClient.sharedClient.threadManager addDelegate:self delegateQueue:nil];
}

- (void)onChatThreadCreate:(EMChatThreadEvent *)event
{
    [EMWrapperHelper.shared.listener onReceive:chatThreadListener method:onChatThreadCreate info:[[event toJson] toJsonString]];
}
- (void)onChatThreadUpdate:(EMChatThreadEvent *)event
{
    [EMWrapperHelper.shared.listener onReceive:chatThreadListener method:onChatThreadUpdate info:[[event toJson] toJsonString]];
}
- (void)onChatThreadDestroy:(EMChatThreadEvent *)event
{
    [EMWrapperHelper.shared.listener onReceive:chatThreadListener method:onChatThreadDestroy info:[[event toJson] toJsonString]];
}
- (void)onUserKickOutOfChatThread:(EMChatThreadEvent *)event
{
    [EMWrapperHelper.shared.listener onReceive:chatThreadListener method:onUserKickOutOfChatThread info:[[event toJson] toJsonString]];
}

@end
