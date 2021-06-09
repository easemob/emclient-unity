#ifndef _CALLBACK_H_
#define _CALLBACK_H_

//callback entries definition
#if defined(_WIN32)
    typedef void(__stdcall *FUNC_OnSuccess)();
    typedef void(__stdcall *FUNC_OnError)(int, const char *);
    typedef void(__stdcall *FUNC_OnProgress)(int);
#else
    typedef void(*FUNC_OnSuccess)();
    typedef void(*FUNC_OnError)(int, const char *);
    typedef void(*FUNC_OnProgress)(int);
#endif

typedef struct _Callback
{
    FUNC_OnSuccess OnSuccess;
    FUNC_OnError OnError;
    FUNC_OnProgress OnProgess;
}Callback;


#endif // _CALLBACK_H_
