#ifndef _GROUP_MANAGER_H_
#define _GROUP_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//GroupManager methods
AGORA_API void GroupManager_AddListener(void *client,FUNC_OnInvitationReceived,
                                        FUNC_OnRequestToJoinReceived, FUNC_OnRequestToJoinAccepted, FUNC_OnRequestToJoinDeclined,
                                        FUNC_OnInvitationAccepted, FUNC_OnInvitationDeclined, FUNC_OnUserRemoved,
                                        FUNC_OnGroupDestroyed, FUNC_OnAutoAcceptInvitationFromGroup, FUNC_OnMuteListAdded,
                                        FUNC_OnMuteListRemoved, FUNC_OnAdminAdded, FUNC_OnAdminRemoved, FUNC_OnOwnerChanged,
                                        FUNC_OnMemberJoined, FUNC_OnMemberExited, FUNC_OnAnnouncementChanged, FUNC_OnSharedFileAdded,
                                        FUNC_OnSharedFileDeleted);
AGORA_API void GroupManager_CreateGroup(void *client, const char * groupName, GroupOptions options, const char * desc, const char * inviteMembers[], int size, const char * inviteReason, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);

#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_GROUP_MANAGER_H_
