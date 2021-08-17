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

bool MandatoryCheck(const void* ptr1, const void* ptr2, EMError& error) {
    if(nullptr == ptr1 || nullptr == ptr2) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return false;
    } else {
        return true;
    }
}

bool MandatoryCheck(const void* ptr1, const void* ptr2, const void* ptr3, EMError& error) {
    if(nullptr == ptr1 || nullptr == ptr2 || nullptr == ptr3) {
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
