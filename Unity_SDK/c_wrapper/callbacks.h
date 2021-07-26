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
    typedef void(__stdcall *FUNC_OnSuccess)();
    typedef void(__stdcall *FUNC_OnSuccess_With_Result)(void * data[], DataType type, int size);
    typedef void(__stdcall *FUNC_OnError)(int, const char *);
    typedef void(__stdcall *FUNC_OnProgress)(int);
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
    typedef void (*FUNC_OnChatRoomDestroyed)(__stdcall const char * roomId, const char * roomName);
    typedef void (*FUNC_OnRemovedFromChatRoom)(__stdcall const char * roomId, const char * roomName, const char * participant);
#else
    //Callback
    typedef void(*FUNC_OnSuccess)();
    typedef void(*FUNC_OnSuccess_With_Result)(void * data[], DataType type, int size);
    typedef void(*FUNC_OnError)(int, const char *);
    typedef void(*FUNC_OnProgress)(int);
    //Connection Listener
    typedef void(*FUNC_OnConnected)();
    typedef void(*FUNC_OnDisconnected)(int);
    typedef void(*FUNC_OnPong)();
    //ChatManager Listener
    typedef void (*FUNC_OnMessagesReceived)(void * messages[],
                                            EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnCmdMessagesReceived)(void * messages[], int size);
    typedef void (*FUNC_OnMessagesRead)(void * messages[], int size);
    typedef void (*FUNC_OnMessagesDelivered)(void * messages[], int size);
    typedef void (*FUNC_OnMessagesRecalled)(void * messages[], int size);
    typedef void (*FUNC_OnReadAckForGroupMessageUpdated)();
    typedef void (*FUNC_OnGroupMessageRead)(GroupReadAck acks[], int size);
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
    typedef void (*FUNC_OnSharedFileAdded)(const char * groupId, GroupSharedFileTO sharedFile);
    typedef void (*FUNC_OnSharedFileDeleted)(const char * groupId, const char * fileId);
    //RoomManager Listener
    typedef void (*FUNC_OnChatRoomDestroyed)(const char * roomId, const char * roomName);
    typedef void (*FUNC_OnRemovedFromChatRoom)(const char * roomId, const char * roomName, const char * participant);
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
        LOG("Connection discontinued with code=%d.", error->mErrorCode);
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
            delete (MessageTO *)message;
        }
    }
    
    void onReceiveCmdMessages(const EMMessageList &messages) override {
        LOG("%d cmd messages received!", messages.size());
    }
    
    void onReceiveHasReadAcks(const EMMessageList &messages) override{
        LOG("%d messages read!", messages.size());
    }
    
    void onReceiveHasDeliveredAcks(const EMMessageList &messages) override{
        LOG("%d messages delivered!", messages.size());
    }
    
    void onReceiveRecallMessages(const EMMessageList &messages) override {
        LOG("%d messages recalled!", messages.size());
    }
    
    void onUpdateGroupAcks() override {
        
    }
    
    void onUpdateConversationList(const EMConversationList &conversations) override {
        
    }
    
    void onReceiveReadAckForConversation(const std::string& fromUsername, const std::string& toUsername) override {
        
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
    GroupManagerListener(void * client, FUNC_OnInvitationReceived onInvitationReceived, FUNC_OnRequestToJoinReceived onRequestToJoinReceived, FUNC_OnRequestToJoinAccepted onRequestToJoinAccepted, FUNC_OnRequestToJoinDeclined onRequestToJoinDeclined, FUNC_OnInvitationAccepted onInvitationAccepted, FUNC_OnInvitationDeclined onInvitationDeclined, FUNC_OnUserRemoved onUserRemoved, FUNC_OnGroupDestroyed onGroupDestroyed, FUNC_OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroup, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved, FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited, FUNC_OnAnnouncementChanged onAnnouncementChanged, FUNC_OnSharedFileAdded onSharedFileAdded, FUNC_OnSharedFileDeleted onSharedFileDeleted):client(client),onInvitationReceived(onInvitationReceived),onRequestToJoinReceived(onRequestToJoinReceived),onRequestToJoinAccepted(onRequestToJoinAccepted),onRequestToJoinDeclined(onRequestToJoinDeclined),onInvitationAccepted(onInvitationAccepted),onInvitationDeclined(onInvitationDeclined),onUserRemoved(onUserRemoved),onGroupDestroyed(onGroupDestroyed),onAutoAcceptInvitationFromGroup(onAutoAcceptInvitationFromGroup),onMuteListAdded(onMuteListAdded),onMuteListRemoved(onMuteListRemoved),onAdminAdded(onAdminAdded),onAdminRemoved(onAdminRemoved),onOwnerChanged(onOwnerChanged),onMemberJoined(onMemberJoined),onMemberExited(onMemberExited),onAnnouncementChanged(onAnnouncementChanged),onSharedFileAdded(onSharedFileAdded),onSharedFileDeleted(onSharedFileDeleted) {}
    
    void onReceiveInviteFromGroup(const std::string groupId, const std::string& inviter, const std::string& inviteMessage) override {
        if(onInvitationReceived) {
            auto group = CLIENT->getGroupManager().groupWithId(groupId);
            auto groupName = group->groupSubject();
            onInvitationReceived(groupId.c_str(), groupName.c_str(), inviter.c_str(), inviteMessage.c_str());
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
    FUNC_OnAutoAcceptInvitationFromGroup onAutoAcceptInvitationFromGroup;
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
    RoomManagerListener(void * client, FUNC_OnChatRoomDestroyed onChatRoomDestroyed, FUNC_OnMemberJoined onMemberJoined, FUNC_OnMemberExited onMemberExited,
                        FUNC_OnRemovedFromChatRoom onRemovedFromChatRoom, FUNC_OnMuteListAdded onMuteListAdded, FUNC_OnMuteListRemoved onMuteListRemoved,
                        FUNC_OnAdminAdded onAdminAdded, FUNC_OnAdminRemoved onAdminRemoved, FUNC_OnOwnerChanged onOwnerChanged, FUNC_OnAnnouncementChanged onAnnouncementChanged ):client(client), onChatRoomDestroyed(onChatRoomDestroyed), onMemberJoined(onMemberJoined), onMemberExited(onMemberExited), onRemovedFromChatRoom(onRemovedFromChatRoom), onMuteListAdded(onMuteListAdded), onMuteListRemoved(onMuteListRemoved), onAdminAdded(onAdminAdded), onAdminRemoved(onAdminRemoved), onOwnerChanged(onOwnerChanged), onAnnouncementChanged(onAnnouncementChanged) {}
    
    void  onMemberJoinedChatroom(const EMChatroomPtr chatroom, const std::string &member) override {
        if(onMemberJoined) {
            onMemberJoined(chatroom->chatroomId().c_str(), member.c_str());
        }
    }
private:
    void * client;
    FUNC_OnChatRoomDestroyed onChatRoomDestroyed;
    FUNC_OnMemberJoined onMemberJoined;
    FUNC_OnMemberExited onMemberExited;
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
    }
    void onContactDeleted(const std::string &username) override {
        LOG("receive contactdeleted from user %s!", username.c_str());
    }
    void onContactInvited(const std::string &username, std::string &reason) override {
        LOG("receive contactinvited from user %s with reason %s!", username.c_str(), reason.c_str());
    }
    void onContactAgreed(const std::string &username) override {
        LOG("receive contactagreed from user %s!", username.c_str());
    }
    void onContactRefused(const std::string &username) override {
        LOG("receive contactrefused from user %s!", username.c_str());
    }

private:
    FUNC_OnContactAdded onContactAdded_;
    FUNC_OnContactDeleted onContactDeleted_;
    FUNC_OnContactInvited onContactInvited_;
    FUNC_OnFriendRequestAccepted onFriendRequestAccepted_;
    FUNC_OnFriendRequestDeclined OnFriendRequestDeclined_;
};

#endif // _CALLBACKS_H_
