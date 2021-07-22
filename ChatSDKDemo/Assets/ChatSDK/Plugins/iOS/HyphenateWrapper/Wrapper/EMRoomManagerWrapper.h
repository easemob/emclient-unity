//
//  EMRoomManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMRoomManagerWrapper : EMWrapper

- (void)getChatroomsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)createChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)joinChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)leaveChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)destroyChatRoom:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)fetchChatroomInfoFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getChatroom:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getAllChatrooms:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getChatroomMemberListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)fetchChatroomBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getChatroomMuteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)fetchChatroomAnnouncement:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomUpdateSubject:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomUpdateDescription:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomRemoveMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomBlockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomUnblockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomChangeOwner:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomAddAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomRemoveAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomMuteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)chatRoomUnmuteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)updateChatroomAnnouncement:(NSDictionary *)param callbackId:(NSString *)callbackId;

@end

NS_ASSUME_NONNULL_END
