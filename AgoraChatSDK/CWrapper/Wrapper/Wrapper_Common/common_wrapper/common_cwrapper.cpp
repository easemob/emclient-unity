
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
	func_map_client["login"] = Client_Login;
	func_map_client["logout"] = Client_Logout;

	manager_map["Client"] = func_map_client;

	func_map_chat_manager["sendMessage"] = ChatManager_SendMessage;
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

void AddListener_Common(void* callback_handle)
{
	//gCallback = (NativeListenerEvent)callback_handle;
	AddListener_SDKWrapper(callback_handle);
}

void CleanListener_Common()
{
	//gCallback = nullptr;
	CleanListener_SDKWrapper();
}

void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
	FUNC_CALL func = GetFuncHandle(manager, method);
	if (nullptr != func) {
		func(jstr, cbid, nullptr);
		return;
	}
}

int NativeGet_Common(const char* manager, const char* method, const char* jstr, char* buf, const char* cbid)
{
	FUNC_CALL func = GetFuncHandle(manager, method);
	if (nullptr != func) {
		func(jstr, cbid, buf);
	}

	return 0;
}