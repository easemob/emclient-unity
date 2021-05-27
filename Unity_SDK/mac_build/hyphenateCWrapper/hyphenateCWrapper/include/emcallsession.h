//
//  emcallsession.hpp
//  ioseasemob
//
//  Created by XieYajie on 4/8/16.
//
//

#ifndef emcallsession_hpp
#define emcallsession_hpp

#include <stdio.h>
#include <string>
#include <mutex>
#include <vector>
#include <memory>

#include "emerror.h"
#include "emcallenum.h"
#include "call/emcallsessionstatistics.h"

namespace easemob {
    
class EMSessionManager;
class EMConfigManager;
class EMCallManager;
    
class EMCallSessionPrivate;
class EMCallRtcProxyInterface;
    
class EMCallSession
{
public:
    
    virtual ~EMCallSession();
    
    const std::string getCallId();
    
    const std::string getServerRecordId();
    
    bool willRecordStream();
    
    bool willMergeStream();
    
    const std::string getLocalName();
    
    emcall::Type getType();
    
    const std::string getRemoteName();
    
    bool getIsCaller();
    
    emcall::Status getStatus();
    
    EMCallSessionStatisticsPtr getStatistics();
    
    const std::string getExt();
    
    void update(emcall::StreamControlType controlType, EMError& error);
    
    void switchCamera();
    
    void enableQualityChecker(bool enable);
    
protected:
    EMCallSession(const std::string& callId, const std::string& localName, const std::string& remoteName="", bool isCaller=true, emcall::Type type=emcall::VOICE, EMCallRtcProxyInterface *rtcProxy=nullptr);
    
private:
    std::string mCallId;
    std::string mSessionId;
    std::string mLocalName;
    std::string mRemoteName;
    bool mIsCaller;
    emcall::Type mType;
    void *mLocalVideoView;
    void *mRemoteVideoView;
    
    friend class EMCallManager;
    std::shared_ptr<EMCallSessionPrivate> mPrivate;
};
    
typedef std::shared_ptr<EMCallSession> EMCallSessionPtr;
    
}

#endif /* emcallsession_hpp */
