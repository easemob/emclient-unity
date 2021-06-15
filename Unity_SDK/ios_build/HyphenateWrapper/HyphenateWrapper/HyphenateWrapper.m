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
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"initializeSDKWithOptions"]) {
        [EMClientWrapper.instance initSDKWithDict:dic];
    }else if ([method isEqualToString:@"createAccount"]) {
        [EMClientWrapper.instance createAccount:dic callbackId:callId];
    }else if ([method isEqualToString:@"login"]) {
        [EMClientWrapper.instance login:dic callbackId:callId];
    }else if ([method isEqualToString:@"logout"]) {
        [EMClientWrapper.instance logout:dic callbackId:callId];
    }
}

const char* Client_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    if ([method isEqualToString:@"getCurrentUsername"]) {
        return [Transfrom DictToCString:@{@"getCurrentUsername":EMClient.sharedClient.currentUsername}];
    } else if ([method isEqualToString:@"isConnected"]) {
        return [Transfrom DictToCString:@{@"isConnected": @(EMClient.sharedClient.isConnected)}];
    } else if ([method isEqualToString:@"isLoggedIn"]) {
        return [Transfrom DictToCString:@{@"isLoggedIn": @(EMClient.sharedClient.isLoggedIn)}];
    }else if ([method isEqualToString:@"accessToken"]) {
        return [Transfrom DictToCString:@{@"accessToken": EMClient.sharedClient.accessUserToken}];
    }else {
        return "";
    }
}

void ContactManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"addContact"]) {
        [EMClientWrapper.instance.contactManager addContact:dic callbackId:callId];
    }else if ([method isEqualToString:@"deleteContact"]) {
        [EMClientWrapper.instance.contactManager deleteContact:dic callbackId:callId];
    }else if ([method isEqualToString:@"getAllContactsFromServer"]) {
        [EMClientWrapper.instance.contactManager getAllContactsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getAllContactsFromDB"]) {
        [EMClientWrapper.instance.contactManager getAllContactsFromDB:dic callbackId:callId];
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
    return NULL;
}

void ChatManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
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
    }else if ([method isEqualToString:@"updateChatMessage"]) {
        [EMClientWrapper.instance.chatManager updateChatMessage:dic callbackId:callId];
    }
}

const char* ChatManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"deleteConversation"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager deleteConversation:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"getConversation"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager getConversation:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"getUnreadMessageCount"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager getUnreadMessageCount:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"importMessages"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager importMessages:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"loadAllConversations"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager loadAllConversations:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"getMessage"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager getMessage:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"resendMessage"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager resendMessage:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"searchChatMsgFromDB"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager searchChatMsgFromDB:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }else if ([method isEqualToString:@"sendMessage"]) {
        NSDictionary *dict = [EMClientWrapper.instance.chatManager sendMessage:dic callbackId:callId];
        return [Transfrom DictToCString:dict];
    }
    return NULL;
}

void GroupManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
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
    }else if ([method isEqualToString:@"getGroupsWithoutPushNotification"]) {
        [EMClientWrapper.instance.groupManager getGroupsWithoutPushNotification:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupWhiteListFromServer"]) {
        [EMClientWrapper.instance.groupManager getGroupWhiteListFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupsWithoutPushNotification"]) {
        [EMClientWrapper.instance.groupManager getGroupsWithoutPushNotification:dic callbackId:callId];
    }else if ([method isEqualToString:@"getGroupWithId"]) {
        [EMClientWrapper.instance.groupManager getGroupWithId:dic callbackId:callId];
    }else if ([method isEqualToString:@"getJoinedGroups"]) {
        [EMClientWrapper.instance.groupManager getJoinedGroups:dic callbackId:callId];
    }else if ([method isEqualToString:@"getPublicGroupsFromServer"]) {
        [EMClientWrapper.instance.groupManager getPublicGroupsFromServer:dic callbackId:callId];
    }else if ([method isEqualToString:@"ignoreGroupPush"]) {
        [EMClientWrapper.instance.groupManager ignoreGroupPush:dic callbackId:callId];
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
    return NULL;
}

void RoomManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSString *method = [Transfrom NSStringFromCString:methodName];
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
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
    }else if ([method isEqualToString:@"getAllChatRooms"]) {
        [EMClientWrapper.instance.roomManager getAllChatrooms:dic callbackId:callId];
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
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
    NSString *callId = [Transfrom NSStringFromCString:callbackId];
    if ([method isEqualToString:@"getNoDisturbGroups"]) {
        [EMClientWrapper.instance.pushManager getNoDisturbGroups:dic callbackId:callId];
    }else if ([method isEqualToString:@"getPushConfig"]) {
        [EMClientWrapper.instance.pushManager getPushConfig:dic callbackId:callId];
    }else if ([method isEqualToString:@"getPushConfigFromServer"]) {
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
    
    return NULL;
}
