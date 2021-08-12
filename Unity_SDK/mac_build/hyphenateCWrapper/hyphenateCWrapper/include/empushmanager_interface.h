//
//  empushmanager_interface.h
//  ioseasemob
//
//  Created by XieYajie on 30/11/2016.
//
//

#ifndef empushmanager_interface_h
#define empushmanager_interface_h

#include <stdio.h>

#include "emdefines.h"
#include "empushconfigs.h"

namespace easemob
{
    
class EMError;
    
class EASEMOB_API EMPushManagerInterface
{
public:
    virtual ~EMPushManagerInterface() { };
    
    virtual EMPushConfigsPtr getPushConfigs() = 0;
    
    virtual EMPushConfigsPtr getUserConfigsFromServer(EMError& error) = 0;
    
    virtual void updateUserConfigsWithoutIgnoredGroupIds(EMPushConfigsPtr config, EMError& error) = 0;
    
    virtual void bindUserDeviceToken(const std::string& deviceToken, const std::string& certName, EMError& error) = 0;
    
    virtual void unBindUserDeviceToken(const std::string& certName, EMError& error) = 0;
    
    virtual void ignoreGroupPush(const std::string& groupId, bool isIgnore, EMError& error) = 0;
    
    virtual void ignoreGroupsPush(const std::vector<std::string> groupIds, bool isIgnore, EMError& error) = 0;
    
    virtual void updatePushNickName(const std::string& name, EMError& error) = 0;
    
    virtual void updatePushDisplayStyle(EMPushConfigs::EMPushDisplayStyle style, EMError& error) = 0;
    
    virtual void updatePushNoDisturbing(EMPushConfigs::EMPushDisplayStyle style, EMPushConfigs::EMPushNoDisturbStatus status, int startHour, int endHour, EMError& error) = 0;
};
    
}

#endif /* empushmanager_interface_hpp */
