#ifndef _CALLBACKS_H_
#define _CALLBACKS_H_

#include "models.h"
#include "LogHelper.h"

#include "emclient.h"
#include "emconnection_listener.h"
#include "emerror.h"
#include "emmessagebody.h"
#include "emcontactlistener.h"

using namespace easemob;

//callback entries definition
#if defined(_WIN32)
    //Callback
    typedef void(__stdcall *FUNC_OnSuccess)(int callbackId);
    typedef void(__stdcall *FUNC_OnSuccess_With_Result)(void * data[], DataType type, int size, int callbackId);
    typedef void(__stdcall *FUNC_OnSuccess_With_Result_V2)(void * header, void * data[], DataType type, int size, int callbackId);
    typedef void(__stdcall *FUNC_OnError)(int, const char *, int);
    typedef void(__stdcall *FUNC_OnProgress)(int, int);

    //Connection Listeners
    typedef void(__stdcall *FUNC_OnConnected)();
    typedef void(__stdcall *FUNC_OnDisconnected)(int);

    //ChatManager Listeners
    typedef void (__stdcall *FUNC_OnMessagesReceived)(void * messages[],
                                                      EMMessageBody::EMMessageBodyType types[], int size);
    typedef void (__stdcall *FUNC_OnCmdMessagesReceived)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnMessagesRead)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnMessagesDelivered)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnMessagesRecalled)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnReadAckForGroupMessageUpdated)();
    typedef void (__stdcall *FUNC_OnGroupMessageRead)(GroupReadAck acks[], int size);
    typedef void (__stdcall *FUNC_OnConversationsUpdate)();
    typedef void (__stdcall *FUNC_OnConversationRead)(const char * from, const char * to);

    //GroupManager Listener
    typedef void (__stdcall *FUNC_OnInvitationReceived)(const char * groupId, const char * groupName, const char * inviter, const char * reason);
    typedef void (__stdcall *FUNC_OnRequestToJoinReceived)(const char * groupId, const char * groupName, const char * applicant, const char * reason);
    typedef void (__stdcall *FUNC_OnRequestToJoinAccepted)(const char * groupId, const char * groupName, const char * accepter);
    typedef void (__stdcall *FUNC_OnRequestToJoinDeclined)(const char * groupId, const char * groupName, const char * decliner, const char * reason);
    typedef void (__stdcall *FUNC_OnInvitationAccepted)(const char * groupId, const char * invitee, const char * reason);
    typedef void (__stdcall *FUNC_OnInvitationDeclined)(const char * groupId, const char * invitee, const char * reason);
    typedef void (__stdcall *FUNC_OnUserRemoved)(const char * groupId, const char * groupName);
    typedef void (__stdcall *FUNC_OnGroupDestroyed)(const char * groupId, const char * groupName);
    typedef void (__stdcall *FUNC_OnAutoAcceptInvitationFromGroup)(const char * groupId, const char * inviter, const char * inviteMessage);
    typedef void (__stdcall *FUNC_OnMuteListAdded)(const char * groupId, const char * mutes[], int size, int muteExpire);
    typedef void (__stdcall *FUNC_OnMuteListRemoved)(const char * groupId, const char * mutes[], int size);
    typedef void (__stdcall *FUNC_OnAdminAdded)(const char * groupId, const char * administrator);
    typedef void (__stdcall *FUNC_OnAdminRemoved)(const char * groupId, const char * administrator);
    typedef void (__stdcall *FUNC_OnOwnerChanged)(const char * groupId, const char * newOwner, const char * oldOwner);
    typedef void (__stdcall *FUNC_OnMemberJoined)(const char * groupId, const char * member);
    typedef void (__stdcall *FUNC_OnMemberExited)(const char * groupId, const char * member);
    typedef void (__stdcall *FUNC_OnAnnouncementChanged)(const char * groupId, const char * announcement);
    typedef void (__stdcall *FUNC_OnSharedFileAdded)(const char * groupId, GroupSharedFileTO sharedFile);
    typedef void (__stdcall *FUNC_OnSharedFileDeleted)(const char * groupId, const char * fileId);

    //RoomManager Listener
    typedef void (__stdcall *FUNC_OnChatRoomDestroyed)(__stdcall const char * roomId, const char * roomName);
    typedef void (__stdcall *FUNC_OnRemovedFromChatRoom)(__stdcall const char * roomId, const char * roomName, const char * participant);
    typedef void (__stdcall *FUNC_OnMemberExitedFromRoom)(const char * roomId, const char * roomName, const char * member);
