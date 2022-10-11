#ifndef _SDK_WRAPPER_MODELS_IMPL_
#define _SDK_WRAPPER_MODELS_IMPL_

#include <string>

#include "message/emmessage.h"
#include "message/emfilemessagebody.h"
#include "message/emcustommessagebody.h"
#include "emchatconfigs.h"

#include "sdk_wrapper_internal.h"

namespace sdk_wrapper {

	class Options
	{
	public:
		static int FromJson(const char* json, EMChatConfigsPtr config);
	};

	class Message
	{
	public:
		static int FromJson(const char* json, EMMessagePtr em_msg);
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
		static void ToJsonWriter(Writer<StringBuffer>& writer, EMMessagePtr msg);
		static string ToJson(EMMessagePtr msg);

		static EMMessageBodyPtr BodyFromJsonObject(const Value& jnode);
		static EMMessageBodyPtr BodyFromJson(std::string json);
		static EMMessagePtr FromJsonObject(const Value& jnode);
		static EMMessagePtr FromJson(std::string json);
	};
}

#endif