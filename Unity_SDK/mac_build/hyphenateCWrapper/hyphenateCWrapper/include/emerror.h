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
//  EMError.h
//
//  Copyright (c) 2015 EaseMob Inc. All rights reserved.
//

#ifndef __easemob__EMError__
#define __easemob__EMError__

#include "embaseobject.h"

#include <string>

namespace easemob {

class EASEMOB_API EMError : EMBaseObject
{
public:
   
    typedef enum
    {
        EM_NO_ERROR = 0,                    //No error
        
        GENERAL_ERROR,                      //General error
        NETWORK_ERROR,                      //Network isn't avaliable
        DATABASE_ERROR,                     //Database operation failed
        EXCEED_SERVICE_LIMIT,               //Exceed service limit
        SERVICE_ARREARAGES,                 //Need charge for service
        
        INVALID_APP_KEY = 100,              //App key is invalid
        INVALID_USER_NAME,                  //User name is illegal
        INVALID_PASSWORD,                   //Password is illegal
        INVALID_URL,                        //URL is invalid
        INVALID_TOKEN,                      //Token is invalid
        USER_NAME_TOO_LONG,                 //User name too long
        CHANNEL_SYNC_NOT_OPEN,              //Channel sync not open
        INVALID_CONVERSATION,               //Invalid conversation
        
        USER_ALREADY_LOGIN_SAME = 200,      //Same User is already login
        USER_NOT_LOGIN,                     //User has not login
        USER_AUTHENTICATION_FAILED,         //User name or password is wrong
        USER_ALREADY_EXIST,                 //User has already exist
        USER_NOT_FOUND,                     //User dosn't exist
        USER_ILLEGAL_ARGUMENT,              //Illegal argument
        USER_LOGIN_ANOTHER_DEVICE,          //User login on another device
        USER_REMOVED,                       //User was removed from server
        USER_REG_FAILED,                    //User register failed
        PUSH_UPDATECONFIGS_FAILED,          //Update push configs failed
        USER_PERMISSION_DENIED,             //User has no right for this operation
        USER_BINDDEVICETOKEN_FAILED,        //Bind device token failed
        USER_UNBIND_DEVICETOKEN_FAILED,     //Unbind device token failed
        USER_BIND_ANOTHER_DEVICE,           //Bind another device and do not allow auto login
        USER_LOGIN_TOO_MANY_DEVICES,        //User login on too many devices
        USER_MUTED,                         //User mutes in groups or chatrooms
        USER_KICKED_BY_CHANGE_PASSWORD,     //User has changed the password
        USER_KICKED_BY_OTHER_DEVICE,        //User was kicked by other device or console backend
        USER_ALREADY_LOGIN_ANOTHER,         //Another User is already login
        
        SERVER_NOT_REACHABLE = 300,         //Server is not reachable
        SERVER_TIMEOUT,                     //Wait server response timeout
        SERVER_BUSY,                        //Server is busy
        SERVER_UNKNOWN_ERROR,               //Unknown server error
        SERVER_GET_DNSLIST_FAILED,          //Can't get dns list
        SERVER_SERVING_DISABLED,            //Serving is disabled
        SERVER_DECRYPTION_FAILED,           //Server transfer decryption failure.
        SERVER_GET_RTCCONFIG_FAILED,        //Can't get rtc config
        
        FILE_NOT_FOUND = 400,               //File isn't exist
        FILE_INVALID,                       //File is invalid
        FILE_UPLOAD_FAILED,                 //Failed uploading file to server
        FILE_DOWNLOAD_FAILED,               //Failed donwloading file from server
        FILE_DELETE_FAILED,                 //Failed delete file
        FILE_TOO_LARGE,                     //file too large to upload
        FILE_CONTENT_IMPROPER,              //file content improper
        
        MESSAGE_INVALID = 500,              //Message is invalid
        MESSAGE_INCLUDE_ILLEGAL_CONTENT,    //Message include illegal content
        MESSAGE_SEND_TRAFFIC_LIMIT,
        MESSAGE_ENCRYPTION_ERROR,
        MESSAGE_RECALL_TIME_LIMIT,          //Message recall exceed time limit
        SERVICE_NOT_ENABLED,
        MESSAGE_EXPIRED,                    //The message has expired
        MESSAGE_ILLEGAL_WHITELIST,          //The message has delivery failed because it was not in the whitelist
        MESSAGE_EXTERNAL_LOGIC_BLOCKED,     //The message blocked by external logic
        
        GROUP_INVALID_ID = 600,             //Group id is invalid
        GROUP_ALREADY_JOINED,               //User has already joined the group
        GROUP_NOT_JOINED,                   //User has not joined the group
        GROUP_PERMISSION_DENIED,            //User has no right for this operation
        GROUP_MEMBERS_FULL,                 //Group members is full
        GROUP_NOT_EXIST,                    //Group is not exist
        
        CHATROOM_INVALID_ID = 700,          //Chatroom id is invalid
        CHATROOM_ALREADY_JOINED,            //User has already joined the chatroom
        CHATROOM_NOT_JOINED,                //User has not joined the chatroom
        CHATROOM_PERMISSION_DENIED,         //User has no right for this operation
        CHATROOM_MEMBERS_FULL,              //Chatroom members is full
        CHATROOM_NOT_EXIST,                 //Chatroom is not exist

        CALL_INVALID_ID = 800,              //Call id is invalid
        CALL_BUSY,                          //Call in progress
        CALL_REMOTE_OFFLINE,                //remote offline
        CALL_CONNECTION_FAILED,             //Establish connection failed
        CALL_INVALID_CAMERA_INDEX,          //Invalid camera index
        CALL_OPERATION_CANCEL,              //Call cancel operation
        CALL_PERMISSION_DENIED,             //Call permission denied
        CALL_NOT_JOINED,                    //Call not joined
        CALL_JOIN_FAILED,                   //Call join failed
        CALL_CREATE_FAILED,                 //Call create failed
        CALL_UNSUB_FAILED,                  //Call unsub failed
        CALL_ROOM_NOT_EXIST = 816,          //conference room not exist or timeout
        CALL_SPEAKERS_LIMIT = 823,          //Speakers in conference is limit
        CALL_CDN_ERROR = 825,               //CDN push error
        CALL_OPTION_CANCEL=CALL_OPERATION_CANCEL,
        
        USERINFO_USERCOUNT_EXCEED = 900,    //Uses count exceed
        USERINFO_DATALENGTH_EXCEED = 901,   //Userinfo data lenght exceed
        
    }EMErrorCode;
    
    /**
      * \brief EMCallback's constructor.
      *
      * Note: If description is empty, constructor will provide default description accord the error code.
      * @param  Error code
      * @param  Error description
      * @return NA
      */
    EMError(const EMError &e) {
        mErrorCode = e.mErrorCode;
        mDescription = e.mDescription;
    }
    EMError(int errorCode = EM_NO_ERROR, const std::string &description = "");
    void setErrorCode(int errorCode, const std::string &description = "");
    virtual ~EMError() {}
    EMError & operator=(const EMError& e) {
        mErrorCode = e.mErrorCode;
        mDescription = e.mDescription;
        return *this;
    }
    
    static bool isNoError(std::shared_ptr<EMError> e) {
        return (e && e->mErrorCode == EM_NO_ERROR);
    }
    
    int mErrorCode;
    std::string mDescription;
};

typedef std::shared_ptr<EMError> EMErrorPtr;

}

#endif /* defined(__easemob__EMError__) */
