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

#ifndef __easemob__emgroupmanager_listener__
#define __easemob__emgroupmanager_listener__

#include "emgroup.h"

#include <string>

namespace easemob
{

class EASEMOB_API EMGroupManagerListener
{
public:
    virtual ~EMGroupManagerListener() {}
    
    /**
      * \brief Callback user when user is invited to a group.
      *
      * Note: User can accept or decline the invitation.
      * @param  The group that invite the user.
      * @param  The inviter.
      * @param  The invite message.
      * @return NA
      */
    virtual void onReceiveInviteFromGroup(const std::string groupId, const std::string& inviter, const std::string& inviteMessage) {}
    
    /**
      * \brief Callback user when the user accept to join the group.
      *
      * @param  The group that invite the user.
      * @return NA
      */
    virtual void onReceiveInviteAcceptionFromGroup(const EMGroupPtr group, const std::string& invitee) {}
    
    /**
      * \brief Callback user when the user decline to join the group.
      *
      * @param  The group that invite the user.
      * @param  User's decline reason.
      * @return NA
      */
    virtual void onReceiveInviteDeclineFromGroup(const EMGroupPtr group, const std::string& invitee, const std::string &reason) {}
    
    /**
      * \brief Callback user when user is invited to a group.
      *
      * Note: User has been added to the group when received this callback.
      * @param  The group that invite the user.
      * @param  The inviter.
      * @param  The invite message.
      * @return NA
      */
    virtual void onAutoAcceptInvitationFromGroup(const EMGroupPtr group, const std::string& inviter, const std::string& inviteMessage) {}
    
    /**
      * \brief Callback user when user is kicked out from a group or the group is destroyed.
      *
      * @param  The group that user left.
      * @param  The leave reason.
      * @return NA
      */
    virtual void onLeaveGroup(const EMGroupPtr group, EMMuc::EMMucLeaveReason reason) {}
    
    /**
      * \brief Callback user when receive a join group application.
      *
      * @param  The group that user try to join.
      * @param  User that try to join the group.
      * @param  The apply message.
      * @return NA
      */
    virtual void onReceiveJoinGroupApplication(const EMGroupPtr group, const std::string& from, const std::string& message) {}
    
    /**
      * \brief Callback user when receive owner's approval.
      *
      * @param  The group to join.
      * @return NA
      */
    virtual void onReceiveAcceptionFromGroup(const EMGroupPtr group) {}
    
    /**
      * \brief Callback user when receive group owner's rejection.
      *
      * @param  The group that user try to join.
      * @param  Owner's reject reason.
      * @return NA
      */
    virtual void onReceiveRejectionFromGroup(const std::string &groupId, const std::string &reason) {}
    
    /**
      * \brief Callback user when login user's group list is updated.
      *
      * @param  The login user's group list.
      * @return NA
      */
    virtual void onUpdateMyGroupList(const std::vector<EMGroupPtr> &list) {};

    /**
      * \brief Callback user when user add to group mute list.
      *
      * @param  The group that add user to mute list.
      * @param  The user add to the mute list.
      * @param  The mute time.
      * @return NA
      */
    virtual void onAddMutesFromGroup(const EMGroupPtr group, const std::vector<std::string> &mutes, int64_t muteExpire) {};

    /**
      * \brief Callback user when user remove from group mute list.
      *
      * @param  The group that remove user from mute list.
      * @param  The user remove from the mute list.
      * @return NA
      */
    virtual void onRemoveMutesFromGroup(const EMGroupPtr group, const std::vector<std::string> &mutes) {};
    
    /**
      * \brief Callback user when user add to chat room white list.
      *
      * @param  The Group that add user to white list.
      * @param  The users add to the white list.
      * @return NA
      */
    virtual void onAddWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const std::vector<std::string> &members) {};
    
    
    /**
      * \brief Callback user when user remove from chat room white list.
      *
      * @param  The Group that remove user from white list.
      * @param  The users remove from the white list.
      * @return NA
      */
    virtual void onRemoveWhiteListMembersFromGroup(const easemob::EMGroupPtr Group, const std::vector<std::string> &members) {};
    
    /**
      * \brief Callback user when all member mute has changed
      *
      * @param  The Group that all member mute changed
      * @param  The users all member muted
      * @return NA
      */
    virtual void onAllMemberMuteChangedFromGroup(const easemob::EMGroupPtr Group, bool isAllMuted) {};

    /**
      * \brief Callback user when promote to group admin.
      *
      * @param  The group that promote user admin.
      * @param  The new admin.
      * @return NA
      */
    virtual void onAddAdminFromGroup(const EMGroupPtr group, const std::string& admin) {};

    /**
      * \brief Callback user when cancel admin.
      *
      * @param  The group that cancel user admin.
      * @param  The admin remove from the group.
      * @return NA
      */
    virtual void onRemoveAdminFromGroup(const EMGroupPtr group, const std::string& admin) {};

    /**
      * \brief Callback user when promote to group owner.
      *
      * @param  The group that promote user to owner.
      * @param  The new owner of the group.
      * @param  The old owner of the group.
      * @return NA
      */
    virtual void onAssignOwnerFromGroup(const EMGroupPtr group, const std::string& newOwner, const std::string& oldOwner) {};
    
    /**
     * \brief Callback user when a user join the group.
     *
     * @param  The group that user joined.
     * @param  The member.
     * @return NA
     */
    virtual void onMemberJoinedGroup(const EMGroupPtr group, const std::string& member) {};
    
    /**
     * \brief Callback user when a user leave the group.
     *
     * @param  The group that user left.
     * @param  The member.
     * @return NA
     */
    virtual void onMemberLeftGroup(const EMGroupPtr group, const std::string& member) {};

    /**
     * \brief Callback user when update group announcement.
     *
     * @param  The group that update the announcement.
     * @param  The announcement.
     * @return NA
     */
    virtual void onUpdateAnnouncementFromGroup(const EMGroupPtr group, const std::string& announcement) {};

    /**
     * \brief Callback user when group member upload share file.
     *
     * @param  The group that upload share file.
     * @param  The upload share file info.
     * @return NA
     */
    virtual void onUploadSharedFileFromGroup(const EMGroupPtr group, const EMMucSharedFilePtr sharedFile) {};

    /**
     * \brief Callback user when group admin or owner or file uploader delete share file.
     *
     * @param  The group that delete share file.
     * @param  The delete share file id.
     * @return NA
     */
    virtual void onDeleteSharedFileFromGroup(const EMGroupPtr group, const std::string& fileId) {};
};

}

#endif /* defined(__easemob__emgroupmanager_listener__) */
