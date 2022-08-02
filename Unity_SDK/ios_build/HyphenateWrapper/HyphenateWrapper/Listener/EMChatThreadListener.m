//
//  EMChatThreadListener.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2022/7/27.
//

#import "EMChatThreadListener.h"
#import "EMChatThreadEvent+Helper.h"
#import "HyphenateWrapper.h"
#import "Transfrom.h"
#import "EMMethod.h"

@implementation EMChatThreadListener
- (void)onChatThreadCreate:(EMChatThreadEvent *)event
{
    UnitySendMessage(ChatThreadListener_Obj, "OnChatThreadCreate", [Transfrom JsonObjectToCSString:[event toJson]]);
}

- (void)onChatThreadUpdate:(EMChatThreadEvent *)event
{
    UnitySendMessage(ChatThreadListener_Obj, "OnChatThreadUpdate", [Transfrom JsonObjectToCSString:[event toJson]]);
}

- (void)onChatThreadDestroy:(EMChatThreadEvent *)event
{
    UnitySendMessage(ChatThreadListener_Obj, "OnChatThreadDestroy", [Transfrom JsonObjectToCSString:[event toJson]]);
}

- (void)onUserKickOutOfChatThread:(EMChatThreadEvent *)event
{
    UnitySendMessage(ChatThreadListener_Obj, "OnUserKickOutOfChatThread", [Transfrom JsonObjectToCSString:[event toJson]]);
}
@end