#else
    //Callback
    typedef void(*FUNC_OnSuccess)(int callbackId);
    typedef void(*FUNC_OnSuccess_With_Result)(void * data[], DataType type, int size, int callbackId);
    typedef void(*FUNC_OnSuccess_With_Result_V2)(void * header, void * data[], DataType type, int size, int callbackId);
    typedef void(*FUNC_OnError)(int, const char *, int);
    typedef void(*FUNC_OnProgress)(int, int);

    //Connection Listener
    typedef void(*FUNC_OnConnected)();
    typedef void(*FUNC_OnDisconnected)(int);
    typedef void(*FUNC_OnPong)();

    //ChatManager Listener
    typedef void (*FUNC_OnMessagesReceived)(void * messages[],EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnCmdMessagesReceived)(void * messages[],EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnMessagesRead)(void * messages[],EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnMessagesDelivered)(void * messages[],EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnMessagesRecalled)(void * messages[],EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnReadAckForGroupMessageUpdated)();
    typedef void (*FUNC_OnGroupMessageRead)(void * acks[], int size);
    typedef void (*FUNC_OnConversationsUpdate)();
    typedef void (*FUNC_OnConversationRead)(const char * from, const char * to);

    //GroupManager Listener
    typedef void (*FUNC_OnInvitationReceived)(const char * groupId, const char * groupName, const char * inviter, const char * reason);
    typedef void (*FUNC_OnRequestToJoinReceived)(const char * groupId, const char * groupName, const char * applicant, const char * reason);
    typedef void (*FUNC_OnRequestToJoinAccepted)(const char * groupId, const char * groupName, const char * accepter);
    typedef void (*FUNC_OnRequestToJoinDeclined)(const char * groupId, const char * groupName, const char * decliner, const char * reason);
    typedef void (*FUNC_OnInvitationAccepted)(const char * groupId, const char * invitee, const char * reason);
    typedef void (*FUNC_OnInvitationDeclined)(const char * groupId, const char * invitee, const char * reason);
    typedef void (*FUNC_OnUserRemoved)(const char * groupId, const char * groupName);
    typedef void (*FUNC_OnGroupDestroyed)(const char * groupId, const char * groupName);
    typedef void (*FUNC_OnAutoAcceptInvitationFromGroup)(const char * groupId, const char * inviter, const char * inviteMessage);
    typedef void (*FUNC_OnMuteListAdded)(const char * groupId, const char * mutes[], int size, int muteExpire);
    typedef void (*FUNC_OnMuteListRemoved)(const char * groupId, const char * mutes[], int size);
    typedef void (*FUNC_OnAdminAdded)(const char * groupId, const char * administrator);
    typedef void (*FUNC_OnAdminRemoved)(const char * groupId, const char * administrator);
    typedef void (*FUNC_OnOwnerChanged)(const char * groupId, const char * newOwner, const char * oldOwner);
    typedef void (*FUNC_OnMemberJoined)(const char * groupId, const char * member);
    typedef void (*FUNC_OnMemberExited)(const char * groupId, const char * member);
    typedef void (*FUNC_OnAnnouncementChanged)(const char * groupId, const char * announcement);
    typedef void (*FUNC_OnSharedFileAdded)(const char * groupId, void * sharedFile[], int size);
    typedef void (*FUNC_OnSharedFileDeleted)(const char * groupId, const char * fileId);

    //RoomManager Listener
    typedef void (*FUNC_OnChatRoomDestroyed)(const char * roomId, const char * roomName);
    typedef void (*FUNC_OnRemovedFromChatRoom)(const char * roomId, const char * roomName, const char * participant);
    typedef void (*FUNC_OnMemberExitedFromRoom)(const char * roomId, const char * roomName, const char * member);

    //ContactManager Listener
    typedef void (*FUNC_OnContactAdded)(const char * username);
    typedef void (*FUNC_OnContactDeleted)(const char * username);
    typedef void (*FUNC_OnContactInvited)(const char * username, const char * reason);
    typedef void (*FUNC_OnFriendRequestAccepted)(const char * username);
    typedef void (*FUNC_OnFriendRequestDeclined)(const char * username);

