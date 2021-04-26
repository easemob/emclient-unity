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

#ifndef __easemob__emchatroommanager_interface__
#define __easemob__emchatroommanager_interface__

#include "emchatroommanager_listener.h"
#include "emchatroom.h"
#include "emerror.h"
#include "emcursorresult.h"

#include <vector>

namespace easemob
{
    typedef std::vector<EMChatroomPtr> EMChatroomList;
    
    class EASEMOB_API EMChatroomManagerInterface {
        
    public:
        virtual ~EMChatroomManagerInterface() {};
        
        // TODO: update all the owner/admin/member permission for all the methods
        
        /**
         * \brief Add chatroom manager listener
         *
         * @param  chatroom manager listener
         * @return NA
         */
        virtual void addListener(EMChatroomManagerListener*) = 0;
        
        /**
         * \brief Remove chatroom manager listener
         *
         * @param  chatroom manager listener
         * @return NA
         */
        virtual void removeListener(EMChatroomManagerListener*) = 0;
        
        /**
         * \brief Remove all the chatroom manager listeners
         *
         * @param  NA
         * @return NA
         */
        virtual void clearListeners() = 0;
        
        /**
         * \brief Get the chatroom by chatroomId, create a chatroom if not existed
         *
         * @param  Chatroom id.
         * @return A chatroom
         */
        virtual EMChatroomPtr chatroomWithId(const std::string &chatroomId) = 0;
        
        /**
         * \brief Fetch all chatrooms of the app
         *
         * @param  EMError
         * @return Chatroom  a list of chatrooms
         */
        virtual EMChatroomList fetchAllChatrooms(EMError &error) = 0;
        
        /**
         * \brief Create a new chatroom.
         *
         * Note: Login user will be the owner of the chat room created
         *
         * @param subject           chatroom's subject
         * @param description       chatroom's description.
         * @param welcomeMessage    Welcome message that will be sent to invited users
         * @param setting           chatroom's setting.
         * @param members           a list of chatroom's members
         * @param error             EMError
         * @return                  the chatroom created
         */
        virtual EMChatroomPtr createChatroom(const std::string &subject,
                                             const std::string &description,
                                             const std::string &welcomeMessage,
                                             const EMMucSetting &setting,
                                             const EMMucMemberList &members,
                                             EMError &error) = 0;
        
        /**
         * \brief Destroy a chatroom
         *
         * ONLY chatroom's owner can destroy the chatroom.
         
         * @param  chatroomId   chatroom ID
         * @param  error        EMError used for output
         * @return              NA
         */
        virtual void destroyChatroom(const std::string &chatroomId,
                                     EMError &error) = 0;
        
        /**
         * \brief Change chatroom's subject.
         *
         * ONLY chatroom's owner can change chatroom's subject
         
         * @param  chatroomId   chatroom ID
         * @param  newSubject   The new chatroom subject
         * @param  error        EMError used for output
         * @return              The chatroom that with new subject
         */
        virtual EMChatroomPtr changeChatroomSubject(const std::string &chatroomId,
                                                    const std::string &newSubject,
                                                    EMError &error) = 0;
        
        /**
         * \brief Change chatroom's description
         *
         * ONLY chatroom's owner can change chatroom's description
         
         * @param  chatroomId       chatroom ID
         * @param  newDescription   The new chatroom description
         * @param  error            EMError used for output
         * @return                  The chatroom that with new description
         */
        virtual EMChatroomPtr changeChatroomDescription(const std::string &chatroomId,
                                                        const std::string &newDescription,
                                                        EMError &error) = 0;
        
        /**
         * \brief Change chatroom's extension
         *
         * ONLY chatroom's owner can change chatroom's extension
         
         * @param  chatroomId       chatroom ID
         * @param  newExtension     The new chatroom extension
         * @param  error            EMError used for output
         * @return                  The chatroom that with new extension
         */
        virtual EMChatroomPtr changeChatroomExtension(const std::string &chatroomId,
                                                      const std::string &newExtension,
                                                      EMError &error) = 0;
        
        /**
         * \brief Get chatroom's specifications
         *
         * @param  chatroomId       chatroom ID
         * @param  error            EMError used for output
         * @param  fetchMembers     Wether to fetch members in the chatroom
         * @return                  The chatroom that update it's specification
         */
        virtual EMChatroomPtr fetchChatroomSpecification(const std::string &chatroomId,
                                                         EMError &error,
                                                         bool fetchMembers = false) = 0;
        
