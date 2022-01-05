//
//  chat_manager.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/6/28.
//  Copyright © 2021 easemob. All rights reserved.
//
#include <thread>
#include <mutex>
#include "chat_manager.h"
#include "emclient.h"
#include "emmessagebody.h"
#include "tool.h"

typedef std::map<int64_t, MessageTO*>::iterator TsMsgIter;
typedef std::map<int64_t, EMMessagePtr>::iterator TsMsgPtrIter;

std::mutex tsMsgLocker;
//MessgeTO must be freed at C# side!
std::map<int64_t, MessageTO*> tsMsgTOMap;
std::map<int64_t, EMMessagePtr> tsMsgPtrMap;

static EMCallbackObserverHandle gCallbackObserverHandle;

EMChatManagerListener *gChatManagerListener = nullptr;

void AddTsMsgItem(int64_t ts,  MessageTO* mto, EMMessagePtr msgPtr)
{
    std::lock_guard<std::mutex> maplocker(tsMsgLocker);
    tsMsgTOMap[ts] = mto;
    tsMsgPtrMap[ts] = msgPtr;
}

void DeleteTsMsgItem(int64_t ts)
{
    std::lock_guard<std::mutex> maplocker(tsMsgLocker);
    auto it = tsMsgTOMap.find(ts);
    if(tsMsgTOMap.end() != it) {
        tsMsgTOMap.erase(it);
    }
    
    auto itPtr = tsMsgPtrMap.find(ts);
    if(tsMsgPtrMap.end() != itPtr) {
        tsMsgPtrMap.erase(itPtr);
    }
}

void UpdateTsMsgMap(int64_t ts)
{
    LOG("UpdateTsMsgMap ts:%ld", ts);
    std::lock_guard<std::mutex> maplocker(tsMsgLocker);
    
    auto it = tsMsgTOMap.find(ts);
    if(tsMsgTOMap.end() == it) {
        return;
    }
    
    auto itPtr = tsMsgPtrMap.find(ts);
    if(tsMsgPtrMap.end() == itPtr) {
        return;
    }
    std::string msgId = it->second->MsgId;
    it->second->MsgId = itPtr->second->msgId().c_str();
    LOG("after update, msgid: %s -> %s", msgId.c_str(), it->second->MsgId);
}

