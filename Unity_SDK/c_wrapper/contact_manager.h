#ifndef _CONTACT_MANAGER_H_
#define _CONTACT_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//ContactManager methods
AGORA_API void ContactManager_AddListener(void *client,
                                        FUNC_OnContactAdded onContactAdded,
                                        FUNC_OnContactDeleted onContactDeleted,
                                        FUNC_OnContactInvited onContactInvited,
                                        FUNC_OnFriendRequestAccepted onFriendRequestAccepted,
                                        FUNC_OnFriendRequestDeclined OnFriendRequestDeclined
                                        );

AGORA_API void ContactManager_AddContact(void *client, const char* username, const char* reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_DeleteContact(void *client, const char* username, bool keepConversation, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_GetContactsFromServer(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_GetContactsFromDB(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_AddToBlackList(void *client, const char* username, bool both, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_RemoveFromBlackList(void *client, const char* username,FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_GetBlackListFromServer(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_AcceptInvitation(void *client, const char* username,FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_DeclineInvitation(void *client, const char* username,FUNC_OnSuccess onSuccess, FUNC_OnError onError);
AGORA_API void ContactManager_GetSelfIdsOnOtherPlatform(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

EMContactListener* ContactManager_GetListeners();

#endif //_CONTACT_MANAGER_H_

