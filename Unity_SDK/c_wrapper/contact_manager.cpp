//
//  contact_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2021/7/21.
//  Copyright © 2021 easemob. All rights reserved.
//
#include <thread>
#include "contact_manager.h"
#include "emclient.h"
#include "tool.h"

EMContactListener *gContactListener = nullptr;

EMContactListener* ContactManager_GetListeners()
{
    return gContactListener;
}

Hypheante_API void ContactManager_AddListener(void *client,
                                        FUNC_OnContactAdded onContactAdded,
                                        FUNC_OnContactDeleted onContactDeleted,
                                        FUNC_OnContactInvited onContactInvited,
                                        FUNC_OnFriendRequestAccepted onFriendRequestAccepted,
                                        FUNC_OnFriendRequestDeclined OnFriendRequestDeclined
                                        )
{
    if(nullptr == gContactListener) { //only set once!
        gContactListener = new ContactManagerListener(onContactAdded, onContactDeleted, onContactInvited, onFriendRequestAccepted, OnFriendRequestDeclined);
        CLIENT->getContactManager().registerContactListener(gContactListener);
    }
}

Hypheante_API void ContactManager_AddContact(void *client, int callbackId, const char* username, const char* reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string usernameStr = username;
    std::string reasonStr = OptionalStrParamCheck(reason);
    
    std::thread t([=](){
        EMError error;
        CLIENT->getContactManager().inviteContact(usernameStr, reasonStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("AddContact success.");
            if(onSuccess) onSuccess(callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_DeleteContact(void *client, int callbackId, const char* username, bool keepConversation, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string usernameStr = username;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getContactManager().deleteContact(usernameStr, error, keepConversation);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("DeleteContact success.");
            if(onSuccess) onSuccess(callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_GetContactsFromServer(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        vector<std::string> vec = CLIENT->getContactManager().getContactsFromServer(error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            int size = (int)vec.size();
            LOG("GetContactsFromServer success, contact num:%d.", size);
            const char* data[size];
            for(int i=0; i<size; i++) {
                data[i] = vec[i].c_str();
            }
            onSuccess((void**)data, DataType::ListOfString, size, callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_GetContactsFromDB(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    vector<std::string> vec = CLIENT->getContactManager().getContactsFromDB(error);
    if(EMError::EM_NO_ERROR == error.mErrorCode) {
        int size = (int)vec.size();
        LOG("GetContactsFromDB success, contact num:%d.",size);
        const char * data[size];
        for(int i=0; i<size; i++) {
            data[i] = vec[i].c_str();
        }
        onSuccess((void **)data, DataType::ListOfString, size, -1);
    } else {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), -1);
        LOG("GetContactsFromDB failed with error:%d, desc:%s.", error.mErrorCode, error.mDescription.c_str());
        return;
    }
}

Hypheante_API void ContactManager_AddToBlackList(void *client, int callbackId, const char* username, bool both, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string usernameStr = username;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getContactManager().addToBlackList(usernameStr, both, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("AddToBlackList success.");
            if(onSuccess) onSuccess(callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_RemoveFromBlackList(void *client, int callbackId, const char* username,FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string usernameStr = username;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getContactManager().removeFromBlackList(usernameStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("RemoveFromBlackList success.");
            if(onSuccess) onSuccess(callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_GetBlackListFromServer(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        vector<std::string> vec = CLIENT->getContactManager().getBlackListFromServer(error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GetBlackListFromServer success.");
            int size = (int)vec.size();
            const char * data[size];
            for(int i=0; i<size; i++) {
                data[i] = vec[i].c_str();
            }
            onSuccess((void**)data, DataType::ListOfString, size, callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_AcceptInvitation(void *client, int callbackId, const char* username,FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string usernameStr = username;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getContactManager().acceptInvitation(usernameStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("AcceptInvitation success.");
            if(onSuccess) onSuccess(callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_DeclineInvitation(void *client, int callbackId, const char* username,FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(username, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string usernameStr = username;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getContactManager().declineInvitation(usernameStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("DeclineInvitation success.");
            if(onSuccess) onSuccess(callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void ContactManager_GetSelfIdsOnOtherPlatform(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        vector<std::string> vec = CLIENT->getContactManager().getSelfIdsOnOtherPlatform(error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GetSelfIdsOnOtherPlatform success.");
            int size = (int)vec.size();
            const char * data[size];
            for(int i=0; i<size; i++) {
                data[i] = vec[i].c_str();
            }
            onSuccess((void**)data, DataType::ListOfString, size, callbackId);
        } else {
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

void ContactManager_RemoveListener(void*client)
{
    if(nullptr != gContactListener)
    {
        CLIENT->getContactManager().removeContactListener(gContactListener);
        LOG("ContactManager listener cleared.");
        delete gContactListener;
        gContactListener = nullptr;
        LOG("ContactManager listener handle deleted.");
    }
}
