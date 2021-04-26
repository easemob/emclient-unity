//
//  emcallrtc_listener.hpp
//  ioseasemob
//
//  Created by XieYajie on 4/11/16.
//
//

#ifndef emcallrtc_listener_hpp
#define emcallrtc_listener_hpp

#include <stdio.h>
#include <string>
#include <memory>

namespace easemob {
    
class EMError;
class EMCallSessionStatistics;
    
class EMCallRtcListener {
public:
    virtual ~EMCallRtcListener() { }
    
    virtual void onReceiveLocalSdp(const std::string& localSdp) = 0;
    virtual void onReceiveLocalCandidate(const std::string& localCandidate) = 0;
    virtual void onReceiveLocalCandidateCompleted() = 0;
    virtual void onReceiveSetup(const std::string& reason) = 0;
    virtual void onReceiveClose(const std::string& reason) = 0;
    virtual void onReceiveError(const std::shared_ptr<EMError> error) = 0;
    
    //配置
    virtual void onReceiveCallStatistics(const std::shared_ptr<EMCallSessionStatistics> statistics) = 0;
    
    //网络
    virtual void onReceiveNetworkConnected() = 0;
    virtual void onReceiveNetworkDisconnected() = 0;
};
typedef std::shared_ptr<EMCallRtcListener> EMCallRtcListenerPtr;
}

#endif /* emcallrtc_listener_hpp */
