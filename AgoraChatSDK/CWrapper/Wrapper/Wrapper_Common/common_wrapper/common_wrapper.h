#ifndef _COMMON_WRAPPER_IMPL_
#define _COMMON_WRAPPER_IMPL_

#if defined(_WIN32)

	#define COMMON_WRAPPER_CALL __stdcall

	#if defined(COMMON_WRAPPER_EXPORT)
		#define COMMON_WRAPPER_API extern "C" __declspec(dllexport)
	#else
		#define COMMON_WRAPPER_API extern "C" __declspec(dllimport)
	#endif

#else

	#define COMMON_WRAPPER_CALL
	#define HYPHENATE_API extern "C"

#endif

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL Init_Common(int sdkType, void* callback_handle);
COMMON_WRAPPER_API void COMMON_WRAPPER_CALL Uninit_Common();
COMMON_WRAPPER_API void COMMON_WRAPPER_CALL NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
COMMON_WRAPPER_API const char*  COMMON_WRAPPER_CALL NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid);

#endif