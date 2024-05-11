
#include <map>
#include <string>
#include "common_wrapper.h"
#include "common_wrapper_internal.h"
#include "sdk_wrapper.h"

#if defined(_WIN32)
typedef const char* (__stdcall *FUNC_CALL)(const char* jstr, const char* cbid, char* buf);
#else
typedef const char* (*FUNC_CALL)(const char* jstr, const char* cbid, char* buf);
#endif

typedef std::map<std::string, FUNC_CALL> FUNC_MAP;		// function name -> function handle
typedef std::map<std::string, FUNC_MAP>  MANAGER_MAP;   // manager name -> function map

//NativeListenerEvent gCallback;

MANAGER_MAP manager_map;

const int COMPILE_TYPE_MONO = 0;
const int COMPILE_TYPE_IL2CPP = 1;
int compile_type = 0;

void InitManagerMap()
{
	FUNC_MAP func_map_client;
	FUNC_MAP func_map_chat_manager;
	FUNC_MAP func_map_message_manager;
	FUNC_MAP func_map_group_manager;
	FUNC_MAP func_map_room_manager;
	FUNC_MAP func_map_contact_manager;
	FUNC_MAP func_map_conversation_manager;
	FUNC_MAP func_map_presence_manager;
	FUNC_MAP func_map_thread_manager;
	FUNC_MAP func_map_userinfo_manager;

	func_map_client["init"] = Client_InitWithOptions;
	func_map_client["createAccount"] = Client_CreateAccount;
	func_map_client["login"] = Client_Login;
	func_map_client["logout"] = Client_Logout;
	func_map_client["getCurrentUser"] = Client_CurrentUsername;
	func_map_client["isLoggedInBefore"] = Client_isLoggedIn;
	func_map_client["isConnected"] = Client_isConnected;
	func_map_client["getToken"] = Client_LoginToken;
	func_map_client["loginWithAgoraToken"] = Client_LoginWithAgoraToken;
	func_map_client["renewToken"] = Client_RenewToken;
	//func_map_client["autoLogin"] = Client_AutoLogin; // not support for platform
	func_map_client["clearResource"] = Client_ClearResource;

	//func_map_client["changeAppKey"] = Client_ChangeAppKey;
	//func_map_client["uploadLog"] = Client_UploadLog;
	//func_map_client["compressLogs"] = Client_CompressLogs;

    func_map_client["runDelegateTester"] = Client_RunDelegateTester;

	func_map_client["kickDevice"] = Client_KickDevice;
    func_map_client["kickDeviceWithToken"] = Client_KickDeviceWithToken;
	func_map_client["kickAllDevices"] = Client_KickDevices;
    func_map_client["kickAllDevicesWithToken"] = Client_KickDevicesWithToken;
	func_map_client["getLoggedInDevicesFromServer"] = Client_GetLoggedInDevicesFromServer;
    func_map_client["getLoggedInDevicesFromServerWithToken"] = Client_GetLoggedInDevicesFromServerWithToken;

    func_map_client["logDebug"] = Client_LogDebug;
    func_map_client["logWarn"] = Client_LogWarn;
    func_map_client["logError"] = Client_LogError;

	manager_map["EMClient"] = func_map_client;

	func_map_chat_manager["deleteConversation"] = ChatManager_RemoveConversation;
	func_map_chat_manager["downloadAttachment"] = ChatManager_DownloadMessageAttachments;
	func_map_chat_manager["downloadThumbnail"] = ChatManager_DownloadMessageThumbnail;
	func_map_chat_manager["fetchHistoryMessages"] = ChatManager_FetchHistoryMessages;
    func_map_chat_manager["fetchHistoryMessagesBy"] = ChatManager_FetchHistoryMessagesBy;
	func_map_chat_manager["getConversation"] = ChatManager_ConversationWithType;
	func_map_chat_manager["getThreadConversation"] = ChatManager_ConversationWithType;
	func_map_chat_manager["getConversationsFromServer"] = ChatManager_GetConversationsFromServer;
    func_map_chat_manager["getConversationsFromServerWithCursor"] = ChatManager_GetConversationsFromServerWithCursor;
    func_map_chat_manager["getConversationsFromServerWithCursorAndMark"] = ChatManager_GetConversationsFromServerWithCursorAndMark;
    func_map_chat_manager["getConversationsFromServerWithPage"] = ChatManager_GetConversationsFromServerWithPage;
	func_map_chat_manager["getUnreadMessageCount"] = ChatManager_GetUnreadMessageCount;
	func_map_chat_manager["importMessages"] = ChatManager_InsertMessages;
	func_map_chat_manager["loadAllConversations"] = ChatManager_LoadAllConversationsFromDB;
	func_map_chat_manager["getMessage"] = ChatManager_GetMessage;
	func_map_chat_manager["markAllChatMsgAsRead"] = ChatManager_MarkAllConversationsAsRead;
	func_map_chat_manager["recallMessage"] = ChatManager_RecallMessage;
	func_map_chat_manager["resendMessage"] = ChatManager_ResendMessage;
	func_map_chat_manager["searchChatMsgFromDB"] = ChatManager_LoadMoreMessages;
    func_map_chat_manager["searchChatMsgFromDBWithScope"] = ChatManager_LoadMoreMessagesWithScope;
	func_map_chat_manager["ackConversationRead"] = ChatManager_SendReadAckForConversation;
	func_map_chat_manager["sendMessage"] = ChatManager_SendMessage;
	func_map_chat_manager["ackMessageRead"] = ChatManager_SendReadAckForMessage;
	func_map_chat_manager["ackGroupMessageRead"] = ChatManager_SendReadAckForGroupMessage;
	func_map_chat_manager["updateChatMessage"] = ChatManager_UpdateMessage;
	func_map_chat_manager["deleteMessagesBeforeTimestamp"] = ChatManager_RemoveMessagesBeforeTimestamp;
	func_map_chat_manager["deleteRemoteConversation"] = ChatManager_DeleteConversationFromServer;
	func_map_chat_manager["fetchSupportLanguages"] = ChatManager_FetchSupportLanguages;
	func_map_chat_manager["translateMessage"] = ChatManager_TranslateMessage;
	func_map_chat_manager["asyncFetchGroupAcks"] = ChatManager_FetchGroupReadAcks;
	func_map_chat_manager["reportMessage"] = ChatManager_ReportMessage;
	func_map_chat_manager["addReaction"] = ChatManager_AddReaction;
	func_map_chat_manager["removeReaction"] = ChatManager_RemoveReaction;
	func_map_chat_manager["fetchReactionList"] = ChatManager_GetReactionList;
	func_map_chat_manager["fetchReactionDetail"] = ChatManager_GetReactionDetail;
    func_map_chat_manager["removeMessagesFromServerWithMsgIds"] = ChatManager_RemoveMessagesFromServerWithMsgIds;
    func_map_chat_manager["removeMessagesFromServerWithTs"] = ChatManager_RemoveMessagesFromServerWithTs;
    func_map_chat_manager["pinConversation"] = ChatManager_PinConversation;
    func_map_chat_manager["getMessagesCount"] = ChatManager_GetMessagesCount;
    func_map_chat_manager["removeEarlierHistoryMessages"] = ChatManager_RemoveEarlierHistoryMessages;
    func_map_chat_manager["modifyMessage"] = ChatManager_ModifyMessage;
    func_map_chat_manager["downloadCombineMessages"] = ChatManager_DownloadCombineMessages;
    func_map_chat_manager["markConversations"] = ChatManager_MarkConversations;
    func_map_chat_manager["deleteAllMessagesAndConversations"] = ChatManager_DeleteAllMessagesAndConversations;
    func_map_chat_manager["pinMessage"] = ChatManager_PinMessage;
    func_map_chat_manager["getPinnedMessagesFromServer"] = ChatManager_GetPinnedMessagesFromServer;

	manager_map["EMChatManager"] = func_map_chat_manager;

	//message manager
	func_map_message_manager["groupAckCount"] = ChatManager_GetGroupAckCount;
	func_map_message_manager["getHasDeliverAck"] = ChatManager_GetHasDeliverAck;
	func_map_message_manager["getHasReadAck"] = ChatManager_GetHasReadAck;
	func_map_message_manager["getReactionList"] = ChatManager_GetReactionListForMsg;
	func_map_message_manager["chatThread"] = ChatManager_GetChatThreadForMsg;
    func_map_message_manager["pinnedInfo"] = ChatManager_GetPinnedInfo;
	manager_map["EMMessageManager"] = func_map_message_manager;


	func_map_group_manager["requestToJoinGroup"] = GroupManager_ApplyJoinPublicGroup;
	func_map_group_manager["acceptInvitationFromGroup"] = GroupManager_AcceptInvitationFromGroup;
	func_map_group_manager["acceptJoinApplication"] = GroupManager_AcceptJoinGroupApplication;
	func_map_group_manager["addAdmin"] = GroupManager_AddAdmin;
	func_map_group_manager["addMembers"] = GroupManager_AddMembers;
	func_map_group_manager["addWhiteList"] = GroupManager_AddWhiteListMembers;
	func_map_group_manager["blockGroup"] = GroupManager_BlockGroupMessage;
	func_map_group_manager["blockMembers"] = GroupManager_BlockGroupMembers;
	func_map_group_manager["updateDescription"] = GroupManager_ChangeGroupDescription;
	func_map_group_manager["updateGroupSubject"] = GroupManager_ChangeGroupName;
	func_map_group_manager["updateGroupOwner"] = GroupManager_TransferGroupOwner;
	func_map_group_manager["isMemberInWhiteListFromServer"] = GroupManager_FetchIsMemberInWhiteList;
	func_map_group_manager["createGroup"] = GroupManager_CreateGroup;
	func_map_group_manager["declineInvitationFromGroup"] = GroupManager_DeclineInvitationFromGroup;
	func_map_group_manager["declineJoinApplication"] = GroupManager_DeclineJoinGroupApplication;
	func_map_group_manager["destroyGroup"] = GroupManager_DestoryGroup;
	func_map_group_manager["downloadGroupSharedFile"] = GroupManager_DownloadGroupSharedFile;
	func_map_group_manager["getGroupAnnouncementFromServer"] = GroupManager_FetchGroupAnnouncement;
	func_map_group_manager["getGroupBlockListFromServer"] = GroupManager_FetchGroupBans;
	func_map_group_manager["getGroupMemberListFromServer"] = GroupManager_FetchGroupMembers;
	func_map_group_manager["getGroupMuteListFromServer"] = GroupManager_FetchGroupMutes;
	func_map_group_manager["getGroupFileListFromServer"] = GroupManager_FetchGroupSharedFiles;
	func_map_group_manager["getGroupSpecificationFromServer"] = GroupManager_FetchGroupSpecification;
	func_map_group_manager["getGroupWhiteListFromServer"] = GroupManager_FetchGroupWhiteList;
	func_map_group_manager["getGroupWithId"] = GroupManager_GetGroupWithId;
	func_map_group_manager["getJoinedGroups"] = GroupManager_LoadAllMyGroupsFromDB;
	func_map_group_manager["getJoinedGroupsFromServer"] = GroupManager_FetchAllMyGroupsWithPage;
    func_map_group_manager["getJoinedGroupsFromServerSimple"] = GroupManager_FetchAllMyGroupsWithPageSimple;
	func_map_group_manager["getPublicGroupsFromServer"] = GroupManager_FetchPublicGroupsWithCursor;
	func_map_group_manager["joinPublicGroup"] = GroupManager_JoinPublicGroup;
	func_map_group_manager["leaveGroup"] = GroupManager_LeaveGroup;
	func_map_group_manager["muteAllMembers"] = GroupManager_MuteAllGroupMembers;
	func_map_group_manager["muteMembers"] = GroupManager_MuteGroupMembers;
	func_map_group_manager["removeAdmin"] = GroupManager_RemoveGroupAdmin;
	func_map_group_manager["removeGroupSharedFile"] = GroupManager_DeleteGroupSharedFile;
	func_map_group_manager["removeMembers"] = GroupManager_RemoveMembers;
	func_map_group_manager["removeWhiteList"] = GroupManager_RemoveWhiteListMembers;
	func_map_group_manager["unblockGroup"] = GroupManager_UnblockGroupMessage;
	func_map_group_manager["unblockMembers"] = GroupManager_UnblockGroupMembers;
	func_map_group_manager["unMuteAllMembers"] = GroupManager_UnMuteAllMembers;
	func_map_group_manager["unMuteMembers"] = GroupManager_UnmuteGroupMembers;
	func_map_group_manager["updateGroupAnnouncement"] = GroupManager_UpdateGroupAnnouncement;
	func_map_group_manager["updateGroupExt"] = GroupManager_ChangeGroupExtension;
	func_map_group_manager["uploadGroupSharedFile"] = GroupManager_UploadGroupSharedFile;
    func_map_group_manager["fetchMemberAttributes"] = GroupManager_FetchMemberAttributes;
    func_map_group_manager["setMemberAttributes"] = GroupManager_SetMemberAttributes;
    func_map_group_manager["fetchMyGroupsCount"] = GroupManager_FetchMyGroupsCount;

	manager_map["EMGroupManager"] = func_map_group_manager;


	func_map_room_manager["getChatRoom"] = RoomManager_GetChatRoom;
	func_map_room_manager["addChatRoomAdmin"] = RoomManager_AddRoomAdmin;
	func_map_room_manager["blockChatRoomMembers"] = RoomManager_BlockChatroomMembers;
	func_map_room_manager["changeChatRoomOwner"] = RoomManager_TransferChatroomOwner;
	func_map_room_manager["changeChatRoomDescription"] = RoomManager_ChangeChatroomDescription;
	func_map_room_manager["changeChatRoomSubject"] = RoomManager_ChangeRoomSubject;
	func_map_room_manager["createChatRoom"] = RoomManager_CreateRoom;
	func_map_room_manager["destroyChatRoom"] = RoomManager_DestroyChatroom;
	func_map_room_manager["fetchPublicChatRoomsFromServer"] = RoomManager_FetchChatroomsWithPage;
	func_map_room_manager["fetchChatRoomAnnouncement"] = RoomManager_FetchChatroomAnnouncement;
	func_map_room_manager["fetchChatRoomBlockList"] = RoomManager_FetchChatroomBans;
	func_map_room_manager["fetchChatRoomInfoFromServer"] = RoomManager_FetchChatroomSpecification;
	func_map_room_manager["fetchChatRoomMembers"] = RoomManager_FetchChatroomMembers;
	func_map_room_manager["fetchChatRoomMuteList"] = RoomManager_FetchChatroomMutes;
	func_map_room_manager["joinChatRoom"] = RoomManager_JoinChatroom;
	func_map_room_manager["leaveChatRoom"] = RoomManager_LeaveChatroom;
	func_map_room_manager["muteChatRoomMembers"] = RoomManager_MuteChatroomMembers;
	func_map_room_manager["removeChatRoomAdmin"] = RoomManager_RemoveChatroomAdmin;
	func_map_room_manager["removeChatRoomMembers"] = RoomManager_RemoveRoomMembers;
	func_map_room_manager["unBlockChatRoomMembers"] = RoomManager_UnblockChatroomMembers;
	func_map_room_manager["unMuteChatRoomMembers"] = RoomManager_UnmuteChatroomMembers;
	func_map_room_manager["updateChatRoomAnnouncement"] = RoomManager_UpdateChatroomAnnouncement;
	func_map_room_manager["muteAllChatRoomMembers"] = RoomManager_MuteAllChatroomMembers;
	func_map_room_manager["unMuteAllChatRoomMembers"] = RoomManager_UnMuteAllChatroomMembers;
	func_map_room_manager["addMembersToChatRoomWhiteList"] = RoomManager_AddWhiteListMembers;
	func_map_room_manager["removeMembersFromChatRoomWhiteList"] = RoomManager_RemoveWhiteListMembers;

	func_map_room_manager["fetchChatRoomWhiteListFromServer"] = RoomManager_FetchChatRoomWhiteListFromServer;
	func_map_room_manager["isMemberInChatRoomWhiteListFromServer"] = RoomManager_IsMemberInChatRoomWhiteListFromServer;

	func_map_room_manager["fetchChatRoomAttributes"] = RoomManager_FetchChatRoomAttributes;
	func_map_room_manager["setChatRoomAttributes"] = RoomManager_SetChatRoomAttributes;
	func_map_room_manager["removeChatRoomAttributes"] = RoomManager_RemoveChatRoomAttributes;

    func_map_room_manager["getAllChatRooms"] = RoomManager_GetAllChatRooms;

	manager_map["EMRoomManager"] = func_map_room_manager;

	func_map_contact_manager["acceptInvitation"] = ContactManager_AcceptInvitation;
	func_map_contact_manager["addContact"] = ContactManager_AddContact;
	func_map_contact_manager["addUserToBlockList"] = ContactManager_AddToBlackList;
	func_map_contact_manager["declineInvitation"] = ContactManager_DeclineInvitation;
	func_map_contact_manager["deleteContact"] = ContactManager_DeleteContact;
	func_map_contact_manager["getAllContactsFromDB"] = ContactManager_GetContactsFromDB;
	func_map_contact_manager["getAllContactsFromServer"] = ContactManager_GetContactsFromServer;
	func_map_contact_manager["getBlockListFromServer"] = ContactManager_GetBlackListFromServer;
	func_map_contact_manager["getSelfIdsOnOtherPlatform"] = ContactManager_GetSelfIdsOnOtherPlatform;
	func_map_contact_manager["removeUserFromBlockList"] = ContactManager_RemoveFromBlackList;
	func_map_contact_manager["getBlockListFromDB"] = ContactManager_GetBlockListFromDB;
    func_map_contact_manager["setContactRemark"] = ContactManager_SetContactRemark;
    func_map_contact_manager["fetchContactFromLocal"] = ContactManager_FetchContactFromLocal;
    func_map_contact_manager["fetchAllContactsFromLocal"] = ContactManager_FetchAllContactsFromLocal;
    func_map_contact_manager["fetchAllContactsFromServer"] = ContactManager_FetchAllContactsFromServer;
    func_map_contact_manager["fetchAllContactsFromServerByPage"] = ContactManager_FetchAllContactsFromServerByPage;
	manager_map["EMContactManager"] = func_map_contact_manager;


	func_map_conversation_manager["appendMessage"] = ConversationManager_AppendMessage;
	func_map_conversation_manager["clearAllMessages"] = ConversationManager_ClearAllMessages;
	func_map_conversation_manager["removeMessage"] = ConversationManager_RemoveMessage;
    func_map_conversation_manager["removeMessages"] = ConversationManager_RemoveMessages;
	func_map_conversation_manager["conversationExt"] = ConversationManager_ExtField;
	func_map_conversation_manager["insertMessage"] = ConversationManager_InsertMessage;
	func_map_conversation_manager["getLatestMessage"] = ConversationManager_LatestMessage;
	func_map_conversation_manager["getLatestMessageFromOthers"] = ConversationManager_LatestMessageFromOthers;
	func_map_conversation_manager["loadMsgWithId"] = ConversationManager_LoadMessage;
	func_map_conversation_manager["loadMsgWithStartId"] = ConversationManager_LoadMessages;
	func_map_conversation_manager["loadMsgWithKeywords"] = ConversationManager_LoadMessagesWithKeyword;
	func_map_conversation_manager["loadMsgWithMsgType"] = ConversationManager_LoadMessagesWithMsgType;
	func_map_conversation_manager["loadMsgWithTime"] = ConversationManager_LoadMessagesWithTime;
    func_map_conversation_manager["loadMsgWithScope"] = ConversationManager_LoadMessagesWithScope;
	func_map_conversation_manager["markAllMessagesAsRead"] = ConversationManager_MarkAllMessagesAsRead;
	func_map_conversation_manager["markMessageAsRead"] = ConversationManager_MarkMessageAsRead;
	func_map_conversation_manager["syncConversationExt"] = ConversationManager_SetExtField;
	func_map_conversation_manager["getConversationUnreadMsgCount"] = ConversationManager_UnreadMessagesCount;
	func_map_conversation_manager["messageCount"] = ConversationManager_MessagesCount;
	func_map_conversation_manager["updateConversationMessage"] = ConversationManager_UpdateMessage;
    func_map_conversation_manager["pinnedMessages"] = ConversationManager_PinnedMessages;
    func_map_conversation_manager["marks"] = ConversationManager_Marks;
	manager_map["EMConversationManager"] = func_map_conversation_manager;


	func_map_presence_manager["publishPresenceWithDescription"] = PresenceManager_PublishPresence;
	func_map_presence_manager["presenceSubscribe"] = PresenceManager_SubscribePresences;
	func_map_presence_manager["presenceUnsubscribe"] = PresenceManager_UnsubscribePresences;
	func_map_presence_manager["fetchSubscribedMembersWithPageNum"] = PresenceManager_FetchSubscribedMembers;
	func_map_presence_manager["fetchPresenceStatus"] = PresenceManager_FetchPresenceStatus;
	manager_map["EMPresenceManager"] = func_map_presence_manager;

	func_map_thread_manager["updateChatThreadSubject"] = ThreadManager_ChangeThreadSubject;
	func_map_thread_manager["createChatThread"] = ThreadManager_CreateThread;
	func_map_thread_manager["destroyChatThread"] = ThreadManager_DestroyThread;
	func_map_thread_manager["fetchChatThreadsWithParentId"] = ThreadManager_FetchThreadListOfGroup;
	func_map_thread_manager["fetchChatThreadMember"] = ThreadManager_FetchThreadMembers;
	func_map_thread_manager["fetchLastMessageWithChatThreads"] = ThreadManager_GetLastMessageAccordingThreads;
	func_map_thread_manager["fetchChatThreadDetail"] = ThreadManager_GetThreadDetail;
	func_map_thread_manager["joinChatThread"] = ThreadManager_JoinThread;
	func_map_thread_manager["leaveChatThread"] = ThreadManager_LeaveThread;
	func_map_thread_manager["removeMemberFromChatThread"] = ThreadManager_RemoveThreadMember;
	func_map_thread_manager["fetchJoinedChatThreads"] = ThreadManager_FetchMineJoinedThreadList;
	manager_map["EMThreadManager"] = func_map_thread_manager;

	func_map_userinfo_manager["fetchUserInfoById"] = UserInfoManager_FetchUserInfoByUserId;
	func_map_userinfo_manager["updateOwnUserInfo"] = UserInfoManager_UpdateOwnInfo;
	manager_map["EMUserInfoManager"] = func_map_userinfo_manager;

	//func_map_push_manager["getImPushConfig"] = PushManager_NotImplement;
	//manager_map["getNoPushUsers"] = func_map_push_manager;
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

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL FreeMemory_Common(void* p)
{
    FreeMemory_SDKWrapper(p);
}

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL Init_Common(int sdkType, int compileType, void* callback_handle)
{
    compile_type = compileType;
	Init_SDKWrapper(sdkType, compileType, callback_handle);
}

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL Uninit_Common()
{
	Uninit_SDKWrapper();
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

COMMON_WRAPPER_API const char*  COMMON_WRAPPER_CALL NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
	FUNC_CALL func = GetFuncHandle(manager, method);

	if (nullptr != func) {
		std::string json = GetUTF8FromUnicode(jstr);
		return func(json.c_str(), cbid, nullptr);
	}

	return nullptr;
}

std::string GetUTF8FromUnicode(const char* src)
{
	// Here cannot add judgement of strlen(src) == 0
	// since unicode maybe is 00 xx!!
	if (nullptr == src)
		return std::string("");

	std::string dst = std::string(src);

    // For il2cpp compile mode, no need to convert from unicode to utf8
    if (COMPILE_TYPE_IL2CPP == compile_type) {
        return dst;
    }

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
