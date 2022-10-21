#ifndef _SDK_WRAPPER_CALLBACKS_H_
#define _SDK_WRAPPER_CALLBACKS_H_

#include "emconnection_listener.h"
#include "emchatmanager_listener.h"

#include "sdk_wrapper_internal.h"
#include "models.h"

extern NativeListenerEvent gCallback;

namespace sdk_wrapper {

    class ConnectionListener : public EMConnectionListener
    {
    public:
        void onConnect(const std::string& info) override
        {
            if (gCallback)
                gCallback("connectionListener", "OnConnected", info.c_str());
        }

        void onDisconnect(EMErrorPtr error) override
        {
            if (gCallback)
                gCallback("connectionListener", "OnDisconnected", to_string(error->mErrorCode).c_str());
        }

        void onPong() override
        {
        }

        void onTokenNotification(EMErrorPtr error) override
        {
            if (gCallback) {
                if (EMError::TOKEN_EXPIRED == error->mErrorCode) {
                    gCallback("connectionListener", "OnTokenExpired", to_string(error->mErrorCode).c_str());
                }
                else if (EMError::TOKEN_WILL_EXPIRE == error->mErrorCode) {
                    gCallback("connectionListener", "OnTokenWillExpire", to_string(error->mErrorCode).c_str());
                }
            }

        }
    };

    class ChatManagerListener : public EMChatManagerListener
    {
    public:
        void onReceiveMessages(const EMMessageList& messages) override {
            if (gCallback) {
                string json = Message::ToJson(messages);
                if (json.size() > 0)
                    gCallback("chatManagerListener", "OnMessageReceived", json.c_str());
            }
        }

        void onReceiveCmdMessages(const EMMessageList& messages) override {
        }

        void onReceiveHasReadAcks(const EMMessageList& messages) override {
            if (gCallback) {
                string json = Message::ToJson(messages);
                if (json.size() > 0)
                    gCallback("chatManagerListener", "OnMessageRead", json.c_str());
            }
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
