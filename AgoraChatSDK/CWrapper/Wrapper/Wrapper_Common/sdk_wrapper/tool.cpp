
#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <cstdio>
#include <stdexcept>
#include <algorithm>

#ifndef _WIN32
#include <sys/utsname.h>
#include <sys/sysctl.h>
#endif

#include "emclient.h"
#include "utils/emencryptutils.h"
#include "utils/emutils.h"

#include "tool.h"
#include "models.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;
extern NativeListenerEvent gCallback;

SDK_WRAPPER_API void SDK_WRAPPER_CALL FreeMemory_SDKWrapper(void* p)
{
    if (nullptr != p) free(p);
}

void CallBack(const char* listener, const char* method, const char* jstr)
{
    if (nullptr == listener || nullptr == method || strlen(method) == 0)
        return;

    const char* j = jstr;

#ifdef _PURE_WIN32
    std::wstring unicode = L"";
    EMStringUtil::UTF8_to_Unicode(jstr, unicode);
    j = reinterpret_cast<const char*>(unicode.c_str());
#endif

    if (gCallback)
        gCallback(listener, method, j);
}

void CallBack(const char* method, const char* jstr)
{
    CallBack(STRING_CALLBACK_LISTENER.c_str(), method, jstr);
}

void CallBackProgress(const char* method, const char* jstr)
{
    CallBack(STRING_CALLBACK_PROGRESS_LISTENER.c_str(), method, jstr);
}

/*
const char* CopyToPointer(const string& src)
{
    if (0 == src.length()) return nullptr;

    size_t len = src.length() + 1;

    char* p = (char*)malloc(len * sizeof(char));

    memcpy(p, src.c_str(), len);

    p[len - 1] = '\0';

    return p;
}
*/
const char* CopyToPointer(const string& src)
{
    if (0 == src.length()) return nullptr;

#ifdef _PURE_WIN32
    std::wstring unicode = L"";
    EMStringUtil::UTF8_to_Unicode(src.c_str(), unicode);

    size_t length = unicode.length() + 1;
    wchar_t* wstrPtr = (wchar_t*)malloc(length * sizeof(wchar_t));

    if (nullptr == wstrPtr) return nullptr;

    wmemset(wstrPtr, 0, length);
    wcsncpy_s(wstrPtr, length, unicode.c_str(), length);
    const char* p = reinterpret_cast<const char*>(wstrPtr);
#else
    size_t len = src.length() + 1;
    char* p = (char*)malloc(len * sizeof(char));

    if (nullptr == p) return nullptr;

    memset(p, 0, len);
    memcpy(p, src.c_str(), len);
    p[len - 1] = '\0';
#endif
    return p;
}

void CopyToBuffer(char* dst, const char* src, size_t len)
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

// AppKey looks like: easemob-demo#unitytest
bool CheckAppKey(const char* app_key)
{
    if (nullptr == app_key || strlen(app_key) == 0) return false;

    string s = app_key;
    size_t len = s.length();

    string::size_type pos = s.find("#");

    if (s.npos == pos || 0 == pos || len - 1 == pos) {
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
        //LOG("Error: start timer for token failed.");
        return;
    }
}

