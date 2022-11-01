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

	#define COMMON_WRAPPER_CALL
	#define HYPHENATE_API extern "C"

#endif

SDK_WRAPPER_API void SDK_WRAPPER_CALL AddListener_SDKWrapper(void* callback_handle);
SDK_WRAPPER_API void SDK_WRAPPER_CALL CleanListener_SDKWrapper();

// Client =====================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_InitWithOptions(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AddMultiDeviceListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_CreateAccount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Login(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Logout(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_CurrentUsername(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isLoggedIn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isConnected(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_LoginToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_LoginWithAgoraToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_RenewAgoraToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_ClearResource(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AutoLogin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// ChatManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_DownloadMessageAttachments(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_DownloadMessageThumbnail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_FetchHistoryMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_ConversationWithType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetConversationsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetUnreadMessageCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_InsertMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_LoadAllConversationsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_MarkAllConversationsAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RecallMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_ResendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_LoadMoreMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendReadAckForConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendReadAckForMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendReadAckForGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveMessagesBeforeTimestamp(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_DeleteConversationFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_FetchSupportLanguages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_TranslateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_FetchGroupReadAcks(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_ReportMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_AddReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetReactionList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetReactionDetail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

#endif