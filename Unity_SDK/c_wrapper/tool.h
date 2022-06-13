#ifndef _CWRAPPER_TOOL_H_
#define _CWRAPPER_TOOL_H_

#pragma once
#include <sstream>

#ifdef _WIN32
#include <windows.h>
#include <threadpoollegacyapiset.h>
#endif

#include "common.h"
#include "models.h"
#include "callbacks.h"
#include "emerror.h"
#include "utils/emutils.h"

using namespace easemob;

typedef void (*TIMER_FUNC)(int);

bool MandatoryCheck(const void* ptr, EMError& error);
bool MandatoryCheck(const char* ptr, EMError& error);
bool MandatoryCheck(const char* ptr);
bool MandatoryCheck(const void* ptr);

bool MandatoryCheck(const char* ptr1, void* ptr2, EMError& error);
bool MandatoryCheck(const char* ptr1, const char* ptr2, EMError& error);
bool MandatoryCheck(const char* ptr1, const char* ptr2);

bool MandatoryCheck(const char* ptr1, const char* ptr2, const char* ptr3, EMError& error);

std::string OptionalStrParamCheck(const char* ptr);

std::string GetLeftValue(const std::string& str);
std::string GetRightValue(const std::string& str);

char* GetPointer(const char* src);
std::string GetUTF8FromUnicode(const char* src);
std::string UTF8toANSI(std::string& strUTF8);
std::string ANSItoUTF8(std::string& strAnsi);

#ifndef _WIN32
void StartTimer(int interval, TIMER_FUNC timer_func);
#else
void StartTimer(int interval, TIMER_FUNC timer_func);
#endif
void StopTimer();

void EncryptAndSaveToFile(const std::string& plainMsg, const std::string& key, std::string fn="");
std::string DecryptAndGetFromFile(const std::string& key, std::string fn="");

#ifndef _WIN32
std::string GetMacUuid();
#endif

template<typename T>
static std::string convert2String(const T &from)
{
    std::stringstream stream;
    stream << from;
    return stream.str();
}

template<typename T>
static T convertFromString(const std::string& from)
{
    std::stringstream stream;
    stream << from;
    T to;
    stream >> to;
    return to;
}

#endif //_CWRAPPER_TOOL_H_
