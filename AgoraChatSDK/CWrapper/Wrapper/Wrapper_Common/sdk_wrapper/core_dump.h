#ifndef _SDK_WRAPPER_COREDUMP_IMPL_
#define _SDK_WRAPPER_COREDUMP_IMPL_

#ifdef _COREDUMP_

    #ifdef _WIN32
        #include <stdio.h>
        #include <string>
        #include <stdlib.h>

        #include <windows.h>
        #include <DbgHelp.h>  

        LONG ApplicationCrashHandler(EXCEPTION_POINTERS* pException);
        bool DisableSetUnhandledExceptionFilter();
    #endif //_WIN32

#endif //_COREDUMP_

void SetUnhandledExceptionHandle();

#endif
