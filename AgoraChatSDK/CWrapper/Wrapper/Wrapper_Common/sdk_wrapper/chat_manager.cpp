
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
//TODO: EMReactionManagerListener* gReactionManagerListener = nullptr;

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
            return Message::ToJson(update_msg);
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

        /* TODO:
        if (nullptr == gReactionManagerListener) {
            gReactionManagerListener = new ReactionManagerListener(messageReactionDidChange);
            CLIENT->getReactionManager().addListener(gReactionManagerListener);
        }
        */
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendMessage(const char* jstr, const char* cbid, char* buf)
	{
        if (!CheckClientInitOrNot(cbid)) return;

        EMMessagePtr message_ptr = Message::FromJson(jstr);
        string msg_id = message_ptr->msgId();
        string local_cbid = cbid;

        AddMsgItem(msg_id, message_ptr);
        AddProgressItem(msg_id);

        EMCallbackPtr callback_ptr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string update_msg_json = JsonStringFromUpdatedMessage(msg_id);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), update_msg_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteMsgItem(msg_id);
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const EMErrorPtr error)->bool {
                string update_msg_json = JsonStringFromUpdatedMessage(msg_id);
                string call_back_jstr = JsonStringFromErrorResult(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str(), update_msg_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteMsgItem(msg_id);
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
            int last_progress = GetLastProgress(msg_id);
            if (progress - last_progress >= 5) {
                string call_back_jstr = JsonStringFromProcess(local_cbid.c_str(), progress);
                CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                UpdateProgressMap(msg_id, progress);
            }
            return;
            }));

        message_ptr->setCallback(callback_ptr);
        CLIENT->getChatManager().sendMessage(message_ptr);

        string updated_msg_json = JsonStringFromUpdatedMessage(msg_id);
        if(updated_msg_json.size() > 0)
            memcpy(buf, updated_msg_json.c_str(), updated_msg_json.size());
	}

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");
        bool delete_messages = GetJsonValue_Bool(d, "deleteMessages", true);

        CLIENT->getChatManager().removeConversation(conv_id, delete_messages, false);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_DownloadMessageAttachments(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string local_cbid = cbid;

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return;
        }

        AddProgressItem(msg_id);

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
                int last_progress = GetLastProgress(msg_id);
                if (progress - last_progress >= 5) {
                    string call_back_jstr = JsonStringFromProcess(local_cbid.c_str(), progress);
                    CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                    UpdateProgressMap(msg_id, progress);
                }
                return;
            }
            ));
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().downloadMessageAttachments(messagePtr);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_DownloadMessageThumbnail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string local_cbid = cbid;

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return;
        }

        AddProgressItem(msg_id);

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
                int last_progress = GetLastProgress(msg_id);
                if (progress - last_progress >= 5) {
                    string call_back_jstr = JsonStringFromProcess(local_cbid.c_str(), progress);
                    CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                    UpdateProgressMap(msg_id, progress);
                }
                return;
            }
            ));
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().downloadMessageThumbnail(messagePtr);

    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_FetchHistoryMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        string start_msg_id = GetJsonValue_String(d, "startMsgId", "");

        EMConversation::EMMessageSearchDirection direction = EMConversation::EMMessageSearchDirection::UP;
        int var_direction = GetJsonValue_Int(d, "direction", 0);
        direction = EMConversation::EMMessageSearchDirection(var_direction);

        int count = GetJsonValue_Int(d, "count", 20);

        thread t([=]() {
            EMError error;
            EMCursorResultRaw<EMMessagePtr> msgCursorResult = CLIENT->getChatManager().fetchHistoryMessages(cov_id, conv_type, error, start_msg_id, count, direction);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string cursor = msgCursorResult.nextPageCursor();
                string msgs_json = Message::ToJson(msgCursorResult.result());
                string cursor_json = JsonStringFromCursorResult(cursor, msgs_json);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), cursor_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_ConversationWithType(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);
        string cov_id = GetJsonValue_String(d, "convId", "");

        EMConversation::EMConversationType conv_type = EMConversation::EMConversationType::CHAT;
        int var_type = GetJsonValue_Int(d, "convType", 0);
        conv_type = EMConversation::EMConversationType(var_type);

        bool create_if_need = GetJsonValue_Bool(d, "createIfNeed", true);
        bool is_thread = GetJsonValue_String(d, "isThread", false);

        string json = "";

        EMConversationPtr conversationPtr = CLIENT->getChatManager().conversationWithType(cov_id, conv_type, create_if_need, is_thread);
        if (nullptr != conversationPtr) {
            json = Conversation::ToJson(conversationPtr);
        }
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetConversationsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        thread t([=]() {
            EMError error;
            EMConversationList conversationList = CLIENT->getChatManager().getConversationsFromServer(error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = Conversation::ToJson(conversationList);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetUnreadMessageCount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

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
        writer.String(to_string(count).c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_InsertMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        EMMessageList list = Message::ListFromJson(jstr);

        bool ret = true;
        if (list.size() > 0) {
            ret = CLIENT->getChatManager().insertMessages(list);
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_LoadAllConversationsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        EMConversationList conversationList = CLIENT->getChatManager().loadAllConversationsFromDB();
        string json = Conversation::ToJson(conversationList);
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        string json = "";
        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);
        if (nullptr != messagePtr) {
            json = Message::ToJson(messagePtr);

        }
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_MarkAllConversationsAsRead(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

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
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RecallMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return;
        }

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return true;
            },
            [=](int progress) {
                string call_back_jstr = JsonStringFromProcess(local_cbid.c_str(), progress);
                CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }));

        EMError error;
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().recallMessage(messagePtr, error);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_ResendMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);

        //verify message
        if (nullptr == messagePtr) {
            EMError error(EMError::MESSAGE_INVALID);
            string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return;
        }

        EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
            [=]()->bool {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return true;
            },
            [=](const easemob::EMErrorPtr error)->bool {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return true;
            }));
        messagePtr->setCallback(callbackPtr);
        CLIENT->getChatManager().resendMessage(messagePtr);

        string json = Message::ToJson(messagePtr);
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_LoadMoreMessages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        Document d; d.Parse(jstr);

        string keywords = GetJsonValue_String(d, "keywords", "");
        string from = GetJsonValue_String(d, "from", "");
        int count = GetJsonValue_Int(d, "count", 20);

        string timestamp_str = GetJsonValue_String(d, "timestamp", "0");
        int64_t ts = atol(timestamp_str.c_str());

        EMConversation::EMMessageSearchDirection direction = EMConversation::EMMessageSearchDirection::UP;
        int var_direction = GetJsonValue_Int(d, "direction", 0);
        direction = EMConversation::EMMessageSearchDirection(var_direction);

        EMMessageList messageList = CLIENT->getChatManager().loadMoreMessages(ts, keywords, count, from, direction);

        string json = "";

        if (messageList.size() > 0) {
            json = Message::ToJson(messageList);
        }

        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendReadAckForConversation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string conv_id = GetJsonValue_String(d, "convId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().sendReadAckForConversation(conv_id, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendReadAckForMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");

        thread t([=]() {

            EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);
            if (nullptr == messagePtr) {
                EMError error(EMError::MESSAGE_INVALID);
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            CLIENT->getChatManager().sendReadAckForMessage(messagePtr);

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendReadAckForGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string content = GetJsonValue_String(d, "content", "");

        thread t([=]() {
            EMError error;
            EMMessagePtr messagePtr = CLIENT->getChatManager().getMessage(msg_id);
            if (nullptr == messagePtr) {
                EMError error(EMError::MESSAGE_INVALID);
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }
            CLIENT->getChatManager().sendReadAckForGroupMessage(messagePtr, content);
            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_UpdateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        EMMessagePtr messagePtr = Message::FromJson(jstr);

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
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveMessagesBeforeTimestamp(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string timestamp = GetJsonValue_String(d, "timestamp", "0");

        int64_t ts = atol(timestamp.c_str());

        thread t([=]() {

            bool ret = CLIENT->getChatManager().removeMessagesBeforeTimestamp(ts);

            if (false == ret) {
                EMError error(EMError::DATABASE_ERROR);
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_DeleteConversationFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

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
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_FetchSupportLanguages(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        thread t([=]() {
            vector<tuple<string, string, string>> languages;

            EMErrorPtr error = CLIENT->getChatManager().fetchSupportLanguages(languages);

            if (EMError::EM_NO_ERROR != error->mErrorCode) {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string json = SupportLanguage::ToJson(languages);
            string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_TranslateMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        EMMessagePtr msg = Message::FromJsonObject(d["message"]);

        string langs_json = GetJsonValue_String(d, "languages", "");
        vector<string> langs_vec = JsonStringToVector(langs_json);

        thread t([=]() {
            EMErrorPtr error = CLIENT->getChatManager().translateMessage(msg, langs_vec);
            if (EMError::EM_NO_ERROR == error->mErrorCode) {

                string json = Message::ToJson(msg);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_FetchGroupReadAcks(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msg_id", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string ack_id = GetJsonValue_String(d, "ack_id", "");

        thread t([=]() {

            EMError error;
            int totalCount = 0;
            EMCursorResultRaw<EMGroupReadAckPtr> msgCursorResult = CLIENT->getChatManager().fetchGroupReadAcks(msg_id, group_id, error, page_size, &totalCount, ack_id);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string cursor = msgCursorResult.nextPageCursor();
                string group_acks_json = GroupReadAck::ToJson(msgCursorResult.result());

                string json = JsonStringFromCursorResult(cursor, group_acks_json);

                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_ReportMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string tag = GetJsonValue_String(d, "tag", "");
        string reason = GetJsonValue_String(d, "reason", "");

        thread t([=]() {
            EMError error;
            CLIENT->getChatManager().reportMessage(msg_id, tag, reason, error);

            if (EMError::EM_NO_ERROR != error.mErrorCode) {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_AddReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string reaction = GetJsonValue_String(d, "reaction", "");

        thread t([=]() {
            EMError error;
            CLIENT->getReactionManager().addReaction(msg_id, reaction, error);

            if (EMError::EM_NO_ERROR != error.mErrorCode) {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                return;
            }

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_RemoveReaction(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string reaction = GetJsonValue_String(d, "reaction", "");

        thread t([=]() {
            EMError error;
            CLIENT->getReactionManager().removeReaction(msg_id, reaction, error);
            if (EMError::EM_NO_ERROR != error.mErrorCode) {

                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());

                return;
            }

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetReactionList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);

        string vec_str = GetJsonValue_String(d, "msgIds", "");
        vector<string> id_vec = JsonStringToVector(vec_str);

        string group_id = GetJsonValue_String(d, "groupId", "");

        string type = GetJsonValue_String(d, "type", "groupchat"); // TODO:need to check

        thread t([=]() {
            EMError error;
            map<string, EMMessageReactionList> map = CLIENT->getReactionManager().getReactionList(id_vec, type, group_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = MessageReaction::ToJson(map);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_GetReactionDetail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string msg_id = GetJsonValue_String(d, "msgId", "");
        string reaction = GetJsonValue_String(d, "reaction", "");
        string cursor = GetJsonValue_String(d, "cursor", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        std::thread t([=]() {
            EMError error;
            std::string outCursor = "";
            EMMessageReactionPtr ret = CLIENT->getReactionManager().getReactionDetail(msg_id, reaction, cursor, page_size, outCursor, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string reaction_json = MessageReaction::ToJson(ret);
                string json = JsonStringFromCursorResult(outCursor, reaction_json);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }
}

