#ifndef _SDK_WRAPPER_CALLBACKS_H_
#define _SDK_WRAPPER_CALLBACKS_H_

#include "emconnection_listener.h"
#include "emchatmanager_listener.h"
#include "emgroupmanager_listener.h"
#include "emchatroommanager_listener.h"
#include "emcontactlistener.h"

#include "sdk_wrapper_internal.h"
#include "models.h"
#include "tool.h"

extern EMClient* gClient;
extern NativeListenerEvent gCallback;

namespace sdk_wrapper {

    class ConnectionListener : public EMConnectionListener
    {
    public:
        void onConnect(const string& info) override
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

        void onReceiveReadAckForConversation(const string& fromUsername, const string& toUsername) override {
            //TODO
        }
    };

    class MultiDevicesListener : public EMMultiDevicesListener
    {
    public:
        void onContactMultiDevicesEvent(MultiDevicesOperation operation, const string& target, const string& ext) override {

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

        void onGroupMultiDevicesEvent(MultiDevicesOperation operation, const string& target, const vector<string>& usernames) override {

            JSON_STARTOBJ
            writer.Key("event");
            writer.String(to_string((int)operation).c_str());
            writer.Key("groupId");
            writer.String(target.c_str());
            writer.Key("usernames");
            writer.String(MyJson::ToJson(usernames).c_str());
            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), "OnGroupMultiDevicesEvent", json.c_str());
        }

        void onThreadMultiDevicesEvent(MultiDevicesOperation operation, const string& target, const vector<string>& usernames) override {
            //TODO
        }

