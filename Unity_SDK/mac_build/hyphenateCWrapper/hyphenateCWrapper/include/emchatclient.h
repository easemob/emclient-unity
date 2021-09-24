/************************************************************
 *  * EaseMob CONFIDENTIAL
 * __________________
 * Copyright (C) 2015 EaseMob Technologies. All rights reserved.
 * 
 * NOTICE: All information contained herein is, and remains
 * the property of EaseMob Technologies.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from EaseMob Technologies.
 */
#ifndef EMCORE_CHATCLIENT_H
#define EMCORE_CHATCLIENT_H

#include <string>
#include <map>
#include "emerror.h"
#include "emchatconfigs.h"
#include "emcallback.h"
#include "emnetworklistener.h"
#include "emconnection_listener.h"
#include "emattributevalue.h"
#include "emlogcallbackinterface.h"
#include "emmultidevices_listener.h"
#include "emdeviceinfo.h"

namespace easemob {

// forward declarations
class EMChatManagerInterface;
class EMGroupManagerInterface;
class EMContactManagerInterface;
class EMChatClientImpl;
class EMChatroomManagerInterface;
class EMPushManagerInterface;
    
class EMCallManagerInterface;
//class EMConferenceManagerInterface;
    
class EMLoginInfo;
class EMDatabase;
class EMConfigManager;
class EMLogHandlerInterface;
class EMUserInfoManagerInterface;
    
/**
 * Enjoy your IM journey! 
 *
 * \section intro_sec Introduction
 * Easemob Linux SDK is an SDK for you to create IM related applications on linux platform.
 *
 * \section integration Integration guide
 * For your fast integration, here is an short guide.
 * 
 * \subsection step1 Step 1: Get chat client
 * Include emchatclient.h and call EMChatClient::create() to get your client, which is the start point of your IM.
 *
 * \subsection step2 Step 2: Register and login
 * Call register() to register a new account. Then you can call login() or logout() to login or logout.
 *
 * \subsection step3 Step 3: Manage your contacts
 * Call getContactManager() to get contact manager to add/remove your contact.
 *
 * \subsection step4 step 4: Let's chat
 * Now you can call getChatManager() to chat with your friends.
 *
 * That's it!
 */
class EASEMOB_API EMChatClient : public EMNetworkListener {
public:
    virtual ~EMChatClient();

    /**
      * \brief Get the chat client with configs.
      *
      * Note: Caller should delete the client when it is not used any more.
      * @param  chat configurations.
      * @return EMChatClient instance.
      */
    static EMChatClient* create(const EMChatConfigsPtr configs);

    /**
      * \brief Login with user name and password.
      *
      * Note: Blocking and time consuming operation.
      * @param:  user name and password
      * @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
      */
    EMErrorPtr login(const std::string &username, const std::string &password);

    /**
      * \brief Login with user name and token.
      *
      * Note: Blocking and time consuming operation.
      * @param:  user name and token
      * @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
      */
    EMErrorPtr loginWithToken(const std::string &username, const std::string &token);

    /**
     * \brief auto Login with user name and password, sdk will retry automatically if login fail.
     *
     * Note: Blocking and time consuming operation.
     * @param:  user name and password or token
     * @param:  whether login with token
     * @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
     */
    EMErrorPtr autoLogin(const std::string &username, const std::string &code, bool loginWithToken);
    
    /**
     * \brief report user location/network info.
     */
    void setPresence( const std::string &location);

    /**
      * \brief Logout current user.
      *
      * @param  NA
      * @return NA.
      */
    void logout();
    
    /**
      * \brief Get info of current logoin user.
      *
      * @param  NA
      * @return Login info.
      */
    const EMLoginInfo& getLoginInfo();

    /**
     * \brief change appkey only when user not logged in.
     *
     * @param  NA
     * @return change appkey result.
     */
    EMErrorPtr changeAppkey(const std::string &appkey);

    /**
      * \brief register connection listener.
      *
      * @param  EMConnectionListenerPtr
      * @return NA.
      */
    void addConnectionListener(EMConnectionListener*);
    
