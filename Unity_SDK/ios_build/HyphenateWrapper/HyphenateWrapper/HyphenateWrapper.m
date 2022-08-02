//
//  HyphenateWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/2.
//

#import <Foundation/Foundation.h>
#import "HyphenateWrapper.h"
#import "Transfrom.h"
#import "EMClientWrapper.h"

void Client_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"initWithOptions"]) {
        [EMClientWrapper.instance initWithOptions:dic];
    }else if ([method isEqualToString:@"createAccount"]) {
        [EMClientWrapper.instance createAccount:dic callbackId:callId];
    }else if ([method isEqualToString:@"login"]) {
        [EMClientWrapper.instance login:dic callbackId:callId];
    }else if ([method isEqualToString:@"logout"]) {
        [EMClientWrapper.instance logout:dic callbackId:callId];
    }else if ([method isEqualToString:@"loginWithAgoraToken"]) {
        [EMClientWrapper.instance loginWithAgoraToken:dic callbackId:callId];
    }else if ([method isEqualToString:@"renewToken"]) {
        [EMClientWrapper.instance renewToken:dic callbackId:callId];
    }
}

const char* Client_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    char* res = NULL;
    id jsonObject = nil;
    NSString *method = [Transfrom NSStringFromCString:methodName];
    if ([method isEqualToString:@"getCurrentUsername"]) {
        jsonObject = @{@"getCurrentUsername":EMClient.sharedClient.currentUsername};
    } else if ([method isEqualToString:@"isConnected"]) {
        jsonObject = @{@"isConnected": @(EMClient.sharedClient.isConnected)};
    } else if ([method isEqualToString:@"isLoggedIn"]) {
        jsonObject = @{@"isLoggedIn": @(EMClient.sharedClient.isLoggedIn)};
    }else if ([method isEqualToString:@"accessToken"]) {
        jsonObject = @{@"accessToken": EMClient.sharedClient.accessUserToken};
    }
    
    const char *csStr = [Transfrom JsonObjectToCSString:jsonObject];
    if (csStr != NULL) {
        res = (char*)malloc(strlen(csStr) +1);
        strcpy(res, csStr);
    }
    
    return res;
}

void ContactManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"addContact"]) {
        [EMClientWrapper.instance.contactManager addContact:dic callbackId:callId];
    }else if ([method isEqualToString:@"deleteContact"]) {
        [EMClientWrapper.instance.contactManager deleteContact:dic callbackId:callId];
    }else if ([method isEqualToString:@"getAllContactsFromServer"]) {
        [EMClientWrapper.instance.contactManager getAllContactsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"addUserToBlockList"]) {
        [EMClientWrapper.instance.contactManager addUserToBlockList:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeUserFromBlockList"]) {
        [EMClientWrapper.instance.contactManager removeUserFromBlockList:dic callbackId:callId];
    }else if ([method isEqualToString:@"getBlockListFromServer"]) {
        [EMClientWrapper.instance.contactManager getBlockListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"acceptInvitation"]) {
        [EMClientWrapper.instance.contactManager acceptInvitation:dic callbackId:callId];
    }else if ([method isEqualToString:@"declineInvitation"]) {
        [EMClientWrapper.instance.contactManager declineInvitation:dic callbackId:callId];
    }else if ([method isEqualToString:@"getSelfIdsOnOtherPlatform"]) {
        [EMClientWrapper.instance.contactManager getSelfIdsOnOtherPlatform:dic callbackId:callId];
    }
}

const char* ContactManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    char* ret = NULL;
    id jsonObject = nil;
    if ([method isEqualToString:@"getAllContactsFromDB"]) {
        jsonObject = [EMClientWrapper.instance.contactManager getAllContactsFromDB:dic callbackId:callId];
    }
    
    const char *csStr = [Transfrom JsonObjectToCSString:jsonObject];
    if (csStr != NULL) {
        ret = (char*)malloc(strlen(csStr) +1);
        strcpy(ret, csStr);
    }
    
    return ret;
}

void ChatManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"downloadAttachment"]) {
        [EMClientWrapper.instance.chatManager downloadAttachment:dic callbackId:callId];
    }else if ([method isEqualToString:@"downloadThumbnail"]) {
        [EMClientWrapper.instance.chatManager downloadThumbnail:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchHistoryMessages"]) {
        [EMClientWrapper.instance.chatManager fetchHistoryMessages:dic callbackId:callId];
    }else if ([method isEqualToString:@"getConversationsFromServer"]) {
        [EMClientWrapper.instance.chatManager getConversationsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"recallMessage"]) {
        [EMClientWrapper.instance.chatManager recallMessage:dic callbackId:callId];
    }else if ([method isEqualToString:@"ackConversationRead"]) {
        [EMClientWrapper.instance.chatManager ackConversationRead:dic callbackId:callId];
    }else if ([method isEqualToString:@"ackMessageRead"]) {
        [EMClientWrapper.instance.chatManager ackMessageRead:dic callbackId:callId];
    }else if ([method isEqualToString:@"sendReadAckFromGroupMessage"]) {
        [EMClientWrapper.instance.chatManager sendReadAckForGroupMessage:dic callbackId:callId];
    }else if ([method isEqualToString:@"searchChatMsgFromDB"]) {
        [EMClientWrapper.instance.chatManager searchChatMsgFromDB:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeMessageBeforeTimestamp"]) {
        [EMClientWrapper.instance.chatManager removeMessagesBeforeTimestamp:dic callbackId:callId];
    }else if ([method isEqualToString:@"deleteConversationFromServer"]) {
        [EMClientWrapper.instance.chatManager deleteConversationFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchSupportLanguages"]) {
        [EMClientWrapper.instance.chatManager fetchSupportLanguages:dic callbackId:callId];
    }else if ([method isEqualToString:@"translateMessage"]) {
        [EMClientWrapper.instance.chatManager translateMessage:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchGroupReadAcks"]) {
        [EMClientWrapper.instance.chatManager fetchGroupReadAcks:dic callbackId:callId];
    }else if ([method isEqualToString:@"reportMessage"]) {
        [EMClientWrapper.instance.chatManager reportMessage:dic callbackId:callId];
    }else if ([method isEqualToString:@"addReaction"]) {
        [EMClientWrapper.instance.chatManager addReaction:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeReaction"]) {
        [EMClientWrapper.instance.chatManager removeReaction:dic callbackId:callId];
    }else if ([method isEqualToString:@"getReactionList"]) {
        [EMClientWrapper.instance.chatManager getReactionList:dic callbackId:callId];
    }else if ([method isEqualToString:@"getReactionDetail"]) {
        [EMClientWrapper.instance.chatManager getReactionDetail:dic callbackId:callId];
    }
}

const char* ChatManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    char* res = NULL;
    id jsonObject = nil;
    if ([method isEqualToString:@"deleteConversation"]) {
        jsonObject = [EMClientWrapper.instance.chatManager deleteConversation:dic];
    }else if ([method isEqualToString:@"getConversation"]) {
        jsonObject = [EMClientWrapper.instance.chatManager getConversation:dic];
    }else if ([method isEqualToString:@"getUnreadMessageCount"]) {
        jsonObject = [EMClientWrapper.instance.chatManager getUnreadMessageCount:dic];
    }else if ([method isEqualToString:@"importMessages"]) {
        jsonObject = [EMClientWrapper.instance.chatManager importMessages:dic];
    }else if ([method isEqualToString:@"loadAllConversations"]) {
        jsonObject = [EMClientWrapper.instance.chatManager loadAllConversations:dic];
    }else if ([method isEqualToString:@"getMessage"]) {
        jsonObject = [EMClientWrapper.instance.chatManager getMessage:dic];
    }else if ([method isEqualToString:@"resendMessage"]) {
        jsonObject = [EMClientWrapper.instance.chatManager resendMessage:dic callbackId:callId];
    }else if ([method isEqualToString:@"sendMessage"]) {
        jsonObject = [EMClientWrapper.instance.chatManager sendMessage:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateChatMessage"]) {
        jsonObject =  [EMClientWrapper.instance.chatManager updateChatMessage:dic];
    }else if ([method isEqualToString:@"markAllChatMsgAsRead"]) {
        jsonObject =  [EMClientWrapper.instance.chatManager markAllChatMsgAsRead:dic];
    }
    
    const char *csStr = [Transfrom JsonObjectToCSString:jsonObject];
    if (csStr != NULL) {
        res = (char*)malloc(strlen(csStr) +1);
        strcpy(res, csStr);
    }
    
    return res;
}

void GroupManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"acceptInvitationFromGroup"]) {
        [EMClientWrapper.instance.groupManager acceptInvitationFromGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"addAdmin"]) {
        [EMClientWrapper.instance.groupManager addAdmin:dic callbackId:callId];
    }else if ([method isEqualToString:@"addMembers"]) {
        [EMClientWrapper.instance.groupManager addMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"addWhiteList"]) {
        [EMClientWrapper.instance.groupManager addWhiteList:dic callbackId:callId];
    }else if ([method isEqualToString:@"blockGroup"]) {
        [EMClientWrapper.instance.groupManager blockGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"blockMembers"]) {
        [EMClientWrapper.instance.groupManager blockMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateDescription"]) {
        [EMClientWrapper.instance.groupManager updateDescription:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateGroupSubject"]) {
        [EMClientWrapper.instance.groupManager updateGroupSubject:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateGroupOwner"]) {
        [EMClientWrapper.instance.groupManager updateGroupOwner:dic callbackId:callId];
    }else if ([method isEqualToString:@"checkIfInGroupWhiteList"]) {
        [EMClientWrapper.instance.groupManager isMemberInWhiteListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"createGroup"]) {
        [EMClientWrapper.instance.groupManager createGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"declineInvitationFromGroup"]) {
        [EMClientWrapper.instance.groupManager declineInvitationFromGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"declineJoinApplication"]) {
        [EMClientWrapper.instance.groupManager declineJoinApplication:dic callbackId:callId];
    }else if ([method isEqualToString:@"destroyGroup"]) {
        [EMClientWrapper.instance.groupManager destroyGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"downloadGroupSharedFile"]) {
        [EMClientWrapper.instance.groupManager downloadGroupSharedFile:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupAnnouncementFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupAnnouncementFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupMemberListFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupMemberListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupMuteListFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupMuteListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupSpecificationFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupSpecificationFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupWhiteListFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupWhiteListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getPublicGroupsFromServer"]) {
        [EMClientWrapper.instance.groupManager getPublicGroupsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"joinPublicGroup"]) {
        [EMClientWrapper.instance.groupManager joinPublicGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"leaveGroup"]) {
        [EMClientWrapper.instance.groupManager leaveGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"muteAllMembers"]) {
        [EMClientWrapper.instance.groupManager muteAllMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"muteMembers"]) {
        [EMClientWrapper.instance.groupManager muteMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeAdmin"]) {
        [EMClientWrapper.instance.groupManager removeAdmin:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeGroupSharedFile"]) {
        [EMClientWrapper.instance.groupManager removeGroupSharedFile:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeMembers"]) {
        [EMClientWrapper.instance.groupManager removeMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeWhiteList"]) {
        [EMClientWrapper.instance.groupManager removeWhiteList:dic callbackId:callId];
    }else if ([method isEqualToString:@"requestToJoinPublicGroup"]) {
        [EMClientWrapper.instance.groupManager requestToJoinPublicGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"unblockGroup"]) {
        [EMClientWrapper.instance.groupManager unblockGroup:dic callbackId:callId];
    }else if ([method isEqualToString:@"unblockMembers"]) {
        [EMClientWrapper.instance.groupManager unblockMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"unMuteAllMembers"]) {
        [EMClientWrapper.instance.groupManager unMuteAllMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"unMuteMembers"]) {
        [EMClientWrapper.instance.groupManager unMuteMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateGroupAnnouncement"]) {
        [EMClientWrapper.instance.groupManager updateGroupAnnouncement:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateGroupExt"]) {
        [EMClientWrapper.instance.groupManager updateGroupExt:dic callbackId:callId];
    }else if ([method isEqualToString:@"uploadGroupSharedFile"]) {
        [EMClientWrapper.instance.groupManager uploadGroupSharedFile:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupBlockListFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupBlockListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupFileListFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupFileListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getJoinedGroupsFromServer"]) {
        [EMClientWrapper.instance.groupManager getJoinedGroupsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"acceptJoinApplication"]) {
        [EMClientWrapper.instance.groupManager acceptJoinApplication:dic callbackId:callId];
    }
    
}

const char* GroupManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    char* ret = NULL;
    id jsonObject = nil;
    if ([method isEqualToString:@"getGroupWithId"]) {
        jsonObject = [EMClientWrapper.instance.groupManager getGroupWithId:dic];
    }else if([method isEqualToString:@"getJoinedGroups"]){
        jsonObject = [EMClientWrapper.instance.groupManager getJoinedGroups:dic];
    }
    
    const char *csStr = [Transfrom JsonObjectToCSString:jsonObject];
    if (csStr != NULL) {
        ret = (char*)malloc(strlen(csStr) +1);
        strcpy(ret, csStr);
    }
    
    return ret;
}

void RoomManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"addChatRoomAdmin"]) {
        [EMClientWrapper.instance.roomManager chatRoomAddAdmin:dic callbackId:callId];
    }else if ([method isEqualToString:@"blockChatRoomMembers"]) {
        [EMClientWrapper.instance.roomManager chatRoomBlockMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"changeChatRoomOwner"]) {
        [EMClientWrapper.instance.roomManager chatRoomChangeOwner:dic callbackId:callId];
    }else if ([method isEqualToString:@"changeChatRoomDescription"]) {
        [EMClientWrapper.instance.roomManager chatRoomUpdateDescription:dic callbackId:callId];
    }else if ([method isEqualToString:@"changeChatRoomSubject"]) {
        [EMClientWrapper.instance.roomManager chatRoomUpdateSubject:dic callbackId:callId];
    }else if ([method isEqualToString:@"createChatroom"]) {
        [EMClientWrapper.instance.roomManager createChatroom:dic callbackId:callId];
    }else if ([method isEqualToString:@"destroyChatRoom"]) {
        [EMClientWrapper.instance.roomManager destroyChatRoom:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchPublicChatRoomsFromServer"]) {
        [EMClientWrapper.instance.roomManager getChatroomsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchChatRoomAnnouncement"]) {
        [EMClientWrapper.instance.roomManager fetchChatroomAnnouncement:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchChatRoomBlockList"]) {
        [EMClientWrapper.instance.roomManager fetchChatroomBlockListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchChatRoomInfoFromServer"]) {
        [EMClientWrapper.instance.roomManager fetchChatroomInfoFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchChatRoomMembers"]) {
        [EMClientWrapper.instance.roomManager getChatroomMemberListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"fetchChatRoomMuteList"]) {
        [EMClientWrapper.instance.roomManager getChatroomMuteListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getChatRoom"]) {
        [EMClientWrapper.instance.roomManager getChatroom:dic callbackId:callId];
    }else if ([method isEqualToString:@"joinChatRoom"]) {
        [EMClientWrapper.instance.roomManager joinChatroom:dic callbackId:callId];
    }else if ([method isEqualToString:@"leaveChatRoom"]) {
        [EMClientWrapper.instance.roomManager leaveChatroom:dic callbackId:callId];
    }else if ([method isEqualToString:@"muteChatRoomMembers"]) {
        [EMClientWrapper.instance.roomManager chatRoomMuteMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeChatRoomAdmin"]) {
        [EMClientWrapper.instance.roomManager chatRoomRemoveAdmin:dic callbackId:callId];
    }else if ([method isEqualToString:@"removeChatRoomMembers"]) {
        [EMClientWrapper.instance.roomManager chatRoomRemoveMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"unBlockChatRoomMembers"]) {
        [EMClientWrapper.instance.roomManager chatRoomUnblockMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"unMuteChatRoomMembers"]) {
        [EMClientWrapper.instance.roomManager chatRoomUnmuteMembers:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateChatRoomAnnouncement"]) {
        [EMClientWrapper.instance.roomManager updateChatroomAnnouncement:dic callbackId:callId];
    }
}

const char* RoomManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    return NULL;
}

void PushManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
   if ([method isEqualToString:@"getPushConfigFromServer"]) {
        [EMClientWrapper.instance.pushManager getPushConfigFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"updateGroupPushService"]) {
        [EMClientWrapper.instance.pushManager updateGroupPushService:dic callbackId:callId];
    }else if ([method isEqualToString:@"PushNoDisturb"]) {
        [EMClientWrapper.instance.pushManager PushNoDisturb:dic callbackId:callId];
    }else if ([method isEqualToString:@"updatePushStyle"]) {
        [EMClientWrapper.instance.pushManager updatePushStyle:dic callbackId:callId];
    }else if ([method isEqualToString:@"updatePushNickname"]) {
        [EMClientWrapper.instance.pushManager updatePushNickname:dic callbackId:callId];
    }
}

const char* PushManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    char* ret = NULL;
    id jsonObject = nil;
    if ([method isEqualToString:@"getPushConfig"]) {
        jsonObject = [EMClientWrapper.instance.pushManager getPushConfig:dic];
    } else if ([method isEqualToString:@"getNoDisturbGroups"]) {
        jsonObject =[EMClientWrapper.instance.pushManager getNoDisturbGroups:dic];
    }
    
    const char *csStr = [Transfrom JsonObjectToCSString:jsonObject];
    if (csStr != NULL) {
        ret = (char*)malloc(strlen(csStr) +1);
        strcpy(ret, csStr);
    }
    
    return ret;
}

void Conversation_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"markMessageAsRead"]) {
        [EMClientWrapper.instance.conversationWrapper markMessageAsRead:dic];
    }else if ([method isEqualToString:@"syncConversationExt"]) {
        [EMClientWrapper.instance.conversationWrapper syncConversationExt:dic];
    }else if ([method isEqualToString:@"markAllMessagesAsRead"]) {
        [EMClientWrapper.instance.conversationWrapper markAllMessagesAsRead:dic];
    }else if ([method isEqualToString:@"loadMsgWithMsgType"]) {
        [EMClientWrapper.instance.conversationWrapper loadMsgWithMsgType:dic callbackId:callId];
    }else if ([method isEqualToString:@"loadMsgWithStartId"]) {
        [EMClientWrapper.instance.conversationWrapper loadMsgWithStartId:dic callbackId:callId];
    }else if ([method isEqualToString:@"loadMsgWithKeywords"]) {
        [EMClientWrapper.instance.conversationWrapper loadMsgWithKeywords:dic callbackId:callId];
    }else if ([method isEqualToString:@"loadMsgWithTime"]) {
        [EMClientWrapper.instance.conversationWrapper loadMsgWithTime:dic callbackId:callId];
    }
}

const char* Conversation_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    char* ret = NULL;
    id jsonObject = nil;
    if ([method isEqualToString:@"getUnreadMsgCount"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper getUnreadMsgCount:dic];
    }else if ([method isEqualToString:@"getLatestMessage"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper getLatestMessage:dic];
    }else if ([method isEqualToString:@"getLatestMessageFromOthers"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper getLatestMessageFromOthers:dic];
    }else if ([method isEqualToString:@"conversationExt"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper conversationExt:dic];
    }else if ([method isEqualToString:@"insertMessage"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper insertMessage:dic];
    }else if ([method isEqualToString:@"appendMessage"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper appendMessage:dic];
    }else if ([method isEqualToString:@"updateConversationMessage"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper updateConversationMessage:dic];
    }else if ([method isEqualToString:@"removeMessage"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper removeMessage:dic];
    }else if ([method isEqualToString:@"clearAllMessages"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper clearAllMessages:dic];
    }else if ([method isEqualToString:@"loadMsgWithId"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper loadMsgWithId:dic];
    }else if ([method isEqualToString:@"messageCount"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper messageCount:dic];
    }else if ([method isEqualToString:@"isThread"]) {
        jsonObject = [EMClientWrapper.instance.conversationWrapper isThread:dic];
    }
    
    const char *csStr = [Transfrom JsonObjectToCSString:jsonObject];
    if (csStr != NULL) {
        ret = (char*)malloc(strlen(csStr) +1);
        strcpy(ret, csStr);
    }
    
    return ret;
}

void UserInfoManager_MethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if([method isEqualToString:@"fetchUserInfoByUserId"]) {
        [EMClientWrapper.instance.userInfoManager fetchUserInfoById:dic callbackId:callId];
    } else if([method isEqualToString:@"updateOwnInfo"]) {
        [EMClientWrapper.instance.userInfoManager updateOwnUserInfo:dic callbackId:callId];
    } else if([method isEqualToString:@"updateOwnByAttribute"]) {
        [EMClientWrapper.instance.userInfoManager updateOwnUserInfoWithType:dic callbackId:callId];
    }
}

void PresenceManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if([method isEqualToString:@"publishPresence"]) {
        [EMClientWrapper.instance.presenceManager publishPresence:dic callbackId:callId];
    } else if ([method isEqualToString:@"subscribePresences"]) {
        [EMClientWrapper.instance.presenceManager subscribePresences:dic callbackId:callId];
    } else if ([method isEqualToString:@"unsubscribePresences"]) {
        [EMClientWrapper.instance.presenceManager unsubscribePresences:dic callbackId:callId];
    } else if ([method isEqualToString:@"fetchSubscribedMembers"]) {
        [EMClientWrapper.instance.presenceManager fetchSubscribedMembers:dic callbackId:callId];
    } else if ([method isEqualToString:@"fetchPresenceStatus"]) {
        [EMClientWrapper.instance.presenceManager fetchPresenceStatus:dic callbackId:callId];
    }
}

void ChatThreadManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom JsonObjectFromCSString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if([method isEqualToString:@"changeThreadName"]) {
        [EMClientWrapper.instance.chatThreadManager changeThreadName:dic callbackId:callId];
    } else if ([method isEqualToString:@"createThread"]) {
        [EMClientWrapper.instance.chatThreadManager createThread:dic callbackId:callId];
    } else if ([method isEqualToString:@"destroyThread"]) {
        [EMClientWrapper.instance.chatThreadManager destroyThread:dic callbackId:callId];
    } else if ([method isEqualToString:@"fetchMineJoinedThreadList"]) {
        [EMClientWrapper.instance.chatThreadManager fetchMineJoinedThreadList:dic callbackId:callId];
    } else if ([method isEqualToString:@"fetchThreadListOfGroup"]) {
        [EMClientWrapper.instance.chatThreadManager fetchThreadListOfGroup:dic callbackId:callId];
    } else if ([method isEqualToString:@"fetchThreadMembers"]) {
        [EMClientWrapper.instance.chatThreadManager fetchThreadMembers:dic callbackId:callId];
    } else if ([method isEqualToString:@"getLastMessageAccordingThreads"]) {
        [EMClientWrapper.instance.chatThreadManager getLastMessageAccordingThreads:dic callbackId:callId];
    } else if ([method isEqualToString:@"getThreadDetail"]) {
        [EMClientWrapper.instance.chatThreadManager getThreadDetail:dic callbackId:callId];
    } else if ([method isEqualToString:@"joinThread"]) {
        [EMClientWrapper.instance.chatThreadManager joinThread:dic callbackId:callId];
    } else if ([method isEqualToString:@"leaveThread"]) {
        [EMClientWrapper.instance.chatThreadManager leaveThread:dic callbackId:callId];
    } else if ([method isEqualToString:@"removeThreadMember"]) {
        [EMClientWrapper.instance.chatThreadManager removeThreadMember:dic callbackId:callId];
    }
}
