#ifndef _SDK_WRAPPER_IMPL_
#define _SDK_WRAPPER_IMPL_

#if defined(_WIN32)

	#define SDK_WRAPPER_CALL __stdcall

	#if defined(SDK_WRAPPER_EXPORT)
		#define SDK_WRAPPER_API extern "C" __declspec(dllexport)
	#else
		#define SDK_WRAPPER_API extern "C" __declspec(dllimport)
	#endif

#else

	#define SDK_WRAPPER_CALL
	#define SDK_WRAPPER_API extern "C"

#endif

SDK_WRAPPER_API void SDK_WRAPPER_CALL Init_SDKWrapper(int sdkType, int compileType, void* callback_handle);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Uninit_SDKWrapper();

SDK_WRAPPER_API void SDK_WRAPPER_CALL FreeMemory_SDKWrapper(void* p);

// Client =====================================================================
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_InitWithOptions(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_RemoveListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_CreateAccount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_Login(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_Logout(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_CurrentUsername(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_isLoggedIn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_isConnected(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LoginToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LoginWithAgoraToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_RenewToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_ClearResource(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_ChangeAppKey(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_UploadLog(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_CompressLogs(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDevice(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDeviceWithToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDevices(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDevicesWithToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_GetLoggedInDevicesFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_GetLoggedInDevicesFromServerWithToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LogDebug(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LogWarn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LogError(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_ConnectionDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// ChatManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DownloadMessageAttachments(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DownloadMessageThumbnail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchHistoryMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchHistoryMessagesBy(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ConversationWithType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServerWithCursor(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServerWithCursorAndMark(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetUnreadMessageCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_InsertMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_LoadAllConversationsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_MarkAllConversationsAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RecallMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ResendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_LoadMoreMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_LoadMoreMessagesWithScope(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendReadAckForConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendReadAckForMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendReadAckForGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveMessagesBeforeTimestamp(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DeleteConversationFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchSupportLanguages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_TranslateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchGroupReadAcks(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ReportMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_AddReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetReactionList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetReactionDetail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServerWithPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveMessagesFromServerWithMsgIds(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveMessagesFromServerWithTs(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_PinConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetMessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveEarlierHistoryMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ModifyMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DownloadCombineMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetPinnedInfo(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_MarkConversations(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DeleteAllMessagesAndConversations(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_PinMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetPinnedMessagesFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// MessageManager-------------------------------------------------------------
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetGroupAckCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetHasDeliverAck(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetHasReadAck(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetReactionListForMsg(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetChatThreadForMsg(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// GroupManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_RemoveListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ApplyJoinPublicGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AcceptInvitationFromGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AcceptJoinGroupApplication(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AddAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AddMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AddWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_BlockGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_BlockGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ChangeGroupDescription(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ChangeGroupName(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_TransferGroupOwner(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchIsMemberInWhiteList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_CreateGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DeclineInvitationFromGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DeclineJoinGroupApplication(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DestoryGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DownloadGroupSharedFile(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupBans(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupSharedFiles(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupMutes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupSpecification(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupWhiteList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_GetGroupWithId(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_LoadAllMyGroupsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchAllMyGroupsWithPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchAllMyGroupsWithPageSimple(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchPublicGroupsWithCursor(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_JoinPublicGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_LeaveGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_MuteAllGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_MuteGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RemoveGroupAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DeleteGroupSharedFile(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RemoveMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RemoveWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnblockGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnblockGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnMuteAllMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnmuteGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UpdateGroupAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ChangeGroupExtension(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UploadGroupSharedFile(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchMemberAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_SetMemberAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchMyGroupsCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// RoomManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_RemoveListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_AddRoomAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_BlockChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_TransferChatroomOwner(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_ChangeChatroomDescription(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_ChangeRoomSubject(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_CreateRoom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_DestroyChatroom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatroomsWithPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatroomAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatroomBans(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatroomSpecification(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatroomMutes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_JoinChatroom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_LeaveChatroom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_MuteChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_RemoveChatroomAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_RemoveRoomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_UnblockChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_UnmuteChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_UpdateChatroomAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_MuteAllChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_UnMuteAllChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_AddWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_RemoveWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatRoomWhiteListFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_IsMemberInChatRoomWhiteListFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_FetchChatRoomAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_SetChatRoomAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_RemoveChatRoomAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_GetChatRoom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_GetAllChatRooms(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL RoomManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// ContactManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_RemoveListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_AcceptInvitation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_AddContact(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_AddToBlackList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_DeclineInvitation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_DeleteContact(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetContactsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetContactsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetBlackListFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetSelfIdsOnOtherPlatform(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_RemoveFromBlackList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetBlockListFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_SetContactRemark(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchContactFromLocal(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchAllContactsFromLocal(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchAllContactsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchAllContactsFromServerByPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// ConversationManager ================================================================
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_AppendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_ClearAllMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_RemoveMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_RemoveMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_ExtField(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_InsertMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LatestMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LatestMessageFromOthers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithKeyword(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithMsgType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithTime(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithScope(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_MarkAllMessagesAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_MarkMessageAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_SetExtField(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_UnreadMessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_MessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_PinnedMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_Marks(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// PresenceManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL PresenceManager_AddListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_PublishPresence(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_SubscribePresences(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_UnsubscribePresences(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_FetchSubscribedMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_FetchPresenceStatus(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// ThreadManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL ThreadManager_AddListener();
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_ChangeThreadSubject(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_CreateThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_DestroyThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_FetchThreadListOfGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_FetchThreadMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_GetLastMessageAccordingThreads(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_GetThreadDetail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_JoinThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_LeaveThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_RemoveThreadMember(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_FetchMineJoinedThreadList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);


// UserInfoManager ================================================================
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL UserInfoManager_FetchUserInfoByUserId(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL UserInfoManager_UpdateOwnInfo(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// PushManager ================================================================
SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PushManager_NotImplement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
#endif
