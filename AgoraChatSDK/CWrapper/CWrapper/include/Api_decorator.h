#ifndef _API_DECORATOR_H_
#define _API_DECORATOR_H_

#include <cinttypes>

#if defined(_WIN32)

	#define WIN32_LEAN_AND_MEAN
	#include <windows.h>
	#include <cstdint>

	#define AGORA_CALL __stdcall

	#if defined(AGORACHAT_EXPORT)
		#define HYPHENATE_API extern "C" __declspec(dllexport)
	#else
		#define HYPHENATE_API extern "C" __declspec(dllimport)
	#endif

#elif defined(__APPLE__)

	#define AGORA_CALL
	#define HYPHENATE_API __attribute__((visibility("default"))) extern "C"

#else

	#define AGORA_CALL
	#define HYPHENATE_API extern "C"

#endif

#endif
