//
//  room_manager.h
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/7/11.
//  Copyright © 2021 easemob. All rights reserved.
//

#ifndef _ROOM_MANAGER_H_
#define _ROOM_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//RoomManager methods
AGORA_API void RoomManager_AddListener(void *client, FUNC_OnChatRoomDestroyed onChatRoomDestroyed, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExitedFromRoom onMemberExited,
                                       FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved,
                                       FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnAnnouncementChanged onAnnouncementChanged);
AGORA_API void RoomManager_AddRoomAdmin(void * client, const char * roomId, const char * memberId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_BlockChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_CreateRoom(void *client, const char * subject, const char * desc, const char * welcomMsg, int MaxUserCount, const char * memberArray[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_ChangeRoomSubject(void *client, const char * roomId, const char * newSubject, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_RemoveRoomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_TransferChatroomOwner(void * client, const char * roomId, const char * newOwner, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_ChangeChatroomDescription(void * client, const char * roomId, const char * newDescription, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_DestroyChatroom(void *client, const char * roomId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_FetchChatroomsWithPage(void *client, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_FetchChatroomAnnouncement(void *client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_FetchChatroomBans(void *client, const char * roomId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_FetchChatroomSpecification(void * client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_FetchChatroomMembers(void * client, const char * roomId, const char * cursor, int pageSize, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_FetchChatroomMutes(void * client, const char * roomId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_GetAllRoomsFromLocal(void * client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_JoinedChatroomById(void * client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_JoinChatroom(void *client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_LeaveChatroom(void *client, const char * roomId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_MuteChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, int muteDuration, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_RemoveChatroomAdmin(void *client, const char * roomId, const char * adminId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_UnblockChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_UnmuteChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_UpdateChatroomAnnouncement(void *client, const char * roomId, const char * newAnnouncement, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

#endif /* _ROOM_MANAGER_H_ */
