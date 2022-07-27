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

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

extern EMClient* gClient;

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

void TestParseUserInfoResponseFromServer()
{
    StringBuffer s;
    Writer<StringBuffer> writer(s);

    writer.StartObject();
    {
            writer.Key("user1");
            writer.StartObject();
            {
                writer.Key("nickname");
                std::string str = "我的昵称";
                writer.String(str.c_str());

                writer.Key("avatarurl");
                writer.String("nick.com");

                writer.Key("mail");
                writer.String("mail.nick.com");

                writer.Key("phone");
                writer.String("12345678998");

                writer.Key("gender");
                writer.Int(1);

                writer.Key("sign");
                writer.String("this is sign 1");

                writer.Key("birth");
                writer.String("2022-01-01");

                writer.Key("ext");
                writer.String("this is ext 1");
            }
            writer.EndObject();

            writer.Key("user2");
            writer.StartObject();
            {
                writer.Key("nickname");
                std::string str = "mynickname";
                writer.String(str.c_str());

                writer.Key("avatarurl");
                writer.String("ava.com");

                writer.Key("mail");
                writer.String("mail.ava.com");

                writer.Key("phone");
                writer.String("98765432123");

                writer.Key("gender");
                writer.Int(0);

                writer.Key("sign");
                writer.String("this is sign 2");

                writer.Key("birth");
                writer.String("2002-01-01");

                writer.Key("ext");
                writer.String("this is ext 2");
            }
            writer.EndObject();
    }
    writer.EndObject();

    string jstr = s.GetString();

    std::map<std::string, UserInfo> userinfoMap;

    // save all strings into userinfoMap
    userinfoMap = UserInfo::FromResponse(jstr, UserInfoTypeMap);

    return;
}

HYPHENATE_API void UserInfoManager_UpdateOwnInfo(void *client, int callbackId, void* userInfo, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    if (!CheckClientInitOrNot(callbackId, onError)) return;

    EMError error;
    if(!MandatoryCheck(userInfo, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    UserInfoTO* uto = static_cast<UserInfoTO*>(userInfo);
    std::string nickNameUtf8 = GetUTF8FromUnicode(uto->nickName);
    //std::string nickNameAnsi = UTF8toANSI(nickNameUtf8);
    
    StringBuffer s;
    Writer<StringBuffer> writer(s);

    writer.StartObject();

    if (nickNameUtf8.length() > 0) {
        writer.Key(UserInfoTypeMap[NICKNAME].c_str());
        writer.String(nickNameUtf8.c_str());
    }
    
    if (strlen(uto->avatarUrl) > 0) {
        writer.Key(UserInfoTypeMap[AVATAR_URL].c_str());
        writer.String(uto->avatarUrl);
    }

    if (strlen(uto->email) > 0) {
        writer.Key(UserInfoTypeMap[EMAIL].c_str());
        writer.String(uto->email);
    }

    if (strlen(uto->phoneNumber) > 0) {
        writer.Key(UserInfoTypeMap[PHONE].c_str());
        writer.String(uto->phoneNumber);
    }

    if (0 == uto->gender || 1 == uto->gender || 2 == uto->gender) {
        writer.Key(UserInfoTypeMap[GENDER].c_str());
        writer.Int(uto->gender);
    }

    if (strlen(uto->signature) > 0) {
        writer.Key(UserInfoTypeMap[SIGN].c_str());
        writer.String(uto->signature);
    }

    if (strlen(uto->birth) > 0) {
        writer.Key(UserInfoTypeMap[BIRTH].c_str());
        writer.String(uto->birth);
    }

    if (strlen(uto->ext) > 0) {
        writer.Key(UserInfoTypeMap[EXT].c_str());
        writer.String(uto->ext);
    }

    writer.EndObject();

    string jstr = s.GetString();
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
    if (!CheckClientInitOrNot(callbackId, onError)) return;

    EMError error;
    auto it = UserInfoTypeMap.find((UserInfoType)userinfoType); // TO-DO: userinfoType is not UserInfoType, then? 
    // value can be empty?
    if(nullptr == value || UserInfoTypeMap.end() == it) {
        error.setErrorCode(EMError::INVALID_PARAM);
        error.mDescription = "Mandatory parameter is null!";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    nlohmann::json j;
    std::string jstr = "";
    std::string attr = GetUTF8FromUnicode(value);
    try {
        // TO-DO: check it is ok or not for ""
        j[UserInfoTypeMap[(UserInfoType)userinfoType]] = attr;
        jstr = j.dump();
    }
    catch(std::exception) {
        LOG("Make json failed, exit from UserInfoManager_UpdateOwnInfoByAttribute");
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
    if (!CheckClientInitOrNot(callbackId, onError)) return;

    EMError error;
    if(nullptr == users || size <= 0) {
        error.setErrorCode(EMError::INVALID_PARAM);
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

                    const UserInfoTO** data = new const UserInfoTO*[userinfoToMap.size()];
                    int foundCount = 0;
                    
                    for(auto it : userv) {
                        auto found = userinfoToMap.find(it);
                        if(userinfoToMap.end() == found) continue;
                        data[foundCount] = &(found->second);
                        foundCount++;
                    }
                    
                    LOG("UserInfoManager_FetchUserInfoByUserId succeeds, user count=%d", foundCount);
                    onSuccess((void **)data, DataType::ListOfGroup, foundCount, callbackId);
		            delete []data;
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
    if (!CheckClientInitOrNot(callbackId, onError)) return;

    EMError error;
    if(nullptr == users || userSize <= 0 || typeSize <= 0) {
        error.setErrorCode(EMError::INVALID_PARAM);
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
        error.setErrorCode(EMError::INVALID_PARAM);
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
                    
                    const UserInfoTO** data = new const UserInfoTO*[userinfoToMap.size()];
                    int foundCount = 0;
                    for(auto it : userv) {
                        auto found = userinfoToMap.find(it);
                        if(userinfoToMap.end() == found) continue;
                        data[foundCount] = &(found->second);
                        foundCount++;
                    }
                    
                    LOG("UserInfoManager_FetchUserInfoByAttribute succeeds, user count=%d", foundCount);
                    onSuccess((void **)data, DataType::ListOfGroup, foundCount, callbackId);
		    delete []data;
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