void StopTimer()
{
    struct itimerval tick;
    memset(&tick, 0, sizeof(tick));
    if (setitimer(ITIMER_REAL, &tick, NULL) < 0) {
        //LOG("ERROR: stop timer for token failed.");
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
        STOP = false;
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

#ifndef _WIN32

void getMacOSVersion(int& major, int& minor, int& patch) {
    char str[256];
    size_t size = sizeof(str);
    memset(str, 0, size);

    if (sysctlbyname("kern.osrelease", str, &size, NULL, 0) == 0) {
        std::istringstream iss(str);
        char dot;
        iss >> major >> dot >> minor >> dot >> patch;
    }
    else {
        major = minor = patch = 0;
    }
}

string GetMacDid() {
    char deviceId[256];
    memset(deviceId, 0, sizeof(deviceId));

    struct utsname systemInfo;
    if (uname(&systemInfo) != 0) {
        return "";
    }

    char buf[128];
    size_t length = sizeof(buf);
    memset(buf, 0, length);

    int intErr = sysctlbyname("hw.model", buf, &length, NULL, 0);
    if (intErr != 0) {
        return "";
    }
    else {
        int major, minor, patch;
        getMacOSVersion(major, minor, patch);

        snprintf(deviceId, sizeof(deviceId), "%s/%s/OS X/%d.%d.%d", buf, systemInfo.machine, major, minor, patch);

        return string(deviceId);
    }
}
#else
// Helper function to execute a command and get the output
string execCommand(const char* cmd) {
    string result;
    char buffer[128];
    FILE* pipe = _popen(cmd, "r");
    if (!pipe) throw runtime_error("popen failed!");
    try {
        while (fgets(buffer, sizeof buffer, pipe) != nullptr) {
            result += buffer;
        }
    }
    catch (...) {
        _pclose(pipe);
        throw;
    }
    _pclose(pipe);
    return result;
}

string execCommandHidden(const char* cmd) {
    string output = "";
    HANDLE hPipeRead = NULL;
    HANDLE hPipeWrite = NULL;

    // Sets the security attribute to allow the pipe handle to be inherited by child processes.
    SECURITY_ATTRIBUTES saAttr;
    saAttr.nLength = sizeof(SECURITY_ATTRIBUTES);
    saAttr.bInheritHandle = TRUE;
    saAttr.lpSecurityDescriptor = NULL;

    // Create an anonymous pipe to read the output of the child process.
    if (!CreatePipe(&hPipeRead, &hPipeWrite, &saAttr, 0)) {
        return "";
    }

    // Set startup information
    STARTUPINFOA si;
    PROCESS_INFORMATION pi;
    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    si.hStdError = hPipeWrite;   // Redirect standard error to the pipe
    si.hStdOutput = hPipeWrite;  // Redirect standard output to the pipe
    si.dwFlags |= STARTF_USESTDHANDLES | STARTF_USESHOWWINDOW; // Key: Use standard handles and window display settings.
    si.wShowWindow = SW_HIDE;    // Key: Hide the window.

    ZeroMemory(&pi, sizeof(pi));

    // The second parameter of CreateProcessA requires a writable string; perform the conversion here
    string cmdStr = cmd;
    vector<char> cmdBuffer(cmdStr.begin(), cmdStr.end());
    cmdBuffer.push_back('\0');

    // Create the process
    BOOL success = CreateProcessA(
        NULL,
        cmdBuffer.data(),
        NULL, NULL, TRUE,
        0, // No new console needs to be created
        NULL, NULL,
        &si, &pi
    );

    if (success) {
        // Close the parent's write pipe to prevent the child process from hanging due to missing EOF.
        CloseHandle(hPipeWrite);
        hPipeWrite = NULL;

        // Read the output from the child process
        char buffer[128];
        DWORD bytesRead;
        while (ReadFile(hPipeRead, buffer, sizeof(buffer) - 1, &bytesRead, NULL) && bytesRead > 0) {
            buffer[bytesRead] = '\0';
            output += buffer;
        }

        // Wait for the process to finish
        WaitForSingleObject(pi.hProcess, INFINITE);

        // Clean up process and thread handles.
        CloseHandle(pi.hProcess);
        CloseHandle(pi.hThread);
    }

    // Clean up the pipe handles
    if (hPipeRead) CloseHandle(hPipeRead);
    if (hPipeWrite) CloseHandle(hPipeWrite);

    return output;
}


// Helper function to replace all slashes in a string
string replaceAllSlash(const string& str) {
    string result = str;
    size_t pos = 0;
    while ((pos = result.find('/', pos)) != string::npos) {
        result.replace(pos, 1, "\\");
        pos += 1;
    }
    return result;
}

// Helper function to trim whitespace from both ends of a string
string trim(const string& str) {
    size_t first = str.find_first_not_of(" \t\n\r");
    size_t last = str.find_last_not_of(" \t\n\r");
    return (first == string::npos || last == string::npos) ? "" : str.substr(first, last - first + 1);
}

// Function to get Windows version
string getWindowsVersion() {
    string version;
    try {
        string command = "wmic os get version";
        string output = execCommandHidden(command.c_str());

        // Parse the output
        istringstream iss(output);
        string line;
        while (getline(iss, line)) {
            line = trim(line); // Trim the line to remove any leading/trailing whitespace
            if (!line.empty() && line.find("Version") == string::npos) { // Skip header line
                version = line;
                break;
            }
        }
    }
    catch (const exception& e) {
        version = "";
    }
    return version;
}

// Main function to get device ID using wmic
string GetWinDid(bool infoOnly) {
    string deviceId;

    try {
        // Execute the wmic command to get the required information
        string command = "wmic computersystem get Manufacturer,Model,SystemType /format:csv";
        string output = execCommandHidden(command.c_str());

        // Parse the output
        istringstream iss(output);
        string line;
        vector<string> results;
        while (getline(iss, line)) {
            line = trim(line); // Trim the line to remove any leading/trailing whitespace
            if (!line.empty() && line.find("Node") == string::npos) { // Skip header line
                results.push_back(line);
            }
        }

        if (!results.empty() && results.size() > 0) {
            istringstream ss(results[0]); // Use results[0] to get the correct line
            string token;
            vector<string> tokens;
            while (getline(ss, token, ',')) {
                tokens.push_back(trim(token)); // Trim each token to remove any leading/trailing whitespace
            }

            if (tokens.size() >= 4) { // Ensure there are enough tokens
                deviceId = replaceAllSlash(tokens[1]); // Manufacturer
                deviceId += '/';
                deviceId += replaceAllSlash(tokens[2]); // Model
                if (!infoOnly) {
                    deviceId += '/';
                    deviceId += replaceAllSlash(tokens[3]); // SystemType
                }
            }
        }

        // Get Windows version and append to deviceId
        string windowsVersion = getWindowsVersion();
        if (!windowsVersion.empty()) {
            deviceId += "/Windows " + windowsVersion;
        }
    }
    catch (const exception& e) {
        return ""; // Return an empty string or handle the error as needed
    }

    return deviceId;
}
#endif

