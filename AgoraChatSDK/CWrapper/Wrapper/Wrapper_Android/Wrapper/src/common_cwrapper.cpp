
#include <string>
#include <jni.h>
#include <assert.h>
#include "jni_gloable.h"

#include "common_wrapper.h"
#include "common_wrapper_internal.h"

using namespace std;
using namespace wrapper_jni;



NativeListenerEvent gCallback;

void AddListener_Common(void* callback_handle)
{
  wrapper_jni::add_listener(callback_handle);
}

void CleanListener_Common()
{

}

void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    wrapper_jni::call_Common(manager, method, jstr, cbid);
}

int NativeGet_Common(const char* manager, const char* method,const char* jstr, char* buf, const char* cbid)
{
    string value = wrapper_jni::get_Common(manager, method, jstr, buf).c_str();
    memcpy(buf, value.c_str(), value.size());
    return strlen(buf);
}




