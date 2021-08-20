//
//  hyphenateCWrapper.cpp
//  hyphenateCWrapper
//
//  Created by 杜洁鹏 on 2021/4/20.
//  Copyright © 2021 easemob. All rights reserved.
//

#include "hyphenateCWrapper.h"
#include "emchatclient.h"
#include "emchatconfigs.h"

easemob::EMChatClient *chatClient_;
easemob::EMChatConfigsPtr chatConfigs_;

extern "C" int EXPORT_API TestAB(int a,int b)
{
    
    chatConfigs_ = easemob::EMChatConfigsPtr(new easemob::EMChatConfigs("", "/Documents/", "easemob-demo#chatdemoui"));
    chatClient_ = easemob::EMChatClient::create(chatConfigs_);
    
    return a+b;
}

extern "C"
{
    int TestAC(int a);
}

int TestAC(int w )
{
    return w * 10;

}

