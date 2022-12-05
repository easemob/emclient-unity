//
//  EMWrapperHelper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2022/11/14.
//

#import "EMWrapperHelper.h"


@implementation EMWrapperHelper

+ (EMWrapperHelper *)shared {
    static EMWrapperHelper *_instance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        _instance = [[EMWrapperHelper alloc] init];
    });
    
    return _instance;
}
@end
