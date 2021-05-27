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
#ifndef EMCORE_CALLMANAGER_H
#define EMCORE_CALLMANAGER_H

#include <set>
#include <string>
#include <vector>
#include <map>

#include "emmanager_base.h"
#include "calleventhandler.h"
#include "emsessionmanager.h"
#include "emconfigmanager.h"
#include "chatclient.hpp"
#include "emcallenum.h"
#include "emerror.h"
#include "emtaskqueue.h"
#include "call/emcallintermediate.h"

#include "emcallmanager_interface.h"
#include "emcallmanager_listener.h"
#include "call/emcallconference_listener.h"

namespace easemob {

class EMCallRtcListener;
class EMCallSession;
    
class EMCallManager : public EMManagerBase, public EMCallManagerInterface, public protocol::CallEventHandler, public protocol::SyncTrackHandler
{
public:
    
    EMCallManager(const std::shared_ptr<EMSessionManager>, const std::shared_ptr<EMConfigManager>);
    virtual ~EMCallManager();
    
    // MARK: -- Call Manager Listener --
    virtual void addListener(EMCallManagerListener *);
    virtual void removeListener(EMCallManagerListener *);
    virtual void clearListeners();
    
    // MARK: -- Rtc Manager --
    virtual void setRtcProxy(EMCallRtcProxyInterface *);
    
    // MARK: -- Super Methods --
    virtual void onDestroy();
    
    // MARK: -- CallEventHandler --
    virtual void handleConference(const protocol::Conference& conference);
    
    // MARK: -- SyncTrackHandler --
    virtual void handleSync(const protocol::SyncDL&);

    // MARK: -- Call Configs --
    virtual EMCallConfigsPtr getCallConfigs();
    virtual void setCallConfigs(EMCallConfigsPtr configs);
    
    // MARK: -- Operation For 1v1 --
    virtual EMCallSessionPtr asyncMakeCall(const std::string& remoteName, emcall::Type type, const std::string& ext, EMError& error,
                                           bool isRecord = false, bool isMergeStream = false);
    virtual void asyncAnswerCall(const std::string& callId, EMError& error);
    virtual void asyncEndCall(const std::string& callId, emcall::EndReason reason);
    virtual void updateCall(const std::string& callId, emcall::StreamControlType controlType, EMError& error);
    virtual void forceEndAllCall();
    
    // MARK: -- Broadcast For 1v1 --
    void broadcastCallRemoteInitiateWithId(const std::string callId);
    void broadcastCallConnectedWithId(const std::string callId);
    void broadcastCallAnsweredWithId(const std::string callId);
    void broadcastCallAnswered(EMCallSessionPtr callPtr);
    void broadcastCallEndWithId(const std::string callId, emcall::EndReason endReason, const EMErrorPtr error, bool isCancleNotify);
    void broadcastCallEnd(EMCallSessionPtr callPtr, emcall::EndReason endReason, const EMErrorPtr error, bool isCancleNotify);
    void broadcastCallNetworkChangedWithId(const std::string callId, emcall::NetworkStatus toStatus);
    
    // MARK: -- Operation For Conference --
    virtual void setCallConferenceListener(EMCallConferenceListener *);
    virtual void removeCallConferenceListener();
    virtual std::string confCreatorGetTicketFromServer(std::string& confId, std::string& password, EMError& error);
    virtual std::string confMemberGetTicketFromServer(const std::string& confId, const std::string& password, EMError& error);
    virtual void inviteUserToJoinConference(const std::string& confId, const std::string& remoteName, const std::string& pswd, const std::string& ext, EMError& error);
    void onRecvInvite(EMCallIntermediatePtr intermediate);
    
