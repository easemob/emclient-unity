#ifndef _SDK_WRAPPER_MODELS_IMPL_
#define _SDK_WRAPPER_MODELS_IMPL_

#include "message/emmessage.h"
#include "message/emfilemessagebody.h"
#include "message/emcustommessagebody.h"
#include "message/emtextmessagebody.h"
#include "message/emlocationmessagebody.h"
#include "message/emcmdmessagebody.h"
#include "message/emimagemessagebody.h"
#include "message/emvideomessagebody.h"
#include "message/emvoicemessagebody.h"
#include "message/emcombinemessagebody.h"
#include "message/emmessageencoder.h"
#include "emgroupmanager_interface.h"
#include "emmessagereactionchange.h"
#include "emchatroommanager_interface.h"

#include "emattributevalue.h"
#include "emchatconfigs.h"
#include "emconversation.h"
#include "emmuc.h"
#include "emthreadmanager_listener.h"
#include "empresence.h"
#include "emmultidevices_listener.h"
#include "emfetchmessageoption.h"

#include "sdk_wrapper_internal.h"

namespace sdk_wrapper {

	class MyJson
	{
	public:
		static string ToJsonWithJsonObject(const Value& obj);

		static string ToJsonWithResult(const char* cbid, int process, int code, const char* desc, const char* jstr);
		static string ToJsonWithError(const char* cbid, int code, const char* desc);
		static string ToJsonWithSuccess(const char* cbid);
		static string ToJsonWithErrorResult(const char* cbid, int code, const char* desc, const char* jstr);
		static string ToJsonWithSuccessResult(const char* cbid, const char* jstr);
		static string ToJsonWithProcess(const char* cbid, int process);

        static void ToJsonObject(Writer<StringBuffer>& writer, const vector<int>& vec);

		static void ToJsonObject(Writer<StringBuffer>& writer, const vector<string>& vec);
		static vector<string> FromJsonObjectToVector(const Value& jnode, bool filter = false);

		static string ToJson(const vector<string>& vec);
		static vector<string> FromJsonToVector(string& jstr);

		static void ToJsonObject(Writer<StringBuffer>& writer, const map<string, string>& map);
        static void ToJsonObject(Writer<StringBuffer>& writer, const unordered_map<string, string>& map);
		static void ToJsonObject(Writer<StringBuffer>& writer, const map<string, int>& map);
		static map<string, string> FromJsonObjectToMap(const Value& jnode);
        static unordered_map<string, string> FromJsonObjectToUnorderedMap(const Value& jnode);
		//static map<string, int> FromJsonObjectToIntMap(const Value& jnode);

		static string ToJson(const map<string, string>& map);
        static string ToJson(const unordered_map<string, string>& map);
		static string ToJson(const map<string, int>& map);
		static map<string, string> FromJsonToMap(const string& jstr);
        static unordered_map<string, string> FromJsonToUnorderedMap(const string& jstr);
		//static map<string, int> FromJsonToIntMap(const string& jstr);

	};

	class Options
	{
	public:
		static EMChatConfigsPtr FromJson(const char* json, const char* rs, const char* wk);
	};

	class Message
	{
	public:
		static EMMessagePtr FromJsonToMessage(const char* json);
		static EMMessageList FromJsonToMessageList(const char* json);

		static string ToJson(EMMessagePtr em_msg);
		static string ToJson(EMMessageList messages);

	public:
		static int MsgTypeToInt(EMMessage::EMChatType type);
		static EMMessage::EMChatType MsgTypeFromInt(int i);

		static int StatusToInt(EMMessage::EMMessageStatus status);
		static EMMessage::EMMessageStatus StatusFromInt(int i);

		static int MessageDirectionToInt(EMMessage::EMMessageDirection direction);
		static EMMessage::EMMessageDirection MessageDirectionFromInt(int i);

		static int BodyTypeToInt(EMMessageBody::EMMessageBodyType btype);
		static EMMessageBody::EMMessageBodyType BodyTypeFromInt(int i);

		static int DownLoadStatusToInt(EMFileMessageBody::EMDownloadStatus download_status);
		static EMFileMessageBody::EMDownloadStatus DownLoadStatusFromInt(int i);

