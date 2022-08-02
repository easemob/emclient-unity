//
//  EMPresenceManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/27.
//

#import "EMPresenceManagerWrapper.h"
#import "NSArray+Helper.h"
#import "EMMethod.h"
#import "Transfrom.h"
#import "HyphenateWrapper.h"

@interface EMPresenceManagerWrapper ()
{
    EMPresenceListener *_listener;
}
@end


@implementation EMPresenceManagerWrapper

- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMPresenceListener alloc] init];
    }
    return self;
}

- (void)publishPresence:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *desc = param[@"desc"];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.presenceManager publishPresenceWithDescription:desc completion:^(EMError * _Nullable error) {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)subscribePresences:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *members = param[@"members"];
    long expiry = [param[@"expiry"] longValue];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.presenceManager subscribe:members expiry:expiry
                                          completion:^(NSArray<EMPresence *> * _Nullable presences, EMError * _Nullable error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:@"List<Presence>" callbackId:callbackId userInfo:[presences toJsonArray]];
        }
    }];
}
- (void)unsubscribePresences:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *members = param[@"members"];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.presenceManager unsubscribe:members completion:^(EMError * _Nullable error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }
    }];
}
- (void)fetchSubscribedMembers:(NSDictionary *)param callbackId:(NSString *)callbackId {
    int pageNum = [param[@"pageNum"] intValue];
    int pageSize = [param[@"pageSize"] intValue];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.presenceManager fetchSubscribedMembersWithPageNum:pageNum
                                                                    pageSize:pageSize
                                                                  Completion:^(NSArray<NSString *> * _Nullable members, EMError * _Nullable error)
     {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:@"List<String>" callbackId:callbackId userInfo:[members toJsonArray]];
        }
    }];
}
- (void)fetchPresenceStatus:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSArray *members = param[@"members"];
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.presenceManager fetchPresenceStatus:members
                                                    completion:^(NSArray<EMPresence *> * _Nullable presences, EMError * _Nullable error) {
        if (error) {
            [weakSelf onError:callbackId error:error];
        }else {
            [weakSelf onSuccess:@"List<Presence>" callbackId:callbackId userInfo:[presences toJsonArray]];
        }
    }];
}
@end

@interface EMPresenceListener ()

@end

@implementation EMPresenceListener

- (void)presenceStatusDidChanged:(NSArray<EMPresence*>* _Nonnull)presences {
    UnitySendMessage(PresenceListener_Obj, "OnPresenceUpdated", [Transfrom JsonObjectToCSString:[presences toJsonArray]]);
}

@end

