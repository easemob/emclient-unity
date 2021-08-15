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
AGORA_API void GroupManager_CreateGroup(void *client, const char * groupName, GroupOptions *options, const char * desc, const char * inviteMembers[], int size, const char * inviteReason, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_ChangeGroupName(void *client, const char * groupId, const char * groupName, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_DestoryGroup(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_AddMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_RemoveMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_AddAdmin(void *client, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_GetGroupWithId(void *client, const char * groupId, void * array[], int size);
AGORA_API void GroupManager_AcceptInvitationFromGroup(void *client, const char * groupId, const char * inviter, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_AcceptJoinGroupApplication(void *client, const char * groupId, const char * username, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_AddWhiteListMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_BlockGroupMessage(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_BlockGroupMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_ChangeGroupDescription(void *client, const char * groupId, const char * desc, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_TransferGroupOwner(void *client, const char * groupId, const char * newOwner, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchIsMemberInWhiteList(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_DeclineInvitationFromGroup(void *client, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_DeclineJoinGroupApplication(void *client, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_DownloadGroupSharedFile(void *client, const char * groupId, const char * filePath, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupAnnouncement(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupBans(void *client, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupSharedFiles(void *client, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupMembers(void *client, const char * groupId, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupMutes(void *client, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupSpecification(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_GetGroupsWithoutNotice(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchGroupWhiteList(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_LoadAllMyGroupsFromDB(void *client, void * array[], int size);
AGORA_API void GroupManager_FetchAllMyGroupsWithPage(void *client, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_FetchPublicGroupsWithCursor(void *client, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_JoinPublicGroup(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_LeaveGroup(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_MuteAllGroupMembers(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_MuteGroupMembers(void *client, const char * groupId, const char * members[], int size, int muteDuration, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_RemoveGroupAdmin(void *client, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_DeleteGroupSharedFile(void *client, const char * groupId, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_RemoveWhiteListMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_ApplyJoinPublicGroup(void *client, const char * groupId, const char * nickName, const char * message, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_UnblockGroupMessage(void *client, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_UnblockGroupMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_UnMuteAllMembers(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_UnmuteGroupMembers(void *client, const char * groupId, const char * members[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_UpdateGroupAnnouncement(void *client, const char * groupId, const char * newAnnouncement, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_ChangeGroupExtension(void *client, const char * groupId, const char * newExtension, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void GroupManager_UploadGroupSharedFile(void *client, const char * groupId, const char * filePath, FUNC_OnSuccess onSuccess, FUNC_OnError onError);

AGORA_API void GroupManager_ReleaseGroupList(void * groupArray[], int size);
#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_GROUP_MANAGER_H_
