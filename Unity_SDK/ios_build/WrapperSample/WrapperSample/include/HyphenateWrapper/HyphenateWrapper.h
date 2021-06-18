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
void Client_HandleMethodCall(char* methodName, char* jsonString, char* callbackId);
const char* Client_GetMethodCall(char* methodName, char* jsonString, char* callbackId);

void ChatManager_HandleMethodCall(char* methodName, char* jsonString, char* callbackId);
const char* ChatManager_GetMethodCall(char* methodName, char* jsonString, char* callbackId);

void GroupManager_HandleMethodCall(char* methodName, char* jsonString, char* callbackId);
const char* GroupManager_GetMethodCall(char* methodName, char* jsonString, char* callbackId);

void RoomManager_HandleMethodCall(char* methodName, char* jsonString, char* callbackId);
const char* RoomManager_GetMethodCall(char* methodName, char* jsonString, char* callbackId);

void PushManager_HandleMethodCall(char* methodName, char* jsonString, char* callbackId);
const char* PushManager_GetMethodCall(char* methodName, char* jsonString, char* callbackId);

#ifdef __cplusplus
}
#endif
