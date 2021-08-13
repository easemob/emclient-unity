#ifndef _COMMON_H_
#define _COMMON_H_

#pragma once
#include <stdio.h>
#include "api_decorator.h"
#include "LogHelper.h"

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
#define CALLBACK static_cast<Callback *>(callback)
}

#if defined(_WIN32)
#define AGORACHAT_EXPORT

#elif defined(__APPLE__)

#elif defined(__ANDROID__) || defined(__linux__)

#endif

#endif //_COMMON_H_
