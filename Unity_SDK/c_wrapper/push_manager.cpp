//
//  contact_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2021/8/3.
//  Copyright © 2021 easemob. All rights reserved.
//

//#include "empushmanager_interface.h"
#include "emchatclient.h"
#include "emchatconfigs.h"
#include "emclient.h"
#include "empushconfigs.h"
#include "empushmanager_interface.h"
#include "models.h"
#include "push_manager.h"

EMChatClient* globalChatClient;

EMPushManagerInterface* GetPushManager(void *client)
{
    if(nullptr == globalChatClient)
    {
        EMChatConfigsPtr configPtr = CLIENT->getChatConfigs();
        globalChatClient = easemob::EMChatClient::create(configPtr);
    }
    return &globalChatClient->getPushManager();
}

AGORA_API void PushManager_GetIgnoredGroupIds(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::vector<std::string> ignoreList = GetPushManager(client)->getPushConfigs()->getIgnoredGroupIds();
    
    EMError error;
    error.setErrorCode(EMError::EM_NO_ERROR); //impossible to return error.
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        if(onSuccess) {
            size_t size = ignoreList.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                char* ptr = new char[ignoreList[i].size()+1];
                strncpy(ptr, ignoreList[i].c_str(), ignoreList[i].size()+1);
                data[i] = ptr;
            }
            onSuccess((void **)data, DataType::ListOfString, (int)size);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_GetPushConfig(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMPushConfigsPtr configPtr = GetPushManager(client)->getPushConfigs();
    
    EMError error;
    error.setErrorCode(EMError::EM_NO_ERROR); //impossible to return error.
    
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        if(onSuccess) {
            
            PushConfigTO* pushConfigTO = new PushConfigTO();
            
            if(configPtr->getDisplayStatus() == easemob::EMPushConfigs::EMPushNoDisturbStatus::Close)
                pushConfigTO->NoDisturb = true;
            else
                pushConfigTO->NoDisturb = false;

            pushConfigTO->NoDisturbStartHour = configPtr->getNoDisturbingStartHour();
            pushConfigTO->NoDisturbEndHour = configPtr->getNoDisturbingEndHour();
            pushConfigTO->Style = configPtr->getDisplayStyle();

            PushConfigTO * data[1] = {pushConfigTO};
            onSuccess((void **)data, DataType::Group, 1);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_GetUserConfigsFromServer(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMPushConfigsPtr configPtr = GetPushManager(client)->getUserConfigsFromServer(error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_GetUserConfigsFromServer execution succeeds");
        if(onSuccess) {
            
            PushConfigTO* pushConfigTO = new PushConfigTO();
            
            if(configPtr->getDisplayStatus() == easemob::EMPushConfigs::EMPushNoDisturbStatus::Close)
                pushConfigTO->NoDisturb = true;
            else
                pushConfigTO->NoDisturb = false;

            pushConfigTO->NoDisturbStartHour = configPtr->getNoDisturbingStartHour();
            pushConfigTO->NoDisturbEndHour = configPtr->getNoDisturbingEndHour();
            pushConfigTO->Style = configPtr->getDisplayStyle();

            PushConfigTO * data[1] = {pushConfigTO};
            onSuccess((void **)data, DataType::Group, 1);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_IgnoreGroupPush(void *client, const char * groupId, bool noDisturb, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    GetPushManager(client)->ignoreGroupPush(groupId, noDisturb, error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_IgnoreGroupPush execution succeeds: %s", groupId);
        if(onSuccess) {
            onSuccess();
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_UpdatePushNoDisturbing(void *client, bool noDisturb, int startTime, int endTime, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMPushConfigsPtr pushConfigPtr = GetPushManager(client)->getPushConfigs();
    
    EMPushConfigs::EMPushDisplayStyle style = pushConfigPtr->getDisplayStyle();
    EMPushConfigs::EMPushNoDisturbStatus status = pushConfigPtr->getDisplayStatus();
    
    if(noDisturb)
        status = EMPushConfigs::EMPushNoDisturbStatus::Day; //to-do: is this right to set Day?
    else
        status = EMPushConfigs::EMPushNoDisturbStatus::Close;
    
    GetPushManager(client)->updatePushNoDisturbing(style, status, startTime, endTime, error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_UpdatePushNoDisturbing execution succeeds, and NoDisturbStatus: %d", status);
        if(onSuccess) {
            onSuccess();
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_UpdatePushDisplayStyle(void *client, EMPushConfigs::EMPushDisplayStyle style, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    GetPushManager(client)->updatePushDisplayStyle(style, error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_updatePushDisplayStyle execution succeeds, and style: %d", style);
        if(onSuccess) {
            onSuccess();
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_UpdateFCMPushToken(void *client, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    GetPushManager(client)->bindUserDeviceToken(token, "FCM", error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_UpdateFCMPushToken execution succeeds, token: %s for FCM", token);
        if(onSuccess) {
            onSuccess();
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_UpdateHMSPushToken(void *client, const char * token, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    GetPushManager(client)->bindUserDeviceToken(token, "HMS", error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_UpdateHMSPushToken execution succeeds, token: %s for FCM", token);
        if(onSuccess) {
            onSuccess();
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void PushManager_UpdatePushNickName(void *client, const char * nickname, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    GetPushManager(client)->updatePushNickName(nickname, error);

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_UpdatePushNickName execution succeeds, nickname: %s", nickname);
        if(onSuccess) {
            onSuccess();
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}
