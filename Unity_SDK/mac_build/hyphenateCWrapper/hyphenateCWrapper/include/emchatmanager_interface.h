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
//
//  EMChatManagerInterface.h
//
//  Copyright (c) 2015 EaseMob Inc. All rights reserved.
//

#ifndef __easemob__EMChatManagerInterface__
#define __easemob__EMChatManagerInterface__

#include "message/emmessage.h"
#include "emcallback.h"

#include "emcursorresult.h"
#include "emchatmanager_listener.h"
#include "emconversation.h"
#include "emencryptprovider_interface.h"

namespace easemob {

class EASEMOB_API EMChatManagerInterface
{
public:
    /**
      * \brief Destructor.
      *
      * @param  NA
      * @return NA
      */
    virtual ~EMChatManagerInterface() { };
    
    /**
      * \brief Send a message
      *
     // TODO: what is this following sentance mean?
      * Will callback user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
      *
      * @param  EMMessagePtrThe     message to send.
      * @return                     NA
      */
    virtual void sendMessage(const EMMessagePtr) = 0;
    
    /**
      * \brief Send read ask for a message
      *
      * @param  message     the message to be mark as read ack
      * @return             NA
      */
    virtual void sendReadAckForMessage(const EMMessagePtr) = 0;
    
    /**
     * \brief Send read ask for a group message
     *
     * @param  message     the message to be mark as read ack
     * @param  ackContent  user defined attach message for ack
     * @return             NA
     */
    virtual void sendReadAckForGroupMessage(const EMMessagePtr, std::string ackContent = "") = 0;

    /**
     * \brief Send read ask for a conversation
     *
     * @param conversationId     the conversation id
     * @param EMError            used for output.
     * @return                   NA
     */
    virtual void sendReadAckForConversation(const std::string &conversationId, 
                                            EMError& error) = 0;
    
    /**
     * \brief  Recall a message
     *
     * @param  message     the message to be recalled
     * @param  EMError     used for output.
     * @return             NA
     */
    virtual void recallMessage(const EMMessagePtr,
                               EMError& error) = 0;
    
    /**
      * \brief Resend a message
      *
      * Will callback user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
      *
      * @param  message     the message to be re-send.
      * @return             NA
      */
    virtual void resendMessage(const EMMessagePtr) = 0;
    
    /**
      * \brief Download thumbnail for image or video message
      *
      * Image and video message thumbnail will be downloaded automatically. ONLY call this method if automatic download failed.
        SDK will callback the user by EMChatManagerListener if user doesn't provide a callback in the message or callback return false.
      *
      * @param  The message to download thumbnail.
      * @return NA
      */
    virtual void downloadMessageThumbnail(const EMMessagePtr) = 0;
    
    /**
      * \brief Download attachment of a message.
      *
      * User should call this method to download file, voice, image, video.
        SDK will callback the user by EMChatManagerListener if user doesn't provide a callback or callback return false.
      * 
      * @param  The message to download attachment.
      * @return NA
      */
    virtual void downloadMessageAttachments(const EMMessagePtr) = 0;
    
    /**
     * \brief Get group's message acks from server
     *
     * @param The group message id.
     * @param The group id.
     * @param EMError used for output.
     * @param the page size.
     * @param the start ackId.
     * @return server return acks.
     */
    virtual EMCursorResultRaw<EMGroupReadAckPtr> fetchGroupReadAcks(const std::string &msgId,
                                                                     const std::string &groupId,
                                                                     EMError& error,
                                                                     int pageSize = 20,
                                                                     int *totalCount = nullptr,
                                                                     const std::string& startAckId = "") = 0;
    
    /**
      * \brief Remove a conversation from cache and local database
      *
      * Before removing a conversation, all conversations must be loaded from local database first
      * 
      * @param  conversationId      The conversation id
      * @param  isRemoveMessages    The flag of whether remove the messages belongs to this conversation.
      * @return NA
      */
    virtual void removeConversation(const std::string &conversationId,
                                    bool isRemoveMessages = true) = 0;
    
    /**
     * \brief Remove conversations from cache and local database
     *
     * Before removing conversations, all conversations must be loaded from local database first
     *
     * @param  list                The list of conversation
     * @param  isRemoveMessages    The flag of whether remove the messages belongs to those conversation.
     * @return NA
     */
    virtual void removeConversations(const EMConversationList& list,
                                     bool isRemoveMessages = true) = 0;
    
