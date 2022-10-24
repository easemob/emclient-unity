
#include "emclient.h"
#include "utils/emencryptutils.h"
#include "utils/emutils.h"

#include "tool.h"

extern EMClient* gClient;
extern NativeListenerEvent gCallback;

void CallBack(const char* method, const char* jstr)
{
    if (nullptr == method || strlen(method) == 0)
        return;

    if (gCallback)
        gCallback(STRING_CALLBACK_LISTENER.c_str(), method, jstr);
}

void CallBackProgress(const char* method, const char* jstr)
{
    if (nullptr == method || strlen(method) == 0)
        return;

    if (gCallback)
        gCallback(STRING_CALLBACK_PROGRESS_LISTENER.c_str(), method, jstr);
}

string JsonStringFromResult(const char* cbid, int process, int code, const char* desc, const char* jstr)
{
    if (nullptr == cbid || strlen(cbid) == 0) return string();

    StringBuffer s;
    Writer<StringBuffer> writer(s);
    writer.StartObject();

    writer.Key("callbackId");
    writer.String(cbid);

    if (process >= 0) {
        writer.Key("progress");
        writer.Int(process);
    }

    if (code >= 0) {
        writer.Key("error");
        writer.StartObject();

        writer.Key("code");
        writer.Int(code);

        writer.Key("desc");
        if (nullptr != desc && strlen(desc) != 0) {
            writer.String(desc);
        }
        else {
            writer.String("");
        }

        writer.EndObject();
    }

    if (nullptr != jstr && strlen(jstr) != 0) {
        writer.Key("value");
        writer.String(jstr);
    }
    
    writer.EndObject();

    return s.GetString();
}

string JsonStringFromError(const char* cbid, int code, const char* desc)
{
    return JsonStringFromResult(cbid, -1, code, desc, nullptr);
}

string JsonStringFromErrorResult(const char* cbid, int code, const char* desc, const char* jstr)
{
    return JsonStringFromResult(cbid, -1, code, desc, jstr);
}

string JsonStringFromSuccess(const char* cbid)
{
    return JsonStringFromResult(cbid, -1, -1, nullptr, nullptr);
}

string JsonStringFromSuccessResult(const char* cbid, const char* jstr)
{
    return JsonStringFromResult(cbid, -1, -1, nullptr, jstr);
}

string JsonStringFromProcess(const char* cbid, int process)
{
    return JsonStringFromResult(cbid, process, -1, nullptr, nullptr);
}

bool CheckClientInitOrNot(const char* cbid)
{    
    if (nullptr == gClient) {
        string call_back_jstr = JsonStringFromError(cbid, (int)EMError::GENERAL_ERROR, "Sdk is not initialized!");
        CallBack(cbid, call_back_jstr.c_str());
        return false;
    }
    else {
        return true;
    }
}

string JsonStringFromObject(const Value& obj)
{
    StringBuffer s;
    Writer<StringBuffer> writer(s);
    obj.Accept(writer);
    return s.GetString();
}

string JsonStringFromVector(vector<string>& vec) {
    if (vec.size() == 0) return string("");

    StringBuffer s;
    Writer<StringBuffer> writer(s);

    writer.StartArray();
    for (int i = 0; i < vec.size(); i++) {
        writer.String(vec[i].c_str());
    }
    writer.EndArray();

    string data = s.GetString();

    return data;
}

string JsonStringFromMap(map<string, string>& map) {
    if (map.size() == 0) return string("");

    StringBuffer s;
    Writer<StringBuffer> writer(s);

    writer.StartObject();
    for (auto it : map) {
        writer.Key(it.first.c_str());
        writer.String(it.second.c_str());
    }
    writer.EndObject();

    string data = s.GetString();
    return data;
}

vector<string> JsonStringToVector(string& jstr) {
    vector<string> vec;
    if (jstr.length() < 3) return vec;

    Document d;
    if (!d.Parse(jstr.data()).HasParseError()) {
        if (d.IsArray() == true) {
            int size = d.Size();
            for (int i = 0; i < size; i++) {
                vec.push_back(d[i].GetString());
            }
        }
    }
    return vec;
}

map<string, string> JsonStringToMap(string& jstr) {
    map<string, string> map;
    if (jstr.length() < 3) return map;

    Document d;
    if (!d.Parse(jstr.data()).HasParseError()) {
        for (auto iter = d.MemberBegin(); iter != d.MemberEnd(); ++iter) {
            auto key = iter->name.GetString();
            auto value = iter->value.GetString();

            map.insert(pair<string, string>(key, value));
        }
    }
    return map;
}