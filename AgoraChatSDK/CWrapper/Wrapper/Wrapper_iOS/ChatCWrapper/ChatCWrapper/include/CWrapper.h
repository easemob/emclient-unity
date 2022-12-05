#ifndef _CWRAPPER_H_
#define _CWRAPPER_H_

#include "Api_decorator.h"

#if defined(_WIN32)
	typedef void(AGORA_CALL *NativeListenerEvent)(const char* listener, const char* method, const char* jstr);
#else
	typedef void(AGORA_CALL *NativeListenerEvent)(const char* listener, const char* method, const char* jstr);
#endif

HYPHENATE_API void AGORA_CALL Init(int sdkType, void* cb);
HYPHENATE_API void AGORA_CALL UnInit();
HYPHENATE_API void AGORA_CALL _NativeCall(const char* manager, const char* method, const char* jstr, const char* cbid);
HYPHENATE_API const char*  AGORA_CALL _NativeGet(const char* manager, const char* method, const char* jstr, const char* cbid);

#endif
