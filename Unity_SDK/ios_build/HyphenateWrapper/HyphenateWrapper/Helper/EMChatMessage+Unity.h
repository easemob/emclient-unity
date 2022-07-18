//
//  EMMessage+Unity.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMChatMessage (Unity)
+ (EMChatMessage *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@interface EMMessageBody (Unity)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(nullable NSString *)type;
- (NSDictionary *)toJson;
+ (EMMessageBodyType)typeFromString:(NSString *)aStrType;
- (NSString *)typeToString;
@end

NS_ASSUME_NONNULL_END