        /**
         * \brief add chatroom members
         *
         // TODO: what to expect if I pass empty string as cursor at the first time?
         * Note: Input empty string for cursor at the first time.
         *
         * @param  chatroomId    chatroom ID
         * @param  cursor        Page's cursor
         * @param  pageSize      Page size. ex. 20 for 20 objects
         * @param  error         EMError used for output
         * @return               a list of chatroom members
         */
        virtual const EMCursorResultRaw<std::string> fetchChatroomMembers(const std::string &chatroomId,
                                                                          const std::string &cursor,
                                                                          int pageSize,
                                                                          EMError &error) = 0;
        
        /**
         * \brief Join a chatroom
         *
         * @param  chatroomId   chatroom ID
         * @param  error        EMError used for output
         * @return              The joined chatroom
         */
        virtual EMChatroomPtr joinChatroom(const std::string &chatroomId,
                                           EMError &error) = 0;
        
        /**
         * \brief Leave a chatroom.
         *
         * @param  chatroomId   chatroom ID
         * @param  error        EMError used for output
         */
        virtual void leaveChatroom(const std::string &chatroomId,
                                   EMError &error) = 0;
        
        /**
         * \brief fetch chatroom with cursor
         * 
         // TODO: what to expect if I pass empty string as cursor at the first time?
         * Note: Input empty string for cursor at the first time.
         *
         * @param  cursor       Page's cursor.
         * @param  pageSize     Page size. ex. 20 for 20 objects
         * @param  error        EMError used for output
         * @return              Wrapper of next page's cursor and current page result.
         */
        virtual EMCursorResult fetchChatroomsWithCursor(const std::string &cursor,
                                                        int pageSize,
                                                        EMError &error) = 0;
        
        /**
         * \brief fetch chatroom with page
         *
         // TODO:          * If PageNum=0, then there is no pagination and will get all the users on the blacklist. is this correct?
         * If PageNum=1, then will start from the first page of pagination
         *
         * @param  pageNum      page number of pagination
         * @param  pageSize     Page size. ex. 20 for 20 objects
         * @param  error        EMError used for output
         * @return              Wrapper of current page's count and current page result.
         */
        virtual EMPageResult fetchChatroomsWithPage(int pageNum,
                                                    int pageSize,
                                                    EMError &error) = 0;
        
        /**
         * \brief Get the chatroom by chatroom id
         *
         * @param   chatroomId  chat room ID
         * @return              the chat room
         */
        virtual EMChatroomPtr joinedChatroomById(const std::string &chatroomId) = 0;
        
        /**
         * \brief transfer chatroom newOwner.
         *
         * @param  chatroomId   chatroom ID
         * @param  newOwner     new chatroom owner
         * @param  error        EMError used for output
         * @return
         */
        virtual EMChatroomPtr transferChatroomOwner(const std::string &chatroomId,
                                                    const std::string &newOwner,
                                                    EMError &error) = 0;
        
        /**
         * \brief add admin to the chatroom.
         *
         * ONLY owner has permission, not for admin or member.
         * There can be multiple admins
         *
         * @param   chatroomId  chatroom ID.
         * @param   admin       new admin added
         * @param   error       EMError used for output
         * @return
         */
        virtual EMChatroomPtr addChatroomAdmin(const std::string &chatroomId,
                                               const std::string &admin,
                                               EMError &error) = 0;
        
        /**
         * \brief remove admin to the chatroom. Only owner has permission, not for admin or member.
         *
         * There can be multiple or no admins, but at least one owner
         *
         * @param   chatroomId  chatroom ID.
         * @param   admin       new admin added
         * @param   error       EMError used for output
         * @return
         */
        virtual EMChatroomPtr removeChatroomAdmin(const std::string &chatroomId,
                                                  const std::string &admin,
                                                  EMError &error) = 0;
        
