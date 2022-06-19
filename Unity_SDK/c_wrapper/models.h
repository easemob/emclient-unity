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
#include "emvideomessagebody.h"
#include "emcustommessagebody.h"
#include "emmucsetting.h"
#include "empushconfigs.h"
#include "empresence.h"
#include "json.hpp"

using namespace easemob;

const int ARRAY_SIZE_LIMITATION = 200;

EMMessagePtr BuildEMMessage(void *mto, EMMessageBody::EMMessageBodyType type, bool buildReceiveMsg=false);
void UpdateMessageTO(void *mto, EMMessagePtr msg, void* localMto);
void SetMessageAttrs(EMMessagePtr msg, string attrs);
std::string GetAttrsStringFromMessage(EMMessagePtr msg);

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
    int64_t Int64V;
    uint64_t UInt64V;
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
    const char * TargetLanguages;
    const char * Translations;
};

//C# side: class LocationMessageBodyTo
struct LocationMessageBodyTO {
    double Latitude;
    double Longitude;
    const char * Address;
    const char * BuildingName;
};

struct CmdMessageBodyTO {
    const char * Action;
    bool DeliverOnlineOnly;
};

struct FileMessageBodyTO {
    const char * LocalPath;
    const char * DisplayName;
    const char * Secret;
    const char * RemotePath;
    int64_t FileSize;
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
    int64_t FileSize;
    EMFileMessageBody::EMDownloadStatus DownStatus;
    EMFileMessageBody::EMDownloadStatus ThumbnaiDownStatus;
    bool Original;
};

struct VoiceMessageBodyTO {
    const char * LocalPath;
    const char * DisplayName;
    const char * Secret;
    const char * RemotePath;
    int64_t FileSize;
    EMFileMessageBody::EMDownloadStatus DownStatus;
    int Duration;
};

struct VideoMessageBodyTO {
    const char * LocalPath;
    const char * DisplayName;
    const char * Secret;
    const char * RemotePath;
    const char * ThumbnaiLocationPath;
    const char * ThumbnaiRemotePath;
    const char * ThumbnaiSecret;
    double Height;
    double Width;
    int Duration;
    int64_t FileSize;
    EMFileMessageBody::EMDownloadStatus DownStatus;
};

struct CustomMessageBodyTO {
    const char *  CustomEvent;
    const char *  CustomParams;
};

class MessageTO
{
public:
    const char * MsgId;
    const char * ConversationId;
    const char * From;
    const char * To;
    const char * RecallBy;
    EMMessage::EMChatType Type;
    EMMessage::EMMessageDirection Direction;
    EMMessage::EMMessageStatus Status;
    bool HasDeliverAck;
    bool HasReadAck;
    bool IsNeedGroupAck;
    bool IsRead;
    bool MessageOnlineState;

    const char * AttributesValues;
    int64_t LocalTime;
    int64_t ServerTime;
    
    EMMessageBody::EMMessageBodyType BodyType;
    MessageTO();
    
    static MessageTO * FromEMMessage(const EMMessagePtr &_message);
    //since can not use destory function
    //so here add a function to free related resource
    static void FreeResource(MessageTO * mto);
    
    //virtual ~MessageTO();
protected:
    MessageTO(const EMMessagePtr &message);
};

struct MessageTOLocal {
    std::string MsgId;
    EMMessageBody::EMMessageBodyType BodyType;
    
    // text body fields
    std::string Content;
    std::string TargetLanguages; // json string
    std::string Translations; // json string
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

class VideoMessageTO : public MessageTO
{
public:
    VideoMessageBodyTO body;
    VideoMessageTO(const EMMessagePtr &message);
};

class CustomMessageTO : public MessageTO
{
public:
    CustomMessageBodyTO body;
    CustomMessageTO(const EMMessagePtr &message);
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
    EMMessageBody::EMMessageBodyType SubTypes[ARRAY_SIZE_LIMITATION]; //sub types if any
    void * Data[ARRAY_SIZE_LIMITATION]; //list of data
};

struct CursorResultTOV2
{
    const char * NextPageCursor;
    DataType Type;
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
    int64_t CreateTime;
    int64_t FileSize;
    
    static GroupSharedFileTO * FromEMGroupSharedFile(const EMMucSharedFilePtr &sharedFile);
    static void DeleteGroupSharedFileTO(GroupSharedFileTO* gto);
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
    
    ~GroupTO();
    
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
    int MemberListCount;
    int MemberCount;
    int AdminCount;
    int BlockCount;
    int MuteCount;
    EMGroup::EMMucMemberType PermissionType;
    int MaxUsers;
    bool IsAllMemberMuted;
    
    ~RoomTO();

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
    int64_t timestamp;
    
    static GroupReadAckTO * FromGroupReadAck(const EMGroupReadAckPtr&  groupReadAckPtr);
};

struct ConversationTO
{
    const char * ConverationId;
    EMConversation::EMConversationType type;
    const char * ExtField;
    
    static ConversationTO * FromEMConversation(EMConversationPtr&  conversationPtr);
};

struct PushConfigTO
{
    bool NoDisturb;
    int NoDisturbStartHour;
    int NoDisturbEndHour;
    easemob::EMPushConfigs::EMPushDisplayStyle Style;
    
    static PushConfigTO * FromEMPushConfig(EMPushConfigsPtr&  pushConfigPtr);
};

enum UserInfoType {
    NICKNAME    = 0,
    AVATAR_URL  = 1,
    EMAIL       = 2,
    PHONE       = 3,
    GENDER      = 4,
    SIGN        = 5,
    BIRTH       = 6,
    EXT         = 100
};

struct UserInfoTO
{
    const char* nickName;
    const char* avatarUrl;
    const char* email;
    const char* phoneNumber;
    const char* signature;
    const char* birth;
    const char* userId;
    const char* ext;
    int         gender = 0;
};

struct UserInfo
{
    std::string nickName;
    std::string avatarUrl;
    std::string email;
    std::string phoneNumber;
    std::string signature;
    std::string birth;
    std::string userId;
    std::string ext;
    int         gender = 0;
    
    static std::map<std::string, UserInfo> FromResponse(std::string json, std::map<UserInfoType, std::string>& utypeMap);
    static std::map<std::string, UserInfoTO> Convert2TO(std::map<std::string, UserInfo>& userInfoMap);
};

struct SupportLanguagesTO
{
    const char* languageCode;
    const char* languageName;
    const char* languageNativeName;
};

struct PresenceTO
{
    const char* publisher;
    const char* deviceList; // json string
    const char* statusList; // json string
    const char* ext;
    int64_t     latestTime;
    int64_t     expiryTime;
};

struct PresenceTOWrapper
{
    PresenceTO presenceTO;
    std::string deviceListJson;
    std::string statusListJson;
    std::string ext;
    
    static PresenceTOWrapper FromPresence(EMPresencePtr presencePtr);
};

struct TOArray
{
    DataType Type;
    int Size;
    void * Data[ARRAY_SIZE_LIMITATION]; //list of data
};

struct TOArrayDiff
{
    int Size;
    void * Data[ARRAY_SIZE_LIMITATION];
    int Type[ARRAY_SIZE_LIMITATION];
};

struct TOItem
{
    int Type;
    void * Data;
    
    TOItem() { Type = 0; Data = nullptr; }
    TOItem(int type, void* data): Type(type), Data(data){}
};

#endif //_MODELS_H_
