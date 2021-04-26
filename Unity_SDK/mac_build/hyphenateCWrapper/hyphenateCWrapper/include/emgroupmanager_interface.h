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

#ifndef EMCORE_GROUPMANAGER_INTERFACE_H
#define EMCORE_GROUPMANAGER_INTERFACE_H

#include <vector>
#include <string>

#include "emgroup.h"
#include "emerror.h"
#include "emcursorresult.h"
#include "emgroupmanager_listener.h"
#include "emcallback.h"

namespace easemob
{
typedef std::vector<EMGroupPtr> EMGroupList;

class EASEMOB_API EMGroupManagerInterface {

public:
    virtual ~EMGroupManagerInterface() {};
    
    /**
     * \brief Add group manager listener
     *
     * @param  A group manager listener
     * @return NA
     */
    virtual void addListener(EMGroupManagerListener*) = 0;
    
    /**
     * \brief Remove group manager listener
     *
     * @param  A group manager listener
     * @return NA
     */
    virtual void removeListener(EMGroupManagerListener*) = 0;
    
    /**
     * \brief Remove all the listeners
     *
     * @param  NA
     * @return NA
     */
    virtual void clearListeners() = 0;
    
    /**
     * \brief Get a group with groupId, create the group if not exist.
     *
     * @param   groupId    Group's id.
     * @return             the group
     */
    virtual EMGroupPtr groupWithId(const std::string& groupId) = 0;
    
    /**
     * \brief Get groups for login user from cache, or from local database if not in cache.
     *
     * @param  error   EMError used for output
     * @return         All user joined groups in memory
     */
    virtual EMGroupList allMyGroups(EMError &error) = 0;
    
    /**
     * \brief Get groups for login user from local database
     *
     * @return All user joined groups in local database
     */
    virtual EMGroupList loadAllMyGroupsFromDB() = 0;
    
    /**
     * \brief Fetch all groups for login user from server
     *
     * Note: Groups in memory will be updated
     *
     * @param  error   EMError used for output
     * @return         All user joined groups
     */
    virtual EMGroupList fetchAllMyGroups(EMError &error) = 0;
    
    // TODO: need more clarity in usage of pageNum and pageSize. Can pageNum=0? pageNum=1?
    /**
     * \brief Fetch all groups for login user from server with page.
     *
     * If PageNum=1, then will start from the first page of pagination
     *
     * @param  pageNum     Page's number
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output
     * @return             current page of groups user joined
     */
    virtual EMGroupList fetchAllMyGroupsWithPage(int pageNum,
                                                 int pageSize,
                                                 EMError& error) = 0;
    
    // TODO: what's the difference between fetchPublicGroupsWithCursor vs. fetchPublicGroupsWithPage?
    /**
     * \brief Fetch app's public groups with cursor.
     *
     // TODO: what to expect if I pass empty string as cursor at the first time?
     * Note: User can input empty string as cursor at the first time.
     *
     * @param  cursor      Page cursor
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output.
     * @return             Wrapper of next page's cursor and current page result.
     */
    virtual EMCursorResult fetchPublicGroupsWithCursor(const std::string &cursor,
                                                       int pageSize,
                                                       EMError &error) = 0;
    
    /**
     * \brief Fetch app's public groups with page.
     *
     * If PageNum=0, then there is no pagination and will get all the users on the list
     * If PageNum=1, then will start from the first page of pagination
     *
     * @param  pageNum     Page number of pagination
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output
     * @return             Wrapper of current page's count and current page result
     */
    virtual EMPageResult fetchPublicGroupsWithPage(int pageNum,
                                                   int pageSize,
                                                   EMError& error) = 0;
    
    /**
     * \brief Create a new group.
     *
     * Note: user will be the owner of the group created
     *
     * @param  subject             Group's subject
     * @param  description         Group's description
     * @param  welcomeMessage      Welcoming message that will be sent to invited user.
     * @param  setting             Group's setting
     * @param  members             Group's members
     * @param  error               EMError used for output
     * @return                     The group created
     */
    virtual EMGroupPtr createGroup(const std::string &subject,
                                   const std::string &description,
                                   const std::string &welcomeMessage,
                                   const EMMucSetting &setting,
                                   const EMMucMemberList &members,
                                   EMError &error) = 0;
    
