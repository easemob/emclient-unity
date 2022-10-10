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

SDK_WRAPPER_API void SDK_WRAPPER_CALL AddListener_Common(void* callback_handle);
SDK_WRAPPER_API void SDK_WRAPPER_CALL CleanListener_Common();
SDK_WRAPPER_API void SDK_WRAPPER_CALL NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
SDK_WRAPPER_API int  SDK_WRAPPER_CALL NativeGet_Common(const char* manager, const char* method, const char* jstr, char* buf);

#endif