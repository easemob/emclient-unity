#ifndef _COMMON_WRAPPER_INTERNAL_IMPL_
#define _COMMON_WRAPPER_INTERNAL_IMPL_

#if defined(_WIN32)

	#define WIN32_LEAN_AND_MEAN
	#include <windows.h>
	#include <cstdint>

	typedef void(__stdcall* NativeListenerEvent)(const char* listener, const char* method, const char* jstr);

#else

	typedef void(*NativeListenerEvent)(const char* listener, const char* method, const char* jstr);

#endif

#endif