    /**
     * \brief Join a public group.
     *
     * Note: The group's style must be PUBLIC_JOIN_OPEN, or will return error
     *
     * @param  groupId     Group ID
     * @param  errro       EMError used for output
     * @return             The group joined
     */
    virtual EMGroupPtr joinPublicGroup(const std::string &groupId,
                                       EMError &error) = 0;
    
    /**
     * \brief Request to join a public group, need owner or admin's approval
     *
     * Note: The group's style must be PUBLIC_JOIN_APPROVAL, or will return error.
     *
     * @param  groupId     Group ID
     * @param  nickName    user's nickname in the group
     * @param  message     requesting message, that will be sent to group owner
     * @param  error       EMError used for output
     * @return             The group to join
     */
    virtual EMGroupPtr applyJoinPublicGroup(const std::string &groupId,
                                            const std::string &nickName,
                                            const std::string &message,
                                            EMError &error) = 0;
    
    /**
     * \brief Leave a group.
     *
     // TODO: how many group owner can be in a group? one?
     * Note: Group owner cannot leave the group
     *
     * @param  groupId     Group ID
     * @param  error       EMError used for output
     * @return             NA
     */
    virtual void leaveGroup(const std::string &groupId,
                            EMError &error) = 0;
    
    /**
     * \brief Destroy a group.
     *
     * Note: Only group owner can destroy the group
     *
     * @param  groupId     Group ID
     * @param  error       EMError used for output
     * @return             NA
     */
    virtual void destroyGroup(const std::string &groupId,
                              EMError &error) = 0;
    
    /**
     * \brief Add members to a group.
     *
     * whether if user has permission to invite other user depends on group's setting
     *
     * @param  groupId             Group ID
     * @param  members             Invited users
     * @param  welcomeMessage      Welcome message that will be sent to invited user
     * @param  error               EMError used for output
     * @return                     The group
     */
    virtual EMGroupPtr addGroupMembers(const std::string &groupId,
                                       const EMMucMemberList &members,
                                       const std::string &welcomeMessage,
                                       EMError &error) = 0;
    
    /**
     * \brief Remove members from a group
     *
     * ONLY group owner and admin owner can remove members.
     * ONLY group owner can remove both admin and members
     *
     * @param  groupId     Group ID
     * @param  members     Removed members
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr removeGroupMembers(const std::string &groupId,
                                          const EMMucMemberList &members,
                                          EMError &error) = 0;
    
    /**
     * \brief Block group's members, the blocked user cannot send message in the group.
     *
     * ONLY group owner and admin owner can block members
     * ONLY group owner can block both admin and members
     *
     * @param  groupId     Group ID
     * @param  members     Blocked members
     * @param  error       EMError used for output
     * @param  reason      The reason of blocking members
     * @return             The group
     */
    virtual EMGroupPtr blockGroupMembers(const std::string &groupId,
                                         const EMMucMemberList &members,
                                         EMError &error,
                                         const std::string &reason = "") = 0;
    
    /**
     * \brief Unblock group's members.
     *
     * ONLY group owner and admin owner can unblock members
     * ONLY group owner can unblock both admin and members
     *
     * @param  groupId     Group's ID.
     * @param  members     Unblocked users.
     * @param  error       EMError used for output.
     * @return             The group
     */
    virtual EMGroupPtr unblockGroupMembers(const std::string &groupId,
                                           const EMMucMemberList &members,
                                           EMError &error) = 0;
    
    /**
     * \brief Change group's subject.
     *
     // TODO: what about group admin?
     * Only group's owner can change group's subject
     *
     * @param  groupId     Group ID
     * @param  newSubject  The new group subject
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr changeGroupSubject(const std::string &groupId,
                                          const std::string &newSubject,
                                          EMError &error) = 0;
    
    /**
     * \brief Change group's description.
     *
     // TODO: what about group admin?
     * Only group's owner can change group's description.
     *
     * @param  groupId         Group's ID.
     * @param  newDescription  The new group description.
     * @param  error           EMError used for output.
     * @return                 The group
     */
    virtual EMGroupPtr changeGroupDescription(const std::string &groupId,
                                              const std::string &newDescription,
                                              EMError &error) = 0;
    