#endif

class ConnectionListener : public EMConnectionListener
{
public:
    ConnectionListener(FUNC_OnConnected connected, FUNC_OnDisconnected disconnected, FUNC_OnPong pong) : onConnected(connected), onDisconnected(disconnected), onPonged(pong){}
    void onConnect() override {
        LOG("Connection established.");
        if(onConnected)
            onConnected();
    }
    void onDisconnect(EMErrorPtr error) override {
        LOG("Connection disconnected with code=%d.", error->mErrorCode);
        if(onDisconnected)
            onDisconnected(error->mErrorCode);
    }

    void onPong() override {
        LOG("Server ponged.");
        if(onPonged)
            onPonged();
    }
    
private:
    FUNC_OnConnected onConnected;
    FUNC_OnDisconnected onDisconnected;
    FUNC_OnPong onPonged;
};

class ChatManagerListener : public EMChatManagerListener
{
public:
    ChatManagerListener(FUNC_OnMessagesReceived onMessagesReceived, FUNC_OnCmdMessagesReceived onCmdMessagesReceived, FUNC_OnMessagesRead onMessagesRead, FUNC_OnMessagesDelivered onMessagesDelivered, FUNC_OnMessagesRecalled onMessagesRecalled, FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated, FUNC_OnGroupMessageRead onGroupMessageRead, FUNC_OnConversationsUpdate onConversationsUpdate, FUNC_OnConversationRead onConversationRead):onMessagesReceived(onMessagesReceived),onCmdMessagesReceived(onCmdMessagesReceived),onMessagesRead(onMessagesRead),onMessagesDelivered(onMessagesDelivered),onMessagesRecalled(onMessagesRecalled),onReadAckForGroupMessageUpdated(onReadAckForGroupMessageUpdated), onGroupMessageRead(onGroupMessageRead),onConversationsUpdate(onConversationsUpdate),onConversationRead(onConversationRead) {}
    
    void onReceiveMessages(const EMMessageList &messages) override {
        size_t size = messages.size();
        LOG("%d messages received!", size);
        
        void * result[size];
        EMMessageBody::EMMessageBodyType types[size];
        int i = 0;
        for(EMMessagePtr message : messages) {
            MessageTO *mto = MessageTO::FromEMMessage(message);
            result[i] = mto;
            types[i] = mto->BodyType;
            i++;
        }
        if(onMessagesReceived) {
            LOG("Call onMessagesReceived delegate in managed side...");
            onMessagesReceived(result, types, (int)size);
        }
        //release memory manually
        for(void * message : result) {
            MessageTO::FreeResource((MessageTO*)message);
            delete (MessageTO *)message;
        }
    }
    
    void onReceiveCmdMessages(const EMMessageList &messages) override {
        LOG("%d cmd messages received!", messages.size());
        
        size_t size = messages.size();
        void * result[size];
        EMMessageBody::EMMessageBodyType types[size];
        int i = 0;
        for(EMMessagePtr message : messages) {
            MessageTO *mto = MessageTO::FromEMMessage(message);
            result[i] = mto;
            types[i] = mto->BodyType;
            i++;
        }
        if(onCmdMessagesReceived) {
            LOG("Call onCmdMessagesReceived delegate in managed side...");
            onCmdMessagesReceived(result, types, (int)size);
        }
        //release memory manually
        for(void * message : result) {
            MessageTO::FreeResource((MessageTO*)message);
            delete (MessageTO *)message;
        }
    }
    
    void onReceiveHasReadAcks(const EMMessageList &messages) override{
        LOG("%d messages read!", messages.size());
        
        size_t size = messages.size();
        void * result[size];
        EMMessageBody::EMMessageBodyType types[size];
        int i = 0;
        for(EMMessagePtr message : messages) {
            MessageTO *mto = MessageTO::FromEMMessage(message);
            result[i] = mto;
            types[i] = mto->BodyType;
            i++;
        }
        if(onMessagesRead) {
            LOG("Call onMessagesRead delegate in managed side...");
            onMessagesRead(result, types, (int)size);
        }
        //release memory manually
        for(void * message : result) {
            MessageTO::FreeResource((MessageTO*)message);
            delete (MessageTO *)message;
        }
    }
    
