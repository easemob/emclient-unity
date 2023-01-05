#ifndef _COMMON_WRAPPER_IMPL_
#define _COMMON_WRAPPER_IMPL_

#define COMMON_WRAPPER_CALL
#define COMMON_WRAPPER_API extern "C"

COMMON_WRAPPER_API void COMMON_WRAPPER_CALL Init_Common(int sdkType, int compileType, void* callback);
COMMON_WRAPPER_API void COMMON_WRAPPER_CALL Uninit_Common();
COMMON_WRAPPER_API void COMMON_WRAPPER_CALL NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
COMMON_WRAPPER_API const char* COMMON_WRAPPER_CALL NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid);


#endif
