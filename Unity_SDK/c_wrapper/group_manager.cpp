//
//  group_manager.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/7/7.
//  Copyright © 2021 easemob. All rights reserved.
//

#include "group_manager.h"
#include "emclient.h"
#include "tool.h"

static EMCallbackObserverHandle gCallbackObserverHandle;
EMGroupManagerListener *gGroupManagerListener = NULL;

AGORA_API void GroupManager_AddListener(void *client,FUNC_OnInvitationReceived onInvitationReceived, FUNC_OnRequestToJoinReceived onRequestToJoinReceived, FUNC_OnRequestToJoinAccepted onRequestToJoinAccepted, FUNC_OnRequestToJoinDeclined onRequestToJoinDeclined, FUNC_OnInvitationAccepted onInvitationAccepted, FUNC_OnInvitationDeclined onInvitationDeclined, FUNC_OnUserRemoved onUserRemoved, FUNC_OnGroupDestroyed onGroupDestroyed, FUNC_OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroup, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved, FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited, FUNC_OnAnnouncementChanged onAnnouncementChanged, FUNC_OnSharedFileAdded onSharedFileAdded, FUNC_OnSharedFileDeleted onSharedFileDeleted)
{
    if(gGroupManagerListener == NULL) { //only set once!
        gGroupManagerListener = new GroupManagerListener(client, onInvitationReceived,  onRequestToJoinReceived, onRequestToJoinAccepted, onRequestToJoinDeclined, onInvitationAccepted, onInvitationDeclined, onUserRemoved, onGroupDestroyed, onAutoAcceptInvitationFromGroup, onMuteListAdded, onMuteListRemoved, onAdminAdded, onAdminRemoved, onOwnerChanged, onMemberJoined, onMemberExited, onAnnouncementChanged, onSharedFileAdded, onSharedFileDeleted);
        CLIENT->getGroupManager().addListener(gGroupManagerListener);
    }
}