        /**
         * \brief add chatroom muted members
         *
         * Temporary mute members will not be able to talk in the chat room for period of time
         *
         * @param chatroomId    chat room ID
         * @param members       add muted members
         # @param muteDuration  members mute duration in milliseconds
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr muteChatroomMembers(const std::string &chatroomId,
                                                  const EMMucMemberList &members,
                                                  int64_t muteDuration,
                                                  EMError &error) = 0;
        
        /**
         * \brief remove chat room mute members
         *
         * @param chatroomId    chat room ID
         * @param members       remove mute members
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr unmuteChatroomMembers(const std::string &chatroomId,
                                                    const EMMucMemberList &members,
                                                    EMError &error) = 0;
        
        /**
         * \brief add chatroom white list members
         *
         * Temporary whitelist members will not be muted or automatically removed from the room.
         *
         * @param chatroomId    chat room ID
         * @param members       add members
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr addWhiteListMembers(const std::string &chatroomId,
                                                  const EMMucMemberList &members,
                                                  EMError &error) = 0;
        
        
        /**
         * \brief remove chatroom white list members
         *
         *
         * @param chatroomId    chat room ID
         * @param members       remove members
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr removeWhiteListMembers(const std::string &chatroomId,
                                                     const EMMucMemberList &members,
                                                     EMError &error) = 0;
        
        
        /**
         * \brief mute all chat room members
         *
         * Temporary all members not be able to talk in the chat room
         *
         * @param chatroomId    chat room ID
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr muteAllChatroomMembers(const std::string &chatroomId,
                                                     EMError &error) = 0;

        /**
         * \brief unmute chat room all members
         *
         * Temporary remove chat room all mute members
         *
         * @param chatroomId    chat room ID
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr unmuteAllChatroomMembers(const std::string &chatroomId,
                                                       EMError &error) = 0;
        /**
         * \brief fetch chat room mute members
         *
         * If PageNum=0, then there is no pagination and will get all the users on the list
         * If PageNum=1, then will start from the first page of pagination
         *
         * @param  chatroomId   chat room ID
         * @param  pageNum      page number of pagination
         * @param  pageSize     Page size. ex. 20 for 20 objects
         * @param  error        EMError used for output
         * @return              the chat room
         */
        virtual const EMMucMuteList fetchChatroomMutes(const std::string &chatroomId,
                                                       int pageNum,
                                                       int pageSize,
                                                       EMError &error) = 0;
        
        /**
         * \brief fetch chat room white list members
         *
         * If PageNum=0, then there is no pagination and will get all the users on the list
         * If PageNum=1, then will start from the first page of pagination
         *
         * @param  chatroomId   chat room ID
         * @param  error        EMError used for output
         * @return              the chat room
         */
        virtual const EMMucMemberList fetchChatroomWhiteList(const std::string &chatroomId,
                                                             EMError &error) = 0;
        
        /**
         * \brief gets whether the member is on the whitelist
         *
         * @param  chatroomId   chat room ID
         * @param  error        EMError used for output
         * @return              result
         */
        virtual bool fetchIsMemberInWhiteList(const std::string &chatroomId,
                                              EMError &error) = 0;
        
        /**
         * \brief remove chat room members
         *
         * @param chatroomId    chat room ID
         * @param members       remove chat room members
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual EMChatroomPtr removeChatroomMembers(const std::string &chatroomId,
                                                    const EMMucMemberList &members,
                                                    EMError &error) = 0;
        
        /**
         * \brief add user to chat room blacklist
         *
         * @param  chatroomId    chat room ID
         * @param  members       add to chat room blacklist
         * @param  error         EMError used for output
         * @return               the chat room
         */
        virtual EMChatroomPtr blockChatroomMembers(const std::string &chatroomId,
                                                   const EMMucMemberList &members,
                                                   EMError &error) = 0;
        
        /**
         * \brief remove members from chat room blacklist
         *
         * @param  chatroomId    chat room ID
         * @param  members       remeve from chat room blacklist.
         * @param  error         EMError used for output
         * @return               the chat room
         */
        virtual EMChatroomPtr unblockChatroomMembers(const std::string &chatroomId,
                                                     const EMMucMemberList &members,
                                                     EMError &error) = 0;
        
        /**
         * \brief fetch chat room blacklist members
         *
         * If PageNum=0, then there is no pagination and will get all the users on the list
         * If PageNum=1, then will start from the first page of pagination
         *
         * @param chatroomId    chat room ID
         * @param pageNum       page number of pagination
         * @param pageSize      Page size. ex. 20 for 20 objects
         * @param error         EMError used for output
         * @return              the chat room
         */
        virtual const EMMucMemberList fetchChatroomBans(const std::string &chatroomId,
                                                        int pageNum,
                                                        int pageSize,
                                                        EMError &error) = 0;
        
        /**
         * \brief fetch chat room announcement
         *
         * Note: Only chat room members can fetch chat room announcement
         *
         * @param  chatroomId    chat room ID
         * @param  error         EMError used for output
         * @return               The chat room announcement
         */
        virtual std::string fetchChatroomAnnouncement(const std::string &chatroomId,
                                                      EMError &error) = 0;
        
        /**
         * \brief Update chat room announcement
         *
         * Note: Only chatroom's owner can update chatroom's announcement
         
         * @param  chatroomId       chatroom ID
         * @param  newAnnouncement  The new chatroom announcement
         * @param  error            EMError used for output
         * @return                  The chatroom
         */
        virtual EMChatroomPtr updateChatroomAnnouncement(const std::string &chatroomId,
                                                         const std::string &newAnnouncement,
                                                         EMError &error) = 0;
    };
}

#endif /* defined(__easemob__emchatroommanager_interface__) */
