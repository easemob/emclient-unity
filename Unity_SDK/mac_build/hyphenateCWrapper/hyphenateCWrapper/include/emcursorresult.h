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

#ifndef __easemob__EMCursorResult__
#define __easemob__EMCursorResult__

#include <vector>
#include <string>

#include "embaseobject.h"

namespace easemob
{

template<typename T>
class EASEMOB_API EMCursorResultRaw : public EMBaseObject
{
public:
    EMCursorResultRaw(const std::vector<T> &result, const std::string &nextPageCursor) :
        mResult(result), mNextPageCursor(nextPageCursor) {}
    virtual ~EMCursorResultRaw() {};
    
    EMCursorResultRaw(const EMCursorResultRaw &a) {
        mResult = a.mResult;
        mNextPageCursor = a.mNextPageCursor;
    }
    
    /**
      * \brief Get cursor of next page.
      *
      * @param  NA
      * @return The cursor.
      */
    const std::string& nextPageCursor() const { return mNextPageCursor; }
    
    /**
      * \brief Get the result of current page.
      *
      * @param  NA
      * @return The result.
      */
    const std::vector<T>& result() const { return mResult; }
    
private:
    std::vector<T> mResult;
    std::string mNextPageCursor;
};
typedef EMCursorResultRaw<EMBaseObjectPtr> EMCursorResult;


template<typename T>
class EASEMOB_API EMPageResultRaw : public EMBaseObject
{
public:
    EMPageResultRaw(const std::vector<T> &result, const int count) :
        mResult(result), mCount(count) {}
    virtual ~EMPageResultRaw() {};
    
    EMPageResultRaw(const EMPageResultRaw &a) {
        mResult = a.mResult;
        mCount = a.mCount;
    }
    
    /**
     * \brief Get count.
     *
     * @param  NA
     * @return The count.
     */
    const int count() const { return mCount; }
    
    /**
     * \brief Get the result of current page.
     *
     * @param  NA
     * @return The result.
     */
    const std::vector<T>& result() const { return mResult; }
private:
    std::vector<T> mResult;
    int mCount;
};
typedef EMPageResultRaw<EMBaseObjectPtr> EMPageResult;
    
}

#endif /* defined(__easemob__EMCursorResult__) */
