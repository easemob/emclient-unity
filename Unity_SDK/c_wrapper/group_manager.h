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
Hypheante_API void GroupManager_AddListener(void *client,FUNC_OnInvitationReceived,
                                        FUNC_OnRequestToJoinReceived, FUNC_OnRequestToJoinAccepted, FUNC_OnRequestToJoinDeclined,
                                        FUNC_OnInvitationAccepted, FUNC_OnInvitationDeclined, FUNC_OnUserRemoved,
                                        FUNC_OnGroupDestroyed, FUNC_OnAutoAcceptInvitationFromGroup, FUNC_OnMuteListAdded,
                                        FUNC_OnMuteListRemoved, FUNC_OnAdminAdded, FUNC_OnAdminRemoved, FUNC_OnOwnerChanged,
                                        FUNC_OnMemberJoined, FUNC_OnMemberExited, FUNC_OnAnnouncementChanged, FUNC_OnSharedFileAdded,
                                        FUNC_OnSharedFileDeleted);
Hypheante_API void GroupManager_CreateGroup(void *client, int callbackId, const char * groupName, GroupOptions *options, const char * desc, const char * inviteMembers[], int size, const char * inviteReason, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_ChangeGroupName(void *client, int callbackId, const char * groupId, const char * groupName, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_DestoryGroup(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_AddMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_RemoveMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_AddAdmin(void *client, int callbackId, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_GetGroupWithId(void *client, const char * groupId, FUNC_OnSuccess_With_Result onSuccess);
Hypheante_API void GroupManager_AcceptInvitationFromGroup(void *client, int callbackId, const char * groupId, const char * inviter, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_AcceptJoinGroupApplication(void *client, int callbackId, const char * groupId, const char * username, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_AddWhiteListMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_BlockGroupMessage(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_BlockGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_ChangeGroupDescription(void *client, int callbackId, const char * groupId, const char * desc, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_TransferGroupOwner(void *client, int callbackId, const char * groupId, const char * newOwner, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchIsMemberInWhiteList(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_DeclineInvitationFromGroup(void *client, int callbackId, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_DeclineJoinGroupApplication(void *client, int callbackId, const char * groupId, const char * username, const char * reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_DownloadGroupSharedFile(void *client, int callbackId, const char * groupId, const char * filePath, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupAnnouncement(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupBans(void *client, int callbackId, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupSharedFiles(void *client, int callbackId, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupMembers(void *client, int callbackId, const char * groupId, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupMutes(void *client, int callbackId, const char * groupId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupSpecification(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_GetGroupsWithoutNotice(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchGroupWhiteList(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_LoadAllMyGroupsFromDB(void *client, FUNC_OnSuccess_With_Result onSuccess);
Hypheante_API void GroupManager_FetchAllMyGroupsWithPage(void *client, int callbackId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_FetchPublicGroupsWithCursor(void *client, int callbackId, int pageSize, const char * cursor, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_JoinPublicGroup(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_LeaveGroup(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_MuteAllGroupMembers(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_MuteGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, int muteDuration, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_RemoveGroupAdmin(void *client, int callbackId, const char * groupId, const char * admin, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_DeleteGroupSharedFile(void *client, int callbackId, const char * groupId, const char * fileId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_RemoveWhiteListMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_ApplyJoinPublicGroup(void *client, int callbackId, const char * groupId, const char * nickName, const char * message, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_UnblockGroupMessage(void *client, int callbackId, const char * groupId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_UnblockGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_UnMuteAllMembers(void *client, int callbackId, const char * groupId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_UnmuteGroupMembers(void *client, int callbackId, const char * groupId, const char * members[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_UpdateGroupAnnouncement(void *client, int callbackId, const char * groupId, const char * newAnnouncement, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_ChangeGroupExtension(void *client, int callbackId, const char * groupId, const char * newExtension, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
Hypheante_API void GroupManager_UploadGroupSharedFile(void *client, int callbackId, const char * groupId, const char * filePath, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

void GroupManager_RemoveListener(void*client);

#endif //_GROUP_MANAGER_H_
