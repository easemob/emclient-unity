//
//  EMChatroom+Helper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/9.
//

#import "EMChatroom+Helper.h"
#import "Transfrom.h"

@implementation EMChatroom (Unity)
- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    ret[@"roomId"] = self.chatroomId;
    ret[@"name"] = self.subject;
    ret[@"desc"] = self.description;
    ret[@"owner"] = self.owner;
    ret[@"maxUsers"] = @(self.maxOccupantsCount);
    ret[@"memberCount"] = @(self.occupantsCount);
    ret[@"memberList"] = [Transfrom NSStringFromJsonObject:self.memberList];
    ret[@"adminList"] = [Transfrom NSStringFromJsonObject:self.adminList];
    ret[@"blockList"] = [Transfrom NSStringFromJsonObject:self.blacklist];
    ret[@"muteList"] = [Transfrom NSStringFromJsonObject:self.muteList];
    ret[@"isAllMemberMuted"] = @(self.isMuteAllMembers);
    ret[@"announcement"] = self.announcement;
    ret[@"permissionType"] = @([self premissionTypeToInt:self.permissionType]);


    return ret;
}

- (int)premissionTypeToInt:(EMChatroomPermissionType)type {
    int ret = -1;
    switch (type) {
        case EMChatroomPermissionTypeNone:
        {
            ret = -1;
        }
            break;
        case EMChatroomPermissionTypeMember:
        {
            ret = 0;
        }
            break;
        case EMChatroomPermissionTypeAdmin:
        {
            ret = 1;
        }
            break;
        case EMChatroomPermissionTypeOwner:
        {
            ret = 2;
        }
            break;
        default:
            break;
    }
    return ret;
}
@end
