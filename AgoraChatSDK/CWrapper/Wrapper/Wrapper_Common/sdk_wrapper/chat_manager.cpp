
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

    void AddMsgItem(std::string msgId, EMMessagePtr msgPtr)
    {
        std::lock_guard<std::mutex> maplocker(msg_locker);
        msg_ptr_map[msgId] = msgPtr;
    }

    void DeleteMsgItem(std::string msgId)
    {
        std::lock_guard<std::mutex> maplocker(msg_locker);

        auto itPtr = msg_ptr_map.find(msgId);
        if (msg_ptr_map.end() != itPtr) {
            msg_ptr_map.erase(itPtr);
        }
    }

    string JsonStringFromUpdatedMessage(std::string msgId)
    {
        EMMessagePtr update_msg = nullptr;
        {
            std::lock_guard<std::mutex> maplocker(msg_locker);

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

    void AddProgressItem(std::string msgId)
    {
        std::lock_guard<std::mutex> maplocker(progress_msg_locker);
        progress_map[msgId] = 0;
    }

    void DeleteProgressItem(std::string msgId)
    {
        std::lock_guard<std::mutex> maplocker(progress_msg_locker);
        auto it = progress_map.find(msgId);
        if (progress_map.end() != it) {
            progress_map.erase(it);
        }
    }

    void UpdateProgressMap(std::string msgId, int progress)
    {
        std::lock_guard<std::mutex> maplocker(progress_msg_locker);

        auto it = progress_map.find(msgId);
        if (progress_map.end() == it) {
            return;
        }
        it->second = progress;
    }

    int GetLastProgress(std::string msgId)
    {
        std::lock_guard<std::mutex> maplocker(progress_msg_locker);
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

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ChatManager_SendMessage(const char* jstr, const char* cbid)
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
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), "OnMessageSuccess", update_msg_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteMsgItem(msg_id);
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](const EMErrorPtr error)->bool {
                string update_msg_json = JsonStringFromUpdatedMessage(msg_id);
                string call_back_jstr = JsonStringFromErrorResult(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str(), "OnMessageError", update_msg_json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                DeleteMsgItem(msg_id);
                DeleteProgressItem(msg_id);
                return true;
            },
            [=](int progress) {
            int last_progress = GetLastProgress(msg_id);
            if (progress - last_progress >= 5) {
                string call_back_jstr = JsonStringFromProcess(local_cbid.c_str(), progress);
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
                UpdateProgressMap(msg_id, progress);
            }
            return;
            }));

        message_ptr->setCallback(callback_ptr);
        CLIENT->getChatManager().sendMessage(message_ptr);
	}
}

