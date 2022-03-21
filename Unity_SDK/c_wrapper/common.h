#ifndef _COMMON_H_
#define _COMMON_H_

#pragma once
#include <stdio.h>
#include "api_decorator.h"
#include "LogHelper.h"

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
}

#if defined(_WIN32)

#ifndef AGORACHAT_EXPORT
#define AGORACHAT_EXPORT
#endif

#elif defined(__APPLE__)

#elif defined(__ANDROID__) || defined(__linux__)

#endif

#endif //_COMMON_H_
