
#include <map>
#include <string>
#include "common_wrapper.h"
#include "common_wrapper_internal.h"
#include "sdk_wrapper.h"

typedef void (*FUNC_CALL)(const char* jstr, const char* cbid, char* buf);

typedef std::map<std::string, FUNC_CALL> FUNC_MAP;		// function name -> function handle
typedef std::map<std::string, FUNC_MAP>  MANAGER_MAP;   // manager name -> function map

//NativeListenerEvent gCallback;

MANAGER_MAP manager_map;

void InitManagerMap()
{
	FUNC_MAP func_map_client;
	FUNC_MAP func_map_chat_manager;
	FUNC_MAP func_map_group_manager;
	FUNC_MAP func_map_room_manager;
	FUNC_MAP func_map_contact_manager;
	FUNC_MAP func_map_conversation_manager;
	FUNC_MAP func_map_presence_manager;
	FUNC_MAP func_map_thread_manager;
	FUNC_MAP func_map_userinfo_manager;

	func_map_client["initWithOptions"] = Client_InitWithOptions;
	func_map_client["createAccount"] = Client_CreateAccount;
	func_map_client["login"] = Client_Login;
	func_map_client["logout"] = Client_Logout;
	func_map_client["currentUsername"] = Client_CurrentUsername;
	func_map_client["isLoggedIn"] = Client_isLoggedIn;
	func_map_client["isConnected"] = Client_isConnected;
	func_map_client["accessToken"] = Client_LoginToken;
	func_map_client["loginWithAgoraToken"] = Client_LoginWithAgoraToken;
	func_map_client["renewToken"] = Client_RenewAgoraToken;
	func_map_client["autoLogin"] = Client_AutoLogin;
	func_map_client["clearResource"] = Client_ClearResource;
	manager_map["Client"] = func_map_client;

	func_map_chat_manager["deleteConversation"] = ChatManager_RemoveConversation;
	func_map_chat_manager["downloadAttachment"] = ChatManager_DownloadMessageAttachments;
	func_map_chat_manager["downloadThumbnail"] = ChatManager_DownloadMessageThumbnail;
	func_map_chat_manager["fetchHistoryMessages"] = ChatManager_FetchHistoryMessages;
	func_map_chat_manager["getConversation"] = ChatManager_ConversationWithType;
	func_map_chat_manager["getConversationsFromServer"] = ChatManager_GetConversationsFromServer;
	func_map_chat_manager["getUnreadMessageCount"] = ChatManager_GetUnreadMessageCount;
	func_map_chat_manager["importMessages"] = ChatManager_InsertMessages;
	func_map_chat_manager["loadAllConversations"] = ChatManager_LoadAllConversationsFromDB;
	func_map_chat_manager["getMessage"] = ChatManager_GetMessage;
	func_map_chat_manager["getMessage"] = ChatManager_GetMessage;
	func_map_chat_manager["markAllChatMsgAsRead"] = ChatManager_MarkAllConversationsAsRead;
	func_map_chat_manager["recallMessage"] = ChatManager_RecallMessage;
	func_map_chat_manager["resendMessage"] = ChatManager_ResendMessage;
	func_map_chat_manager["searchChatMsgFromDB"] = ChatManager_LoadMoreMessages;
	func_map_chat_manager["ackConversationRead"] = ChatManager_SendReadAckForConversation;
	func_map_chat_manager["sendMessage"] = ChatManager_SendMessage;
	func_map_chat_manager["ackMessageRead"] = ChatManager_SendReadAckForMessage;
	func_map_chat_manager["sendReadAckForGroupMessage"] = ChatManager_SendReadAckForGroupMessage;
	func_map_chat_manager["updateChatMessage"] = ChatManager_UpdateMessage;
	func_map_chat_manager["removeMessageBeforeTimestamp"] = ChatManager_RemoveMessagesBeforeTimestamp;
	func_map_chat_manager["deleteConversationFromServer"] = ChatManager_DeleteConversationFromServer;
	func_map_chat_manager["fetchSupportLanguages"] = ChatManager_FetchSupportLanguages;
	func_map_chat_manager["translateMessage"] = ChatManager_TranslateMessage;
	func_map_chat_manager["fetchGroupReadAcks"] = ChatManager_FetchGroupReadAcks;
	func_map_chat_manager["reportMessage"] = ChatManager_ReportMessage;
	func_map_chat_manager["addReaction"] = ChatManager_AddReaction;
	func_map_chat_manager["removeReaction"] = ChatManager_RemoveReaction;
	func_map_chat_manager["getReactionList"] = ChatManager_GetReactionList;
	func_map_chat_manager["getReactionDetail"] = ChatManager_GetReactionDetail;
	//message manager
	func_map_chat_manager["getGroupAckCount"] = ChatManager_GetGroupAckCount;
	func_map_chat_manager["getHasDeliverAck"] = ChatManager_GetHasDeliverAck;
	func_map_chat_manager["getHasReadAck"] = ChatManager_GetHasReadAck;
	func_map_chat_manager["getReactionList"] = ChatManager_GetReactionListForMsg;
	func_map_chat_manager["getChatThread"] = ChatManager_GetChatThreadForMsg;

	manager_map["ChatManager"] = func_map_chat_manager;

	func_map_chat_manager["requestToJoinPublicGroup"] = GroupManager_ApplyJoinPublicGroup;
	func_map_chat_manager["acceptInvitationFromGroup"] = GroupManager_AcceptInvitationFromGroup;
	func_map_chat_manager["acceptJoinApplication"] = GroupManager_AcceptJoinGroupApplication;
	func_map_chat_manager["addAdmin"] = GroupManager_AddAdmin;
	func_map_chat_manager["addMembers"] = GroupManager_AddMembers;
	func_map_chat_manager["addWhiteList"] = GroupManager_AddWhiteListMembers;
	func_map_chat_manager["blockGroup"] = GroupManager_BlockGroupMessage;
	func_map_chat_manager["blockMembers"] = GroupManager_BlockGroupMembers;
	func_map_chat_manager["updateDescription"] = GroupManager_ChangeGroupDescription;
	func_map_chat_manager["updateGroupSubject"] = GroupManager_ChangeGroupName;
	func_map_chat_manager["updateGroupOwner"] = GroupManager_TransferGroupOwner;
	func_map_chat_manager["checkIfInGroupWhiteList"] = GroupManager_FetchIsMemberInWhiteList;
	func_map_chat_manager["createGroup"] = GroupManager_CreateGroup;
	func_map_chat_manager["declineInvitationFromGroup"] = GroupManager_DeclineInvitationFromGroup;
	func_map_chat_manager["declineJoinApplication"] = GroupManager_DeclineJoinGroupApplication;
	func_map_chat_manager["destroyGroup"] = GroupManager_DestoryGroup;
	func_map_chat_manager["downloadGroupSharedFile"] = GroupManager_DownloadGroupSharedFile;
	func_map_chat_manager["getGroupAnnouncementFromServer"] = GroupManager_FetchGroupAnnouncement;
	func_map_chat_manager["getGroupBlockListFromServer"] = GroupManager_FetchGroupBans;
	func_map_chat_manager["getGroupMemberListFromServer"] = GroupManager_FetchGroupMembers;
	func_map_chat_manager["getGroupMuteListFromServer"] = GroupManager_FetchGroupMutes;
	func_map_chat_manager["getGroupSpecificationFromServer"] = GroupManager_FetchGroupSpecification;
	func_map_chat_manager["getGroupWhiteListFromServer"] = GroupManager_FetchGroupWhiteList;
	func_map_chat_manager["getGroupWithId"] = GroupManager_GetGroupWithId;
	func_map_chat_manager["getJoinedGroupsFromServer"] = GroupManager_FetchAllMyGroupsWithPage;
	func_map_chat_manager["getPublicGroupsFromServer"] = GroupManager_FetchPublicGroupsWithCursor;
	func_map_chat_manager["joinPublicGroup"] = GroupManager_JoinPublicGroup;
	func_map_chat_manager["leaveGroup"] = GroupManager_LeaveGroup;
	func_map_chat_manager["muteAllMembers"] = GroupManager_MuteAllGroupMembers;
	func_map_chat_manager["muteMembers"] = GroupManager_MuteGroupMembers;
	func_map_chat_manager["removeAdmin"] = GroupManager_RemoveGroupAdmin;
	func_map_chat_manager["removeGroupSharedFile"] = GroupManager_DeleteGroupSharedFile;
	func_map_chat_manager["removeMembers"] = GroupManager_RemoveMembers;
	func_map_chat_manager["removeWhiteList"] = GroupManager_RemoveWhiteListMembers;
	func_map_chat_manager["unblockGroup"] = GroupManager_UnblockGroupMessage;
	func_map_chat_manager["unblockMembers"] = GroupManager_UnblockGroupMembers;
	func_map_chat_manager["unMuteAllMembers"] = GroupManager_UnMuteAllMembers;
	func_map_chat_manager["unMuteMembers"] = GroupManager_UnmuteGroupMembers;
	func_map_chat_manager["updateGroupAnnouncement"] = GroupManager_UpdateGroupAnnouncement;
	func_map_chat_manager["updateGroupExt"] = GroupManager_ChangeGroupExtension;
	func_map_chat_manager["uploadGroupSharedFile"] = GroupManager_UploadGroupSharedFile;
	manager_map["GroupManager"] = func_map_group_manager;


	func_map_chat_manager["addChatRoomAdmin"] = RoomManager_AddRoomAdmin;
	func_map_chat_manager["blockChatRoomMembers"] = RoomManager_BlockChatroomMembers;
	func_map_chat_manager["changeChatRoomOwner"] = RoomManager_TransferChatroomOwner;
	func_map_chat_manager["changeChatRoomDescription"] = RoomManager_ChangeChatroomDescription;
	func_map_chat_manager["changeChatRoomSubject"] = RoomManager_ChangeRoomSubject;
	func_map_chat_manager["createChatroom"] = RoomManager_CreateRoom;
	func_map_chat_manager["destroyChatRoom"] = RoomManager_DestroyChatroom;
	func_map_chat_manager["fetchPublicChatRoomsFromServer"] = RoomManager_FetchChatroomsWithPage;
	func_map_chat_manager["fetchChatRoomAnnouncement"] = RoomManager_FetchChatroomAnnouncement;
	func_map_chat_manager["fetchChatRoomBlockList"] = RoomManager_FetchChatroomBans;
	func_map_chat_manager["fetchChatRoomInfoFromServer"] = RoomManager_FetchChatroomSpecification;
	func_map_chat_manager["fetchChatRoomMembers"] = RoomManager_FetchChatroomMembers;
	func_map_chat_manager["fetchChatRoomMuteList"] = RoomManager_FetchChatroomMutes;
	func_map_chat_manager["joinChatRoom"] = RoomManager_JoinChatroom;
	func_map_chat_manager["leaveChatRoom"] = RoomManager_LeaveChatroom;
	func_map_chat_manager["muteChatRoomMembers"] = RoomManager_MuteChatroomMembers;
	func_map_chat_manager["removeChatRoomAdmin"] = RoomManager_RemoveChatroomAdmin;
	func_map_chat_manager["removeChatRoomMembers"] = RoomManager_RemoveRoomMembers;
	func_map_chat_manager["unBlockChatRoomMembers"] = RoomManager_UnblockChatroomMembers;
	func_map_chat_manager["unMuteChatRoomMembers"] = RoomManager_UnmuteChatroomMembers;
	func_map_chat_manager["updateChatRoomAnnouncement"] = RoomManager_UpdateChatroomAnnouncement;
	func_map_chat_manager["muteAllRoomMembers"] = RoomManager_MuteAllChatroomMembers;
	func_map_chat_manager["unMuteAllRoomMembers"] = RoomManager_UnMuteAllChatroomMembers;
	func_map_chat_manager["addWhiteListMembers"] = RoomManager_AddWhiteListMembers;
	func_map_chat_manager["removeWhiteListMembers"] = RoomManager_RemoveWhiteListMembers;
	manager_map["RoomManager"] = func_map_room_manager;

	func_map_chat_manager["acceptInvitation"] = ContactManager_AcceptInvitation;
	func_map_chat_manager["addContact"] = ContactManager_AddContact;
	func_map_chat_manager["addUserToBlockList"] = ContactManager_AddToBlackList;
	func_map_chat_manager["declineInvitation"] = ContactManager_DeclineInvitation;
	func_map_chat_manager["deleteContact"] = ContactManager_DeleteContact;
	func_map_chat_manager["getAllContactsFromDB"] = ContactManager_GetContactsFromDB;
	func_map_chat_manager["getAllContactsFromServer"] = ContactManager_GetContactsFromServer;
	func_map_chat_manager["getBlockListFromServer"] = ContactManager_GetBlackListFromServer;
	func_map_chat_manager["getSelfIdsOnOtherPlatform"] = ContactManager_GetSelfIdsOnOtherPlatform;
	func_map_chat_manager["removeUserFromBlockList"] = ContactManager_RemoveFromBlackList;
	manager_map["ContactManager"] = func_map_contact_manager;


	func_map_chat_manager["appendMessage"] = ConversationManager_AppendMessage;
	func_map_chat_manager["clearAllMessages"] = ConversationManager_ClearAllMessages;
	func_map_chat_manager["removeMessage"] = ConversationManager_RemoveMessage;
	func_map_chat_manager["conversationExt"] = ConversationManager_ExtField;
	func_map_chat_manager["insertMessage"] = ConversationManager_InsertMessage;
	func_map_chat_manager["getLatestMessage"] = ConversationManager_LatestMessage;
	func_map_chat_manager["getLatestMessageFromOthers"] = ConversationManager_LatestMessageFromOthers;
	func_map_chat_manager["loadMsgWithId"] = ConversationManager_LoadMessage;
	func_map_chat_manager["loadMsgWithStartId"] = ConversationManager_LoadMessages;
	func_map_chat_manager["loadMsgWithKeywords"] = ConversationManager_LoadMessagesWithKeyword;
	func_map_chat_manager["loadMsgWithMsgType"] = ConversationManager_LoadMessagesWithMsgType;
	func_map_chat_manager["loadMsgWithTime"] = ConversationManager_LoadMessagesWithTime;
	func_map_chat_manager["markAllMessagesAsRead"] = ConversationManager_MarkAllMessagesAsRead;
	func_map_chat_manager["markMessageAsRead"] = ConversationManager_MarkMessageAsRead;
	func_map_chat_manager["syncConversationExt"] = ConversationManager_SetExtField;
	func_map_chat_manager["getUnreadMsgCount"] = ConversationManager_UnreadMessagesCount;
	func_map_chat_manager["messageCount"] = ConversationManager_MessagesCount;
	func_map_chat_manager["updateConversationMessage"] = ConversationManager_UpdateMessage;
	manager_map["ConversationManager"] = func_map_conversation_manager;


	func_map_chat_manager["publishPresence"] = PresenceManager_PublishPresence;
	func_map_chat_manager["subscribePresences"] = PresenceManager_SubscribePresences;
	func_map_chat_manager["unsubscribePresences"] = PresenceManager_UnsubscribePresences;
	func_map_chat_manager["fetchSubscribedMembers"] = PresenceManager_FetchSubscribedMembers;
	func_map_chat_manager["fetchPresenceStatus"] = PresenceManager_FetchPresenceStatus;
	manager_map["PresenceManager"] = func_map_presence_manager;

	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_ChangeThreadSubject;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_CreateThread;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_DestroyThread;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_FetchThreadListOfGroup;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_FetchThreadMembers;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_GetLastMessageAccordingThreads;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_GetThreadDetail;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_JoinThread;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_LeaveThread;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_RemoveThreadMember;
	func_map_chat_manager["fetchPresenceStatus"] = ThreadManager_FetchMineJoinedThreadList;
	manager_map["ThreadManager"] = func_map_thread_manager;

	func_map_chat_manager["fetchUserInfoByUserId"] = UserInfoManager_FetchUserInfoByUserId;
	func_map_chat_manager["updateOwnInfo"] = UserInfoManager_UpdateOwnInfo;
	manager_map["UserInfoManager"] = func_map_userinfo_manager;
}

