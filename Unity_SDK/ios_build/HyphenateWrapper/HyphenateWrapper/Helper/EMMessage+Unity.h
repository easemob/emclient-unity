//
//  EMMessage+Unity.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMMessage (Unity)
+ (EMMessage *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@interface EMMessageBody (Unity)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
+ (EMMessageBodyType)typeFromString:(NSString *)aStrType;

@end

NS_ASSUME_NONNULL_END
