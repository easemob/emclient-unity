
#include "emclient.h"
#include "utils/emencryptutils.h"
#include "utils/emutils.h"

#include "tool.h"

extern EMClient* gClient;
extern NativeListenerEvent gCallback;


void CallBack(const char* listener, const char* method, const char* jstr)
{
    if (nullptr == listener || nullptr == method || strlen(method) == 0)
        return;

    if (gCallback)
        gCallback(listener, method, jstr);
}

void CallBack(const char* method, const char* jstr)
{
    CallBack(STRING_CALLBACK_LISTENER.c_str(), method, jstr);
}

void CallBackProgress(const char* method, const char* jstr)
{
    CallBack(STRING_CALLBACK_PROGRESS_LISTENER.c_str(), method, jstr);
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

string JsonStringFromVector(const vector<string>& vec) {
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

string JsonStringFromMap(const map<string, string>& map) {
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

string JsonStringFromCursorResult(string cursor, string result)
{
    StringBuffer s;
    Writer<StringBuffer> writer(s);

    writer.StartObject();
    {
        writer.Key("cursor");
        writer.String(cursor.c_str());

        writer.Key("list");
        writer.String(result.c_str());
    }
    writer.EndObject();

    std::string data = s.GetString();
    return data;
}

string GetLeftValue(const string& str)
{
    if (str.size() == 0) return "";
    string::size_type pos;
    pos = str.find("=");
    if (string::npos == pos) return "";
    return string(str, 0, pos - 0);
}

string GetRightValue(const string& str)
{
    if (str.size() == 0) return "";
    string::size_type pos;
    pos = str.find("=");
    if (string::npos == pos) return "";
    if (pos == str.size() - 1) return "";
    return string(str, pos + 1, str.size() - pos - 1);
}

#ifndef _WIN32
void StartTimer(int interval, TIMER_FUNC timer_func)
{
    StopTimer();

    struct itimerval tick;
    signal(SIGALRM, timer_func);

    memset(&tick, 0, sizeof(tick));

    //timeout to run first time
    tick.it_value.tv_sec = interval;
    tick.it_value.tv_usec = 0;

    //the interval time for clock
    tick.it_interval.tv_sec = interval;
    tick.it_interval.tv_usec = 0;


    if (setitimer(ITIMER_REAL, &tick, NULL) < 0) {
        LOG("Error: start timer for token failed.");
        return;
    }
}

void StopTimer()
{
    struct itimerval tick;
    memset(&tick, 0, sizeof(tick));
    if (setitimer(ITIMER_REAL, &tick, NULL) < 0) {
        LOG("ERROR: stop timer for token failed.");
        return;
    }
}

#else

int PARAM_INTERVAL = 0;
int RUN_INTERVAL = 0;
bool ISRUN = false;
bool STOP = false;

void StartTimer(int interval, TIMER_FUNC timer_func)
{
    if (interval <= 0) {
        return;
    }

    PARAM_INTERVAL = interval;
    if (ISRUN) return;

    std::thread t([=]() {
        RUN_INTERVAL = PARAM_INTERVAL;
        int tick_count = 0;
        while (!STOP) {
            ISRUN = true;
            Sleep(1 * 1000);
            tick_count++;

            if (STOP) {
                break; // if STOP is true, stop at once
            }

            if (tick_count == RUN_INTERVAL) { // timer fired
                timer_func(0);
                tick_count = 0; // restart count
            }

            if (RUN_INTERVAL != PARAM_INTERVAL) { // change interval
                RUN_INTERVAL = PARAM_INTERVAL;
                tick_count = 0; // restart count
                continue;
            }
        }
        ISRUN = false;
        });
    t.detach();
}

void StopTimer()
{
    STOP = true;
}

#endif

void EncryptAndSaveToFile(const std::string& plainMsg, const std::string& key, std::string fn)
{
    if (plainMsg.size() == 0 || key.size() == 0) {
        return;
    }

    // get file name
    std::string fileName = "";
    if (fn.size() != 0)
        fileName = fn;
    else {
        fileName = "./sdkdata/easemobDB/.atconfig";
#ifdef _WIN32
        fileName = "./sdkdata/easemobDB/.atconfig";
#endif
    }

    // open file
    FILE* f = nullptr;
    f = fopen(fileName.c_str(), "w");

    // clear old contents
    fseek(f, 0, SEEK_END);
    long fsize = ftell(f);
    if (fsize > 0) {
        fclose(f);
#ifndef _WIN32
        truncate(fileName.c_str(), 0);
#else
        DeleteFileA(fileName.c_str());
#endif
        f = fopen(fileName.c_str(), "wb");
    }

    // get encrypt key
    EMEncryptUtils* encrypt = EMEncryptUtils::createInstance();
    unsigned char bytes[16];
    EMEncryptCalculateUtil::getAESKey(key, key, bytes, encrypt);

    // encrypt
    std::string ret = encrypt->aesEncrypt(plainMsg, bytes, 16);

    // save result
    fwrite(ret.c_str(), ret.size(), 1, f);
    fclose(f);

    delete encrypt;
}

std::string DecryptAndGetFromFile(const std::string& key, std::string fn)
{
    if (key.size() == 0) {
        return "";
    }

    // get file name
    std::string fileName = "";
    if (fn.size() != 0)
        fileName = fn;
    else {
        fileName = "./sdkdata/easemobDB/.atconfig";
#ifdef _WIN32
        fileName = "./sdkdata/easemobDB/.atconfig";
#endif
    }

    // open file
    FILE* f = nullptr;
    f = fopen(fileName.c_str(), "r");

    // check size
    fseek(f, 0, SEEK_END);
    long fsize = ftell(f);
    if (0 == fsize) {
        return "";
    }
    fseek(f, 0, SEEK_SET);

    // get Decrypt key
    EMEncryptUtils* encrypt = EMEncryptUtils::createInstance();
    unsigned char bytes[16];
    EMEncryptCalculateUtil::getAESKey(key, key, bytes, encrypt);

    char* content = new char[fsize];
    fread(content, fsize, 1, f);
    std::string encryptedData(content, fsize);

    // Decrypt
    std::string ret = encrypt->aesDecrypt(encryptedData, bytes, 16);

    fclose(f);
    delete encrypt;
    delete[]content;
    return ret;
}

#ifndef _WIN32
std::string GetMacUuid() {
    std::string uuidPath = "./sdkdata/easemobDB/"; // default directory
    bool isTempPath = false;

    // check directory, maybe default directory is not created yet, then use /tmp
    if (access(uuidPath.c_str(), 0) != 0) {
        uuidPath = "/tmp/";
        isTempPath = true;
    }

    std::string uuidFile = uuidPath + ".uuid";
    std::string genenrateUUID = "ioreg -d2 -c IOPlatformExpertDevice | grep IOPlatformUUID";
    std::string saveUUID2File = genenrateUUID + " > " + uuidFile;
    std::string delUUIDFile = "rm -fr " + uuidFile;

    ifstream in;
    in.open(uuidFile);

    // check file size
    in.seekg(0, ios::end);
    int fsize = (int)in.tellg();
    in.seekg(0, ios::beg);

    // 32byte and four "-"
    if (fsize < 36) {
        // invalid content then generate content again
        system(saveUUID2File.c_str());
        in.open(uuidFile);
    }

    string line;
    getline(in, line);

    string::size_type pos1 = line.rfind("\"");
    if (line.npos == pos1) {
        if (line.size() == 36) {
            //LOG("uuid is %s", line.c_str()); // 128 bit for Uuid + four "-", is 36byte
            in.close();
            return line;
        }
        else {
            return std::string();
        }
    }
    string::size_type pos2 = line.rfind("\"", line.length() - 2);
    if (line.npos == pos2 || (line.npos != pos2 && pos1 <= pos2)) {
        return std::string();
    }
    string uuid = line.substr(pos2 + 1, pos1 - pos2 - 1);
    // 128 bit for Uuid + four "-", is 36byte
    if (uuid.size() != 36) {
        return std::string();
    }
    in.close();

    if (isTempPath)
        system(delUUIDFile.c_str());

    return uuid;
}
#endif