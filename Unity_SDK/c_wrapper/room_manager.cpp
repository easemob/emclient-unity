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

AGORA_API void RoomManager_AddListener(void *client, FUNC_OnChatRoomDestroyed onChatRoomDestroyed, FUNC_OnMemberJoined onMemberJoined,
                                       FUNC_OnMemberExitedFromRoom onMemberExited,FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom,
                                       FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved,FUNC_OnAdminAdded onAdminAdded,
                                       FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnAnnouncementChanged onAnnouncementChanged)
{
    if(gRoomManagerListener == NULL) { //only set once!
        gRoomManagerListener = new RoomManagerListener(client, onChatRoomDestroyed,  onMemberJoined, onMemberExited, onRemovedFromChatRoom, onMuteListAdded, onMuteListRemoved, onAdminAdded, onAdminRemoved, onOwnerChanged, onAnnouncementChanged);
       CLIENT->getChatroomManager().addListener(gRoomManagerListener);
    }
}

AGORA_API void RoomManager_AddRoomAdmin(void * client, const char * roomId, const char * memberId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().addChatroomAdmin(roomId, memberId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_AddRoomAdmin succeeds: %s", result->chatroomSubject().c_str());
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

AGORA_API void RoomManager_BlockChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(memberArray[i]);
    }
    EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().blockChatroomMembers(roomId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_BlockChatroomMembers succeeds: %s", chatRoomPtr->chatroomSubject().c_str());
        if(onSuccess) {
            RoomTO *datum = RoomTO::FromEMChatRoom(chatRoomPtr);
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

AGORA_API void RoomManager_RemoveRoomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(memberArray[i]);
    }
    EMChatroomPtr result = CLIENT->getChatroomManager().removeChatroomMembers(roomId, memberList, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_RemoveRoomMembers succeeds: %s", result->chatroomSubject().c_str());
        if(onSuccess) {
            onSuccess();
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
    
}

AGORA_API void RoomManager_TransferChatroomOwner(void * client, const char * roomId, const char * newOwner, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().transferChatroomOwner(roomId, newOwner, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_TransferChatroomOwner succeeds: %s", result->chatroomSubject().c_str());
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

AGORA_API void RoomManager_ChangeChatroomDescription(void * client, const char * roomId, const char * newDescription, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().changeChatroomDescription(roomId, newDescription, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_ChangeChatroomDescription succeeds: %s", result->chatroomSubject().c_str());
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

AGORA_API void RoomManager_DestroyChatroom(void *client, const char * roomId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    CLIENT->getChatroomManager().destroyChatroom(roomId, error);
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

AGORA_API void RoomManager_FetchChatroomsWithPage(void *client, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMPageResult pageResult = CLIENT->getChatroomManager().fetchChatroomsWithPage(pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            int size = (int)pageResult.result().size();
            RoomTO* data[size];
            for(int i=0; i<size; i++) {
                EMChatroomPtr charRoomPtr((EMChatroom*)pageResult.result().at(i).get());
                data[i] = RoomTO::FromEMChatRoom(charRoomPtr);
            }
            onSuccess((void **)data, DataType::Room, (int)size);
            //NOTE: NO need to release mem. after onSuccess call, managed side would free them.
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void RoomManager_FetchChatroomAnnouncement(void *client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    std::string announcement = CLIENT->getChatroomManager().fetchChatroomAnnouncement(roomId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_FetchChatroomAnnouncement succeeds: %s", roomId);
        if(onSuccess) {
            char* ptr = new char[announcement.size()+1];
            strncpy(ptr, announcement.c_str(), announcement.size()+1);
            char *data[1] = {ptr};
            onSuccess((void **)data, DataType::String, 1);
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void RoomManager_FetchChatroomBans(void *client, const char * roomId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMemberList banList = CLIENT->getChatroomManager().fetchChatroomBans(roomId, pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("RoomManager_FetchChatroomBans succeeds, roomId: %s", roomId);
        if(onSuccess) {
            size_t size = banList.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                char* ptr = new char[banList[i].size()+1];
                strncpy(ptr, banList[i].c_str(), banList[i].size()+1);
                data[i] = ptr;
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

AGORA_API void RoomManager_FetchChatroomSpecification(void * client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().fetchChatroomSpecification(roomId, error, false);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //succees
        LOG("RoomManager_FetchChatroomSpecification succeeds roomId: %s", result->chatroomId().c_str());
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

AGORA_API void RoomManager_FetchChatroomMembers(void * client, const char * roomId, const char * cursor, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMCursorResultRaw<std::string> msgCursorResult = CLIENT->getChatroomManager().fetchChatroomMembers(roomId, cursor, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            auto cursorResultTo = new CursorResultTO();
            CursorResultTO *data[1] = {cursorResultTo};
            data[0]->NextPageCursor = msgCursorResult.nextPageCursor().c_str();
            size_t size = msgCursorResult.result().size();
            data[0]->Type = DataType::ListOfString;
            data[0]->Size = (int)size;
            //copy string
            for(int i=0; i<size; i++) {
                std::string member = msgCursorResult.result().at(i);
                char* ptr = new char[member.size()+1];
                strncpy(ptr, member.c_str(), member.size()+1);
                data[0]->Data[i] = ptr;
            }
            onSuccess((void **)data, DataType::CursorResult, 1);
            //NOTE: NO need to release mem. after onSuccess call, managed side would free them.
        }
    }else{
        //error
        if(onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void RoomManager_FetchChatroomMutes(void * client, const char * roomId, int pageNum, int pageSize, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMuteList muteList = CLIENT->getChatroomManager().fetchChatroomMutes(roomId, pageNum, pageSize, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        LOG("RoomManager_FetchChatroomMutes succeeds, roomId: %s", roomId);
        if(onSuccess) {
            size_t size = muteList.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                char* ptr = new char[muteList[i].first.size()+1];
                strncpy(ptr, muteList[i].first.c_str(), muteList[i].first.size()+1);
                data[i] = ptr;
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

AGORA_API void RoomManager_GetAllRoomsFromLocal(void * client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    //mdata private??
    //loadAllChatroomsFromDB
    //CLIENT->getChatroomManager()
}

AGORA_API void RoomManager_JoinedChatroomById(void * client, const char * roomId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMChatroomPtr result = CLIENT->getChatroomManager().joinedChatroomById(roomId);
    if(result != nullptr)
    {
        //succees
        LOG("RoomManager_JoinedChatroomById succeeds roomId: %s", result->chatroomId().c_str());
        if(onSuccess) {
            RoomTO *datum = RoomTO::FromEMChatRoom(result);
            RoomTO *data[1] = {datum};
            datum->LogInfo();
            onSuccess((void **)data, DataType::Room, 1);
        }
    }else{
        LOG("RoomManager_JoinedChatroomById succeeds, but no room is found");
    }
}

AGORA_API void RoomManager_JoinChatroom(void *client, const char * roomId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().joinChatroom(roomId, error);
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

AGORA_API void RoomManager_LeaveChatroom(void *client, const char * roomId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    CLIENT->getChatroomManager().leaveChatroom(roomId, error);
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

AGORA_API void RoomManager_MuteChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, int muteDuration, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(memberArray[i]);
    }
    EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().muteChatroomMembers(roomId, memberList, muteDuration, error);
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

AGORA_API void RoomManager_RemoveChatroomAdmin(void *client, const char * roomId, const char * adminId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr result = CLIENT->getChatroomManager().removeChatroomAdmin(roomId, adminId, error);
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

AGORA_API void RoomManager_UnblockChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(memberArray[i]);
    }
    EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().unblockChatroomMembers(roomId, memberList, error);
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

AGORA_API void RoomManager_UnmuteChatroomMembers(void * client, const char * roomId, const char * memberArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMucMemberList memberList;
    for(int i=0; i<size; i++) {
        memberList.push_back(memberArray[i]);
    }
    EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().unmuteChatroomMembers(roomId, memberList, error);
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

AGORA_API void RoomManager_UpdateChatroomAnnouncement(void *client, const char * roomId, const char * newAnnouncement, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().updateChatroomAnnouncement(roomId, newAnnouncement, error);
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
