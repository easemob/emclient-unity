
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
	manager_map["ChatManager"] = func_map_chat_manager;
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