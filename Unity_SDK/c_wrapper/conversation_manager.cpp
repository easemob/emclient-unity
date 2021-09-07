//
//  contact_manager.cpp
//  hyphenateCWrapper
//
//  Created by Qiang Yu on 2021/8/2.
//  Copyright © 2021 easemob. All rights reserved.
//
#include <thread>
#include "conversation_manager.h"
#include "emclient.h"
#include "emmessagebody.h"
#include "tool.h"

AGORA_API bool ConversationManager_AppendMessage(void *client, const char * conversationId, EMConversation::EMConversationType converationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(!MandatoryCheck(conversationId))
       return false;
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, converationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->appendMessage(messagePtr);
}

AGORA_API bool ConversationManager_ClearAllMessages(void *client, const char * conversationId, EMConversation::EMConversationType converationType)
{
    if(!MandatoryCheck(conversationId))
       return false;
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, converationType, true);
    return conversationPtr->clearAllMessages();
}

AGORA_API bool ConversationManager_RemoveMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId)
{
    if(!MandatoryCheck(conversationId))
       return false;
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    return conversationPtr->removeMessage(messageId);
}

AGORA_API void ConversationManager_ExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, FUNC_OnSuccess_With_Result onSuccess)
{
    if(!MandatoryCheck(conversationId)) {
        onSuccess(nullptr, DataType::String, 0, -1);
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    std::string str = conversationPtr->extField();
    
    if(str.size() > 0) {
        LOG("Ext is %s for conversation %s", str.c_str(), conversationId);
        const char *data[1];
        data[0] = str.c_str();
        onSuccess((void **)data, DataType::String, 1, -1);
    } else {
        LOG("Ext is empty for conversation %s", str.c_str(), conversationId);
        onSuccess(nullptr, DataType::String, 0, -1);
    }
}

AGORA_API bool ConversationManager_InsertMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(!MandatoryCheck(conversationId))
       return false;
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->insertMessage(messagePtr);
}

