
#include <string>
#include <jni.h>
#include <assert.h>
#include "jni_gloable.h"

#include "common_wrapper.h"
#include "common_wrapper_internal.h"

using namespace std;
using namespace wrapper_jni;


void Init_Common(int sdkType, void* callback) {
    wrapper_jni::init_common(sdkType, callback);
}

void Uninit_Common() {
    wrapper_jni::uninit_common();
}

void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    wrapper_jni::call_Common(manager, method, jstr, cbid);
}

int NativeGet_Common(const char* manager, const char* method,const char* jstr, char* buf, const char* cbid)
{
    return wrapper_jni::get_Common(manager, method, jstr, buf, cbid);
}



