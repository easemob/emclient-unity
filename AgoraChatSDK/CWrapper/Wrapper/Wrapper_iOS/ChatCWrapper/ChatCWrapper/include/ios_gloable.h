//
//  ios_gloable.h
//  OCWrapperDemo
//
//  Created by 杜洁鹏 on 2022/11/16.
//

namespace wrapper_ios {
    void init_common(int sdkType, void* listener);
    void uninit_common();
    const char* get_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
    void call_Common(const char* manager, const char* method, const char* jstr, const char* cbid);
}
