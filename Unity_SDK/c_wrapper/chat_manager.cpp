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
EMMessagePtr toEMMessage(void *mto, EMMessageBody::EMMessageBodyType type)
{
    //compose message body
    std::string from, to;
    EMMessageBodyPtr messageBody;
    if(type == EMMessageBody::TEXT) {
        auto tm = static_cast<TextMessageTO *>(mto);
        //create message body
        messageBody = EMMessageBodyPtr(new EMTextMessageBody(std::string(tm->body.Content)));
        //unlink Body.textBody
        tm->body.Content = nullptr;
        from = tm->header.From;
        to = tm->header.To;
    }else if(type == EMMessageBody::LOCATION) {
        auto tm = static_cast<LocationMessageTO *>(mto);
        messageBody = EMMessageBodyPtr(new EMLocationMessageBody(tm->body.Latitude, tm->body.Longitude, tm->body.Address));
        tm->body.Address = nullptr;
        from = tm->header.From;
        to = tm->header.To;
    }else if(type == EMMessageBody::COMMAND) {
        auto cm = static_cast<CmdMessageTO *>(mto);
        auto body = new EMCmdMessageBody(cm->body.Action);
        body->deliverOnlineOnly(cm->body.DeliverOnlineOnly);
        messageBody = EMMessageBodyPtr(body);
        cm->body.Action = nullptr;
        from = cm->header.From;
        to = cm->header.To;
    }
    LOG("Message created: From->%s, To->%s.", from.c_str(), to.c_str());
    EMMessagePtr messagePtr = EMMessage::createSendMessage(from, to, messageBody);
    return messagePtr;
}


EMCallbackObserverHandle gCallbackObserverHandle;

AGORA_API void ChatManager_SendMessage(void *client, FUNC_OnSuccess onSuccess, FUNC_OnError onError, void *mto, EMMessageBody::EMMessageBodyType type) {
    EMMessagePtr messagePtr = toEMMessage(mto, type);
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

AGORA_API void ChatManager_AddListener(void *client,
                                       FUNC_OnMessagesReceived onMessagesReceived,
                                       FUNC_OnCmdMessagesReceived onCmdMessagesReceived,
                                       FUNC_OnMessagesRead onMessagesRead,
                                       FUNC_OnMessagesDelivered onMessagesDelivered,
                                       FUNC_OnMessagesRecalled onMessagesRecalled,
                                       FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated,
                                       FUNC_OnConversationsUpdate onConversationsUpdate,
                                       FUNC_OnConversationRead onConversationRead
                                       )
{
    //TODO: implement EMChatManagerListener interface
}
