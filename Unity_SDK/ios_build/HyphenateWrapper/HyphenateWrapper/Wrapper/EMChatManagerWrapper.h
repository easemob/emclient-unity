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
- (void)updateChatMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (id)deleteConversation:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)getConversation:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)getUnreadMessageCount:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)importMessages:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)loadAllConversations:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)getMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)markAllChatMsgAsRead:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)resendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)searchChatMsgFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (id)sendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

NS_ASSUME_NONNULL_END
