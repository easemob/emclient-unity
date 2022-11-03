#ifndef _SDK_WRAPPER_TOOL_IMPL_
#define _SDK_WRAPPER_TOOL_IMPL_

#include <map>
#include <sstream>
#include <vector>

#include "sdk_wrapper_internal.h"

#define GetJsonValue_String(jnode, name, default_value) (!jnode.HasParseError()  && !jnode.IsNull() \
                                                        && !jnode[name].IsNull() && jnode[name].IsString()) \
                                                        ? jnode[name].GetString() : default_value; 

#define GetJsonValue_Bool(jnode, name, default_value)   (!jnode.HasParseError() && !jnode.IsNull() \
                                                        && !jnode[name].IsNull() && jnode[name].IsBool()) \
                                                        ? jnode[name].GetBool() : default_value; 

#define GetJsonValue_Int(jnode, name, default_value)   (!jnode.HasParseError() && !jnode.IsNull() \
                                                        && !jnode[name].IsNull() && jnode[name].IsInt()) \
                                                        ? jnode[name].GetInt() : default_value;


#define GetJsonValue_Int64(jnode, name, default_value)   (!jnode.HasParseError() && !jnode.IsNull() \
                                                        && !jnode[name].IsNull() && jnode[name].IsInt64()) \
                                                        ? jnode[name].GetInt64() : default_value;

#define JSON_STARTOBJ         StringBuffer s; \
                              Writer<StringBuffer> writer(s); \
                              writer.StartObject();

#define JSON_ENDOBJ           writer.EndObject();

template<typename T>
static string convert2String(const T& from)
{
    stringstream stream;
    stream << from;
    return stream.str();
}

template<typename T>
static T convertFromString(const string& from)
{
    stringstream stream;
    stream << from;
    T to;
    stream >> to;
    return to;
}

void CallBack(const char* listener, const char* method, const char* jstr);
void CallBack(const char* method, const char* jstr);
void CallBackProgress(const char* method, const char* jstr);

bool CheckClientInitOrNot(const char* cbid);

string GetLeftValue(const string& str);
string GetRightValue(const string& str);

typedef void (*TIMER_FUNC)(int);
#ifndef _WIN32
void StartTimer(int interval, TIMER_FUNC timer_func);
#else
void StartTimer(int interval, TIMER_FUNC timer_func);
#endif
void StopTimer();

void EncryptAndSaveToFile(const string& plainMsg, const string& key, string fn = "");
string DecryptAndGetFromFile(const string& key, string fn = "");

#ifndef _WIN32
string GetMacUuid();
#endif

#endif