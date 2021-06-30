//
//  models.cpp
//  hyphenateCWrapper
//
//  Created by Bingo Zhao on 2021/6/30.
//  Copyright © 2021 easemob. All rights reserved.
//

#include "models.h"

//MessageTO
MessageTO::MessageTO(EMMessagePtr &_message) {
    this->MsgId = _message->msgId().c_str();
    this->ConversationId = _message->conversationId().c_str();
    this->From = _message->from().c_str();
    this->To = _message->to().c_str();
    this->Type = _message->chatType();
    this->Direction = _message->msgDirection();
    this->Status = _message->status();
    this->HasDeliverAck = _message->isDeliverAcked();
    this->HasReadAck = _message->isReadAcked();
    this->LocalTime = _message->localTime();
    this->ServerTime = _message->timestamp();
}

//TextMessageTO
TextMessageTO::TextMessageTO(EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMTextMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Content = body->text().c_str();
}

//LocationMessageTO
LocationMessageTO::LocationMessageTO(EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMLocationMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Latitude = body->latitude();
    this->body.Longitude = body->longitude();
    this->body.Address = body->address().c_str();
}

//CmdMessageTO
CmdMessageTO::CmdMessageTO(EMMessagePtr &_message):MessageTO(_message) {
    auto body = (EMCmdMessageBody *)_message->bodies().front().get();
    this->BodyType = body->type(); //TODO: only 1st body type determined
    this->body.Action = body->action().c_str();
    this->body.DeliverOnlineOnly = body->isDeliverOnlineOnly();
}

MessageTO * MessageTO::FromEMMessage(EMMessagePtr &_message)
{
    //TODO: assume that only 1 body in _message->bodies
    MessageTO * message;
    auto body = _message->bodies().front();
    switch(body->type()) {
        case EMMessageBody::TEXT:
        {
            message = new TextMessageTO(_message);
        }
            break;
        case EMMessageBody::LOCATION:
        {
            message = new LocationMessageTO(_message);
        }
            break;
        default:
            message = NULL;
    }    
    return message;
}
