#ifndef _MODELS_H_
#define _MODELS_H_
#include "emmessage.h"
#include "emchatmanager_interface.h"
#include "emtextmessagebody.h"

using namespace easemob;

struct Options
{
    char *AppKey;
    char *DNSURL;
    char *IMServer;
    char *RestServer;
    int IMPort;
    // sizeof(bool)=1
    bool EnableDNSConfig;
    bool DebugMode;
    bool AutoLogin;
    bool AcceptInvitationAlways;
    bool AutoAcceptGroupInvitation;
    bool RequireAck;
    bool RequireDeliveryAck;
    bool DeleteMessagesAsExitGroup;
    bool DeleteMessagesAsExitRoom;
    bool IsRoomOwnerLeaveAllowed;
    bool SortMessageByServerTime;
    bool UsingHttpsOnly;
    bool ServerTransfer;
    bool IsAutoDownload;
};

enum class AttributeValueType
{
    BOOL,
    CHAR,
    UCHAR,
    SHORT,
    USHORT,
    INT32,
    UINT32,
    INT64,
    UINT64,
    FLOAT,
    DOUBLE,
    STRING,
    STRVECTOR,
    JSONSTRING,
    NULLOBJ
};

union AttributeValueUnion {
    bool BoolV;
    unsigned char CharV;
    char UCharV;
    short ShortV;
    unsigned short UShortV;
    int Int32V;
    unsigned int UInt32V;
    long Int64V;
    unsigned long UInt64V;
    float FloatV;
    double DoubleV;
    char *StringV;
};

struct AttributeValue
{
    AttributeValueType VType;
    AttributeValueUnion Value;
};

struct MessageTransferObject
{
    char * MsgId;
    char * ConversationId;
    char * From;
    char * To;
    EMMessage::EMChatType Type; //EMMessage::EMChatType
    EMMessage::EMMessageDirection Direction; //EMMessage::EMMessageDirection
    EMMessage::EMMessageStatus Status; //EMMessage::EMMessageStatus
    bool HasDeliverAck;
    bool HasReadAck;
    EMMessageBody::EMMessageBodyType BodyType; //EMMessageBody::EMMessageBodyType
    /*void *Body;
    char ** AttributesKeys;
    AttributeValue* AttributesValues;
    int AttributesSize;*/
    long LocalTime;
    long ServerTime;
    
    EMMessagePtr toEMMessage() {
        LOG("Message MsgId: %s", MsgId);
        LOG("Message ConversationId: %s", ConversationId);
        LOG("Message From: %s", From);
        LOG("Message To: %s", To);
        LOG("Is 1:1 message: %s", Type == EMMessage::EMChatType::SINGLE ? "true" : "false");
        LOG("Is SEND message: %s", Direction == EMMessage::EMMessageDirection::SEND ? "true" : "false");
        LOG("Status NEW: %s", Status == EMMessage::EMMessage::NEW ? "true" : "false");
        LOG("Message HasDeliverAck: %s", HasDeliverAck ? "true" : "false");
        LOG("Message HasReadAck: %s", HasReadAck ? "true" : "false");
        LOG("TXT BodyType: %s", BodyType == EMMessageBody::EMMessageBodyType::TEXT ? "true" : "false");
        //LOG("Message Content: %s", Body);
        LOG("sizeof LocalTime: %d", sizeof(LocalTime));
        LOG("Message LocalTime: %ll", LocalTime);
        LOG("Message ServerTime: %ll", ServerTime);
        EMMessageBodyPtr messageBody = EMMessageBodyPtr(new EMTextMessageBody("hello"));
        EMMessagePtr messagePtr = EMMessage::createSendMessage(std::string(From), std::string(To), messageBody);
        return messagePtr;
    }
};

#endif //_MODELS_H_
