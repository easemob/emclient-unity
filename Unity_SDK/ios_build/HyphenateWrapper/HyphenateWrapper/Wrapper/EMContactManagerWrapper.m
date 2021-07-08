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

- (void)addContact:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId =callbackId;
    __weak typeof(self) weakSelf = self;
    NSString *username = param[@"username"];
    NSString *reason = param[@"reason"];
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

- (void)deleteContact:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId =callbackId;
    NSString *username = param[@"username"];
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

- (void)getAllContactsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __block NSString *callId =callbackId;
    __weak typeof(self) weakSelf = self;
    [EMClient.sharedClient.contactManager getContactsFromServerWithCompletion:^(NSArray *aList, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:aList];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}

- (void)getAllContactsFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __block NSString *callId =callbackId;
    NSArray<NSString*>* aList = [EMClient.sharedClient.contactManager getContacts];
    [self onSuccess:@"List<String>" callbackId:callId userInfo:aList];
}
- (void)addUserToBlockList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId =callbackId;
    __weak typeof(self) weakSelf = self;
    NSString *username = param[@"username"];
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
- (void)removeUserFromBlockList:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __block NSString *callId =callbackId;
    __weak typeof(self) weakSelf = self;
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
- (void)getBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __weak typeof(self) weakSelf = self;
    __block NSString *callId =callbackId;
    [EMClient.sharedClient.contactManager getBlackListFromServerWithCompletion:^(NSArray *aList, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:@"List<String>" callbackId:callId userInfo:aList];
        }else {
            [weakSelf onError:callId error:aError];
        }
    }];
}
- (void)acceptInvitation:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId =callbackId;
    NSString *username = param[@"username"];
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
- (void)declineInvitation:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (!param) {
        EMError *aError = [[EMError alloc] initWithDescription:@"Param error" code: EMErrorMessageIncludeIllegalContent];
        [self onError:callbackId error:aError];
        return;
    }
    __weak typeof(self) weakSelf = self;
    __block NSString *callId =callbackId;
    NSString *username = param[@"username"];
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
- (void)getSelfIdsOnOtherPlatform:(NSDictionary *)param callbackId:(NSString *)callbackId {
    __weak typeof(self) weakSelf = self;
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
