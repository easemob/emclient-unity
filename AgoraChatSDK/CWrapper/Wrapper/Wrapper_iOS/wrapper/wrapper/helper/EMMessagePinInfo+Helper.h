//
//  EMMessagePinInfo+Helper.h
//  wrapper
//
//  Created by yuqiang on 2024/4/7.
//

#import <HyphenateChat/HyphenateChat.h>
#import "EaseModeToJson.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMMessagePinInfo (Helper) <EaseModeToJson>
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END
