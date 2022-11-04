#ifndef _SDK_WRAPPER_INTERNAL_IMPL_
#define _SDK_WRAPPER_INTERNAL_IMPL_

#if defined(_WIN32)

	#define WIN32_LEAN_AND_MEAN
	#include <windows.h>
	#include <cstdint>
	
	typedef void(__stdcall* NativeListenerEvent)(const char* listener, const char* method, const char* jstr);

#else
	#include <unistd.h>
	#include <sys/time.h>

	typedef void(*NativeListenerEvent)(const char* listener, const char* method, const char* jstr);

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

#define CLIENT static_cast<EMClient *>(gClient)

const string STRING_CALLBACK_LISTENER = "callback";
const string STRING_CALLBACK_PROGRESS_LISTENER = "callbackProgress";

const string STRING_CLIENT_LISTENER   = "connectionListener";
const string STRING_CHATMANAGER_LISTENER = "chatManagerListener";
const string STRING_CONTACTMANAGER_LISTENER = "contactManagerListener";
const string STRING_GROUPMANAGER_LISTENER = "groupManagerListener";
const string STRING_ROOMMANAGER_LISTENER = "roomManagerListener";
const string STRING_PRESENCEMANAGER_LISTENER = "presenceManagerListener";
const string STRING_THREADMANAGER_LISTENER = "chatThreadManagerListener";
const string STRING_MULTIDEVICE_LISTENER = "multiDeviceListener";

#endif