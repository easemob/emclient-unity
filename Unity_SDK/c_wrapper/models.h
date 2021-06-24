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
        LOG("sizeof char=%lu", sizeof(char));
        LOG("Address of MsgId=%x",&MsgId);
        LOG("MsgId=%s", MsgId);
        int i = 0;
        while(MsgId[i] != '\0')
        {
            LOG("MsgId[%d]=%d", i, MsgId[i]);
            i++;
        }
        LOG("Address of ConversationId=%x",&ConversationId);
        LOG("ConversationId=%s", ConversationId);
        LOG("Address of From=%x",&From);
        LOG("From=%s", From);
        i=0;
        while(From[i] != '\0')
        {
            LOG("From[%d]=%d", i, From[i]);
            i++;
        }
        LOG("Address of To=%x",&To);
        LOG("To=%s", To);
        LOG("Address of Type=%x",&Type);
        LOG("Type=%d", Type);
        LOG("Address of Direction=%x",&Direction);
        LOG("Direction=%lu", Direction);
        LOG("Address of Status=%x",&Status);
        LOG("Status=%lu", Status);
        LOG("Address of HasDeliverAck=%x",&HasDeliverAck);
        LOG("HasDeliverAck=%lu", HasDeliverAck);
        LOG("Address of HasReadAck=%x",&HasReadAck);
        LOG("HasReadAck=%lu", HasReadAck);
        LOG("Address of BodyType=%x",&BodyType);
        LOG("BodyType=%lu", BodyType);
        LOG("Address of LocalTime=%x",&LocalTime);
        LOG("LocalTime=%lu", LocalTime);
        LOG("Address of ServerTime=%x",&ServerTime);
        LOG("ServerTime=%lu", ServerTime);
        
        EMMessageBodyPtr messageBody = EMMessageBodyPtr(new EMTextMessageBody("hello"));
        EMMessagePtr messagePtr = EMMessage::createSendMessage(std::string(From), std::string(To), messageBody);
        return messagePtr;
    }
};

#endif //_MODELS_H_
