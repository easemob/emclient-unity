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
    typedef void(__stdcall *FUNC_OnConnect)();
    typedef void(__stdcall *FUNC_OnDisconnect)(int);
#else
    typedef void(*FUNC_OnSuccess)();
    typedef void(*FUNC_OnError)(int, const char *);
    typedef void(*FUNC_OnProgress)(int);
    typedef void(*FUNC_OnConnect)();
    typedef void(*FUNC_OnDisconnect)(int);
#endif

class Callback
{
public:
    
    void OnSuccess() {
        if(onSuccess)
            onSuccess();
    }
    
    void OnError(int code, const char *description) {
        if(onError)
            onError(code, description);
    }
    
    void OnProgress(int progress) {
        if(onProgress)
            onProgress(progress);
    }

private:
    FUNC_OnSuccess onSuccess;
    FUNC_OnError onError;
    FUNC_OnProgress onProgress;
    char * callbackId;
};

struct ConnListenerFptrs
{
    FUNC_OnConnect Connected;
    FUNC_OnDisconnect Disconnected;
};

class ConnectionListener : public EMConnectionListener
{
public:
    ConnectionListener(ConnListenerFptrs fptr) : _OnConnected(fptr.Connected), _OnDisconnected(fptr.Disconnected){}
    void onConnect() override {
        if(_OnConnected)
            _OnConnected();
    }
    void onDisconnect(EMErrorPtr error) override {
        if(_OnDisconnected)
            _OnDisconnected(error->mErrorCode);
    }
    void onPong() override {
        // do nothing
    }
private:
    FUNC_OnConnect _OnConnected;
    FUNC_OnDisconnect _OnDisconnected;
};

#endif // _CALLBACKS_H_
