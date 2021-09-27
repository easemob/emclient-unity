//
//  contact_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2021/8/3.
//  Copyright © 2021 easemob. All rights reserved.
//

#include <thread>
//#include "empushmanager_interface.h"
#include "emchatclient.h"
#include "emchatconfigs.h"
#include "emclient.h"
#include "empushconfigs.h"
#include "empushmanager_interface.h"
#include "models.h"
#include "push_manager.h"
#include "tool.h"

Hypheante_API void PushManager_GetIgnoredGroupIds(void *client, FUNC_OnSuccess_With_Result onSuccess)
{
    EMPushConfigsPtr configPtr = CLIENT->getPushManager().getPushConfigs();
    if(!configPtr) {
        LOG("Cannot get any push config ");
        onSuccess(nullptr, DataType::ListOfString, 0, -1);
        return;
    }
    std::vector<std::string> ignoreList = configPtr->getIgnoredGroupIds();
    int size = (int)ignoreList.size();
    LOG("GetIgnoredGroupIds group id number:%d", size);
    const char * data[size];
    for(size_t i=0; i<size; i++) {
        data[i] = ignoreList[i].c_str();
    }
    onSuccess((void **)data, DataType::ListOfString, size, -1);
}

Hypheante_API void PushManager_GetPushConfig(void *client, FUNC_OnSuccess_With_Result onSuccess)
{
    EMPushConfigsPtr configPtr = CLIENT->getPushManager().getPushConfigs();
    if(!configPtr) {
        //DataType has no suitable enum value for this.
        LOG("No any push config.");
        onSuccess(nullptr, DataType::ListOfString, 0, -1);
        return;
    }
    LOG("Found push config.");
    PushConfigTO* data[1];
    data[0] = PushConfigTO::FromEMPushConfig(configPtr);
    onSuccess((void**)data, DataType::ListOfString, 1, -1);
    delete data[0];
}

Hypheante_API void PushManager_GetUserConfigsFromServer(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        EMPushConfigsPtr configPtr = CLIENT->getPushManager().getUserConfigsFromServer(error);
        if(!configPtr) {
            LOG("No any push config ");
            onSuccess(nullptr, DataType::Group, 0, callbackId);
            return;
        }
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_GetUserConfigsFromServer execution succeeds");
            if(onSuccess) {
                PushConfigTO * data[1];
                data[0] = PushConfigTO::FromEMPushConfig(configPtr);
                onSuccess((void **)data, DataType::ListOfString, 1, callbackId);
                delete data[0];
            }
        }else{
            LOG("PushManager_GetUserConfigsFromServer execution failed, code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void PushManager_IgnoreGroupPush(void *client, int callbackId, const char * groupId, bool noDisturb, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getPushManager().ignoreGroupPush(groupIdStr, noDisturb, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_IgnoreGroupPush execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PushManager_IgnoreGroupPush execution failed, groupId=%s, code=%d, desc=%s", groupIdStr.c_str(), error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void PushManager_UpdatePushNoDisturbing(void *client, int callbackId, bool noDisturb, int startTime, int endTime, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        EMPushConfigsPtr pushConfigPtr = CLIENT->getPushManager().getPushConfigs();
        EMPushConfigs::EMPushDisplayStyle style = pushConfigPtr->getDisplayStyle();
        EMPushConfigs::EMPushNoDisturbStatus status = pushConfigPtr->getDisplayStatus();
        
        if(noDisturb)
            status = EMPushConfigs::EMPushNoDisturbStatus::Custom; //to-do: is this right to set Day?
        else
            status = EMPushConfigs::EMPushNoDisturbStatus::Close;
        
        CLIENT->getPushManager().updatePushNoDisturbing(style, status, startTime, endTime, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_UpdatePushNoDisturbing execution succeeds, and NoDisturbStatus: %d", status);
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PushManager_UpdatePushNoDisturbing execution failed, code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void PushManager_UpdatePushDisplayStyle(void *client, int callbackId, EMPushConfigs::EMPushDisplayStyle style, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        CLIENT->getPushManager().updatePushDisplayStyle(style, error);

        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_updatePushDisplayStyle execution succeeds, and style: %d", style);
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PushManager_updatePushDisplayStyle execution failed, code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void PushManager_UpdateFCMPushToken(void *client, int callbackId, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(token, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string tokenStr = token;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getPushManager().bindUserDeviceToken(tokenStr, "FCM", error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_UpdateFCMPushToken execution succeeds, token: %s for FCM", tokenStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PushManager_UpdateFCMPushToken execution failed, code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void PushManager_UpdateHMSPushToken(void *client, int callbackId, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(token, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string tokenStr = token;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getPushManager().bindUserDeviceToken(tokenStr, "HMS", error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_UpdateHMSPushToken execution succeeds, token: %s for HMS", tokenStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PushManager_UpdateHMSPushToken execution failed HMS, code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void PushManager_UpdatePushNickName(void *client, int callbackId, const char * nickname, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(nickname, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string nicknameStr = nickname;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getPushManager().updatePushNickName(nickname, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("PushManager_UpdatePushNickName execution succeeds, nickname: %s", nicknameStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PushManager_UpdatePushNickName execution failed, code=%d, desc=%s", error.mErrorCode, error.mDescription.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}
