//
//  EMChatManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMChatManagerWrapper : EMWrapper
- (id)deleteConversation:(NSDictionary *)param;
- (void)downloadAttachment:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)downloadThumbnail:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchHistoryMessages:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)getConversation:(NSDictionary *)param;
- (void)getConversationsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)getUnreadMessageCount:(NSDictionary *)param;
- (id)importMessages:(NSDictionary *)param;
- (id)loadAllConversations:(NSDictionary *)param;
- (id)getMessage:(NSDictionary *)param;
- (id)markAllChatMsgAsRead:(NSDictionary *)param;
- (void)recallMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)resendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)searchChatMsgFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)ackConversationRead:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)sendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)ackMessageRead:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)sendReadAckForGroupMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)updateChatMessage:(NSDictionary *)param;
- (void)removeMessagesBeforeTimestamp:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)deleteConversationFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

@end

NS_ASSUME_NONNULL_END
