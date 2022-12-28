//
//  EMPresenceManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMPresenceManagerWrapper.h"
#import "EMPresence+Helper.h"
#import "EMUtil.h"

@interface EMPresenceManagerWrapper()

@end

@implementation EMPresenceManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}


- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {
    NSString *str = nil;
    if ([presenceWithDescription isEqualToString:method]) {
        str = [self publishPresenceWithDescription:params callback:callback];
    } else if ([presenceSubscribe isEqualToString:method]) {
        str = [self subscribe:params callback:callback];
    } else if ([presenceUnsubscribe isEqualToString:method]) {
        str = [self unsubscribe:params callback:callback];
    } else if ([fetchPresenceStatus isEqualToString:method]) {
        str = [self fetchPresenceStatus:params callback:callback];
    } else if ([fetchSubscribedMembersWithPageNum isEqualToString:method]) {
        str = [self fetchSubscribedMembersWithPageNum:params callback:callback];
    } else {
        str = [super onMethodCall:method params:params callback:callback];
    }
    return str;
}

- (NSString *)publishPresenceWithDescription:(NSDictionary *)param
                                    callback:(EMWrapperCallback *)callback {
    __weak EMPresenceManagerWrapper *weakSelf = self;
    NSString *desc = param[@"desc"];
    [EMClient.sharedClient.presenceManager publishPresenceWithDescription:desc
                                                               completion:^(EMError *error)
     {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    return nil;
}

- (NSString *)subscribe:(NSDictionary *)param
               callback:(EMWrapperCallback *)callback {
    __weak EMPresenceManagerWrapper *weakSelf = self;
    NSArray *members = param[@"userIds"];
    NSInteger expiry = [param[@"expiry"] integerValue];
    
    [EMClient.sharedClient.presenceManager subscribe:members
                                              expiry:expiry
                                          completion:^(NSArray<EMPresence *> *presences, EMError *error)
     {
        NSMutableArray *list = [NSMutableArray array];
        for (EMPresence *presence in presences) {
            [list addObject:[presence toJson]];
        }
        [weakSelf wrapperCallback:callback error:error object:list];
    }];
    return nil;
}

- (NSString *)unsubscribe:(NSDictionary *)param
                 callback:(EMWrapperCallback *)callback {
    __weak EMPresenceManagerWrapper *weakSelf = self;
    NSArray *members = param[@"userIds"];
    
    [EMClient.sharedClient.presenceManager unsubscribe:members
                                            completion:^(EMError *error)
     {
        [weakSelf wrapperCallback:callback error:error object:nil];
    }];
    return nil;
}

- (NSString *)fetchPresenceStatus:(NSDictionary *)param
                         callback:(EMWrapperCallback *)callback {
    __weak EMPresenceManagerWrapper *weakSelf = self;
    NSArray *members = param[@"userIds"];
    
    [EMClient.sharedClient.presenceManager fetchPresenceStatus:members
                                                    completion:^(NSArray<EMPresence *> *presences, EMError *error)
     {
        NSMutableArray *list = [NSMutableArray array];
        for (EMPresence *presence in presences) {
            [list addObject:[presence toJson]];
        }
        [weakSelf wrapperCallback:callback error:error object:list];
    }];
    return nil;
}

- (NSString *)fetchSubscribedMembersWithPageNum:(NSDictionary *)param
                                       callback:(EMWrapperCallback *)callback {

    __weak EMPresenceManagerWrapper *weakSelf = self;
    int pageNum = [param[@"pageNum"] intValue];
    int pageSize = [param[@"pageSize"] intValue];
    
    [EMClient.sharedClient.presenceManager fetchSubscribedMembersWithPageNum:pageNum
                                                                    pageSize:pageSize
                                                                  Completion:^(NSArray<NSString *> *members, EMError *error)
     {
        [weakSelf wrapperCallback:callback error:error object:members];
    }];
    return nil;
}


- (void)registerEaseListener{
    [EMClient.sharedClient.presenceManager addDelegate:self delegateQueue:nil];
}

- (void)presenceStatusDidChanged:(NSArray<EMPresence*>* _Nonnull)presences {
    NSMutableArray *array = [NSMutableArray array];
    for (EMPresence *presence in presences) {
        [array addObject:[presence toJson]];
    }
    [EMWrapperHelper.shared.listener onReceive:chatListener method:onPresenceUpdated info:[array toJsonString]];
}

@end
