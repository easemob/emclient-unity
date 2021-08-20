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
//  EMCustomMessageBody.h
//
//  Copyright (c) 2015 EaseMob Inc. All rights reserved.
//

#ifndef __easemob__EMCustomMessageBody__
#define __easemob__EMCustomMessageBody__

#include <string>
#include <vector>
#include "emmessagebody.h"

namespace easemob {

class EMCustomMessageBodyPrivate;

class EASEMOB_API EMCustomMessageBody : public EMMessageBody
{
public:
    typedef std::pair<std::string, std::string> EMCustomExt;
    typedef std::vector<EMCustomExt> EMCustomExts;
    /**
      * \brief Custom message body constructor.
      *
      * @param  Custom event
      * @param  Custom exts
      * @return NA
      */
    EMCustomMessageBody(const std::string& event);
    
    /**
      * \brief Class destructor.
      *
      * @param  NA
      * @return NA
      */
    virtual ~EMCustomMessageBody();
    
    /**
      * \brief Get custom event.
      *
      * @param  NA
      * @return The custom event.
      */
    const std::string& event() const;
    
    /**
     * \brief Set custom event.
     *
     * @param  The custom event.
     * @return NA
     */
    void setEvent(const std::string &event) { mEvent = event; }

    /**
      * \brief Get custom exts.
      *
      * @param  NA
      * @return The custom exts.
      */
    const EMCustomExts& exts() const;
    
    /**
      * \brief Set custom exts.
      *
      * @param  The custom exts.
      * @return NA
      */
    void setExts(const EMCustomExts&);
    
protected:
    /**
      * \brief Protected constructor.
      *
      * @param  NA
      * @return NA
      */
    EMCustomMessageBody();
    
private:
    /**
      * \brief Class initializer.
      *
      * @param  NA
      * @return NA
      */
    void init();

private:
    EMCustomMessageBody(const EMCustomMessageBody&);
    EMCustomMessageBody& operator=(const EMCustomMessageBody&);
    virtual void dummy() const{}
    std::string mEvent;
    EMCustomExts mExts;
    friend class EMCustomMessageBodyPrivate;
    friend class EMMessageEncoder;
};

typedef std::shared_ptr<EMCustomMessageBody> EMCustomMessageBodyPtr;

}

#endif /* defined(__easemob__EMCustomMessageBody__) */
