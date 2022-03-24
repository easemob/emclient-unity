#ifndef _WIN32
    #include <unistd.h>
    #include <sys/time.h>
#else
    #include <windows.h>
#endif

#include <time.h>
#include <signal.h>
#include <math.h>
#include "utils/emencryptutils.h"
#include "utils/emutils.h"
#include "tool.h"

bool MandatoryCheck(const void* ptr, EMError& error) {
    if(nullptr == ptr) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const char* ptr, EMError& error) {
    if(nullptr == ptr || strlen(ptr) == 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const char* ptr) {
    if(nullptr == ptr || strlen(ptr) == 0) {
        LOG("Mandatory parameter is null!");
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const void* ptr) {
    if(nullptr == ptr) {
        LOG("Mandatory parameter is null!");
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const char* ptr1, void* ptr2, EMError& error)
{
    if(nullptr == ptr1 || nullptr == ptr2 || strlen(ptr1) == 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const char* ptr1, const char* ptr2, EMError& error) {
    if(nullptr == ptr1 || nullptr == ptr2 || strlen(ptr1) == 0 || strlen(ptr2) == 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const char* ptr1, const char* ptr2) {
    if(nullptr == ptr1 || nullptr == ptr2 || strlen(ptr1) == 0 || strlen(ptr2) == 0) {
        LOG("Mandatory parameter is null!");
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const char* ptr1, const char* ptr2, const char* ptr3, EMError& error) {
    if(nullptr == ptr1 || nullptr == ptr2 || nullptr == ptr3 ||
       strlen(ptr1) == 0 || strlen(ptr2) == 0 || strlen(ptr3) == 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return false;
    } else {
        return true;
    }
}

std::string OptionalStrParamCheck(const char* ptr) {
    return (nullptr == ptr)?"":ptr;
}

std::string GetLeftValue(const std::string& str)
{
    if (str.size() == 0) return "";
    std::string::size_type pos;
    pos = str.find("=");
    if (std::string::npos == pos) return "";
    return std::string(str, 0, pos - 0);
}

std::string GetRightValue(const std::string& str)
{
    if (str.size() == 0) return "";
    std::string::size_type pos;
    pos = str.find("=");
    if (std::string::npos == pos) return "";
    if (pos == str.size() - 1) return "";
    return std::string(str, pos+1, str.size() - pos - 1);
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
    
    //after first, the interval time for clock
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

HANDLE hTimerQueue;
HANDLE hTimerQueueTimer;
void StartTimer(int interval, WAITORTIMERCALLBACK timer_func)
{
    StopTimer();
    if (NULL == hTimerQueue && NULL == hTimerQueueTimer)
        {
            hTimerQueue = CreateTimerQueue();
            if (hTimerQueue != NULL)
            {
                if (!CreateTimerQueueTimer(&hTimerQueueTimer, hTimerQueue, timer_func, NULL, interval*1000, interval*1000, WT_EXECUTEDEFAULT))
                {
                    hTimerQueue = NULL;
                    hTimerQueueTimer = NULL;
                }
            }
            else
            {
                hTimerQueue = NULL;
                hTimerQueueTimer = NULL;
            }
        }
}

void StopTimer()
{
    if (NULL != hTimerQueueTimer)
        DeleteTimerQueueTimer(hTimerQueue, hTimerQueueTimer, INVALID_HANDLE_VALUE);
    if (NULL != hTimerQueue)
        DeleteTimerQueueEx(hTimerQueue, INVALID_HANDLE_VALUE);
    hTimerQueueTimer = NULL;
    hTimerQueue = NULL;
}
#endif

void EncryptAndSaveToFile(const std::string& plainMsg, const std::string& key, std::string fn)
{
    if (plainMsg.size() == 0 || key.size() == 0) {
        LOG("Error: invalid input parameters for SaveAndEncrypt.");
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
        LOG("Error: empty key for decrypt.");
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
        LOG("Error: empty file for decrypt.");
        return "";
    }
    fseek(f, 0, SEEK_SET);
    
    // get Decrypt key
    EMEncryptUtils* encrypt = EMEncryptUtils::createInstance();
    unsigned char bytes[16];
    EMEncryptCalculateUtil::getAESKey(key, key, bytes, encrypt);
    
    char* content = new char[fsize];
    fread(content,fsize, 1, f);
    std::string encryptedData(content, fsize);
    
    // Decrypt
    std::string ret = encrypt->aesDecrypt(encryptedData, bytes, 16);
    
    fclose(f);
    delete encrypt;
    delete []content;
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
    if(line.npos == pos1) {
        if (line.size() == 36) {
            LOG("uuid is %s", line.c_str()); // 128 bit for Uuid + four "-", is 36byte
            in.close();
            return line;
        } else {
            LOG("Bad format, cannot get uuid from file %s. line size:%d, pos:%d", uuidFile.c_str(), line.size(), pos1);
            return std::string();
        }
    }
    string::size_type pos2 = line.rfind("\"", line.length()-2);
    if(line.npos == pos2 || (line.npos != pos2 && pos1 <= pos2)) {
        LOG("Bad format, cannot get uuid from file %s. pos2:%d", uuidFile.c_str(), pos2);
        return std::string();
    }
    string uuid = line.substr(pos2 + 1, pos1 - pos2 - 1 );
    // 128 bit for Uuid + four "-", is 36byte
    if (uuid.size() != 36) {
        LOG("uuid content is not correct: %s", uuid.c_str());
        return std::string();
    }
    LOG("uuid is %s", uuid.c_str());
    in.close();
    
    if(isTempPath)
        system(delUUIDFile.c_str());
    
    return uuid;
}
#endif