AGORA_API void GroupManager_CreateGroup(void *client, const char * groupName, GroupOptions * options, const char * desc, const char * inviteMembers[], int size, const char * inviteReason, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupName, options, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    LOG("GroupOption, ext:%s, maxcout:%d, style:%d, confirm:%d", options->Ext, options->MaxCount, options->Style, options->InviteNeedConfirm);

    EMMucSetting setting = options->toMucSetting();
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(inviteMembers[i]);
    }
    
    std::string descStr = OptionalStrParamCheck(desc);
    std::string inviteReasonStr = OptionalStrParamCheck(inviteReason);
    
    EMGroupPtr result = CLIENT->getGroupManager().createGroup(groupName, descStr, inviteReasonStr, setting, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("GroupManager_CreateGroup succeeds: name=%s, id=%s", result->groupSubject().c_str(), result->groupId().c_str());
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)(data[0]);
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_ChangeGroupName(void *client, const char * groupId, const char * groupName, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupName, groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }

    CLIENT->getGroupManager().changeGroupSubject(groupId, groupName, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_ChangeGroupName execution succeeds: %s %s", groupId, groupName);
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

AGORA_API void GroupManager_DestoryGroup(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    CLIENT->getGroupManager().destroyGroup(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_DestoryGroup execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_AddMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
         if(onError) onError(error.mErrorCode, error.mDescription.c_str());
         return;
     }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    CLIENT->getGroupManager().addGroupMembers(groupId, memberList, "", error); //TODO: lack of welcome message param. in signature
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_AddMembers execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_RemoveMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
         if(onError) onError(error.mErrorCode, error.mDescription.c_str());
         return;
     }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    CLIENT->getGroupManager().removeGroupMembers(groupId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_RemoveMembers execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_AddAdmin(void *client, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, admin, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().addGroupAdmin(groupId, admin, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_AddAdmin succeeds: %s %s", groupId, admin);
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_GetGroupWithId(void *client, const char * groupId, void * array[], int size)
{
    TOArray* toArray = (TOArray*)array[0];
    EMError error;
    EMGroupPtr result = CLIENT->getGroupManager().fetchGroupSpecification(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        if(result) {
            LOG("GetGroupWithId successfully, group id:%s", result->groupId().c_str());
            GroupTO *datum = GroupTO::FromEMGroup(result);
            toArray->Size = 1;
            toArray->Data[0] = (void*)datum;
            toArray->Type = DataType::Group;
        } else {
            LOG("Cannot get group with id %s", groupId);
            toArray->Size = 0;
        }
    }else{
        LOG("Cannot get group with id %s", groupId);
        toArray->Size = 0;
    }
}

AGORA_API void GroupManager_AcceptInvitationFromGroup(void *client, const char * groupId, const char * inviter, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, inviter, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    EMGroupPtr result = CLIENT->getGroupManager().acceptInvitationFromGroup(groupId, inviter, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            GroupTO *gto = GroupTO::FromEMGroup(result);
            GroupTO *data[1] = {gto};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_AcceptJoinGroupApplication(void *client, const char * groupId, const char * username, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, username, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    EMGroupPtr result = CLIENT->getGroupManager().acceptJoinGroupApplication(groupId, username, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
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

AGORA_API void GroupManager_AddWhiteListMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    CLIENT->getGroupManager().addWhiteListMembers(groupId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("AddWhiteListMembers succeed for group: %s", groupId);
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

AGORA_API void GroupManager_BlockGroupMessage(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().blockGroupMessage(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
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

AGORA_API void GroupManager_BlockGroupMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    CLIENT->getGroupManager().blockGroupMembers(groupId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("Block members of group: %s succesfully", groupId);
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

AGORA_API void GroupManager_ChangeGroupDescription(void *client, const char * groupId, const char * desc, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, desc, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().changeGroupDescription(groupId, desc, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
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

AGORA_API void GroupManager_TransferGroupOwner(void *client, const char * groupId, const char * newOwner, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, newOwner, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().transferGroupOwner(groupId, newOwner, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_TransferGroupOwner succeeds: group:%s newowner: %s", groupId, newOwner);
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchIsMemberInWhiteList(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    bool result = CLIENT->getGroupManager().fetchIsMemberInWhiteList(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchIsMemberInWhiteList succeeds, groupid: %s", groupId);
        if(onSuccess) {
            
            //convert bool to int
            int *boolInt = new int;
            int *data[1] = {boolInt};
            if(result) {
                *(data[0]) = 1; // 1 - true
            }else{
                *(data[0]) = 0; // 0 - false
            }
            onSuccess((void **)data, DataType::Bool, 1);
            delete boolInt;
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_DeclineInvitationFromGroup(void *client, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string usernameStr = OptionalStrParamCheck(username);
    std::string reasonStr = OptionalStrParamCheck(reason);
    CLIENT->getGroupManager().declineInvitationFromGroup(groupId, usernameStr, reasonStr, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
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

AGORA_API void GroupManager_DeclineJoinGroupApplication(void *client, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string usernameStr = OptionalStrParamCheck(username);
    std::string reasonStr = OptionalStrParamCheck(reason);
    CLIENT->getGroupManager().declineJoinGroupApplication(groupId, usernameStr, reasonStr, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
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

AGORA_API void GroupManager_DownloadGroupSharedFile(void *client, const char * groupId, const char * filePath, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, filePath, fileId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Download group shared file succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download group shared file failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    
    EMGroupPtr groupPtr = CLIENT->getGroupManager().downloadGroupSharedFile(groupId, filePath, fileId, callbackPtr, error);
}

AGORA_API void GroupManager_FetchGroupAnnouncement(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string ret = CLIENT->getGroupManager().fetchGroupAnnouncement(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchGroupAnnouncement succeeds, groupid: %s, announcement:%s", groupId, ret.c_str());
        if(onSuccess) {
            const char *data[1] = {ret.c_str()};
            onSuccess((void **)data, DataType::String, 1);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchGroupBans(void *client, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList banList = CLIENT->getGroupManager().fetchGroupBans(groupId, pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchGroupBans succeeds, groupid: %s, banlist size:%d", groupId, banList.size());
        if(onSuccess) {
            size_t size = banList.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                data[i] = banList[i].c_str();
            }
            onSuccess((void **)data, DataType::String, (int)size);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchGroupSharedFiles(void *client, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucSharedFileList fileList = CLIENT->getGroupManager().fetchGroupSharedFiles(groupId, pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_fetchGroupSharedFiles succeeds, groupid: %s, sharedlist size: %d", groupId, fileList.size());
        if(onSuccess) {
            size_t size = fileList.size();
            GroupSharedFileTO * data[size];
            for(size_t i=0; i<size; i++) {
                GroupSharedFileTO* gsTO = new GroupSharedFileTO();
                gsTO->FileName = fileList[i]->fileName().c_str();
                gsTO->FileId = fileList[i]->fileId().c_str();
                gsTO->FileOwner = fileList[i]->fileOwner().c_str();
                gsTO->CreateTime = fileList[i]->create();
                gsTO->FileSize = fileList[i]->fileSize();
                data[i] = gsTO;
            }
            onSuccess((void **)data, DataType::ListOfGroupSharedFile, (int)size);
            for(size_t i=0; i<size; i++) {
                delete (GroupSharedFileTO*)data[i];
            }
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchGroupMembers(void *client, const char * groupId, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string cursorStr = OptionalStrParamCheck(cursor);
    EMCursorResultRaw<std::string> msgCursorResult = CLIENT->getGroupManager().fetchGroupMembers(groupId, cursorStr, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            CursorResultTO cursorResultTo;
            CursorResultTO *data[1] = {&cursorResultTo};
            
            data[0]->NextPageCursor = msgCursorResult.nextPageCursor().c_str();
            size_t size = msgCursorResult.result().size();
            data[0]->Type = DataType::ListOfString;
            data[0]->Size = (int)size;
            LOG("GroupManager_FetchGroupMembers member size: %d", size);
            
            for(int i=0; i<size; i++) {
                data[0]->Data[i] = (void*)msgCursorResult.result().at(i).c_str();
                LOG("member %d: %s", i, msgCursorResult.result().at(i).c_str());
            }
            onSuccess((void **)data, DataType::CursorResult, 1);
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchGroupMutes(void *client, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMuteList muteList = CLIENT->getGroupManager().fetchGroupMutes(groupId, pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchGroupMutes succeeds, groupid: %s, mutelist size:%d", groupId, muteList.size());
        if(onSuccess) {
            size_t size = muteList.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                data[i] = muteList[i].first.c_str();
            }
            onSuccess((void **)data, DataType::String, (int)size);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchGroupSpecification(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().fetchGroupSpecification(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchGroupSpecification succeeds: group:%s", groupId);
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_GetGroupsWithoutNotice(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    error.setErrorCode(EMError::EM_NO_ERROR); //loadAllMyGroupsFromDB no error result
    EMGroupList groupList = CLIENT->getGroupManager().loadAllMyGroupsFromDB();
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            size_t size = groupList.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                if(groupList[i]->isPushEnabled() == false) {
                    data[i] = groupList[i]->groupId().c_str();
                }
            }
            onSuccess((void **)data, DataType::String, (int)size);
        }
    }else{
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_FetchGroupWhiteList(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList = CLIENT->getGroupManager().fetchGroupWhiteList(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchGroupWhiteList succeeds, groupid: %s, memberlist size:%d", groupId, memberList.size());
        if(onSuccess) {
            size_t size = memberList.size();
            const char * data[size];
            
            for(size_t i=0; i<size; i++) {
                data[i] = memberList[i].c_str();
            }
            onSuccess((void **)data, DataType::String, (int)size);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_LoadAllMyGroupsFromDB(void *client, void * array[], int size)
{
    TOArray* toArray = (TOArray*)array[0];
    EMGroupList groupList = CLIENT->getGroupManager().loadAllMyGroupsFromDB();

    if(groupList.size() > 0) {
        LOG("LoadAllMyGroupsFromDB successfully, group num:%d", groupList.size());
        
        toArray->Size = (int)groupList.size();
        toArray->Type = DataType::ListOfGroup;
        
        for(size_t i=0; i<groupList.size(); i++) {
            GroupTO *datum = GroupTO::FromEMGroup(groupList[i]);
            toArray->Data[i] = (void*)datum;
        }
    } else {
        LOG("Cannot get any group in LoadAllMyGroupsFromDB");
        toArray->Size = 0;
    }
}

AGORA_API void GroupManager_FetchAllMyGroupsWithPage(void *client, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMGroupList groupList = CLIENT->getGroupManager().fetchAllMyGroupsWithPage(pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchAllMyGroupsWithPage succeeds, return group size: %d", groupList.size());
        if(onSuccess) {
            size_t size = groupList.size();
            GroupTO * data[size];
            for(size_t i=0; i<size; i++) {
                data[i] = GroupTO::FromEMGroup(groupList[i]);
            }
            onSuccess((void **)data, DataType::ListOfGroup, (int)size);
            for(size_t i=0; i<size; i++) {
                delete (GroupTO*)data[i];
            }
        }
    }else{
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

/*
 AGORA_API void GroupManager_FetchPublicGroupsWithCursor(void *client, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
 {
     EMError error;
     LOG("pageSize is:%d", pageSize);
     
     std::string cursorStr = OptionalStrParamCheck(cursor);
     EMCursorResult cursorResult = CLIENT->getGroupManager().fetchPublicGroupsWithCursor(cursorStr, pageSize, error);
     if(error.mErrorCode == EMError::EM_NO_ERROR) {
         //success
         LOG("GroupManager_FetchPublicGroupsWithCursor succeeds, return group size: %d", cursorResult.result().size());
         if(onSuccess) {
             
             //auto cursorResultTo = new CursorResultTO();
             CursorResultTO cursorResultTo;
             CursorResultTO *data[1] = {&cursorResultTo};
             data[0]->NextPageCursor = cursorResult.nextPageCursor().c_str();
             LOG("nextPageCursor is:%s", cursorResult.nextPageCursor().c_str());
             size_t size = (cursorResult.result().size() > 200)? 200:cursorResult.result().size();
             data[0]->Type = DataType::ListOfGroup;
             data[0]->Size = (int)size;

             for(size_t i=0; i<size; i++) {
                 EMGroupPtr groupPtr = std::dynamic_pointer_cast<EMGroup>(cursorResult.result().at(i));
                 LOG("%d Group id is: %s, group name: %s", i, groupPtr->groupId().c_str(), groupPtr->groupSubject().c_str());
                 data[0]->Data[i] = GroupTO::FromEMGroup(groupPtr);
             }
             LOG("Convert EMGroupPtr to GroupTO complete.");
             onSuccess((void **)data, DataType::CursorResult, 1);
             
             //free memory
             for(size_t i=0; i<size; i++) {
                 delete (GroupTO*)data[0]->Data[i];
             }
         }
     }else{
         if(onError) {
             onError(error.mErrorCode, error.mDescription.c_str());
         }
     }
 }
 */

AGORA_API void GroupManager_FetchPublicGroupsWithCursor(void *client, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    LOG("pageSize is:%d", pageSize);
    
    std::string cursorStr = OptionalStrParamCheck(cursor);
    EMCursorResult cursorResult = CLIENT->getGroupManager().fetchPublicGroupsWithCursor(cursorStr, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_FetchPublicGroupsWithCursor succeeds, return group size: %d", cursorResult.result().size());
        if(onSuccess) {
            
            //auto cursorResultTo = new CursorResultTO();
            CursorResultTO cursorResultTo;
            CursorResultTO *data[1] = {&cursorResultTo};
            data[0]->NextPageCursor = cursorResult.nextPageCursor().c_str();
            LOG("nextPageCursor is:%s", cursorResult.nextPageCursor().c_str());
            size_t size = (cursorResult.result().size() > 200)? 200:cursorResult.result().size();
            data[0]->Type = DataType::ListOfGroup;
            data[0]->Size = (int)size;

            for(size_t i=0; i<size; i++) {
                EMGroupPtr groupPtr = std::dynamic_pointer_cast<EMGroup>(cursorResult.result().at(i));
                LOG("%d Group id is: %s, group name: %s", i, groupPtr->groupId().c_str(), groupPtr->groupSubject().c_str());
                
                GroupInfoTO* grouInfoTo = new GroupInfoTO();
                grouInfoTo->GroupId = groupPtr->groupId().c_str();
                grouInfoTo->Name = groupPtr->groupSubject().c_str();
                data[0]->Data[i] = grouInfoTo;
            }
            LOG("Convert EMGroupPtr to GroupTO complete.");
            onSuccess((void **)data, DataType::CursorResult, 1);
            
            //free memory
            for(size_t i=0; i<size; i++) {
                delete (GroupInfoTO*)data[0]->Data[i];
            }
        }
    }else{
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_JoinPublicGroup(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    CLIENT->getGroupManager().joinPublicGroup(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_JoinPublicGroup execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_LeaveGroup(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    CLIENT->getGroupManager().leaveGroup(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_LeaveGroup execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_MuteAllGroupMembers(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr group = CLIENT->getGroupManager().muteAllGroupMembers(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_MuteAllGroupMembers execution succeeds: %s", groupId);
        
        if(onSuccess) {
            GroupTO * data[1];
            if(group) {
                data[0] = GroupTO::FromEMGroup(group);
                onSuccess((void **)data, DataType::Group, 1);
                delete (GroupTO*)data[0];
            } else {
                data[0] = nullptr;
                onSuccess((void **)data, DataType::Group, 0);
            }
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_MuteGroupMembers(void *client, const char * groupId, const char * members[], int size, int muteDuration, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    EMGroupPtr result = CLIENT->getGroupManager().muteGroupMembers(groupId, memberList, muteDuration, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_MuteGroupMembers succeeds: group:%s", groupId);
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_RemoveGroupAdmin(void *client, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, admin, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().removeGroupAdmin(groupId, admin, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            GroupTO *datum = GroupTO::FromEMGroup(result);
            GroupTO *data[1] = {datum};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_DeleteGroupSharedFile(void *client, const char * groupId, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, fileId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    CLIENT->getGroupManager().deleteGroupSharedFile(groupId, fileId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_DeleteGroupSharedFile execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_RemoveWhiteListMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    CLIENT->getGroupManager().removeWhiteListMembers(groupId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_RemoveWhiteListMembers execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_ApplyJoinPublicGroup(void *client, const char * groupId, const char * nickName, const char * message, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string nickNameStr = OptionalStrParamCheck(nickName);
    std::string messageStr = OptionalStrParamCheck(message);
    CLIENT->getGroupManager().applyJoinPublicGroup(groupId, nickNameStr, messageStr, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_ApplyJoinPublicGroup execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_UnblockGroupMessage(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    CLIENT->getGroupManager().unblockGroupMessage(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_UnblockGroupMessage execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_UnblockGroupMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    CLIENT->getGroupManager().unblockGroupMembers(groupId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_UnblockGroupMembers execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_UnMuteAllMembers(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr group = CLIENT->getGroupManager().unmuteAllGroupMembers(groupId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_UnMuteAllMembers execution succeeds: %s", groupId);
        
        if(onSuccess) {
            GroupTO * data[1];
            if(group) {
                data[0] = GroupTO::FromEMGroup(group);
                onSuccess((void **)data, DataType::Group, 1);
                delete (GroupTO*)data[0];
            } else {
                data[0] = nullptr;
                onSuccess((void **)data, DataType::Group, 0);
            }
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_UnmuteGroupMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMucMemberList memberList;
    for(int i=0; i<size; i++){
        memberList.push_back(members[i]);
    }
    EMGroupPtr result = CLIENT->getGroupManager().unmuteGroupMembers(groupId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_UnmuteGroupMembers succeeds: group:%s", groupId);
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_UpdateGroupAnnouncement(void *client, const char * groupId, const char * newAnnouncement, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, newAnnouncement, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    CLIENT->getGroupManager().updateGroupAnnouncement(groupId, newAnnouncement,error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("GroupManager_UpdateGroupAnnouncement execution succeeds: %s", groupId);
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

AGORA_API void GroupManager_ChangeGroupExtension(void *client, const char * groupId, const char * newExtension, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, newExtension, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMGroupPtr result = CLIENT->getGroupManager().changeGroupExtension(groupId, newExtension, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            GroupTO *datum = GroupTO::FromEMGroup(result);
            GroupTO *data[1] = {datum};
            onSuccess((void **)data, DataType::Group, 1);
            delete (GroupTO*)data[0];
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void GroupManager_UploadGroupSharedFile(void *client, const char * groupId, const char * filePath, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(groupId, filePath, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Upload group shared file succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Failed to upload group shared file failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    
    CLIENT->getGroupManager().uploadGroupSharedFile(groupId, filePath, callbackPtr, error);
}

AGORA_API void GroupManager_ReleaseGroupList(void * groupArray[], int size)
{
    if(size != 1) return;
    TOArray* toArray = (TOArray*)groupArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (GroupTO*)toArray->Data[i];
        }
    }
}
