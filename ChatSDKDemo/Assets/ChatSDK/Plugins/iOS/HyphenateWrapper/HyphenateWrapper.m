//
//  HyphenateWrapper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/2.
//

#import <Foundation/Foundation.h>
#import "HyphenateWrapper.h"
#import "Transfrom.h"
#import "EMClientWrapper.h"

void outputAppendString (char *str1, char *str2)
{
  NSString *string1 = [[NSString alloc] initWithUTF8String:str1];
  NSString *string2 = [[NSString alloc] initWithUTF8String:str2];
  
  NSLog(@"###%@", [NSString stringWithFormat:@"%@ %@", string1, string2]);
}


void Client_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    NSDictionary *dic = [Transfrom DictFromCString:jsonString];
    if ([[Transfrom NSStringFromCString:methodName] isEqualToString:@"initializeSDKWithOptions"]) {
        [EMClientWrapper.instance initSDKWithDict:dic];
    }
    if ([[Transfrom NSStringFromCString:methodName] isEqualToString:@"createAccount"]) {
        [EMClientWrapper.instance createAccount:[Transfrom NSStringFromCString:jsonString]
                                     callbackId:[Transfrom NSStringFromCString:callbackId]];
    }
}

const char* Client_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    return NULL;
}

void ChatManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    
}

const char* ChatManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    return NULL;
}

void GroupManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    
}

const char* GroupManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    return NULL;
}

void RoomManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    
}

const char* RoomManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    return NULL;
}

void PushManager_HandleMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    
}

const char* PushManager_GetMethodCall(const char* methodName, const char* jsonString, const char* callbackId) {
    return NULL;
}
