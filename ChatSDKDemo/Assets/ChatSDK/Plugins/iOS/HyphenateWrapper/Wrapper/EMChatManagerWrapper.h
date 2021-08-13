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
- (void)downloadAttachment:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)downloadThumbnail:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)fetchHistoryMessages:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)getConversationsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)recallMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)ackConversationRead:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)ackMessageRead:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (void)searchChatMsgFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (id)deleteConversation:(NSDictionary *)param;
- (id)getConversation:(NSDictionary *)param;
- (id)getUnreadMessageCount:(NSDictionary *)param;
- (id)importMessages:(NSDictionary *)param;
- (id)loadAllConversations:(NSDictionary *)param;
- (id)getMessage:(NSDictionary *)param;
- (id)markAllChatMsgAsRead:(NSDictionary *)param;
- (id)resendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)sendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)updateChatMessage:(NSDictionary *)param;
@end

NS_ASSUME_NONNULL_END
