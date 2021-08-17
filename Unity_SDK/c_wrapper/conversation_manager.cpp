//
//  contact_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2021/8/2.
//  Copyright © 2021 easemob. All rights reserved.
//
#include "conversation_manager.h"

#include "emclient.h"
#include "emmessagebody.h"
#include "tool.h"

AGORA_API bool ConversationManager_AppendMessage(void *client, const char * conversationId, EMConversation::EMConversationType converationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return false;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, converationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->appendMessage(messagePtr);
}

AGORA_API bool ConversationManager_ClearAllMessages(void *client, const char * conversationId, EMConversation::EMConversationType converationType)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return false;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, converationType, true);
    return conversationPtr->clearAllMessages();
}

AGORA_API bool ConversationManager_RemoveMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return false;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    return conversationPtr->removeMessage(messageId);
}

AGORA_API void ConversationManager_ExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size)
{
    TOArray* toArray = (TOArray*)ext[0];
    
    if(nullptr == conversationId) {
        toArray->Size = 0;
        LOG("Mandatory parameter is null!.");
        return;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    std::string str = conversationPtr->extField();
    
    
    toArray->Type = DataType::ListOfString;
    
    if(str.size() > 0)
    {
        toArray->Size = 1;
        char * p = new char[str.size()+1];
        strncpy(p, str.c_str(), str.size()+1);
        toArray->Data[0] = (void*)p;
    }
    else
    {
        toArray->Size = 0;
    }
}

AGORA_API bool ConversationManager_InsertMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return false;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->insertMessage(messagePtr);
}

AGORA_API void ConversationManager_LatestMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size)
{
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext[0];
    
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        toArrayDiff->Size = 0;
        return;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    if(!conversationPtr) {
        LOG("Cannot get conversation with id:%s", conversationId);
        toArrayDiff->Size = 0;
    }
    EMMessagePtr msgPtr = conversationPtr->latestMessage();
    
    if(msgPtr) {
        LOG("Found last message for conversation id:%s, message id:%s", conversationId, msgPtr->msgId().c_str());
        MessageTO* mto = MessageTO::FromEMMessage(msgPtr);
        toArrayDiff->Size = 1;
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgPtr->bodies()[0]->type();
    } else {
        LOG("NOT find any last message for conversation id:%s", conversationId);
        toArrayDiff->Size = 0;
    }
}

AGORA_API void ConversationManager_LatestMessageFromOthers(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size)
{
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext[0];
    
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        toArrayDiff->Size = 0;
        return;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr msgPtr = conversationPtr->latestMessageFromOthers();
    
    if(msgPtr) {
        MessageTO* mto = MessageTO::FromEMMessage(msgPtr);
        toArrayDiff->Size = 1;
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgPtr->bodies()[0]->type();
    } else {
        toArrayDiff->Size = 0;
    }
}


