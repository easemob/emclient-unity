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

#ifndef __easemob__emgroup__
#define __easemob__emgroup__

#include "emmucsetting.h"
#include "emmuc.h"

namespace easemob
{

class EMChatManager;
class EMMucPrivate;
class EMMucCreator;
class EMGroupManager;
class EMDatabase;
class EASEMOB_API EMGroup : public EMMuc
{
public:
    
    virtual ~EMGroup();
    
    /**
      * \brief Get group's ID.
      *
      * @param  NA
      * @return Group's ID.
      */
    const std::string& groupId() const;
    
    /**
      * \brief Get group's subject.
      *
      * @param  NA
      * @return Group's subject
      */
    const std::string& groupSubject() const;
    
    /**
      * \brief Get group's description.
      *
      * @param  NA
      * @return Group's description
      */
    const std::string& groupDescription() const;
    
    /**
      * \brief Get group's owner.
      *
      * @param  NA
      * @return Group's owner
      */
    const std::string& groupOwner() const;
    
    /**
      * \brief Get group's setting.
      *
      * Note: Will return nullptr if have not ever got group's specification.
      * @param  NA
      * @return Group's setting.
      */
    const EMMucSettingPtr groupSetting() const;
    
    /**
      * \brief Get current members count.
      *
      * Note: Will return 0 if have not ever got group's specification.
      * @param  NA
      * @return Members count
      */
    int groupMembersCount() const;
    
    /**
      * \brief Get current login user type.
      *
      * Note: Will return  GROUP_MEMBER if have not ever got group's member type.
      * @param  NA
      * @return login member type
      */
    EMMucMemberType groupMemberType() const;

    /**
      * \brief Get whether push is enabled status.
      *
      * @param  NA
      * @return Push status.
      */
    bool isPushEnabled() const;
    
    /**
      * \brief Get whether group messages is blocked.
      *
      * Note: Group owner can't block group message.
      * @param  NA
      * @return Group message block status.
      */
    bool isMessageBlocked() const;
    
    /**
      * \brief Get a copy of group's member list.
      *
      * Note: Will return nullptr if have not ever got group's members.
      * @param  NA
      * @return Group's member list.
      */
    const EMMucMemberList groupMembers() const;
    
    /**
      * \brief Get a copy of group's bans.
      *
      * Note: Will return nullptr if have not ever got group's bans.
      * @param  NA
      * @return Group's bans list.
      */
    const EMMucMemberList groupBans() const;

    /**
      * \brief Get group's admins.
      *
      * Note: Will return nullptr if have not ever got group's admins.
      * @param  NA
      * @return Group's admins list.
      */
    const EMMucMemberList groupAdmins() const;

    /**
      * \brief Get group's mutes.
      *
      * Note: Will return nullptr if have not ever got group's mutes.
      * @param  NA
      * @return Group's mutes list.
      */
    const EMMucMuteList groupMutes() const;
    
    /**
      * \brief Get group's white list.
      *
      * Note: Will return nullptr if have not ever got group's white list.
      * @param  NA
      * @return Group's white list.
      */
    const EMMucMemberList groupWhiteList() const;

    /**
     * \brief Get group's share files.
     *
     * Note: Will return nullptr if have not ever got group's share files.
     * @param  NA
     * @return Group's share files list.
     */
    const EMMucSharedFileList groupSharedFiles() const;

    /**
     * \brief Get group's announcement.
     *
     * @param  NA
     * @return Group's announcement.
     */
    const std::string& groupAnnouncement() const;
    
    /**
    * \brief Get group's all members muted.
    *
    * @param  NA
    * @return All members muted.
    */
    bool groupAllMembersMuted() const;

private:
    /**
      * \brief Constructor.
      *
      * Note: User can‘t directly create a group.
      * @param  Group's ID
      * @return NA.
      */
    EMGroup(const std::string &groupId);
    EMGroup(const EMGroup&);
    EMGroup(const EMMuc&);
    EMGroup& operator=(const EMGroup&);
    
    friend EMGroupManager;
    friend EMMucCreator;
    friend EMDatabase;
    friend EMChatManager;
};

typedef std::shared_ptr<EMGroup> EMGroupPtr;

}

#endif /* defined(__easemob__emgroup__) */
