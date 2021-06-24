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

struct TextMessageBody {
    char * content;
};

struct FileMessageBody {
    char * localPath;
};

union MessageBody {
    TextMessageBody textBody;
    FileMessageBody fileBody;
};

struct MessageTransferObject
{
    char * MsgId;
    char * ConversationId;
    char * From;
    char * To;
    EMMessage::EMChatType Type;
    EMMessage::EMMessageDirection Direction;
    EMMessage::EMMessageStatus Status;
    bool HasDeliverAck;
    bool HasReadAck;
    
    /*char ** AttributesKeys;
    AttributeValue* AttributesValues;
    int AttributesSize;*/
    
    long LocalTime;
    long ServerTime;
    
    EMMessageBody::EMMessageBodyType BodyType;
    MessageBody Body;
    
    EMMessagePtr toEMMessage() {
        //compose message body
        EMMessageBodyPtr messageBody = EMMessageBodyPtr(new EMTextMessageBody("hello"));
        /*if(BodyType == EMMessageBody::TEXT) {
            messageBody = EMMessageBodyPtr(new EMTextMessageBody("test"));
        }*/
        EMMessagePtr messagePtr = EMMessage::createSendMessage(std::string(From), std::string(To), messageBody);
        return messagePtr;
    }
};

#endif //_MODELS_H_
