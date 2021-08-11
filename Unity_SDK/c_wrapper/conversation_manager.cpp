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

//to-do: just for testing
void* global_ptr;
AGORA_API void* DirectReturnSinglePointer()
{
    std::string fileName("Hello everbody~~");
    char* ptr = new char[fileName.length()+1];
    strncpy(ptr, fileName.c_str(), fileName.length()+1);
    global_ptr = (void*)ptr;
    return ptr;
    
    /*
    GroupSharedFileTO* toPtr = new GroupSharedFileTO();
    
    std::string fileName("FileName");
    std::string fileId("FileId");
    std::string fileOwner("FileOwner");
    
    char* ptr = new char[fileName.length()+1];
    strncpy(ptr, fileName.c_str(), fileName.length()+1);
    toPtr->FileName = ptr;
    
    LOG("address1: %x", ptr);
    
    ptr = new char[fileId.length()+1];
    strncpy(ptr, fileId.c_str(), fileId.length()+1);
    toPtr->FileId = ptr;
    
    LOG("address2: %x", ptr);
    
    ptr = new char[fileOwner.length()+1];
    strncpy(ptr, fileOwner.c_str(), fileOwner.length()+1);
    toPtr->FileOwner = ptr;
    
    LOG("address3: %x", ptr);
    
    global_ptr = (GroupSharedFileTO*)toPtr;
    
    toPtr->CreateTime = 12345;
    toPtr->FileSize = 678910;
    
    return (void*)toPtr;
     */
}

//cancel, c# cannot pick up an array just from a IntPtr!!
AGORA_API void* DirectReturnArrayPointers()
{
    GroupSharedFileTO** array = new GroupSharedFileTO* [5];
    //void* array[5];
    
    std::string fileName("FileName");
    std::string fileId("FileId");
    std::string fileOwner("FileOwner");
    char* ptr = nullptr;
    std::string tmpStr = "";
    
    for(int i=0; i<5; i++) {
        GroupSharedFileTO* toPtr = new GroupSharedFileTO();
        
        tmpStr = fileName;
        tmpStr.append(std::to_string(i));
        ptr = new char[tmpStr.length()+1];
        strncpy(ptr, tmpStr.c_str(), tmpStr.length()+1);
        toPtr->FileName = ptr;
        
        tmpStr = fileId;
        tmpStr.append(std::to_string(i));
        ptr = new char[fileId.length()+1];
        strncpy(ptr, fileId.c_str(), fileId.length()+1);
        toPtr->FileId = ptr;
        
        tmpStr = fileOwner;
        tmpStr.append(std::to_string(i));
        ptr = new char[fileOwner.length()+1];
        strncpy(ptr, fileOwner.c_str(), fileOwner.length()+1);
        toPtr->FileOwner = ptr;
        
        array[i] = toPtr;
    }
    
    return array;
}
AGORA_API void  ParamReturnPointersInStruct(void * astruct)
{
    GroupSharedFileTO* toPtr = (GroupSharedFileTO*)astruct;
    
    std::string fileName("FileNameInUnmanageSide");
    char* ptr = new char[fileName.length()+1];
    strncpy(ptr, fileName.c_str(), fileName.length()+1);
    
    toPtr->FileName = ptr;
    global_ptr = ptr;
}

AGORA_API void  ParamReturnPointersInArray(void * array[], int size)
{
    for(int i=0; i<size; i++) {
        
        GroupSharedFileTO* toPtr = (GroupSharedFileTO*)array[i];
        
        std::string fileName("FileNameInUnmanageSide");
        fileName.append(std::to_string(i));
        char* ptr = new char[fileName.length()+1];
        strncpy(ptr, fileName.c_str(), fileName.length()+1);
        
        toPtr->FileName = ptr;
        global_ptr = toPtr;
    }
}

AGORA_API void  CallbackReturnPointersinStruct(FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    error.setErrorCode(EMError::EM_NO_ERROR);
    
    if(error.mErrorCode == EMError::EM_NO_ERROR) {
        if(onSuccess) {
            GroupSharedFileTO* toPtr = new GroupSharedFileTO();
            
            std::string fileOwner("FileOwnerInUnmanageSide");
            //fileOwner.append(std::to_string(i));
            char* ptr = new char[fileOwner.length()+1];
            strncpy(ptr, fileOwner.c_str(), fileOwner.length()+1);
            toPtr->FileOwner = ptr;
            
            GroupSharedFileTO *data[1] = {toPtr};
            onSuccess((void **)data, DataType::ListOfGroupSharedFile, 1);
           // global_ptr = toPtr;
            delete toPtr;
        }
    }else{
        if(onError)
        {
            onError(error.mErrorCode, error.mDescription.c_str());
        }
    }
}

AGORA_API void  PrintAndFreeResource()
{
    //GroupSharedFileTO* toPtr = (GroupSharedFileTO*)global_ptr;
    //LOG("ptr has value: %s",global_ptr);
    //LOG("fn:%s, fi:%s, fo:%s, ct:%ld, fz:%ld", toPtr->FileName, toPtr->FileId, toPtr->FileOwner, toPtr->CreateTime, toPtr->FileSize);
    //delete (GroupSharedFileTO*)global_ptr;
    
    LOG("ptr has value: %s",global_ptr);
    delete (char*) global_ptr;
}
