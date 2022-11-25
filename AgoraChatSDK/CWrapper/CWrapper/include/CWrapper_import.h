#ifndef _CWRAPPER_IMPORT_H_
#define _CWRAPPER_IMPORT_H_

#if defined(_WIN32)
    extern "C" __declspec(dllimport) void __stdcall Init_Common(int sdkType, void* callback_handle);
    extern "C" __declspec(dllimport) void __stdcall Uninit_Common();
	extern "C" __declspec(dllimport) void __stdcall NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
	extern "C" __declspec(dllimport) const char*  __stdcall NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
#else

    extern "C" void Init_Common(int sdkType, void* callback_handle);
    extern "C" void Uninit_Common();
	extern "C" void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
	extern "C" const char*  NativeGet_Common(const char* manager, const char* method, const char* jstr, const char* cbid);

#endif

#endif
