//
//  EMContactManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import "EMContactManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMContactListener.h"
#import "Transfrom.h"

@interface EMContactManagerWrapper ()<EMContactManagerDelegate>
{
    EMContactListener *_listener;
}
@end

@implementation EMContactManagerWrapper
- (instancetype)init {
    if (self = [super init]) {
        _listener = [[EMContactListener alloc] init];
        [EMClient.sharedClient.contactManager addDelegate:_listener delegateQueue:nil];
    }
    return self;
}

- (void)acceptInvitation:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *username = param[@"username"];
    if (!username) {
        EMError *aError = [[EMError alloc] initWithDescription:@"username is invalid " code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMContactManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.contactManager approveFriendRequestFromUser:username
                                                            completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)addContact:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *username = param[@"username"];
    if (!username) {
        EMError *aError = [[EMError alloc] initWithDescription:@"username is invalid " code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    
    NSString *reason = param[@"reason"];
    __block NSString *callId = callbackId;
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager addContact:username
                                             message:reason
                                          completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callbackId error:aError];
        }
    }];
}

- (void)addUserToBlockList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *username = param[@"username"];
    if (!username) {
        EMError *aError = [[EMError alloc] initWithDescription:@"username is invalid " code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId = callbackId;
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager addUserToBlackList:username
                                                  completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)declineInvitation:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *username = param[@"username"];
    if (!username) {
        EMError *aError = [[EMError alloc] initWithDescription:@"username is invalid " code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMContactManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.contactManager declineFriendRequestFromUser:username
                                                            completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}


- (void)deleteContact:(NSDictionary *)param callbackId:(NSString *)callbackId {
    NSString *username = param[@"username"];
    if (!username) {
        EMError *aError = [[EMError alloc] initWithDescription:@"username is invalid " code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak EMContactManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    BOOL keepConversation = [param[@"keepConversation"] boolValue];
    [EMClient.sharedClient.contactManager deleteContact:username
                                   isDeleteConversation:keepConversation
                                          completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (id)getAllContactsFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId {
    
    NSArray<NSString*>* aList = [EMClient.sharedClient.contactManager getContacts];
    if (aList == nil) {
        return nil;
    }
    return aList;
}


- (void)getAllContactsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __block NSString *callId = callbackId;
    __weak EMContactManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.contactManager getContactsFromServerWithCompletion:^(NSArray *aList, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:aList]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __weak EMContactManagerWrapper * weakSelf = self;
    __block NSString *callId = callbackId;
    [EMClient.sharedClient.contactManager getBlackListFromServerWithCompletion:^(NSArray *aList, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:[Transfrom NSStringFromJsonObject:aList]];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}


- (void)removeUserFromBlockList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId =callbackId;
    __weak EMContactManagerWrapper * weakSelf = self;
    NSString *username = param[@"username"];
    [EMClient.sharedClient.contactManager removeUserFromBlackList:username
                                                  completion:^(NSString *aUsername, EMError *aError)
    {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callId userInfo:nil];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}



- (void)getSelfIdsOnOtherPlatform:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __weak EMContactManagerWrapper * weakSelf = self;
    __block NSString *callId =callbackId;
    [EMClient.sharedClient.contactManager getSelfIdsOnOtherPlatformWithCompletion:^(NSArray *aList, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:aList];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

@end
