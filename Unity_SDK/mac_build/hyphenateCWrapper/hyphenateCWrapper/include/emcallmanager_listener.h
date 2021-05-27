//
//  emcallmanager_listener.h
//  easemob
//
//  Created by dhc on 15/8/12.
//
//

#ifndef easemob_emcallmanager_listener_h
#define easemob_emcallmanager_listener_h

#include "emerror.h"
#include "emdefines.h"
#include "emcallenum.h"
#include "emcallstream.h"
#include "emcallsession.h"

namespace easemob{
    
class EASEMOB_API EMCallManagerListener
{
public:
    
    /**
     * \brief Constructor.
     *
     * @param  NA
     * @return NA
     */
    EMCallManagerListener() {}
    
    /**
     * \brief Destructor.
     *
     * @param  NA
     * @return NA
     */
    virtual ~EMCallManagerListener() {}
    
    virtual void onRecvCallFeatureUnsupported(EMCallSessionPtr callSession, const EMErrorPtr error) = 0;
    
    virtual void onRecvCallIncoming(EMCallSessionPtr callSession) = 0;
    virtual void onRecvCallConnected(EMCallSessionPtr callSession) = 0;
    virtual void onRecvCallAccepted(EMCallSessionPtr callSession) = 0;
    virtual void onRecvCallEnded(EMCallSessionPtr callSession, emcall::EndReason endReason, const EMErrorPtr error) = 0;
    virtual void onRecvCallNetworkStatusChanged(EMCallSessionPtr callSession, emcall::NetworkStatus toStatus) = 0;
    virtual void onRecvCallStateChanged(EMCallSessionPtr callSession, emcall::StreamControlType type) = 0;
    
};
}


#endif
