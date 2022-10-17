
#include <map>
#include <string>
#include "common_wrapper.h"
#include "common_wrapper_internal.h"

typedef void (*FUNC_CALL)(const char* jstr, char* buf_or_cbid);

typedef std::map<std::string, FUNC_CALL> FUNC_MAP;		// function name -> function handle
typedef std::map<std::string, FUNC_MAP>  MANAGER_MAP;   // manager name -> function map

NativeListenerEvent gCallback;

MANAGER_MAP manager_map_;

void InitManagerMap()
{
	FUNC_MAP func_map_chat_manager;

	//func_map_chat_manager["ChatManager_Call"] = ChatManager_Call;
	//func_map_chat_manager["ChatManager_Get"] = ChatManager_Get;

	manager_map_["ChatManager"] = func_map_chat_manager;
}

void CheckManagerMap()
{
	if (manager_map_.size() == 0) InitManagerMap();
}

FUNC_CALL GetFuncHandle(const char* manager, const char* method)
{
	CheckManagerMap();

	if (nullptr == manager || strlen(manager) == 0 || nullptr == method || strlen(method) == 0) return nullptr;

	auto mit = manager_map_.find(manager);
	if (manager_map_.end() == mit) return nullptr;

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
	gCallback = (NativeListenerEvent)callback_handle;
}

void CleanListener_Common()
{
	gCallback = nullptr;
}

void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
	FUNC_CALL func = GetFuncHandle(manager, method);
	if (nullptr != func) {
		func(jstr, const_cast<char*>(cbid));
		return;
	}
}

int NativeGet_Common(const char* manager, const char* method, const char* jstr, char* buf)
{
	FUNC_CALL func = GetFuncHandle(manager, method);
	if (nullptr != func) {
		func(jstr, buf);
	}

	return 0;
}