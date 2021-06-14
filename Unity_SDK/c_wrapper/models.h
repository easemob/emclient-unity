#ifndef _MODELS_H_
#define _MODELS_H_
#include "emmessage.h"
#include "emchatmanager_interface.h"
#include "emtextmessagebody.h"

using namespace easemob;

typedef struct _Options
{
  char *AppKey;
  char *DNSURL;
  char *IMServer;
  char *RestServer;
  int IMPort;
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
  bool EnableDNSConfig;
} Options;

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

typedef struct _AttributeValue
{
    AttributeValueType VType;
    AttributeValueUnion Value;
}AttributeValue;

typedef struct _MessageTransferObject
{
    char * MsgId;
    char * ConversationId;
    char * From;
    char * To;
    EMMessage::EMChatType Type;
    EMMessage::EMMessageDirection Direction;
    EMMessage::EMMessageStatus Status;
    long LocalTime;
    long ServerTime;
    bool HasDeliverAck;
    bool HasReadAck;
    EMMessageBody::EMMessageBodyType BodyType;
    void *Body;
    char ** AttributesKeys;
    AttributeValue* AttributesValues;
    int AttributesSize;
    
    EMMessagePtr toEMMessage() {
        LOG("Message MsgId: %s", MsgId);
        LOG("Message ConversationId: %s", ConversationId);
        LOG("Message From: %s", From);
        LOG("Message To: %s", To);
        LOG("Message Type: %d", Type);
        LOG("Message Direction: %d", Direction);
        LOG("Message Status: %d", Status);
        LOG("Message LocalTime: %l", LocalTime);
        LOG("Message ServerTime: %l", ServerTime);
        LOG("Message HasDeliverAck: %s", HasDeliverAck ? "true" : "false");
        LOG("Message HasReadAck: %s", HasReadAck ? "true" : "false");
        LOG("Message BodyType: %d", BodyType);
        //LOG("Message Content: %s", Body);
        EMMessageBodyPtr messageBody = EMMessageBodyPtr(new EMTextMessageBody("hello"));
        EMMessagePtr messagePtr = EMMessage::createSendMessage(From, To, messageBody);
        /*messagePtr->setMsgId(MsgId);
        messagePtr->setConversationId(ConversationId);
        messagePtr->setChatType(Type);
        messagePtr->setMsgDirection(Direction);
        messagePtr->setStatus(Status);
        messagePtr->setLocalTime(LocalTime);
        messagePtr->setIsDeliverAcked(HasDeliverAck);
        messagePtr->setIsReadAcked(HasReadAck);*/
        return messagePtr;
    }
} MessageTransferObject;

#endif //_MODELS_H_
