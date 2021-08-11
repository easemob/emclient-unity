#ifndef _CONVERSATION_MANAGER_H_
#define _CONVERSATION_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"

//to-do:just for testing
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//ConversationManager methods
AGORA_API bool ConversationManager_AppendMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type);
AGORA_API bool ConversationManager_ClearAllMessages(void *client, const char * conversationId, EMConversation::EMConversationType conversationType);
AGORA_API bool ConversationManager_RemoveMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId);
AGORA_API void ConversationManager_ExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size);
AGORA_API bool ConversationManager_InsertMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type);
AGORA_API void ConversationManager_LatestMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size);
AGORA_API void ConversationManager_LatestMessageFromOthers(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size);
AGORA_API void ConversationManager_LoadMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId, void * ext[], int size);
AGORA_API void ConversationManager_LoadMessages(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * startMessageId, int count, EMConversation::EMMessageSearchDirection direction, void * ext[], int size);
AGORA_API void ConversationManager_LoadMessagesWithKeyword(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * keywords, const char * sender,long timestamp, int count, EMConversation::EMMessageSearchDirection direction, void * ext[], int size);
AGORA_API void ConversationManager_LoadMessagesWithMsgType(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, EMMessageBody::EMMessageBodyType type, long timestamp, int count, const char * sender, EMConversation::EMMessageSearchDirection direction, void * ext[], int size);
AGORA_API void ConversationManager_LoadMessagesWithTime(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, long startTimeStamp, long endTimeStamp, int count, void * ext[], int size);
AGORA_API void ConversationManager_MarkAllMessagesAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType);
AGORA_API void ConversationManager_MarkMessageAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId);
AGORA_API void ConversationManager_SetExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * ext);
AGORA_API int ConversationManager_UnreadMessagesCount(void *client, const char * conversationId, EMConversation::EMConversationType conversationType);
AGORA_API bool ConversationManager_UpdateMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type);

//to-do: just for testing
AGORA_API void* DirectReturnSinglePointer();
AGORA_API void* DirectReturnArrayPointers();
AGORA_API void  ParamReturnPointersInStruct(void * astruct);
AGORA_API void  ParamReturnPointersInArray(void * array[], int size);
AGORA_API void  CallbackReturnPointersinStruct(FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void  PrintAndFreeResource();
#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CONVERSATION_MANAGER_H_

