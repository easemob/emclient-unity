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
//  EMMessage.h
//
//  Copyright (c) 2015 EaseMob Inc. All rights reserved.
//

#ifndef __easemob__EMMessage__
#define __easemob__EMMessage__

#include <memory>
#include <map>

#include "embaseobject.h"
#include "emmessagebody.h"
#include "emcallback.h"
#include "emjsonstring.h"

namespace easemob {
    
    class EMAttributeValue;
    class EMMessagePrivate;
    
    class EASEMOB_API EMMessage : public EMBaseObject
    {
    public:
        
        /**
         *  Message chat type.
         */
        typedef enum
        {
            SINGLE,     // One-to-one chat
            GROUP,      // Group chat
            CHATROOM    // Chatroom chat
        } EMChatType;
        
        /**
         *  Message status.
         */
        typedef enum
        {
            NEW,            // New message
            DELIVERING,     // Message is delivering
            SUCCESS,        // Message is delivered successfully
            FAIL            // Message delivering failed
        } EMMessageStatus;
        
        typedef enum {
            SEND,
            RECEIVE
        } EMMessageDirection;

        /**
         * \brief Class destructor.
         *
         * @param  NA
         * @return NA
         */
        virtual ~EMMessage();
        
        /**
         * \brief Get message id.
         *
         * @param  NA
         * @return The message id.
         */
        const std::string& msgId();
        
        /**
         * \brief Set message id.
         *
         * Note: User should never change a message's id if you don't want to save as a new message.
         * @param  The new message id.
         * @return NA.
         */
        void setMsgId(const std::string &);
        
        /**
         * \brief Get message sender.
         *
         * @param  NA
         * @return The message sender.
         */
        const std::string& from();
        
        /**
         * \brief Set message sender.
         *
         * @param  The new message sender.
         * @return NA.
         */
        void setFrom(const std::string&);
        
        /**
         * \brief Get message receiver.
         *
         * @param  NA
         * @return The message receiver.
         */
        const std::string& to();
        
        /**
         * \brief Set message receiver.
         *
         * @param  The new message receiver.
         * @return NA.
         */
        void setTo(const std::string&);
        
        /**
         * \brief Get conversation id.
         *
         * @param  NA
         * @return The conversation id.
         */
        const std::string& conversationId();
        
        /**
         * \brief Set message's conversation id.
         *
         * Note: User should NOT change message's conversation id after received or sent a message.
         * @param  The new conversation id.
         * @return NA.
         */
        void setConversationId(const std::string&);
        
        /**
         * \brief Get message status.
         *
         * @param  NA
         * @return The message status.
         */
        EMMessageStatus status();
        
        /**
         * \brief Set message's status.
         *
         * Note: User should NOT change message's status directly.
         * @param  The new message status.
         * @return NA.
         */
        void setStatus(EMMessageStatus);
        
        /**
         * \brief Get message chat type.
         *
         * @param  NA
         * @return The message chat type.
         */
        EMChatType chatType();
        
        /**
         * \brief Set message's chat type.
         *
         * Note: User should NOT change message's chat type after receive or send a message.
         * @param  The new conversation id.
         * @return NA.
         */
        void setChatType(EMChatType);
        
        /**
         * \brief Get message direction.
         *
         * @param  NA
         * @return The message direction.
         */
        EMMessageDirection msgDirection();
        
        /**
         * \brief Set message's direction.
         *
         * Note: User should NOT change message's message direction after received or sent a message.
         * @param  NA
         * @return NA.
         */
        void setMsgDirection(EMMessageDirection);
        
        /**
         * \brief Get if message is read status.
         *
         * @param  NA
         * @return The message read status.
         */
        bool isRead();
        
        /**
         * \brief Set if message is read status.
         *
         * Note: User should NOT change message's read status directly.
         * @param  The new message read status.
         * @return NA.
         */
        void setIsRead(bool isRead);
        
        /**
         * \brief Get message if has listened status.
         *
         * @param  NA
         * @return The message listened status.
         */
        bool isListened();
        
        /**
         * \brief Set message's listened status.
         *
         * Note: User should NOT change message's listened status directly.
         * @param  The new message listened status.
         * @return NA.
         */
        void setIsListened(bool isListened);
        
        /**
         * \brief Get message read ack status.
         *
         * Note: For receiver, it indicates whether has sent read ack, and for sender, it indicates whether has received read ack.
         * @param  NA
         * @return The message read ack status.
         */
        bool isReadAcked();
        
        /**
         * \brief Set message's read ack status.
         *
         * Note: User should NOT change message's read ack status directly.
         * @param  The new message read ack status.
         * @return NA.
         */
        void setIsReadAcked(bool);
        
        /**
         * \brief Get message delivering status.
         *
         * Note: For receiver, it indicates whether has sent delivering succeed ack; and for sender, it indicates whether has received delivering succeed ack.
         * @param  NA
         * @return The message delivering status.
         */
        bool isDeliverAcked();
        
        /**
         * \brief Set message's delivery ack status.
         *
         * Note: User should NOT change message's delivery ack status directly.
         * @param  The new message delivery ack status.
         * @return NA.
         */
        void setIsDeliverAcked(bool);
        
        /**
         * \brief Get message timestamp(server time).
         *
         * @param  NA
         * @return The message timestamp.
         */
        int64_t timestamp();
        
        /**
         * \brief Set message's timestamp.
         *
         * Note: User should NOT change message's timestamp.
         * @param  The new message timestamp.
         * @return NA.
         */
        void setTimestamp(int64_t);
        
        /**
         * \brief Get message's local time.
         *
         * @param  NA
         * @return The server time.
         */
        int64_t localTime();
        
        /**
         * \brief Set message's local time.
         *
         * Note: User should NOT change message's server time.
         * @param  The new message server time.
         * @return NA.
         */
        void setLocalTime(int64_t);
        
