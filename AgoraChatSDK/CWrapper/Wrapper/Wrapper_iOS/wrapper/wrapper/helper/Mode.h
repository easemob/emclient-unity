//
//  Mode.h
//  wrapper
//
//  Created by yuqiang on 2024/4/7.
//

#import <Foundation/Foundation.h>
#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface Mode : NSObject

+ (EMMessageSearchScope)searchScopeFromInt:(int)scope;

@end

NS_ASSUME_NONNULL_END

