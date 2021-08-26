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

AGORA_API void PushManager_GetIgnoredGroupIds(void *client, FUNC_OnSuccess_With_Result onSuccess)
{
    EMPushConfigsPtr configPtr = CLIENT->getPushManager().getPushConfigs();
    if(!configPtr) {
        LOG("Cannot get any push config ");
        onSuccess(nullptr, DataType::ListOfString, 0);
        return;
    }
    std::vector<std::string> ignoreList = configPtr->getIgnoredGroupIds();
    int size = (int)ignoreList.size();
    LOG("GetIgnoredGroupIds group id number:%d", size);
    const char * data[size];
    for(size_t i=0; i<size; i++) {
        data[i] = ignoreList[i].c_str();
    }
    onSuccess((void **)data, DataType::ListOfString, size);
}

AGORA_API void PushManager_GetPushConfig(void *client, FUNC_OnSuccess_With_Result onSuccess)
{
    EMPushConfigsPtr configPtr = CLIENT->getPushManager().getPushConfigs();
    if(!configPtr) {
        //DataType has no suitable enum value for this.
        LOG("No any push config ");
        onSuccess(nullptr, DataType::ListOfString, 0);
        return;
    }
    PushConfigTO* data[1];
    data[0] = PushConfigTO::FromEMPushConfig(configPtr);
    onSuccess((void**)data, DataType::ListOfString, 1);
    delete data[0];
}

AGORA_API void PushManager_GetUserConfigsFromServer(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        EMPushConfigsPtr configPtr = CLIENT->getPushManager().getUserConfigsFromServer(error);
        if(!configPtr) {
            LOG("No any push config ");
            onSuccess(nullptr, DataType::Group, 0);
            return;
        }
        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_GetUserConfigsFromServer execution succeeds");
            if(onSuccess) {
                PushConfigTO * data[1];
                data[0] = PushConfigTO::FromEMPushConfig(configPtr);
                onSuccess((void **)data, DataType::ListOfString, 1);
                delete data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}

AGORA_API void PushManager_IgnoreGroupPush(void *client, const char * groupId, bool noDisturb, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        if(!MandatoryCheck(groupId, error)) {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
            return;
        }
        CLIENT->getPushManager().ignoreGroupPush(groupId, noDisturb, error);
        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_IgnoreGroupPush execution succeeds: %s", groupId);
            if(onSuccess) onSuccess();
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}

AGORA_API void PushManager_UpdatePushNoDisturbing(void *client, bool noDisturb, int startTime, int endTime, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        EMPushConfigsPtr pushConfigPtr = CLIENT->getPushManager().getPushConfigs();
        EMPushConfigs::EMPushDisplayStyle style = pushConfigPtr->getDisplayStyle();
        EMPushConfigs::EMPushNoDisturbStatus status = pushConfigPtr->getDisplayStatus();
        
        if(noDisturb)
            status = EMPushConfigs::EMPushNoDisturbStatus::Day; //to-do: is this right to set Day?
        else
            status = EMPushConfigs::EMPushNoDisturbStatus::Close;
        
        CLIENT->getPushManager().updatePushNoDisturbing(style, status, startTime, endTime, error);
        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_UpdatePushNoDisturbing execution succeeds, and NoDisturbStatus: %d", status);
            if(onSuccess) onSuccess();
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}

AGORA_API void PushManager_UpdatePushDisplayStyle(void *client, EMPushConfigs::EMPushDisplayStyle style, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        CLIENT->getPushManager().updatePushDisplayStyle(style, error);

        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_updatePushDisplayStyle execution succeeds, and style: %d", style);
            if(onSuccess) onSuccess();
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}

AGORA_API void PushManager_UpdateFCMPushToken(void *client, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        if(!MandatoryCheck(token, error)) {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
            return;
        }
        CLIENT->getPushManager().bindUserDeviceToken(token, "FCM", error);
        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_UpdateFCMPushToken execution succeeds, token: %s for FCM", token);
            if(onSuccess) onSuccess();
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}

AGORA_API void PushManager_UpdateHMSPushToken(void *client, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        if(!MandatoryCheck(token, error)) {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
            return;
        }
        CLIENT->getPushManager().bindUserDeviceToken(token, "HMS", error);
        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_UpdateHMSPushToken execution succeeds, token: %s for FCM", token);
            if(onSuccess) onSuccess();
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}

AGORA_API void PushManager_UpdatePushNickName(void *client, const char * nickname, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        if(!MandatoryCheck(nickname, error)) {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
            return;
        }
        CLIENT->getPushManager().updatePushNickName(nickname, error);
        if(error.mErrorCode == EMError::EM_NO_ERROR) {
            LOG("PushManager_UpdatePushNickName execution succeeds, nickname: %s", nickname);
            if(onSuccess) onSuccess();
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        }
    });
    t.join();
}
