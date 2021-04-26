//
//  emcallenum.h
//  ioseasemob
//
//  Created by XieYajie on 4/8/16.
//
//

#ifndef emcallenum_h
#define emcallenum_h

namespace easemob {
    
namespace emcall
{
    typedef enum
    {
        VOICE = 0,  //voice call
        VIDEO,      //video call
    } Type;
    
    typedef enum
    {
        NONE = 0,   //Initial value
        DIRECT,     //direct
        RELAY,      //relay
    } ConnectType;
    
    typedef enum
    {
        DISCONNECTED    = 0,       //Disconnected, initial value
        RINGING,                   //Ringing
        CONNECTING,                //Connecting
        CONNECTED,                 //Connected
        ACCEPTED,                  //Accepted
    } Status;
    
    typedef enum
    {
        HANGUP  = 0,    //hang up
        NORESPONSE,     //no response
        REJECT,         //reject
        BUSY,           //busy
        FAIL,           //fail
        UNSUPPORTED,
        OFFLINE,        //remote offline
        RTC_SERVICE_NOT_ENABLE = 101,
        RTC_ARREARAGES,
        RTC_FORBIDDEN,
        
    } EndReason;
    
    typedef enum {
        PAUSE_VOICE,
        RESUME_VOICE,
        PAUSE_VIDEO,
        RESUME_VIDEO,
    } StreamControlType;
    
    typedef enum
    {
        MODE_1v1,     //1v1
        MODE_CONFERENCE,     //多人会议
    } Mode;
    
    typedef enum
    {
        NETWORK_CONNECTED = 0,
        NETWORK_UNSTABLE,
        NETWORK_DISCONNECTED,
    } NetworkStatus;
};
    
}

#endif /* emcallenum_h */
