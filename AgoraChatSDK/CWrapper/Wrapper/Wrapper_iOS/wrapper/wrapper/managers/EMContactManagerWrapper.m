//
//  EMContactManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMContactManagerWrapper.h"
#import "EMContact+Helper.h"
#import "EMCursorResult+Helper.h"
#import "EMHelper.h"
#import "EMUtil.h"

@interface EMContactManagerWrapper () 

@end

@implementation EMContactManagerWrapper

- (instancetype)init {
    if (self = [super init]) {
        [self registerEaseListener];
    }
    
    return self;
}


- (NSString *)onMethodCall:(NSString *)method
                    params:(NSDictionary *)params
                  callback:(EMWrapperCallback *)callback {
    
    NSString *ret = nil;
    if ([addContact isEqualToString:method]) {
        ret = [self addContact:params callback:callback];
    } else if ([deleteContact isEqualToString:method]) {
        ret = [self deleteContact:params callback:callback];
    } else if ([getAllContactsFromServer isEqualToString:method]) {
        ret = [self getAllContactsFromServer:params callback:callback];
    } else if ([getAllContactsFromDB isEqualToString:method]) {
        ret = [self getAllContactsFromDB:params callback:callback];
    } else if ([addUserToBlockList isEqualToString:method]) {
        ret = [self addUserToBlockList:params callback:callback];
    } else if ([removeUserFromBlockList isEqualToString:method]) {
        ret = [self removeUserFromBlockList:params callback:callback];
    } else if ([getBlockListFromServer isEqualToString:method]) {
        ret = [self getBlockListFromServer:params callback:callback];
    } else if ([getBlockListFromDB isEqualToString:method]) {
        ret = [self getBlockListFromDB:params callback:callback];
    } else if ([acceptInvitation isEqualToString:method]) {
        ret = [self acceptInvitation:params callback:callback];
    } else if ([declineInvitation isEqualToString:method]) {
        ret = [self declineInvitation:params callback:callback];
    } else if ([getSelfIdsOnOtherPlatform isEqualToString:method]) {
        ret = [self getSelfIdsOnOtherPlatform:params callback:callback];
    } else if ([setContactRemark isEqualToString:method]) {
        ret = [self setContactRemark:params callback:callback];
    } else if ([fetchContactFromLocal isEqualToString:method]) {
        ret = [self fetchContactFromLocal:params callback:callback];
    } else if ([fetchAllContactsFromLocal isEqualToString:method]) {
        ret = [self fetchAllContactsFromLocal:params callback:callback];
    } else if ([fetchAllContactsFromServer isEqualToString:method]) {
        ret = [self fetchAllContactsFromServer:params callback:callback];
    } else if ([fetchAllContactsFromServerByPage isEqualToString:method]) {
        ret = [self fetchAllContactsFromServerByPage:params callback:callback];
    } else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}

- (NSString *)addContact:(NSDictionary *)param
                callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    
    NSString *username = param[@"userId"];
    NSString *reason = param[@"reason"];
    [EMClient.sharedClient.contactManager addContact:username
                                             message:reason
                                          completion:^(NSString *aUsername, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    
    return nil;
}

- (NSString *)deleteContact:(NSDictionary *)param
                   callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    
    NSString *username = param[@"userId"];
    BOOL keepConversation = [param[@"keepConversation"] boolValue];
    [EMClient.sharedClient.contactManager deleteContact:username
                                   isDeleteConversation:keepConversation
                                             completion:^(NSString *aUsername, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    
    return nil;
}

- (NSString *)getAllContactsFromServer:(NSDictionary *)param
                              callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager getContactsFromServerWithCompletion:^(NSArray *aList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    
    return nil;
}

- (NSString *)getAllContactsFromDB:(NSDictionary *)param
                          callback:(EMWrapperCallback *)callback {
    
    NSArray *aList = EMClient.sharedClient.contactManager.getContacts;
    return [[EMHelper getReturnJsonObject:aList] toJsonString];
}

- (NSString *)addUserToBlockList:(NSDictionary *)param
                        callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *username = param[@"userId"];
    [EMClient.sharedClient.contactManager addUserToBlackList:username
                                                  completion:^(NSString *aUsername, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    
    return nil;
}

- (NSString *)removeUserFromBlockList:(NSDictionary *)param
                             callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *username = param[@"userId"];
    [EMClient.sharedClient.contactManager removeUserFromBlackList:username
                                                       completion:^(NSString *aUsername, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    return nil;
}

- (NSString *)getBlockListFromServer:(NSDictionary *)param
                            callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager getBlackListFromServerWithCompletion:^(NSArray *aList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    return nil;
}

- (NSString *)getBlockListFromDB:(NSDictionary *)param
                        callback:(EMWrapperCallback *)callback {
    NSArray * list = [EMClient.sharedClient.contactManager getBlackList];
    return [[EMHelper getReturnJsonObject:list] toJsonString];
}

- (NSString *)acceptInvitation:(NSDictionary *)param
                      callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *username = param[@"userId"];
    [EMClient.sharedClient.contactManager approveFriendRequestFromUser:username
                                                            completion:^(NSString *aUsername, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    return nil;
}

- (NSString *)declineInvitation:(NSDictionary *)param
                       callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *username = param[@"userId"];
    [EMClient.sharedClient.contactManager declineFriendRequestFromUser:username
                                                            completion:^(NSString *aUsername, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aUsername];
    }];
    return nil;
}

- (NSString *)getSelfIdsOnOtherPlatform:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager getSelfIdsOnOtherPlatformWithCompletion:^(NSArray *aList, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:aList];
    }];
    return nil;
}

