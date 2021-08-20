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

#ifndef __easemob__emmucsetting__
#define __easemob__emmucsetting__

#include "embaseobject.h"
#include <memory>
#include <string>

namespace easemob
{

class EASEMOB_API EMMucSetting : public EMBaseObject
{
public:

    typedef enum{
        PRIVATE_OWNER_INVITE,   //Private group, only group owner can invite user to the group
        PRIVATE_MEMBER_INVITE,  //Private group, both group owner and members can invite user to the group
        PUBLIC_JOIN_APPROVAL,   //Public group, user can apply to join the group, but need group owner's approval, and owner can invite user to the group
        PUBLIC_JOIN_OPEN,       //Public group, any user can freely join the group, and owner can invite user to the group
        PUBLIC_ANONYMOUS,       //Anonymous group, NOT support now
        DEFAUT = PRIVATE_OWNER_INVITE
    } EMMucStyle;
    
    EMMucSetting(EMMucStyle style, int maxUserCount, bool inviteNeedConfirm, std::string extension = "") : 
    mStyle(style), mMaxUserCount(maxUserCount), mInviteNeedConfirm(inviteNeedConfirm), mExtension(extension)
    {}
    EMMucSetting(const EMMucSetting& a) {
        mStyle = a.mStyle;
        mMaxUserCount = a.mMaxUserCount;
        mInviteNeedConfirm = a.mInviteNeedConfirm;
        mExtension = a.mExtension;
    }
    
    virtual ~EMMucSetting() {}
    
    EMMucStyle style() const { return mStyle; }
    void setStyle(EMMucStyle style) { mStyle = style; }
    
    int maxUserCount() const { return mMaxUserCount; }
    void setMaxUserCount(int maxUserCount) { mMaxUserCount = maxUserCount; }

    bool inviteNeedConfirm() const { return mInviteNeedConfirm; }
    void setInviteNeedConfirm(bool inviteNeedConfirm) { mInviteNeedConfirm = inviteNeedConfirm; }

    std::string extension() const { return mExtension; }
    void setExtension(const std::string& extension) { mExtension = extension; }
    
    EMMucSetting& operator=(const EMMucSetting &a) {
        mStyle = a.mStyle;
        mMaxUserCount = a.mMaxUserCount;
        mInviteNeedConfirm = a.mInviteNeedConfirm;
        mExtension = a.mExtension;
        return *this;
    }

private:
    EMMucStyle mStyle;
    int mMaxUserCount;
    bool mInviteNeedConfirm;
    std::string mExtension;
};
    
typedef std::shared_ptr<EMMucSetting> EMMucSettingPtr;

}

#endif /* defined(__easemob__emmucsetting__) */