    void onReceiveHasDeliveredAcks(const EMMessageList &messages) override{
        LOG("%d messages delivered!", messages.size());
        
        size_t size = messages.size();
        void * result[size];
        EMMessageBody::EMMessageBodyType types[size];
        int i = 0;
        for(EMMessagePtr message : messages) {
            MessageTO *mto = MessageTO::FromEMMessage(message);
            result[i] = mto;
            types[i] = mto->BodyType;
            i++;
        }
        if(onMessagesDelivered) {
            LOG("Call onMessagesDelivered delegate in managed side...");
            onMessagesDelivered(result, types, (int)size);
        }
        //release memory manually
        for(void * message : result) {
            MessageTO::FreeResource((MessageTO*)message);
            delete (MessageTO *)message;
        }
    }
    
    void onReceiveRecallMessages(const EMMessageList &messages) override {
        LOG("%d messages recalled!", messages.size());
        
        size_t size = messages.size();
        void * result[size];
        EMMessageBody::EMMessageBodyType types[size];
        int i = 0;
        for(EMMessagePtr message : messages) {
            MessageTO *mto = MessageTO::FromEMMessage(message);
            result[i] = mto;
            types[i] = mto->BodyType;
            i++;
        }
        if(onMessagesRecalled) {
            LOG("Call onMessagesRecalled delegate in managed side...");
            onMessagesRecalled(result, types, (int)size);
        }
        //release memory manually
        for(void * message : result) {
            MessageTO::FreeResource((MessageTO*)message);
            delete (MessageTO *)message;
        }
    }
    
    void onUpdateGroupAcks() override {
        if(onReadAckForGroupMessageUpdated) {
            LOG("Call onReadAckForGroupMessageUpdated delegate in managed side...");
            onReadAckForGroupMessageUpdated();
        }
    }
    
    void onReceiveReadAcksForGroupMessage(const EMGroupReadAckList &acks) override {
        LOG("%d group messages read!", acks.size());
        
        size_t size = acks.size();
        void * result[size];
        int i = 0;
        for(EMGroupReadAckPtr ack : acks) {
            GroupReadAckTO *gto = GroupReadAckTO::FromGroupReadAck(ack);
            result[i] = gto;
            i++;
        }
        if(onGroupMessageRead) {
            LOG("Call onGroupMessageRead delegate in managed side...");
            onGroupMessageRead(result, (int)size);
        }
        //release memory manually
        for(void * gto : result) {
            delete (GroupReadAckTO *)gto;
        }
    }
    
    void onUpdateConversationList(const EMConversationList &conversations) override {
        LOG("%d conversation updated!");
        if(onConversationsUpdate) {
            LOG("Call onConversationsUpdate delegate in managed side...");
            onConversationsUpdate();
        }
    }
    
    void onReceiveReadAckForConversation(const std::string& fromUsername, const std::string& toUsername) override {
        LOG("Receive read ack for conversation to:%s, from:%s", toUsername.c_str(), fromUsername.c_str());
        if(onConversationRead) {
            LOG("Call onConversationRead delegate in managed side...");
            onConversationRead(fromUsername.c_str(), toUsername.c_str());
        }
    }
private:
    FUNC_OnMessagesReceived onMessagesReceived;
    FUNC_OnCmdMessagesReceived onCmdMessagesReceived;
    FUNC_OnMessagesRead onMessagesRead;
    FUNC_OnMessagesDelivered onMessagesDelivered;
    FUNC_OnMessagesRecalled onMessagesRecalled;
    FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated;
    FUNC_OnGroupMessageRead onGroupMessageRead;
    FUNC_OnConversationsUpdate onConversationsUpdate;
    FUNC_OnConversationRead onConversationRead;
};

