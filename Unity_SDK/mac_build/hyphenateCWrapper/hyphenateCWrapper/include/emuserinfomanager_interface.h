//
//  emuserinfomanager_interface_h.h
//  ioseasemob
//
//  Created by lixiaoming on 21/03/24.
//
//

#ifndef emuserinfomanager_interface_h
#define emuserinfomanager_interface_h

#include <stdio.h>
#include <vector>

#include "emdefines.h"

namespace easemob
{
    
class EMError;
    
class EASEMOB_API EMUserInfoManagerInterface
{
public:
    virtual ~EMUserInfoManagerInterface() { };
    
    virtual void updateOwnUserInfo(const std::string& strUsrProperty,std::string& responce,EMError& error) = 0;
    virtual void fetchUsersPropertyByType(const std::vector<std::string>& users,const std::vector<std::string>& properties,std::string& usersProperty,EMError& error) = 0;
};
    
}

#endif
