#include <jni.h>
#include <string>

using namespace std;

namespace wrapper_jni {
    jobject javaWrapper();
    jclass javaWrapperClass();
    string get_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    void call_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    void add_listener(void *listener);
}