class GroupManagerListener : public EMGroupManagerListener
{
public:
    GroupManagerListener(void * client, FUNC_OnInvitationReceived onInvitationReceived, FUNC_OnRequestToJoinReceived onRequestToJoinReceived, FUNC_OnRequestToJoinAccepted onRequestToJoinAccepted, FUNC_OnRequestToJoinDeclined onRequestToJoinDeclined, FUNC_OnInvitationAccepted onInvitationAccepted, FUNC_OnInvitationDeclined onInvitationDeclined, FUNC_OnUserRemoved onUserRemoved, FUNC_OnGroupDestroyed onGroupDestroyed, FUNC_OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroupCB, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved, FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited, FUNC_OnAnnouncementChanged onAnnouncementChanged, FUNC_OnSharedFileAdded onSharedFileAdded, FUNC_OnSharedFileDeleted onSharedFileDeleted):client(client),onInvitationReceived(onInvitationReceived),onRequestToJoinReceived(onRequestToJoinReceived),onRequestToJoinAccepted(onRequestToJoinAccepted),onRequestToJoinDeclined(onRequestToJoinDeclined),onInvitationAccepted(onInvitationAccepted),onInvitationDeclined(onInvitationDeclined),onUserRemoved(onUserRemoved),onGroupDestroyed(onGroupDestroyed),onAutoAcceptInvitationFromGroupCB(onAutoAcceptInvitationFromGroupCB),onMuteListAdded(onMuteListAdded),onMuteListRemoved(onMuteListRemoved),onAdminAdded(onAdminAdded),onAdminRemoved(onAdminRemoved),onOwnerChanged(onOwnerChanged),onMemberJoined(onMemberJoined),onMemberExited(onMemberExited),onAnnouncementChanged(onAnnouncementChanged),onSharedFileAdded(onSharedFileAdded),onSharedFileDeleted(onSharedFileDeleted) {}
    
    void onReceiveInviteFromGroup(const std::string groupId, const std::string& inviter, const std::string& inviteMessage) override {
        if(onInvitationReceived) {
            auto group = CLIENT->getGroupManager().groupWithId(groupId);
            auto groupName = group->groupSubject();
            onInvitationReceived(groupId.c_str(), groupName.c_str(), inviter.c_str(), inviteMessage.c_str());
        }
    }
    
    void onReceiveInviteAcceptionFromGroup(const EMGroupPtr group, const std::string& invitee) override {
        if(onInvitationAccepted) {
            auto groupName = group->groupSubject();
            onInvitationAccepted(group->groupId().c_str(), invitee.c_str(), "");
        }
    }

    void onReceiveInviteDeclineFromGroup(const EMGroupPtr group, const std::string& invitee, const std::string &reason) override {
        if(onInvitationDeclined) {
            onInvitationDeclined(group->groupId().c_str(), invitee.c_str(), reason.c_str());
        }
    }

    void onAutoAcceptInvitationFromGroup(const EMGroupPtr group, const std::string& inviter, const std::string& inviteMessage) override {
        if(onAutoAcceptInvitationFromGroupCB) {
            onAutoAcceptInvitationFromGroupCB(group->groupId().c_str(), inviter.c_str(), inviteMessage.c_str());
        }
    }

    void onLeaveGroup(const EMGroupPtr group, EMMuc::EMMucLeaveReason reason) override {
        
        if(onUserRemoved && EMMuc::EMMucLeaveReason::BE_KICKED == reason) {
            auto groupName = group->groupSubject();
            onUserRemoved(group->groupId().c_str(), groupName.c_str());
            return;
        }
        if(onGroupDestroyed && EMMuc::EMMucLeaveReason::DESTROYED == reason) {
            auto groupName = group->groupSubject();
            onGroupDestroyed(group->groupId().c_str(), groupName.c_str());
            return;
        }
    }

    void onReceiveJoinGroupApplication(const EMGroupPtr group, const std::string& from, const std::string& message) override {
        if(onRequestToJoinReceived) {
            auto groupName = group->groupSubject();
            onRequestToJoinReceived(group->groupId().c_str(), groupName.c_str(), from.c_str(), message.c_str());
        }
    }

    void onReceiveAcceptionFromGroup(const EMGroupPtr group) override {
        if(onRequestToJoinAccepted) {
            auto groupName = group->groupSubject();
            onRequestToJoinAccepted(group->groupId().c_str(), groupName.c_str(), "");
        }
    }

