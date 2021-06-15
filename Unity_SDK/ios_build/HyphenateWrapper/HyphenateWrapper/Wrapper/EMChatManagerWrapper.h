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

- (NSDictionary *)deleteConversation:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)getConversation:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)getUnreadMessageCount:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)importMessages:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)loadAllConversations:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)getMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)markAllChatMsgAsRead:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)resendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)searchChatMsgFromDB:(NSDictionary *)param callbackId:(NSString *)callbackId;
- (NSDictionary *)sendMessage:(NSDictionary *)param callbackId:(NSString *)callbackId;
@end

NS_ASSUME_NONNULL_END
