#ifndef _CWRAPPER_TOOL_H_
#define _CWRAPPER_TOOL_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"
#include "emerror.h"

using namespace easemob;

bool MandatoryCheck(const void* ptr, EMError& error);
bool MandatoryCheck(const void* ptr1, const void* ptr2, EMError& error);
bool MandatoryCheck(const void* ptr1, const void* ptr2, const void* ptr3, EMError& error);
std::string OptionalStrParamCheck(const char* ptr);

#endif //_CWRAPPER_TOOL_H_
