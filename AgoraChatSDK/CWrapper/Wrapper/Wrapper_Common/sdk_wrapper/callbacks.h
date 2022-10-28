#ifndef _SDK_WRAPPER_CALLBACKS_H_
#define _SDK_WRAPPER_CALLBACKS_H_

#include "emconnection_listener.h"
#include "emchatmanager_listener.h"

#include "sdk_wrapper_internal.h"
#include "models.h"
#include "tool.h"

extern NativeListenerEvent gCallback;

namespace sdk_wrapper {

    class ConnectionListener : public EMConnectionListener
    {
    public:
        void onConnect(const std::string& info) override
        {
            CallBack(STRING_CLIENT_LISTENER.c_str(), "OnConnected", info.c_str());
        }

        void onDisconnect(EMErrorPtr error) override
        {
            CallBack(STRING_CLIENT_LISTENER.c_str(), "OnDisconnected", to_string(error->mErrorCode).c_str());
        }

        void onPong() override
        {
        }

        void onTokenNotification(EMErrorPtr error) override
        {
            if (EMError::TOKEN_EXPIRED == error->mErrorCode) {
                CallBack(STRING_CLIENT_LISTENER.c_str(), "OnTokenExpired", to_string(error->mErrorCode).c_str());
            }
            else if (EMError::TOKEN_WILL_EXPIRE == error->mErrorCode) {
                CallBack(STRING_CLIENT_LISTENER.c_str(), "OnTokenWillExpire", to_string(error->mErrorCode).c_str());
            }
        }
    };

    class ChatManagerListener : public EMChatManagerListener
    {
    public:
        void onReceiveMessages(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnMessagesReceived", json.c_str());
        }

        void onReceiveCmdMessages(const EMMessageList& messages) override {
        }

        void onReceiveHasReadAcks(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnMessagesRead", json.c_str());
        }

        void onReceiveHasDeliveredAcks(const EMMessageList& messages) override {
            //TODO
        }

        void onReceiveRecallMessages(const EMMessageList& messages) override {
            //TODO
        }

        void onUpdateGroupAcks() override {
            //TODO
        }

        void onReceiveReadAcksForGroupMessage(const EMGroupReadAckList& acks) override {
            //TODO

        }

        void onUpdateConversationList(const EMConversationList& conversations) override {
            //TODO
        }

        void onReceiveReadAckForConversation(const std::string& fromUsername, const std::string& toUsername) override {
            //TODO
        }
    };
}

#endif // _CALLBACKS_H_
