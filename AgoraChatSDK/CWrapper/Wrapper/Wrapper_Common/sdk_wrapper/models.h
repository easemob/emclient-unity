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
#include "message/emmessageencoder.h"
#include "emgroupmanager_interface.h"
#include "emmessagereactionchange.h"

#include "emattributevalue.h"
#include "emchatconfigs.h"
#include "emconversation.h"
#include "emmuc.h"
#include "emthreadmanager_listener.h"
#include "empresence.h"

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

		static void ToJsonObject(Writer<StringBuffer>& writer, const vector<string>& vec);
		static vector<string> FromJsonObjectToVector(const Value& jnode);

		static string ToJson(const vector<string>& vec);
		static vector<string> FromJsonToVector(string& jstr);

		static void ToJsonObject(Writer<StringBuffer>& writer, const map<string, string>& map);
		static map<string, string> FromJsonObjectToMap(const Value& jnode);

		static string ToJson(const map<string, string>& map);
		static map<string, string> FromJsonToMap(string& jstr);
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

		static string MessageDirectionToString(EMMessage::EMMessageDirection direction);
		static EMMessage::EMMessageDirection MessageDirectionFromString(string str);

		static string BodyTypeToString(EMMessageBody::EMMessageBodyType btype);
		static EMMessageBody::EMMessageBodyType BodyTypeFromString(string str);

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
		static int ConversationTypeToInt(EMConversation::EMConversationType type);
		static EMConversation::EMConversationType ConversationTypeFromInt(int i);
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
		static string ToJson(EMMessage& msg);
		static string ToJson(map<string, EMMessageReactionList>& map);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionPtr reaction);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMessageReactionList& list);

		static EMMessageReactionPtr FromJsonObjectToReaction(const Value& jnode);
		static EMMessageReactionList FromJsonObjectToReactionList(const Value& jnode);
		static EMMessageReactionList FromJsonToReactionList(string json);
	};

	struct MessageReactionChange
	{
		static std::string ToJson(EMMessageReactionChangePtr reactionChangePtr, std::string curname);
		static std::string ToJson(EMMessageReactionChangeList list, std::string curname);
		static void ToJsonObject(Writer<StringBuffer>& writer, EMMessageReactionChangePtr reactionChangePtr, std::string curname);
	};

	class Group
	{
	public:
		static string ToJson(EMGroupPtr group);
		static string ToJson(const EMMucMuteList& vec);
		static string ToJson(const EMGroupList& list);

		static void ToJsonObject(Writer<StringBuffer>& writer, EMGroupPtr group);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMMucMuteList& vec);
		static void ToJsonObject(Writer<StringBuffer>& writer, const EMGroupList& list);

		static string ToJson(const EMMucSettingPtr setting);
		static EMMucSettingPtr FromJsonToMucSetting(string json);

		static void JsonObject(Writer<StringBuffer>& writer, const EMMucSettingPtr setting);
		static EMMucSettingPtr JsonObjectToMucSetting(const Value& jnode);

		static int MemberTypeToInt(EMMuc::EMMucMemberType type);

		static int GroupStyleToInt(EMMucSetting::EMMucStyle style);
		static EMMucSetting::EMMucStyle GroupStyleFromInt(int i);
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
	};

	class Room
	{
	public:
		static void ToJsonObject(Writer<StringBuffer>& writer, EMChatroomPtr room);
		static string ToJson(EMChatroomPtr room);

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

	class TokenWrapper
	{
		struct AutoLoginConfig {
			string userName;
			string passwd;
			string token;
			string expireTS; // milli-second
			int64_t expireTsInt; //second
			int64_t availablePeriod; //second
		};

	public:
		TokenWrapper();

		bool GetTokenCofigFromJson(string& raw, string& token, string& expireTS);
		int GetTokenCheckInterval(int pre_check_interval,int availablePeriod);

		bool SetTokenInAutoLogin(const string& username, const string& token, const string expireTS);
		void SetPasswdInAutoLogin(const string& username, const string& passwd);

		void SaveAutoLoginConfigToFile(const string& uuid);
		void GetAutoLoginConfigFromFile(const string& uuid);

		AutoLoginConfig autologin_config_;
	};
}

#endif