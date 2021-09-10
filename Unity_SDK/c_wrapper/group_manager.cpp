//
//  group_manager.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/7/7.
//  Copyright © 2021 easemob. All rights reserved.
//
#include <thread>
#include "group_manager.h"
#include "emclient.h"
#include "tool.h"

static EMCallbackObserverHandle gCallbackObserverHandle;
EMGroupManagerListener *gGroupManagerListener = nullptr;

Hypheante_API void GroupManager_AddListener(void *client,FUNC_OnInvitationReceived onInvitationReceived, FUNC_OnRequestToJoinReceived onRequestToJoinReceived, FUNC_OnRequestToJoinAccepted onRequestToJoinAccepted, FUNC_OnRequestToJoinDeclined onRequestToJoinDeclined, FUNC_OnInvitationAccepted onInvitationAccepted, FUNC_OnInvitationDeclined onInvitationDeclined, FUNC_OnUserRemoved onUserRemoved, FUNC_OnGroupDestroyed onGroupDestroyed, FUNC_OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroup, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved, FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited, FUNC_OnAnnouncementChanged onAnnouncementChanged, FUNC_OnSharedFileAdded onSharedFileAdded, FUNC_OnSharedFileDeleted onSharedFileDeleted)
{
    if(nullptr == gGroupManagerListener) { //only set once!
        gGroupManagerListener = new GroupManagerListener(client, onInvitationReceived,  onRequestToJoinReceived, onRequestToJoinAccepted, onRequestToJoinDeclined, onInvitationAccepted, onInvitationDeclined, onUserRemoved, onGroupDestroyed, onAutoAcceptInvitationFromGroup, onMuteListAdded, onMuteListRemoved, onAdminAdded, onAdminRemoved, onOwnerChanged, onMemberJoined, onMemberExited, onAnnouncementChanged, onSharedFileAdded, onSharedFileDeleted);
        CLIENT->getGroupManager().addListener(gGroupManagerListener);
    }
}

