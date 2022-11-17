
#include "emclient.h"
#include "utils/emencryptutils.h"
#include "utils/emutils.h"

#include "tool.h"
#include "models.h"

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

void Copy_To_Buffer(char* dst, const char* src, size_t len)
{
    if (nullptr == dst || nullptr == src ) return;

    // there are some useless data in buffer.
    // seems system alloc a memory in random, but not clear the data in it.
    // dst_len = strlen(dst);

    //if (dst_len > 0) {
    //    memset(dst, 0, dst_len);
    //}

    if(len > 0) memcpy(dst, src, len + 1);

    // unicode may need two bytes of \0 to regard as temination flag.
    //dst[len] = '\0';
    dst[len + 1] = '\0';
}

bool CheckClientInitOrNot(const char* cbid)
{    
    if (nullptr == gClient) {
        string call_back_jstr = sdk_wrapper::MyJson::ToJsonWithError(cbid, (int)EMError::GENERAL_ERROR, "Sdk is not initialized!");
        CallBack(cbid, call_back_jstr.c_str());
        return false;
    }
    else {
        return true;
    }
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

    thread t([=]() {
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

void EncryptAndSaveToFile(const string& plainMsg, const string& key, string fn)
{
    if (plainMsg.size() == 0 || key.size() == 0) {
        return;
    }

    // get file name
    string fileName = "";
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
    string ret = encrypt->aesEncrypt(plainMsg, bytes, 16);

    // save result
    fwrite(ret.c_str(), ret.size(), 1, f);
    fclose(f);

    delete encrypt;
}

string DecryptAndGetFromFile(const string& key, string fn)
{
    if (key.size() == 0) {
        return "";
    }

    // get file name
    string fileName = "";
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
    string encryptedData(content, fsize);

    // Decrypt
    string ret = encrypt->aesDecrypt(encryptedData, bytes, 16);

    fclose(f);
    delete encrypt;
    delete[]content;
    return ret;
}

#ifndef _WIN32
string GetMacUuid() {
    string uuidPath = "./sdkdata/easemobDB/"; // default directory
    bool isTempPath = false;

    // check directory, maybe default directory is not created yet, then use /tmp
    if (access(uuidPath.c_str(), 0) != 0) {
        uuidPath = "/tmp/";
        isTempPath = true;
    }

    string uuidFile = uuidPath + ".uuid";
    string genenrateUUID = "ioreg -d2 -c IOPlatformExpertDevice | grep IOPlatformUUID";
    string saveUUID2File = genenrateUUID + " > " + uuidFile;
    string delUUIDFile = "rm -fr " + uuidFile;

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
            return string();
        }
    }
    string::size_type pos2 = line.rfind("\"", line.length() - 2);
    if (line.npos == pos2 || (line.npos != pos2 && pos1 <= pos2)) {
        return string();
    }
    string uuid = line.substr(pos2 + 1, pos1 - pos2 - 1);
    // 128 bit for Uuid + four "-", is 36byte
    if (uuid.size() != 36) {
        return string();
    }
    in.close();

    if (isTempPath)
        system(delUUIDFile.c_str());

    return uuid;
}
#endif