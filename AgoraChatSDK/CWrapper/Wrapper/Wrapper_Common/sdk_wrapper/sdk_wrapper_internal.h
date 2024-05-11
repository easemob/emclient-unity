#ifndef _SDK_WRAPPER_INTERNAL_IMPL_
#define _SDK_WRAPPER_INTERNAL_IMPL_

#if defined(_WIN32)

	#define WIN32_LEAN_AND_MEAN
	#include <windows.h>
	#include <cstdint>
	
	typedef void(__stdcall* NativeListenerEvent)(const char* listener, const char* method, const char* jstr);

#else
	#include <unistd.h>
	#include <sys/time.h>

	typedef void(*NativeListenerEvent)(const char* listener, const char* method, const char* jstr);

#endif

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

#include <string>

using namespace std;
using namespace easemob;

#define CLIENT static_cast<EMClient *>(gClient)


// Listeners name
const string STRING_CALLBACK_LISTENER = "callback";
const string STRING_CALLBACK_PROGRESS_LISTENER = "callbackProgress";

const string STRING_CLIENT_LISTENER   = "connectionListener";
const string STRING_CHATMANAGER_LISTENER = "chatListener";
const string STRING_CONTACTMANAGER_LISTENER = "contactListener";
const string STRING_GROUPMANAGER_LISTENER = "groupListener";
const string STRING_ROOMMANAGER_LISTENER = "chatRoomListener";
const string STRING_PRESENCEMANAGER_LISTENER = "presenceListener";
const string STRING_THREADMANAGER_LISTENER = "chatThreadListener";
const string STRING_MULTIDEVICE_LISTENER = "multiDeviceListener";

// ChatManagerDelegate
const string STRING_onMessagesReceived = "onMessagesReceived";
const string STRING_onCmdMessagesReceived = "onCmdMessagesReceived";
const string STRING_onMessagesRead = "onMessagesRead";
const string STRING_onGroupMessageRead = "onGroupMessageRead";
const string STRING_onReadAckForGroupMessageUpdated = "onReadAckForGroupMessageUpdated";
const string STRING_onMessagesDelivered = "onMessagesDelivered";
const string STRING_onMessagesRecalled = "onMessagesRecalled";
const string STRING_onConversationsUpdate = "onConversationsUpdate";
const string STRING_onConversationRead = "onConversationRead";
const string STRING_onMessageReactionDidChange = "messageReactionDidChange";
const string STRING_onMessageIdChanged = "onMessageIdChanged";
const string STRING_onMessageContentChanged = "onMessageContentChanged";
const string STRING_onMessagePinChanged = "onMessagePinChanged";

// ChatThreadManagerDelegate
const string STRING_onChatThreadCreate = "onChatThreadCreate";
const string STRING_onChatThreadUpdate = "onChatThreadUpdate";
const string STRING_onChatThreadDestroy = "onChatThreadDestroy";
const string STRING_onUserKickOutOfChatThread = "onUserKickOutOfChatThread";

// ContactManagerDelegate
const string STRING_onContactAdded = "onContactAdded";
const string STRING_onContactDeleted = "onContactDeleted";
const string STRING_onContactInvited = "onContactInvited";
const string STRING_onFriendRequestAccepted = "onFriendRequestAccepted";
const string STRING_onFriendRequestDeclined = "onFriendRequestDeclined";

// MultiDeviceDelegate
const string STRING_onContactMultiDevicesEvent = "onContactMultiDevicesEvent";
const string STRING_onGroupMultiDevicesEvent = "onGroupMultiDevicesEvent";
const string STRING_onUndisturbMultiDevicesEvent = "onUndisturbMultiDevicesEvent";
const string STRING_onThreadMultiDevicesEvent = "onThreadMultiDevicesEvent";
const string STRING_onRoamDeleteMultiDevicesEvent = "onRoamDeleteMultiDevicesEvent";
const string STRING_onConversationMultiDevicesEvent = "onConversationMultiDevicesEvent";

// PresenceManagerDelegate
const string STRING_onPresenceUpdated = "onPresenceUpdated";

// ConnectionDelegate
const string STRING_onConnected = "onConnected";
const string STRING_onDisconnected = "onDisconnected";
const string STRING_onLoggedOtherDevice = "onLoggedOtherDevice";
const string STRING_onRemovedFromServer = "onRemovedFromServer";
const string STRING_onForbidByServer = "onForbidByServer";
const string STRING_onChangedImPwd = "onChangedImPwd";
const string STRING_onLoginTooManyDevice = "onLoginTooManyDevice";
const string STRING_onKickedByOtherDevice = "onKickedByOtherDevice";
const string STRING_onAuthFailed = "onAuthFailed";
const string STRING_onTokenExpired = "onTokenExpired";
const string STRING_onTokenWillExpire = "onTokenWillExpire";
const string STRING_onAppActiveNumberReachLimitation = "onAppActiveNumberReachLimitation";

