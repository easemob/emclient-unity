#ifndef _CHAT_MANAGER_H_
#define _CHAT_MANAGER_H_

#pragma once
#include "common.h"
#include "models.h"
#include "callbacks.h"

#ifdef __cplusplus
extern "C"
{
#endif //__cplusplus
//ChatManager methods
HYPHENATE_API void ChatManager_SendMessage(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress, void *mto, EMMessageBody::EMMessageBodyType type);
HYPHENATE_API void ChatManager_FetchHistoryMessages(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType type, 
    const char * startMessageId, int count, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_GetConversationsFromServer(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_AddListener(void *client,
                                       FUNC_OnMessagesReceived onMessagesReceived,
                                       FUNC_OnCmdMessagesReceived onCmdMessagesReceived,
                                       FUNC_OnMessagesRead onMessagesRead,
                                       FUNC_OnMessagesDelivered onMessagesDelivered,
                                       FUNC_OnMessagesRecalled onMessagesRecalled,
                                       FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated,
                                       FUNC_OnGroupMessageRead onGroupMessageRead,
                                       FUNC_OnConversationsUpdate onConversationsUpdate,
                                       FUNC_OnConversationRead onConversationRead,
                                       FUNC_MessageReactionDidChange messageReactionDidChange
                                       );

HYPHENATE_API void ChatManager_RemoveConversation(void *client, const char * conversationId, bool isRemoveMessagesbool, bool isThread = false);
HYPHENATE_API void ChatManager_DownloadMessageAttachments(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress);
HYPHENATE_API void ChatManager_DownloadMessageThumbnail(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress);
HYPHENATE_API bool ChatManager_ConversationWithType(void *client, const char * conversationId, EMConversation::EMConversationType type, bool createIfNotExist = true, bool isThread = false);
HYPHENATE_API int  ChatManager_GetUnreadMessageCount(void *client);
HYPHENATE_API bool ChatManager_InsertMessages(void *client, void * messageList[], EMMessageBody::EMMessageBodyType typeList[], int size);
HYPHENATE_API void ChatManager_LoadAllConversationsFromDB(void *client, FUNC_OnSuccess_With_Result onSuccess);
HYPHENATE_API void ChatManager_GetMessage(void *client, const char * messageId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API bool ChatManager_MarkAllConversationsAsRead(void *client);
HYPHENATE_API void ChatManager_RecallMessage(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress);
HYPHENATE_API void ChatManager_ResendMessage(void *client, int callbackId, const char * messageId, FUNC_OnSuccess_With_Result onSuccessResult, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_LoadMoreMessages(void *client, FUNC_OnSuccess_With_Result onSuccess, const char * keywords, int64_t timestamp, int maxcount, const char * from, EMConversation::EMMessageSearchDirection direction);
HYPHENATE_API void ChatManager_SendReadAckForConversation(void *client, int callbackId, const char * conversationId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_SendReadAckForMessage(void *client,int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_SendReadAckForGroupMessage(void *client,int callbackId, const char * messageId, const char* ackContent, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API bool ChatManager_UpdateMessage(void *client, void *mto, EMMessageBody::EMMessageBodyType type);
HYPHENATE_API void ChatManager_RemoveMessagesBeforeTimestamp(void *client, int callbackId, int64_t timeStamp, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_DeleteConversationFromServer(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType conversationType, bool isDeleteServerMessages, FUNC_OnSuccess onSuccess, FUNC_OnError onError);

HYPHENATE_API int  ChatManager_GetGroupAckCount(const char* messageId);
HYPHENATE_API bool ChatManager_GetHasDeliverAck(const char* messageId);
HYPHENATE_API bool ChatManager_GetHasReadAck(const char* messageId);
HYPHENATE_API void ChatManager_GetReactionListForMsg(const char* messageId, char* buf, int len);
HYPHENATE_API void ChatManager_GetChatThreadForMsg(const char* messageId, char* buf, int len);

HYPHENATE_API void ChatManager_FetchSupportLanguages(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccessResult, FUNC_OnError onError);
HYPHENATE_API void ChatManager_TranslateMessage(void *client, int callbackId, void *mto, EMMessageBody::EMMessageBodyType type, const char * targetLanguages[], int size, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_FetchGroupReadAcks(void* client, int callbackId, const char* messageId, const char* groupId, int pageSize, const char* startAckId, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_ReportMessage(void* client, int callbackId, const char* messageId, const char* tag, const char* reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError);

HYPHENATE_API void ChatManager_AddReaction(void* client, int callbackId, const char* messageId, const char* reaction, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_RemoveReaction(void* client, int callbackId, const char* messageId, const char* reaction, FUNC_OnSuccess onSuccess, FUNC_OnError onError);
HYPHENATE_API void ChatManager_GetReactionList(void* client, int callbackId, const char* messageIdList, const char* messageType, const char* groupId, FUNC_OnSuccess_With_Result onSuccessResult, FUNC_OnError onError);
HYPHENATE_API void ChatManager_GetReactionDetail(void* client, int callbackId, const char* messageId, const char* reaction, const char* cursor, uint64_t pageSize, FUNC_OnSuccess_With_Result_V2 onSuccessResult, FUNC_OnError onError);
#ifdef __cplusplus
}
#endif //__cplusplus

void ChatManager_RemoveListener(void*client);

#endif //_CHAT_MANAGER_H_

