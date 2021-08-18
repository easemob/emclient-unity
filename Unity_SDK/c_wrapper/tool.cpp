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
