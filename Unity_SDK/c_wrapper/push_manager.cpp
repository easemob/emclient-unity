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

AGORA_API void PushManager_GetIgnoredGroupIds(void *client, void * array[], int size)
{
    std::vector<std::string> ignoreList = CLIENT->getPushManager().getPushConfigs()->getIgnoredGroupIds();
    
    TOArray* toArray = (TOArray*)array[0];
    
    if(ignoreList.size() > 0) {
        LOG("GetIgnoredGroupIds group id number:%d", ignoreList.size());
        toArray->Size = (int)ignoreList.size();
        toArray->Type = DataType::ListOfString;
        
        for(size_t i=0; i<ignoreList.size(); i++) {
            char* p = new char[ignoreList[i].size()+1];
            strncpy(p, ignoreList[i].c_str(), ignoreList[i].size()+1);
            toArray->Data[1] = (void*)p;
        }
    } else {
        toArray->Size = 0;
    }
}

AGORA_API void PushManager_GetPushConfig(void *client, void * array[], int size)
{
    TOArray* toArray = (TOArray*)array[0];
    EMPushConfigsPtr configPtr = CLIENT->getPushManager().getPushConfigs();
    if(!configPtr) {
        LOG("Cannot get any push config ");
        toArray->Size = 0;
        return;
    }

    PushConfigTO* pushConfigTO = new PushConfigTO();
    
    if(configPtr->getDisplayStatus() == easemob::EMPushConfigs::EMPushNoDisturbStatus::Close)
        pushConfigTO->NoDisturb = true;
    else
        pushConfigTO->NoDisturb = false;

    pushConfigTO->NoDisturbStartHour = configPtr->getNoDisturbingStartHour();
    pushConfigTO->NoDisturbEndHour = configPtr->getNoDisturbingEndHour();
    pushConfigTO->Style = configPtr->getDisplayStyle();

    LOG("Push config, starthour:%d, endhour:%d, style:%d", pushConfigTO->NoDisturbStartHour, pushConfigTO->NoDisturbEndHour, pushConfigTO->Style);
    
    toArray->Size = 1;
    toArray->Data[0] = pushConfigTO;
}

AGORA_API void PushManager_GetUserConfigsFromServer(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMPushConfigsPtr configPtr = CLIENT->getPushManager().getUserConfigsFromServer(error);
    if(!configPtr) {
        LOG("Cannot get any push config ");
        onSuccess(nullptr, DataType::Group, 0);
        return;
    }

    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("PushManager_GetUserConfigsFromServer execution succeeds");
        if(onSuccess) {
            
            PushConfigTO pushConfigTO;
            
            if(configPtr->getDisplayStatus() == easemob::EMPushConfigs::EMPushNoDisturbStatus::Close)
                pushConfigTO.NoDisturb = true;
            else
                pushConfigTO.NoDisturb = false;

            pushConfigTO.NoDisturbStartHour = configPtr->getNoDisturbingStartHour();
            pushConfigTO.NoDisturbEndHour = configPtr->getNoDisturbingEndHour();
            pushConfigTO.Style = configPtr->getDisplayStyle();

            PushConfigTO * data[1] = {&pushConfigTO};
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
    CLIENT->getPushManager().ignoreGroupPush(groupId, noDisturb, error);

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
    EMPushConfigsPtr pushConfigPtr = CLIENT->getPushManager().getPushConfigs();
    
    EMPushConfigs::EMPushDisplayStyle style = pushConfigPtr->getDisplayStyle();
    EMPushConfigs::EMPushNoDisturbStatus status = pushConfigPtr->getDisplayStatus();
    
    if(noDisturb)
        status = EMPushConfigs::EMPushNoDisturbStatus::Day; //to-do: is this right to set Day?
    else
        status = EMPushConfigs::EMPushNoDisturbStatus::Close;
    
    CLIENT->getPushManager().updatePushNoDisturbing(style, status, startTime, endTime, error);
    //GetPushManager(client)->updatePushNoDisturbing(style, status, startTime, endTime, error);

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
    CLIENT->getPushManager().updatePushDisplayStyle(style, error);

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
    CLIENT->getPushManager().bindUserDeviceToken(token, "FCM", error);

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
    CLIENT->getPushManager().bindUserDeviceToken(token, "HMS", error);

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
    CLIENT->getPushManager().updatePushNickName(nickname, error);

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

AGORA_API void PushManager_ReleaseStringList(void * stringArray[], int size)
{
    if(size != 1) return;
    TOArray* toArray = (TOArray*)stringArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (char*)toArray->Data[i];
        }
    }
}

AGORA_API void ConversationManager_ReleasePushConfigList(void * pcArray[], int size)
{
    if(size != 1) return;
    TOArray* toArray = (TOArray*)pcArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (PushConfigTO*)toArray->Data[i];
        }
    }
}
