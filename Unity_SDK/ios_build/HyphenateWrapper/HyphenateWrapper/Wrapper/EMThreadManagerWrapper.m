//
//  EMThreadManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/27.
//

#import "EMThreadManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMChatThreadListener.h"

@implementation EMThreadManagerWrapper {
    EMChatThreadListener * _listener;
}


- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMChatThreadListener alloc] init];
        [EMClient.sharedClient.threadManager addDelegate:_listener delegateQueue:nil];
    }
    
    return self;
}

- (void)createThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];
    NSString *groupId = param[@"groupId"];
    NSString *name = param[@"name"];
    [EMClient.sharedClient.threadManager createChatThread:name
                                                messageId:msgId
                                                 parentId:groupId
                                               completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
            
    }];
}
- (void)joinThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    [EMClient.sharedClient.threadManager joinChatThread:threadId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
            
    }];
}
- (void)leaveThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    [EMClient.sharedClient.threadManager leaveChatThread:threadId completion:^(EMError * _Nonnull aError) {
            
    }];
}
- (void)destroyThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    [EMClient.sharedClient.threadManager destroyChatThread:threadId completion:^(EMError * _Nonnull aError) {
    
    }];
}
- (void)removeThreadMember:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    NSString *memberId = param[@"username"];
    [EMClient.sharedClient.threadManager removeMemberFromChatThread:memberId threadId:threadId completion:^(EMError * _Nonnull aError) {
            
    }];
}
- (void)changeThreadName:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    NSString *name = param[@"name"];
    [EMClient.sharedClient.threadManager updateChatThreadName:name
                                                     threadId:threadId
                                                   completion:^(EMError * _Nonnull aError){
            
    }];
}
- (void)fetchThreadMembers:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    [EMClient.sharedClient.threadManager getChatThreadMemberListFromServerWithId:threadId cursor:cursor pageSize:pageSize completion:^(EMCursorResult<NSString *> * _Nonnull aResult, EMError * _Nonnull aError) {
            
    }];
}
- (void)fetchThreadListOfGroup:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *groupId = param[@"groupId"];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    BOOL joined = [param[@"joined"] boolValue];
    if (joined) {
        [EMClient.sharedClient.threadManager getJoinedChatThreadsFromServerWithParentId:groupId cursor:cursor pageSize:pageSize completion:^(EMCursorResult<EMChatThread *> * _Nonnull result, EMError * _Nonnull aError) {
                    
        }];
    }else {
        [EMClient.sharedClient.threadManager getChatThreadsFromServerWithParentId:groupId cursor:cursor pageSize:param completion:^(EMCursorResult<EMChatThread *> * _Nonnull result, EMError * _Nonnull aError) {
            
        }];
    }
    
}
- (void)fetchMineJoinedThreadList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    [EMClient.sharedClient.threadManager getJoinedChatThreadsFromServerWithCursor:cursor
                                                                         pageSize:pageSize
                                                                       completion:^(EMCursorResult<EMChatThread *> * _Nonnull result, EMError * _Nonnull aError) {
            
    }];
}
- (void)getThreadDetail:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    [EMClient.sharedClient.threadManager getChatThreadFromSever:threadId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
            
    }];
}
- (void)getLastMessageAccordingThreads:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *threadIds = param[@"threadIds"];
    [EMClient.sharedClient.threadManager getLastMessageFromSeverWithChatThreads:threadIds completion:^(NSDictionary<NSString *,EMChatMessage *> * _Nonnull messageMap, EMError * _Nonnull aError) {
            
    }];
}
@end
