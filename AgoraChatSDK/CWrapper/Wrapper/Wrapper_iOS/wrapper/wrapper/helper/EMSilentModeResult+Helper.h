//
//  EMSilentModeResult+Helper.h
//  im_flutter_sdk
//
//  Created by 杜洁鹏 on 2022/7/11.
//

#import "ChatHeaders.h"
#import "EaseModeToJson.h"
NS_ASSUME_NONNULL_BEGIN

@interface EMSilentModeResult (Helper) <EaseModeToJson>
- (NSDictionary *)toJson;
@end

NS_ASSUME_NONNULL_END
