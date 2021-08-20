//
//  EMConversation+Unity.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMConversation (Unity)
- (NSDictionary *)toJson;
+ (int)typeToInt:(EMConversationType)aType;
+ (EMConversationType)typeFromInt:(int)aType;
@end

NS_ASSUME_NONNULL_END
