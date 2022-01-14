//
//  userinfo_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2021/10/28.
//  Copyright © 2021 easemob. All rights reserved.
//
#include <thread>
#include "userinfo_manager.h"
#include "emclient.h"
#include "tool.h"
#include "json.hpp"

std::map<UserInfoType, std::string> UserInfoTypeMap =
                                    {
                                        {NICKNAME,      "nickname"},
                                        {AVATAR_URL,    "avatarurl"},
                                        {EMAIL,         "mail"},
                                        {PHONE,         "phone"},
                                        {GENDER,        "gender"},
                                        {SIGN,          "sign"},
                                        {BIRTH,         "birth"},
                                        {EXT,           "ext"}
                                    };

HYPHENATE_API void UserInfoManager_UpdateOwnInfo(void *client, int callbackId, void* userInfo, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(userInfo, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    UserInfoTO* uto = static_cast<UserInfoTO*>(userInfo);
    
    nlohmann::json j;
    std::string jstr = "";
    try {
        // TO-DO: check it is ok or not for ""
        // TO-DO: check it is ok without birth? and userId?
        j[UserInfoTypeMap[NICKNAME]]    = uto->nickName;
        j[UserInfoTypeMap[AVATAR_URL]]  = uto->avatarUrl;
        j[UserInfoTypeMap[EMAIL]]       = uto->email;
        j[UserInfoTypeMap[PHONE]]       = uto->phoneNumber;
        j[UserInfoTypeMap[GENDER]]      = uto->gender;
        j[UserInfoTypeMap[SIGN]]        = uto->signature;
        j[UserInfoTypeMap[BIRTH]]       = uto->birth;
        j[UserInfoTypeMap[EXT]]         = uto->ext;
        jstr = j.dump();
    }
    catch(std::exception) {
        LOG("Make json failed, exit from UserInfoManager_UpdateOwnInfo");
    }

    if (jstr.length() <= 2) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Convert userinfo to json failed !";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    LOG("Json from userinfo: %s", jstr.c_str());
    
    std::string nickname = uto->nickName;
    std::thread t([=](){
        std::string response = "";
        EMError error;
        CLIENT->getUserInfoManager().updateOwnUserInfo(jstr, response, error);
        
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("UserInfoManager_UpdateOwnInfo succeeds for nickname=%s and respose=%s", nickname.c_str(), response.c_str());
            if(onSuccess) onSuccess(callbackId);
        } else {
            LOG("UserInfoManager_UpdateOwnInfo failed for nickname=%s, code=%d, desc=%s", nickname.c_str(), error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void UserInfoManager_UpdateOwnInfoByAttribute(void *client, int callbackId, int userinfoType, const char* value, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    auto it = UserInfoTypeMap.find((UserInfoType)userinfoType); // TO-DO: userinfoType is not UserInfoType, then? 
    // value can be empty?
    if(nullptr == value || UserInfoTypeMap.end() == it) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    nlohmann::json j;
    std::string jstr = "";
    std::string attr = value;
    try {
        // TO-DO: check it is ok or not for ""
        j[UserInfoTypeMap[(UserInfoType)userinfoType]] = attr;
        jstr = j.dump();
    }
    catch(std::exception) {
        LOG("Make json failed, exit from UserInfoManager_UpdateOwnInfoByAttribute");
    }

    if (jstr.length() <= 2) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Convert userinfo to json failed !";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    LOG("Json from attribute: %s", jstr.c_str());
    
    std::thread t([=](){
        std::string response = "";
        EMError error;
        CLIENT->getUserInfoManager().updateOwnUserInfo(jstr, response, error);
        
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("UserInfoManager_UpdateOwnInfoByAttribute succeeds for attr=%s and respose=%s", attr.c_str(), response.c_str());
            if(onSuccess) {
                const char *data[1] = {response.c_str()};
                onSuccess((void **)data, DataType::String, 1, callbackId);
            }
        } else {
            LOG("UserInfoManager_UpdateOwnInfoByAttribute failed for attr=%s, code=%d, desc=%s", attr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void UserInfoManager_FetchUserInfoByUserId(void *client, int callbackId, const char * users[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(nullptr == users || size <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    std::vector<std::string> userv;
    for(int i=0; i<size; i++) {
        userv.push_back(users[i]);
    }
    std::vector<std::string> attrv;
    for(auto it : UserInfoTypeMap) {
        attrv.push_back(it.second);
    }
    
    std::thread t([=](){
        std::string response = "";
        EMError error;
        CLIENT->getUserInfoManager().fetchUsersPropertyByType(userv, attrv, response, error);
        
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            
            if(onSuccess) {
                std::map<std::string, UserInfo> userinfoMap;
                std::map<std::string, UserInfoTO> userinfoToMap;

                // save all strings into userinfoMap
                userinfoMap = UserInfo::FromResponse(response,UserInfoTypeMap);

                if (userinfoMap.size() > 0) {
                    // userinfoToMap will point to userinfoMap
                    userinfoToMap = UserInfo::Convert2TO(userinfoMap);

                    const UserInfoTO* data[userinfoToMap.size()];
                    int foundCount = 0;
                    
                    for(auto it : userv) {
                        auto found = userinfoToMap.find(it);
                        if(userinfoToMap.end() == found) continue;
                        data[foundCount] = &(found->second);
                        foundCount++;
                    }
                    
                    LOG("UserInfoManager_FetchUserInfoByUserId succeeds, user count=%d", foundCount);
                    onSuccess((void **)data, DataType::ListOfGroup, foundCount, callbackId);
                } else {
                    LOG("UserInfoManager_FetchUserInfoByUserId cannot get any data from server");
                    onSuccess(nullptr, DataType::ListOfGroup, 0, callbackId);
                }
            }
        } else {
            LOG("UserInfoManager_UpdateOwnInfo failed for code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void UserInfoManager_FetchUserInfoByAttribute(void *client, int callbackId, const char * users[], int userSize, int userinfoTypes[], int typeSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(nullptr == users || userSize <= 0 || typeSize <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    std::vector<std::string> userv;
    for(int i=0; i<userSize; i++) {
        userv.push_back(users[i]);
    }
    
    std::vector<std::string> attrv;
    // select qualified type
    for(int i=0; i<typeSize; i++) {
        //TO-DO: userinfoType is not UserInfoType, then?
        auto it = UserInfoTypeMap.find((UserInfoType)userinfoTypes[i]);
        if(UserInfoTypeMap.end() == it) {
            LOG("Found illegal userinfo type :%d", userinfoTypes[i]);
            continue;
        }
        LOG("Qulified userinfo type %s will used for query.", UserInfoTypeMap[(UserInfoType)userinfoTypes[i]].c_str());
        attrv.push_back(UserInfoTypeMap[(UserInfoType)userinfoTypes[i]]);
    }
    if (attrv.size() == 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "No qulified attribute type parameter!";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    std::thread t([=](){
        std::string response = "";
        EMError error;
        CLIENT->getUserInfoManager().fetchUsersPropertyByType(userv, attrv, response, error);
        
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) {
                std::map<std::string, UserInfo> userinfoMap;
                std::map<std::string, UserInfoTO> userinfoToMap;

                // save all strings into userinfoMap
                userinfoMap = UserInfo::FromResponse(response,UserInfoTypeMap);
                
                if (userinfoMap.size() > 0) {
                    // userinfoToMap will point to userinfoMap
                    userinfoToMap = UserInfo::Convert2TO(userinfoMap);
                    
                    const UserInfoTO* data[userinfoToMap.size()];
                    int foundCount = 0;
                    for(auto it : userv) {
                        auto found = userinfoToMap.find(it);
                        if(userinfoToMap.end() == found) continue;
                        data[foundCount] = &(found->second);
                        foundCount++;
                    }
                    
                    LOG("UserInfoManager_FetchUserInfoByAttribute succeeds, user count=%d", foundCount);
                    onSuccess((void **)data, DataType::ListOfGroup, foundCount, callbackId);
                } else {
                    LOG("UserInfoManager_FetchUserInfoByAttribute cannot get any data from server");
                    onSuccess(nullptr, DataType::ListOfGroup, 0, callbackId);
                }
            }
        } else {
            LOG("UserInfoManager_FetchUserInfoByAttribute failed for code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}
