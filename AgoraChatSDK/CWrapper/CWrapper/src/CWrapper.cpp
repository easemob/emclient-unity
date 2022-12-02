#include "CWrapper.h"
#include "CWrapper_import.h"

// #include <TargetConditionals.h>

NativeListenerEvent gCallback = nullptr;

HYPHENATE_API void Init(int sdkType, NativeListenerEvent cb) {
    gCallback = cb;
    Init_Common(sdkType, (void*)gCallback);
}

HYPHENATE_API void UnInit() {
    gCallback = nullptr;
    Uninit_Common();
}

HYPHENATE_API void _NativeCall(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    NativeCall_Common(manager, method, jstr, cbid);
}

HYPHENATE_API const char*  _NativeGet(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    return NativeGet_Common(manager, method, jstr, cbid);
}
