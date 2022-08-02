//
//  EMClientWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"
#import "EMContactManagerWrapper.h"
#import "EMChatManagerWrapper.h"
#import "EMRoomManagerWrapper.h"
#import "EMGroupManagerWrapper.h"
#import "EMPushManagerWrapper.h"
#import "EMConversationWrapper.h"
#import "EMUserInfoManagerWrapper.h"
#import "EMPresenceManagerWrapper.h"
#import "EMThreadManagerWrapper.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMClientWrapper : EMWrapper
@property (nonatomic, strong) EMContactManagerWrapper *contactManager;
@property (nonatomic, strong) EMChatManagerWrapper *chatManager;
@property (nonatomic, strong) EMGroupManagerWrapper *groupManager;
@property (nonatomic, strong) EMRoomManagerWrapper *roomManager;
@property (nonatomic, strong) EMPushManagerWrapper *pushManager;
@property (nonatomic, strong) EMConversationWrapper *conversationWrapper;
@property (nonatomic, strong) EMUserInfoManagerWrapper *userInfoManager;
@property (nonatomic, strong) EMPresenceManagerWrapper *presenceManager;
@property (nonatomic, strong) EMThreadManagerWrapper *chatThreadManager;

+ (EMClientWrapper *)instance;
- (void)initWithOptions:(NSDictionary *)param;
- (void)createAccount:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)login:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)logout:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)loginWithAgoraToken:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)renewToken:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

NS_ASSUME_NONNULL_END
