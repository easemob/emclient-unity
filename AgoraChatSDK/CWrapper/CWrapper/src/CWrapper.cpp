#include "CWrapper.h"
#include "CWrapper_import.h"

NativeListenerEvent gCallback = nullptr;

HYPHENATE_API void Init(int sdkType, NativeListenerEvent cb) {
    gCallback = cb;

#if defined(_WIN32)
    Init_Common(sdkType, (void*)gCallback);

#elif defined(__APPLE__)
    AddListener_IOS(gCallback);

#else
    Init_Common(sdkType, (void*)gCallback);
#endif

}
HYPHENATE_API void UnInit() {
    gCallback = nullptr;

#if defined(_WIN32)
    Uninit_Common();

#elif defined(__APPLE__)
    CleanListener_IOS();

#else
    Uninit_Common();

#endif
}

HYPHENATE_API void _NativeCall(const char* manager, const char* method, const char* jstr, const char* cbid)
{
#if defined(_WIN32)
	NativeCall_Common(manager, method, jstr, cbid);

#elif defined(__APPLE__)
	NativeCall_IOS(manager, method, jstr, cbid);

#else
	NativeCall_Common(manager, method, jstr, cbid);

#endif
}

HYPHENATE_API const char*  _NativeGet(const char* manager, const char* method, const char* jstr, const char* cbid)
{
#if defined(_WIN32)
	return NativeGet_Common(manager, method, jstr, cbid);

#elif defined(__APPLE__)
	return NativeGet_IOS(manager, method, jstr, buf);
#else
	return NativeGet_Common(manager, method, jstr, buf, cbid);

#endif
}
