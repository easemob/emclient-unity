//
//  EMThreadManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/27.
//

#import "EMThreadManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMChatThreadListener.h"
#import "EMChatThread+Helper.h"
#import "EMCursorResult+Helper.h"
#import "EMChatMessage+Helper.h"

@implementation EMThreadManagerWrapper {
    EMChatThreadListener * _listener;
}


- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMChatThreadListener alloc] init];
        [EMClient.sharedClient.threadManager addDelegate:_listener delegateQueue:dispatch_get_main_queue()];
    }
    
    return self;
}

- (void)createThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *msgId = param[@"msgId"];
    NSString *groupId = param[@"groupId"];
    NSString *name = param[@"name"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager createChatThread:name
                                                messageId:msgId
                                                 parentId:groupId
                                               completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"ChatThread" callbackId:callbackId userInfo:[thread toJson]];
        }
    }];
}
- (void)joinThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager joinChatThread:threadId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"ChatThread" callbackId:callbackId userInfo:[thread toJson]];
        }
    }];
}
- (void)leaveThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager leaveChatThread:threadId completion:^(EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)destroyThread:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager destroyChatThread:threadId completion:^(EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)removeThreadMember:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    NSString *memberId = param[@"username"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager removeMemberFromChatThread:memberId threadId:threadId completion:^(EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)changeThreadName:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    NSString *name = param[@"name"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager updateChatThreadName:name
                                                     threadId:threadId
                                                   completion:^(EMError * _Nonnull aError){
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)fetchThreadMembers:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager getChatThreadMemberListFromServerWithId:threadId cursor:cursor pageSize:pageSize completion:^(EMCursorResult<NSString *> * _Nonnull aResult, EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"CursorResult<String>" callbackId:callbackId userInfo:[aResult toJson]];
        }
    }];
}
- (void)fetchThreadListOfGroup:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *groupId = param[@"groupId"];
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    BOOL joined = [param[@"joined"] boolValue];
    __weak EMThreadManagerWrapper * weakSelf = self;
    if (joined) {
        [EMClient.sharedClient.threadManager getJoinedChatThreadsFromServerWithParentId:groupId cursor:cursor pageSize:pageSize completion:^(EMCursorResult<EMChatThread *> * _Nonnull result, EMError * _Nonnull aError) {
            if (aError) {
                [weakSelf onError:callbackId error:aError];
            }else {
                [weakSelf onSuccess:@"CursorResult<ChatThread>" callbackId:callbackId userInfo:[result toJson]];
            }
        }];
    }else {
        [EMClient.sharedClient.threadManager getChatThreadsFromServerWithParentId:groupId cursor:cursor pageSize:pageSize completion:^(EMCursorResult<EMChatThread *> * _Nonnull result, EMError * _Nonnull aError) {
            if (aError) {
                [weakSelf onError:callbackId error:aError];
            }else {
                [weakSelf onSuccess:@"CursorResult<ChatThread>" callbackId:callbackId userInfo:[result toJson]];
            }
        }];
    }
    
}
- (void)fetchMineJoinedThreadList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *cursor = param[@"cursor"];
    int pageSize = [param[@"pageSize"] intValue];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager getJoinedChatThreadsFromServerWithCursor:cursor
                                                                         pageSize:pageSize
                                                                       completion:^(EMCursorResult<EMChatThread *> * _Nonnull result, EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"CursorResult<ChatThread>" callbackId:callbackId userInfo:[result toJson]];
        }
    }];
}
- (void)getThreadDetail:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *threadId = param[@"threadId"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager getChatThreadFromSever:threadId completion:^(EMChatThread * _Nonnull thread, EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            [weakSelf onSuccess:@"ChatThread" callbackId:callbackId userInfo:[thread toJson]];
        }
    }];
}

- (void)getLastMessageAccordingThreads:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *threadIds = param[@"threadIds"];
    __weak EMThreadManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.threadManager getLastMessageFromSeverWithChatThreads:threadIds completion:^(NSDictionary<NSString *,EMChatMessage *> * _Nonnull messageMap, EMError * _Nonnull aError) {
        if (aError) {
            [weakSelf onError:callbackId error:aError];
        }else {
            NSMutableDictionary *dict = [NSMutableDictionary dictionary];
            for (NSString *key in messageMap.allKeys) {
                dict[key] = [messageMap[key] toJson];
            }
            [weakSelf onSuccess:@"Dictionary<string, Message>" callbackId:callbackId userInfo:dict];
        }
    }];
}
@end
