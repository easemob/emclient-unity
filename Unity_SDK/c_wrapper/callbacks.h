#ifndef _CALLBACKS_H_
#define _CALLBACKS_H_

#include "emconnection_listener.h"
#include "emerror.h"

using namespace easemob;

//callback entries definition
#if defined(_WIN32)
    typedef void(__stdcall *FUNC_OnSuccess)();
    typedef void(__stdcall *FUNC_OnError)(int, const char *);
    typedef void(__stdcall *FUNC_OnProgress)(int);
    typedef void(__stdcall *FUNC_OnConnected)();
    typedef void(__stdcall *FUNC_OnDisconnected)(int);
#else
    typedef void(*FUNC_OnSuccess)();
    typedef void(*FUNC_OnError)(int, const char *);
    typedef void(*FUNC_OnProgress)(int);
    typedef void(*FUNC_OnConnected)();
    typedef void(*FUNC_OnDisconnected)(int);
    typedef void(*FUNC_OnPong)();
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

#endif // _CALLBACKS_H_
