//
//  EMPushOptions+Unity.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMPushOptions (Unity)
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END
