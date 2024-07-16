#ifndef _SDK_WRAPPER_CALLBACKS_H_
#define _SDK_WRAPPER_CALLBACKS_H_

#include "emconnection_listener.h"
#include "emchatmanager_listener.h"
#include "emgroupmanager_listener.h"
#include "emchatroommanager_listener.h"
#include "emcontactlistener.h"
#include "emerror.h"

#include "sdk_wrapper_internal.h"
#include "models.h"
#include "tool.h"

extern EMClient* gClient;
extern NativeListenerEvent gCallback;
extern const int TOKEN_CHECK_INTERVAL;

namespace sdk_wrapper {

    class ConnectionListener : public EMConnectionListener
    {
    public:
        void onConnect(const string& info) override
        {
            CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onConnected.c_str(), info.c_str());
        }

        void onDisconnect(EMErrorPtr error, const std::string& info, const std::string& ext) override
        {
            string json = "";

            switch (error->mErrorCode)
            {
            case EMError::APP_ACTIVE_NUMBER_REACH_LIMITATION:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onAppActiveNumberReachLimitation.c_str(), json.c_str());
                break;
            case EMError::USER_AUTHENTICATION_FAILED:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onAuthFailed.c_str(), json.c_str());
                break;
            case EMError::USER_REMOVED:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onRemovedFromServer.c_str(), json.c_str());
                break;
            case EMError::USER_LOGIN_TOO_MANY_DEVICES:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onLoginTooManyDevice.c_str(), json.c_str());
                break;
            case EMError::USER_KICKED_BY_CHANGE_PASSWORD:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onChangedImPwd.c_str(), json.c_str());
                break;
            case EMError::USER_KICKED_BY_OTHER_DEVICE:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onKickedByOtherDevice.c_str(), json.c_str());
                break;
            case EMError::USER_LOGIN_ANOTHER_DEVICE:
            case EMError::USER_DEVICE_CHANGED:
                {
                    JSON_STARTOBJ
                    writer.Key("deviceName");
                    writer.String(info.c_str());
                    JSON_ENDOBJ

                    json = s.GetString();
                    CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onLoggedOtherDevice.c_str(), json.c_str());
                }
                break;
            case EMError::SERVER_SERVING_DISABLED:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onForbidByServer.c_str(), json.c_str());
                break;
            default:
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onDisconnected.c_str(), json.c_str());
                break;
            }
        }

        void onPong() override
        {
        }

        void onTokenNotification(EMErrorPtr error) override
        {
            if (EMError::TOKEN_EXPIRED == error->mErrorCode) {
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onTokenExpired.c_str(), to_string(error->mErrorCode).c_str());
            }
            else if (EMError::TOKEN_WILL_EXPIRE == error->mErrorCode) {
                CallBack(STRING_CLIENT_LISTENER.c_str(), STRING_onTokenWillExpire.c_str(), to_string(error->mErrorCode).c_str());
            }
        }

        void onReceiveToken(const std::string& token, int64_t expiredTs) override
        {
            TokenWrapper::GetInstance()->SetAndStartTokenCheckTimer(expiredTs, TOKEN_CHECK_INTERVAL);
        }
    };

    class ChatManagerListener : public EMChatManagerListener
    {
    public:
        void onReceiveMessages(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessagesReceived.c_str(), json.c_str());
        }

        void onReceiveCmdMessages(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onCmdMessagesReceived.c_str(), json.c_str());
        }

        void onReceiveHasReadAcks(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessagesRead.c_str(), json.c_str());
        }

        void onReceiveHasDeliveredAcks(const EMMessageList& messages) override {
            string json = Message::ToJson(messages);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessagesDelivered.c_str(), json.c_str());
        }

        void onReceiveRecallMessages(const std::vector<std::tuple<std::string, std::string, std::string, easemob::EMMessagePtr>>& list) override {
            if (list.size() > 0) {
                string json = RecallMessageInfo::ToJson(list);
                if (json.size() > 0)
                    CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessagesRecalledByExt.c_str(), json.c_str());

                /*
                json.clear();
                EMMessageList msgList;
                for (auto it : list) {
                    EMMessagePtr msg = nullptr;
                    msg = std::get<3>(it);
                    if (nullptr != msg && nullptr != msg.get()) {
                        msgList.push_back(msg);
                    }
                }
                json = Message::ToJson(msgList);
                if (json.size() > 0)
                    CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessagesRecalled.c_str(), json.c_str());
                */
            }
        }

        void onUpdateGroupAcks() override {
            CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onReadAckForGroupMessageUpdated.c_str(), "");
        }

        void onReceiveReadAcksForGroupMessage(const EMGroupReadAckList& acks) override {
            string json = GroupReadAck::ToJson(acks);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onGroupMessageRead.c_str(), json.c_str());

        }

        void onUpdateConversationList(const EMConversationList& conversations) override {
            string json = Conversation::ToJson(conversations);
            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onConversationsUpdate.c_str(), json.c_str());
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
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onConversationRead.c_str(), json.c_str());
        }

        void onMessageContentChanged(const EMMessagePtr message, const std::string operatorId, uint64_t operationTime) override {

            JSON_STARTOBJ
            writer.Key("msg");
            Message::ToJsonObjectWithMessage(writer, message);

            writer.Key("operatorId");
            writer.String(operatorId.c_str());

            writer.Key("operationTime");
            writer.Uint64(operationTime);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessageContentChanged.c_str(), json.c_str());
        }

        void onMessagePinChanged(const std::string& messageId, const std::string& conversationId, bool isPinned, const std::string& operatorId, int64_t ts) {

            JSON_STARTOBJ
            writer.Key("msgId");
            writer.String(messageId.c_str());

            writer.Key("convId");
            writer.String(conversationId.c_str());

            writer.Key("isPinned");
            writer.Bool(isPinned);

            writer.Key("operatorId");
            writer.String(operatorId.c_str());

            writer.Key("ts");
            writer.Uint64(ts);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessagePinChanged.c_str(), json.c_str());
        }

        void onMessageIdChanged(const string& conversationId, const string& oldMsgId, const string& newMsgId) override {
            /*
            JSON_STARTOBJ
            writer.Key("convId");
            writer.String(conversationId.c_str());

            writer.Key("oldMsgId");
            writer.String(oldMsgId.c_str());

            writer.Key("newMsgId");
            writer.String(newMsgId.c_str());
            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessageIdChanged.c_str(), json.c_str());
            */
        }
    };

    class MultiDevicesListener : public EMMultiDevicesListener
    {
    public:
        void onContactMultiDevicesEvent(MultiDevicesOperation operation, const string& target, const string& ext) override {

            JSON_STARTOBJ
            writer.Key("operation");
            writer.Int(MultiDevices::MultiDevicesOperationToInt(operation));
            writer.Key("target");
            writer.String(target.c_str());
            writer.Key("ext");
            writer.String(ext.c_str());
            JSON_ENDOBJ
            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), STRING_onContactMultiDevicesEvent.c_str(), json.c_str());
        }

        void onGroupMultiDevicesEvent(MultiDevicesOperation operation, const string& target, const vector<string>& usernames) override {

            JSON_STARTOBJ
            writer.Key("operation");
            writer.Int(MultiDevices::MultiDevicesOperationToInt(operation));
            writer.Key("target");
            writer.String(target.c_str());
            writer.Key("userIds");
            MyJson::ToJsonObject(writer, usernames);
            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), STRING_onGroupMultiDevicesEvent.c_str(), json.c_str());
        }

        void onThreadMultiDevicesEvent(MultiDevicesOperation operation, const string& target, const vector<string>& usernames) override {

            JSON_STARTOBJ
            writer.Key("operation");
            writer.Int(MultiDevices::MultiDevicesOperationToInt(operation));
            writer.Key("target");
            writer.String(target.c_str());
            writer.Key("userIds");
            MyJson::ToJsonObject(writer, usernames);
            JSON_ENDOBJ
            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), STRING_onThreadMultiDevicesEvent.c_str(), json.c_str());
        }

        void undisturbMultiDevicesEvent(const string& data) override {
            CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), STRING_onUndisturbMultiDevicesEvent.c_str(), data.c_str());
        }


        void onRoamDeleteMultiDevicesEvent(const std::string& conversationId, const std::string& deviceId, const std::vector<std::string>& msgIdList, int64_t beforeTimeStamp)
        {
            JSON_STARTOBJ
            writer.Key("operation");
            writer.Int(MultiDevices::MultiDevicesOperationToInt(MultiDevicesOperation::UNKNOW));
            writer.Key("convId");
            writer.String(conversationId.c_str());
            writer.Key("deviceId");
            writer.String(deviceId.c_str());
            JSON_ENDOBJ
            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), STRING_onRoamDeleteMultiDevicesEvent.c_str(), json.c_str());
        }

        void onConversationMultiDevicesEvent(MultiDevicesOperation operation, const std::string& conversationId, EMConversation::EMConversationType type)
        {
            JSON_STARTOBJ
            writer.Key("operation");
            writer.Int(MultiDevices::MultiDevicesOperationToInt(operation));
            writer.Key("convId");
            writer.String(conversationId.c_str());
            writer.Key("type");
            writer.Int(Conversation::ConversationTypeToInt(type));
            JSON_ENDOBJ
                string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_MULTIDEVICE_LISTENER.c_str(), STRING_onConversationMultiDevicesEvent.c_str(), json.c_str());
        }
    };

    class GroupManagerListener : public EMGroupManagerListener
    {
    public:
        void onReceiveInviteFromGroup(const string groupId, const string groupName, const string& inviter, const string& inviteMessage) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(groupId.c_str());

            writer.Key("name");
            writer.String(groupName.c_str());

            writer.Key("userId");
            writer.String(inviter.c_str());

            writer.Key("msg");
            writer.String(inviteMessage.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onInvitationReceivedFromGroup.c_str(), json.c_str());
        }

        void onReceiveInviteAcceptionFromGroup(const EMGroupPtr group, const string& invitee) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("userId");
            writer.String(invitee.c_str());

            //writer.Key("msg");
            //writer.String("");

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onInvitationAcceptedFromGroup.c_str(), json.c_str());
        }

        void onReceiveInviteDeclineFromGroup(const EMGroupPtr group, const string& invitee, const string& reason) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            //writer.Key("userId");
            //writer.String(invitee.c_str());

            writer.Key("msg");
            writer.String(reason.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onInvitationDeclinedFromGroup.c_str(), json.c_str());
        }

        void onAutoAcceptInvitationFromGroup(const EMGroupPtr group, const string& inviter, const string& inviteMessage) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("userId");
            writer.String(inviter.c_str());

            writer.Key("msg");
            writer.String(inviteMessage.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onAutoAcceptInvitationFromGroup.c_str(), json.c_str());
        }

        void onLeaveGroup(const EMGroupPtr group, EMMuc::EMMucLeaveReason reason) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("name");
            writer.String(group->groupSubject().c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (EMMuc::EMMucLeaveReason::BE_KICKED == reason) {
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onUserRemovedFromGroup.c_str(), json.c_str());
                return;
            }
            if (EMMuc::EMMucLeaveReason::DESTROYED == reason) {
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onDestroyedFromGroup.c_str(), json.c_str());
                return;
            }
        }

        void onReceiveJoinGroupApplication(const EMGroupPtr group, const string& from, const string& message) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("name");
            writer.String(group->groupSubject().c_str());

            writer.Key("userId");
            writer.String(from.c_str());

            writer.Key("msg");
            writer.String(message.c_str());

            JSON_ENDOBJ

           string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onRequestToJoinReceivedFromGroup.c_str(), json.c_str());
        }

        void onReceiveAcceptionFromGroup(const EMGroupPtr group) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("name");
            writer.String(group->groupSubject().c_str());
          
            writer.Key("userId");
            writer.String("");

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onRequestToJoinAcceptedFromGroup.c_str(), json.c_str());
        }

        void onReceiveRejectionFromGroup(const string& groupId, const string& reason, const std::string& decliner, const std::string& applicant) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(groupId.c_str());

            writer.Key("msg");
            writer.String(reason.c_str());

            writer.Key("decliner");
            writer.String(decliner.c_str());

            writer.Key("applicant");
            writer.String(applicant.c_str());

            JSON_ENDOBJ

           string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onRequestToJoinDeclinedFromGroup.c_str(), json.c_str());
        }

        void onUpdateMyGroupList(const vector<EMGroupPtr>& list) override {
            //no corresponding delegate defined in API
        }

        void onAddMutesFromGroup(const EMGroupPtr group, const vector<string>& mutes, int64_t muteExpire) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("expireTime");
            writer.Int64(muteExpire);

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, mutes);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onMuteListAddedFromGroup.c_str(), json.c_str());
        }

        void onRemoveMutesFromGroup(const EMGroupPtr group, const vector<string>& mutes) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, mutes);

            JSON_ENDOBJ

             string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onMuteListRemovedFromGroup.c_str(), json.c_str());
        }

        void onAddWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const vector<string>& members) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, members);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onAddWhiteListMembersFromGroup.c_str(), json.c_str());

        }

        void onRemoveWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const vector<string>& members) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, members);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onRemoveWhiteListMembersFromGroup.c_str(), json.c_str());
        }

        void onAllMemberMuteChangedFromGroup(const easemob::EMGroupPtr Group, bool isAllMuted) override {

            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("isMuteAll");
            writer.Bool(isAllMuted);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onAllMemberMuteChangedFromGroup.c_str(), json.c_str());
        }

        void onAddAdminFromGroup(const EMGroupPtr Group, const string& admin) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(Group->groupId().c_str());

            writer.Key("userId");
            writer.String(admin.c_str());

            JSON_ENDOBJ

             string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onAdminAddedFromGroup.c_str(), json.c_str());
        }

        void onRemoveAdminFromGroup(const EMGroupPtr group, const string& admin) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("userId");
            writer.String(admin.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onAdminRemovedFromGroup.c_str(), json.c_str());
        }

        void onAssignOwnerFromGroup(const EMGroupPtr group, const string& newOwner, const string& oldOwner) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("newOwner");
            writer.String(newOwner.c_str());

            writer.Key("oldOwner");
            writer.String(oldOwner.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onOwnerChangedFromGroup.c_str(), json.c_str());
        }

        void onMemberJoinedGroup(const EMGroupPtr group, const string& member) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("userId");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onMemberJoinedFromGroup.c_str(), json.c_str());
        }

        void onMemberLeftGroup(const EMGroupPtr group, const string& member) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("userId");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onMemberExitedFromGroup.c_str(), json.c_str());
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
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onAnnouncementChangedFromGroup.c_str(), json.c_str());
        }

        void onUploadSharedFileFromGroup(const EMGroupPtr group, const EMMucSharedFilePtr sharedFile) override {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("file");
            GroupSharedFile::ToJsonObject(writer, sharedFile);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onSharedFileAddedFromGroup.c_str(), json.c_str());
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
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onSharedFileDeletedFromGroup.c_str(), json.c_str());
        }

        void onDisabledStateChangedFromGroup(const easemob::EMGroupPtr group, bool isDisabled)
        {
            JSON_STARTOBJ
            writer.Key("groupId");
            writer.String(group->groupId().c_str());

            writer.Key("isDisabled");
            writer.Bool(isDisabled);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onStateChangedFromGroup.c_str(), json.c_str());
        }

        void onUpdateSpecificationFromGroup(const easemob::EMGroupPtr group)
        {
            JSON_STARTOBJ

            if (nullptr != group) {
                writer.Key("group");
                Group::ToJsonObject(writer, group);
            }

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onSpecificationChangedFromGroup.c_str(), json.c_str());
        }

        void onUpdateMemberAttributesFromGroup(const std::string& groupId, const std::string& username, const std::unordered_map<std::string, std::string>& attributes, const std::string& from)
        {
            JSON_STARTOBJ

            writer.Key("groupId");
            writer.String(groupId.c_str());

            writer.Key("userId");
            writer.String(username.c_str());

            writer.Key("from");
            writer.String(from.c_str());

            writer.Key("attrs");
            MyJson::ToJsonObject(writer, attributes);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_GROUPMANAGER_LISTENER.c_str(), STRING_onUpdateMemberAttributesFromGroup.c_str(), json.c_str());
        }
    };

    class RoomManagerListener : public EMChatroomManagerListener
    {
    public:

        void  onMemberJoinedChatroom(const EMChatroomPtr chatroom, const std::string& member) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userId");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onMemberJoinedFromRoom.c_str(), json.c_str());
        }

        void onLeaveChatroom(const EMChatroomPtr chatroom, EMMuc::EMMucLeaveReason reason) override {

            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("name");
            writer.String(chatroom->chatroomSubject().c_str());

            string json = "";

            if (EMMuc::EMMucLeaveReason::DESTROYED == reason) {

                JSON_ENDOBJ
                json = s.GetString();

                if (json.size() > 0)
                    CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onDestroyedFromRoom.c_str(), json.c_str());

                return;
            }
            if ((EMMuc::EMMucLeaveReason::BE_KICKED == reason)) {

                JSON_ENDOBJ
                json = s.GetString();

                if (json.size() > 0)
                    CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onRemovedFromRoom.c_str(), json.c_str());

                return;
            }
            if ((EMMuc::EMMucLeaveReason::BE_KICKED_FOR_OFFLINE == reason)) {

                JSON_ENDOBJ
                json = s.GetString();

                if (json.size() > 0)
                    CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onRemoveFromRoomByOffline.c_str(), json.c_str());

                return;
            }
        }

        void onMemberLeftChatroom(const EMChatroomPtr chatroom, const std::string& member) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("name");
            writer.String(chatroom->chatroomSubject().c_str());

            writer.Key("userId");
            writer.String(member.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onMemberExitedFromRoom.c_str(), json.c_str());
        }

        void onAddMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string>& mutes, int64_t muteExpire) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, mutes);

            writer.Key("expireTime");
            writer.Int64(muteExpire);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onMuteListAddedFromRoom.c_str(), json.c_str());
        }

        void onRemoveMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string>& mutes) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, mutes);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onMuteListRemovedFromRoom.c_str(), json.c_str());
        }

        void onAddWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string>& members) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, members);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onAddWhiteListMembersFromRoom.c_str(), json.c_str());
        }

        void onRemoveWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string>& members) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userIds");
            MyJson::ToJsonObject(writer, members);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onRemoveWhiteListMembersFromRoom.c_str(), json.c_str());
        }

        void onAllMemberMuteChangedFromChatroom(const easemob::EMChatroomPtr chatroom, bool isAllMuted) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("isAllMuted");
            writer.Bool(isAllMuted);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onAllMemberMuteChangedFromRoom.c_str(), json.c_str());
        }

        void onAddAdminFromChatroom(const EMChatroomPtr chatroom, const std::string& admin) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userId");
            writer.String(admin.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onAdminAddedFromRoom.c_str(), json.c_str());
        }

        void onRemoveAdminFromChatroom(const EMChatroomPtr chatroom, const std::string& admin) override {
            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroom->chatroomId().c_str());

            writer.Key("userId");
            writer.String(admin.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onAdminRemovedFromRoom.c_str(), json.c_str());
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
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onOwnerChangedFromRoom.c_str(), json.c_str());
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
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onAnnouncementChangedFromRoom.c_str(), json.c_str());
        }

        void onChatroomAttributesChanged(const std::string chatroomId, const std::string& ext, std::string from) override {

            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroomId.c_str());

            writer.Key("userId");
            writer.String(from.c_str());

            map<string, string> ext_map = Room::FromJsonToRoomSuccessAttribute(ext);
            writer.Key("kv");
            MyJson::ToJsonObject(writer, ext_map);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onChatroomAttributesChanged.c_str(), json.c_str());
        }

        void onChatroomAttributesRemoved(const std::string chatroomId, const std::string& ext, std::string from) override {

            JSON_STARTOBJ
            writer.Key("roomId");
            writer.String(chatroomId.c_str());

            writer.Key("userId");
            writer.String(from.c_str());

            vector<string> list = Room::FromJsonToRoomSuccessKeys(ext);
            writer.Key("list");
            MyJson::ToJsonObject(writer, list);

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onChatroomAttributesRemoved.c_str(), json.c_str());
        }

        void onUpdateSpecificationFromChatroom(const easemob::EMChatroomPtr chatroom_) override {

            JSON_STARTOBJ

            if (nullptr != chatroom_) {
                writer.Key("room");
                Room::ToJsonObject(writer, chatroom_);
            }

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_ROOMMANAGER_LISTENER.c_str(), STRING_onSpecificationChangedFromRoom.c_str(), json.c_str());
        }
    };

    class ContactManagerListener : public EMContactListener
    {
    public:
        void onContactAdded(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("userId");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), STRING_onContactAdded.c_str(), json.c_str());
        }
        void onContactDeleted(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("userId");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), STRING_onContactDeleted.c_str(), json.c_str());
        }
        void onContactInvited(const std::string& username, std::string& reason) override {
            JSON_STARTOBJ

            writer.Key("userId");
            writer.String(username.c_str());

            writer.Key("msg");
            writer.String(reason.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), STRING_onContactInvited.c_str(), json.c_str());
        }
        void onContactAgreed(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("userId");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), STRING_onFriendRequestAccepted.c_str(), json.c_str());
        }
        void onContactRefused(const std::string& username) override {
            JSON_STARTOBJ

            writer.Key("userId");
            writer.String(username.c_str());

            JSON_ENDOBJ

            string json = s.GetString();

            if (json.size() > 0)
                CallBack(STRING_CONTACTMANAGER_LISTENER.c_str(), STRING_onFriendRequestDeclined.c_str(), json.c_str());
        }
    };

    class PresenceManagerListener : public EMPresenceManagerListener
    {
    public:

        void onPresenceUpdated(const std::vector<EMPresencePtr>& presence) override {
            string json = Presence::ToJson(presence);
            if (json.size() > 0)
                CallBack(STRING_PRESENCEMANAGER_LISTENER.c_str(), STRING_onPresenceUpdated.c_str(), json.c_str());
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
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), STRING_onChatThreadCreate.c_str(), json.c_str());
            }
            else if (event->threadOperation().compare("delete") == 0) {
                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), STRING_onChatThreadDestroy.c_str(), json.c_str());
            }
            else if (event->threadOperation().compare("update") == 0 ||
                event->threadOperation().compare("update_msg") == 0) {

                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), STRING_onChatThreadUpdate.c_str(), json.c_str());

            }
        }

        void onLeaveThread(const EMThreadEventPtr event, EMThreadLeaveReason reason) override {

            std::string json = ChatThreadEvent::ToJson(event);

            switch (reason)
            {
            case EMThreadLeaveReason::BE_KICKED:

                if (json.size() > 0)
                    CallBack(STRING_THREADMANAGER_LISTENER.c_str(), STRING_onUserKickOutOfChatThread.c_str(), json.c_str());

                break;
            default:
                break;
            }
        }

        void onMemberJoined(const EMThreadEventPtr event) override {
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
                CallBack(STRING_CHATMANAGER_LISTENER.c_str(), STRING_onMessageReactionDidChange.c_str(), json.c_str());
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
