//
//  EMFetchServerMessagesOption+Helper.m
//  wrapper
//
//  Created by 杜洁鹏 on 2023/5/30.
//

#import "EMFetchServerMessagesOption+Helper.h"

@implementation EMFetchServerMessagesOption (Helper)
+ (EMFetchServerMessagesOption *)fromJson:(NSDictionary *)dict {
    EMFetchServerMessagesOption *options = [[EMFetchServerMessagesOption alloc] init];
    options.direction = [dict[@"direction"] intValue] == 0 ? EMMessageSearchDirectionUp : EMMessageSearchDirectionDown;
    options.startTime = [dict[@"startTime"] intValue];
    options.endTime = [dict[@"endTime"] intValue];
    options.from = dict[@"from"];
    options.isSave = [dict[@"isSave"] boolValue];
    NSArray *types = dict[@"types"];
    NSMutableArray<NSNumber*> *list = [NSMutableArray new];
    if(types) {
        for (int i = 0; i < types.count; i++) {
            int type = [types[i] intValue];
            if (type == 0) {
                [list addObject:@(EMMessageBodyTypeText)];
            } else if (type == 1) {
                [list addObject:@(EMMessageBodyTypeImage)];
            } else if (type == 2) {
                [list addObject:@(EMMessageBodyTypeVideo)];
            } else if (type == 3) {
                [list addObject:@(EMMessageBodyTypeLocation)];
            } else if (type == 4) {
                [list addObject:@(EMMessageBodyTypeVoice)];
            } else if (type == 5) {
                [list addObject:@(EMMessageBodyTypeFile)];
            } else if (type == 6) {
                [list addObject:@(EMMessageBodyTypeCmd)];
            } else if (type == 7) {
                [list addObject:@(EMMessageBodyTypeCustom)];
            }
        }
    }
    
    if(list.count > 0) {
        options.msgTypes = list;
    }
    
    return options;
}
@end