// GroupManagerDeleagate
const string STRING_onInvitationReceivedFromGroup = "onInvitationReceivedFromGroup";
const string STRING_onRequestToJoinReceivedFromGroup = "onRequestToJoinReceivedFromGroup";
const string STRING_onRequestToJoinAcceptedFromGroup = "onRequestToJoinAcceptedFromGroup";
const string STRING_onRequestToJoinDeclinedFromGroup = "onRequestToJoinDeclinedFromGroup";
const string STRING_onInvitationAcceptedFromGroup = "onInvitationAcceptedFromGroup";
const string STRING_onInvitationDeclinedFromGroup = "onInvitationDeclinedFromGroup";
const string STRING_onUserRemovedFromGroup = "onUserRemovedFromGroup";
const string STRING_onDestroyedFromGroup = "onDestroyedFromGroup";
const string STRING_onAutoAcceptInvitationFromGroup = "onAutoAcceptInvitationFromGroup";
const string STRING_onMuteListAddedFromGroup = "onMuteListAddedFromGroup";
const string STRING_onMuteListRemovedFromGroup = "onMuteListRemovedFromGroup";
const string STRING_onAdminAddedFromGroup = "onAdminAddedFromGroup";
const string STRING_onAdminRemovedFromGroup = "onAdminRemovedFromGroup";
const string STRING_onOwnerChangedFromGroup = "onOwnerChangedFromGroup";
const string STRING_onMemberJoinedFromGroup = "onMemberJoinedFromGroup";
const string STRING_onMemberExitedFromGroup = "onMemberExitedFromGroup";
const string STRING_onAnnouncementChangedFromGroup = "onAnnouncementChangedFromGroup";
const string STRING_onSharedFileAddedFromGroup = "onSharedFileAddedFromGroup";
const string STRING_onSharedFileDeletedFromGroup = "onSharedFileDeletedFromGroup";
const string STRING_onAddWhiteListMembersFromGroup = "onAddWhiteListMembersFromGroup";
const string STRING_onRemoveWhiteListMembersFromGroup = "onRemoveWhiteListMembersFromGroup";
const string STRING_onAllMemberMuteChangedFromGroup = "onAllMemberMuteChangedFromGroup";
const string STRING_onStateChangedFromGroup = "onStateChangedFromGroup";
const string STRING_onSpecificationChangedFromGroup = "onSpecificationChangedFromGroup";
const string STRING_onUpdateMemberAttributesFromGroup = "onUpdateMemberAttributesFromGroup";

// RoomManagerDelegate
const string STRING_onDestroyedFromRoom = "onDestroyedFromRoom";
const string STRING_onMemberJoinedFromRoom = "onMemberJoinedFromRoom";
const string STRING_onMemberExitedFromRoom = "onMemberExitedFromRoom";
const string STRING_onRemovedFromRoom = "onRemovedFromRoom";
const string STRING_onRemoveFromRoomByOffline = "onRemoveFromRoomByOffline";
const string STRING_onMuteListAddedFromRoom = "onMuteListAddedFromRoom";
const string STRING_onMuteListRemovedFromRoom = "onMuteListRemovedFromRoom";
const string STRING_onAdminAddedFromRoom = "onAdminAddedFromRoom";
const string STRING_onAdminRemovedFromRoom = "onAdminRemovedFromRoom";
const string STRING_onOwnerChangedFromRoom = "onOwnerChangedFromRoom";
const string STRING_onAnnouncementChangedFromRoom = "onAnnouncementChangedFromRoom";
const string STRING_onChatroomAttributesChanged = "onAttributesChangedFromRoom";
const string STRING_onChatroomAttributesRemoved = "onAttributesRemovedFromRoom";
const string STRING_onSpecificationChangedFromRoom = "onSpecificationChangedFromRoom";
const string STRING_onAddWhiteListMembersFromRoom = "onAddWhiteListMembersFromRoom";
const string STRING_onRemoveWhiteListMembersFromRoom = "onRemoveWhiteListMembersFromRoom";
const string STRING_onAllMemberMuteChangedFromRoom = "onAllMemberMuteChangedFromRoom";

#endif