//
//  EMThreadManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/27.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMThreadManagerWrapper : EMWrapper
- (void)createThread:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)joinThread:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)leaveThread:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)destroyThread:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)removeThreadMember:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)changeThreadName:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchThreadMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchThreadListOfGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchMineJoinedThreadList:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)getThreadDetail:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)getLastMessageAccordingThreads:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

NS_ASSUME_NONNULL_END
