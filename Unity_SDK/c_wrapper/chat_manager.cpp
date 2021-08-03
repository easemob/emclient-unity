//
//  chat_manager.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/6/28.
//  Copyright © 2021 easemob. All rights reserved.
//
#include "chat_manager.h"

#include "emclient.h"
#include "emmessagebody.h"

static EMCallbackObserverHandle gCallbackObserverHandle;

AGORA_API void ChatManager_SendMessage(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, void *mto, EMMessageBody::EMMessageBodyType type) {
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Message sent succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Message sent failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().sendMessage(messagePtr);
}

EMChatManagerListener *gChatManagerListener = NULL;

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
                                       )
{
    if(gChatManagerListener == NULL) { //only set once!
        gChatManagerListener = new ChatManagerListener(onMessagesReceived, onCmdMessagesReceived, onMessagesRead, onMessagesDelivered, onMessagesRecalled, onReadAckForGroupMessageUpdated, onGroupMessageRead, onConversationsUpdate, onConversationRead);
        CLIENT->getChatManager().addListener(gChatManagerListener);
    }
}

AGORA_API void ChatManager_FetchHistoryMessages(void *client, const char * conversationId, EMConversation::EMConversationType type, const char * startMessageId, int count, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(conversationId, type, error, count, startMessageId);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            auto cursorResultTo = new CursorResultTO();
            CursorResultTO *data[1] = {cursorResultTo};
            data[0]->NextPageCursor = msgCursorResult.nextPageCursor().c_str();
            size_t size = msgCursorResult.result().size();
            data[0]->Type = DataType::ListOfMessage;
            data[0]->Size = (int)size;
            //copy EMMessagePtr -> MessageTO
            for(int i=0; i<size; i++) {
                MessageTO *mto = MessageTO::FromEMMessage(msgCursorResult.result().at(i));
                data[0]->Data[i] = mto;
                data[0]->SubTypes[i] = mto->BodyType;
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

AGORA_API void ChatManager_GetConversationsFromServer(void *client, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
    if (error.mErrorCode == EMError::EM_NO_ERROR) {
        if (onSuccess) {
            size_t size = conversationList.size();
            auto conversationTOArray = new TOArray();
            TOArray *data[1] = {conversationTOArray};
            data[0]->Type = DataType::ListOfConversation;
            data[0]->Size = (int)size;
            for(size_t i=0; i<size; i++) {
                ConversationTO* conversationTO = new ConversationTO();
                conversationTO->ConverationId = conversationList.at(i)->conversationId().c_str();
                conversationTO->type = conversationList.at(i)->conversationType();
                conversationTO->ExtField = conversationList.at(i)->extField().c_str();
                LOG("GetConversation %d, id=%s, type=%d, extfiled=%s",
                    i, conversationTO->ConverationId, conversationTO->type, conversationTO->ExtField);
                data[0]->Data[i] = conversationTO;
            }
            onSuccess((void**)data, DataType::ListOfConversation, 1);
            //NOTE: NO need to release mem. after onSuccess call, managed side would free them.
        }
    }else{
        if (onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void ChatManager_RemoveConversation(void *client, const char * conversationId, bool isRemoveMessages)
{
    CLIENT->getChatManager().removeConversation(conversationId, isRemoveMessages);
}

AGORA_API void ChatManager_DownloadMessageAttachments(void *client, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Download message attachment succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download message attachment failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().downloadMessageAttachments(messagePtr);
}

AGORA_API void ChatManager_DownloadMessageThumbnail(void *client, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Download message thumbnail succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download message thumbnail failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().downloadMessageThumbnail(messagePtr);
}

AGORA_API int ChatManager_ConversationWithType(void *client, const char * conversationId, EMConversation::EMConversationType type, bool createIfNotExist)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, type, createIfNotExist);
    //verify sharedptr
    if(conversationPtr) {
        return 1; //Conversation exist
    } else {
        return 0; //Conversation not exist
    }
}

AGORA_API int ChatManager_GetUnreadMessageCount(void *client)
{
    EMError error;
    int count = 0;
    //get conversations
    EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
    if (error.mErrorCode == EMError::EM_NO_ERROR) {
        //sum all unread messages in all conversations
        for(size_t i=0; i<conversationList.size(); i++) {
            count += conversationList[i]->unreadMessagesCount();
        }
    }
    return count;
}

AGORA_API int ChatManager_InsertMessages(void *client, void * messageList[], EMMessageBody::EMMessageBodyType typeList[], int size)
{
    EMMessageList list;
    //convert TO to EMMessagePtr
    for(int i=0; i<size; i++) {
        EMMessagePtr messagePtr = BuildEMMessage(messageList[i], typeList[i]);
        list.push_back(messagePtr);
    }
    if(list.size() > 0) {
        if(CLIENT->getChatManager().insertMessages(list))
            return 1;
        else;
        return 0;
    }
    return 0;
}

AGORA_API void ChatManager_LoadAllConversationsFromDB(void *client, void * conversationArray[], int size)
{
    EMConversationList conversationList = CLIENT->getChatManager().loadAllConversationsFromDB();
    if(conversationList.size() == 0 || conversationArray == NULL || size != 1)
        return;
    //save return list to array
    TOArray* toArray = (TOArray*)conversationArray;
    for(size_t i=0; i<conversationList.size(); i++) {
        ConversationTO* conversationTO = new ConversationTO;
        conversationTO->ConverationId = conversationList[i]->conversationId().c_str();
        conversationTO->type = conversationList[i]->conversationType();
        conversationTO->ExtField = conversationList[i]->extField().c_str();
        toArray->Data[i] = conversationTO;
        toArray->Size++;
        toArray->Type = DataType::ListOfConversation;
    }
    return;
}

AGORA_API void ChatManager_GetMessage(void *client, const char * messageId, void * messageArray[], int size)
{
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    if(messagePtr == NULL || messageArray == NULL | size != 1)
        return;
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray;
    MessageTO* mto = MessageTO::FromEMMessage(messagePtr);
    toArray->Data[0] = mto;
    toArray->Type[0] = (int)messagePtr->bodies()[0]->type();
    toArray->Size++;
    return;
}

AGORA_API int ChatManager_MarkAllConversationsAsRead(void *client)
{
    EMError error;
    int ret = 1; // default is true
    EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
    if(conversationList.size() == 0)
        return 1; //true
    else
    {
        for(size_t i=0; i<conversationList.size(); i++) {
            if(!conversationList[i]->markAllMessagesAsRead())
                ret = 0; // false
        }
    }
    return ret;
}

AGORA_API void ChatManager_RecallMessage(void *client, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Recall message succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Recall message failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().recallMessage(messagePtr, error);
}

AGORA_API void ChatManager_ResendMessage(void *client, const char * messageId, void * messageArray[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
        return;
    }
    
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [onSuccess]()->bool {
                                                LOG("Resend message succeeds.");
                                                if(onSuccess) onSuccess();
                                                return true;
                                             },
                                             [onError](const easemob::EMErrorPtr error)->bool{
                                                LOG("Resend message failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str());
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().resendMessage(messagePtr);
    
    //return message
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray;
    MessageTO* mto = MessageTO::FromEMMessage(messagePtr);
    toArray->Data[0] = mto;
    toArray->Type[0] = (DataType)messagePtr->bodies()[0]->type();
    toArray->Size++;
    return;
}

AGORA_API void ChatManager_LoadMoreMessages(void *client, void * messageArray[], int size, const char * keywords, long timestamp, int maxcount, const char * from, EMConversation::EMMessageSearchDirection direction)
{
    EMMessageList messageList = CLIENT->getChatManager().loadMoreMessages(timestamp, keywords, maxcount, from, direction);
    if(messageList.size() == 0)
        return;
    
    //save return list to array
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray;
    for(size_t i=0; i<messageList.size(); i++) {
        MessageTO* mto = MessageTO::FromEMMessage(messageList[i]);
        toArray->Data[i] = mto;
        toArray->Type[i] = (int)messageList[i]->bodies()[0]->type();
        toArray->Size++;
    }
    return;
}

AGORA_API void ChatManager_SendReadAckForConversation(void *client, const char * conversationId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    CLIENT->getChatManager().sendReadAckForConversation(conversationId, error);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        LOG("Send read ack for conversation:%s successfully.", conversationId);
        if(onSuccess) onSuccess();
    } else {
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
    }
}

AGORA_API void ChatManager_SendReadAckForMessage(void *client, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
        return;
    }
    
    CLIENT->getChatManager().sendReadAckForMessage(messagePtr);
    LOG("Send read ack for message:%s successfully.", messageId);
    if(onSuccess) onSuccess();
}

AGORA_API void ChatManager_UpdateMessage(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, void *mto, EMMessageBody::EMMessageBodyType type)
{
    
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    //only look for conversation, not create one if cannot find.
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(messagePtr->conversationId(), EMConversation::EMConversationType::CHAT, false);
    EMError error;
    if(nullptr == conversationPtr) {
        
        LOG("Cannot find conversation with message id:%s", messagePtr->msgId().c_str());
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
        return;
    }
    
    if(conversationPtr->updateMessage(messagePtr))
    {
        if (onSuccess) onSuccess();
    }
    else
    {
        error.mErrorCode = EMError::DATABASE_ERROR;
        error.mDescription = "Database operation failed";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str());
    }
}