        /**
         * \brief Get property if need group ack.
         *
         * @return if this message need group ack.
         */
        bool isNeedGroupAck();
        
        /**
         * \brief Set property if need group ack.
         *
         * @Param if this message need group ack.
         */
        void setIsNeedGroupAck(bool);
        
        
        int16_t groupAckCount();
        
        
        void setGroupAckCount(int16_t);

        /**
         * \brief Get ack content.
         *
         * @return the content with ack.
         */
        const std::string ackContent();
        
        /**
         * \brief Set ack content.
         *
         * @Param user defined content for ack.
         */
        void setAckContent(std::string ackContent);
        
        /**
         * \brief Get message body list.
         *
         * @param  NA
         * @return The message body list.
         */
        std::vector<EMMessageBodyPtr> bodies();
        
        
        /**
         * \brief Clear all bodies.
         * 清空message里面的所有messagebody。每个message中可以有多个messagebody，messagebod是一个列表。
         *
         * @param  NA
         * @return NA.
         */
        void clearBodies();
        
        /**
         * \brief Add a body to message.
         *
         * 添加messagebody。一个消息里面可以有多个messagebody。但是现在我们处理上只有一个。
         * Note: The ownership of the body will be transfered, user must NOT release it.
         * @param  A message body.
         * @return NA
         */
        void addBody(const EMMessageBodyPtr &body);
        
        /**
         * \brief Add a extend attribute to message.
         *
         * Note: Supported types: bool, int32_t, uint32_t, int64_t, double, string and EMJsonString. If the attribute already existed, then will replace existing one.
         * @param  The attribute key.
         * @param  The attribute value.
         * @return NA
         */
        template<typename T>
        void setAttribute(const std::string &attribute, const T &value);
        
        /**
         * \brief Get extend attribute of message.
         *
         * Note: Supported types: bool, int, unsigned int, int64_t and string.
         * @param  The attrubute key.
         * @param  The attrubute value, it's a out argument.
         * @return Return false if attribute not exist or attribute type is wrong.
         */
        template<typename T>
        bool getAttribute(const std::string &attribute, T &value);
        
        /**
         * \brief Remove a attribute from message by key.
         *
         * @param  The attribute key.
         * @return NA
         */
        void removeAttribute(const std::string &attribute);
        
        /**
         * \brief Remove all attributes from message.
         *
         * @param  NA
         * @return NA
         */
        void clearAttributes();
        
        /**
         * \brief Get all attributes from message.
         *
         * @param  NA
         * @return Attributes map.
         */
        std::map<std::string, std::shared_ptr<EMAttributeValue> > ext();
        
        /**
         * \brief Get message's callback to notify status change.
         *
         * @param  NA
         * @return The callback.
         */
        EMCallbackPtr callback();
        
        /**
         * \brief Set message's callback to notify status change.
         *
         * @param  The callback.
         * @return NA.
         */
        void setCallback(EMCallbackPtr);

        void setProgress(float percent) { mProgress = percent; }
        
        float getProgress() { return mProgress; }
        
    public:
        // factory method
        /**
         * \brief Create a receiving message.
         *
         * @param  The message sender.
         * @param  The message receiver.
         * @param  The message body.
         * @param  The message chat type.
         * @param  The message id.
         * @return A message instance.
         */
        static std::shared_ptr<EMMessage> createReceiveMessage(const std::string &from, const std::string &to, const EMMessageBodyPtr &body, EMChatType = SINGLE, const std::string &msgId = "");
        
        /**
         * \brief Create a sending message.
         *
         * @param  The message sender.
         * @param  The message receiver.
         * @param  The message body.
         * @param  The message chat type.
         * @return A message instance.
         */
        static std::shared_ptr<EMMessage> createSendMessage(const std::string &from, const std::string &to, const EMMessageBodyPtr &body, EMChatType = SINGLE);
    
    private:
        /**
         * \brief Private message constructor.
         *
         * Note: User should always use the factory method to create a message.
         * @param  Message chat type.
         * @return NA
         */
        EMMessage(EMChatType = SINGLE);
        EMMessage(const std::string& msgId, EMChatType = SINGLE);
        EMMessage(const EMMessage&);
        EMMessage& operator=(const EMMessage&);
        
    private:
        void *mMutex;
        std::string mMsgId;
        std::string mFrom;
        std::string mTo;
        std::string mConversationId;
        
        EMMessageStatus mStatus;
        EMChatType mChatType;
        EMMessageDirection mDirection;
        
        bool mIsRead;
        bool mIsListened;
        bool mIsReadAcked;
        bool mIsDeliverAcked;
        bool mIsNeedGroupAck;
        int16_t mGroupAckCount;
        std::string mAckContent;
        int64_t mTimestamp;
        int64_t mLocalTime;
        std::vector<EMMessageBodyPtr> mBodies;
        std::map<std::string, std::shared_ptr<EMAttributeValue> > mExt;
        EMCallbackPtr mCallback;

        EMMessagePrivate* messagePrivate;
        friend class EMMessagePrivate;
        friend class EMMessageEncoder;
        float mProgress;
        
    };
    
    typedef std::shared_ptr<EMMessage> EMMessagePtr;
    typedef std::vector<EMMessagePtr> EMMessageList;
    
    class EASEMOB_API EMGroupReadAck {
    public:
        std::string meta_id;
        EMMessagePtr msgPtr;
        std::string from;
        std::string content;
        int count;
        int64_t timestamp;
    };

    typedef std::shared_ptr<EMGroupReadAck> EMGroupReadAckPtr;
    typedef std::vector<EMGroupReadAckPtr> EMGroupReadAckList;
}

#endif /* defined(__easemob__EMMessage__) */
