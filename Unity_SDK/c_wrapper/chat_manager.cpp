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
#include "tool.h"

static EMCallbackObserverHandle gCallbackObserverHandle;

AGORA_API void ChatManager_SendMessage(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, void *mto, EMMessageBody::EMMessageBodyType type) {
    EMError error;
    if(!MandatoryCheck(mto, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
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
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    std::string startMessageIdStr = OptionalStrParamCheck(startMessageId);
    
    EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(conversationId, type, error, count, startMessageId);
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        //success
        if(onSuccess) {
            //auto cursorResultTo = new CursorResultTO();
            CursorResultTO cursorResultTo;
            CursorResultTO *data[1] = {&cursorResultTo};
            data[0]->NextPageCursor = msgCursorResult.nextPageCursor().c_str();
            size_t size = msgCursorResult.result().size();
            data[0]->Type = DataType::ListOfMessage;
            data[0]->Size = (int)size;
            LOG("fetchHistoryMessages history message count:%d", size);
            //copy EMMessagePtr -> MessageTO
            for(int i=0; i<size; i++) {
                MessageTO *mto = MessageTO::FromEMMessage(msgCursorResult.result().at(i));
                data[0]->Data[i] = mto;
                data[0]->SubTypes[i] = mto->BodyType;
            }
            onSuccess((void **)data, DataType::CursorResult, 1);
            
            //free memory
            for(int i=0; i<size; i++) {
                delete (MessageTO*)data[0]->Data[i];
            }
        }
    }else{
        //error
        if(onError) {
            LOG("fetchHistoryMessages history message failed, error id:%d, desc::%s", error.mErrorCode, error.mDescription.c_str());
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
            //auto conversationTOArray = new TOArray();
            TOArray conversationTOArray;
            TOArray *data[1] = {&conversationTOArray};
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
            //free memory
            for(size_t i=0; i<size; i++) {
                delete (ConversationTO*)data[0]->Data[i];
            }
        }
    }else{
        if (onError) {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void ChatManager_RemoveConversation(void *client, const char * conversationId, bool isRemoveMessages)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!");
        return;
    }
    CLIENT->getChatManager().removeConversation(conversationId, isRemoveMessages);
}

AGORA_API void ChatManager_DownloadMessageAttachments(void *client, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
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
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
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

AGORA_API bool ChatManager_ConversationWithType(void *client, const char * conversationId, EMConversation::EMConversationType type, bool createIfNotExist)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!");
        return false;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, type, createIfNotExist);
    //verify sharedptr
    if(conversationPtr) {
        return true; //Conversation exist
    } else {
        return false; //Conversation not exist
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

AGORA_API bool ChatManager_InsertMessages(void *client, void * messageList[], EMMessageBody::EMMessageBodyType typeList[], int size)
{
    EMMessageList list;
    //convert TO to EMMessagePtr
    for(int i=0; i<size; i++) {
        EMMessagePtr messagePtr = BuildEMMessage(messageList[i], typeList[i]);
        list.push_back(messagePtr);
    }
    if(list.size() > 0) {
        return CLIENT->getChatManager().insertMessages(list);
    } else {
        return true;
    }
}

AGORA_API void ChatManager_LoadAllConversationsFromDB(void *client, void * conversationArray[], int size)
{
    EMConversationList conversationList = CLIENT->getChatManager().loadAllConversationsFromDB();
    if(conversationList.size() == 0 || conversationArray == NULL || size != 1)
        return;
    //save return list to array
    TOArray* toArray = (TOArray*)conversationArray[0];
    toArray->Size = 0;
    LOG("Found conversation %d in Db", conversationList.size());
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
    if(messageArray == NULL | size != 1) {
        LOG("Parameter error for ChatManager_GetMessage");
        return;
    }
    
    if(nullptr == messageId) {
        LOG("Mandatory parameter is null!");
        return;
    }
    
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);

    if(messagePtr == NULL) {
        LOG("Cannot find the message with id %s in ChatManager_GetMessage", messageId);
        return;
    }
    
    LOG("Found the message with id %s in ChatManager_GetMessage", messageId);
    
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray[0];
    toArray->Size = 0;
    
    MessageTO* mto = MessageTO::FromEMMessage(messagePtr);
    toArray->Data[0] = mto;
    toArray->Type[0] = (int)messagePtr->bodies()[0]->type();
    toArray->Size++;
    return;
}

AGORA_API bool ChatManager_MarkAllConversationsAsRead(void *client)
{
    bool ret = true;
    EMError error;
    EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
    if(conversationList.size() == 0)
        return true;
    else
    {
        for(size_t i=0; i<conversationList.size(); i++) {
            if(!conversationList[i]->markAllMessagesAsRead())
                ret = false;
        }
    }
    return ret;
}

AGORA_API void ChatManager_RecallMessage(void *client, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
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
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        
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
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray[0];
    toArray->Size = 0;
    MessageTO* mto = MessageTO::FromEMMessage(messagePtr);
    toArray->Data[0] = mto;
    toArray->Type[0] = (DataType)messagePtr->bodies()[0]->type();
    toArray->Size++;
    return;
}

AGORA_API void ChatManager_LoadMoreMessages(void *client, void * messageArray[], int size, const char * keywords, long timestamp, int maxcount, const char * from, EMConversation::EMMessageSearchDirection direction)
{
    //save return list to array
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray[0];
    toArray->Size = 0;
    
    std::string keywordsStr = OptionalStrParamCheck(keywords);
    std::string fromStr = OptionalStrParamCheck(from);
        
    EMMessageList messageList = CLIENT->getChatManager().loadMoreMessages(timestamp, keywordsStr, maxcount, fromStr, direction);
    if(messageList.size() == 0) {
        LOG("No messages found with ts:%ld, kw:%s, from:%s, maxc:%d, direct:%d", timestamp, keywords, from, maxcount, direction);
        return;
    }
    
    LOG("Found %d messages with ts:%ld, kw:%s, from:%s, maxc:%d, direct:%d", messageList.size(), timestamp, keywords, from, maxcount, direction);
    
    toArray->Size = (int)messageList.size();
    for(size_t i=0; i<messageList.size(); i++) {
        LOG("Found message %d, id:%s", i, messageList[i]->msgId().c_str());
        MessageTO* mto = MessageTO::FromEMMessage(messageList[i]);
        toArray->Data[i] = mto;
        toArray->Type[i] = (int)messageList[i]->bodies()[0]->type();
    }
    return;
}

AGORA_API void ChatManager_SendReadAckForConversation(void *client, const char * conversationId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
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
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    
    //verify message
    if(nullptr == messagePtr) {
        
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

AGORA_API bool ChatManager_UpdateMessage(void *client, void *mto, EMMessageBody::EMMessageBodyType type)
{
    EMError error;
    if(nullptr == mto) {
        LOG("Mandatory parameter is null!");
        return false;
    }
    
    EMMessagePtr messagePtr = BuildEMMessage(mto, type, true);
    //only look for conversation, not create one if cannot find.
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(messagePtr->conversationId(), EMConversation::EMConversationType::CHAT, true);
    
    if(nullptr == conversationPtr) {
        
        LOG("Cannot find conversation with conversation id:%s", messagePtr->conversationId().c_str());
        return false;
    }
    
    return conversationPtr->updateMessage(messagePtr);
}

AGORA_API void ChatManager_ReleaseConversationList(void * conversationArray[], int size)
{
    if(size != 1) return;
    TOArray* toArray = (TOArray*)conversationArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (ConversationTO*)toArray->Data[i];
        }
    }
}

AGORA_API void ChatManager_ReleaseMessageList(void * messageArray[], int size)
{
    if(size != 1) return;
    TOArrayDiff* toArray = (TOArrayDiff*)messageArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (MessageTO*)toArray->Data[i];
        }
    }
}
