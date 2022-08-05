//
//  HyphenateWrapper.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/2.
//

#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C"{
#endif

void Client_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* Client_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void ContactManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* ContactManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void ChatManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* ChatManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void GroupManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* GroupManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void RoomManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* RoomManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void PushManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* PushManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void Conversation_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);
const char* Conversation_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

const char* MessageManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void UserInfoManager_MethodCall(const char* methodName, const char* jsonString, const char* callbackId);
void UserInfoManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId);


void PresenceManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

void ChatThreadManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId);

extern void UnitySendMessage(const char *,const char *, const char *);
#ifdef __cplusplus
}
#endif
