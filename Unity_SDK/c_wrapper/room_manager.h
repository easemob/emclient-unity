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
AGORA_API void RoomManager_AddListener(void *client, FUNC_OnChatRoomDestroyed onChatRoomDestroyed, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited,
                                       FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved,
                                       FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnAnnouncementChanged onAnnouncementChanged);
AGORA_API void RoomManager_CreateRoom(void *client, const char * subject, const char * desc, const char * welcomMsg, int MaxUserCount, const char * memberArray[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_ChangeRoomSubject(void *client, const char * roomId, const char * newSubject, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_AddRoomAdmin(void * client, const char * roomId, const char * memberId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void RoomManager_RemoveRoomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

#endif /* _ROOM_MANAGER_H_ */