        void undisturbMultiDevicesEvent(const string& data) override {
            CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), "UndisturbMultiDevicesEvent", data.c_str());
        }
    };

    class GroupManagerListener : public EMGroupManagerListener
    {
    public:
        void onReceiveInviteFromGroup(const string groupId, const string groupName, const string& inviter, const string& inviteMessage) override {
            //TODO
        }

        void onReceiveInviteAcceptionFromGroup(const EMGroupPtr group, const string& invitee) override {
            //TODO
        }

        void onReceiveInviteDeclineFromGroup(const EMGroupPtr group, const string& invitee, const string& reason) override {
            //TODO
        }

        void onAutoAcceptInvitationFromGroup(const EMGroupPtr group, const string& inviter, const string& inviteMessage) override {
            //TODO
        }

        void onLeaveGroup(const EMGroupPtr group, EMMuc::EMMucLeaveReason reason) override {
            //TODO
        }

        void onReceiveJoinGroupApplication(const EMGroupPtr group, const string& from, const string& message) override {
            //TODO
        }

        void onReceiveAcceptionFromGroup(const EMGroupPtr group) override {
            //TODO
        }

        void onReceiveRejectionFromGroup(const string& groupId, const string& reason) override {
            //TODO
        }

        void onUpdateMyGroupList(const vector<EMGroupPtr>& list) override {
            //no corresponding delegate defined in API
        }

        void onAddMutesFromGroup(const EMGroupPtr group, const vector<string>& mutes, int64_t muteExpire) override {
            //TODO
        }

        void onRemoveMutesFromGroup(const EMGroupPtr group, const vector<string>& mutes) override {
            //TODO
        }

        void onAddWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const vector<string>& members) override {
            //TODO
        }

        void onRemoveWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const vector<string>& members) override {
            //TODO
        }

        void onAllMemberMuteChangedFromGroup(const easemob::EMGroupPtr Group, bool isAllMuted) override {
            //TODO
        }

        void onAddAdminFromGroup(const EMGroupPtr Group, const string& admin) override {
            //TODO
        }

        void onRemoveAdminFromGroup(const EMGroupPtr group, const string& admin) override {
            //TODO
        }

        void onAssignOwnerFromGroup(const EMGroupPtr group, const string& newOwner, const string& oldOwner) override {
            //TODO
        }

        void onMemberJoinedGroup(const EMGroupPtr group, const string& member) override {
            //TODO
        }

        void onMemberLeftGroup(const EMGroupPtr group, const string& member) override {
            //TODO
        }

        void onUpdateAnnouncementFromGroup(const EMGroupPtr group, const string& announcement) override {
            //TODO
        }

        void onUploadSharedFileFromGroup(const EMGroupPtr group, const EMMucSharedFilePtr sharedFile) override {
            //TODO
        }

        void onDeleteSharedFileFromGroup(const EMGroupPtr group, const string& fileId) override {
            //TODO
        }
    };

    class RoomManagerListener : public EMChatroomManagerListener
    {
    public:

        void  onMemberJoinedChatroom(const EMChatroomPtr chatroom, const std::string& member) override {
            //TODO
        }

        void onLeaveChatroom(const EMChatroomPtr chatroom, EMMuc::EMMucLeaveReason reason) override {
            //TODO
        }

        void onMemberLeftChatroom(const EMChatroomPtr chatroom, const std::string& member) override {
            //TODO
        }

        void onAddMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string>& mutes, int64_t muteExpire) override {
            //TODO
        }

        void onRemoveMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string>& mutes) override {
            //TODO
        }

        void onAddWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string>& members) override {
            //no corresponding delegate defined in API
        }

        void onRemoveWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string>& members) override {
            //no corresponding delegate defined in API
        }

        void onAllMemberMuteChangedFromChatroom(const easemob::EMChatroomPtr chatroom, bool isAllMuted) override {
            //no corresponding delegate defined in API
        }

        void onAddAdminFromChatroom(const EMChatroomPtr chatroom, const std::string& admin) override {
            //TODO
        }

        void onRemoveAdminFromChatroom(const EMChatroomPtr chatroom, const std::string& admin) override {
            //TODO
        }

        void onAssignOwnerFromChatroom(const EMChatroomPtr chatroom, const std::string& newOwner, const std::string& oldOwner) override {
            //TODO
        }

        void onUpdateAnnouncementFromChatroom(const EMChatroomPtr chatroom, const std::string& announcement) override {
            //TODO
        }

        void onChatroomAttributesChanged(const std::string chatroomId, const std::string& ext, std::string from) override {

        }

        void onChatroomAttributesRemoved(const std::string chatroomId, const std::string& ext, std::string from) override {

        }
    };

    class ContactManagerListener : public EMContactListener
    {
    public:
        void onContactAdded(const std::string& username) override {
            //TODO
        }
        void onContactDeleted(const std::string& username) override {
            //TODO
        }
        void onContactInvited(const std::string& username, std::string& reason) override {
            //TODO
        }
        void onContactAgreed(const std::string& username) override {
            //TODO
        }
        void onContactRefused(const std::string& username) override {
            //TODO
        }
    };

    class PresenceManagerListener : public EMPresenceManagerListener
    {
    public:

        void onPresenceUpdated(const std::vector<EMPresencePtr>& presence) override {
                //TODO
        }
    };

    class ThreadManagerListener : public EMThreadManagerListener
    {
    public:
        void onCreatThread(const EMThreadEventPtr event) override {
            // Implement in onThreadNotifyChange, here no need to add anything
        }

        void onUpdateMyThread(const EMThreadEventPtr event) override {
            // Implement in onThreadNotifyChange, here no need to add anything
        }

        void onThreadNotifyChange(const EMThreadEventPtr event) override {

            //TODO: refer to old code
        }

        void onLeaveThread(const EMThreadEventPtr event, EMThreadLeaveReason reason) override {
            //TODO
        }

        void onMemberJoined(const EMThreadEventPtr event) override {
            // Implement in onThreadNotifyChange, here no need to add anything
        }

        void onMemberLeave(const EMThreadEventPtr event) override {
            // Implement in onThreadNotifyChange, here no need to add anything
        }
    };
}

#endif // _CALLBACKS_H_