Hypheante_API void GroupManager_CreateGroup(void *client, int callbackId, const char * groupName, GroupOptions * options, const char * desc, const char * inviteMembers[], int size, const char * inviteReason, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupName, options, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    LOG("GroupOption, ext:%s, maxcout:%d, style:%d, confirm:%d", options->Ext, options->MaxCount, options->Style, options->InviteNeedConfirm);
    EMMucSetting setting = options->toMucSetting();
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(inviteMembers[i]);
    }
    std::string groupNameStr = groupName;
    std::string descStr = OptionalStrParamCheck(desc);
    std::string inviteReasonStr = OptionalStrParamCheck(inviteReason);
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().createGroup(groupNameStr, descStr, inviteReasonStr, setting, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_CreateGroup succeeds: name=%s, id=%s", result->groupSubject().c_str(), result->groupId().c_str());
            if(onSuccess) {
                GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)(data[0]);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_ChangeGroupName(void *client, int callbackId, const char * groupId, const char * groupName, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupName, groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupNameStr = groupName;
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().changeGroupSubject(groupIdStr, groupNameStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_ChangeGroupName execution succeeds: %s %s", groupIdStr.c_str(), groupNameStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_DestoryGroup(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().destroyGroup(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_DestoryGroup execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_AddMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
         if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
         return;
     }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().addGroupMembers(groupIdStr, memberList, "", error); //TODO: lack of welcome message param. in signature
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_AddMembers execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_RemoveMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
         if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
         return;
     }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().removeGroupMembers(groupIdStr, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_RemoveMembers execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_AddAdmin(void *client, int callbackId, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, admin, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string adminStr = admin;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().addGroupAdmin(groupIdStr, adminStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_AddAdmin succeeds: %s %s", groupIdStr.c_str(), adminStr.c_str());
            if(onSuccess) {
                GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_GetGroupWithId(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess)
{
    if(!MandatoryCheck(groupId)) {
        onSuccess(nullptr, DataType::Group, 0, -1);
        return;
    }
    EMError error;
    EMGroupPtr result = CLIENT->getGroupManager().fetchGroupSpecification(groupId, error);
    GroupTO* data[1];
    if(EMError::EM_NO_ERROR == error.mErrorCode) {
        if(result) {
            LOG("GetGroupWithId successfully, group id:%s", result->groupId().c_str());
            data[0] = GroupTO::FromEMGroup(result);
            data[0]->LogInfo();
            onSuccess((void **)data, DataType::Group, 1, -1);
            delete (GroupTO*)data[0];
            return;
        }
    }
    LOG("Cannot get group with id %s", groupId);
    onSuccess(nullptr, DataType::Group, 0, -1);
}

Hypheante_API void GroupManager_AcceptInvitationFromGroup(void *client, int callbackId, const char * groupId, const char * inviter, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string inviterStr = OptionalStrParamCheck(inviter);
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().acceptInvitationFromGroup(groupIdStr, inviterStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) {
                GroupTO *gto = GroupTO::FromEMGroup(result);
                GroupTO *data[1] = {gto};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_AcceptJoinGroupApplication(void *client, int callbackId, const char * groupId, const char * username, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, username, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string usernameStr = username;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().acceptJoinGroupApplication(groupIdStr, usernameStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_AddWhiteListMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().addWhiteListMembers(groupIdStr, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("AddWhiteListMembers succeed for group: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_BlockGroupMessage(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().blockGroupMessage(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_BlockGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().blockGroupMembers(groupIdStr, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("Block members of group: %s succesfully", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_ChangeGroupDescription(void *client, int callbackId, const char * groupId, const char * desc, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, desc, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string descStr = desc;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().changeGroupDescription(groupIdStr, descStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_TransferGroupOwner(void *client, int callbackId, const char * groupId, const char * newOwner, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, newOwner, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string newOwnerStr = newOwner;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().transferGroupOwner(groupIdStr, newOwnerStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_TransferGroupOwner succeeds: group:%s newowner: %s", groupIdStr.c_str(), newOwnerStr.c_str());
            if(onSuccess) {
                GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchIsMemberInWhiteList(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        bool result = CLIENT->getGroupManager().fetchIsMemberInWhiteList(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchIsMemberInWhiteList succeeds, groupid: %s", groupIdStr.c_str());
            if(onSuccess) {
                //convert bool to int
                int *boolInt = new int;
                int *data[1] = {boolInt};
                if(result) {
                    *(data[0]) = 1; // 1 - true
                }else{
                    *(data[0]) = 0; // 0 - false
                }
                onSuccess((void **)data, DataType::Bool, 1, callbackId);
                delete boolInt;
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_DeclineInvitationFromGroup(void *client, int callbackId, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string usernameStr = OptionalStrParamCheck(username);
    std::string reasonStr = OptionalStrParamCheck(reason);
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().declineInvitationFromGroup(groupIdStr, usernameStr, reasonStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_DeclineJoinGroupApplication(void *client, int callbackId, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string usernameStr = OptionalStrParamCheck(username);
    std::string reasonStr = OptionalStrParamCheck(reason);
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().declineJoinGroupApplication(groupIdStr, usernameStr, reasonStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_DownloadGroupSharedFile(void *client, int callbackId, const char * groupId, const char * filePath, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, filePath, fileId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Download group shared file succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download group shared file failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                return true;
                                             }));
    
    EMGroupPtr groupPtr = CLIENT->getGroupManager().downloadGroupSharedFile(groupId, filePath, fileId, callbackPtr, error);
}

Hypheante_API void GroupManager_FetchGroupAnnouncement(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        std::string ret = CLIENT->getGroupManager().fetchGroupAnnouncement(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchGroupAnnouncement succeeds, groupid: %s, announcement:%s", groupIdStr.c_str(), ret.c_str());
            if(onSuccess) {
                const char *data[1] = {ret.c_str()};
                onSuccess((void **)data, DataType::String, 1, callbackId);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchGroupBans(void *client, int callbackId, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMMucMemberList banList = CLIENT->getGroupManager().fetchGroupBans(groupIdStr, pageNum, pageSize, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchGroupBans succeeds, groupid: %s, banlist size:%d", groupIdStr.c_str(), banList.size());
            if(onSuccess) {
                size_t size = banList.size();
                const char * data[size];
                for(size_t i=0; i<size; i++) {
                    data[i] = banList[i].c_str();
                }
                onSuccess((void **)data, DataType::String, (int)size, callbackId);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchGroupSharedFiles(void *client, int callbackId, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMMucSharedFileList fileList = CLIENT->getGroupManager().fetchGroupSharedFiles(groupIdStr, pageNum, pageSize, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_fetchGroupSharedFiles succeeds, groupid: %s, sharedlist size: %d", groupIdStr.c_str(), fileList.size());
            if(onSuccess) {
                size_t size = fileList.size();
                GroupSharedFileTO * data[size];
                for(size_t i=0; i<size; i++) {
                    data[i] = GroupSharedFileTO::FromEMGroupSharedFile(fileList[i]);
                    LOG("share file %d, id=%s, name=%s", i, data[i]->FileId, data[i]->FileName);
                }
                onSuccess((void **)data, DataType::ListOfGroupSharedFile, (int)size, callbackId);
                for(size_t i=0; i<size; i++) {
                    GroupSharedFileTO::DeleteGroupSharedFileTO(data[i]);
                }
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchGroupMembers(void *client, int callbackId, const char * groupId, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string cursorStr = OptionalStrParamCheck(cursor);
    
    std::thread t([=](){
        EMError error;
        EMCursorResultRaw<std::string> msgCursorResult = CLIENT->getGroupManager().fetchGroupMembers(groupIdStr, cursorStr, pageSize, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) {
                //header
                CursorResultTOV2 cursorResultTo;
                cursorResultTo.NextPageCursor = msgCursorResult.nextPageCursor().c_str();
                cursorResultTo.Type = DataType::ListOfString;
                //items
                int size = (int)msgCursorResult.result().size();
                LOG("GroupManager_FetchGroupMembers member size: %d", size);
                const char *data[size];
                for(int i=0; i<size; i++) {
                    data[i] = msgCursorResult.result().at(i).c_str();
                    LOG("member %d: %s", i, data[i]);
                }
                onSuccess((void *)&cursorResultTo, (void **)data, DataType::CursorResult, size, callbackId);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchGroupMutes(void *client, int callbackId, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMMucMuteList muteList = CLIENT->getGroupManager().fetchGroupMutes(groupIdStr, pageNum, pageSize, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchGroupMutes succeeds, groupid: %s, mutelist size:%d", groupIdStr.c_str(), muteList.size());
            if(onSuccess) {
                size_t size = muteList.size();
                const char * data[size];
                for(size_t i=0; i<size; i++) {
                    data[i] = muteList[i].first.c_str();
                }
                onSuccess((void **)data, DataType::String, (int)size, callbackId);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchGroupSpecification(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().fetchGroupSpecification(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchGroupSpecification succeeds: group:%s", groupIdStr.c_str());
            if(onSuccess) {
                GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_GetGroupsWithoutNotice(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        error.setErrorCode(EMError::EM_NO_ERROR); //loadAllMyGroupsFromDB no error result
        EMGroupList groupList = CLIENT->getGroupManager().loadAllMyGroupsFromDB();
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            //success
            if(onSuccess) {
                size_t size = groupList.size();
                const char * data[size];
                for(size_t i=0; i<size; i++) {
                    if(groupList[i]->isPushEnabled() == false) {
                        data[i] = groupList[i]->groupId().c_str();
                    }
                }
                onSuccess((void **)data, DataType::String, (int)size, callbackId);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchGroupWhiteList(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMMucMemberList memberList = CLIENT->getGroupManager().fetchGroupWhiteList(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchGroupWhiteList succeeds, groupid: %s, memberlist size:%d", groupIdStr.c_str(), memberList.size());
            if(onSuccess) {
                size_t size = memberList.size();
                const char * data[size];
                
                for(size_t i=0; i<size; i++) {
                    data[i] = memberList[i].c_str();
                }
                onSuccess((void **)data, DataType::String, (int)size, callbackId);
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_LoadAllMyGroupsFromDB(void *client, FUNC_OnSuccess_With_Result onSuccess)
{
    EMGroupList groupList = CLIENT->getGroupManager().loadAllMyGroupsFromDB();
    int size = (int)groupList.size();
    LOG("LoadAllMyGroupsFromDB successfully, group num:%d", size);
    
    GroupTO *data[size];
    for(size_t i=0; i<size; i++) {
        data[i] = GroupTO::FromEMGroup(groupList[i]);
    }
    onSuccess((void **)data, DataType::ListOfGroup, size, -1);
    for(size_t i=0; i<size; i++) {
        delete (GroupTO*)data[i];
    }
}

Hypheante_API void GroupManager_FetchAllMyGroupsWithPage(void *client, int callbackId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        EMGroupList groupList = CLIENT->getGroupManager().fetchAllMyGroupsWithPage(pageNum, pageSize, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchAllMyGroupsWithPage succeeds, return group size: %d", groupList.size());
            if(onSuccess) {
                size_t size = groupList.size();
                GroupTO * data[size];
                for(size_t i=0; i<size; i++) {
                    data[i] = GroupTO::FromEMGroup(groupList[i]);
                    data[i]->LogInfo();
                }
                onSuccess((void **)data, DataType::ListOfGroup, (int)size, callbackId);
                for(size_t i=0; i<size; i++) {
                    delete (GroupTO*)data[i];
                }
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_FetchPublicGroupsWithCursor(void *client, int callbackId, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError)
{
    std::string cursorStr = OptionalStrParamCheck(cursor);
    
    std::thread t([=](){
        EMError error;
        LOG("pageSize is:%d", pageSize);
        
        EMCursorResult cursorResult = CLIENT->getGroupManager().fetchPublicGroupsWithCursor(cursorStr, pageSize, error);
        
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_FetchPublicGroupsWithCursor succeeds, return group size: %d", cursorResult.result().size());
            if(onSuccess) {
                //header
                CursorResultTOV2 cursorResultTo;
                cursorResultTo.NextPageCursor = cursorResult.nextPageCursor().c_str();
                LOG("nextPageCursor is:%s", cursorResultTo.NextPageCursor);
                cursorResultTo.Type = DataType::ListOfGroup;
                //items
                size_t size = cursorResult.result().size();
                GroupInfoTO* data[size];
                for(size_t i=0; i<size; i++) {
                    EMGroupPtr groupPtr = std::dynamic_pointer_cast<EMGroup>(cursorResult.result().at(i));
                    LOG("%d Public group id is: %s, group name: %s", i, groupPtr->groupId().c_str(), groupPtr->groupSubject().c_str());
                    
                    GroupInfoTO* grouInfoTo = new GroupInfoTO();
                    grouInfoTo->GroupId = groupPtr->groupId().c_str();
                    grouInfoTo->Name = groupPtr->groupSubject().c_str();
                    data[i] = grouInfoTo;
                }
                LOG("Convert EMGroupPtr to GroupTO(%d) complete.",size);
                onSuccess((void *)&cursorResultTo, (void **)data, DataType::CursorResult, (int)size, callbackId);
                //free memory
                for(size_t i=0; i<size; i++) {
                    delete (GroupInfoTO*)data[i];
                }
            }
        }else{
            if(onError) {
                onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
            }
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_JoinPublicGroup(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().joinPublicGroup(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_JoinPublicGroup execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_LeaveGroup(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().leaveGroup(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_LeaveGroup execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_MuteAllGroupMembers(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr group = CLIENT->getGroupManager().muteAllGroupMembers(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_MuteAllGroupMembers execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) {
                if(group) {
                    GroupTO * data[1];
                    data[0] = GroupTO::FromEMGroup(group);
                    onSuccess((void **)data, DataType::Group, 1, callbackId);
                    delete (GroupTO*)data[0];
                } else {
                    onSuccess(nullptr, DataType::Group, 0, callbackId);
                }
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_MuteGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, int muteDuration, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().muteGroupMembers(groupIdStr, memberList, muteDuration, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_MuteGroupMembers succeeds: group:%s", groupIdStr.c_str());
            if(onSuccess) {
                GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_RemoveGroupAdmin(void *client, int callbackId, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, admin, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string adminStr = admin;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().removeGroupAdmin(groupIdStr, adminStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) {
                GroupTO *datum = GroupTO::FromEMGroup(result);
                GroupTO *data[1] = {datum};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_DeleteGroupSharedFile(void *client, int callbackId, const char * groupId, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, fileId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string fileIdStr = fileId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().deleteGroupSharedFile(groupIdStr, fileIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_DeleteGroupSharedFile succeeds, group=%s, fileid=%s", groupIdStr.c_str(), fileIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            LOG("GroupManager_DeleteGroupSharedFile failed, group=%s, fileid=%s", groupIdStr.c_str(), fileIdStr.c_str());
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_RemoveWhiteListMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().removeWhiteListMembers(groupIdStr, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_RemoveWhiteListMembers execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_ApplyJoinPublicGroup(void *client, int callbackId, const char * groupId, const char * nickName, const char * message, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string nickNameStr = OptionalStrParamCheck(nickName);
    std::string messageStr = OptionalStrParamCheck(message);
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().applyJoinPublicGroup(groupIdStr, nickNameStr, messageStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_ApplyJoinPublicGroup execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_UnblockGroupMessage(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().unblockGroupMessage(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_UnblockGroupMessage execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_UnblockGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().unblockGroupMembers(groupIdStr, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_UnblockGroupMembers execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_UnMuteAllMembers(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr group = CLIENT->getGroupManager().unmuteAllGroupMembers(groupIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_UnMuteAllMembers execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) {
                GroupTO * data[1];
                if(group) {
                    data[0] = GroupTO::FromEMGroup(group);
                    onSuccess((void **)data, DataType::Group, 1, callbackId);
                    delete (GroupTO*)data[0];
                } else {
                    data[0] = nullptr;
                    onSuccess((void **)data, DataType::Group, 0, callbackId);
                }
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_UnmuteGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    std::string groupIdStr = groupId;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().unmuteGroupMembers(groupIdStr, memberList, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_UnmuteGroupMembers succeeds: group:%s", groupIdStr.c_str());
            if(onSuccess) {
                GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_UpdateGroupAnnouncement(void *client, int callbackId, const char * groupId, const char * newAnnouncement, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, newAnnouncement, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string newAnnouncementStr = newAnnouncement;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getGroupManager().updateGroupAnnouncement(groupIdStr, newAnnouncementStr,error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("GroupManager_UpdateGroupAnnouncement execution succeeds: %s", groupIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_ChangeGroupExtension(void *client, int callbackId, const char * groupId, const char * newExtension, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, newExtension, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string groupIdStr = groupId;
    std::string newExtensionStr = newExtension;
    
    std::thread t([=](){
        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().changeGroupExtension(groupIdStr, newExtensionStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            if(onSuccess) {
                GroupTO *datum = GroupTO::FromEMGroup(result);
                GroupTO *data[1] = {datum};
                onSuccess((void **)data, DataType::Group, 1, callbackId);
                delete (GroupTO*)data[0];
            }
        }else{
            if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

Hypheante_API void GroupManager_UploadGroupSharedFile(void *client, int callbackId, const char * groupId, const char * filePath, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, filePath, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Upload group shared file succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Failed to upload group shared file failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                return true;
                                             }));
    
    CLIENT->getGroupManager().uploadGroupSharedFile(groupId, filePath, callbackPtr, error);
}
