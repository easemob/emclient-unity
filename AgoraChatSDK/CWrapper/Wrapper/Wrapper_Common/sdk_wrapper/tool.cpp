#include "tool.h"

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