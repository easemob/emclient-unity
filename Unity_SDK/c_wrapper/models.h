#ifndef _MODELS_H_
#define _MODELS_H_
#include "emmessage.h"
#include "emchatmanager_interface.h"
#include "emtextmessagebody.h"
#include "emlocationmessagebody.h"
#include "emcmdmessagebody.h"
#include "emfilemessagebody.h"
#include "emimagemessagebody.h"

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

struct GroupReadAck
{
    char * AckId;
    char * MsgId;
    char * From;
    char * Content;
    long Timestamp;
    int Count;
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

//C# side: class TextMessageBodyTO
struct TextMessageBodyTO {
    const char * Content;
};

//C# side: class LocationMessageBodyTo
struct LocationMessageBodyTO {
    double Latitude;
    double Longitude;
    const char * Address;
};

struct CmdMessageBodyTO {
    const char * Action;
    bool DeliverOnlineOnly;
};

struct FileMessageBodyTO {
    const char * LocalPath;
    const char *DisplayName;
    const char * Secret;
    const char * RemotePath;
    long FileSize;
    EMFileMessageBody::EMDownloadStatus DownStatus;
};

struct ImageMessageBodyTO {
    const char * LocalPath;
    const char * DisplayName;
    const char * Secret;
    const char * RemotePath;
    const char * ThumbnailLocalPath;
    const char * ThumbnaiRemotePath;
    const char * ThumbnaiSecret;
    long FileSize;
    double Height;
    double Width;
    EMFileMessageBody::EMDownloadStatus DownStatus;
    EMFileMessageBody::EMDownloadStatus ThumbnaiDownStatus;
    bool Original;
};

class MessageTO
{
public:
    const char * MsgId;
    const char * ConversationId;
    const char * From;
    const char * To;
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
    MessageTO();
    
    static MessageTO * FromEMMessage(const EMMessagePtr &_message);
    
    //virtual ~MessageTO();
protected:
    MessageTO(const EMMessagePtr &message);
};

class TextMessageTO : public MessageTO
{
public:
    TextMessageBodyTO body;
    TextMessageTO(const EMMessagePtr &message);
};

class LocationMessageTO : public MessageTO
{
public:
    LocationMessageBodyTO body;
    LocationMessageTO(const EMMessagePtr &message);
};

class CmdMessageTO : public MessageTO
{
public:
    CmdMessageBodyTO body;
    CmdMessageTO(const EMMessagePtr &message);
};

class FileMessageTO : public MessageTO
{
public:
    FileMessageBodyTO body;
    FileMessageTO(const EMMessagePtr &message);
};

class ImageMessageTO : public MessageTO
{
public:
    ImageMessageBodyTO body;
    ImageMessageTO(const EMMessagePtr &message);
};

enum DataType {
    Bool,
    String,
    Group,
    CursorResult,
    ListOfString,
    ListOfMessage,
    ListOfConversation,
    ListOfGroup,
    ListOfGroupSharedFile,
};

struct CursorResultTO
{
    const char * NextPageCursor;
    DataType Type;
    int Size;
    EMMessageBody::EMMessageBodyType SubTypes[32]; //sub types if any
    void * Data[32]; //list of data
};

#endif //_MODELS_H_
