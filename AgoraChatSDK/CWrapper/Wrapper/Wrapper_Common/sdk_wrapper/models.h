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
#include "emattributevalue.h"
#include "emchatconfigs.h"

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
		static string ToJson(EMMessagePtr em_msg);

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
		static EMMessageBodyPtr BodyFromJson(std::string json);

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
}

#endif