    void onReceiveRejectionFromGroup(const std::string &groupId, const std::string &reason) override {
        if(onRequestToJoinDeclined) {
            auto group = CLIENT->getGroupManager().groupWithId(groupId);
            auto groupName = group->groupSubject();
            onRequestToJoinDeclined(groupId.c_str(), groupName.c_str(), "", reason.c_str());
        }
    }

    void onUpdateMyGroupList(const std::vector<EMGroupPtr> &list) override {
        //no corresponding delegate defined in API
    }

 
    void onAddMutesFromGroup(const EMGroupPtr group, const std::vector<std::string> &mutes, int64_t muteExpire) override {
        if(onMuteListAdded) {
            int size = (int)mutes.size();
            //convert vector to array
            const char * muteArray[size];
            for(size_t i=0; i<mutes.size(); i++) {
                char * ptr = new char[mutes[i].size()+1];
                strncpy(ptr, mutes[i].c_str(), mutes.size()+1);
                muteArray[i] = ptr;
            }
            onMuteListAdded(group->groupId().c_str(), muteArray, size, (int)muteExpire);
        }
    }

 
    void onRemoveMutesFromGroup(const EMGroupPtr group, const std::vector<std::string> &mutes) override {
        if(onMuteListRemoved) {
            int size = (int)mutes.size();
            //convert vector to array
            const char * muteArray[size];
            for(size_t i=0; i<mutes.size(); i++) {
                char * ptr = new char[mutes[i].size()+1];
                strncpy(ptr, mutes[i].c_str(), mutes.size()+1);
                muteArray[i] = ptr;
            }
            onMuteListRemoved(group->groupId().c_str(), muteArray, size);
        }
    }
       

    void onAddWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const std::vector<std::string> &members) override {
        //no corresponding delegate defined in API
    }
       
       
    void onRemoveWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const std::vector<std::string> &members) override {
        //no corresponding delegate defined in API
    }
       

    void onAllMemberMuteChangedFromGroup(const easemob::EMGroupPtr Group, bool isAllMuted) override {
        //no corresponding delegate defined in API
    }

  
    void onAddAdminFromGroup(const EMGroupPtr group, const std::string& admin) override {
        if(onAdminAdded) {
            onAdminAdded(group->groupId().c_str(), admin.c_str());
        }
    }


    void onRemoveAdminFromGroup(const EMGroupPtr group, const std::string& admin) override {
        if(onAdminRemoved) {
            onAdminRemoved(group->groupId().c_str(), admin.c_str());
        }
    }


    void onAssignOwnerFromGroup(const EMGroupPtr group, const std::string& newOwner, const std::string& oldOwner) override {
        if(onOwnerChanged) {
            onOwnerChanged(group->groupId().c_str(), newOwner.c_str(), oldOwner.c_str());
        }
    }
       

    void onMemberJoinedGroup(const EMGroupPtr group, const std::string& member) override {
        if(onMemberJoined) {
            onMemberJoined(group->groupId().c_str(), member.c_str());
        }
    }

    void onMemberLeftGroup(const EMGroupPtr group, const std::string& member) override {
        if(onMemberExited) {
            onMemberExited(group->groupId().c_str(), member.c_str());
        }
    }

    void onUpdateAnnouncementFromGroup(const EMGroupPtr group, const std::string& announcement) override {
        if(onAnnouncementChanged) {
            onAnnouncementChanged(group->groupId().c_str(), announcement.c_str());
        }
    }

    void onUploadSharedFileFromGroup(const EMGroupPtr group, const EMMucSharedFilePtr sharedFile) override {
        if(onSharedFileAdded) {
            if(sharedFile) {
                GroupSharedFileTO* groupSharedFileTO = GroupSharedFileTO::FromEMGroupSharedFile(sharedFile);
                GroupSharedFileTO* groupSharedFileTOArray[1] = {groupSharedFileTO};
                onSharedFileAdded(group->groupId().c_str(), (void **)groupSharedFileTOArray, 1);
                GroupSharedFileTO::DeleteGroupSharedFileTO(groupSharedFileTO);
            }
        }
    }

    void onDeleteSharedFileFromGroup(const EMGroupPtr group, const std::string& fileId) override {
        if(onSharedFileDeleted) {
            onSharedFileDeleted(group->groupId().c_str(), fileId.c_str());
        }
    }
    