		static string ToJson(EMCustomMessageBody::EMCustomExts& exts);
		static EMCustomMessageBody::EMCustomExts FromJsonToCustomExts(string json);

		static void ToJsonObject(Writer<StringBuffer>& writer, EMCustomMessageBody::EMCustomExts& exts);
		static EMCustomMessageBody::EMCustomExts FromJsonObjectToCustomExts(const Value& jnode);

		static void ToJsonObjectWithBody(Writer<StringBuffer>& writer, EMMessagePtr msg);
		static string ToJsonWithBody(EMMessagePtr msg);

		static EMMessageBodyPtr FromJsonObjectToBody(const Value& jnode);
		static EMMessageBodyPtr FromJsonToBody(string json);

		static void ToJsonObjectWithMessage(Writer<StringBuffer>& writer, EMMessagePtr msg);
		static EMMessagePtr FromJsonObjectToMessage(const Value& jnode);

		static void ToJsonObjectWithMessageList(Writer<StringBuffer>& writer, EMMessageList messages);
	};

	class AttributesValue
	{
	public:
		static void ToJsonObjectWithAttribute(Writer<StringBuffer>& writer, EMAttributeValuePtr attribute);
		static void ToJsonObjectWithAttributes(Writer<StringBuffer>& writer, EMMessagePtr msg);
		static string ToJson(EMMessagePtr msg);

		static void SetMessageAttr(EMMessagePtr msg, string& key, const Value& jnode);
		static void SetMessageAttrs(EMMessagePtr msg, const Value& jnode);
		static void SetMessageAttrs(EMMessagePtr msg, string json);
	};

	class Conversation
	{
	public:
		static string ToJson(EMConversationPtr conversation);
		static string ToJson(EMConversationList conversations);
		static void ToJsonObject(Writer<StringBuffer>& writer, EMConversationPtr conversation);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMConversationList& conversations);

		static int ConversationTypeToInt(EMConversation::EMConversationType type);
		static EMConversation::EMConversationType ConversationTypeFromInt(int i);

		static int EMMessageSearchDirectionToInt(EMConversation::EMMessageSearchDirection direction);
		static EMConversation::EMMessageSearchDirection EMMessageSearchDirectionFromInt(int i);

