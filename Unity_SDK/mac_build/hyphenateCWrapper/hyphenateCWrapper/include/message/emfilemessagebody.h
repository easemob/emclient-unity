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
//  EMFileMessageBody.h
//
//  Copyright (c) 2015 EaseMob Inc. All rights reserved.
//

#ifndef __easemob__EMFileMessageBody__
#define __easemob__EMFileMessageBody__

#include <string>
#include "message/emmessagebody.h"

namespace easemob {

class EASEMOB_API EMFileMessageBody : public EMMessageBody
{
public:

    /**
     * File downloading status
     */
    typedef enum
    {
        DOWNLOADING,    // Downloading in progress
        SUCCESSED,      // Succeed
        FAILED,         // Failed
        PENDING         // Download has not begun
    } EMDownloadStatus;
    
    /**
      * \brief File message body constructor.
      *
      * @param  Attachment file type.
      * @return NA
      */
    EMFileMessageBody(EMMessageBodyType = FILE);
    
    /**
      * \brief File message body constructor.
      *
      * @param  Attachment local path.
      * @param  Attachment type
      * @return NA
      */
    EMFileMessageBody(const std::string &localPath, EMMessageBodyType = FILE);
    
    /**
      * \brief Class destructor.
      *
      * @param  NA
      * @return NA
      */
    virtual ~EMFileMessageBody();
    
    /**
      * \brief Get display name of the attachment.
      *
      * @param  NA
      * @return The display name.
      */
    std::string displayName() const;
    
    /**
      * \brief Set display name of the attachment.
      *
      * @param  The display name.
      * @return NA
      */
    void setDisplayName(const std::string &);
    
    /**
      * \brief Get local path of the attachment.
      *
      * @param  NA
      * @return The local path.
      */
    const std::string& localPath() const;
    
    /**
      * \brief Set local path of the attachment.
      *
      * Note: should NOT change the local path of the Received meesage.
      * @param  The local path.
      * @return NA
      */
    void setLocalPath(const std::string &);
    
    /**
      * \brief Get remote path of the attachment.
      *
      * @param  NA
      * @return The remote path.
      */
    const std::string& remotePath() const;
    
    /**
      * \brief Set remote path of the attachment.
      *
      * Note: It's internal used, user should never need to call this method.
      * @param  The remote path.
      * @return NA
      */
    void setRemotePath(const std::string &);
    
    /**
      * \brief Get secret key of the attachment, it's used to download attachment from server.
      *
      * @param  NA
      * @return The secret key.
      */
    const std::string& secretKey() const;
    
    /**
      * \brief Set secret key of the attachment.
      *
      * Note: It's internal used, user should never need to call this method.
      * @param  The secret key.
      * @return NA
      */
    void setSecretKey(const std::string &);
    
    /**
      * \brief Get file length of the attachment.
      *
      * @param  NA
      * @return The file length.
      */
    int64_t fileLength() const;
    
    /**
      * \brief Set file length of the attachment.
      *
      * Note: It's usually not necessary to call this method, will calculate file length automatically when setting local path.
      * @param  The file length.
      * @return NA
      */
    void setFileLength(int64_t);
    
    /**
      * \brief Get file downloading status
      *
      * @param  NA
      * @return The file downloading status
      */
    EMDownloadStatus downloadStatus() const;
    
    /**
      * \brief Set file downloading status
      *
      * Note: Usually, user should NOT call this method directly.
      * @param  The download status.
      * @return NA
      */
    void setDownloadStatus(EMDownloadStatus);
    
private:
    /**
      * \brief Class initializer.
      *
      * @param  NA
      * @return NA
      */
    void init();
    
private:
    EMFileMessageBody(const EMFileMessageBody&);
    EMFileMessageBody& operator=(const EMFileMessageBody&);
    virtual void dummy() const{}
    std::string mDisplayName;
    std::string mLocalPath;
    std::string mRemotePath;
    std::string mSecretKey;
    int64_t mFileLength;
    EMDownloadStatus mDownloadStatus;
};

typedef std::shared_ptr<EMFileMessageBody> EMFileMessageBodyPtr;

}

#endif /* defined(__easemob__EMFileMessageBody__) */
