//
//  DelegateTester.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/12/28.
//

#import "DelegateTester.h"
#import "EMClientWrapper.h"

@implementation DelegateTester {
    EMChatroom *_room;
    EMGroup *_group;
}

+ (DelegateTester *)shared {
    static DelegateTester *tester_;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        tester_ = [[DelegateTester alloc] init];
    });
    return tester_;
}


- (void)startTest {
    [self connnectDelegateTest];
    [self multiDeviceDelegateTest];
    [self contactDelegateTest];
    [self chatManagerDelegateTest];
    EMCursorResult *result = [EMClient.sharedClient.groupManager getPublicGroupsFromServerWithCursor:nil pageSize:10 error:nil];
    _group = result.list.firstObject;
    [self groupManagerDelegateTest];
    EMPageResult *pages = [EMClient.sharedClient.roomManager getChatroomsFromServerWithPage:0 pageSize:10 error:nil];
    _room = pages.list.firstObject;
    [self roomManagerDelegateTest];
    [self presenceManagerTest];
    [self threadManagerTest];
}


- (void)connnectDelegateTest {
    [EMClientWrapper.shared connectionStateDidChange:EMConnectionConnected];
    [EMClientWrapper.shared connectionStateDidChange:EMConnectionDisconnected];
    [EMClientWrapper.shared userAccountDidLoginFromOtherDevice:@"another-device"];
    [EMClientWrapper.shared userAccountDidRemoveFromServer];
    [EMClientWrapper.shared userDidForbidByServer];
    [EMClientWrapper.shared userAccountDidForcedToLogout:[EMError errorWithDescription:@"" code:216]];
    [EMClientWrapper.shared userAccountDidForcedToLogout:[EMError errorWithDescription:@"" code:214]];
    [EMClientWrapper.shared userAccountDidForcedToLogout:[EMError errorWithDescription:@"" code:217]];
    [EMClientWrapper.shared userAccountDidForcedToLogout:[EMError errorWithDescription:@"" code:202]];
    [EMClientWrapper.shared tokenWillExpire:0];
    [EMClientWrapper.shared tokenDidExpire:0];
}

- (void)multiDeviceDelegateTest {
    [EMClientWrapper.shared multiDevicesContactEventDidReceive:EMMultiDevicesEventContactAccept username:@"username" ext:@"ext"];
    [EMClientWrapper.shared multiDevicesGroupEventDidReceive:EMMultiDevicesEventGroupInviteAccept groupId:@"groupId" ext:@[@"userId1", @"userId2"]];
    [EMClientWrapper.shared multiDevicesChatThreadEventDidReceive:EMMultiDevicesEventChatThreadJoin threadId:@"threadId" ext:@[@"userId", @"userId2"]];
}

- (void)contactDelegateTest {
    [EMClientWrapper.shared.contactManagerWrapper friendRequestDidApproveByUser:@"test"];
    [EMClientWrapper.shared.contactManagerWrapper friendRequestDidDeclineByUser:@"test"];
    [EMClientWrapper.shared.contactManagerWrapper friendshipDidRemoveByUser:@"test"];
    [EMClientWrapper.shared.contactManagerWrapper friendshipDidAddByUser:@"test"];
    [EMClientWrapper.shared.contactManagerWrapper friendRequestDidReceiveFromUser:@"test" message:@"message"];
}