        static int EMMessageSearchScopeToInt(EMConversation::EMMessageSearchScope scope);
        static EMConversation::EMMessageSearchScope EMMessageSearchScopeFromInt(int i);
	};

	class SupportLanguage
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, tuple<string, string, string>& lang);
		static void ToJsonObject(Writer<StringBuffer>& writer, vector<tuple<string, string, string>>& langs);

		static string ToJson(tuple<string, string, string>& lang);
		static string ToJson(vector<tuple<string, string, string>>& langs);
	};

	class GroupReadAck
	{
	public:
		static string ToJson(EMGroupReadAckPtr group_read_ack);
		static string ToJson(const vector<EMGroupReadAckPtr>& group_read_ack_vec);

		static void ToJsonObject(Writer<StringBuffer>& writer, EMGroupReadAckPtr group_read_ack);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMGroupReadAckList& group_read_ack_vec);
	};

	class MessageReaction
	{
	public:
		static string ToJson(EMMessageReactionPtr reaction);
		static string ToJson(EMMessageReactionList& list);
		static string ToJson(EMMessagePtr msg);
		static string ToJson(map<string, EMMessageReactionList>& map);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionPtr reaction);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionList& list);

		static EMMessageReactionPtr FromJsonObjectToReaction(const Value& jnode);
		static EMMessageReactionList FromJsonObjectToReactionList(const Value& jnode);
		static EMMessageReactionList FromJsonToReactionList(string json);
	};

	struct MessageReactionChange
	{
		static string ToJson(EMMessageReactionChangePtr reactionChangePtr, std::string curname);
		static string ToJson(EMMessageReactionChangeList list, std::string curname);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionChangePtr reactionChangePtr, std::string curname);
	};

    struct MessageReactionOperation
    {
        static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionOperationPtr reactionOperationPtr);
        static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionOperationList list);
    };

	class Group
	{
	public:
		static string ToJson(EMGroupPtr group);
		static string ToJson(const EMMucMuteList& vec);
		static string ToJson(const EMGroupList& list);
        static string ToJson(const unordered_map<string, unordered_map<string, string>>& map);

		static void ToJsonObjectWithGroupInfo(Writer<StringBuffer>& writer, EMGroupPtr group);
		static void ToJsonObject(Writer<StringBuffer>& writer, EMGroupPtr group);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMucMuteList& vec);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMGroupList& list);
        static void ToJsonObject(Writer<StringBuffer>& writer, const unordered_map<string, unordered_map<string, string>>& map);

		static string ToJson(const EMMucSettingPtr setting);
		static EMMucSettingPtr FromJsonToMucSetting(string json);

		static void JsonObject(Writer<StringBuffer>& writer, const EMMucSettingPtr setting);
		static EMMucSettingPtr JsonObjectToMucSetting(const Value& jnode);

		static int MemberTypeToInt(EMMuc::EMMucMemberType type);

		static int GroupStyleToInt(EMMucSetting::EMMucStyle style);
		static EMMucSetting::EMMucStyle GroupStyleFromInt(int i);

        static bool IsMemberOnly(EMMucSetting::EMMucStyle style);
        static bool IsMemberAllowToInvite(EMMucSetting::EMMucStyle style);
	};

	class GroupSharedFile
	{
	public:
		static string ToJson(EMMucSharedFilePtr file);
		static string ToJson(EMMucSharedFileList file_list);

		static void ToJsonObject(Writer<StringBuffer>& writer, EMMucSharedFilePtr file);
		static void ToJsonObject(Writer<StringBuffer>& writer, EMMucSharedFileList file_list);
	};

	class CursorResult
	{
	public:
		static string ToJson(string cursor, const vector<string> list);
		static string ToJson(string cursor, const EMMessageList& msg_list);
		static string ToJson(string cursor, const EMGroupReadAckList& group_ack_list);
		static string ToJson(string cursor, const EMMessageReactionPtr reaction);
		static string ToJsonWithGroupInfo(string cursor, const EMCursorResult& result);
		static string ToJson(string cursor, const EMCursorResultRaw<EMThreadEventPtr> cusorResult);

        template<typename ResultType, typename ConvertType>
        static string ToJson(string cursor, const vector<ResultType>& cusorResult)
        {
            StringBuffer s;
            Writer<StringBuffer> writer(s);

            writer.StartObject();
            {
                writer.Key("cursor");
                writer.String(cursor.c_str());

                writer.Key("list");

                writer.StartArray();
                for (size_t i = 0; i < cusorResult.size(); i++) {
                    ConvertType::ToJsonObject(writer, cusorResult[i]);
                }
                writer.EndArray();
            }
            writer.EndObject();

            std::string data = s.GetString();
            return data;
        }
	};

	class Room
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMChatroomPtr room);
		static string ToJson(const EMChatroomPtr room);

		static void ToJsonObject(Writer<StringBuffer>& writer, const EMChatroomList room_list);
		static string ToJson(const EMChatroomList room_list);

		static string ToJsonFromRoomAttribute(const map<string, string>& kvs, bool deleteWhenExit);
		static map<string, string> FromJsonToRoomSuccessAttribute(const string& json);
		static vector<string> FromJsonToRoomSuccessKeys(const string& json);

		static int EMMucMemberTypeToInt(EMMuc::EMMucMemberType type);
		static EMMuc::EMMucMemberType EMMucMemberTypeFromInt(int i);
	};

	class PageResult
	{
	public:
		static string ToJsonWithRoom(int count, const EMPageResult result);
	};

	class ChatThreadEvent
	{
	public:
		static std::string ToJson(EMThreadEventPtr threadEventPtr);

		static int ThreadOperationToInt(const std::string& operation);
	};

	class ChatThread
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, EMThreadEventPtr threadEventPtr);
		static std::string ToJson(EMThreadEventPtr threadEventPtr);
		static std::string ToJson(EMCursorResultRaw<EMThreadEventPtr> cusorResult);
		static std::string ToJson(std::vector<EMThreadEventPtr>& vec);
		static std::string ToJson(std::map<std::string, EMMessagePtr> map);
		static EMThreadEventPtr FromJsonObject(const Value& jnode);
		static EMThreadEventPtr FromJson(std::string json);

		static EMThreadLeaveReason ThreadLeaveReasonFromInt(int i);
		static int ThreadLeaveReasonToInt(EMThreadLeaveReason reason);
	};

	class PreseceDeviceStatus
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, const pair<string, int>& status);
		static void ToJsonObject(Writer<StringBuffer>& writer, const set<pair<string, int>>& status_set);

		static string ToJson(const pair<string, int>& status);
		static string ToJson(const set<pair<string, int>>& status_set);
	};

	class Presence
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, EMPresencePtr presence);
		static void ToJsonObject(Writer<StringBuffer>& writer, const vector<EMPresencePtr>& vec);

		static string ToJson(EMPresencePtr presence);
		static string ToJson(const vector<EMPresencePtr>& vec);
	};

	class MultiDevices
	{
	public:
		static int MultiDevicesOperationToInt(EMMultiDevicesListener::MultiDevicesOperation operation);
		static EMMultiDevicesListener::MultiDevicesOperation MultiDevicesOperationFromInt(int i);
	};

	class DeviceInfo
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMDeviceInfoPtr di);
		static string ToJson(const EMDeviceInfoPtr di);

		static void ToJsonObject(Writer<StringBuffer>& writer, const vector<EMDeviceInfoPtr> vec);
		static string ToJson(const vector<EMDeviceInfoPtr> vec);
	};

    class Contact
    {
    public:
        static EMContactPtr FromJson(const char* json);

        static void ToJsonObject(Writer<StringBuffer>& writer, const EMContactPtr contact);
        static string ToJson(const EMContactPtr contact);

        static void ToJsonObject(Writer<StringBuffer>& writer, const vector<EMContactPtr> vec);
        static string ToJson(const vector<EMContactPtr> vec);
    };

    struct FetchMessageOption
    {
        static EMFetchMessageOptionPtr FromJsonObject(const Value& jnode);
        static EMFetchMessageOptionPtr FromJson(std::string json);

        static vector<EMMessageBody::EMMessageBodyType> FromJsonObjectToBodyTypeVector(const Value& jnode);
    };

	struct UserInfo
	{
	public:
		string nickName;
		string avatarUrl;
		string email;
		string phoneNumber;
		string signature;
		string birth;
		string userId;
		string ext;
		int gender;

		UserInfo();

		static void ToJsonObject(Writer<StringBuffer>& writer, UserInfo& ui);
		static void ToJsonObject(Writer<StringBuffer>& writer, map<string, UserInfo>& uis);
		static UserInfo FromJsonObject(const Value& jnode);

		static string ToJson(UserInfo& ui);
		static string ToJson(map<string, UserInfo>& uis);
		static UserInfo FromJson(string json);

		static void ToJsonObjectForServer(Writer<StringBuffer>& writer, UserInfo& ui);
		static map<string, UserInfo> FromJsonObjectFromServer(const Value& jnode);

		static string ToJsonForServer(UserInfo& ui);
		static map<string, UserInfo> FromJsonFromServer(string json);
	};

    class PinnedInfo
    {
    public:
        static void ToJsonObject(Writer<StringBuffer>& writer, bool isPinned, const string& operatorId, int64_t ts);
    };

    class TokenWrapper
    {
    public:
        TokenWrapper();
        ~TokenWrapper();

        static TokenWrapper* GetInstance();
        static void TimeFunc(int signo);

        int GetTokenCheckInterval();
        void AdjustLastCheckInterval(int64_t remain_ts);
        void onTokenNotification(int errCode, int64_t remain_ts);

        void SetAndStartTokenCheckTimer(int64_t expireTS, int interval);
        void StopTokenCheckTimer();

        void TokenCheck();

        int64_t expireTS_; //second
        int64_t availablePeriod_; //second
        int check_interval_;

    private:
        static TokenWrapper* instance_;
    };

}

#endif