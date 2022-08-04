//
//  EMMessageWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/29.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMMessageWrapper : NSObject
- (NSDictionary *)getGroupAckCount:(NSDictionary *)param;
- (NSDictionary *)getHasDeliverAck:(NSDictionary *)param;
- (NSDictionary *)getHasReadAck:(NSDictionary *)param;
- (NSDictionary *)getReactionList:(NSDictionary *)param;
- (NSDictionary *)getChatThread:(NSDictionary *)param;
@end

NS_ASSUME_NONNULL_END
