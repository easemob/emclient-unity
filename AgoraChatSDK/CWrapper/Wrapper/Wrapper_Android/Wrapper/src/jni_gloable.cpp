
#include <jni.h>
#include <assert.h>
#include "jni_gloable.h"
#include "jni_native.h"



using namespace std;
using namespace wrapper_jni;

JavaVM *g_vm = NULL;
jclass wrapper_class = NULL;
jobject wrapperJObj = NULL;


namespace wrapper_jni {



    JNIEnv* getCurrentThreadEnv()
    {
        JNIEnv *env = NULL;
        assert(g_vm != NULL);
        g_vm->AttachCurrentThread((JNIEnv**)&env, NULL);
        assert(env != NULL);
        return env;
    }

    void detachCurrentThreadEnv() {
        assert(g_vm != NULL);
        g_vm->DetachCurrentThread();
    }

    jstring getJStringObject(JNIEnv *env, const string &str) {
        jclass clazz = (*env).FindClass("java/lang/String");
        jbyteArray jArray = getJByteArray(env, (byte*)str.c_str(), str.length());
        jstring jstr = (jstring)(*env).NewObject(clazz, (*env).GetMethodID(clazz, "<init>", "([B)V"), jArray);
        (*env).DeleteLocalRef(jArray);
        return jstr;
    }

    jbyteArray getJByteArray(JNIEnv *env, const byte *str, unsigned int len) {
        jbyteArray jArray = env->NewByteArray(len);
        env->SetByteArrayRegion(jArray, 0, len, reinterpret_cast<const jbyte*>(str));
        return jArray;
    }

    string extractJString(JNIEnv *env, jstring jStr) {
        std::string str;
        if (jStr == NULL) {
            return "";
        }
        jclass clazz = (*env).FindClass("java/lang/String");
        jstring strencode = env->NewStringUTF("utf-8");
        jmethodID mid = env->GetMethodID(clazz, "getBytes", "(Ljava/lang/String;)[B");
        jbyteArray barr= (jbyteArray)env->CallObjectMethod(jStr, mid, strencode);
        jsize alen = env->GetArrayLength(barr);
        jbyte* ba = env->GetByteArrayElements(barr, JNI_FALSE);
        if (alen > 0)
        {
            str = std::string((const char *)ba, (int)alen);
        }
        env->ReleaseByteArrayElements(barr, ba, 0);
        (*env).DeleteLocalRef(strencode);
        (*env).DeleteLocalRef(barr);
        return str;
    }

    jobject javaWrapper() {
        return wrapperJObj;
    }

    jclass javaWrapperClass() {
        JNIEnv *env = getCurrentThreadEnv();
        return (*env).FindClass("com/hyphenate/javawrapper/channel/EMChannel");
    }

    void init_common(int sdkType, void* listener) {
        if(wrapperJObj == NULL) {
            JNIEnv *env = getCurrentThreadEnv();
            jclass cls = (*env).FindClass("com/hyphenate/javawrapper/JavaWrapper");
            jmethodID mid = (*env).GetStaticMethodID(cls,"wrapper","(IJ)Lcom/hyphenate/javawrapper/channel/EMChannel;");
            jobject jobj = (*env).CallStaticObjectMethod(cls, mid, sdkType, (long)listener);
            if (jobj != NULL) 
            {
                wrapperJObj = (*env).NewGlobalRef(jobj);
            }
        }
    }

    void uninit_common() {
        JNIEnv *env = getCurrentThreadEnv();
        jclass cls = javaWrapperClass();
        if(wrapperJObj != NULL) {
            jfieldID fidNativeListener = (*env).GetFieldID(cls, "nativeListener", "J");
            if (fidNativeListener != NULL)
            {
                (*env).SetLongField(wrapperJObj, fidNativeListener, 0);
            }
            (*env).DeleteGlobalRef(wrapperJObj);
            wrapperJObj = NULL;
        }
    }

    int get_Common(const char* manager, const char* method, const char* jstr, char* buf, const char* cbid)
    {
        JNIEnv* env = getCurrentThreadEnv();
        jobject jObj = javaWrapper();
        jclass cls = javaWrapperClass();
        jmethodID get_method = (*env).GetMethodID(cls,"nativeGet","(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;");

        jobject j1 = getJStringObject(env, manager);
        jobject j2 = getJStringObject(env, method);
        jobject j3 = getJStringObject(env, jstr);
        jobject j4 = getJStringObject(env, cbid);

        jstring javaString = (jstring)(*env).CallObjectMethod(jObj, get_method, j1, j2, j3, j4);
        string str = extractJString(env, javaString);
        memcpy(buf, str.c_str(), str.size() + 1);
        buf[str.size()] = '\0';
        return 0;
    }

    void call_Common(const char* manager, const char* method, const char* jstr, const char* cbid) {
        JNIEnv* env = getCurrentThreadEnv();
        jobject jObj = javaWrapper();
        jclass cls = javaWrapperClass();
        jmethodID call_method = (*env).GetMethodID(cls,"nativeCall","(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V");

        jobject j1 = getJStringObject(env, manager);
        jobject j2 = getJStringObject(env, method);
        jobject j3 = getJStringObject(env, jstr);
        jobject j4 = getJStringObject(env, cbid);
        
        (*env).CallVoidMethod(jObj, call_method, j1, j2, j3, j4);
    }

}



jint JNI_OnLoad(JavaVM* vm, void* reserved)
{
    g_vm = vm;
    return JNI_VERSION_1_2;
}


void JNI_OnUnload(JavaVM *vm, void *reserved) {

    if (wrapper_class != NULL)
    {
        JNIEnv *env = NULL;
        if (g_vm->GetEnv((void **) &env, JNI_VERSION_1_2) != JNI_OK) {
            return;
        }
        (*env).DeleteGlobalRef(wrapper_class);
    }
    g_vm = NULL;
    return;
}

