#ifndef _LOG_HELPER_H_
#define _LOG_HELPER_H_

#include <iostream>
#include <fstream>
#include <stdarg.h>

class LogHelper {
private:
    FILE* fileStream = nullptr;
    
public:
    LogHelper() {

    }
    
    ~LogHelper() {
        stopLogService();
    }
    
    static LogHelper& getInstance() {
        static LogHelper logHelper;
        return logHelper;
    }
    

public:
    void startLogService(const char *filePath) {
        if (fileStream)
            return;
        
        if (filePath)
            fileStream = fopen(filePath, "ab+");
    }

    void stopLogService() {
        if (fileStream) {
            fflush(fileStream);
            fclose(fileStream);
            fileStream = nullptr;
        }
    }

    void writeLog(const char *format, ...) {
        va_list la;
        va_start(la, format);
        
        if (!fileStream)
            return;
        
        vfprintf(fileStream, format, la);
        va_end(la);
        fprintf(fileStream, "\n");
        fflush(fileStream);
    }
};

#ifdef DEBUG
#define LOG(fmt, ...) LogHelper::getInstance().writeLog(fmt, __VA_ARGS__)
#else
#define LOG(fmt, ...)
#endif //DEBUG LOG

#endif //_LOG_HELPER_H_

