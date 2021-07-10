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
AGORA_API void ChatManager_SendMessage(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, void *mto, EMMessageBody::EMMessageBodyType type);
AGORA_API void ChatManager_FetchHistoryMessages(void *client, const char * conversationId, EMConversation::EMConversationType type, const char * startMessageId, int count, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError);
AGORA_API void ChatManager_AddListener(void *client,
                                       FUNC_OnMessagesReceived onMessagesReceived,
                                       FUNC_OnCmdMessagesReceived onCmdMessagesReceived,
                                       FUNC_OnMessagesRead onMessagesRead,
                                       FUNC_OnMessagesDelivered onMessagesDelivered,
                                       FUNC_OnMessagesRecalled onMessagesRecalled,
                                       FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated,
                                       FUNC_OnGroupMessageRead onGroupMessageRead,
                                       FUNC_OnConversationsUpdate onConversationsUpdate,
                                       FUNC_OnConversationRead onConversationRead
                                       );
#ifdef __cplusplus
}
#endif //__cplusplus

#endif //_CHAT_MANAGER_H_

