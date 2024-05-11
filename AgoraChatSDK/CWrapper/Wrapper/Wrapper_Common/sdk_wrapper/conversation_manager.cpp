#include "emclient.h"
#include "emchatmanager_interface.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

namespace sdk_wrapper {

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_AppendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        EMMessagePtr msg = Message::FromJsonObjectToMessage(d["msg"]);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        bool ret = conversation->appendMessage(msg);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_ClearAllMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        bool ret = conversation->clearAllMessages();

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_RemoveMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        bool ret = conversation->removeMessage(msg_id);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_ExtField(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        std::string ext = conversationPtr->extField();

        JSON_STARTOBJ
        writer.Key("ret");
        writer.String(ext.c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_InsertMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        EMMessagePtr msg = Message::FromJsonObjectToMessage(d["msg"]);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        bool ret = conversation->insertMessage(msg);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LatestMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        EMMessagePtr msgPtr = conversation->latestMessage();

        string json = "";

        if (nullptr != msgPtr) {
            JSON_STARTOBJ
            writer.Key("ret");
            Message::ToJsonObjectWithMessage(writer, msgPtr);
            JSON_ENDOBJ
            json = s.GetString();
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LatestMessageFromOthers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        EMMessagePtr msgPtr = conversation->latestMessageFromOthers();

        string json = "";

        if (nullptr != msgPtr) {
            JSON_STARTOBJ
            writer.Key("ret");
            Message::ToJsonObjectWithMessage(writer, msgPtr);
            JSON_ENDOBJ
            json = s.GetString();
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        EMMessagePtr msgPtr = conversation->loadMessage(msg_id);

        string json = "";

        if (nullptr != msgPtr) {
            JSON_STARTOBJ
            writer.Key("ret");
            Message::ToJsonObjectWithMessage(writer, msgPtr);
            JSON_ENDOBJ
            json = s.GetString();
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        string start_id = GetJsonValue_String(d, "startMessageId", "");
        int count = GetJsonValue_Int(d, "count", 20);

        int int_direction = GetJsonValue_Int(d, "direction", 0);
        EMConversation::EMMessageSearchDirection direction = Conversation::EMMessageSearchDirectionFromInt(int_direction);

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(start_id, count, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

         });
        t.detach();

        return nullptr;
    }


    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithKeyword(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        string keywords = GetJsonValue_String(d, "keywords", "");
        string sender = GetJsonValue_String(d, "sender", "");

        int count = GetJsonValue_Int(d, "count", 20);
        string timestamp = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atol(timestamp.c_str());

        int int_direction = GetJsonValue_Int(d, "direction", 0);
        EMConversation::EMMessageSearchDirection direction = Conversation::EMMessageSearchDirectionFromInt(int_direction);

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(keywords, ts, count, sender, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithMsgType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        int int_btype = GetJsonValue_Int(d, "bodyType", 0);
        EMMessageBody::EMMessageBodyType body_type = Message::BodyTypeFromInt(int_btype);

        string sender = GetJsonValue_String(d, "sender", "");

        int count = GetJsonValue_Int(d, "count", 20);

        string timestamp = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atol(timestamp.c_str());

        int int_direction = GetJsonValue_Int(d, "direction", 0);
        EMConversation::EMMessageSearchDirection direction = Conversation::EMMessageSearchDirectionFromInt(int_direction);

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(body_type, ts, count, sender, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithTime(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        int64_t start_ts = GetJsonValue_Int64(d, "startTime", 0);
        int64_t end_ts = GetJsonValue_Int64(d, "endTime", 0);

        int count = GetJsonValue_Int(d, "count", 20);

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(start_ts, end_ts, count);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithScope(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);

        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        string keywords = GetJsonValue_String(d, "keywords", "");
        string from = GetJsonValue_String(d, "from", "");
        int count = GetJsonValue_Int(d, "count", 20);

        string timestamp_str = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atol(timestamp_str.c_str());

        int var_direction = GetJsonValue_Int(d, "direction", 0);
        EMConversation::EMMessageSearchDirection direction = Conversation::EMMessageSearchDirectionFromInt(var_direction);

        int var_scope = GetJsonValue_Int(d, "scope", 0);
        EMConversation::EMMessageSearchScope scope = Conversation::EMMessageSearchScopeFromInt(var_scope);

        thread t([=]() {
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(keywords, scope, ts, count, from, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_MarkAllMessagesAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        conversationPtr->markAllMessagesAsRead(true);

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_MarkMessageAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        conversationPtr->markMessageAsRead(msg_id,true);

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_SetExtField(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        map<string, string> ext_map;
        if (d.HasMember("ext")) {
            ext_map = MyJson::FromJsonObjectToMap(d["ext"]);
        }
        
        string ext = MyJson::ToJson(ext_map);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        conversationPtr->setExtField(ext);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(true);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_UnreadMessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        int count = conversationPtr->unreadMessagesCount();

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Int(count);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_MessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        int count = conversationPtr->messagesCount();

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Int(count);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        EMMessagePtr msg = Message::FromJsonObjectToMessage(d["msg"]);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        bool ret = conversation->updateMessage(msg);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_RemoveMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        int64_t start_time = GetJsonValue_Int64(d, "startTime", 0);
        int64_t endTime = GetJsonValue_Int64(d, "endTime", 0);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        bool ret = conversation->removeMessages(start_time, endTime);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_PinnedMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);

        EMMessageList messageList = conversation->pinnedMessages();

        string json = "";

        if (messageList.size() > 0) {
            JSON_STARTOBJ
            writer.Key("ret");
            Message::ToJsonObjectWithMessageList(writer, messageList);
            JSON_ENDOBJ
            json = s.GetString();
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ConversationManager_Marks(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        uint64_t markValue = (uint64_t)conversation->markValue();

        std::vector<int> marks;
        for (int offset = 0; markValue > 0; ++offset) {
            if (markValue & 1) {
                marks.push_back(offset);
            }
            markValue = markValue >> 1;
        }

        JSON_STARTOBJ
        writer.Key("ret");
        MyJson::ToJsonObject(writer, marks);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }
}
