#ifndef _CWRAPPER_IMPORT_H_
#define _CWRAPPER_IMPORT_H_

#if defined(_WIN32)
    extern "C" __declspec(dllimport) void __stdcall Init_Common(int sdkType, int compileType, void* callback_handle);
    extern "C" __declspec(dllimport) void __stdcall Uninit_Common();
    extern "C" __declspec(dllimport) void __stdcall NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    extern "C" __declspec(dllimport) const char*  __stdcall NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    extern "C" __declspec(dllimport) void __stdcall FreeMemory_Common(void* p);
#else

    extern "C" void Init_Common(int sdkType, int compileType, void* callback_handle);
    extern "C" void Uninit_Common();
    extern "C" void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    extern "C" const char*  NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    extern "C" void FreeMemory_Common(void* p);

#endif

#endif
