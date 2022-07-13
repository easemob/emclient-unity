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

typedef std::map<std::string, MessageTO*>::iterator TsMsgIter;
typedef std::map<std::string, EMMessagePtr>::iterator TsMsgPtrIter;

std::mutex msgLocker;
//MessgeTO must be freed at C# side!
std::map<std::string, MessageTO*> msgTOMap;
std::map<std::string, EMMessagePtr> msgPtrMap;
std::map<std::string, MessageTOLocal> msgTOLocalMap;

std::mutex progressMsgLocker;
std::map<std::string, int> progressMap;

static EMCallbackObserverHandle gCallbackObserverHandle;

EMChatManagerListener *gChatManagerListener = nullptr;
EMReactionManagerListener* gReactionManagerListener = nullptr;

void AddMsgItem(std::string msgId,  MessageTO* mto, EMMessagePtr msgPtr)
{
    std::lock_guard<std::mutex> maplocker(msgLocker);
    msgTOMap[msgId] = mto;
    msgPtrMap[msgId] = msgPtr;
    
    MessageTOLocal toLocal;
    msgTOLocalMap[msgId] = toLocal;
}

void DeleteMsgItem(std::string msgId)
{
    std::lock_guard<std::mutex> maplocker(msgLocker);
    auto it = msgTOMap.find(msgId);
    if(msgTOMap.end() != it) {
        msgTOMap.erase(it);
    }
    
    auto itPtr = msgPtrMap.find(msgId);
    if(msgPtrMap.end() != itPtr) {
        msgPtrMap.erase(itPtr);
    }
    
    auto itl = msgTOLocalMap.find(msgId);
    if (msgTOLocalMap.end() != itl) {
        msgTOLocalMap.erase(itl);
    }
}

void UpdateMsgMap(std::string msgId)
{
    LOG("UpdateMsgMap msgId:%s", msgId.c_str());
    std::lock_guard<std::mutex> maplocker(msgLocker);
    
    auto it = msgTOMap.find(msgId);
    if(msgTOMap.end() == it) {
        return;
    }
    
    auto itPtr = msgPtrMap.find(msgId);
    if(msgPtrMap.end() == itPtr) {
        return;
    }
    
    auto itl = msgTOLocalMap.find(msgId);
    if(msgTOLocalMap.end() == itl) {
        return;
    }
    
    std::string beforeMsgId = it->second->MsgId;
    
    UpdateMessageTO((void*)it->second, itPtr->second, (void*)&itl->second);
    
    LOG("after update, msgid: %s -> %s", beforeMsgId.c_str(), it->second->MsgId);
}

void AddProgressItem(std::string msgId)
{
    std::lock_guard<std::mutex> maplocker(progressMsgLocker);
    progressMap[msgId] = 0;
}

void DeleteProgressItem(std::string msgId)
{
    std::lock_guard<std::mutex> maplocker(progressMsgLocker);
    auto it = progressMap.find(msgId);
    if(progressMap.end() != it) {
        progressMap.erase(it);
    }
}

void UpdateProgressMap(std::string msgId, int progress)
{
    std::lock_guard<std::mutex> maplocker(progressMsgLocker);
    
    auto it = progressMap.find(msgId);
    if(progressMap.end() == it) {
        return;
    }
    it->second = progress;
}

int GetLastProgress(std::string msgId)
{
    std::lock_guard<std::mutex> maplocker(progressMsgLocker);
    auto it = progressMap.find(msgId);
    if(progressMap.end() == it) {
        return 0;
    }
    return it->second;
}

