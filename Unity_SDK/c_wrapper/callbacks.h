#ifndef _CALLBACKS_H_
#define _CALLBACKS_H_

#include "models.h"
#include "LogHelper.h"

#include "emconnection_listener.h"
#include "emerror.h"
#include "emmessagebody.h"

using namespace easemob;

//callback entries definition
#if defined(_WIN32)
    //Callback
    typedef void(__stdcall *FUNC_OnSuccess)();
    typedef void(__stdcall *FUNC_OnSuccess_With_Result)(void * data[], DataType type, int size);
    typedef void(__stdcall *FUNC_OnError)(int, const char *);
    typedef void(__stdcall *FUNC_OnProgress)(int);
    //Connection Listeners
    typedef void(__stdcall *FUNC_OnConnected)();
    typedef void(__stdcall *FUNC_OnDisconnected)(int);
    //ChatManager Listeners
    typedef void (__stdcall *FUNC_OnMessagesReceived)(void * messages[],
                                                      EMMessageBody::EMMessageBodyType types[], int size);
    typedef void (__stdcall *FUNC_OnCmdMessagesReceived)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnMessagesRead)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnMessagesDelivered)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnMessagesRecalled)(void * messages[], int size);
    typedef void (__stdcall *FUNC_OnReadAckForGroupMessageUpdated)();
    typedef void (__stdcall *FUNC_OnGroupMessageRead)(GroupReadAck acks[], int size);
    typedef void (__stdcall *FUNC_OnConversationsUpdate)();
    typedef void (__stdcall *FUNC_OnConversationRead)(const char * from, const char * to);
#else
    //Callback
    typedef void(*FUNC_OnSuccess)();
    typedef void(*FUNC_OnSuccess_With_Result)(void * data[], DataType type, int size);
    typedef void(*FUNC_OnError)(int, const char *);
    typedef void(*FUNC_OnProgress)(int);
    //Connection Listeners
    typedef void(*FUNC_OnConnected)();
    typedef void(*FUNC_OnDisconnected)(int);
    typedef void(*FUNC_OnPong)();
    //ChatManager Listeners
    typedef void (*FUNC_OnMessagesReceived)(void * messages[],
                                            EMMessageBody::EMMessageBodyType types[],int size);
    typedef void (*FUNC_OnCmdMessagesReceived)(void * messages[], int size);
    typedef void (*FUNC_OnMessagesRead)(void * messages[], int size);
    typedef void (*FUNC_OnMessagesDelivered)(void * messages[], int size);
    typedef void (*FUNC_OnMessagesRecalled)(void * messages[], int size);
    typedef void (*FUNC_OnReadAckForGroupMessageUpdated)();
    typedef void (*FUNC_OnGroupMessageRead)(GroupReadAck acks[], int size);
    typedef void (*FUNC_OnConversationsUpdate)();
    typedef void (*FUNC_OnConversationRead)(const char * from, const char * to);
#endif

class ConnectionListener : public EMConnectionListener
{
public:
    ConnectionListener(FUNC_OnConnected connected, FUNC_OnDisconnected disconnected, FUNC_OnPong pong) : onConnected(connected), onDisconnected(disconnected), onPonged(pong){}
    void onConnect() override {
        LOG("Connection established.");
        if(onConnected)
            onConnected();
    }
    void onDisconnect(EMErrorPtr error) override {
        LOG("Connection discontinued with code=%d.", error->mErrorCode);
        if(onDisconnected)
            onDisconnected(error->mErrorCode);
    }
    void onPong() override {
        LOG("Server ponged.");
        if(onPonged)
            onPonged();
    }
private:
    FUNC_OnConnected onConnected;
    FUNC_OnDisconnected onDisconnected;
    FUNC_OnPong onPonged;
};

class ChatManagerListener : public EMChatManagerListener
{
public:
    ChatManagerListener(FUNC_OnMessagesReceived onMessagesReceived, FUNC_OnCmdMessagesReceived onCmdMessagesReceived, FUNC_OnMessagesRead onMessagesRead, FUNC_OnMessagesDelivered onMessagesDelivered, FUNC_OnMessagesRecalled onMessagesRecalled, FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated, FUNC_OnGroupMessageRead onGroupMessageRead, FUNC_OnConversationsUpdate onConversationsUpdate, FUNC_OnConversationRead onConversationRead):onMessagesReceived(onMessagesReceived),onCmdMessagesReceived(onCmdMessagesReceived),onMessagesRead(onMessagesRead),onMessagesDelivered(onMessagesDelivered),onMessagesRecalled(onMessagesRecalled),onReadAckForGroupMessageUpdated(onReadAckForGroupMessageUpdated), onGroupMessageRead(onGroupMessageRead),onConversationsUpdate(onConversationsUpdate),onConversationRead(onConversationRead) {}
    
    void onReceiveMessages(const EMMessageList &messages) override {
        size_t size = messages.size();
        LOG("%d messages received!", size);
        
        void * result[size];
        EMMessageBody::EMMessageBodyType types[size];
        int i = 0;
        for(EMMessagePtr message : messages) {
            MessageTO *mto = MessageTO::FromEMMessage(message);
            result[i] = mto;
            types[i] = mto->BodyType;
            i++;
        }
        if(onMessagesReceived) {
            LOG("Call onMessagesReceived delegate in managed side...");
            onMessagesReceived(result, types, (int)size);
        }
        //release memory manually
        for(void * message : result) {
            delete (MessageTO *)message;
        }
    }
    
    void onReceiveCmdMessages(const EMMessageList &messages) override {
        LOG("%d cmd messages received!", messages.size());
    }
    
    void onReceiveHasReadAcks(const EMMessageList &messages) override{
        LOG("%d messages read!", messages.size());
    }
    
    void onReceiveHasDeliveredAcks(const EMMessageList &messages) override{
        LOG("%d messages delivered!", messages.size());
    }
    
    void onReceiveRecallMessages(const EMMessageList &messages) override {
        LOG("%d messages recalled!", messages.size());
    }
    
    void onUpdateGroupAcks() override {
        
    }
    
    void onUpdateConversationList(const EMConversationList &conversations) override {
        
    }
    
    void onReceiveReadAckForConversation(const std::string& fromUsername, const std::string& toUsername) override {
        
    }
private:
    FUNC_OnMessagesReceived onMessagesReceived;
    FUNC_OnCmdMessagesReceived onCmdMessagesReceived;
    FUNC_OnMessagesRead onMessagesRead;
    FUNC_OnMessagesDelivered onMessagesDelivered;
    FUNC_OnMessagesRecalled onMessagesRecalled;
    FUNC_OnReadAckForGroupMessageUpdated onReadAckForGroupMessageUpdated;
    FUNC_OnGroupMessageRead onGroupMessageRead;
    FUNC_OnConversationsUpdate onConversationsUpdate;
    FUNC_OnConversationRead onConversationRead;
};

#endif // _CALLBACKS_H_
