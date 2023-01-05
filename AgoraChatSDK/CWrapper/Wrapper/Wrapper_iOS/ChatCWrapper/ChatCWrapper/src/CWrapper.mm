#include "CWrapper.h"
#include "common_wrapper.h"

#import <Foundation/Foundation.h>

NativeListenerEvent gCallback = nullptr;


HYPHENATE_API void Init(int sdkType, int compileType, void* cb) {
    NSLog(@"init run!");
    gCallback = (NativeListenerEvent)cb;
    Init_Common(sdkType, (void *)cb);
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
