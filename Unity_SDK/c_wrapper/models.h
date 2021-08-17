#ifndef _MODELS_H_
#define _MODELS_H_
#include "emmessage.h"
#include "emchatmanager_interface.h"
#include "emcontactmanager_interface.h"
#include "emmuc.h"
#include "emgroup.h"
#include "emchatroom.h"
#include "emgroupmanager_interface.h"
#include "emchatroommanager_interface.h"
#include "emtextmessagebody.h"
#include "emlocationmessagebody.h"
#include "emcmdmessagebody.h"
#include "emfilemessagebody.h"
#include "emimagemessagebody.h"
#include "emvoicemessagebody.h"
#include "emmucsetting.h"
#include "empushconfigs.h"

using namespace easemob;

EMMessagePtr BuildEMMessage(void *mto, EMMessageBody::EMMessageBodyType type, bool buildReceiveMsg=false);

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
    double Height;
    double Width;
    long FileSize;
    EMFileMessageBody::EMDownloadStatus DownStatus;
    EMFileMessageBody::EMDownloadStatus ThumbnaiDownStatus;
    bool Original;
};

struct VoiceMessageBodyTO {
    const char * LocalPath;
    const char * DisplayName;
    const char * Secret;
    const char * RemotePath;
    long FileSize;
    EMFileMessageBody::EMDownloadStatus DownStatus;
    int Duration;
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

class VoiceMessageTO : public MessageTO
{
public:
    VoiceMessageBodyTO body;
    VoiceMessageTO(const EMMessagePtr &message);
};

enum DataType {
    Bool,
    String,
    Group,
    Room,
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
    EMMessageBody::EMMessageBodyType SubTypes[200]; //sub types if any
    void * Data[200]; //list of data
};

struct GroupOptions
{
    EMMucSetting::EMMucStyle Style;
    int MaxCount;
    bool InviteNeedConfirm;
    const char * Ext;

    EMMucSetting toMucSetting() {
        return EMMucSetting(Style, MaxCount, InviteNeedConfirm, Ext);
    }
    
    static GroupOptions FromMucSetting(EMMucSettingPtr setting);
};

struct GroupSharedFileTO
{
    const char * FileName;
    const char * FileId;
    const char * FileOwner;
    long CreateTime;
    long FileSize;
};

struct Mute
{
    const char * Member;
    int64_t Duration;
};

struct GroupTO
{
    const char * GroupId;
    const char * Name;
    const char * Description;
    const char * Owner;
    const char * Announcement;
    char ** MemberList;
    char ** AdminList;
    char ** BlockList;
    Mute * MuteList;
    GroupOptions Options;
    int MemberCount;
    int AdminCount;
    int BlockCount;
    int MuteCount;
    EMGroup::EMMucMemberType PermissionType;
    bool NoticeEnabled;
    bool MessageBlocked;
    bool IsAllMemberMuted;
    
    static GroupTO * FromEMGroup(EMGroupPtr &group);
    
    void LogInfo();
};

struct GroupInfoTO
{
    const char * GroupId;
    const char * Name;
};

struct RoomTO
{
    const char * RoomId;
    const char * Name;
    const char * Description;
    const char * Owner;
    const char * Announcement;
    char ** MemberList;
    char ** AdminList;
    char ** BlockList;
    Mute * MuteList;
    int MemberCount;
    int AdminCount;
    int BlockCount;
    int MuteCount;
    EMGroup::EMMucMemberType PermissionType;
    int MaxUsers;
    bool IsAllMemberMuted;
    
    static RoomTO * FromEMChatRoom(EMChatroomPtr &room);
    
    void LogInfo();
};

struct GroupReadAckTO
{
    const char * metaId;
    const char * msgId;
    const char * from;
    const char * content;
    int count;
    long timestamp;
    
    static GroupReadAckTO * FromGroupReadAck(EMGroupReadAckPtr&  groupReadAckPtr);
};

struct ConversationTO
{
    const char * ConverationId;
    EMConversation::EMConversationType type;
    const char * ExtField;
};

struct PushConfigTO
{
    bool NoDisturb;
    int NoDisturbStartHour;
    int NoDisturbEndHour;
    easemob::EMPushConfigs::EMPushDisplayStyle Style;
};

struct TOArray
{
    DataType Type;
    int Size;
    void * Data[200]; //list of data
};

struct TOArrayDiff
{
    int Size;
    void * Data[200];
    int Type[200];
};

#endif //_MODELS_H_
