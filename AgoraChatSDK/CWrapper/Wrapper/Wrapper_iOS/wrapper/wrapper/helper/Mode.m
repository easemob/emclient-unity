//
//  Mode.m
//  wrapper
//
//  Created by yuqiang on 2024/4/7.
//

#import "Mode.h"

@implementation Mode

+ (EMMessageSearchScope)searchScopeFromInt:(int)scope {
    if (scope == 0) {
        return EMMessageSearchScopeContent;
    } else if (scope == 1) {
        return EMMessageSearchScopeExt;
    } else if (scope == 2) {
        return EMMessageSearchScopeAll;
    } else {
        return EMMessageSearchScopeContent;
    }
}

@end
