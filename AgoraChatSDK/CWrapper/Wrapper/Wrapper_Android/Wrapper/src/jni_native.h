#include <jni.h>

using namespace std;
typedef unsigned char charbyte;

namespace wrapper_jni {
    JavaVM *getGlobalJavaVM();
    JNIEnv *getCurrentThreadEnv();
    void detachCurrentThreadEnv();
    jstring getJStringObject(JNIEnv *env, const string &str);
    string extractJString(JNIEnv *env, jstring jStr);
    jbyteArray getJByteArray(JNIEnv *env, const charbyte *str, unsigned int len);
}
