//
//  EMRecallMessageInfo+Helper.h
//  wrapper
//
//  Created by yuqiang on 2024/5/31.
//

#import <HyphenateChat/HyphenateChat.h>
#import "EaseModeToJson.h"

NS_ASSUME_NONNULL_BEGIN

@interface EMRecallMessageInfo (Helper) <EaseModeToJson>
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END
