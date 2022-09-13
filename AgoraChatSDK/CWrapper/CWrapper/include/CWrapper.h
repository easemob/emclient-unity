#ifndef _CWRAPPER_H_
#define _CWRAPPER_H_

#include "api_decorator.h"

#if defined(_WIN32)
	typedef void(__stdcall * NativeListenerEvent)(const char* listener, const char* method, const char* jstr);
#else
	typedef void(*NativeListenerEvent)(const char* listener, const char* method, const char* jstr);
#endif

HYPHENATE_API void AddListener(NativeListenerEvent cb);
HYPHENATE_API void CleanListener();
HYPHENATE_API void _NativeCall(const char* manager, const char* method, const char* jstr, const char* cbid);
HYPHENATE_API int  _NativeGet(const char* manager, const char* method, const char* jstr, char* buf);

#endif
