#ifndef _API_DECORATOR_H_
#define _API_DECORATOR_H_

#include <cinttypes>

#if defined(_WIN32)

	#define WIN32_LEAN_AND_MEAN
	#include <windows.h>
	#include <cstdint>

	#if defined(AGORACHAT_EXPORT)
		#define HYPHENATE_API extern "C" __declspec(dllexport)
	#else
		#define HYPHENATE_API extern "C" __declspec(dllimport)
	#endif

#elif defined(__APPLE__)

	#define HYPHENATE_API __attribute__(visibility("default")) extern "C"

#elif define(__ANDROID__) || defined(__linux__)

	#if defined(__ANDROID__) && defined(FEATURE_RTM_STANDALONE_SDK)
		#define HYPHENATE_API extern "C"
	#else
		#define HYPHENATE_API __attribute__(visibility("default")) extern "C" 
	#endif

#else

	#define HYPHENATE_API extern "C"

#endif

#endif