    virtual std::string requestMediaFromServer(EMMediaRestOp op, const std::string& body, EMError &error);
    virtual std::string requestMediaFromServer(EMMediaRestOp op, const std::string& urlParams, const std::string& body, EMError &error);
    virtual std::string requestWBFromServer(easemob::EMWBRestOp op, const std::string &token, const std::string& roomId, const std::string& parameters, easemob::EMError &error);
    // MARK: -- Broadcast To Listener --
    void broadcastFeatureUnsupported(EMCallSessionPtr callSession, const EMErrorPtr error);
    void broadcastCallStateChangedWithId(const std::string callId, emcall::StreamControlType toType);
    void broadcastSendPushMessage(const std::string& from, const std::string& to, int callType);
    
    // MARK: -- Send Meta --
    int sendProbePing(EMCallIntermediatePtr intermediate);
    bool sendReqInitiate(EMCallIntermediatePtr intermediate);
    bool sendCallInfoMeta(EMCallIntermediatePtr intermediate);
    bool sendTerminate(EMCallIntermediatePtr intermediate);
    int sendUpdateControlTypeMeta(EMCallIntermediatePtr intermediate, emcall::StreamControlType controlType);
    
    // MARK: -- Helper --
    void addTsxAndCallId(const std::string& tsxId, const std::string& callId);
    std::string getTsxValue(const std::string& tsxId, bool isErase=false);
    std::vector<std::string> getTsxIdsForValue(const std::string& value, bool isErase=false);
    void removeTsxId(const std::string& tsxId);
    
    EMCallIntermediatePtr getNotifyResult(const std::string& notifyId);
    void addNotifyResult(const std::string& notifyId, EMCallIntermediatePtr resultPtr);
    void removeNotifyResult(const std::string& notifyId);
    
private:
    emcall::EndReason result2EndReason(int r);
    int result2ErrorCode(int r);

    std::unique_ptr<protocol::ChatClient> &mClient;
    std::shared_ptr<EMSessionManager> mSessionManager;
    std::shared_ptr<EMConfigManager> mConfigManager;
    
    EMTaskQueuePtr mRecvQueue;
    EMTaskQueuePtr mDoingQueue;
    EMSemaphoreTrackerPtr mTracker;
    EMTaskQueueThreadPtr mBroadcastThread;
    
    std::recursive_mutex mListenerMutex;
    std::set<EMCallManagerListener*> mListeners;
    
    std::recursive_mutex mRtcProxyMutex;
    EMCallRtcProxyInterface* mRtcProxy;
    
    std::recursive_mutex mConfigsMutex;
    EMCallConfigsPtr mConfigs;
    
    std::recursive_mutex mCurrentCallMutex;
    EMCallSessionPtr mCurrent1v1Call;
    
    std::recursive_mutex mTsxIdMutex;
    std::map<std::string, std::string> mTsxIdMap;
    
    std::recursive_mutex mNotityMutex;
    std::map<std::string, EMCallIntermediatePtr> mNotityResults;
    
    std::recursive_mutex mCacheCandidateMutex;
    std::vector<EMCallIntermediatePtr> mCacheCandidates;
    
    std::recursive_mutex mConfListenerMutex;
    EMCallConferenceListener *mConfListener;
    std::string getConfTicketFromServer(std::string& confId, std::string& password, bool isCreator, EMError& error);
    
    EMCallIntermediatePtr getConfigFromServer(EMCallIntermediatePtr intermediate, EMError& error);
    
    void broadcastCallRemoteInitiate(EMCallSessionPtr callPtr);
    
    void onRecvCallRemotePing(EMCallIntermediatePtr intermediate);
    void onRecvCallRemoteInitiate(EMCallIntermediatePtr intermediate);
    
    //helper for 1v1
    void cancelWaitNotify(const std::string& callId);
    
    EMCallSessionPtr create1v1CallSession(const std::string& callId, const std::string& localName, const std::string& remoteName, bool isCaller, emcall::Type type, EMError& error);
    EMCallSessionPtr getCurrent1v1Call(const std::string& callId="", bool isReset=false);
    void resetCurrent1v1Call(const std::string& callId);
    
    // MARK: -- Private Send Meta --
    int sendProbePong(EMCallIntermediatePtr intermediate);

};

}

#endif
