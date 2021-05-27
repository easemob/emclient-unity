//
//  emmultidevices_listener.h
//  ioseasemob
//
//  Created by XieYajie on 07/04/2017.
//
//

#ifndef emmultidevices_listener_h
#define emmultidevices_listener_h

#include <stdio.h>
#include "emdefines.h"
#include <vector>

namespace easemob
{
    
class EASEMOB_API EMMultiDevicesListener
{
public:
    typedef enum {
        UNKNOW                              = -1,
        CONTACT_REMOVE                      = 2, //好友已经在其他机子上被移除
        CONTACT_ACCEPT                      = 3, //好友请求已经在其他机子上被同意
        CONTACT_DECLINE                     = 4, //好友请求已经在其他机子上被拒绝
        CONTACT_BAN                         = 5, //其他设备进行了操作：将好友加入了黑名单
        CONTACT_ALLOW                       = 6, //其他设备进行了操作：将用户从黑名单中移除
        
        GROUP_CREATE                        = 10,
        GROUP_DESTROY                       = 11,
        GROUP_JOIN                          = 12, //已经加入群组
        GROUP_LEAVE                         = 13, //已经离开群组
        GROUP_APPLY                         = 14,
        GROUP_APPLY_ACCEPT	                = 15, //同意群组申请
        GROUP_APPLY_DECLINE	                = 16, //拒绝群组申请
        GROUP_INVITE                        = 17,
        GROUP_INVITE_ACCEPT	                = 18, //同意群组邀请
        GROUP_INVITE_DECLINE                = 19, //拒绝群组邀请
        GROUP_KICK                          = 20,
        GROUP_BAN                           = 21,
        GROUP_ALLOW                         = 22,
        GROUP_BLOCK                         = 23,
        GROUP_UNBLOCK                       = 24,
        GROUP_ASSIGN_OWNER                  = 25,
        GROUP_ADD_ADMIN                     = 26,
        GROUP_REMOVE_ADMIN                  = 27,
        GROUP_ADD_MUTE                      = 28,
        GROUP_REMOVE_MUTE                   = 29,
        GROUP_ADD_USER_WHITE_LIST           = 30,
        GROUP_REMOVE_USER_WHITE_LIST        = 31,
        GROUP_ALL_BAN                       = 32,
        GROUP_REMOVE_ALL_BAN                = 33
        
    } MultiDevicesOperation;
    
    virtual ~EMMultiDevicesListener() {}
    
    virtual void onContactMultiDevicesEvent(MultiDevicesOperation operation, const std::string& target, const std::string& ext) = 0;
    
    virtual void onGroupMultiDevicesEvent(MultiDevicesOperation operation, const std::string& target, const std::vector<std::string>& usernames) = 0;
};
    
}

#endif /* emmultidevices_listener_h */
