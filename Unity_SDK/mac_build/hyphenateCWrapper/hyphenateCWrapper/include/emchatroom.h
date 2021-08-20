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

#ifndef __easemob__emchatroom__
#define __easemob__emchatroom__

#include "emmucsetting.h"
#include "emmuc.h"

namespace easemob
{
class EMMucPrivate;
class EMMucCreator;
class EMChatroomManager;
class EMDatabase;
class EASEMOB_API EMChatroom : public EMMuc
{
public:
    virtual ~EMChatroom();
    
    /**
     * \brief Get chatroom's ID.
     *
     * @param  NA
     * @return Chatroom's ID.
     */
    const std::string& chatroomId() const;
    
    /**
     * \brief Get chatroom's subject.
     *
     * @param  NA
     * @return Chatroom's subject
     */
    const std::string& chatroomSubject() const;
    
    /**
     * \brief Get chatroom's description.
     *
     * @param  NA
     * @return Chatroom's description.
     */
    const std::string& chatroomDescription() const;
    
    /**
     * \brief Get chatroom's owner.
     *
     * @param  NA
     * @return Chatroom's owner
     */
    const std::string& owner() const;
    
    /**
     * \brief Get chatroom's setting.
     *
     * Note: Will return nullptr if have not ever got chatroom's specification.
     * @param  NA
     * @return Chatroom's setting.
     */
    const EMMucSettingPtr chatroomSetting() const;
    
    /**
     * \brief Get current members count.
     *
     * Note: Will return 0 if have not ever got chatroom's specification.
     * @param  NA
     * @return Member count
     */
    int chatroomMemberCount() const;
    
    /**
     * \brief Get max count of chatroom member.
     *
     * Note: Will return 0 if have not ever got chatroom's specification.
     * @param  NA
     * @return Max count of chatroom member.
     */
    int chatroomMemberMaxCount() const;
    
    /**
     * \brief Get chatroom's member list.
     *
     * Note: Will return empty list if have not ever got chatroom's members.
     * @param  NA
     * @return Chatroom's member list.
     */
    const EMMucMemberList chatroomMembers() const;
    
    /**
     * \brief Get chatroom's bans list.
     *
     * Note: Will return empty list if have not ever got chatroom's bans.
     * @param  NA
     * @return Chatroom's bans list.
     */
    const EMMucMemberList chatroomBans() const;
    
    /**
     * \brief Get chatroom's admins.
     *
     * Note: Will return nullptr if have not ever got chatroom's admins.
     * @param  NA
     * @return Chatroom's admins list.
     */
    const EMMucMemberList chatroomAdmins() const;
    
    /**
     * \brief Get chatroom's mutes list.
     *
     * Note: Will return empty list if have not ever got chatroom's mutes.
     * @param  NA
     * @return Chatroom's mutes list.
     */
    const EMMucMuteList chatroomMutes() const;

    /**
     * \brief Get chatroom's white list.
     *
     * Note: Will return empty list if have not ever got chatroom's white list.
     * @param  NA
     * @return Chatroom's white list.
     */
    const EMMucMemberList chatroomWhiteList() const;
    
    /**
     * \brief Get current login user type.
     *
     * Note: Will return  GROUP_MEMBER if have not ever got group's member type.
     * @param  NA
     * @return login member type
     */
    EMMucMemberType chatroomMemberType() const;
    
    /**
     * \brief Get chatroom's announcement.
     *
     * @param  NA
     * @return Chatroom's announcement.
     */
    const std::string& chatroomAnnouncement() const;
    
    /**
     * \brief Get chatroom's all members muted.
     *
     * @param  NA
     * @return All members muted.
     */
    bool chatroomAllMembersMuted() const;
    
private:
    /**
     * \brief Constructor.
     *
     * Note: User can‘t directly create a chatroom.
     * @param  Chatroom's ID
     * @return NA.
     */
    EMChatroom(const std::string &chatroomId);
    EMChatroom(const EMChatroom&);
    EMChatroom(const EMMuc&);
    EMChatroom& operator=(const EMChatroom&);
    
    friend EMChatroomManager;
    friend EMMucCreator;
    friend EMDatabase;
};

typedef std::shared_ptr<EMChatroom> EMChatroomPtr;
    
}

#endif /* defined(__easemob__emchatroom__) */
