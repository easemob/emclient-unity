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

// Client =====================================================================
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_InitWithOptions(const char* json);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Login(const char* json);
SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Logout(const char* json);


// ChatManager ================================================================

SDK_WRAPPER_API int  SDK_WRAPPER_CALL ChatManager_SendMessage(const char* json);

#endif