HYPHENATE_API void ChatManager_SendMessage(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, void *mto, EMMessageBody::EMMessageBodyType type) {
    EMError error;
    if(!MandatoryCheck(mto, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    int64_t ts = messagePtr->timestamp();
    AddTsMsgItem(ts, (MessageTO*)mto, messagePtr);
    
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Message sent succeeds.");
                                                UpdateTsMsgMap(ts);
                                                if(onSuccess) onSuccess(callbackId);
                                                DeleteTsMsgItem(ts);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Message sent failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                DeleteTsMsgItem(ts);
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().sendMessage(messagePtr);
}

HYPHENATE_API void ChatManager_AddListener(void *client,
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
    if(nullptr == gChatManagerListener) { //only set once!
        gChatManagerListener = new ChatManagerListener(onMessagesReceived, onCmdMessagesReceived, onMessagesRead, onMessagesDelivered, onMessagesRecalled, onReadAckForGroupMessageUpdated, onGroupMessageRead, onConversationsUpdate, onConversationRead);
        CLIENT->getChatManager().addListener(gChatManagerListener);
        LOG("New ChatManager listener and hook it.");
    }
}

HYPHENATE_API void ChatManager_FetchHistoryMessages(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType type, const char * startMessageId, int count, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    // avoid conversationId is released from stack at calling thread
    std::string conversationIdStr = conversationId;
    std::string startMessageIdStr = OptionalStrParamCheck(startMessageId);
    
    std::thread t([=](){
        EMError error;
        EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(conversationIdStr, type, error, count, startMessageIdStr);
        
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            //header
            CursorResultTOV2 cursorResultTo;
            cursorResultTo.NextPageCursor = msgCursorResult.nextPageCursor().c_str();
            cursorResultTo.Type = DataType::ListOfMessage;
            //items
            int size = (int)msgCursorResult.result().size();
            LOG("fetchHistoryMessages history message count:%d", size);
            TOItem** data = new TOItem*[size];
            for(int i=0; i<size; i++) {
                MessageTO *mto = MessageTO::FromEMMessage(msgCursorResult.result().at(i));
                TOItem* item = new TOItem(mto->BodyType, mto);
                LOG("message %d: msgid=%s, type=%d", i, mto->MsgId, mto->BodyType);
                data[i] = item;
            }
            onSuccess((void *)&cursorResultTo, (void **)data, DataType::CursorResult, size, callbackId);
            //free memory
            for(int i=0; i<size; i++) {
                MessageTO::FreeResource((MessageTO*)data[i]->Data);
                delete (MessageTO*)data[i]->Data;
                delete (TOItem*)data[i];
            }
	    //delete array
	    delete []data;
        } else {
            LOG("fetchHistoryMessages history message failed, error id:%d, desc::%s", error.mErrorCode, error.mDescription.c_str());
            onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void ChatManager_GetConversationsFromServer(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            if (onSuccess) {
                int size = (int)conversationList.size();
                LOG("%d conversation found.", size);
                ConversationTO**data = new ConversationTO*[size];
                for(size_t i=0; i<size; i++) {
                    data[i] = ConversationTO::FromEMConversation(conversationList.at(i));
                    LOG("GetConversation %d, id=%s, type=%d, extfiled=%s",i, data[i]->ConverationId, data[i]->type, data[i]->ExtField);
                }
                onSuccess((void**)data, DataType::ListOfConversation, size, callbackId);
                //free memory
                for(size_t i=0; i<size; i++) {
                    delete (ConversationTO*)data[i];
                }
		delete []data;
            }
        }else{
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void ChatManager_RemoveConversation(void *client, const char * conversationId, bool isRemoveMessages)
{
    if(!MandatoryCheck(conversationId))
        return;
    CLIENT->getChatManager().removeConversation(conversationId, isRemoveMessages);
    LOG("Remove conversation completed.");
}

HYPHENATE_API void ChatManager_DownloadMessageAttachments(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Download message attachment succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download message attachment failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().downloadMessageAttachments(messagePtr);
}

HYPHENATE_API void ChatManager_DownloadMessageThumbnail(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Download message thumbnail succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download message thumbnail failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().downloadMessageThumbnail(messagePtr);
}

HYPHENATE_API bool ChatManager_ConversationWithType(void *client, const char * conversationId, EMConversation::EMConversationType type, bool createIfNotExist)
{
    if(!MandatoryCheck(conversationId))
        return false;
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, type, createIfNotExist);
    //verify sharedptr
    if(conversationPtr) {
        LOG("Get converation with id=%s", conversationId);
        return true; //Conversation exist
    } else {
        LOG("Cannot get converation with id=%s", conversationId);
        return false; //Conversation not exist
    }
}

HYPHENATE_API int ChatManager_GetUnreadMessageCount(void *client)
{
    EMError error;
    int count = 0;
    //get conversations
    EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
    if (EMError::EM_NO_ERROR == error.mErrorCode) {
        //sum all unread messages in all conversations
        for(size_t i=0; i<conversationList.size(); i++) {
            count += conversationList[i]->unreadMessagesCount();
        }
    }
    LOG("Unread message count: %d", count);
    return count;
}

HYPHENATE_API bool ChatManager_InsertMessages(void *client, void * messageList[], EMMessageBody::EMMessageBodyType typeList[], int size)
{
    EMMessageList list;
    //convert TO to EMMessagePtr
    for(int i=0; i<size; i++) {
        EMMessagePtr messagePtr = BuildEMMessage(messageList[i], typeList[i]);
        list.push_back(messagePtr);
    }
    if(list.size() > 0) {
        LOG("%d messages will be inserted.");
        return CLIENT->getChatManager().insertMessages(list);
    } else {
        return true;
    }
}

HYPHENATE_API void ChatManager_LoadAllConversationsFromDB(void *client, FUNC_OnSuccess_With_Result onSuccess)
{
    EMConversationList conversationList = CLIENT->getChatManager().loadAllConversationsFromDB();
    
    int size = (int)conversationList.size();
    ConversationTO** data = new ConversationTO*[size];
    LOG("Found conversations %d in Db", size);
    for(size_t i=0; i<size; i++) {
        data[i] = ConversationTO::FromEMConversation(conversationList.at(i));
        LOG("GetConversation %d, id=%s, type=%d, extfiled=%s",i, data[i]->ConverationId, data[i]->type, data[i]->ExtField);
    }
    onSuccess((void**)data, DataType::ListOfConversation, size, -1);
    //free memory
    for(size_t i=0; i<size; i++) {
        delete (ConversationTO*)data[i];
    }
    delete []data;
}

HYPHENATE_API void ChatManager_GetMessage(void *client, const char * messageId, FUNC_OnSuccess_With_Result onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), -1);
        return;
    }
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    if(nullptr == messagePtr) {
        LOG("Cannot find the message with id %s in ChatManager_GetMessage", messageId);
        onSuccess(nullptr, DataType::ListOfMessage, 0, -1);
        return;
    } else {
        LOG("Found the message with id %s in ChatManager_GetMessage", messageId);
        MessageTO* mto = MessageTO::FromEMMessage(messagePtr);
        TOItem* item = new TOItem((int)messagePtr->bodies()[0]->type(), mto);
        TOItem* data[1] = {item};
        onSuccess((void**)data, DataType::ListOfMessage, 1, -1);
        MessageTO::FreeResource(mto);
        delete mto;
        delete item;
    }
}

HYPHENATE_API bool ChatManager_MarkAllConversationsAsRead(void *client)
{
    bool ret = true;
    EMError error;
    EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
    if(conversationList.size() == 0)
        return true;
    else
    {
        LOG("%d conversations will be marked as read.", conversationList.size());
        for(size_t i=0; i<conversationList.size(); i++) {
            if(!conversationList[i]->markAllMessagesAsRead())
                ret = false;
        }
    }
    return ret;
}

HYPHENATE_API void ChatManager_RecallMessage(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        EMError error;
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Recall message succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Recall message failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().recallMessage(messagePtr, error);
}

HYPHENATE_API void ChatManager_ResendMessage(void *client, int callbackId, const char * messageId, FUNC_OnSuccess_With_Result onSuccessResult, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageId);
    //verify message
    if(nullptr == messagePtr) {
        
        LOG("Cannot find message with message id:%s", messageId);
        error.mErrorCode = EMError::MESSAGE_INVALID;
        error.mDescription = "Invalid message.";
        if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
        return;
    }
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Resend message succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Resend message failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                return true;
                                             }));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().resendMessage(messagePtr);
    
    //pass message to c# side
    MessageTO* mto = MessageTO::FromEMMessage(messagePtr);
    TOItem* item = new TOItem((int)messagePtr->bodies()[0]->type(), mto);
    TOItem* data[1] = {item};
    onSuccessResult((void **)data, DataType::ListOfMessage, 1, callbackId);
    MessageTO::FreeResource(mto);
    delete mto;
    delete item;
}

