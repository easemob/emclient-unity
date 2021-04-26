//
//  emdeviceinfo.h
//  easemob
//
//  Created by XieYajie on 20/06/2017.
//
//

#ifndef emdeviceinfo_h
#define emdeviceinfo_h

#include <stdio.h>
#include <string>

namespace easemob
{
    
class EMDeviceInfo
{
public:
    EMDeviceInfo() { }
    ~EMDeviceInfo() { }
    
    std::string mResource;
    std::string mDeviceUUID;
    std::string mDeviceName;
};
    
typedef std::shared_ptr<EMDeviceInfo> EMDeviceInfoPtr;
    
}

#endif /* emdeviceresource_h */
