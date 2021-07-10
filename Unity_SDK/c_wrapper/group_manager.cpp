//
//  group_manager.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/7/7.
//  Copyright © 2021 easemob. All rights reserved.
//

#include "group_manager.h"
#include "emclient.h"

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
    EMMucSetting setting = options->toMucSetting();
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(inviteMembers[i]);
    }
    EMGroupPtr result = CLIENT->getGroupManager().createGroup(groupName, desc, inviteReason, setting, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("GroupManager_CreateGroup succeeds: %s", result->groupSubject().c_str());
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
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

AGORA_API void GroupManager_AddMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
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
    EMGroupPtr result = CLIENT->getGroupManager().addGroupAdmin(groupId, admin, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("GroupManager_AddAdmin succeeds: %s %s", groupId, admin);
        if(onSuccess) {
            GroupTO *data[1] = {GroupTO::FromEMGroup(result)};
            onSuccess((void **)data, DataType::Group, 1);
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}
