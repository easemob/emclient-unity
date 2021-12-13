//
//  empushmanager.h
//  ioseasemob
//
//  Created by XieYajie on 30/11/2016.
//
//

#ifndef empushmanager_h
#define empushmanager_h

#include <stdio.h>
#include <string>

#include "utils/emhttprequest.h"
#include "empushmanager_interface.h"
#include "empushconfigs.h"

namespace easemob
{
    
class EMConfigManager;
class EMSessionManager;
    
class EMError;
    
class EMPushManager : public EMPushManagerInterface
{
    
public:
    EMPushManager(std::shared_ptr<EMConfigManager> configManager, std::shared_ptr<EMSessionManager> sessionManager);
    virtual ~EMPushManager();
    
    // --- EMManagerBase ---
    virtual void onInit();
    virtual void onDestroy();
    
    // --- EMPushManagerInterface ---
    virtual EMPushConfigsPtr getPushConfigs();
    
    virtual EMPushConfigsPtr getUserConfigsFromServer(EMError& error);
    
    virtual void updateUserConfigsWithoutIgnoredGroupIds(EMPushConfigsPtr config, EMError& error);
    
    void bindUserDeviceToken(const std::string& deviceToken, const std::string& certName, EMError& error);
    
    void unBindUserDeviceToken(const std::string& certName, EMError& error);
    
    void updateDeviceInformation(const std::string& model, const std::string& deviceToken, EMError& error);
    
    virtual void ignoreUsersPush(const std::vector<std::string>& uIds, bool isIgnore, EMError& error);
    virtual std::string ignoreUserPushParamPackage(const std::vector<std::string>& uIds, bool isIgnore);
    
    virtual void ignoreGroupPush(const std::string& groupId, bool isIgnore, EMError& error);
    virtual void ignoreGroupsPush(const std::vector<std::string> groupIds, bool isIgnore, EMError& error);
    
    virtual void updatePushNickName(const std::string& name, EMError& error);
    
    virtual void updatePushDisplayStyle(EMPushConfigs::EMPushDisplayStyle style, EMError& error);
    
    virtual void updatePushNoDisturbing(EMPushConfigs::EMPushDisplayStyle style, EMPushConfigs::EMPushNoDisturbStatus status, int startHour, int endHour, EMError& error);
    
private:
    std::shared_ptr<EMConfigManager> mConfigManager;
    std::shared_ptr<EMSessionManager> mSessionManager;
    
    std::recursive_mutex mConfigsMutex;
    EMPushConfigsPtr mPushConfigs;
    
    void _setPushOptions(EMPushConfigsPtr configs);
    
    EMPushConfigsPtr _updateUserConfigsWithParams(const EMHttpParameters &parameters, EMError& error, const std::string paramsContent = "");
};
    
}

#endif /* empushmanager_h */
