//
//  AppDelegate.m
//  WrapperSample
//
//  Created by 杜洁鹏 on 2021/6/2.
//

#import "AppDelegate.h"
#import "include/HyphenateWrapper/HyphenateWrapper.h"

@interface AppDelegate ()

@end

@implementation AppDelegate


- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    Client_HandleMethodCall("initializeSDKWithOptions", "{\"app_key\":\"easemob-demo#chatdemoui\"}", NULL);
    return YES;
}



@end
