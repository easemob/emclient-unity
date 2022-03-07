#ifndef _WIN32
#include <unistd.h>
#endif
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
