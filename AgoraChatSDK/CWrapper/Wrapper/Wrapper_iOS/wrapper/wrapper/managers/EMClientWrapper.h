//
//  EMClientWrapper.h
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/15.
//

#import <Foundation/Foundation.h>
#import "EMBaseManager.h"

#import "EMChatManagerWrapper.h"
#import "EMContactManagerWrapper.h"
#import "EMRoomManagerWrapper.h"
#import "EMGroupManagerWrapper.h"
#import "EMUserInfoManagerWrapper.h"
#import "EMPresenceManagerWrapper.h"
#import "EMChatThreadManagerWrapper.h"
#import "EMPushManagerWrapper.h"
#import "EMMessageManager.h"
#import "EMConversationWrapper.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMClientWrapper : EMBaseManager <EMClientDelegate, EMMultiDevicesDelegate>

+ (EMClientWrapper *)shared;

@property (nonatomic, strong) EMChatManagerWrapper *chatManager;
@property (nonatomic, strong) EMContactManagerWrapper *contactManagerWrapper;
@property (nonatomic, strong) EMRoomManagerWrapper *roomManagerWrapper;
@property (nonatomic, strong) EMGroupManagerWrapper *groupManagerWrapper;
@property (nonatomic, strong) EMUserInfoManagerWrapper *userInfoManagerWrapper;
@property (nonatomic, strong) EMPresenceManagerWrapper *presenceManagerWrapper;
@property (nonatomic, strong) EMChatThreadManagerWrapper *chatThreadManagerWrapper;
@property (nonatomic, strong) EMPushManagerWrapper *pushManagerWrapper;
@property (nonatomic, strong) EMMessageManager *messageManager;
@property (nonatomic, strong) EMConversationWrapper *conversationWrapper;
@end

NS_ASSUME_NONNULL_END
