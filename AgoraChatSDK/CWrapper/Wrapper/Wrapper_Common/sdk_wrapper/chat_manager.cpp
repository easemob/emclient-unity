
#include <mutex>

#include "message/emmessage.h"
#include "emclient.h"
#include "emchatmanager_interface.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

static EMCallbackObserverHandle gCallbackObserverHandle;

EMChatManagerListener* gChatManagerListener = nullptr;
EMReactionManagerListener* gReactionManagerListener = nullptr;

mutex msg_locker;
map<string, EMMessagePtr> msg_ptr_map;

mutex progress_msg_locker;
map<string, int> progress_map;

namespace sdk_wrapper {

    void AddMsgItem(string msgId, EMMessagePtr msgPtr)
    {
        lock_guard<mutex> maplocker(msg_locker);
        msg_ptr_map[msgId] = msgPtr;
    }

    void DeleteMsgItem(string msgId)
    {
        lock_guard<mutex> maplocker(msg_locker);

        auto itPtr = msg_ptr_map.find(msgId);
        if (msg_ptr_map.end() != itPtr) {
            msg_ptr_map.erase(itPtr);
        }
    }

    string JsonStringFromUpdatedMessage(string msgId)
    {
        EMMessagePtr update_msg = nullptr;
        {
            lock_guard<mutex> maplocker(msg_locker);

            auto it = msg_ptr_map.find(msgId);
            if (msg_ptr_map.end() == it) {
                return string();
            }

            update_msg = it->second;
        }

        if (nullptr != update_msg) {

            JSON_STARTOBJ
            writer.Key("ret");
            Message::ToJsonObjectWithMessage(writer, update_msg);
            JSON_ENDOBJ

            std::string data = s.GetString();
            return data;
        }
        else {
            return string();
        }
    }

    void AddProgressItem(string msgId)
    {
        lock_guard<mutex> maplocker(progress_msg_locker);
        progress_map[msgId] = 0;
    }

    void DeleteProgressItem(string msgId)
    {
        lock_guard<mutex> maplocker(progress_msg_locker);
        auto it = progress_map.find(msgId);
        if (progress_map.end() != it) {
            progress_map.erase(it);
        }
    }

    void UpdateProgressMap(string msgId, int progress)
    {
        lock_guard<mutex> maplocker(progress_msg_locker);

        auto it = progress_map.find(msgId);
        if (progress_map.end() == it) {
            return;
        }
        it->second = progress;
    }

