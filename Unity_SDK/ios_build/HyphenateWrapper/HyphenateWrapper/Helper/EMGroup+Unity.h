//
//  EMGroup+Unity.h
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import <HyphenateChat/HyphenateChat.h>

NS_ASSUME_NONNULL_BEGIN

@interface EMGroup (Unity)
- (NSDictionary *)toJson;
+ (EMGroupPermissionType)premissionTypeFromInt:(int)type;
+ (int)premissionTypeToInt:(EMGroupPermissionType)type;
@end

@interface EMGroupOptions (Unity)
+ (EMGroupOptions *)formJson:(NSDictionary *)dict;
- (NSDictionary *)toJson;
+ (EMGroupStyle)styleFromInt:(int)style;
+ (int)styleToInt:(EMGroupStyle)style;
@end

@interface EMGroupSharedFile (Unity)
- (NSDictionary *)toJson;

@end
NS_ASSUME_NONNULL_END
