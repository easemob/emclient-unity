//
//  ios_gloable.cpp
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/16.
//


#import <wrapper/wrapper.h>

#include <stdio.h>
#include <string>
#include "ios_gloable.h"
#include "common_wrapper_internal.h"

#import "EMCWrapper.h"
#import "EMCWrapperListener.h"
#import "NSString+Category.h"

using namespace wrapper_ios;

static EMCWrapper *cwrapper;
namespace wrapper_ios {

    void init_common(int sdkType, void* listener) {
        cwrapper = [[EMCWrapper alloc] initWithType:sdkType listener:listener];
    }

    void uninit_common() {
        cwrapper = nil;
    }

    const char* get_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
    {
        NSString *str = [cwrapper nativeGet:manager method:method params:jstr cid:cbid];
        
        if(str == nil) str = @"";
        std::string ret = str.toChar;
        char* buf = new char[ret.size() + 1];
        memcpy(buf, ret.c_str(), ret.size() + 1);
        buf[ret.size()] = '\0';
        return buf;
    }

    void call_Common(const char* manager, const char* method, const char* jstr, const char* cbid)
    {
        [cwrapper nativeCall:manager method:method params:jstr cid:cbid];
    }
}