AGORA_API void ConversationManager_LatestMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, FUNC_OnSuccess_With_Result onSuccess)
{
    if(!MandatoryCheck(conversationId)) {
        onSuccess(nullptr, DataType::ListOfMessage, -1, -1);
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    if(!conversationPtr) {
        LOG("Cannot get conversation with id:%s", conversationId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
        return;
    }
    EMMessagePtr msgPtr = conversationPtr->latestMessage();
    if(msgPtr) {
        TOItem* data[1];
        LOG("Found last message for conversation id:%s, message id:%s", conversationId, msgPtr->msgId().c_str());
        TOItem* item = new TOItem((int)msgPtr->bodies()[0]->type(), MessageTO::FromEMMessage(msgPtr));
        data[0] = item;
        onSuccess((void **)data, DataType::ListOfMessage, 1, -1);
        MessageTO::FreeResource((MessageTO*)item->Data);
        delete (MessageTO*)item->Data;
        delete item;
    } else {
        LOG("NOT find any last message for conversation id:%s", conversationId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
    }
}

AGORA_API void ConversationManager_LatestMessageFromOthers(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, FUNC_OnSuccess_With_Result onSuccess)
{
    if(!MandatoryCheck(conversationId)) {
        onSuccess(nullptr, DataType::ListOfMessage, -1, -1);
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    if(!conversationPtr) {
        LOG("Cannot get conversation with id:%s", conversationId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
        return;
    }
    EMMessagePtr msgPtr = conversationPtr->latestMessageFromOthers();
    if(msgPtr) {
        TOItem* data[1];
        LOG("Found last message for conversation id:%s, message id:%s", conversationId, msgPtr->msgId().c_str());
        TOItem* item = new TOItem((int)msgPtr->bodies()[0]->type(), MessageTO::FromEMMessage(msgPtr));
        data[0] = item;
        onSuccess((void **)data, DataType::ListOfMessage, 1, -1);
        MessageTO::FreeResource((MessageTO*)item->Data);
        delete (MessageTO*)item->Data;
        delete item;
    } else {
        LOG("NOT find any last message for conversation id:%s", conversationId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
    }
}

AGORA_API void ConversationManager_LoadMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId, FUNC_OnSuccess_With_Result onSuccess)
{
    if(!MandatoryCheck(conversationId, messageId)) {
        onSuccess(nullptr, DataType::ListOfMessage, -1, -1);
        return;
    }
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    if(!conversationPtr) {
        LOG("Cannot get conversation with id:%s", conversationId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
        return;
    }
    EMMessagePtr msgPtr = conversationPtr->loadMessage(messageId);
    if(msgPtr) {
        TOItem* data[1];
        LOG("Loaded the message for conversation id:%s, message id:%s", conversationId, msgPtr->msgId().c_str());
        TOItem* item = new TOItem((int)msgPtr->bodies()[0]->type(), MessageTO::FromEMMessage(msgPtr));
        data[0] = item;
        onSuccess((void **)data, DataType::ListOfMessage, 1, -1);
        MessageTO::FreeResource((MessageTO*)item->Data);
        delete (MessageTO*)item->Data;
        delete item;
    } else {
        LOG("Failed to load the message %s for conversation id:%s", messageId, conversationId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
    }
}

AGORA_API void ConversationManager_LoadMessages(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType conversationType, const char * startMessageId, int count, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string conversationIdStr = conversationId;
    std::string startMessageIdStr = OptionalStrParamCheck(startMessageId);
    
    std::thread t([=](){
        EMError error;
        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationIdStr, conversationType, true);
        EMMessageList msgList = conversationPtr->loadMoreMessages(startMessageIdStr, count, direction);
        
        int size = (int)msgList.size();
        if(size > 0) {
            LOG("Load messages %d.", size);
            TOItem* data[size];
            for(int i=0; i<size; i++) {
                MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
                TOItem* item = new TOItem((int)msgList[i]->bodies()[0]->type(), mto);
                LOG("message %d, msgid %s", i, mto->MsgId);
                data[i] = item;
            }
            onSuccess((void **)data, DataType::ListOfMessage, size, callbackId);
            for(int i=0; i<size; i++) {
                MessageTO::FreeResource((MessageTO*)data[i]->Data);
                delete (MessageTO*)data[i]->Data;
                delete data[i];
            }
        } else {
            LOG("Cannot load any messages in LoadMessages.");
            onSuccess(nullptr, DataType::ListOfMessage, 0, callbackId);
        }
    });
    t.detach();
}

AGORA_API void ConversationManager_LoadMessagesWithKeyword(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType conversationType, const char * keywords, const char * sender, long timestamp, int count, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, keywords, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string conversationIdStr = conversationId;
    std::string keywordsStr = keywords;
    std::string senderStr = OptionalStrParamCheck(sender);
    
    std::thread t([=](){
        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationIdStr, conversationType, true);
        EMMessageList msgList = conversationPtr->loadMoreMessages(keywordsStr, timestamp, count, senderStr, direction);
        
        int size = (int)msgList.size();
        if(size > 0) {
            LOG("Load messages %d.", size);
            TOItem* data[size];
            for(int i=0; i<size; i++) {
                MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
                TOItem* item = new TOItem((int)msgList[i]->bodies()[0]->type(), mto);
                LOG("message %d, msgid %s", i, mto->MsgId);
                data[i] = item;
            }
            onSuccess((void **)data, DataType::ListOfMessage, size, callbackId);
            for(int i=0; i<size; i++) {
                MessageTO::FreeResource((MessageTO*)data[i]->Data);
                delete (MessageTO*)data[i]->Data;
                delete data[i];
            }
        } else {
            LOG("Cannot load any messages in LoadMessages.");
            onSuccess(nullptr, DataType::ListOfMessage, 0, callbackId);
        }
    });
    t.detach();
}

AGORA_API void ConversationManager_LoadMessagesWithMsgType(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType conversationType, EMMessageBody::EMMessageBodyType type, long timestamp, int count, const char * sender, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string conversationIdStr = conversationId;
    std::string senderStr = OptionalStrParamCheck(sender);
    
    std::thread t([=](){
        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationIdStr, conversationType, true);
        EMMessageList msgList = conversationPtr->loadMoreMessages(type, timestamp, count, senderStr, direction);
        
        int size = (int)msgList.size();
        if(size > 0) {
            LOG("Load messages %d.", size);
            TOItem* data[size];
            for(int i=0; i<size; i++) {
                MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
                TOItem* item = new TOItem((int)msgList[i]->bodies()[0]->type(), mto);
                LOG("message %d, msgid %s", i, mto->MsgId);
                data[i] = item;
            }
            onSuccess((void **)data, DataType::ListOfMessage, size, callbackId);
            for(int i=0; i<size; i++) {
                MessageTO::FreeResource((MessageTO*)data[i]->Data);
                delete (MessageTO*)data[i]->Data;
                delete data[i];
            }
        } else {
            LOG("Cannot load any messages in LoadMessages.");
            onSuccess(nullptr, DataType::ListOfMessage, 0, callbackId);
        }
    });
    t.detach();
}

AGORA_API void ConversationManager_LoadMessagesWithTime(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType conversationType, long startTimeStamp, long endTimeStamp, int count, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string conversationIdStr = conversationId;
    
    std::thread t([=](){
        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationIdStr, conversationType, true);
        EMMessageList msgList = conversationPtr->loadMoreMessages(startTimeStamp, endTimeStamp, count);

        int size = (int)msgList.size();
        if(size > 0) {
            LOG("Load messages %d.", size);
            TOItem* data[size];
            for(int i=0; i<size; i++) {
                MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
                TOItem* item = new TOItem((int)msgList[i]->bodies()[0]->type(), mto);
                LOG("message %d, msgid %s", i, mto->MsgId);
                data[i] = item;
            }
            onSuccess((void **)data, DataType::ListOfMessage, size, callbackId);
            for(int i=0; i<size; i++) {
                MessageTO::FreeResource((MessageTO*)data[i]->Data);
                delete (MessageTO*)data[i]->Data;
                delete data[i];
            }
        } else {
            LOG("Cannot load any messages in LoadMessages.");
            onSuccess(nullptr, DataType::ListOfMessage, 0, callbackId);
        }
    });
    t.detach();
}

AGORA_API void ConversationManager_MarkAllMessagesAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType)
{
    if(!MandatoryCheck(conversationId))
        return;

    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->markAllMessagesAsRead(true);
}

AGORA_API void ConversationManager_MarkMessageAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId)
{
    if(!MandatoryCheck(conversationId, messageId))
        return;
    
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->markMessageAsRead(messageId,true);
}

AGORA_API void ConversationManager_SetExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * ext)
{
    if(!MandatoryCheck(conversationId, ext))
        return;

    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->setExtField(ext);
}

AGORA_API int ConversationManager_UnreadMessagesCount(void *client, const char * conversationId, EMConversation::EMConversationType conversationType)
{
    if(!MandatoryCheck(conversationId))
        return 0;

    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    return conversationPtr->unreadMessagesCount();
}

AGORA_API bool ConversationManager_UpdateMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(!MandatoryCheck(conversationId))
        return false;

    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->updateMessage(messagePtr);
}
