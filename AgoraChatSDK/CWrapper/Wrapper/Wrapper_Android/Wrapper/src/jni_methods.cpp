#include <jni.h>
#include "common_wrapper_internal.h"
#include "jni_gloable.h"
#include "jni_native.h"


using namespace wrapper_jni;

#ifdef __cplusplus
extern "C" {
#endif

JNIEXPORT void JNICALL Java_com_hyphenate_javawrapper_channel_EMChannel_callListener(JNIEnv *, jobject, jstring listenerStr, jstring methodStr, jstring jsonStr)
{
    jobject wrapperObj = javaWrapper();
    jclass cls = javaWrapperClass();
    JNIEnv *env = getCurrentThreadEnv();
    jfieldID fidNativeHandler = (*env).GetFieldID(cls, "nativeListener", "J");
    if (NULL == fidNativeHandler) return;
    jlong nativeHandler = (*env).GetLongField(wrapperObj, fidNativeHandler);
    NativeListenerEvent event = (NativeListenerEvent)((void*)nativeHandler);
    event(extractJString(env, listenerStr).c_str(), extractJString(env, methodStr).c_str(), extractJString(env, jsonStr).c_str());
}


#ifdef __cplusplus
}
#endif
