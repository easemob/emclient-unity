//
//  presence_manager.cpp
//  hyphenateCWrapper
//
//  Created by yuqiang on 2022/4/25.
//  Copyright © 2022 easemob. All rights reserved.
//
#include <thread>
#include "tool.h"
#include "presence_manager.h"

EMPresenceManagerListener *gPresenceManagerListener = nullptr;

HYPHENATE_API void PresenceManager_AddListener(void *client, FUNC_OnPresenceUpdated onPresenceUpdated)
{
    if(nullptr == gPresenceManagerListener) { //only set once!
        gPresenceManagerListener = new PresenceManagerListener(onPresenceUpdated);
        CLIENT->getPresenceManager().addListener(gPresenceManagerListener);
        LOG("New PresenceManager listener and hook it.");
    }
}

HYPHENATE_API void PresenceManager_PublishPresence(void * client, int callbackId, int presenceStatus, const char* ext, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::string extStr = ext;
    
    std::thread t([=](){
        EMErrorPtr error = CLIENT->getPresenceManager().publishPresence(presenceStatus, extStr);
        
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("PresenceManager_PublishPresence execution succeeds: status:%d, ext:%s", presenceStatus, extStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PresenceManager_PublishPresence execution failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void PresenceManager_SubscribePresences(void * client, int callbackId, const char * members[], int size, int64_t expiry, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(nullptr == members || size <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return;
    }
    
    std::vector<std::string> memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    
    std::thread t([=](){
        std::vector<EMPresencePtr> outputPresencesVec;
        EMErrorPtr error = CLIENT->getPresenceManager().subscribePresences(memberList, outputPresencesVec, expiry);
        
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("PresenceManager_SubscribePresences succeeds");
            
            if(onSuccess) {
                size_t size = outputPresencesVec.size();
                if(0 == size) {
                    LOG("PresenceManager_SubscribePresences succeeds, but presence vector is empty.");
                    onSuccess(nullptr, DataType::ListOfString, 0, callbackId);
                    return;
                }
                
                PresenceTO** data = new PresenceTO*[size];
                PresenceTOWrapper* dataLocal = new PresenceTOWrapper[size];
                
                for(size_t i=0; i<size; i++) {
                    PresenceTOWrapper ptoWrapper = PresenceTOWrapper::FromPresence(outputPresencesVec[i]);
                    dataLocal[i] = ptoWrapper;
                    data[i] = &(dataLocal[i].presenceTO);
                }
                
                onSuccess((void **)data, DataType::ListOfString, (int)size, callbackId);
                delete []data;
                delete []dataLocal;
            }
        }else{
            LOG("PresenceManager_SubscribePresences failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void PresenceManager_UnsubscribePresences(void * client, int callbackId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(nullptr == members || size <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return;
    }
    
    std::vector<std::string> memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    
    std::thread t([=](){
        EMErrorPtr error = CLIENT->getPresenceManager().unsubscribePresences(memberList);
        
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("PresenceManager_UnsubscribePresences succeeds");
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("PresenceManager_UnsubscribePresences failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void PresenceManager_FetchSubscribedMembers(void * client, int callbackId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        
        std::vector<std::string> members;
        
        EMErrorPtr error = CLIENT->getPresenceManager().fetchSubscribedMembers(members, pageNum, pageSize);
        
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("PresenceManager_FetchSubscribedMembers succeeds");
            if(onSuccess) {
                size_t size = members.size();
                
                if(0 == size) {
                    LOG("PresenceManager_FetchSubscribedMembers get empty member list.");
                    onSuccess(nullptr, DataType::String, 0, callbackId);
                    return;
                }
                
                std::string jstr = JsonStringFromVector(members);
                const char* data[1];
                data[0] = jstr.c_str();
                onSuccess((void**)data, DataType::String, 1, callbackId);
            }
        }else{
            LOG("PresenceManager_FetchSubscribedMembers failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void PresenceManager_FetchPresenceStatus(void * client, int callbackId, const char * members[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(nullptr == members || size <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        return;
    }
    
    std::vector<std::string> memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    
    std::thread t([=](){
        std::vector<EMPresencePtr> presences;
        EMErrorPtr error = CLIENT->getPresenceManager().fetchPresenceStatus(memberList, presences);
        
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            LOG("PresenceManager_FetchPresenceStatus succeeds");
            
            if(onSuccess) {
                size_t size = presences.size();
                if(0 == size) {
                    LOG("PresenceManager_FetchPresenceStatus succeeds, but presence vector is empty.");
                    onSuccess(nullptr, DataType::ListOfString, 0, callbackId);
                    return;
                }
                
                PresenceTO** data = new PresenceTO*[size];
                PresenceTOWrapper* dataLocal = new PresenceTOWrapper[size];
                
                for(size_t i=0; i<size; i++) {
                    PresenceTOWrapper ptoWrapper = PresenceTOWrapper::FromPresence(presences[i]);
                    dataLocal[i] = ptoWrapper;
                    data[i] = &(dataLocal[i].presenceTO);
                }
                
                onSuccess((void **)data, DataType::ListOfString, (int)size, callbackId);
                delete []data;
                delete []dataLocal;
            }
        }else{
            LOG("PresenceManager_FetchPresenceStatus failed, code=%d, desc=%s", error->mErrorCode, error->mDescription.c_str());
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}
