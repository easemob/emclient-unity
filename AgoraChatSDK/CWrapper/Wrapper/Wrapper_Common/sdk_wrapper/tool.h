#ifndef _SDK_WRAPPER_TOOL_IMPL_
#define _SDK_WRAPPER_TOOL_IMPL_

#include <map>
#include <vector>

#include "sdk_wrapper_internal.h"

#if defined(_WIN32)


#else



#endif

template<typename T>
static std::string convert2String(const T& from)
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

string JsonStringFromObject(const Value& obj);

string JsonStringFromVector(vector<string>& vec);
vector<string> JsonStringToVector(string& jstr);

string JsonStringFromMap(map<string, string>& map);
map<string, string> JsonStringToMap(string& jstr);


#endif