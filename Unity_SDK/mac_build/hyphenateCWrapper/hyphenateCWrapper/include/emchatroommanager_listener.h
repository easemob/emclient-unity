/************************************************************
 *  * EaseMob CONFIDENTIAL
 * __________________
 * Copyright (C) 2015 EaseMob Technologies. All rights reserved.
 * 
 * NOTICE: All information contained herein is, and remains
 * the property of EaseMob Technologies.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from EaseMob Technologies.
 */

#ifndef __easemob__emchatroommanager_listener__
#define __easemob__emchatroommanager_listener__

#include "emchatroom.h"

#include <string>

namespace easemob {

class EASEMOB_API EMChatroomManagerListener
{
public:
    /**
      * \brief Callback user when user is kicked out from a chatroom or the chatroom is destroyed.
      *
      * @param  The chatroom that user left.
      * @param  The leave reason.
      * @return NA
      */
    virtual void onLeaveChatroom(const EMChatroomPtr chatroom, EMMuc::EMMucLeaveReason reason) {}
    
    /**
      * \brief Callback user when a user join the chatroom.
      *
      * @param  The chatroom that user joined.
      * @param  The member.
      * @return NA
      */
    virtual void onMemberJoinedChatroom(const EMChatroomPtr chatroom, const std::string &member) {};
    
    /**
      * \brief Callback user when a user leave the chatroom.
      *
      * @param  The chatroom that user left.
      * @param  The member.
      * @return NA
      */
    virtual void onMemberLeftChatroom(const EMChatroomPtr chatroom, const std::string &member) {};

    /**
      * \brief Callback user when user add to chat room mute list.
      *
      * @param  The chatroom that add user to mute list.
      * @param  The users add to the mutes list.
      * @param  mute time.
      * @return NA
      */
    virtual void onAddMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string> &mutes, int64_t muteExpire) {};

    /**
      * \brief Callback user when user remove from chat room mute list.
      *
      * @param  The chatroom that remove user from mute list.
      * @param  The users remove from the mutes list.
      * @return NA
      */
    virtual void onRemoveMutesFromChatroom(const EMChatroomPtr chatroom, const std::vector<std::string> &mutes) {};

    
    /**
      * \brief Callback user when user add to chat room white list.
      *
      * @param  The chatroom that add user to white list.
      * @param  The users add to the white list.
      * @return NA
      */
    virtual void onAddWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string> &members) {};
    
    
    /**
      * \brief Callback user when user remove from chat room white list.
      *
      * @param  The chatroom that remove user from white list.
      * @param  The users remove from the white list.
      * @return NA
      */
    virtual void onRemoveWhiteListMembersFromChatroom(const easemob::EMChatroomPtr chatroom, const std::vector<std::string> &members) {};
    
    /**
      * \brief Callback user when all member mute has changed
      *
      * @param  The chatroom that all member mute changed
      * @param  The users all member muted
      * @return NA
      */
    virtual void onAllMemberMuteChangedFromChatroom(const easemob::EMChatroomPtr chatroom, bool isAllMuted) {};
    
    /**
      * \brief Callback user when user promote to admin.
      *
      * @param  The chatroom that promote user to admin.
      * @param  The new admin.
      * @return NA
      */
    virtual void onAddAdminFromChatroom(const EMChatroomPtr chatroom, const std::string &admin) {};

    /**
      * \brief Callback user when user cancel admin.
      *
      * @param  The chatroom that cancel user admin.
      * @param  The admin removed.
      * @return NA
      */
    virtual void onRemoveAdminFromChatroom(const EMChatroomPtr chatroom, const std::string &admin) {};

    /**
      * \brief Callback user when promote to chatroom owner.
      *
      * @param  The chatroom that promote user to owner.
      * @param  The new owner of the chatroom.
      * @param  The old owner of the chatroom.
      * @return NA
      */
    virtual void onAssignOwnerFromChatroom(const EMChatroomPtr chatroom, const std::string &newOwner, const std::string &oldOwner) {};

    /**
      * \brief Callback user when chatroom's announcement change.
      *
      * @param  The chatroom that promote user to owner.
      * @param  The new announcement.
      * @return NA
      */
    virtual void onUpdateAnnouncementFromChatroom(const EMChatroomPtr chatroom, const std::string &announcement) {};
};

}

#endif /* defined(__easemob__emchatroommanager_listener__) */