    int GetLastProgress(string msgId)
    {
        lock_guard<mutex> maplocker(progress_msg_locker);
        auto it = progress_map.find(msgId);
        if (progress_map.end() == it) {
            return 0;
        }
        return it->second;
    }

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_AddListener()
	{
		if (!CheckClientInitOrNot(nullptr)) return;

        if (nullptr == gChatManagerListener) {
            gChatManagerListener = new ChatManagerListener();
            CLIENT->getChatManager().addListener(gChatManagerListener);
        }

        if (nullptr == gReactionManagerListener) {
            gReactionManagerListener = new ReactionManagerListener();
            CLIENT->getReactionManager().addListener(gReactionManagerListener);
        }
	}

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        CLIENT->getChatManager().clearListeners();
        if (nullptr != gChatManagerListener) {
            delete gChatManagerListener;
            gChatManagerListener = nullptr;
        }
        if (nullptr != gReactionManagerListener) {
            delete gReactionManagerListener;
            gReactionManagerListener = nullptr;
        }
    }

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendMessage(const char* jstr, const char* cbid, char* buf)
	{
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        EMMessagePtr message_ptr = Message::FromJsonToMessage(jstr);
        string msg_id = message_ptr->msgId();
        string local_cbid = cbid;

        AddMsgItem(msg_id, message_ptr);
        AddProgressItem(msg_id);

        EMCallbackPtr callback_ptr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string update_msg_json = JsonStringFromUpdatedMessage(msg_id);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), update_msg_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteMsgItem(msg_id);
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const EMErrorPtr error)->bool {
                string update_msg_json = JsonStringFromUpdatedMessage(msg_id);
                string call_back_jstr = MyJson::ToJsonWithErrorResult(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str(), update_msg_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteMsgItem(msg_id);
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
            int last_progress = GetLastProgress(msg_id);
            if (progress - last_progress >= 5) {
                string call_back_jstr = MyJson::ToJsonWithProcess(local_cbid.c_str(), progress);
                CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                UpdateProgressMap(msg_id, progress);
            }
            return;
            }));

        message_ptr->setCallback(callback_ptr);
        CLIENT->getChatManager().sendMessage(message_ptr);

        string updated_msg_json = JsonStringFromUpdatedMessage(msg_id);
        return CopyToPointer(updated_msg_json);
	}

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        bool delete_messages = GetJsonValue_Bool(d, "deleteMessages", true);

        CLIENT->getChatManager().removeConversation(conv_id, delete_messages, false);

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DownloadMessageAttachments(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string local_cbid = cbid;

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return nullptr;
        }

        AddProgressItem(msg_id);

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
                int last_progress = GetLastProgress(msg_id);
                if (progress - last_progress >= 5) {
                    string call_back_jstr = MyJson::ToJsonWithProcess(local_cbid.c_str(), progress);
                    CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                    UpdateProgressMap(msg_id, progress);
                }
                return;
            }
            ));
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().downloadMessageAttachments(messagePtr);

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DownloadMessageThumbnail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string local_cbid = cbid;

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return nullptr;
        }

        AddProgressItem(msg_id);

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
                int last_progress = GetLastProgress(msg_id);
                if (progress - last_progress >= 5) {
                    string call_back_jstr = MyJson::ToJsonWithProcess(local_cbid.c_str(), progress);
                    CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                    UpdateProgressMap(msg_id, progress);
                }
                return;
            }
            ));
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().downloadMessageThumbnail(messagePtr);

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchHistoryMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        string start_msg_id = GetJsonValue_String(d, "startMsgId", "");

        int var_direction = GetJsonValue_Int(d, "direction", 0);
        EMConversation::EMMessageSearchDirection direction = Conversation::EMMessageSearchDirectionFromInt(var_direction);

        int count = GetJsonValue_Int(d, "count", 20);

        thread t([=]() {
            EMError error;
            EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(cov_id, conv_type, error, start_msg_id, count, direction);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string cursor = msgCursorResult.nextPageCursor();
                string cursor_json = CursorResult::ToJson(cursor, msgCursorResult.result());
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), cursor_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchHistoryMessagesBy(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        string cursor = GetJsonValue_String(d, "cursor", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 1);

        EMFetchMessageOptionPtr option = nullptr;
        if (d.HasMember("options") && d["options"].IsObject()) {
            option = FetchMessageOption::FromJsonObject(d["options"]);
        }

        thread t([=]() {
            EMError error;
            EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(cov_id, conv_type, error, page_size, cursor, option);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string next_cursor = msgCursorResult.nextPageCursor();
                string cursor_json = CursorResult::ToJson(next_cursor, msgCursorResult.result());
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), cursor_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ConversationWithType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        bool create_if_need = GetJsonValue_Bool(d, "createIfNeed", true);
        bool is_thread = GetJsonValue_Bool(d, "isThread", false);

        string json = "";

        JSON_STARTOBJ
        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(cov_id, conv_type, create_if_need, is_thread);
        if (nullptr != conversationPtr) {
            json = Conversation::ToJson(conversationPtr);
            writer.Key("ret");
            Conversation::ToJsonObject(writer, conversationPtr);
        }
        JSON_ENDOBJ

        json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        thread t([=]() {
            EMError error;
            EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = Conversation::ToJson(conversationList);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServerWithCursor(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        bool pinOnly = GetJsonValue_Bool(d, "pinOnly", false);
        string cursor = GetJsonValue_String(d, "cursor", "");
        int limit = GetJsonValue_Int(d, "limit", 10);

        thread t([=]() {
            EMError error;
            EMCursorResultRaw<EMConversationPtr> result = CLIENT->getChatManager().getConversationsFromServer(pinOnly, cursor, limit, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = CursorResult::ToJson<EMConversationPtr, Conversation>(result.nextPageCursor(), result.result());
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServerWithCursorAndMark(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        bool needMark = GetJsonValue_Bool(d, "needMark", false);
        int mark = GetJsonValue_Int(d, "mark", 0);
        string cursor = GetJsonValue_String(d, "cursor", "");
        int limit = GetJsonValue_Int(d, "limit", 10);

        thread t([=]() {
            EMError error;

            EMConversationFilterPtr filter(new EMConversationFilter(mark, limit, needMark));

            EMCursorResultRaw<EMConversationPtr> result = CLIENT->getChatManager().getConversationsFromServer(cursor, std::move(filter), error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = CursorResult::ToJson<EMConversationPtr, Conversation>(result.nextPageCursor(), result.result());
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetUnreadMessageCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        EMError error;
        int count = 0;

        //get conversations
        EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);

        if (EMError::EM_NO_ERROR == error.mErrorCode) {
            //sum all unread messages in all conversations
            for (size_t i = 0; i < conversationList.size(); i++) {
                count += conversationList[i]->unreadMessagesCount();
            }
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Int(count);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_InsertMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        string local_cbid = cbid;
        EMMessageList list = Message::FromJsonToMessageList(jstr);

        thread t([=]() {

            bool ret = true;
            if (list.size() > 0) {
                ret = CLIENT->getChatManager().insertMessages(list);
            }

            if (true == ret) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                EMError error(EMError::DATABASE_ERROR);
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_LoadAllConversationsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        Document d; d.Parse(jstr);
        bool is_sort = GetJsonValue_Bool(d, "isSort", true);

        EMConversationList conversationList = CLIENT->getChatManager().getConversations(is_sort);

        JSON_STARTOBJ
        writer.Key("ret");
        Conversation::ToJsonObject(writer, conversationList);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        string json = "";
        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);


        if (nullptr != messagePtr) {

            JSON_STARTOBJ
            writer.Key("ret");
            Message::ToJsonObjectWithMessage(writer, messagePtr);
            JSON_ENDOBJ
            json = s.GetString();
        }

        return CopyToPointer(json);
    }
    /*
    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_MarkAllConversationsAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        bool ret = true;

        EMError error;
        EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);
        if (conversationList.size() == 0)
            ret = true;
        else
        {
            for (size_t i = 0; i < conversationList.size(); i++) {
                if (!conversationList[i]->markAllMessagesAsRead())
                    ret = false;
            }
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }
    */
    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_MarkAllConversationsAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        bool ret = true;

        EMError error;
        CLIENT->getChatManager().markAllConversationsAsRead(error);

        if (error.mErrorCode != EMError::EM_NO_ERROR) ret = false;

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RecallMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return nullptr;
        }

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().recallMessage(messagePtr, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ResendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return nullptr;
        }

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return true;
            }));
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().resendMessage(messagePtr);

        JSON_STARTOBJ
        writer.Key("ret");
        Message::ToJsonObjectWithMessage(writer, messagePtr);
        JSON_ENDOBJ
        string json = s.GetString();

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_LoadMoreMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);

        string keywords = GetJsonValue_String(d, "keywords", "");
        string from = GetJsonValue_String(d, "from", "");
        int count = GetJsonValue_Int(d, "count", 20);

        string timestamp_str = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atol(timestamp_str.c_str());

        int var_direction = GetJsonValue_Int(d, "direction", 0);
        EMConversation::EMMessageSearchDirection direction = Conversation::EMMessageSearchDirectionFromInt(var_direction);

        thread t([=]() {
            EMMessageList messageList = CLIENT->getChatManager().loadMoreMessages(ts, keywords, count, from, direction);

            string json = Message::ToJson(messageList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_LoadMoreMessagesWithScope(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);

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
            EMMessageList messageList = CLIENT->getChatManager().loadMoreMessages(ts, keywords, count, from, direction, scope);

            string json = Message::ToJson(messageList);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());

            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendReadAckForConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().sendReadAckForConversation(conv_id, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendReadAckForMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        thread t([=]() {

            EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);
            if (nullptr == messagePtr) {
                EMError error(EMError::MESSAGE_INVALID);
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            CLIENT->getChatManager().sendReadAckForMessage(messagePtr);

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_SendReadAckForGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string content = GetJsonValue_String(d, "content", "");

        thread t([=]() {
            EMError error;
            EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);
            if (nullptr == messagePtr) {
                EMError error(EMError::MESSAGE_INVALID);
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }
            CLIENT->getChatManager().sendReadAckForGroupMessage(messagePtr, content);
            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        EMMessagePtr messagePtr = Message::FromJsonToMessage(jstr);

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(messagePtr->conversationId(), EMConversation::EMConversationType::CHAT, true);

        bool ret = true;

        if (nullptr == conversationPtr) {
            ret = false;
        }
        ret = conversationPtr->updateMessage(messagePtr);

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveMessagesBeforeTimestamp(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string timestamp = GetJsonValue_String(d, "timestamp", "0");

        int64_t ts = atoll(timestamp.c_str());

        thread t([=]() {

            bool ret = CLIENT->getChatManager().removeMessagesBeforeTimestamp(ts);

            if (false == ret) {
                EMError error(EMError::DATABASE_ERROR);
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DeleteConversationFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        bool delete_message = GetJsonValue_Bool(d, "isDeleteServerMessages", true);

        thread t([=]() {
            EMErrorPtr error = CLIENT->getChatManager().deleteConversationFromServer(cov_id, conv_type, delete_message);
            if (EMError::EM_NO_ERROR != error->mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchSupportLanguages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        thread t([=]() {
            vector<tuple<string, string, string>> languages;

            EMErrorPtr error = CLIENT->getChatManager().fetchSupportLanguages(languages);

            if (EMError::EM_NO_ERROR != error->mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string json = SupportLanguage::ToJson(languages);
            string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_TranslateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        EMMessagePtr msg = Message::FromJsonObjectToMessage(d["message"]);

        vector<string> langs_vec = MyJson::FromJsonObjectToVector(d["languages"]);

        thread t([=]() {
            EMErrorPtr error = CLIENT->getChatManager().translateMessage(msg, langs_vec);
            if (EMError::EM_NO_ERROR == error->mErrorCode) {

                string json = Message::ToJson(msg);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_FetchGroupReadAcks(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string ack_id = GetJsonValue_String(d, "ackId", "");

        thread t([=]() {

            EMError error;
            int totalCount = 0;
            EMCursorResultRaw<EMGroupReadAckPtr> msgCursorResult = CLIENT->getChatManager().fetchGroupReadAcks(msg_id, group_id, error, page_size, &totalCount, ack_id);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string cursor = msgCursorResult.nextPageCursor();
                string json = CursorResult::ToJson(cursor, msgCursorResult.result());

                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ReportMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string tag = GetJsonValue_String(d, "tag", "");
        string reason = GetJsonValue_String(d, "reason", "");

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().reportMessage(msg_id, tag, reason, error);

            if (EMError::EM_NO_ERROR != error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_AddReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string reaction = GetJsonValue_String(d, "reaction", "");

        thread t([=]() {
            EMError error;
            CLIENT->getReactionManager().addReaction(msg_id, reaction, error);

            if (EMError::EM_NO_ERROR != error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string reaction = GetJsonValue_String(d, "reaction", "");

        thread t([=]() {
            EMError error;
            CLIENT->getReactionManager().removeReaction(msg_id, reaction, error);
            if (EMError::EM_NO_ERROR != error.mErrorCode) {

                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());

                return;
            }

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetReactionList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);

        vector<string> id_vec = MyJson::FromJsonObjectToVector(d["msgIds"]);

        string group_id = GetJsonValue_String(d, "groupId", "");

        string type = GetJsonValue_String(d, "type", "groupchat"); //need to check

        thread t([=]() {
            EMError error;
            map<string, EMMessageReactionList> map = CLIENT->getReactionManager().getReactionList(id_vec, type, group_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = MessageReaction::ToJson(map);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetReactionDetail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string reaction = GetJsonValue_String(d, "reaction", "");
        string cursor = GetJsonValue_String(d, "cursor", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            string outCursor = "";
            EMMessageReactionPtr ret = CLIENT->getReactionManager().getReactionDetail(msg_id, reaction, cursor, page_size, outCursor, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = CursorResult::ToJson(outCursor, ret);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetGroupAckCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        int count = 0;

        if (nullptr != messagePtr) {
            count = messagePtr->groupAckCount();
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Int(count);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetHasDeliverAck(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        bool ret = false;

        if (nullptr != messagePtr) {
            ret = messagePtr->isDeliverAcked();
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetHasReadAck(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        bool ret = false;

        if (nullptr != messagePtr) {
            ret = messagePtr->isReadAcked();
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetReactionListForMsg(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        string json = "";

        if (nullptr != messagePtr)
        {
            EMMessageReactionList& list = messagePtr->reactionList();
            JSON_STARTOBJ
            writer.Key("ret");
            MessageReaction::ToJsonObject(writer, list);
            JSON_ENDOBJ
            json = s.GetString();
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetChatThreadForMsg(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        string json = "";

        if (nullptr != messagePtr) {

            EMThreadEventPtr t = messagePtr->threadOverview();

            JSON_STARTOBJ
            writer.Key("ret");
            ChatThread::ToJsonObject(writer, t);
            JSON_ENDOBJ
            json = s.GetString();
        }
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetConversationsFromServerWithPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 50);

        thread t([=]() {
            EMError error;
            EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServerWithPage(error, page_num, page_size);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = Conversation::ToJson(conversationList);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveMessagesFromServerWithMsgIds(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        vector<string> msgIds = MyJson::FromJsonObjectToVector(d["msgIds"]);

        thread t([=]() {
            EMErrorPtr error = CLIENT->getChatManager().removeMessagesFromServer(cov_id, conv_type, msgIds);

            if (EMError::EM_NO_ERROR == error->mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveMessagesFromServerWithTs(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        string timestamp = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atoll(timestamp.c_str());

        thread t([=]() {
            EMErrorPtr error = CLIENT->getChatManager().removeMessagesFromServer(cov_id, conv_type, ts);

            if (EMError::EM_NO_ERROR == error->mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_PinConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string convId = GetJsonValue_String(d, "convId", "");
        bool isPinned = GetJsonValue_Bool(d, "isPinned", false);

        thread t([=]() {

            EMError error;
            CLIENT->getChatManager().pinConversation(convId, isPinned, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                EMError error(EMError::DATABASE_ERROR);
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetMessagesCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        EMError error;
        int count = 0;

        count = CLIENT->getChatManager().getMessagesCount();

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Int(count);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RemoveEarlierHistoryMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        int count = GetJsonValue_Int(d, "count", 100);

        thread t([=]() {

            bool ret = CLIENT->getChatManager().removeEarlierHistoryMessages(count);

            if (true == ret) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                EMError error(EMError::DATABASE_ERROR);
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_ModifyMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msgId = GetJsonValue_String(d, "msgId", "");
        EMMessageBodyPtr body = Message::FromJsonObjectToBody(d["body"]);

        thread t([=]() {

            EMError error;
            EMMessagePtr msg = CLIENT->getChatManager().modifyMessage(msgId, body, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = Message::ToJson(msg);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DownloadCombineMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        EMMessagePtr msg = Message::FromJsonObjectToMessage(d["msg"]);

        thread t([=]() {
            EMErrorPtr error(new EMError);
            EMMessageList msgList = CLIENT->getChatManager().downloadCombineMessages(msg, error);

            if (EMError::EM_NO_ERROR == error->mErrorCode) {
                string json = Message::ToJson(msgList);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetPinnedInfo(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        string json = "";

        if (nullptr != messagePtr) {
            bool isPinned = false;
            string operatorId = "";
            int64_t ts = 0;

            messagePtr->pinnedInfo(isPinned, operatorId, ts);

            if (isPinned == true) {
                JSON_STARTOBJ
                writer.Key("ret");
                PinnedInfo::ToJsonObject(writer, isPinned, operatorId, ts);
                JSON_ENDOBJ

                json = s.GetString();
            }
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_MarkConversations(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        vector<string> convIds = MyJson::FromJsonObjectToVector(d["convIds"]);
        int mark = GetJsonValue_Int(d, "mark", 0);
        bool isMarked = GetJsonValue_Bool(d, "isMarked", true);

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().markConversation(convIds, mark, isMarked, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_DeleteAllMessagesAndConversations(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        bool clearServerData = GetJsonValue_Bool(d, "clearServerData", false);

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().deleteAllMessagesAndConversations(clearServerData, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_PinMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msgId = GetJsonValue_String(d, "msgId", "");
        bool isPinned = GetJsonValue_Bool(d, "isPinned", false);

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().pinMessage(msgId, isPinned, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_GetPinnedMessagesFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msgId = GetJsonValue_String(d, "convId", "");

        thread t([=]() {
            EMError error;
            EMMessageList msgList = CLIENT->getChatManager().getPinnedMessagesFromServer(msgId, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = Message::ToJson(msgList);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ChatManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != gChatManagerListener) {

            EMTextMessageBodyPtr tb(new EMTextMessageBody("this is text message"));
            EMMessagePtr msg = EMMessage::createSendMessage("from_user", "to_user", tb, EMMessage::EMChatType::SINGLE);

            EMMessageList msg_list;
            msg_list.push_back(msg);

            gChatManagerListener->onReceiveMessages(msg_list);
            gChatManagerListener->onReceiveCmdMessages(msg_list);
            gChatManagerListener->onReceiveHasReadAcks(msg_list);
            gChatManagerListener->onReceiveHasDeliveredAcks(msg_list);
            gChatManagerListener->onReceiveRecallMessages(msg_list);
            gChatManagerListener->onUpdateGroupAcks();

            EMGroupReadAckList acklist;
            EMGroupReadAckPtr ackptr(new EMGroupReadAck());
            ackptr->meta_id = "meta_id";
            ackptr->msgPtr = msg;
            ackptr->from = "from";
            ackptr->content = "content";
            ackptr->count = 123;
            ackptr->timestamp = 123456;
            acklist.push_back(ackptr);
            gChatManagerListener->onReceiveReadAcksForGroupMessage(acklist);

            EMError error;
            EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);

            gChatManagerListener->onUpdateConversationList(conversationList);
            gChatManagerListener->onReceiveReadAckForConversation("fromUsername", "toUsername");
            gChatManagerListener->onMessageIdChanged("user", "oldMsgId", "newMsgId");
            gChatManagerListener->onMessagePinChanged("msgId", "convId", true, "me", 1712802152000);
        }

        if (nullptr != gReactionManagerListener) {

            EMMessageReactionList reacion_list;
            EMMessageReactionPtr reaction(new EMMessageReaction("hehe"));
            reaction->setCount(10);
            reaction->setState(true);

            vector<string> user_list;
            user_list.push_back("user1");
            user_list.push_back("user2");
            reaction->setUserList(user_list);
            reaction->setTs(123456);

            reacion_list.push_back(reaction);

            EMMessageReactionChangeList reaction_change_list;

            EMMessageReactionChangePtr reaction_change(new EMMessageReactionChange());
            reaction_change->setFrom("from");
            reaction_change->setTo("to");
            reaction_change->setMessageId("messageId");
            reaction_change->setReactionList(reacion_list);
            reaction_change_list.push_back(reaction_change);

            gReactionManagerListener->messageReactionDidChange(reaction_change_list);
        }

        return nullptr;
    }
}