    /**
      * \brief remove connection listener.
      *
      * @param  EMConnectionListenerPtr
      * @return NA.
      */
    void removeConnectionListener(EMConnectionListener*);
    
    void addMultiDevicesListener(EMMultiDevicesListener*);
    void removeMultiDevicesListener(EMMultiDevicesListener*);
    void clearAllMultiDevicesListeners();
    
    void addLogCallback(EMLogCallbackInterface* aLogCallback);
    void removeLogCallback(EMLogCallbackInterface* aLogCallback);
    
    
    /**
      * \brief Register a new account with user name and password.
      *
      * Note: Blocking and time consuming operation.
      * @param  user name and password
      * @return register result, EMError::EM_NO_ERROR means success, others means fail.
      */
    EMErrorPtr createAccount(const std::string &username, const std::string &password);
    
    /**
      * \brief Update the chat configs.
      *
      * Note: NA.
      * @param  EMChatConfigPtr
      * @return NA.
      */
    EMChatConfigsPtr getChatConfigs();
    
    /**
      * \brief Get chat manager to handle the message operation.
      *
      * @param  NA
      * @return chat manager instance.
      */
    EMChatManagerInterface&    getChatManager();

    /**
      * \brief Get contact manager to manage the contacts.
      *
      * @param  NA
      * @return contact manager instance.
      */
    EMContactManagerInterface& getContactManager();

    /**
      * \brief Get group manager to manage the group.
      *
      * @param  NA
      * @return group manager instance.
      */
    EMGroupManagerInterface&   getGroupManager();
    
    /**
      * \brief Get chatroom manager to manage the chatroom.
      *
      * @param  NA
      * @return Chatroom manager instance.
      */
    EMChatroomManagerInterface&   getChatroomManager();

    /**
      * \brief Get call manager to handle the voice/video call.
      *
      * Note: not release yet, coming soon
      * @param  NA
      * @return call manager instance.
      */
    EMPushManagerInterface& getPushManager();

    // Add by zhangsong for service check.
    EMErrorPtr check(std::string username, std::string password, EMChatConfigs::CheckType type);
    
#if ENABLE_CALL == 1
    EMCallManagerInterface&    getCallManager();
#endif
    
    std::shared_ptr<EMDatabase> getDatabase();
    
    std::shared_ptr<EMConfigManager> getConfigManager();
    
    EMUserInfoManagerInterface& getUserInfoManager();


    /**
      * \brief call this method to notify SDK the network change.
      *
      * @param  EMNetworkType
      * @param  bool
      * @return NA.
      */
    
    virtual void onNetworkChanged(EMNetworkListener::EMNetworkType to, bool forceReconnect = false);

    bool isConnected();
    bool isLoggedIn();
    bool isLogout();
    void reconnect();
    void disconnect();
    void forceDisconnect();
    void sendPing();
    bool sendPing(bool waitPong, long timeout);
    EMErrorPtr getUserToken(std::string& token, bool fetchFromServer);
    EMErrorPtr getUserTokenFromServer(const std::string& username, const std::string& password, std::string& token);
    
    
    /**
     * \brief Compress the log file into a .gz file, return to the gz file path
     */
    std::string getLogFilesPath(EMError &error);
    
    /**
     * \brief Get all the information about the logged in device
     */
    std::vector<EMDeviceInfoPtr> getLoggedInDevicesFromServer(const std::string&username, const std::string& password, EMError &error);
    
    /**
     * \brief Forced to logout the specified logged in device
     *
     * @param  resource of the specified device
     * @param  error
     */
    void kickDevice(const std::string&username, const std::string& password, const std::string& resource, EMError &error);
    
    /**
     * \brief Forced to logout all logged in device
     *
     * @param  error
     */
    void kickAllDevices(const std::string&username, const std::string& password, EMError &error);
    
    void clearResource();
    void allocResource();
private:
    EMChatClient();
    void init(const EMChatConfigsPtr configs);
    EMChatClient(const EMChatClient&);
    EMChatClient& operator=(const EMChatClient&);
    
    EMChatClientImpl *mImpl;
};

}

#endif // EMCORE_CHATCLIENT_H