    /**
     * \brief Change group's extension
     *
     // TODO: what about group admin?
     * Only group's owner can change group's extension
     *
     * @param  groupId         Group ID
     * @param  newExtension    The new group extension
     * @param  error           EMError used for output
     * @return                 The group
     */
    virtual EMGroupPtr changeGroupExtension(const std::string &groupId,
                                            const std::string &newExtension,
                                            EMError &error) = 0;
    /**
     * \brief Get group's specification
     *
     * @param  groupId         Group ID
     * @param  error           EMError used for output
     * @param  fetchMembers    Whether get group's members
     * @return                 The group
     */
    virtual EMGroupPtr fetchGroupSpecification(const std::string &groupId,
                                               EMError &error,
                                               bool fetchMembers = true) = 0;
    
    /**
     * \brief Get group's member list
     *
     // TODO: what to expect here if passing empyy string?
     * User can input empty string as cursor at the first time
     *
     * @param  groupId     Group ID
     * @param  cursor      Page's cursor
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output
     * @return             the list of group members
     */
    virtual const EMCursorResultRaw<std::string> fetchGroupMembers(const std::string &groupId,
                                                                   const std::string &cursor,
                                                                   int pageSize,
                                                                   EMError &error) = 0;
    
    /**
     * \brief Get group's blacklist
     *
     * If PageNum=0, then there is no pagination and will get all the users on the list
     * If PageNum=1, then will start from the first page of pagination
     *
     * @param  groupId     Group ID
     * @param  pageNum     page number of pagination
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output
     * @return             The blacklist of the group
     */
    virtual const EMMucMemberList fetchGroupBans(const std::string &groupId,
                                                 int pageNum,
                                                 int pageSize,
                                                 EMError &error) = 0;
    
    /**
     * \brief Search for a public group
     *
     * @param  groupId     Group ID to be found
     * @param  error       EMError used for output.
     * @return             The group with specified id
     */
    virtual EMGroupPtr searchPublicGroup(const std::string &groupId,
                                         EMError &error) = 0;
    
    /**
     * \brief Block group message
     *
     * Member will not receive message from group if blocked
     // TODO: can admin block the group message?
     * Owner cannot block the group message
     *
     * @param  groupId     Group ID
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr blockGroupMessage(const std::string &groupId,
                                         EMError &error) = 0;
    
    /**
     * \brief Unblock group message
     *
     * @param  groupId     Group ID
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr unblockGroupMessage(const std::string &groupId,
                                           EMError &error) = 0;
    
    /**
     * \brief Accept user's group joining request
     *
     * Note: Only group's owner and admin can approval user's request to join group
     *
     * @param  groupId     Group ID
     * @param  user        The user that made the request
     * @param  error       EMError used for output
     * @return             NA
     */
    virtual EMGroupPtr acceptJoinGroupApplication(const std::string &groupId,
                                                  const std::string &user,
                                                  EMError &error) = 0;
    
    // TODO:
    /**
     * \brief Decline user's join application
     *
     * Note: Only group's owner and admin can decline user's request to join group
     *
     * @param  groupId     Group ID
     * @param  user        The user that made the request
     * @param  reason      The reject reason
     * @param  error       EMError used for output
     * @return             NA
     */
    virtual EMGroupPtr declineJoinGroupApplication(const std::string &groupId,
                                                   const std::string &user,
                                                   const std::string &reason,
                                                   EMError &error) = 0;
    
    /**
     * \brief accept invitation to join a group
     *
     * @param  groupId     Group ID
     * @param  Inviter     Inviter
     * @param  error       EMError used for output
     * @return             The group user has accepted
     */
    virtual EMGroupPtr acceptInvitationFromGroup(const std::string &groupId,
                                                 const std::string &inviter,
                                                 EMError &error) = 0;
    
