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

extern "C"
{
#define CLIENT static_cast<EMClient *>(client)
#define CALLBACK static_cast<Callback *>(callback)
}
EMMessagePtr BuildEMMessage(void *mto, EMMessageBody::EMMessageBodyType type)
{
    //compose message body
    std::string from, to;
    EMMessageBodyPtr messageBody;
    switch(type) {
        case EMMessageBody::TEXT:
        {
            auto tm = static_cast<TextMessageTO *>(mto);
            //create message body
            messageBody = EMMessageBodyPtr(new EMTextMessageBody(std::string(tm->body.Content)));
            from = tm->From;
            to = tm->To;
        }
            break;
        case EMMessageBody::LOCATION:
        {
            auto lm = static_cast<LocationMessageTO *>(mto);
            messageBody = EMMessageBodyPtr(new EMLocationMessageBody(lm->body.Latitude, lm->body.Longitude, lm->body.Address));
            from = lm->From;
            to = lm->To;
        }
            break;
        case EMMessageBody::COMMAND:
        {
            auto cm = static_cast<CmdMessageTO *>(mto);
            auto body = new EMCmdMessageBody(cm->body.Action);
            body->deliverOnlineOnly(cm->body.DeliverOnlineOnly);
            messageBody = EMMessageBodyPtr(body);
            from = cm->From;
            to = cm->To;
        }
            break;
    }
    LOG("Message created: From->%s, To->%s.", from.c_str(), to.c_str());
    EMMessagePtr messagePtr = EMMessage::createSendMessage(from, to, messageBody);
    return messagePtr;
}


EMCallbackObserverHandle gCallbackObserverHandle;

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