HYPHENATE_API void ChatManager_LoadMoreMessages(void *client, FUNC_OnSuccess_With_Result onSuccess, const char * keywords, int64_t timestamp, int maxcount, const char * from, EMConversation::EMMessageSearchDirection direction)
{
    std::string keywordsStr = OptionalStrParamCheck(keywords);
    std::string fromStr = OptionalStrParamCheck(from);
    EMMessageList messageList = CLIENT->getChatManager().loadMoreMessages(timestamp, keywordsStr, maxcount, fromStr, direction);
    if(messageList.size() == 0) {
        LOG("No messages found with ts:%ld, kw:%s, from:%s, maxc:%d, direct:%d", timestamp, keywordsStr.c_str(), fromStr.c_str(), maxcount, direction);
        return;
    }
    LOG("Found %d messages with ts:%ld, kw:%s, from:%s, maxc:%d, direct:%d", messageList.size(), timestamp, keywordsStr.c_str(), fromStr.c_str(), maxcount, direction);
    int size = (int)messageList.size();
    TOItem** data = new TOItem*[size];
    for(size_t i=0; i<size; i++) {
        MessageTO* mto = MessageTO::FromEMMessage(messageList[i]);
        TOItem* item = new TOItem((int)messageList[i]->bodies()[0]->type(), mto);
        data[i] = item;
    }
    onSuccess((void **)data, DataType::ListOfMessage, size, -1);
    for(size_t i=0; i<size; i++) {
        MessageTO::FreeResource((MessageTO*)data[i]->Data);
        delete (MessageTO*)data[i]->Data;
        delete (TOItem*)data[i];
    }
    delete []data;
}

HYPHENATE_API void ChatManager_SendReadAckForConversation(void *client, int callbackId, const char * conversationId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string conversationIdStr = conversationId;
    
    std::thread t([=](){
        EMError error;
        CLIENT->getChatManager().sendReadAckForConversation(conversationIdStr, error);
        if(EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("Send read ack for conversation:%s successfully.", conversationIdStr.c_str());
            if(onSuccess) onSuccess(callbackId);
        } else {
            LOG("Send read ack for conversation:%s failed.", conversationIdStr.c_str());
            if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

HYPHENATE_API void ChatManager_SendReadAckForMessage(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string messageIdStr = messageId;
    
    std::thread t([=](){
        EMError error;
        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(messageIdStr);
        if(nullptr == messagePtr) {
            
            LOG("Cannot find message with message id:%s", messageIdStr.c_str());
            error.mErrorCode = EMError::MESSAGE_INVALID;
            error.mDescription = "Invalid message.";
            if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
            return;
        }
        CLIENT->getChatManager().sendReadAckForMessage(messagePtr);
        LOG("Send read ack for message:%s successfully.", messageIdStr.c_str());
        if(onSuccess) onSuccess(callbackId);
    });
    t.detach();
}

HYPHENATE_API bool ChatManager_UpdateMessage(void *client, void *mto, EMMessageBody::EMMessageBodyType type)
{
    if(!MandatoryCheck(mto))
        return false;
    EMMessagePtr messagePtr = BuildEMMessage(mto, type, true);
    //only look for conversation, not create one if cannot find.
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(messagePtr->conversationId(), EMConversation::EMConversationType::CHAT, true);
    
    if(nullptr == conversationPtr) {
        LOG("Cannot find conversation with conversation id:%s", messagePtr->conversationId().c_str());
        return false;
    }
    return conversationPtr->updateMessage(messagePtr);
}

void ChatManager_RemoveListener(void*client)
{
    CLIENT->getChatManager().clearListeners();
    if(nullptr != gChatManagerListener) {
        delete gChatManagerListener;
        gChatManagerListener = nullptr;
    }
    LOG("ChatManager listener removed.");
}
