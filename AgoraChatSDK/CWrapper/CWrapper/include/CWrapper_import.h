#ifndef _CWRAPPER_IMPORT_H_
#define _CWRAPPER_IMPORT_H_

#if defined(_WIN32)

	extern "C" __declspec(dllimport) void __stdcall AddListener_Common(void* callback_handle);
	extern "C" __declspec(dllimport) void __stdcall CleanListener_Common();
	extern "C" __declspec(dllimport) void __stdcall NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
	extern "C" __declspec(dllimport) int  __stdcall NativeGet_Common(const char* manager, const char* method, const char* jstr, char* buf);

#elif defined(__APPLE__)

	// Add interface for NativeCall_IOS and NativeGet_IOS here

#else
	extern "C" void AddListener_Common(void* callback_handle);
	extern "C" void CleanListener_Common();
	extern "C" void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
	extern "C" int  NativeGet_Common(const char* manager, const char* method, const char* jstr, char* buf);

#endif

#endif
