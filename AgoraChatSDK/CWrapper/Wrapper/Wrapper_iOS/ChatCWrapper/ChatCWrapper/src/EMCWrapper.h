//
//  EMCWrapper.h
//  ChatCWrapper
//
//  Created by 杜洁鹏 on 2022/11/16.
//


#import <Foundation/Foundation.h>
#import <wrapper/EMWrapper.h>


@interface EMCWrapper : NSObject


- (instancetype)initWithType:(int)iType
                    listener:(void* )listener;

- (NSString *)nativeGet:(const char *)manager method:(const char *)method params:(const char*)jstr cid:(const char *)cid;

- (void)nativeCall:(const char *)manager method:(const char *)method params:(const char*)jstr cid:(const char *)cid;

@end
