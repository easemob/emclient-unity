//
//  empushconfigs.h
//  ioseasemob
//
//  Created by XieYajie on 29/11/2016.
//
//

#ifndef empushconfigs_h
#define empushconfigs_h

#include <stdio.h>
#include <string>
#include <map>

//#include "emdefines.h"
#include "emattributevalue.h"

namespace easemob
{
class EMPushConfigs
{
    
public:
    typedef enum
    {
        SimpleBanner = 0,
        MessageSummary,
    }EMPushDisplayStyle;
    
    typedef enum
    {
        Day = 0,
        Custom,
        Close,
    }EMPushNoDisturbStatus;

    
    EMPushConfigs();
    ~EMPushConfigs();
    
    void setDisplayName(const std::string& name);
    const std::string getDisplayName();
    
    void setDisplayStyle(EMPushDisplayStyle style);
    EMPushDisplayStyle getDisplayStyle();
    
    void setDisplayStatus(EMPushNoDisturbStatus status);
    EMPushNoDisturbStatus getDisplayStatus();
    
    void setNoDisturbingStartHour(int start);
    int getNoDisturbingStartHour();
    
    void setNoDisturbingEndHour(int end);
    int getNoDisturbingEndHour();
    
    std::vector<std::string> getIgnoreUIds();
    
    std::vector<std::string> getIgnoredGroupIds();
    
    std::map<std::string, EMAttributeValue> toParametersWithoutIgnoredGroupIds();
    
    static std::shared_ptr<EMPushConfigs> pushConfigsFromString(const std::string& str);
    
private:
    std::string mDeviceToken;
    std::string mDisplayName;
    int mStyle;
    int mStatus;
    int mNoDisturbingStartH;
    int mNoDisturbingEndH;
    std::vector<std::string> mIgnoreUIds;
    std::vector<std::string> mIgnoredGroupIds;
    
    void setIgnoreUIds(std::vector<std::string> uidList);
    void setIgnoredGroupIds(std::vector<std::string> list);
};
    
typedef std::shared_ptr<EMPushConfigs> EMPushConfigsPtr;
}

#endif /* empushoptions_h */
