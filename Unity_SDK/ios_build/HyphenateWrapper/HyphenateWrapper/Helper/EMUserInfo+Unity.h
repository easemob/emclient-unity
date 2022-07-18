//
//  EMUserInfo+Unity.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/12/3.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMUserInfo (Unity)
+ (EMUserInfo *)fromJson:(NSDictionary *)dict;
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END
