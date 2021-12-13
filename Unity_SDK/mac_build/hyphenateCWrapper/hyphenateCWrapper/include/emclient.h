//
//  emclient.h
//  easemob
//
//  Created by zhaoliang on 11/16/15.
//
//

#ifndef __easemob__emclient__
#define __easemob__emclient__

#include <stdio.h>

#include <string>
#include <vector>
#include "emerror.h"
#include "emchatconfigs.h"
#include "emcallback.h"
#include "emnetworklistener.h"
#include "emconnection_listener.h"
#include "emmultidevicescallbackinterface.h"
#include "emdeviceinfo.h"
#include "empushmanager_interface.h"
#include "emuserinfomanager_interface.h"

namespace easemob {
    
    // forward declarations
    class EMChatManagerInterface;
    class EMGroupManagerInterface;
    class EMContactManagerInterface;
    class EMCallManagerInterface;
    class EMChatClient;
    class EMChatroomManagerInterface;
    class EMLoginInfo;
    class EMTimer;
    
    /**
     * Enjoy your IM journey!
     *
     * \section intro_sec Introduction
     * Easemob Linux SDK is an SDK for you to create IM related applications on linux platform.
     *
     * \section integration Integration guide
     * Here's a guide to speed up the integration.
     *
     * \subsection step1 Step 1: Get client
     * Include emchatclient.h and call EMClient::create() to get the client instance, which is the starting point of IM operation.
     *
     * \subsection step2 Step 2: Register and login
     * Call register() to register a new account and call login()/logout() to login/logout.
     *
     * \subsection step3 Step 3: Manage your contacts (optional)
     * Call getContactManager() to get contact manager to add/remove contact for logged in user.
     *
     * \subsection step4 step 4: Let's chat
     * Now you can call getChatManager() to chat with contacts.
     *
     * That's it!
     */
    class EASEMOB_API EMClient : public EMNetworkListener, public EMConnectionListener {
    public:
        virtual ~EMClient();
        
        /**
         * \brief Get the chat client with configs.
         *
         * Note: Caller should delete the client when it is not used any more.
         * @param  chat configurations.
         * @return EMChatClient instance.
         */
        static EMClient* create(const EMChatConfigsPtr configs);
        
        /**
         * \brief Login with username and password.
         *
         * Note: Blocking and time consuming operation.
         * @param:  user name and password
         * @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
         */
        EMErrorPtr login(const std::string &username, const std::string &password);

        /**
         * \brief Login with username and token.
         *
         * Note: Blocking and time consuming operation.
         * @param:  user name and token
         * @return login result, EMError::EM_NO_ERROR means success, others means fail. @see EMError
         */
        EMErrorPtr loginWithToken(const std::string &username, const std::string &token);
        
        /**
         * \brief Logout current user.
         *
         * @param  NA
         * @return NA.
         */
        void logout();
        
        /**
         * \brief Get info of current login user.
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
        void addConnectionListener(EMConnectionListener *);
        
        /**
         * \brief remove connection listener.
         *
         * @param  EMConnectionListenerPtr
         * @return NA.
         */
        void removeConnectionListener(EMConnectionListener *);
        
        /**
         * \brief Register a new account with user name and password.
         *
         * Note: Blocking and time consuming operation.
         * @param  user name and password
         * @return register result, EMError::EM_NO_ERROR means success, others means fail.
         */
        EMErrorPtr createAccount(const std::string &username, const std::string &password);
        
        /**
         * \brief get the chat configs.
         *
         * @param NA
         * @return EMChatConfigPtr.
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
         * \brief Get push manager to manage the push config.
         *
         * @param  NA
         * @return Push manager instance.
         */
        EMPushManagerInterface& getPushManager();
        
        EMUserInfoManagerInterface& getUserInfoManager();
        /**
         * \brief Get call manager to handle the voice/video call.
         *
         * Note: not release yet, coming soon
         * @param  NA
         * @return call manager instance.
         */
#if ENABLE_CALL
        EMCallManagerInterface&    getCallManager();
#endif

        /**
         * \brief register multi devices listener.
         *
         * @param  EMMultiDevicesListenerPtr
         * @return NA.
         */
        void addMultiDevicesListener(EMMultiDevicesListener* listener);

        /**
         * \brief remove register multi devices listener.
         *
         * @param  EMMultiDevicesListenerPtr
         * @return NA.
         */
        void removeMultiDevicesListener(EMMultiDevicesListener* listener);

        /**
         * \brief clear all register multi devices listener.
         *
         * @param  NA.
         * @return NA.
         */
        void clearAllMultiDevicesListeners();

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

        /**
         * \brief call this method to notify SDK the network change.
         *
         * @param  EMNetworkType
         * @return NA.
         */
        virtual void onNetworkChanged(EMNetworkListener::EMNetworkType to, bool forceReconnect = false);
        
        void reconnect();
        void disconnect();
        
        // method from EMConnectionListener
        void onConnect();
        void onDisconnect(EMErrorPtr error);
        void onPong();
        
        void clearResource();
        void allocResource();
        
    private:
        EMClient();
        void init(const EMChatConfigsPtr configs);
        EMClient(const EMClient&);
        EMClient& operator=(const EMClient&);
        void startHeartBeat();
        void stopHeartBeat();

        EMChatClient *mImpl;
        
        EMCallbackObserverHandle mHandle;
        EMTimer *mTimer;
    };
    
}

#endif /* defined(__easemob__emclient__) */
