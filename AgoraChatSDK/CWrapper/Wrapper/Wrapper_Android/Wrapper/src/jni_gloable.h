#include <jni.h>
#include <android/log.h>
#include <string>

using namespace std;

#define TAG "CWRAPPER"
#define LOGW(...) __android_log_print(ANDROID_LOG_WARN,TAG ,__VA_ARGS__)
#define LOGE(...) __android_log_print(ANDROID_LOG_ERROR,TAG ,__VA_ARGS__)

namespace wrapper_jni {
    jobject javaWrapper();
    jclass javaWrapperClass();

    void init_common(int sdkType, void* listener);
    void uninit_common();
    int get_Common(const char* manager, const char* method, const char* jstr, char* buf, const char* cbid);
    void call_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
}