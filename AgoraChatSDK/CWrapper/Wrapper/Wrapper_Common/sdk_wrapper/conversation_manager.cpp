#include "emclient.h"
#include "emchatmanager_interface.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

namespace sdk_wrapper {

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_AppendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_ClearAllMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_RemoveMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_ExtField(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        std::string json = conversationPtr->extField();

        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_InsertMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LatestMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        EMMessagePtr msgPtr = conversation->latestMessage();

        string json = Message::ToJson(msgPtr);
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LatestMessageFromOthers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        EMMessagePtr msgPtr = conversation->latestMessageFromOthers();

        string json = Message::ToJson(msgPtr);
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LoadMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMConversationPtr conversation = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        EMMessagePtr msgPtr = conversation->loadMessage(msg_id);

        string json = Message::ToJson(msgPtr);
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LoadMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        string start_id = GetJsonValue_String(d, "startId", "");
        int count = GetJsonValue_Int(d, "count", 20);

        string direction_str = GetJsonValue_String(d, "direction", "up");
        EMConversation::EMMessageSearchDirection direction = (direction_str == "up") ? EMConversation::EMMessageSearchDirection::UP : EMConversation::EMMessageSearchDirection::DOWN;

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(start_id, count, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

         });
        t.detach();
    }


    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithKeyword(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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

        string direction_str = GetJsonValue_String(d, "direction", "up");
        EMConversation::EMMessageSearchDirection direction = (direction_str == "up") ? EMConversation::EMMessageSearchDirection::UP : EMConversation::EMMessageSearchDirection::DOWN;

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(keywords, ts, count, sender, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithMsgType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        string type_str = GetJsonValue_String(d, "type", "txt");
        EMMessageBody::EMMessageBodyType body_type = Message::BodyTypeFromString(type_str);

        string sender = GetJsonValue_String(d, "sender", "");

        int count = GetJsonValue_Int(d, "count", 20);

        string timestamp = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atol(timestamp.c_str());

        string direction_str = GetJsonValue_String(d, "direction", "up");
        EMConversation::EMMessageSearchDirection direction = (direction_str == "up") ? EMConversation::EMMessageSearchDirection::UP : EMConversation::EMMessageSearchDirection::DOWN;

        thread t([=]() {
            EMError error;
            EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
            EMMessageList msgList = conversationPtr->loadMoreMessages(body_type, ts, count, sender, direction);

            string json = Message::ToJson(msgList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_LoadMessagesWithTime(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        string start_ts_str = GetJsonValue_String(d, "startTime", "0");
        int64_t start_ts = atol(start_ts_str.c_str());

        string end_ts_str = GetJsonValue_String(d, "endTime", "0");
        int64_t end_ts = atol(end_ts_str.c_str());

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_MarkAllMessagesAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        conversationPtr->markAllMessagesAsRead(true);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_MarkMessageAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        conversationPtr->markMessageAsRead(msg_id,true);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_SetExtField(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);
        string ext = GetJsonValue_String(d, "ext", "");

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        conversationPtr->setExtField(ext);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_UnreadMessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        int count = conversationPtr->unreadMessagesCount();

        JSON_STARTOBJ
        writer.Key("count");
        writer.String(to_string(count).c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_MessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        int int_type = GetJsonValue_Int(d, "convType", 0);
        EMConversation::EMConversationType type = Conversation::ConversationTypeFromInt(int_type);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(conv_id, type, true);
        int count = conversationPtr->messagesCount();

        JSON_STARTOBJ
        writer.Key("count");
        writer.String(to_string(count).c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ConversationManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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
        memcpy(buf, json.c_str(), json.size());
    }

}