    /**
      * \brief Get a conversation
      *
      * All conversations will be loaded from local database
      * 
      * @param  conversationId      The conversation id.
      * @param  type                EMConversationType
      * @param  createIfNotExist    The flag of whether created a conversation if it isn't exist.
      * @return                     The conversation
      */
    virtual EMConversationPtr conversationWithType(const std::string &conversationId,
                                                   EMConversation::EMConversationType type,
                                                   bool createIfNotExist = true) = 0;
    
    /**
      * \brief Get all conversations from cache or local database if not in cache.
      *
      * @return The conversation list
      */
    virtual EMConversationList getConversations() = 0;
    
    /**
      * \brief Get all conversations from server.
      *
      * @return The conversation list
      */
    virtual EMConversationList getConversationsFromServer(EMError &error) = 0;
    
    /**
      * \brief Get all conversations from local database
      *
      * @return The conversation list
      */
    virtual EMConversationList loadAllConversationsFromDB() = 0;
    
    /**
      * \brief Add chat manager listener
      *
      * @param  EMChatManagerListener
      * @return                         NA
      */
    virtual void addListener(EMChatManagerListener*) = 0;
    
    /**
      * \brief Remove chat manager listener
      *
      * @param  EMChatManagerListener
      * @return                         NA
      */
    virtual void removeListener(EMChatManagerListener*) = 0;
    
    /**
      * \brief Remove all the chat manager listeners
      *
      * @return NA
      */
    virtual void clearListeners() = 0;
    
    /**
     * \brief Application can customize encrypt method through EMEncryptProvider
     *
     * If EMConfigManager#KEY_USE_ENCRYPTION is true, but no encryptprovider provided, then SDK will use default encryption method.
     * 
     * @param  provider     EMEncryptProvider Customized encrypt method provider.
     * @return              NA
     */
    virtual void setEncryptProvider(EMEncryptProviderInterface *provider) = 0;
    
    /**
     * \brief Get encrypt method being used
     *
     * @param  createIfNotExist     If createIfNotExist is true, then SDK will use the default encryptProvider if there is no encryptProvider exist.
     * @return                      Encrypt method being used
     */
    virtual EMEncryptProviderInterface *getEncryptProvider(bool createIfNotExist = false) = 0;
    
    /**
     * \brief Insert messages
     * 
     * @param  list     The messages to be inserted
     * @return          NA
     */
    virtual bool insertMessages(const EMMessageList& list) = 0;

    /**
     * \brief fetch conversation roam messages from server.
     * @param the conversation id which select to fetch roam message.
     * @param the conversation type which select to fetch roam message.
     * @param EMError used for output.
     * @param the page size.
     * @param the start search roam message, if empty start from the server leastst message.
     * @return server return messages.
     */
    virtual EMCursorResultRaw<EMMessagePtr> fetchHistoryMessages(const std::string &conversationId, 
        EMConversation::EMConversationType type, 
        EMError& error,
        int pageSize = 20,
        const std::string& startMsgId = "") = 0;

    /**
     * \brief Get message by message Id.
     *
     * @param  messageId    message to be obtained
     * @return              EMMessagePtr
     */
    virtual EMMessagePtr getMessage(const std::string &messageId) = 0;
    
    /**
     * \brief update database participant related records, include message table, conversation table, contact table, blacklist table
     */
    virtual bool updateParticipant(const std::string &from,
                                   const std::string &changeTo) = 0;
    
    /**
     * \brief Upload log to server.
     */
    virtual void uploadLog() = 0;

    /**
     * \brief load more messages with type.
     */
    virtual EMMessageList loadMoreMessages(int64_t timeStamp, EMMessageBody::EMMessageBodyType type, int count, const std::string &from, EMConversation::EMMessageSearchDirection direction) = 0;
    
    /**
     * \brief load more messages with keywords.
     */
    virtual EMMessageList loadMoreMessages(int64_t timeStamp, const std::string& keywords, int count, const std::string &from, EMConversation::EMMessageSearchDirection direction) = 0;
    
};

}

#endif /* defined(__easemob__EMChatManagerInterface__) */