private:
    void * client;
    FUNC_OnInvitationReceived onInvitationReceived;
    FUNC_OnRequestToJoinReceived onRequestToJoinReceived;
    FUNC_OnRequestToJoinAccepted onRequestToJoinAccepted;
    FUNC_OnRequestToJoinDeclined onRequestToJoinDeclined;
    FUNC_OnInvitationAccepted onInvitationAccepted;
    FUNC_OnInvitationDeclined onInvitationDeclined;
    FUNC_OnUserRemoved onUserRemoved;
    FUNC_OnGroupDestroyed onGroupDestroyed;
    FUNC_OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroupCB;
    FUNC_OnMuteListAdded onMuteListAdded;
    FUNC_OnMuteListRemoved onMuteListRemoved;
    FUNC_OnAdminAdded onAdminAdded;
    FUNC_OnAdminRemoved onAdminRemoved;
    FUNC_OnOwnerChanged onOwnerChanged;
    FUNC_OnMemberJoined onMemberJoined;
    FUNC_OnMemberExited onMemberExited;
    FUNC_OnAnnouncementChanged onAnnouncementChanged;
    FUNC_OnSharedFileAdded onSharedFileAdded;
    FUNC_OnSharedFileDeleted onSharedFileDeleted;
};

class RoomManagerListener : public EMChatroomManagerListener
{
public:
    RoomManagerListener(void * client, FUNC_OnChatRoomDestroyed onChatRoomDestroyed, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExitedFromRoom onMemberExited,
                        FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved,
                        FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnAnnouncementChanged onAnnouncementChanged ):client(client), onChatRoomDestroyed(onChatRoomDestroyed), onMemberJoined(onMemberJoined), onMemberExited(onMemberExited), onRemovedFromChatRoom(onRemovedFromChatRoom), onMuteListAdded(onMuteListAdded), onMuteListRemoved(onMuteListRemoved), onAdminAdded(onAdminAdded), onAdminRemoved(onAdminRemoved), onOwnerChanged(onOwnerChanged), onAnnouncementChanged(onAnnouncementChanged) {}
    
    void  onMemberJoinedChatroom(const EMChatroomPtr chatroom, const std::string &member) override {
        if(onMemberJoined) {
            onMemberJoined(chatroom->chatroomId().c_str(), member.c_str());
        }
    }
    
    void onLeaveChatroom(const EMChatroomPtr chatroom, EMMuc::EMMucLeaveReason reason) override {
        if(EMMuc::EMMucLeaveReason::DESTROYED == reason && onChatRoomDestroyed) {
            onChatRoomDestroyed(chatroom->chatroomId().c_str(), chatroom->chatroomSubject().c_str());
            return;
        }
        if((EMMuc::EMMucLeaveReason::BE_KICKED == reason && onRemovedFromChatRoom)) {
            //no participant is return!!
            onRemovedFromChatRoom(chatroom->chatroomId().c_str(), chatroom->chatroomSubject().c_str(), "");
            return;
        }
    }
    
    void onMemberLeftChatroom(const EMChatroomPtr chatroom, const std::string &member) override {
        if(onMemberExited) {
            onMemberExited(chatroom->chatroomId().c_str(), chatroom->chatroomSubject().c_str(), member.c_str());
        }
    }