- (void)chatManagerDelegateTest {
    EMConversation *conv1 = [EMClient.sharedClient.chatManager getConversation:@"chat" type:EMConversationTypeChat createIfNotExist:YES];
    EMConversation *conv2 = [EMClient.sharedClient.chatManager getConversation:@"group" type:EMConversationTypeGroupChat createIfNotExist:YES];
    EMConversation *conv3 = [EMClient.sharedClient.chatManager getConversation:@"room" type:EMConversationTypeChatRoom createIfNotExist:YES];
    [EMClientWrapper.shared.chatManager conversationListDidUpdate:@[conv1, conv2, conv3]];
    
    EMTextMessageBody *body = [[EMTextMessageBody alloc] initWithText:@"hello"];
    EMChatMessage *msg1 = [[EMChatMessage alloc] initWithConversationID:@"chat" body:body ext:@{@"key": @"value"}];
    EMChatMessage *msg2 = [[EMChatMessage alloc] initWithConversationID:@"group" body:body ext:@{@"key": @"value"}];
    EMChatMessage *msg3 = [[EMChatMessage alloc] initWithConversationID:@"room" body:body ext:@{@"key": @"value"}];
    [EMClientWrapper.shared.chatManager messagesDidReceive:@[msg1, msg2, msg3]];
    EMCmdMessageBody *cmdBody = [[EMCmdMessageBody alloc] initWithAction:@"action"];
    cmdBody.isDeliverOnlineOnly = YES;
    EMChatMessage *cmdMsg = [[EMChatMessage alloc] initWithConversationID:@"cmd" body:cmdBody ext:@{@"key": @"value"}];
    [EMClientWrapper.shared.chatManager cmdMessagesDidReceive:@[cmdMsg]];
    [EMClientWrapper.shared.chatManager messagesDidRead:@[msg1, msg2, msg3]];
    
    EMGroupMessageAck *ack = [[EMGroupMessageAck alloc] init];
    ack.messageId = @"msgId";
    ack.content = @"hehe";
    ack.from = @"from";
    ack.readAckId = @"reacAckId";
    ack.readCount = 100;
    ack.timestamp = 10000000000;
    [EMClientWrapper.shared.chatManager groupMessageDidRead:msg1 groupAcks:@[ack]];
    
    [EMClientWrapper.shared.chatManager onConversationRead:@"from" to:@"to"];
    [EMClientWrapper.shared.chatManager groupMessageAckHasChanged];
    [EMClientWrapper.shared.chatManager messagesDidDeliver:@[msg1, msg2, msg3]];
    
    EMRecallMessageInfo *recall = [[EMRecallMessageInfo alloc] init];
    recall.recallBy = @"recallBy";
    recall.recallMessageId =@"messageId";
    recall.ext = @"ext";
    recall.recallMessage = msg1;
    [EMClientWrapper.shared.chatManager messagesInfoDidRecall:@[recall]];
    
    
    [EMClientWrapper.shared.chatManager messageStatusDidChange:msg1 error:[EMError errorWithDescription:@"status change" code:EMErrorGeneral]];
    [EMClientWrapper.shared.chatManager messageAttachmentStatusDidChange:msg1 error:[EMError errorWithDescription:@"status change" code:EMErrorGeneral]];
    
}

- (void)groupManagerDelegateTest {
    [EMClientWrapper.shared.groupManagerWrapper groupInvitationDidReceive:@"groupId" groupName:@"groupName" inviter:@"inviter" message:@"message"];
    [EMClientWrapper.shared.groupManagerWrapper groupInvitationDidAccept:_group invitee:@"invitee"];
    [EMClientWrapper.shared.groupManagerWrapper groupInvitationDidDecline:_group invitee:@"invitee" reason:@"reason"];
    [EMClientWrapper.shared.groupManagerWrapper didJoinGroup:_group inviter:@"inviter" message:@"message"];
    [EMClientWrapper.shared.groupManagerWrapper didLeaveGroup:_group reason: EMGroupLeaveReasonDestroyed];
    [EMClientWrapper.shared.groupManagerWrapper didLeaveGroup:_group reason: EMGroupLeaveReasonBeRemoved];
    [EMClientWrapper.shared.groupManagerWrapper joinGroupRequestDidReceive:_group user:@"user" reason:@"reason"];
    [EMClientWrapper.shared.groupManagerWrapper joinGroupRequestDidDecline:_group.groupId reason: @"reason" decliner:@"decliner" applicant:@"applicant"];
    [EMClientWrapper.shared.groupManagerWrapper joinGroupRequestDidApprove:_group];
    [EMClientWrapper.shared.groupManagerWrapper groupListDidUpdate:@[_group]];
    [EMClientWrapper.shared.groupManagerWrapper groupMuteListDidUpdate:_group addedMutedMembers:@[@"user"] muteExpire:1000000];
    [EMClientWrapper.shared.groupManagerWrapper groupMuteListDidUpdate:_group removedMutedMembers: @[@"user"]];
    [EMClientWrapper.shared.groupManagerWrapper groupWhiteListDidUpdate:_group addedWhiteListMembers: @[@"user"]];
    [EMClientWrapper.shared.groupManagerWrapper groupWhiteListDidUpdate:_group removedWhiteListMembers: @[@"user"]];
    [EMClientWrapper.shared.groupManagerWrapper groupAllMemberMuteChanged:_group isAllMemberMuted:YES];
    [EMClientWrapper.shared.groupManagerWrapper groupAdminListDidUpdate:_group addedAdmin:@"addedAdmin"];
    [EMClientWrapper.shared.groupManagerWrapper groupAdminListDidUpdate:_group removedAdmin:@"removedAdmin"];
    [EMClientWrapper.shared.groupManagerWrapper groupOwnerDidUpdate:_group newOwner:@"newOwner" oldOwner:@"oldOwner"];
    [EMClientWrapper.shared.groupManagerWrapper userDidJoinGroup:_group user:@"user"];
    [EMClientWrapper.shared.groupManagerWrapper userDidLeaveGroup:_group user:@"user"];
    [EMClientWrapper.shared.groupManagerWrapper groupAnnouncementDidUpdate:_group announcement:@"announcement"];
    
    EMGroupSharedFile *file = [[EMGroupSharedFile alloc] init];
    
    [EMClientWrapper.shared.groupManagerWrapper groupFileListDidUpdate:_group addedSharedFile:file];
    [EMClientWrapper.shared.groupManagerWrapper groupFileListDidUpdate:_group removedSharedFile:@"fileId"];
    [EMClientWrapper.shared.groupManagerWrapper groupStateChanged:_group isDisabled:YES];
    [EMClientWrapper.shared.groupManagerWrapper groupSpecificationDidUpdate:_group];
}