    /**
     * \brief decline invitation to join a group
     *
     * @param  groupId     Group ID
     * @param  Inviter     Inviter
     * @param  reason      The decline reason
     * @param  error       EMError used for output
     * @return             NA
     */
    virtual void declineInvitationFromGroup(const std::string &groupId,
                                            const std::string &inviter,
                                            const std::string &reason,
                                            EMError &error) = 0;
    
    /**
     * \brief transfer to new group owner/
     *
     * Note: Only group owner can transfer ownership
     *
     * @param  groupId     Group ID of the current owner
     * @param  newOwner    Group ID of the new owner
     * @param  error       EMError used for output
     * @return             NA
     */
    virtual EMGroupPtr transferGroupOwner(const std::string &groupId,
                                          const std::string &newOwner,
                                          EMError &error) = 0;
    
    // TODO: can group admin adds a new admin? 只有群主可以添加新的管理员，同级别没有权限操作。
    /**
     * \brief add group admin.
     *
     * Note: Only group owner can transfer ownership
     *
     * @param  groupId     Group ID
     * @param  admin       New group admin
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr addGroupAdmin(const std::string &groupId,
                                     const std::string &admin,
                                     EMError &error) = 0;
    
    /**
     * \brief remove group admin
     * ONLY group owner can remove admin, not other admin
     *
     * @param  groupId     Group ID
     * @param  admin       Group admin member
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr removeGroupAdmin(const std::string &groupId,
                                        const std::string &admin,
                                        EMError &error) = 0;
    
    // TODO: can anyone or only owner, admin use this method?
    /**
     * \brief add group mute members
     *
     * Temporary mute members will not be able to talk in the chat room for period of time
     *
     * @param  groupId         Group ID
     * @param  members         Group's mute members
     * @param  muteDuration    mute duration in milliseconds
     * @param  error           EMError used for output
     * @return                 The group
     */
    virtual EMGroupPtr muteGroupMembers(const std::string &groupId,
                                        const EMMucMemberList &members,
                                        int64_t muteDuration,
                                        EMError &error) = 0;
    
    // TODO: can anyone or only owner, admin use this method?
    /**
     * \brief remove group muted members
     *
     * @param  groupId     Group ID
     * @param  members     mute members to be removed
     * @param  error       EMError used for output.
     * @return             The group
     */
    virtual EMGroupPtr unmuteGroupMembers(const std::string &groupId,
                                          const EMMucMemberList &members,
                                          EMError &error) = 0;
    
    /**
     * \brief add group white list members
     *
     * Temporary whitelist members will not be muted.
     *
     * @param chatroomId    group ID
     * @param members       add members
     * @param error         EMError used for output
     * @return              the group
     */
    virtual EMGroupPtr addWhiteListMembers(const std::string &groupId,
                                           const EMMucMemberList &members,
                                           EMError &error) = 0;
    
    
    /**
     * \brief remove group white list members
     *
     *
     * @param chatroomId    group ID
     * @param members       remove members
     * @param error         EMError used for output
     * @return              the group
     */
    virtual EMGroupPtr removeWhiteListMembers(const std::string &groupId,
                                              const EMMucMemberList &members,
                                              EMError &error) = 0;
    
    /**
     * \brief mute all group members
     *
     * Temporary all members not be able to talk in the group
     *
     * @param groupId       Group ID
     * @param error         EMError used for output
     * @return              the group
     */
    virtual EMGroupPtr muteAllGroupMembers(const std::string &groupId,
                                           EMError &error) = 0;
    
    /**
     * \brief unmute group all members
     *
     * Temporary remove group all mute members
     *
     * @param groupId       Group ID
     * @param error         EMError used for output
     * @return              the group
     */
    virtual EMGroupPtr unmuteAllGroupMembers(const std::string &groupId,
                                             EMError &error) = 0;
    
