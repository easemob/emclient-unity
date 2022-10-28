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
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Login(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Logout(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_CurrentUsername(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isLoggedIn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isConnected(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_LoginToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

// ChatManager ================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_AddListener();
SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr);

#endif