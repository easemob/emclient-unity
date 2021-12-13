#ifndef _API_DECORATOR_H_
#define _API_DECORATOR_H_

#pragma once

#include <cinttypes>
#if defined(_WIN32)
#define WIN32_LEAN_AND_MEAN
#include <windows.h>

#include <cstdint>
#define AGORA_CALL __cdecl
#if defined(AGORACHAT_EXPORT)
#define HYPHENATE_API extern "C" __declspec(dllexport)
#else
#define HYPHENATE_API extern "C" __declspec(dllimport)
#endif
#define _AGORA_CPP_API

#elif defined(__APPLE__)
#define HYPHENATE_API __attribute__((visibility("default"))) extern "C"
#define AGORA_CALL
#define _AGORA_CPP_API

#elif defined(__ANDROID__) || defined(__linux__)
#if defined(__ANDROID__) && defined(FEATURE_RTM_STANDALONE_SDK)
#define HYPHENATE_API extern "C"
#define _AGORA_CPP_API
#else
#define HYPHENATE_API extern "C" __attribute__((visibility("default")))
#define _AGORA_CPP_API __attribute__((visibility("default")))
#endif
#define AGORA_CALL

#else
#define HYPHENATE_API extern "C"
#define AGORA_CALL
#define _AGORA_CPP_API
#endif

#endif //_API_DECORATOR_H_
