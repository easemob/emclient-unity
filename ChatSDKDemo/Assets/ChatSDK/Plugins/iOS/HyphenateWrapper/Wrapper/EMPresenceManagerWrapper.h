//
//  EMPresenceManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/27.
//

#import <Foundation/Foundation.h>
#import <HyphenateChat/HyphenateChat.h>
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMPresenceManagerWrapper : EMWrapper
- (void)publishPresence:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)subscribePresences:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)unsubscribePresences:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchSubscribedMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchPresenceStatus:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

@interface EMPresenceListener : NSObject <EMPresenceManagerDelegate>

@end

NS_ASSUME_NONNULL_END
