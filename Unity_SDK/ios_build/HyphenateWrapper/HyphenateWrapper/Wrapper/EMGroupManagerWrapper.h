//
//  EMGroupManagerWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/5.
//

#import <Foundation/Foundation.h>
#import "EMWrapper.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMGroupManagerWrapper : EMWrapper

- (id)getGroupWithId:(NSDictionary *)param;

- (id)getJoinedGroups:(NSDictionary *)param;

- (void)getJoinedGroupsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getPublicGroupsFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)createGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupSpecificationFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupMemberListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupBlockListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupMuteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupWhiteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)isMemberInWhiteListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupFileListFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)getGroupAnnouncementFromServer:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)addMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)removeMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)blockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)unblockMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)updateGroupSubject:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)updateDescription:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)leaveGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)destroyGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)blockGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)unblockGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)updateGroupOwner:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)addAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)removeAdmin:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)muteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)unMuteMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)muteAllMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)unMuteAllMembers:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)addWhiteList:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)removeWhiteList:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)uploadGroupSharedFile:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)downloadGroupSharedFile:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)removeGroupSharedFile:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)updateGroupAnnouncement:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)updateGroupExt:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)joinPublicGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)requestToJoinPublicGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)acceptJoinApplication:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)declineJoinApplication:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)acceptInvitationFromGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

- (void)declineInvitationFromGroup:(NSDictionary *)param callbackId:(NSString *)callbackId;

@end

NS_ASSUME_NONNULL_END
