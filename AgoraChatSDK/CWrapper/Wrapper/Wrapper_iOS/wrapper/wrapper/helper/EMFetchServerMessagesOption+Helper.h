//
//  EMFetchServerMessagesOption+Helper.h
//  wrapper
//
//  Created by 杜洁鹏 on 2023/5/30.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMFetchServerMessagesOption (Helper)
+ (EMFetchServerMessagesOption *)fromJson:(NSDictionary *)dict;
@end

NS_ASSUME_NONNULL_END
