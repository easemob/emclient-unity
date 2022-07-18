//
//  EMUserInfoManagerWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/12/3.
//

#import "EMUserInfoManagerWrapper.h"
#import <HyphenateChat/HyphenateChat.h>
#import "EMUserInfo+Unity.h"
#import "Transfrom.h"

@implementation EMUserInfoManagerWrapper

- (void)updateOwnUserInfo:(NSDictionary *)param callbackId:(NSString *)callbackId {
    if (param.count == 0) {
        EMError *error = [EMError errorWithDescription:@"userInfo is invalid" code:EMErrorGeneral];
        [self onError:callbackId error:error];
        return;
    }
    EMUserInfo *userInfo = [EMUserInfo fromJson:param];
    
    __weak EMUserInfoManagerWrapper * weakSelf = self;
    [EMClient.sharedClient.userInfoManager updateOwnUserInfo:userInfo completion:^(EMUserInfo *aUserInfo, EMError *aError) {
        if (!aError) {
            [weakSelf onSuccess:nil callbackId:callbackId userInfo:nil];
        }else {
            [weakSelf onError:callbackId error:aError];
        }
    }];
}

- (void)updateOwnUserInfoWithType:(NSDictionary *)param callbackId:(NSString *)callbackId {
    
}

- (void)fetchUserInfoById:(NSObject *)param callbackId:(NSString *)callbackId {
    BOOL hasMamber = YES;
    NSArray *ary = nil;
    do {
        if (![param isKindOfClass:[NSArray class]]) {
            hasMamber = NO;
            break;
        }
        
        ary = (NSArray *)param;
        if (ary.count == 0) {
            hasMamber = NO;
            break;
        }
    } while (NO);
    
    if (!hasMamber) {
        EMError *error = [EMError errorWithDescription:@"id is invalid" code:EMErrorGeneral];
        [self onError:callbackId error:error];
        return;
    }
    
    __weak EMUserInfoManagerWrapper *weakSelf = self;
    [EMClient.sharedClient.userInfoManager fetchUserInfoById:ary
                                                  completion:^(NSDictionary *aUserDatas, EMError *aError)
     {
        if (!aError) {
            NSMutableArray *ary = [NSMutableArray array];
            for (NSString *key in aUserDatas.allKeys) {
                EMUserInfo *userInfo = aUserDatas[key];
                [ary addObject:@{key: [Transfrom NSStringFromJsonObject:[userInfo toJson]]}];
            }
            [self onSuccess:@"Map<String, UserInfo>"
                 callbackId:callbackId
                   userInfo:[Transfrom NSStringFromJsonObject:ary]];
        }else {
            [weakSelf onError:callbackId error:aError];
        }
    }];
}

@end
