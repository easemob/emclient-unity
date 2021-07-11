//
//  room_manager.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/7/11.
//  Copyright © 2021 easemob. All rights reserved.
//

#include "room_manager.h"
#include "emclient.h"

using namespace easemob;
EMChatroomManagerListener *gRoomManagerListener = NULL;

AGORA_API void RoomManager_AddListener(void *client, FUNC_OnChatRoomDestroyed onChatRoomDestroyed, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited,
                                       FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved,
                                       FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnAnnouncementChanged onAnnouncementChanged)
{
    if(gRoomManagerListener == NULL) { //only set once!
        gRoomManagerListener = new RoomManagerListener(client, onChatRoomDestroyed,  onMemberJoined, onMemberExited, onRemovedFromChatRoom, onMuteListAdded, onMuteListRemoved, onAdminAdded, onAdminRemoved, onOwnerChanged, onAnnouncementChanged);
        CLIENT->getChatroomManager().addListener(gRoomManagerListener);
    }
}

AGORA_API void RoomManager_CreateRoom(void *client, const char * subject, const char * desc, const char * welcomMsg, int maxUserCount, const char * memberArray[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucSetting setting(EMMucSetting::EMMucStyle::DEFAUT, maxUserCount, false);
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(memberArray[i]);
    }
    EMChatroomPtr result = CLIENT->getChatroomManager().createChatroom(subject, desc, welcomMsg, setting, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_CreateRoom succeeds: %s", result->chatroomId().c_str());
        if(onSuccess) {
            RoomTO *data[1] = {RoomTO::FromEMChatRoom(result)};
            onSuccess((void **)data, DataType::Room, 1);
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void RoomManager_ChangeRoomSubject(void *client, const char * roomId, const char * newSubject, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().changeChatroomSubject(roomId, newSubject, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_ChangeRoomSubject succeeds: %s", result->chatroomSubject().c_str());
        if(onSuccess) {
            RoomTO *datum = RoomTO::FromEMChatRoom(result);
            RoomTO *data[1] = {datum};
            datum->LogInfo();
            onSuccess((void **)data, DataType::Room, 1);
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}