void CheckManagerMap()
{
	if (manager_map.size() == 0) InitManagerMap();
}

FUNC_CALL GetFuncHandle(const char* manager, const char* method)
{
	CheckManagerMap();

	if (nullptr == manager || strlen(manager) == 0 || nullptr == method || strlen(method) == 0) return nullptr;

	auto mit = manager_map.find(manager);
	if (manager_map.end() == mit) return nullptr;

	auto fit = mit->second.find(method);
	if (mit->second.end() == fit) return nullptr;

	return fit->second;
}

bool CheckClientHandle()
{
	// only check client handle
	return true;
}

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL AddListener_Common(void* callback_handle)
{
	//gCallback = (NativeListenerEvent)callback_handle;
	AddListener_SDKWrapper(callback_handle);
}

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL CleanListener_Common()
{
	//gCallback = nullptr;
	CleanListener_SDKWrapper();
}

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
	FUNC_CALL func = GetFuncHandle(manager, method);
	if (nullptr != func) {
		std::string json = GetUTF8FromUnicode(jstr);
		func(json.c_str(), cbid, nullptr);
		return;
	}
}

COMMON_WRAPPER_API int  COMMON_WRAPPER_CALL NativeGet_Common(const char* manager, const char* method, const char* jstr, char* buf, const char* cbid)
{
	FUNC_CALL func = GetFuncHandle(manager, method);
	if (nullptr != func) {
		std::string json = GetUTF8FromUnicode(jstr);
		func(json.c_str(), cbid, buf);
	}

	return 0;
}

std::string GetUTF8FromUnicode(const char* src)
{
	// Here cannot add judgement of strlen(src) == 0
	// since unicode maybe is 00 xx!!
	if (nullptr == src)
		return std::string("");

	std::string dst = std::string(src);

#ifdef _WIN32
	int len;
	len = WideCharToMultiByte(CP_UTF8, 0, (const wchar_t*)src, -1, NULL, 0, NULL, NULL);
	char* szUtf8 = new char[len + 1];
	memset(szUtf8, 0, len + 1);
	len = WideCharToMultiByte(CP_UTF8, 0, (const wchar_t*)src, -1, szUtf8, len, NULL, NULL);
	dst = szUtf8;
	delete szUtf8;
#endif

	return dst;
}