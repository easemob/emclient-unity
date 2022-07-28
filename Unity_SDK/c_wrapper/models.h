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
#include "emmessagereaction.h"
#include "emmessagereactionchange.h"
#include "emthreadmanager_interface.h"
#include "json.hpp"

#ifndef RAPIDJSON_NAMESPACE
#define RAPIDJSON_NAMESPACE easemob
#endif
#include "rapidjson/document.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson/prettywriter.h"

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


struct GroupReadAck
{
    char * AckId;
    char * MsgId;
    char * From;
    char * Content;
    int64_t Timestamp;
    int Count;
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
    const char* ReactionList;

    bool IsThread;
    const char* MucParentId;
    const char* MsgParentId;
    const char* ThreadOverview;
    
    EMMessageBody::EMMessageBodyType BodyType;
    MessageTO();
    
    static MessageTO * FromEMMessage(const EMMessagePtr &_message);
    //since can not use destory function
    //so here add a function to free related resource
    static void FreeResource(MessageTO * mto);
    //virtual ~MessageTO();

    static int MsgTypeToInt(EMMessage::EMChatType type);
    static EMMessage::EMChatType MsgTypeFromInt(int i);

    static int StatusToInt(EMMessage::EMMessageStatus status);
    static EMMessage::EMMessageStatus StatusFromInt(int i);

    static std::string MessageDirectionToString(EMMessage::EMMessageDirection direction);
    static EMMessage::EMMessageDirection MessageDirectionFromString(std::string str);

    static std::string BodyTypeToString(EMMessageBody::EMMessageBodyType btype);
    static EMMessageBody::EMMessageBodyType BodyTypeFromString(std::string str);

    static int DownLoadStatusToInt(EMFileMessageBody::EMDownloadStatus download_status);
    static EMFileMessageBody::EMDownloadStatus DownLoadStatusFromInt(int i);

    static std::string CustomExtsToJson(EMCustomMessageBody::EMCustomExts& exts);
    static EMCustomMessageBody::EMCustomExts CustomExtsFromJson(std::string json);

    static void BodyToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
    static std::string BodyToJson(EMMessagePtr msg);
    static void ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
    static std::string ToJson(EMMessagePtr msg);

    static EMMessageBodyPtr BodyFromJsonObject(const Value& jnode);
    static EMMessageBodyPtr BodyFromJson(std::string json);
    static EMMessagePtr FromJsonObject(const Value& jnode);
    static EMMessagePtr FromJson(std::string json);

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
    std::string publisher;
    std::string deviceListJson;
    std::string statusListJson;
    std::string ext;
    int64_t     latestTime;
    int64_t     expiryTime;
    
    void FromLocalWrapper();
    static PresenceTOWrapper FromPresence(EMPresencePtr presencePtr);
};

struct MessageReactionTO
{
    static std::string ToJson(EMMessageReactionPtr reaction);
    static std::string ToJson(EMMessageReactionList list);
    static std::string ToJson(EMMessage& msg);
    static std::string ToJson(std::map<std::string, EMMessageReactionList> map);
    static void ListToJsonWriter(Writer<StringBuffer>& writer, EMMessageReactionList list);

    static EMMessageReactionPtr FromJsonObject(const Value& jnode);
    static EMMessageReactionList ListFromJsonObject(const Value& jnode);
    static EMMessageReactionList ListFromJson(std::string json);

};

struct MessageReactionChangeTO
{
    static std::string ToJson(EMMessageReactionChangePtr reactionChangePtr, std::string curname);
    static std::string ToJson(EMMessageReactionChangeList list, std::string curname);
    static void ToJsonWriter(Writer<StringBuffer>& writer, EMMessageReactionChangePtr reactionChangePtr, std::string curname);
};

struct SilentModeParamTO
{
    static EMSilentModeParamPtr FromJson(std::string json);
};

struct SilentModeItemTO
{
    static std::string ToJson(EMSilentModeItemPtr itemPtr);
    static std::string ToJson(std::map<std::string, EMSilentModeItemPtr> map);
    static void ToJsonWriter(Writer<StringBuffer>& writer, EMSilentModeItemPtr itemPtr);
};

struct AttributesValueTO
{
    static void ToJsonWriter(Writer<StringBuffer>& writer, EMAttributeValuePtr attribute);
    static void ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
    static std::string ToJson(EMMessagePtr msg);

    static void SetMessageAttr(EMMessagePtr msg, std::string& key, const Value& jnode);
    static void SetMessageAttrs(EMMessagePtr msg, const Value& jnode);
    static void SetMessageAttrs(EMMessagePtr msg, std::string json);
};

struct ChatThreadEvent
{
    static std::string ToJson(EMThreadEventPtr threadEventPtr);
    static int ThreadOperationToInt(const std::string& operation );
};

struct ChatThread
{
    static void ToJsonWriter(Writer<StringBuffer>& writer, EMThreadEventPtr threadEventPtr);
    static std::string ToJson(EMThreadEventPtr threadEventPtr);
    static std::string ToJson(EMCursorResultRaw<EMThreadEventPtr> cusorResult);
    static std::string ToJson(std::vector<EMThreadEventPtr>& vec);
    static std::string ToJson(std::map<std::string, EMMessagePtr> map);
    static EMThreadEventPtr FromJsonObject(const Value& jnode);
    static EMThreadEventPtr FromJson(std::string json);

    static EMThreadLeaveReason ThreadLeaveReasonFromInt(int i);
    static int ThreadLeaveReasonToInt(EMThreadLeaveReason reason);
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
