//
//  EMConversationWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/15.
//

#import <Foundation/Foundation.h>
#import "EMConversationWrapper.h"
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMConversationWrapper : EMWrapper
- (NSDictionary *)getUnreadMsgCount:(NSDictionary *)param;

- (NSDictionary *)getLatestMessage:(NSDictionary *)param;

- (NSDictionary *)getLatestMessageFromOthers:(NSDictionary *)param;

- (void)markMessageAsRead:(NSDictionary *)param;

- (void)syncConversationExt:(NSDictionary *)param;

- (NSDictionary *)conversationExt:(NSDictionary *)param;

- (void)markAllMessagesAsRead:(NSDictionary *)param;

- (NSDictionary *)insertMessage:(NSDictionary *)param;

- (NSDictionary *)appendMessage:(NSDictionary *)param;

- (NSDictionary *)updateConversationMessage:(NSDictionary *)param;

- (NSDictionary *)removeMessage:(NSDictionary *)param;

- (NSDictionary *)clearAllMessages:(NSDictionary *)param;

- (NSDictionary *)loadMsgWithId:(NSDictionary *)param;

- (NSArray *)loadMsgWithMsgType:(NSDictionary *)param;

- (NSArray *)loadMsgWithStartId:(NSDictionary *)param;

- (NSArray *)loadMsgWithKeywords:(NSDictionary *)param;

- (NSArray *)loadMsgWithTime:(NSDictionary *)param;

@end

NS_ASSUME_NONNULL_END
