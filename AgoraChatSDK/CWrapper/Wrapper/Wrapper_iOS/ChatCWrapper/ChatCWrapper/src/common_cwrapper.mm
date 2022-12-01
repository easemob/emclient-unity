
#include <string>
#include <assert.h>

#include "common_wrapper.h"

#include "ios_gloable.h"

using namespace std;
using namespace wrapper_ios;


void Init_Common(int sdkType, void* callback_handle) {
    wrapper_ios::init_common(sdkType, callback_handle);
}

void Uninit_Common() {
    wrapper_ios::uninit_common();
}

void NativeCall_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
{
    wrapper_ios::call_Common(manager, method, jstr, cbid);
}

const char* NativeGet_Common(const char* manager, const char* method,const char* jstr, const char* cbid)
{
    return wrapper_ios::get_Common(manager, method, jstr, cbid);
}
