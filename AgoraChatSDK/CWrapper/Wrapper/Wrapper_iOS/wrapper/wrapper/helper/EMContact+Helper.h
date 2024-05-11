//
//  EMContact+Helper.h
//  wrapper
//
//  Created by yuqiang on 2024/2/22.
//

#import <HyphenateChat/HyphenateChat.h>
#import "EaseModeToJson.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMContact (Helper) <EaseModeToJson>
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END


