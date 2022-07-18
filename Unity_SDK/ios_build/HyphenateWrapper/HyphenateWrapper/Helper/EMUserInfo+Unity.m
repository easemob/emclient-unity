//
//  EMUserInfo+Unity.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/12/3.
//

#import "EMUserInfo+Unity.h"

@implementation EMUserInfo (Unity)

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    ret[@"userId"] = self.userId;
    ret[@"nickName"] = self.nickName;
    ret[@"avatarUrl"] = self.avatarUrl;
    ret[@"mail"] = self.mail;
    ret[@"phone"] = self.phone;
    ret[@"gender"] = @(self.gender);
    ret[@"sign"] = self.sign;
    ret[@"birth"] = self.birth;
    ret[@"ext"] = self.ext;
   
    return ret;

}

+ (EMUserInfo *)fromJson:(NSDictionary *)aJson {
    EMUserInfo *userInfo = EMUserInfo.new;
    userInfo.userId = aJson[@"userId"] ?: @"";
    userInfo.nickName = aJson[@"nickName"] ?: @"";
    userInfo.avatarUrl = aJson[@"avatarUrl"] ?: @"";
    userInfo.mail = aJson[@"mail"] ?: @"";
    userInfo.phone = aJson[@"phone"] ?: @"";
    userInfo.gender = [aJson[@"gender"] integerValue] ?: 0;
    userInfo.sign = aJson[@"sign"] ?: @"";
    userInfo.birth = aJson[@"birth"] ?: @"";
    if ([aJson[@"ext"] isKindOfClass:[NSNull class]]) {
        userInfo.ext = @"";
    }else {
    userInfo.ext = aJson[@"ext"] ?: @"";
    }
    return [userInfo copy];
}

@end
