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
#ifndef EMCORE_CALL_MANAGER_INTERFACE_H
#define EMCORE_CALL_MANAGER_INTERFACE_H

#include <string>

#include "emcallmanager_listener.h"
#include "call/emcallconference_listener.h"
#include "utils/emhttprequest.h"
#include "call/emcallmanagerconfigs.h"
#include "emcallsession.h"

namespace easemob {
   
//op:
//RequestTicket. 请求ticket "easemob/webrtc/req/ticket"
//UpdateRole. 更新成员角色 "easemob/webrtc/chanage/roles"
//KickMember. 踢人 "easemob/webrtc/kick/member"
//DestroyConference. 解散会议 "easemob/webrtc/disband/conference"
//WhetherConferenceExists. 判断会议是否存在 "easemob/webrtc/select/confr"
typedef enum {
    RequestTicket = 0,
    UpdateRole,
    KickMember,
    DestroyConference,
    WhetherConferenceExists,
    UpdateAttribute,
    DegradeRole,
    RequestRoom,
    UpdateLayout,
    DeleteLive,
    AddLive,
    CustomRecord,
} EMMediaRestOp;
    

typedef enum {
    CREATE_ROOM = 0,
    DESTROY_ROOM,
    JOIN_ROOM_WITH_ID,
    JOIN_ROOM_WITH_NAME,
    UPDATE_INTERACT,
} EMWBRestOp;
    
class EMCallRtcProxyInterface;
class EASEMOB_API EMCallManagerInterface {
public:
    
    virtual ~EMCallManagerInterface() { };
    
    /**
     * \brief Add a listener to chat manager
     *
     * @param  EMCallManagerListener
     * @return NA
     */
    virtual void addListener(EMCallManagerListener*) = 0;
    
    /**
     * \brief Remove a listener
     *
     * @param  EMCallManagerListener
     * @return NA
     */
    virtual void removeListener(EMCallManagerListener*) = 0;
    
    /**
     * \brief Remove all the listeners
     *
     * @param  NA
     * @return NA
     */
    virtual void clearListeners() = 0;
    
    virtual void setRtcProxy(EMCallRtcProxyInterface *) = 0;
    
    virtual void setCallConfigs(EMCallConfigsPtr configs) = 0;
    
    virtual EMCallConfigsPtr getCallConfigs() = 0;
    
    // 1v1
    virtual EMCallSessionPtr asyncMakeCall(const std::string& remoteName, emcall::Type type, const std::string& ext, EMError& error,
                                           bool isRecord = false, bool isMergeStream = false) = 0;
    
    virtual void asyncAnswerCall(const std::string& callId, EMError& error) = 0;
    
    virtual void asyncEndCall(const std::string& callId, emcall::EndReason reason) = 0;
    
    virtual void updateCall(const std::string& callId, emcall::StreamControlType controlType, EMError& error) = 0;
    
    virtual void forceEndAllCall() = 0;
    
    // conference
    virtual void setCallConferenceListener(EMCallConferenceListener *) = 0;
    virtual void removeCallConferenceListener() = 0;
    //confId是返回值，password既是参数也是返回值
    virtual std::string confCreatorGetTicketFromServer(std::string& confId, std::string& password, EMError& error) = 0;
    virtual std::string confMemberGetTicketFromServer(const std::string& confId, const std::string& password, EMError& error) = 0;
    //10s后删除
    virtual void inviteUserToJoinConference(const std::string& confId, const std::string& remoteName, const std::string& pswd, const std::string& ext, EMError& error) = 0;
    
    virtual std::string requestMediaFromServer(EMMediaRestOp op, const std::string& body, EMError &error) = 0;
    
    virtual std::string requestMediaFromServer(EMMediaRestOp op, const std::string& urlParams, const std::string& body, EMError &error) = 0;
    
    virtual std::string requestWBFromServer(easemob::EMWBRestOp op, const std::string &token, const std::string& roomId, const std::string& parameters, easemob::EMError &error) = 0;
};
}

#endif