AGORA_API void ConversationManager_LoadMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId, void * ext[], int size)
{
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext[0];
    
    if(nullptr == conversationId || nullptr == messageId) {
        LOG("Mandatory parameter is null!.");
        toArrayDiff->Size = 0;
        return;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr msgPtr = conversationPtr->loadMessage(messageId);
    
    if(msgPtr) {
        MessageTO* mto = MessageTO::FromEMMessage(msgPtr);
        toArrayDiff->Size = 1;
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgPtr->bodies()[0]->type();
    } else {
        toArrayDiff->Size = 0;
    }
}

AGORA_API void ConversationManager_LoadMessages(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * startMessageId, int count, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string startMessageIdStr = OptionalStrParamCheck(startMessageId);
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(startMessageIdStr, count, direction);
    
    TOArrayDiff toArrayDiff;
    toArrayDiff.Size = 0;
    TOArrayDiff *data[1] = {&toArrayDiff};
    
    if(msgList.size() != 0) {
        LOG("Found messages %d.", msgList.size());
        for(size_t i=0; i<msgList.size(); i++)
        {
            MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
            toArrayDiff.Data[i] = (void*)mto;
            toArrayDiff.Type[i] = (int)msgList[i]->bodies()[0]->type();
            toArrayDiff.Size++;
        }
        onSuccess((void **)data, DataType::ListOfString, 1);
        for(size_t i=0; i<msgList.size(); i++) {
            delete (MessageTO*)toArrayDiff.Data[i];
        }
    } else {
        toArrayDiff.Size = 0;
        LOG("Not find any messages.");
        onSuccess((void **)data, DataType::ListOfString, 1);
    }
}

AGORA_API void ConversationManager_LoadMessagesWithKeyword(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * keywords, const char * sender, long timestamp, int count, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string keywordsStr = OptionalStrParamCheck(keywords);
    std::string senderStr = OptionalStrParamCheck(sender);
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(keywords, timestamp, count, sender, direction);
    
    TOArrayDiff toArrayDiff;
    toArrayDiff.Size = 0;
    TOArrayDiff *data[1] = {&toArrayDiff};
    
    if(msgList.size() != 0) {
        LOG("Found messages %d.", msgList.size());
        for(size_t i=0; i<msgList.size(); i++)
        {
            MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
            toArrayDiff.Data[i] = (void*)mto;
            toArrayDiff.Type[i] = (int)msgList[i]->bodies()[0]->type();
            toArrayDiff.Size++;
        }
        onSuccess((void **)data, DataType::ListOfString, 1);
        for(size_t i=0; i<msgList.size(); i++) {
            delete (MessageTO*)toArrayDiff.Data[i];
        }
    } else {
        toArrayDiff.Size = 0;
        LOG("Not find any messages.");
        onSuccess((void **)data, DataType::ListOfString, 1);
    }
}

AGORA_API void ConversationManager_LoadMessagesWithMsgType(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, EMMessageBody::EMMessageBodyType type, long timestamp, int count, const char * sender, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    std::string senderStr = OptionalStrParamCheck(sender);
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(type, timestamp, count, sender, direction);
    
    TOArrayDiff toArrayDiff;
    toArrayDiff.Size = 0;
    TOArrayDiff *data[1] = {&toArrayDiff};
    
    if(msgList.size() != 0) {
        LOG("Found messages %d.", msgList.size());
        for(size_t i=0; i<msgList.size(); i++)
        {
            MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
            toArrayDiff.Data[i] = (void*)mto;
            toArrayDiff.Type[i] = (int)msgList[i]->bodies()[0]->type();
            toArrayDiff.Size++;
        }
        onSuccess((void **)data, DataType::ListOfString, 1);
        for(size_t i=0; i<msgList.size(); i++) {
            delete (MessageTO*)toArrayDiff.Data[i];
        }
    } else {
        toArrayDiff.Size = 0;
        LOG("Not find any messages.");
        onSuccess((void **)data, DataType::ListOfString, 1);
    }
}

AGORA_API void ConversationManager_LoadMessagesWithTime(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, long startTimeStamp, long endTimeStamp, int count, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str());
        return;
    }
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(startTimeStamp, endTimeStamp, count);
    
    TOArrayDiff toArrayDiff;
    toArrayDiff.Size = 0;
    TOArrayDiff *data[1] = {&toArrayDiff};
    
    if(msgList.size() != 0) {
        LOG("Found messages %d.", msgList.size());
        for(size_t i=0; i<msgList.size(); i++)
        {
            MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
            toArrayDiff.Data[i] = (void*)mto;
            toArrayDiff.Type[i] = (int)msgList[i]->bodies()[0]->type();
            toArrayDiff.Size++;
        }
        onSuccess((void **)data, DataType::ListOfString, 1);
        for(size_t i=0; i<msgList.size(); i++) {
            delete (MessageTO*)toArrayDiff.Data[i];
        }
    } else {
        toArrayDiff.Size = 0;
        LOG("Not find any messages.");
        onSuccess((void **)data, DataType::ListOfString, 1);
    }
}

AGORA_API void ConversationManager_MarkAllMessagesAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->markAllMessagesAsRead(true);
}

AGORA_API void ConversationManager_MarkMessageAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId)
{
    if(nullptr == conversationId || nullptr == messageId) {
        LOG("Mandatory parameter is null!.");
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->markMessageAsRead(messageId,true);
}

AGORA_API void ConversationManager_SetExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * ext)
{
    if(nullptr == conversationId || nullptr == ext) {
        LOG("Mandatory parameter is null!.");
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->setExtField(ext);
}

AGORA_API int ConversationManager_UnreadMessagesCount(void *client, const char * conversationId, EMConversation::EMConversationType conversationType)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return 0;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    return conversationPtr->unreadMessagesCount();
}

AGORA_API bool ConversationManager_UpdateMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(nullptr == conversationId) {
        LOG("Mandatory parameter is null!.");
        return false;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->updateMessage(messagePtr);
}

AGORA_API void ConversationManager_ReleaseStringList(void * stringArray[], int size)
{
    if(size != 1) return;
    TOArray* toArray = (TOArray*)stringArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (char*)toArray->Data[i];
        }
    }
}

AGORA_API void ConversationManager_ReleaseMessageList(void * msgArray[], int size)
{
    if(size != 1) return;
    TOArrayDiff* toArray = (TOArrayDiff*)msgArray[0];
    
    if(toArray->Size > 0) {
        for(int i=0; i<toArray->Size; i++) {
            delete (MessageTO*)toArray->Data[i];
        }
    }
}
