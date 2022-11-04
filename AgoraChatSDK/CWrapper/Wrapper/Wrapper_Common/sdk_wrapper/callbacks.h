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
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnCmdMessageReceived", json.c_str());
        }

        void onReceiveHasReadAcks(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnMessagesRead", json.c_str());
        }

        void onReceiveHasDeliveredAcks(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnMessageDelivered", json.c_str());
        }

        void onReceiveRecallMessages(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnMessageRecalled", json.c_str());
        }

        void onUpdateGroupAcks() override {
            CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnReadAckForGroupMessageUpdated", nullptr);
        }

        void onReceiveReadAcksForGroupMessage(const EMGroupReadAckList& acks) override {
            string json = GroupReadAck::ToJson(acks);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnGroupMessageRead", json.c_str());

        }

        void onUpdateConversationList(const EMConversationList& conversations) override {
            string json = Conversation::ToJson(conversations);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnConversationUpdate", json.c_str());
        }

        void onReceiveReadAckForConversation(const string& fromUsername, const string& toUsername) override {

            JSON_STARTOBJ
            writer.Key("from");
            writer.String(fromUsername.c_str());

            writer.Key("to");
            writer.String(toUsername.c_str());
            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), "OnConversationRead", json.c_str());
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

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(groupId.c_str());

            writer.Key("groupName");
            writer.String(groupName.c_str());

            writer.Key("inviter");
            writer.String(inviter.c_str());

            writer.Key("reason");
            writer.String(inviteMessage.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnInvitationReceived", json.c_str());
        }

        void onReceiveInviteAcceptionFromGroup(const EMGroupPtr group, const string& invitee) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("invitee");
            writer.String(invitee.c_str());

            //TODO: need to check
            writer.Key("reason");
            writer.String("");

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnInvitationAccepted", json.c_str());
        }

        void onReceiveInviteDeclineFromGroup(const EMGroupPtr group, const string& invitee, const string& reason) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("invitee");
            writer.String(invitee.c_str());

            //TODO: need to check
            writer.Key("reason");
            writer.String(reason.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnInvitationDeclined", json.c_str());
        }

        void onAutoAcceptInvitationFromGroup(const EMGroupPtr group, const string& inviter, const string& inviteMessage) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("inviter");
            writer.String(inviter.c_str());

            //TODO: need to check
            writer.Key("inviteMessage");
            writer.String(inviteMessage.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnAutoAcceptInvitationFromGroup", json.c_str());
        }

        void onLeaveGroup(const EMGroupPtr group, EMMuc::EMMucLeaveReason reason) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("groupName");
            writer.String(group->groupSubject().c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (EMMuc::EMMucLeaveReason::BE_KICKED == reason) {
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnUserRemoved", json.c_str());
                return;
            }
            if (EMMuc::EMMucLeaveReason::DESTROYED == reason) {
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnGroupDestroyed", json.c_str());
                return;
            }
        }

        void onReceiveJoinGroupApplication(const EMGroupPtr group, const string& from, const string& message) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("groupName");
            writer.String(group->groupSubject().c_str());

            writer.Key("applicant");
            writer.String(from.c_str());

            writer.Key("reason");
            writer.String(message.c_str());

            JSON_ENDOBJ

           string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnRequestToJoinReceived", json.c_str());
        }

        void onReceiveAcceptionFromGroup(const EMGroupPtr group) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("groupName");
            writer.String(group->groupSubject().c_str());

            //TODO: need to check
            writer.Key("accepter");
            writer.String("");

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnRequestToJoinAccepted", json.c_str());
        }

        void onReceiveRejectionFromGroup(const string& groupId, const string& reason) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(groupId.c_str());

            //TODO: need to check
            writer.Key("groupName");
            writer.String("");

            //TODO: need to check
            writer.Key("decliner");
            writer.String("");

            writer.Key("reason");
            writer.String(reason.c_str());

            JSON_ENDOBJ

           string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnRequestToJoinDeclined", json.c_str());
        }

        void onUpdateMyGroupList(const vector<EMGroupPtr>& list) override {
            //no corresponding delegate defined in API
        }

        void onAddMutesFromGroup(const EMGroupPtr group, const vector<string>& mutes, int64_t muteExpire) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("muteExpire");
            writer.Int64(muteExpire);

            writer.Key("list");
            MyJson::ToJsonObject(writer, mutes);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnMuteListAdded", json.c_str());
        }

        void onRemoveMutesFromGroup(const EMGroupPtr group, const vector<string>& mutes) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("list");
            MyJson::ToJsonObject(writer, mutes);

            JSON_ENDOBJ

             string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "onMuteListRemoved", json.c_str());
        }

        void onAddWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const vector<string>& members) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("list");
            MyJson::ToJsonObject(writer, members);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnAddWhiteListMembersFromGroup", json.c_str());

        }

        void onRemoveWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const vector<string>& members) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("list");
            MyJson::ToJsonObject(writer, members);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnRemoveWhiteListMembersFromGroup", json.c_str());
        }

        void onAllMemberMuteChangedFromGroup(const easemob::EMGroupPtr Group, bool isAllMuted) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("muted");
            writer.Bool(isAllMuted);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnAllMemberMuteChangedFromGroup", json.c_str());
        }

        void onAddAdminFromGroup(const EMGroupPtr Group, const string& admin) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("admin");
            writer.String(admin.c_str());

            JSON_ENDOBJ

             string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnAdminAdded", json.c_str());
        }

        void onRemoveAdminFromGroup(const EMGroupPtr group, const string& admin) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("admin");
            writer.String(admin.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnAdminRemoved", json.c_str());
        }

        void onAssignOwnerFromGroup(const EMGroupPtr group, const string& newOwner, const string& oldOwner) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("newOwner");
            writer.String(newOwner.c_str());

            writer.Key("groupId");
            writer.String(oldOwner.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnOwnerChanged", json.c_str());
        }

        void onMemberJoinedGroup(const EMGroupPtr group, const string& member) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("member");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnMemberJoined", json.c_str());
        }

        void onMemberLeftGroup(const EMGroupPtr group, const string& member) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("member");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnMemberExited", json.c_str());
        }

        void onUpdateAnnouncementFromGroup(const EMGroupPtr group, const string& announcement) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("announcement");
            writer.String(announcement.c_str());

            JSON_ENDOBJ

             string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnAnnouncementChanged", json.c_str());
        }

        void onUploadSharedFileFromGroup(const EMGroupPtr group, const EMMucSharedFilePtr sharedFile) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("sharedFile");
            GroupSharedFile::ToJsonObject(writer, sharedFile);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnSharedFileAdded", json.c_str());
        }

        void onDeleteSharedFileFromGroup(const EMGroupPtr group, const string& fileId) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("fileId");
            writer.String(fileId.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), "OnSharedFileDeleted", json.c_str());
        }
    };

    class RoomManagerListener : public EMChatroomManagerListener
    {
    public:

        void  onMemberJoinedChatroom(const EMChatroomPtr chatroom, const std::string& member) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("participant");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnMemberJoined", json.c_str());
        }

        void onLeaveChatroom(const EMChatroomPtr chatroom, EMMuc::EMMucLeaveReason reason) override {

            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("roomName");
            writer.String(chatroom->chatroomSubject().c_str());

            string json = "";

            if (EMMuc::EMMucLeaveReason::DESTROYED == reason) {

                JSON_ENDOBJ
                json = s.GetString();

                if (json.size() > 0)
                    CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnChatRoomDestroyed", json.c_str());

                return;
            }
            if ((EMMuc::EMMucLeaveReason::BE_KICKED == reason)) {

                //TODO: check this this!
                writer.Key("participant");
                writer.String("");

                JSON_ENDOBJ
                    json = s.GetString();

                if (json.size() > 0)
                    CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnRemovedFromChatRoom", json.c_str());

                return;
            }
        }

        void onMemberLeftChatroom(const EMChatroomPtr chatroom, const std::string& member) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("roomName");
            writer.String(chatroom->chatroomSubject().c_str());

            writer.Key("participant");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "onMemberExited", json.c_str());
        }

        void onAddMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string>& mutes, int64_t muteExpire) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("list");
            MyJson::ToJsonObject(writer, mutes);

            writer.Key("expireTime");
            writer.Int64(muteExpire);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnMuteListAdded", json.c_str());
        }

        void onRemoveMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string>& mutes) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("list");
            MyJson::ToJsonObject(writer, mutes);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnMuteListRemoved", json.c_str());
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
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("admin");
            writer.String(admin.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnAdminAdded", json.c_str());
        }

        void onRemoveAdminFromChatroom(const EMChatroomPtr chatroom, const std::string& admin) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("admin");
            writer.String(admin.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnMemberJoined", json.c_str());
        }

        void onAssignOwnerFromChatroom(const EMChatroomPtr chatroom, const std::string& newOwner, const std::string& oldOwner) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("newOwner");
            writer.String(newOwner.c_str());

            writer.Key("oldOwner");
            writer.String(oldOwner.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnOwnerChanged", json.c_str());
        }

        void onUpdateAnnouncementFromChatroom(const EMChatroomPtr chatroom, const std::string& announcement) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("announcement");
            writer.String(announcement.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), "OnAnnouncementChanged", json.c_str());
        }

        void onChatroomAttributesChanged(const std::string chatroomId, const std::string& ext, std::string from) override {
            //TODO: Add this
        }

        void onChatroomAttributesRemoved(const std::string chatroomId, const std::string& ext, std::string from) override {
            //TODO: Add this 
        }
    };

    class ContactManagerListener : public EMContactListener
    {
    public:
        void onContactAdded(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("username");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), "OnContactAdded", json.c_str());
        }
        void onContactDeleted(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("username");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), "OnContactDeleted", json.c_str());
        }
        void onContactInvited(const std::string& username, std::string& reason) override {
            JSON_STARTOBJ

            writer.Key("username");
            writer.String(username.c_str());

            writer.Key("reason");
            writer.String(reason.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), "OnContactInvited", json.c_str());
        }
        void onContactAgreed(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("username");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), "OnFriendRequestAccepted", json.c_str());
        }
        void onContactRefused(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("username");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), "OnFriendRequestDeclined", json.c_str());
        }
    };

    class PresenceManagerListener : public EMPresenceManagerListener
    {
    public:

        void onPresenceUpdated(const std::vector<EMPresencePtr>& presence) override {
            string json = Presence::ToJson(presence);
            if (json.size() > 0)
                CallBack(STRING_PRESENCEMANAGER_LISTENER.c_str(), "OnPresenceUpdated", json.c_str());
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

            std::string json = ChatThreadEvent::ToJson(event);

            if (event->threadOperation().compare("create") == 0) {
                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), "OnChatThreadCreate", json.c_str());
            }
            else if (event->threadOperation().compare("delete") == 0) {
                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), "OnChatThreadDestroy", json.c_str());
            }
            else if (event->threadOperation().compare("update") == 0 ||
                event->threadOperation().compare("update_msg") == 0) {

                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), "OnChatThreadUpdate", json.c_str());

            }
        }

        void onLeaveThread(const EMThreadEventPtr event, EMThreadLeaveReason reason) override {

            std::string json = ChatThreadEvent::ToJson(event);

            switch (reason)
            {
            case EMThreadLeaveReason::BE_KICKED:

                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), "OnUserKickOutOfChatThread", json.c_str());

                break;
            default:
                break;
            }
        }

        void onMemberJoined(const EMThreadEventPtr event) override {
            //TODO: need to check this, not implement before!!!
            // 
            // Implement in onThreadNotifyChange, here no need to add anything
        }

        void onMemberLeave(const EMThreadEventPtr event) override {
            // Implement in onThreadNotifyChange, here no need to add anything
        }
    };

    class ReactionManagerListener : public EMReactionManagerListener
    {
    public:

        void messageReactionDidChange(EMMessageReactionChangeList list) override {

            const EMLoginInfo& loginInfo = CLIENT->getLoginInfo();
            std::string curname = loginInfo.loginUser();

            string json = MessageReactionChange::ToJson(list, curname);

            if (json.size() > 0)
                CallBack(STRING_THREADMANAGER_LISTENER.c_str(), "MessageReactionDidChange", json.c_str());
        }
    };

    class ConnectionCallbackListener : public EMConnectionCallbackListener
    {
    public:

        bool onVerifyServerCert(const std::vector<std::string>& certschain, std::string domain = "") override {
            return true;
        }
    };
}

#endif // _CALLBACKS_H_
