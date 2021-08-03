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


AGORA_API bool ConversationManager_AppendMessage(void *client, const char * conversationId, EMConversation::EMConversationType converationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, converationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->appendMessage(messagePtr);
}

AGORA_API bool ConversationManager_ClearAllMessages(void *client, const char * conversationId, EMConversation::EMConversationType converationType)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, converationType, true);
    return conversationPtr->clearAllMessages();
}

AGORA_API bool ConversationManager_RemoveMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    return conversationPtr->removeMessage(messageId);
}

AGORA_API void ConversationManager_ExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    std::string str = conversationPtr->extField();
    
    TOArray* toArray = (TOArray*)ext;
    toArray->Type = DataType::ListOfString;
    
    if(str.size() > 0)
    {
        toArray->Size = 1;
        char * p = new char[str.size()+1];
        strncpy(p, str.c_str(), str.size()+1);
        toArray->Data[0] = (void*)p; //to-do: will be freed at managed side??
    }
    else
    {
        toArray->Size = 0;
    }
}

AGORA_API bool ConversationManager_InsertMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->insertMessage(messagePtr);
}

AGORA_API void ConversationManager_LatestMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr msgPtr = conversationPtr->latestMessage();
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    
    //to-do: if msgPtr is empty??
    MessageTO* mto = MessageTO::FromEMMessage(msgPtr);
    toArrayDiff->Size = 1;
    toArrayDiff->Data[0] = (void*)mto;
    toArrayDiff->Type[0] = (int)msgPtr->bodies()[0]->type();
}

AGORA_API void ConversationManager_LatestMessageFromOthers(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr msgPtr = conversationPtr->latestMessageFromOthers();
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    
    //to-do: if msgPtr is empty??
    MessageTO* mto = MessageTO::FromEMMessage(msgPtr);
    toArrayDiff->Size = 1;
    toArrayDiff->Data[0] = (void*)mto;
    toArrayDiff->Type[0] = (int)msgPtr->bodies()[0]->type();
}


AGORA_API void ConversationManager_LoadMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr msgPtr = conversationPtr->loadMessage(messageId);
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    
    //to-do: if msgPtr is empty??
    MessageTO* mto = MessageTO::FromEMMessage(msgPtr);
    toArrayDiff->Size = 1;
    toArrayDiff->Data[0] = (void*)mto;
    toArrayDiff->Type[0] = (int)msgPtr->bodies()[0]->type();
}

AGORA_API void ConversationManager_LoadMessages(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * startMessageId, int count, EMConversation::EMMessageSearchDirection direction, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(startMessageId, count, direction);
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    toArrayDiff->Size = 0;
    
    //to-do: if msgPtr is empty??
    for(size_t i=0; i<msgList.size(); i++)
    {
        MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgList[i]->bodies()[0]->type();
        toArrayDiff->Size++;
    }
}

AGORA_API void ConversationManager_LoadMessagesWithKeyword(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * keywords, const char * sender, long timestamp, int count, EMConversation::EMMessageSearchDirection direction, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(keywords, timestamp, count, sender, direction);
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    toArrayDiff->Size = 0;
    
    //to-do: if msgPtr is empty??
    for(size_t i=0; i<msgList.size(); i++)
    {
        MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgList[i]->bodies()[0]->type();
        toArrayDiff->Size++;
    }
}

AGORA_API void ConversationManager_LoadMessagesWithMsgType(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, EMMessageBody::EMMessageBodyType type, long timestamp, int count, const char * sender, EMConversation::EMMessageSearchDirection direction, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(type, timestamp, count, sender, direction);
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    toArrayDiff->Size = 0;
    
    //to-do: if msgPtr is empty??
    for(size_t i=0; i<msgList.size(); i++)
    {
        MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgList[i]->bodies()[0]->type();
        toArrayDiff->Size++;
    }
}

AGORA_API void ConversationManager_LoadMessagesWithTime(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, long startTimeStamp, long endTimeStamp, int count, void * ext[], int size)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessageList msgList = conversationPtr->loadMoreMessages(startTimeStamp, endTimeStamp, count);
    
    TOArrayDiff* toArrayDiff = (TOArrayDiff*)ext;
    toArrayDiff->Size = 0;
    
    //to-do: if msgPtr is empty??
    for(size_t i=0; i<msgList.size(); i++)
    {
        MessageTO* mto = MessageTO::FromEMMessage(msgList[i]);
        toArrayDiff->Data[0] = (void*)mto;
        toArrayDiff->Type[0] = (int)msgList[i]->bodies()[0]->type();
        toArrayDiff->Size++;
    }
}

AGORA_API void ConversationManager_MarkAllMessagesAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->markAllMessagesAsRead(true);
}

AGORA_API void ConversationManager_MarkMessageAsRead(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * messageId)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->markMessageAsRead(messageId,true);
}

AGORA_API void ConversationManager_SetExtField(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, const char * ext)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    conversationPtr->setExtField(ext);
}

AGORA_API int ConversationManager_UnreadMessagesCount(void *client, const char * conversationId, EMConversation::EMConversationType conversationType)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    return conversationPtr->unreadMessagesCount();
}

AGORA_API bool ConversationManager_UpdateMessage(void *client, const char * conversationId, EMConversation::EMConversationType conversationType, void *mto, EMMessageBody::EMMessageBodyType type)
{
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, conversationType, true);
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    return conversationPtr->updateMessage(messagePtr);
}
