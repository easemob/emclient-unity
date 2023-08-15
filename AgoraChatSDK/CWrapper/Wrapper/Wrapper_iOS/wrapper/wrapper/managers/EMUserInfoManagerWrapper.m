//
//  EMUserInfoManagerWrapper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import "EMUserInfoManagerWrapper.h"
#import "EMUserInfo+Helper.h"

@implementation EMUserInfoManagerWrapper
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
    if ([updateOwnUserInfo isEqualToString:method]) {
        ret = [self updateOwnUserInfo:params callback:callback];
    } else if ([updateOwnUserInfoWithType isEqualToString:method]) {
        ret = [self updateOwnUserInfoWithType:params callback:callback];
    }else if ([fetchUserInfoById isEqualToString:method]) {
        ret = [self fetchUserInfoByUserId:params callback:callback];
    }else if ([fetchUserInfoByIdWithType isEqualToString:method]) {
        ret = [self fetchUserInfoByIdWithType:params callback:callback];
    } else {
        ret = [super onMethodCall:method params:params callback:callback];
    }
    return ret;
}

- (NSString *)updateOwnUserInfo:(NSDictionary *)param
                       callback:(EMWrapperCallback *)callback {
    __weak EMUserInfoManagerWrapper *weakSelf = self;
    NSString *usenrame = EMClient.sharedClient.currentUsername;
    if (usenrame == nil) {
        EMError *error = [EMError errorWithDescription:@"User not login" code:EMErrorUserNotLogin];
        [weakSelf wrapperCallback:callback error:error object:nil];
        return nil;
    }
    
    EMUserInfo *userInfo = [EMUserInfo fromJson:param[@"userInfo"]];
    userInfo.userId = usenrame;
    [EMClient.sharedClient.userInfoManager updateOwnUserInfo:userInfo completion:^(EMUserInfo *aUserInfo, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aUserInfo toJson]];
    }];
    
    return nil;
}

- (NSString *)updateOwnUserInfoWithType:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    __weak EMUserInfoManagerWrapper *weakSelf = self;
    
    int typeValue = [param[@"userInfoType"] intValue];
    EMUserInfoType userInfoType = [self userInfoTypeFromInt:typeValue];
    NSString *userInfoValue = param[@"userInfoValue"];
    
    
    [EMClient.sharedClient.userInfoManager updateOwnUserInfo:userInfoValue withType:userInfoType completion:^(EMUserInfo *aUserInfo, EMError *aError) {
        [weakSelf wrapperCallback:callback error:aError object:[aUserInfo toJson]];
    }];
    
    return nil;
}

- (NSString *)fetchUserInfoByUserId:(NSDictionary *)param
                           callback:(EMWrapperCallback *)callback {
    __weak EMUserInfoManagerWrapper *weakSelf = self;
    NSArray *userIds = param[@"userIds"];
    
    [EMClient.sharedClient.userInfoManager fetchUserInfoById:userIds completion:^(NSDictionary *aUserDatas, EMError *aError) {
        
        NSMutableDictionary *dic = NSMutableDictionary.new;
        [aUserDatas enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) {
            dic[key] = [(EMUserInfo *)obj toJson];
        }];
        
        [weakSelf wrapperCallback:callback error:aError object:dic];
    }];
    return nil;
}

- (NSString *)fetchUserInfoByIdWithType:(NSDictionary *)param
                               callback:(EMWrapperCallback *)callback {
    __weak EMUserInfoManagerWrapper *weakSelf = self;
    NSArray *userIds = param[@"userIds"];
    NSArray<NSNumber *> *userInfoTypes = param[@"userInfoTypes"];
    
    [EMClient.sharedClient.userInfoManager fetchUserInfoById:userIds type:userInfoTypes completion:^(NSDictionary *aUserDatas, EMError *aError) {
        
        NSMutableDictionary *dic = NSMutableDictionary.new;
        [aUserDatas enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) {
            dic[key] = [(EMUserInfo *)obj toJson];
        }];
        [weakSelf wrapperCallback:callback error:aError object:dic];
    }];
    return nil;
}

- (EMUserInfoType)userInfoTypeFromInt:(int)typeValue {
    EMUserInfoType userInfoType;
    
    switch (typeValue) {
        case 0:
            userInfoType = EMUserInfoTypeNickName;
            break;
        case 1:
            userInfoType = EMUserInfoTypeAvatarURL;
            break;
        case 2:
            userInfoType = EMUserInfoTypePhone;
            break;
        case 3:
            userInfoType = EMUserInfoTypeMail;
            break;
        case 4:
            userInfoType = EMUserInfoTypeGender;
            break;
        case 5:
            userInfoType = EMUserInfoTypeSign;
            break;
        case 6:
            userInfoType = EMUserInfoTypeBirth;
            break;
        case 7:
            userInfoType = EMUserInfoTypeExt;
            break;
        default:
            userInfoType = EMUserInfoTypeNickName;
            break;
    }
    
    return userInfoType;
}

- (void)registerEaseListener{
    
}
@end
