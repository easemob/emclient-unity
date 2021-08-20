//
//  emmultidevicescallbackinterface.h
//  ioseasemob
//
//  Created by XieYajie on 11/04/2017.
//
//

#ifndef emmultidevicescallbackinterface_h
#define emmultidevicescallbackinterface_h

#include <stdio.h>

#include "emmultidevices_listener.h"

namespace easemob
{
    
class EMMultiDevicesCallbackInterface
{
public:
    
    virtual ~EMMultiDevicesCallbackInterface(){}
    
    virtual void onContactMultiDevicesCallback(EMMultiDevicesListener::MultiDevicesOperation operation, const std::string& username, const std::string& ext) { };
    
    virtual void onGroupMultiDevicesCallback(EMMultiDevicesListener::MultiDevicesOperation operation, const std::string& groupId, const std::vector<std::string>& usernames) { };
    
};
    
}

#endif /* emmultidevicescallbackinterface_h */