- (void)roomManagerDelegateTest {
    [EMClientWrapper.shared.roomManagerWrapper userDidJoinChatroom:_room user:@"user"];
    [EMClientWrapper.shared.roomManagerWrapper userDidLeaveChatroom:_room user:@"user"];
    [EMClientWrapper.shared.roomManagerWrapper didDismissFromChatroom:_room reason:EMChatroomBeKickedReasonOffline];
    [EMClientWrapper.shared.roomManagerWrapper didDismissFromChatroom:_room reason:EMChatroomBeKickedReasonDestroyed];
    [EMClientWrapper.shared.roomManagerWrapper didDismissFromChatroom:_room reason:EMChatroomBeKickedReasonBeRemoved];
    [EMClientWrapper.shared.roomManagerWrapper chatroomSpecificationDidUpdate:_room];
    [EMClientWrapper.shared.roomManagerWrapper chatroomMuteListDidUpdate:_room addedMutedMembers:@[@"user1", @"user2"] muteExpire:1000000];
    [EMClientWrapper.shared.roomManagerWrapper chatroomMuteListDidUpdate:_room removedMutedMembers:@[@"user1", @"user2"]];
    [EMClientWrapper.shared.roomManagerWrapper chatroomWhiteListDidUpdate:_room addedWhiteListMembers:@[@"user1", @"user2"]];
    [EMClientWrapper.shared.roomManagerWrapper chatroomWhiteListDidUpdate:_room removedWhiteListMembers:@[@"user1", @"user2"]];
    [EMClientWrapper.shared.roomManagerWrapper chatroomAllMemberMuteChanged:_room isAllMemberMuted:YES];
    [EMClientWrapper.shared.roomManagerWrapper chatroomAdminListDidUpdate:_room addedAdmin:@"addedAdmin"];
    [EMClientWrapper.shared.roomManagerWrapper chatroomAdminListDidUpdate:_room removedAdmin:@"addedAdmin"];
    [EMClientWrapper.shared.roomManagerWrapper chatroomOwnerDidUpdate:_room newOwner:@"newOwner" oldOwner:@"oldOwner"];
    [EMClientWrapper.shared.roomManagerWrapper chatroomAnnouncementDidUpdate:_room announcement:@"announcement"];
    [EMClientWrapper.shared.roomManagerWrapper chatroomAttributesDidUpdated:_room.chatroomId attributeMap:@{@"key":@"value"} from:@"from"];
    [EMClientWrapper.shared.roomManagerWrapper chatroomAttributesDidRemoved:_room.chatroomId attributes:@[@"key"] from:@"from"];
}

- (void)presenceManagerTest {
    EMPresence *presence = [[EMPresence alloc] init];
    presence.publisher = @"publisher";
    
    EMPresenceStatusDetail *detail = [[EMPresenceStatusDetail alloc] init];
    detail.status = 10;
    detail.device = @"android";
    
    presence.statusDetails = @[detail];
    presence.statusDescription = @"hahaha";
    [EMClientWrapper.shared.presenceManagerWrapper presenceStatusDidChanged:@[presence]];
}

- (void)threadManagerTest {
    EMChatThreadEvent *event = [[EMChatThreadEvent alloc] init];
    [EMClientWrapper.shared.chatThreadManagerWrapper onChatThreadCreate:event];
    [EMClientWrapper.shared.chatThreadManagerWrapper onChatThreadUpdate:event];
    [EMClientWrapper.shared.chatThreadManagerWrapper onChatThreadDestroy:event];
    [EMClientWrapper.shared.chatThreadManagerWrapper onUserKickOutOfChatThread:event];
}


@end
