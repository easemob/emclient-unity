#include "CWrapper.h"
#include "CWrapper_import.h"

NativeListenerEvent gCallback = nullptr;

HYPHENATE_API void AGORA_CALL Init(int sdkType, int compileType, NativeListenerEvent cb) {
    gCallback = cb;
    Init_Common(sdkType, compileType, (void*)gCallback);
}

HYPHENATE_API void AGORA_CALL UnInit() {
    gCallback = nullptr;
    Uninit_Common();
}

HYPHENATE_API void AGORA_CALL _NativeCall(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    NativeCall_Common(manager, method, jstr, cbid);
}

HYPHENATE_API const char* AGORA_CALL _NativeGet(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    return NativeGet_Common(manager, method, jstr, cbid);
}

HYPHENATE_API void AGORA_CALL FreeMemory(void* p)
{
    FreeMemory_Common(p);
}
