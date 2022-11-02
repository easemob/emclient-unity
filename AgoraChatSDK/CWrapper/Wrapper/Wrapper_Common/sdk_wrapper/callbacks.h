#ifndef _SDK_WRAPPER_CALLBACKS_H_
#define _SDK_WRAPPER_CALLBACKS_H_

#include "emconnection_listener.h"
#include "emchatmanager_listener.h"
#include "emgroupmanager_listener.h"

#include "sdk_wrapper_internal.h"
#include "models.h"
#include "tool.h"

extern EMClient* gClient;
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

    class MultiDevicesListener : public EMMultiDevicesListener
    {
    public:
        void onContactMultiDevicesEvent(MultiDevicesOperation operation, const std::string& target, const std::string& ext) override {

            JSON_STARTOBJ
            writer.Key("event");
            writer.String(to_string((int)operation).c_str());
            writer.Key("username");
            writer.String(target.c_str());
            writer.Key("ext");
            writer.String(ext.c_str());
            JSON_ENDOBJ
            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), "OnContactMultiDevicesEvent", json.c_str());
        }

        void onGroupMultiDevicesEvent(MultiDevicesOperation operation, const std::string& target, const std::vector<std::string>& usernames) override {
            JSON_STARTOBJ
            writer.Key("event");
            writer.String(to_string((int)operation).c_str());
            writer.Key("groupId");
            writer.String(target.c_str());
            writer.Key("usernames");
            writer.String(JsonStringFromVector(usernames).c_str());
            JSON_ENDOBJ
            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), "OnGroupMultiDevicesEvent", json.c_str());
        }

        void onThreadMultiDevicesEvent(MultiDevicesOperation operation, const std::string& target, const std::vector<std::string>& usernames) override {
            //TODO
        }

        void undisturbMultiDevicesEvent(const std::string& data) override {
            CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), "UndisturbMultiDevicesEvent", data.c_str());
        }
    };

    class GroupManagerListener : public EMGroupManagerListener
    {
    public:
        void onReceiveInviteFromGroup(const std::string groupId, const std::string groupName, const std::string& inviter, const std::string& inviteMessage) override {
            //TODO
        }

        void onReceiveInviteAcceptionFromGroup(const EMGroupPtr group, const std::string& invitee) override {
            //TODO
        }

        void onReceiveInviteDeclineFromGroup(const EMGroupPtr group, const std::string& invitee, const std::string& reason) override {
            //TODO
        }

        void onAutoAcceptInvitationFromGroup(const EMGroupPtr group, const std::string& inviter, const std::string& inviteMessage) override {
            //TODO
        }

        void onLeaveGroup(const EMGroupPtr group, EMMuc::EMMucLeaveReason reason) override {
            //TODO
        }

        void onReceiveJoinGroupApplication(const EMGroupPtr group, const std::string& from, const std::string& message) override {
            //TODO
        }

        void onReceiveAcceptionFromGroup(const EMGroupPtr group) override {
            //TODO
        }

        void onReceiveRejectionFromGroup(const std::string& groupId, const std::string& reason) override {
            //TODO
        }

        void onUpdateMyGroupList(const std::vector<EMGroupPtr>& list) override {
            //no corresponding delegate defined in API
        }

        void onAddMutesFromGroup(const EMGroupPtr group, const std::vector<std::string>& mutes, int64_t muteExpire) override {
            //TODO
        }

        void onRemoveMutesFromGroup(const EMGroupPtr group, const std::vector<std::string>& mutes) override {
            //TODO
        }

        void onAddWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const std::vector<std::string>& members) override {
            //TODO
        }

        void onRemoveWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const std::vector<std::string>& members) override {
            //TODO
        }

        void onAllMemberMuteChangedFromGroup(const easemob::EMGroupPtr Group, bool isAllMuted) override {
            //TODO
        }

        void onAddAdminFromGroup(const EMGroupPtr Group, const std::string& admin) override {
            //TODO
        }

        void onRemoveAdminFromGroup(const EMGroupPtr group, const std::string& admin) override {
            //TODO
        }

        void onAssignOwnerFromGroup(const EMGroupPtr group, const std::string& newOwner, const std::string& oldOwner) override {
            //TODO
        }

        void onMemberJoinedGroup(const EMGroupPtr group, const std::string& member) override {
            //TODO
        }

        void onMemberLeftGroup(const EMGroupPtr group, const std::string& member) override {
            //TODO
        }

        void onUpdateAnnouncementFromGroup(const EMGroupPtr group, const std::string& announcement) override {
            //TODO
        }

        void onUploadSharedFileFromGroup(const EMGroupPtr group, const EMMucSharedFilePtr sharedFile) override {
            //TODO
        }

        void onDeleteSharedFileFromGroup(const EMGroupPtr group, const std::string& fileId) override {
            //TODO
        }
    };
}

#endif // _CALLBACKS_H_
