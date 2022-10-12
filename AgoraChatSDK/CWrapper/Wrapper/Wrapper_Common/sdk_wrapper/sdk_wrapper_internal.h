#ifndef _SDK_WRAPPER_INTERNAL_IMPL_
#define _SDK_WRAPPER_INTERNAL_IMPL_

#if defined(_WIN32)

	#define WIN32_LEAN_AND_MEAN
	#include <windows.h>
	#include <cstdint>

#else
	#include <unistd.h>
	#include <sys/time.h>
#endif

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

#include <string>

using namespace std;
using namespace easemob;

#endif