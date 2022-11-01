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

#include "emattributevalue.h"
#include "emchatconfigs.h"
#include "emconversation.h"

#include "sdk_wrapper_internal.h"

namespace sdk_wrapper {

	class Options
	{
	public:
		static EMChatConfigsPtr FromJson(const char* json, const char* rs, const char* wk);
	};

	class Message
	{
	public:
		static EMMessagePtr FromJson(const char* json);
		static EMMessageList ListFromJson(const char* json);
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

		static string CustomExtsToJson(EMCustomMessageBody::EMCustomExts& exts);
		static EMCustomMessageBody::EMCustomExts CustomExtsFromJson(string json);

		static void BodyToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
		static string BodyToJson(EMMessagePtr msg);

		static EMMessageBodyPtr BodyFromJsonObject(const Value& jnode);
		static EMMessageBodyPtr BodyFromJson(string json);

		static void ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
		static EMMessagePtr FromJsonObject(const Value& jnode);
	};

	class AttributesValue
	{
	public:
		static void ToJsonWriter(Writer<StringBuffer>& writer, EMAttributeValuePtr attribute);
		static void ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
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
		static int ConversationTypeToInt(EMConversation::EMConversationType type);
	};

	class SupportLanguage
	{
	public:
		static string ToJson(tuple<string, string, string>& lang);
		static string ToJson(vector<tuple<string, string, string>>& langs);
	};

	class GroupReadAck
	{
	public:
		static string ToJson(EMGroupReadAckPtr group_read_ack);
		static string ToJson(vector<EMGroupReadAckPtr> group_read_ack_vec);
	};

	class MessageReaction
	{
	public:
		static string ToJson(EMMessageReactionPtr reaction);
		static string ToJson(EMMessageReactionList& list);
		static string ToJson(EMMessage& msg);
		static string ToJson(map<string, EMMessageReactionList>& map);
		static void ListToJsonWriter(Writer<StringBuffer>& writer, EMMessageReactionList& list);

		static EMMessageReactionPtr FromJsonObject(const Value& jnode);
		static EMMessageReactionList ListFromJsonObject(const Value& jnode);
		static EMMessageReactionList ListFromJson(string json);
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