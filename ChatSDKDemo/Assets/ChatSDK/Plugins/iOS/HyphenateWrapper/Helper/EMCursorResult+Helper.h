//
//  EMCursorResult+Helper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMCursorResult (Helper)
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END