HYPHENATE_API void ChatManager_SendMessage(void *client, int callbackId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress, void *mto, EMMessageBody::EMMessageBodyType type) {
    EMError error;
    if(!MandatoryCheck(mto, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    std::string msgId = messagePtr->msgId();
    
    AddMsgItem(msgId, (MessageTO*)mto, messagePtr);
    AddProgressItem(msgId);
    
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Message sent succeeds.");
                                                UpdateMsgMap(msgId);
                                                if(onSuccess) onSuccess(callbackId);
                                                DeleteMsgItem(msgId);
                                                DeleteProgressItem(msgId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Message sent failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                DeleteMsgItem(msgId);
                                                DeleteProgressItem(msgId);
                                                return true;
                                             },
                                             [=](int progress){
                                                LOG("Message send in progress %d percent.", progress);
                                                int last_progress = GetLastProgress(msgId);
                                                if(progress - last_progress >= 5) {
                                                    if(onProgress) onProgress(progress, callbackId);
                                                    UpdateProgressMap(msgId, progress);
                                                }
                                                return;
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
                                       FUNC_OnConversationRead onConversationRead,
                                       FUNC_MessageReactionDidChange messageReactionDidChange
                                       )
{
    if(nullptr == gChatManagerListener) { //only set once!
        gChatManagerListener = new ChatManagerListener(onMessagesReceived, onCmdMessagesReceived, onMessagesRead, onMessagesDelivered, onMessagesRecalled, onReadAckForGroupMessageUpdated, onGroupMessageRead, onConversationsUpdate, onConversationRead);
        CLIENT->getChatManager().addListener(gChatManagerListener);
        LOG("New ChatManager listener and hook it.");
    }

    if (nullptr == gReactionManagerListener) {
        gReactionManagerListener = new ReactionManagerListener(messageReactionDidChange);
        CLIENT->getReactionManager().addListener(gReactionManagerListener);
        LOG("New ReactionManager listener and hook it.");
    }
}

HYPHENATE_API void ChatManager_FetchHistoryMessages(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType type, 
    const char * startMessageId, int count, EMConversation::EMMessageSearchDirection direction, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError)
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
        EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(conversationIdStr, type, error, startMessageIdStr, count, direction);
        
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

HYPHENATE_API void ChatManager_RemoveConversation(void *client, const char * conversationId, bool isRemoveMessages, bool isThread)
{
    if(!MandatoryCheck(conversationId))
        return;
    CLIENT->getChatManager().removeConversation(conversationId, isRemoveMessages, isThread);
    LOG("Remove conversation completed.");
}

HYPHENATE_API void ChatManager_DownloadMessageAttachments(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress)
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
    
    std::string msgId(messageId);
    AddProgressItem(msgId);
    
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Download message attachment succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                DeleteProgressItem(msgId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download message attachment failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                DeleteProgressItem(msgId);
                                                return true;
                                             },
                                             [=](int progress){
                                                LOG("Download message attachment in progress %d percent.", progress);
                                                int last_progress = GetLastProgress(msgId);
                                                if(progress - last_progress >= 5) {
                                                    if(onProgress) onProgress(progress, callbackId);
                                                    UpdateProgressMap(msgId, progress);
                                                }
                                                return;
                                             }
                                             ));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().downloadMessageAttachments(messagePtr);
}

HYPHENATE_API void ChatManager_DownloadMessageThumbnail(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress)
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
    
    std::string msgId(messageId);
    AddProgressItem(msgId);
    
    EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                                             [=]()->bool {
                                                LOG("Download message thumbnail succeeds.");
                                                if(onSuccess) onSuccess(callbackId);
                                                DeleteProgressItem(msgId);
                                                return true;
                                             },
                                             [=](const easemob::EMErrorPtr error)->bool{
                                                LOG("Download message thumbnail failed with code=%d.", error->mErrorCode);
                                                if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
                                                DeleteProgressItem(msgId);
                                                return true;
                                             },
                                             [=](int progress){
                                                LOG("Download message thumbnail in progress %d percent.", progress);
                                                int last_progress = GetLastProgress(msgId);
                                                if(progress - last_progress >= 5) {
                                                    if(onProgress) onProgress(progress, callbackId);
                                                    UpdateProgressMap(msgId, progress);
                                                }
                                                return;
                                             }
                                             ));
    messagePtr->setCallback(callbackPtr);
    CLIENT->getChatManager().downloadMessageThumbnail(messagePtr);
}

HYPHENATE_API bool ChatManager_ConversationWithType(void *client, const char * conversationId, EMConversation::EMConversationType type, 
                                                    bool createIfNotExist, bool isThread)
{
    if(!MandatoryCheck(conversationId))
        return false;
    EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conversationId, type, createIfNotExist, isThread);
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