    // TODO: can anyone or only owner, admin use this method?
    // TODO: need more clarity in usage of pageNum and pageSize. Can pageNum=0? pageNum=1?
    /**
     * \brief Get the list of group muted users
     *
     * If PageNum=1, then will start from the first page of pagination
     *
     * @param  groupId     Group ID
     * @param  pageNum     page number of pagination
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output
     * @return             The list of mute users
     */
    virtual const EMMucMuteList fetchGroupMutes(const std::string &groupId,
                                                int pageNum,
                                                int pageSize,
                                                EMError &error) = 0;
    
    /**
     * \brief Get the list of group white list
     *
     * @param  groupId     Group ID
     * @param  error       EMError used for output
     * @return             The list of whitelist
     */
    virtual const EMMucMemberList fetchGroupWhiteList(const std::string &groupId,
                                                    EMError &error) = 0;
    
    
    /**
     * \brief gets whether the member is on the whitelist
     *
     * @param  chatroomId   group ID
     * @param  error        EMError used for output
     * @return              result
     */
    virtual bool fetchIsMemberInWhiteList(const std::string &groupId,
                                          EMError &error) = 0;
    
    /**
     * \brief upload a sharing file to group
     *
     * ONLY group members can upload sharing file to group
     *
     * @param  groupId     Group ID
     * @param  filePath    file path to be uploaded to on server. Can be used for file downloading later.
     * @param  callback    EMCallback contains onProgress of file uploading progress
     * @param  error       Check this EMError for upload success/failure. If !error, then it's uploaded successfully
     * @return             The file uploaded
     */
    virtual const EMMucSharedFilePtr uploadGroupSharedFile(const std::string &groupId,
                                                           const std::string &filePath,
                                                           const EMCallbackPtr callback,
                                                           EMError &error) = 0;
    
    // TODO: need more clarity in usage of pageNum and pageSize. Can pageNum=0? pageNum=1?
    /**
     * \brief fetch group's shared files list
     *
     * ONLY group members can fetch shared files list of the group
     * If PageNum=1, then will start from the first page of pagination
     *
     * @param  groupId     Group ID
     * @param  pageNum     page number of pagination
     * @param  pageSize    Page size. ex. 20 for 20 objects
     * @param  error       EMError used for output
     * @return             The list of shared files
     */
    virtual const EMMucSharedFileList fetchGroupSharedFiles(const std::string &groupId,
                                                            int pageNum,
                                                            int pageSize,
                                                            EMError &error) = 0;
    
    /**
     * \brief download group's share file
     *
     * Note: Only group's members can download shared file in the group
     *
     * @param  groupId     Group ID
     * @param  filePath    store file to this path
     * @param  fileId      shared file id
     * @param  callback    EMCallback contains onProgress of file uploading progress
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr downloadGroupSharedFile(const std::string &groupId,
                                               const std::string &filePath,
                                               const std::string &fileId,
                                               const EMCallbackPtr callback,
                                               EMError &error) = 0;
    
    /**
     * \brief delete a shared file
     *
     * ONLY group's admin and owner or file uploader can delete shared file.
     * This operation will only delete file from the server, but not file stored locally
     *
     * @param  groupId     Group ID
     * @param  fileId      shared file id
     * @param  error       EMError used for output
     * @return             The group
     */
    virtual EMGroupPtr deleteGroupSharedFile(const std::string &groupId,
                                             const std::string &fileId,
                                             EMError &error) = 0;
    
    /**
     * \brief fetch group's announcement
     *
     * Note: Only group's members can fetch group's announcement
     *
     * @param  groupId     Group ID
     * @param  error       EMError used for output
     * @return             The group's announcement in string
     */
    virtual std::string fetchGroupAnnouncement(const std::string &groupId,
                                               EMError &error) = 0;
    
    /**
     * \brief Update group's announcement
     *
     * Note: Only group's owner can update group's announcement
     *
     * @param  groupId             Group ID
     * @param  newAnnouncement     a new group announcement
     * @param  error               EMError used for output
     * @return                     The group
     */
    virtual EMGroupPtr updateGroupAnnouncement(const std::string &groupId,
                                               const std::string &newAnnouncement,
                                               EMError &error) = 0;
};
}

#endif