    void onAddMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string> &mutes, int64_t muteExpire) override {
        if(onMuteListAdded) {
            size_t size = mutes.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                char* ptr = new char[mutes[i].size()+1];
                strncpy(ptr, mutes[i].c_str(), mutes[i].size()+1);
                data[i] = ptr;
            }
            onMuteListAdded(chatroom->chatroomId().c_str(), data, (int)size, (int)muteExpire);
        }
    }

    void onRemoveMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string> &mutes) override {
        if(onMuteListRemoved) {
            size_t size = mutes.size();
            const char * data[size];
            for(size_t i=0; i<size; i++) {
                char* ptr = new char[mutes[i].size()+1];
                strncpy(ptr, mutes[i].c_str(), mutes[i].size()+1);
                data[i] = ptr;
            }
            onMuteListRemoved(chatroom->chatroomId().c_str(), data, (int)size);
        }
    }

    void onAddWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string> &members) override {
        
    }
    
    void onRemoveWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string> &members) override {
        
    }
    
    void onAllMemberMuteChangedFromChatroom(const easemob::EMChatroomPtr chatroom, bool isAllMuted) override {
        
    }
    
    void onAddAdminFromChatroom(const EMChatroomPtr chatroom, const std::string &admin) override {
        if(onAdminAdded) {
            onAdminAdded(chatroom->chatroomId().c_str(), admin.c_str());
        }
    }

    void onRemoveAdminFromChatroom(const EMChatroomPtr chatroom, const std::string &admin) override {
        if(onAdminRemoved) {
            onAdminRemoved(chatroom->chatroomId().c_str(), admin.c_str());
        }
    }

    void onAssignOwnerFromChatroom(const EMChatroomPtr chatroom, const std::string &newOwner, const std::string &oldOwner) override {
        if(onOwnerChanged) {
            onOwnerChanged(chatroom->chatroomId().c_str(), newOwner.c_str(), oldOwner.c_str());
        }
    }

    void onUpdateAnnouncementFromChatroom(const EMChatroomPtr chatroom, const std::string &announcement) override {
        if(onAnnouncementChanged) {
            onAnnouncementChanged(chatroom->chatroomId().c_str(), announcement.c_str());
        }
    }
    
private:
    void * client;
    FUNC_OnChatRoomDestroyed onChatRoomDestroyed;
    FUNC_OnMemberJoined onMemberJoined;
    FUNC_OnMemberExitedFromRoom onMemberExited;
    FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom;
    FUNC_OnMuteListAdded onMuteListAdded;
    FUNC_OnMuteListRemoved onMuteListRemoved;
    FUNC_OnAdminAdded onAdminAdded;
    FUNC_OnAdminRemoved onAdminRemoved;
    FUNC_OnOwnerChanged onOwnerChanged;
    FUNC_OnAnnouncementChanged onAnnouncementChanged;
};

class ContactManagerListener : public EMContactListener
{
public:
    ContactManagerListener(FUNC_OnContactAdded _onContactAdded, FUNC_OnContactDeleted _onContactDeleted, FUNC_OnContactInvited _onContactInvited, FUNC_OnFriendRequestAccepted _onFriendRequestAccepted, FUNC_OnFriendRequestDeclined _OnFriendRequestDeclined):onContactAdded_(_onContactAdded),onContactDeleted_(_onContactDeleted),onContactInvited_(_onContactInvited),onFriendRequestAccepted_(_onFriendRequestAccepted),OnFriendRequestDeclined_(_OnFriendRequestDeclined){}
    
    void onContactAdded(const std::string &username) override {
        LOG("receive contactadded from user %s!", username.c_str());
        if(onContactAdded_)
        {
            onContactAdded_(username.c_str());
        }
            
    }
    void onContactDeleted(const std::string &username) override {
        LOG("receive contactdeleted from user %s!", username.c_str());
        if(onContactDeleted_)
        {
            onContactDeleted_(username.c_str());
        }
    }
    void onContactInvited(const std::string &username, std::string &reason) override {
        LOG("receive contactinvited from user %s with reason %s!", username.c_str(), reason.c_str());
        if(onContactInvited_)
        {
            onContactInvited_(username.c_str(), reason.c_str());
        }
    }
    void onContactAgreed(const std::string &username) override {
        LOG("receive contactagreed from user %s!", username.c_str());
        if(onFriendRequestAccepted_)
        {
            onFriendRequestAccepted_(username.c_str());
        }
    }
    void onContactRefused(const std::string &username) override {
        LOG("receive contactrefused from user %s!", username.c_str());
        if(OnFriendRequestDeclined_)
        {
            OnFriendRequestDeclined_(username.c_str());
        }
    }

private:
    FUNC_OnContactAdded onContactAdded_;
    FUNC_OnContactDeleted onContactDeleted_;
    FUNC_OnContactInvited onContactInvited_;
    FUNC_OnFriendRequestAccepted onFriendRequestAccepted_;
    FUNC_OnFriendRequestDeclined OnFriendRequestDeclined_;
};

#endif // _CALLBACKS_H_
