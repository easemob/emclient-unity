#include "core_dump.h"

#ifdef _COREDUMP_

#ifdef _WIN32

#pragma comment(lib, "dbghelp.lib")  

bool CreateDumpFile(const TCHAR* lpstrDumpFilePathName, EXCEPTION_POINTERS* pException)
{
    HANDLE hDumpFile = CreateFile(lpstrDumpFilePathName, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
    if (hDumpFile == INVALID_HANDLE_VALUE) 
    {
        return false;
    }

    MINIDUMP_EXCEPTION_INFORMATION dumpInfo;
    dumpInfo.ExceptionPointers = pException;
    dumpInfo.ThreadId = GetCurrentThreadId();
    dumpInfo.ClientPointers = TRUE;

    MiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(), hDumpFile, MiniDumpNormal, &dumpInfo, NULL, NULL);
    CloseHandle(hDumpFile);
    return true;
}

LONG ApplicationCrashHandler(EXCEPTION_POINTERS* pException)
{
    if (CreateDumpFile(TEXT("sdk.dmp"), pException)) 
    {
        FatalAppExit(-1, TEXT("*** Unhandled Exception in SDK! dmp created ***"));
    }
    else
    {
        FatalAppExit(-1, TEXT("*** Unhandled Exception in SDK! dmp create fail ***"));
    }

    return EXCEPTION_EXECUTE_HANDLER;
}

bool DisableSetUnhandledExceptionFilter()
{
    HMODULE hKernel32 = LoadLibraryW(L"kernel32.dll");

    if (hKernel32 == NULL) return FALSE;
    void* addr = (void*)::GetProcAddress(hKernel32, "SetUnhandledExceptionFilter");

    if (addr)
    {

#ifdef _M_IX86

        // Code for x86:
        // 33 C0                xor         eax,eax  
        // C2 04 00             ret         4 
        unsigned char szExecute[] = { 0x33, 0xC0, 0xC2, 0x04, 0x00 };
#elif _M_X64
        // 33 C0                xor         eax,eax 
        // C3                   ret  
        unsigned char szExecute[] = { 0x33, 0xC0, 0xC3 };
#else
#error "The following code only works for x86 and x64!"
#endif

        SIZE_T bytesWritten = 0;
        DWORD dwOldFlag, dwTempFlag;

        VirtualProtect(addr, sizeof(szExecute), PAGE_EXECUTE_READWRITE, &dwOldFlag);
        BOOL bRet = WriteProcessMemory(GetCurrentProcess(),addr, szExecute, sizeof(szExecute), &bytesWritten);
        VirtualProtect(addr, sizeof(szExecute), dwOldFlag, &dwTempFlag);
        return bRet;
    }
   
    return false;
}
void SetUnhandledExceptionHandle() {

    SetUnhandledExceptionFilter((LPTOP_LEVEL_EXCEPTION_FILTER)ApplicationCrashHandler);
    DisableSetUnhandledExceptionFilter();
}

#endif // _WIN32

#else //_COREDUMP_
void SetUnhandledExceptionHandle()
{

}
#endif
