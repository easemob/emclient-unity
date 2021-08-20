/************************************************************
 *  * EaseMob CONFIDENTIAL
 * __________________
 * Copyright (C) 2016 EaseMob Technologies. All rights reserved.
 * 
 * NOTICE: All information contained herein is, and remains
 * the property of EaseMob Technologies.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from EaseMob Technologies.
 */

#ifndef __easemob__emmuc__
#define __easemob__emmuc__

#include "embaseobject.h"
#include "emmucsetting.h"
#include "emmucsharedfile.h"

#include <map>
#include <memory>
#include <string>
#include <utility>
#include <vector>

namespace easemob
{

typedef std::vector<std::string> EMMucMemberList;
typedef std::vector<std::pair<std::string, int64_t> > EMMucMuteList;
typedef std::vector<EMMucSharedFilePtr> EMMucSharedFileList;

class EMChatManager;
class EMMucPrivate;
class EMGroupManager;
class EMChatroomManager;
class EMMucManager;
class EMDatabase;

class EASEMOB_API EMMuc : public EMBaseObject
{
public:
    
    typedef enum
    {
        BE_KICKED,              //User is kicked out by the muc owner
        DESTROYED,              //Muc was destroyed by the muc owner.
        BE_KICKED_FOR_OFFLINE   //User is kicked out by server for account offline.
    } EMMucLeaveReason;
    
    typedef enum
    {
        MUC_MEMBER,             //user type is muc member.
        MUC_ADMIN,              //user type is muc admin.
        MUC_OWNER,              //user type is muc owner.
        MUC_UNKNOWN = -1,       //user type is unknown.
        DEFAUT = MUC_UNKNOWN
    } EMMucMemberType;
    
    virtual ~EMMuc();
    
    /**
     * \brief Get muc's ID.
     *
     * @param  NA
     * @return Muc's ID.
     */
    const std::string& mucId() const;
    
    /**
     * \brief Get muc's subject.
     *
     * @param  NA
     * @return Muc's subject
     */
    const std::string& mucSubject() const;
    
    /**
     * \brief Get muc's description.
     *
     * @param  NA
     * @return Muc's description
     */
    const std::string& mucDescription() const;
    
    /**
     * \brief Get muc's owner.
     *
     * @param  NA
     * @return Muc's owner
     */
    const std::string& mucOwner() const;
    
    /**
      * \brief Get muc's setting.
      *
      * Note: Will return nullptr if have not ever got muc's specification.
      * @param  NA
      * @return Muc's setting.
      */
    const EMMucSettingPtr mucSetting() const;
    /**
      * \brief Get current members count.
      *
      * Note: Will return 0 if have not ever got muc's specification.
      * @param  NA
      * @return Members count
      */
    int mucMembersCount() const;
    
    /**
      * \brief Get max count of muc member.
      *
      * Note: Will return 0 if have not ever got muc's specification.
      * @param  NA
      * @return Max count of muc member.
      */
    int mucMemberMaxCount() const;

    /**
      * \brief Get current login user type.
      *
      * Note: Will return  MUC_UNKNOWN if have not ever got muc's member type.
      * @param  NA
      * @return login member type
      */
    EMMucMemberType mucMemberType() const;
    
    /**
     * \brief Get whether push is enabled status.
     *
     * @param  NA
     * @return Push status.
     */
    bool isMucPushEnabled() const;
    
    /**
     * \brief Get whether group messages is blocked.
     *
     * Note: Muc owner can't block group message.
     * @param  NA
     * @return Muc message block status.
     */
    bool isMucMessageBlocked() const;
    
    /**
     * \brief Get muc's member list.
     *
     * Note: Will return nullptr if have not ever got muc's members.
     * @param  NA
     * @return Muc's member list.
     */
    const EMMucMemberList mucMembers() const;
    
    /**
     * \brief Get muc's bans.
     *
     * Note: Will return nullptr if have not ever got muc's bans.
     * @param  NA
     * @return Muc's bans list.
     */
    const EMMucMemberList mucBans() const;
    
    /**
     * \brief Get muc's admins.
     *
     * Note: Will return nullptr if have not ever got muc's admins.
     * @param  NA
     * @return Muc's admins list.
     */
    const EMMucMemberList mucAdmins() const;

    /**
     * \brief Get muc's online admins. which use for chatrooms.
     *
     * Note: Will return nullptr if have not ever got muc's admins.
     * @param  NA
     * @return Muc's admins list.
     */
    const EMMucMemberList mucOnlineAdmins() const;

    /**
     * \brief Get muc's mutes.
     *
     * Note: Will return nullptr if have not ever got muc's mutes.
     * @param  NA
     * @return Muc's mutes list.
     */
    const EMMucMuteList mucMutes() const;
    
    /**
     * \brief Get muc's white list.
     *
     * Note: Will return nullptr if have not ever got muc's  white list.
     * @param  NA
     * @return Muc's white list.
     */
    const EMMucMemberList mucWhiteList() const;
    
    /**
     * \brief Get muc's share files.
     *
     * Note: Will return nullptr if have not ever got muc's share files.
     * @param  NA
     * @return Muc's share files list.
     */
    const EMMucSharedFileList mucSharedFiles() const;

    /**
     * \brief Get muc's announcement.
     *
     * @param  NA
     * @return Muc's announcement.
     */
    const std::string& mucAnnouncement() const;
    
    /**
    * \brief Get muc's all members muted.
    *
    * @param  NA
    * @return All members muted.
    */
    bool isMucAllMembersMuted() const;
    
protected:
    /**
     * \brief Constructor.
     *
     * Note: User can‘t directly create a muc.
     * @param  Muc's ID
     * @return NA.
     */
    EMMuc(const std::string &mucId, bool isChatroom);
    EMMuc(const EMMuc&);
    EMMuc& operator=(const EMMuc&);

private:

    EMMucPrivate *mPrivate;
    friend EMMucManager;
    friend EMGroupManager;
    friend EMChatroomManager;
    friend EMDatabase;
    friend EMChatManager;
};

typedef std::shared_ptr<EMMuc> EMMucPtr;
}

#endif /* defined(__easemob__emgroup__) */
