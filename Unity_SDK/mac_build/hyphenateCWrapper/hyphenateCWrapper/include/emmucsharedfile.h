/************************************************************
 *  * EaseMob CONFIDENTIAL
 * __________________
 * Copyright (C) 2017 EaseMob Technologies. All rights reserved.
 * 
 * NOTICE: All information contained herein is, and remains
 * the property of EaseMob Technologies.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from EaseMob Technologies.
 */

#ifndef __easemob__emmucsharedfile__
#define __easemob__emmucsharedfile__

#include "embaseobject.h"
#include <memory>
#include <string>

namespace easemob 
{

class EASEMOB_API EMMucSharedFile : public EMBaseObject
{
public:
    EMMucSharedFile(const std::string fileId, const std::string fileName, const std::string fileOwner, uint64_t create, uint64_t fileSize = 0) :
    mFileId(fileId), mFileName(fileName), mFileOwner(fileOwner), mCreate(create), mFileSize(fileSize)
    {}
    virtual ~EMMucSharedFile(){}

    const std::string fileId() const { return mFileId; }
    const std::string fileName() const { return mFileName; }
    const std::string fileOwner() const {return mFileOwner; }
    uint64_t create() const { return mCreate; }
    uint64_t fileSize() const { return mFileSize; }

private:
    EMMucSharedFile(const EMMucSharedFile& shareFile);
    EMMucSharedFile& operator=(const EMMucSharedFile& shareFile);

    std::string mFileId;
    std::string mFileName;
    std::string mFileOwner;
    uint64_t mCreate;
    uint64_t mFileSize;
};

typedef std::shared_ptr<EMMucSharedFile> EMMucSharedFilePtr;
}

#endif