- (NSString *)setContactRemark:(NSDictionary *)param
                      callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *userId = param[@"userId"];
    NSString *remark = param[@"remark"];
    [EMClient.sharedClient.contactManager setContactRemark:userId
                                                    remark:remark
                                                completion:^(EMContact* aContact, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:nil];
    }];

    return nil;
}

- (NSString *)fetchContactFromLocal:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    NSString *userId = param[@"userId"];
    EMContact* emContact = [EMClient.sharedClient.contactManager getContact:userId];
    return [[emContact toJson] toJsonString];
}

- (NSString *)fetchAllContactsFromLocal:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    // since getAllContacts is implemented in async on Android, so here call getAllContacts in async.
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        NSArray* contactList = [EMClient.sharedClient.contactManager getAllContacts];
        NSMutableArray *array = [NSMutableArray array];
        for (EMContact *contact in contactList) {
            [array addObject:[contact toJson]];
        }
        [weakSelf wrapperCallback:callback error:nil object:array];
    });
    return nil;
}

- (NSString *)fetchAllContactsFromServer:(NSDictionary *)param
                                callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager getAllContactsFromServerWithCompletion:
                                                ^(NSArray<EMContact *> *aList, EMError *aError)
     {
        NSMutableArray *array = [NSMutableArray array];
        for (EMContact *contact in aList) {
            [array addObject:[contact toJson]];
        }
        [weakSelf wrapperCallback:callback error:aError object:array];
    }];

    return nil;
}

- (NSString *)fetchAllContactsFromServerByPage:(NSDictionary *)param
                                      callback:(EMWrapperCallback *)callback {
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *cursor = param[@"cursor"];
    NSInteger limit = [param[@"limit"] intValue];
    [EMClient.sharedClient.contactManager getContactsFromServerWithCursor:cursor
                                                                 pageSize:limit
                                                               completion:^(EMCursorResult *aResult, EMError *aError)
     {
        [weakSelf wrapperCallback:callback error:aError object:[aResult toJson]];
    }];

    return nil;
}

- (void)registerEaseListener {
    [EMClient.sharedClient.contactManager addDelegate:self delegateQueue:nil];
}

- (void)friendRequestDidApproveByUser:(NSString * _Nonnull)aUsername
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:contactListener method:onFriendRequestAccepted info:[dict toJsonString]];
}

- (void)friendRequestDidDeclineByUser:(NSString * _Nonnull)aUsername
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:contactListener method:onFriendRequestDeclined info:[dict toJsonString]];
}

- (void)friendshipDidRemoveByUser:(NSString * _Nonnull)aUsername
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:contactListener method:onContactDeleted info:[dict toJsonString]];
}

- (void)friendshipDidAddByUser:(NSString *_Nonnull)aUsername
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"userId"] = aUsername;
    [EMWrapperHelper.shared.listener onReceive:contactListener method:onContactAdded info:[dict toJsonString]];
}

- (void)friendRequestDidReceiveFromUser:(NSString *_Nonnull)aUsername
                                message:(NSString *_Nullable)aMessage
{
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    dict[@"userId"] = aUsername;
    dict[@"msg"] = aMessage;
    [EMWrapperHelper.shared.listener onReceive:contactListener method:onContactInvited info:[dict toJsonString]];
}
@end