HYPHENATE_API void ChatManager_RecallMessage(void *client, int callbackId, const char * messageId, FUNC_OnSuccess onSuccess, FUNC_OnError onError, FUNC_OnProgress onProgress)
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
                                             },
                                             [=](int progress){
                                                LOG("Recall message in progress %d percent.", progress);
                                                if(onProgress) onProgress(progress, callbackId);
                                                return;
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

HYPHENATE_API void ChatManager_SendReadAckForGroupMessage(void *client,int callbackId, const char * messageId, const char* ackContent, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(messageId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    std::string messageIdStr = messageId;
    std::string ackContentStr = GetUTF8FromUnicode(ackContent);
    
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
        CLIENT->getChatManager().sendReadAckForGroupMessage(messagePtr, ackContentStr);
        LOG("Send read ack for group message:%s successfully.", messageIdStr.c_str());
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

HYPHENATE_API void ChatManager_RemoveMessagesBeforeTimestamp(void *client, int callbackId, int64_t timeStamp,  FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    std::thread t([=](){
        EMError error;
        bool ret = CLIENT->getChatManager().removeMessagesBeforeTimestamp(timeStamp);
        if(false == ret) {
            LOG("RemoveMessagesBeforeTimestamp failed, ts:%d", timeStamp);
            error.mErrorCode = EMError::DATABASE_ERROR;
            error.mDescription = "Remove messages error";
            if(onError) onError(error.mErrorCode,error.mDescription.c_str(), callbackId);
            return;
        }
        LOG("RemoveMessagesBeforeTimestamp successfully.");
        if(onSuccess) onSuccess(callbackId);
    });
    t.detach();
}

HYPHENATE_API void ChatManager_DeleteConversationFromServer(void *client, int callbackId, const char * conversationId, EMConversation::EMConversationType conversationType, bool isDeleteServerMessages, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if(!MandatoryCheck(conversationId, error)) {
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    std::string conversationIdStr = conversationId;
    
    std::thread t([=](){
        EMErrorPtr error = CLIENT->getChatManager().deleteConversationFromServer(conversationIdStr, conversationType, isDeleteServerMessages);
        if(EMError::EM_NO_ERROR != error->mErrorCode) {
            LOG("DeleteConversationFromServer failed for conversation id:%s", conversationIdStr.c_str());
            if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
            return;
        }
        LOG("DeleteConversationFromServer successfully for conversation id:%s", conversationIdStr.c_str());
        if(onSuccess) onSuccess(callbackId);
    });
    t.detach();
}

HYPHENATE_API void ChatManager_FetchSupportLanguages(void *client, int callbackId, FUNC_OnSuccess_With_Result onSuccessResult, FUNC_OnError onError)
{
    std::thread t([=](){
        std::vector<std::tuple<std::string,std::string,std::string>> languages;
        
        EMErrorPtr error = CLIENT->getChatManager().fetchSupportLanguages(languages);
        
        if(EMError::EM_NO_ERROR != error->mErrorCode) {
            LOG("ChatManager_FetchSupportLanguages failed");
            if(onError) onError(error->mErrorCode,error->mDescription.c_str(), callbackId);
            return;
        }
        
        LOG("ChatManager_FetchSupportLanguages successfully");
        if(onSuccessResult) {
            if (languages.size() == 0) {
                onSuccessResult(nullptr, DataType::ListOfString, 0, callbackId);
                return;
            }
            
            SupportLanguagesTO** data = new SupportLanguagesTO*[languages.size()];
            SupportLanguagesTO* localData = new SupportLanguagesTO[languages.size()];
            
            for(int i=0; i<languages.size(); i++) {
                
                SupportLanguagesTO slto;
                slto.languageCode = std::get<0>(languages[i]).c_str();
                slto.languageName = std::get<1>(languages[i]).c_str();
                slto.languageNativeName = std::get<2>(languages[i]).c_str();
                
                localData[i] = slto;
                data[i] = &(localData[i]);
            }
            onSuccessResult((void**)data, DataType::ListOfString, (int)languages.size(), callbackId);
            delete[]data;
            delete[]localData;
        }
    });
    t.detach();
}

HYPHENATE_API void ChatManager_TranslateMessage(void *client, int callbackId, void *mto, EMMessageBody::EMMessageBodyType type, const char * targetLanguages[], int size, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    
    if(!MandatoryCheck(mto) || size <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        if(onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }
    
    EMMessagePtr messagePtr = BuildEMMessage(mto, type);
    std::string msgId = messagePtr->msgId();
    
    std::vector<std::string> vec;
    for(int i=0; i<size; i++){
        vec.push_back(targetLanguages[i]);
    }
    
    AddMsgItem(msgId, (MessageTO*)mto, messagePtr);
    
    std::thread t([=](){
        for(int i=0; i<size; i++) {
            LOG("ChatManager_TranslateMessage, language: %s", vec[i].c_str());
        }
        
        EMErrorPtr error = CLIENT->getChatManager().translateMessage(messagePtr, vec);
        if(EMError::EM_NO_ERROR == error->mErrorCode) {
            UpdateMsgMap(msgId);
            if(onSuccess) onSuccess(callbackId);
            DeleteMsgItem(msgId);
        }else{
            if(onError) onError(error->mErrorCode, error->mDescription.c_str(), callbackId);
            DeleteMsgItem(msgId);
        }
    });
    t.detach();
}

HYPHENATE_API void ChatManager_FetchGroupReadAcks(void* client, int callbackId, const char* messageId, const char* groupId, int pageSize, const char* startAckId, FUNC_OnSuccess_With_Result_V2 onSuccess, FUNC_OnError onError)
{
    EMError error;

    if (!MandatoryCheck(messageId, groupId) || pageSize <= 0) {
        error.setErrorCode(EMError::GENERAL_ERROR);
        error.mDescription = "Mandatory parameter is null!";
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string msgId = messageId;
    std::string gid = groupId;
    std::string startAckIdStr = OptionalStrParamCheck(startAckId);

    std::thread t([=]() {
        EMError error;
        int totalCount = 0;
        EMCursorResultRaw<EMGroupReadAckPtr> msgCursorResult = CLIENT->getChatManager().fetchGroupReadAcks(msgId, groupId, error, pageSize, &totalCount, startAckIdStr);

        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            //header
            CursorResultTOV2 cursorResultTo;
            cursorResultTo.NextPageCursor = msgCursorResult.nextPageCursor().c_str();
            cursorResultTo.Type = DataType::ListOfGroup;
            //items
            int size = (int)msgCursorResult.result().size();
            LOG("ChatManager_FetchGroupReadAcks group ack count:%d", size);
            TOItem** data = new TOItem * [size];
            for (int i = 0; i < size; i++) {
                GroupReadAckTO* gkto = GroupReadAckTO::FromGroupReadAck(msgCursorResult.result().at(i));
                TOItem* item = new TOItem(DataType::Group, gkto);
                LOG("group ack %d: metaid=%s, msgid=%s, from=%s", i, gkto->metaId, gkto->msgId, gkto->from);
                data[i] = item;
            }
            onSuccess((void*)&cursorResultTo, (void**)data, DataType::CursorResult, size, callbackId);
            //free memory
            for (int i = 0; i < size; i++) {
                delete (MessageTO*)data[i]->Data;
                delete (TOItem*)data[i];
            }
            //delete array
            delete[]data;
        }
        else {
            LOG("ChatManager_FetchGroupReadAcks failed, error id:%d, desc::%s", error.mErrorCode, error.mDescription.c_str());
            onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
        });
    t.detach();
}

HYPHENATE_API void ChatManager_ReportMessage(void* client, int callbackId, const char* messageId, const char* tag, const char* reason, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(messageId, tag, reason, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string messageIdStr = messageId;
    std::string tagStr = GetUTF8FromUnicode(tag);
    std::string reasonStr = GetUTF8FromUnicode(reason);

    std::thread t([=]() {
        EMError error;
        CLIENT->getChatManager().reportMessage(messageIdStr, tagStr, reasonStr, error);
        if (EMError::EM_NO_ERROR != error.mErrorCode) {
            LOG("ChatManager_ReportMessage failed for messageId id:%s", messageIdStr.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
            return;
        }
        LOG("ChatManager_ReportMessage successfully for messageId:%s", messageIdStr.c_str());
        if (onSuccess) onSuccess(callbackId);
        });
    t.detach();
}

HYPHENATE_API void ChatManager_AddReaction(void* client, int callbackId, const char* messageId, const char* reaction, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(messageId, reaction, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string messageIdStr = messageId;
    std::string reactionStr = GetUTF8FromUnicode(reaction);

    std::thread t([=]() {
        EMError error;
        CLIENT->getReactionManager().addReaction(messageIdStr, reactionStr, error);
        if (EMError::EM_NO_ERROR != error.mErrorCode) {
            LOG("ChatManager_AddReaction failed for messageId id:%s", messageIdStr.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
            return;
        }
        LOG("ChatManager_AddReaction successfully for messageId:%s", messageIdStr.c_str());
        if (onSuccess) onSuccess(callbackId);
    });
    t.detach();
}

HYPHENATE_API void ChatManager_RemoveReaction(void* client, int callbackId, const char* messageId, const char* reaction, FUNC_OnSuccess onSuccess, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(messageId, reaction, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string messageIdStr = messageId;
    std::string reactionStr = GetUTF8FromUnicode(reaction);

    std::thread t([=]() {
        EMError error;
        CLIENT->getReactionManager().removeReaction(messageIdStr, reactionStr, error);
        if (EMError::EM_NO_ERROR != error.mErrorCode) {
            LOG("ChatManager_RemoveReaction failed for messageId id:%s", messageIdStr.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
            return;
        }
        LOG("ChatManager_RemoveReaction successfully for messageId:%s", messageIdStr.c_str());
        if (onSuccess) onSuccess(callbackId);
        });
    t.detach();
}

HYPHENATE_API void ChatManager_GetReactionList(void* client, int callbackId, const char* messageIdList, const char* messageType, const char* groupId, FUNC_OnSuccess_With_Result onSuccessResult, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(messageIdList, messageType, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string messageIdListStr = messageIdList;
    std::vector<std::string> vec = JsonStringToVector(messageIdListStr);
    std::string messageTypeStr = messageType;
    std::string groupIdStr = groupId;

    std::thread t([=]() {
        EMError error;
        std::map<std::string, EMMessageReactionList> map = CLIENT->getReactionManager().getReactionList(vec, messageTypeStr, groupIdStr, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            LOG("ChatManager_GetReactionList successfully");
            if (onSuccessResult) {
                std::string jstr = MessageReactionTO::ToJson(map);
                const char* data[1] = { jstr.c_str() };
                onSuccessResult((void**)data, DataType::String, 1, callbackId);
            }
        }
        else {
            LOG("ChatManager_GetReactionList failed");
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
     });
    t.detach();
}

HYPHENATE_API void ChatManager_GetReactionDetail(void* client, int callbackId, const char* messageId, const char* reaction, const char* cursor, uint64_t pageSize, FUNC_OnSuccess_With_Result_V2 onSuccessResult, FUNC_OnError onError)
{
    EMError error;
    if (!MandatoryCheck(messageId, reaction, error)) {
        if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        return;
    }

    std::string messageIdStr = messageId;
    std::string reactionStr = GetUTF8FromUnicode(reaction);
    std::string cursorStr = OptionalStrParamCheck(cursor);

    std::thread t([=]() {
        EMError error;
        std::string outCursor = "";
        EMMessageReactionPtr ret = CLIENT->getReactionManager().getReactionDetail(messageId, reactionStr, cursorStr, pageSize, outCursor, error);
        if (EMError::EM_NO_ERROR == error.mErrorCode) {

            //header
            CursorResultTOV2 cursorResultTo;
            cursorResultTo.NextPageCursor = outCursor.c_str();
            cursorResultTo.Type = DataType::String;

            //items
            LOG("ChatManager_GetReactionDetail successfully, for messageId:%s and reaction: %s", messageIdStr.c_str(), reactionStr.c_str());
            if (onSuccessResult) {
                std::string jstr = MessageReactionTO::ToJson(ret);
                const char* data[1] = { jstr.c_str() };
                onSuccessResult((void*)&cursorResultTo, (void**)data, DataType::CursorResult, 1, callbackId);
            }
        }
        else {
            LOG("ChatManager_GetReactionDetail failed for messageId:%s and reaction:%s", messageIdStr.c_str(), reactionStr.c_str());
            if (onError) onError(error.mErrorCode, error.mDescription.c_str(), callbackId);
        }
    });
    t.detach();
}

void ChatManager_RemoveListener(void*client)
{
    CLIENT->getChatManager().clearListeners();
    if(nullptr != gChatManagerListener) {
        delete gChatManagerListener;
        gChatManagerListener = nullptr;
    }
    if (nullptr != gReactionManagerListener) {
        delete gReactionManagerListener;
        gReactionManagerListener = nullptr;
    }
    LOG("ChatManager listener removed.");
}
