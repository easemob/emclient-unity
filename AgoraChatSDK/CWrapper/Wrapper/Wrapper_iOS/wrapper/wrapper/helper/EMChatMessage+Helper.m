//
//  EMChatMessage+Flutter.m
//  Pods
//
//  Created by 杜洁鹏 on 2020/9/11.
//

#import "EMChatMessage+Helper.h"
#import "EMChatThread+Helper.h"
#import "NSDictionary+Category.h"
#import "NSArray+Category.h"

@implementation EMChatMessage (Helper)

+ (EMChatMessage *)fromJson:(NSDictionary *)aJson
{
    EMMessageBody *body = [EMMessageBody fromJson:aJson[@"body"]];
    if (!body) {
        return nil;
    }
    
    
    NSString *from = aJson[@"from"];
    if (from.length == 0) {
        from = EMClient.sharedClient.currentUsername;
    }
    
    NSString *to = aJson[@"to"];
    NSString *conversationId = aJson[@"convId"];
    

    EMChatMessage *msg = [[EMChatMessage alloc] initWithConversationID:conversationId
                                                          from:from
                                                            to:to
                                                          body:body
                                                           ext:nil];
    if (aJson[@"msgId"]) {
        msg.messageId = aJson[@"msgId"];
    }
    
    msg.direction = ({
        [aJson[@"direction"] intValue] == 0 ? EMMessageDirectionSend : EMMessageDirectionReceive;
    });
    
    
    msg.chatType = [EMChatMessage chatTypeFromInt:[aJson[@"chatType"] intValue]];
    msg.status = [msg statusFromInt:[aJson[@"status"] intValue]];
    msg.localTime = [aJson[@"localTime"] longLongValue];
    msg.timestamp = [aJson[@"serverTime"] longLongValue];
    msg.isReadAcked = [aJson[@"hasReadAck"] boolValue];
    msg.isDeliverAcked = [aJson[@"hasDeliverAck"] boolValue];
    msg.isRead = [aJson[@"hasRead"] boolValue];
    msg.isNeedGroupAck = [aJson[@"isNeedGroupAck"] boolValue];
    if (aJson[@"priority"]) {
        msg.priority = [aJson[@"priority"] integerValue];
    }
    if(aJson[@"deliverOnlineOnly"]) {
        msg.deliverOnlineOnly = [aJson[@"deliverOnlineOnly"] boolValue];
    }
    // read only
    // msg.groupAckCount = [aJson[@"groupAckCount"] intValue]
    // msg.chatThread = [EMChatThread forJson:aJson[@"thread"]];
//    msg.onlineState =
    msg.isChatThreadMessage = [aJson[@"isThread"] boolValue];
    NSMutableDictionary *extDict = nil;
    if (aJson[@"attr"]) {
        extDict = [NSMutableDictionary dictionary];
        NSDictionary *attrDict = aJson[@"attr"];
        NSArray *keys = attrDict.allKeys;
        for (NSString *key in keys) {
            NSDictionary *valueDict = attrDict[key];
            NSString *type = valueDict[@"type"];
            NSString *value = valueDict[@"value"];
            if ([type isEqualToString:@"b"]) {
                if ([value.lowercaseString isEqualToString:@"false"]) {
                    extDict[key] = @(NO);
                }else {
                    extDict[key] = @(YES);
                }
            }else if ([type isEqualToString:@"i"]) {
                extDict[key] = @([value intValue]);
            }else if ([type isEqualToString:@"l"]) {
                extDict[key] = @([value longLongValue]);
            }else if ([type isEqualToString:@"f"]) {
                extDict[key] = @([value floatValue]);
            }else if ([type isEqualToString:@"d"]) {
                extDict[key] = @([value doubleValue]);
            }else if ([type isEqualToString:@"str"]) {
                extDict[key] = value;
            }else if ([type isEqualToString:@"jstr"]) {
                extDict[key] = [NSArray fromString:value];
            }
        }
    }
    if(aJson[@"receiverList"]) {
        msg.receiverList = aJson[@"receiverList"];
    }
    msg.ext = extDict;
    return msg;
}

- (NSDictionary *)toJson
{
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    ret[@"from"] = self.from;
    ret[@"msgId"] = self.messageId;
    ret[@"to"] = self.to;
    ret[@"convId"] = self.conversationId;
    ret[@"hasRead"] = @(self.isRead);
    ret[@"hasDeliverAck"] = @(self.isDeliverAcked);
    ret[@"hasReadAck"] = @(self.isReadAcked);
    ret[@"isNeedGroupAck"] = @(self.isNeedGroupAck);
    ret[@"serverTime"] = @(self.timestamp);
    ret[@"groupAckCount"] = @(self.groupAckCount);
    ret[@"localTime"] = @(self.localTime);
    ret[@"status"] = @([self statusToInt:self.status]);
    ret[@"chatType"] = @([EMChatMessage chatTypeToInt:self.chatType]);
    ret[@"isThread"] = @(self.isChatThreadMessage);
    ret[@"direction"] = self.direction == EMMessageDirectionSend ? @(0) : @(1);
    ret[@"body"] = [self.body toJson];
    ret[@"messageOnlineState"] = @(self.onlineState);
    ret[@"deliverOnlineOnly"] = @(self.deliverOnlineOnly);
    ret[@"broadcast"] = @(self.broadcast);
    ret[@"isContentReplaced"] = @(self.isContentReplaced);
    if (self.ext) {
        NSMutableDictionary *dict = [NSMutableDictionary dictionary];
        for (NSString *key in self.ext.allKeys) {
            id value = self.ext[key];
            if ([value isKindOfClass:[NSNumber class]]) {
                const char* objCType = [((NSNumber*)value) objCType];
                if (strcmp(objCType, @encode(float)) == 0)
                {
                    dict[key] = @{@"type":@"f", @"value": value};
                }
                else if (strcmp(objCType, @encode(double)) == 0)
                {
                    dict[key] = @{@"type":@"d",@"value":value};
                }
                else if (strcmp(objCType, @encode(int)) == 0)
                {
                    dict[key] = @{@"type":@"i",@"value":value};
                }
                else if (strcmp(objCType, @encode(long)) == 0)
                {
                    dict[key] = @{@"type":@"l",@"value":value};
                }else {
                    if ([value boolValue] == YES) {
                        dict[key] = @{@"type":@"b",@"value":@"True"};
                    }else {
                        dict[key] = @{@"type":@"b",@"value":@"False"};
                    }
                }
            } else if ([value isKindOfClass:[NSDictionary class]] || [value isKindOfClass:[NSArray class]]) {
                dict[key] = @{@"type":@"jstr",@"value": value};
            }
            else if([value isKindOfClass:[NSString class]]) {
                dict[key] = @{@"type":@"str",@"value":value};;
            }
        }
        ret[@"attr"] = dict;
    }
    return ret;
}

- (EMMessageStatus)statusFromInt:(int)aStatus {
    EMMessageStatus status = EMMessageStatusPending;
    switch (aStatus) {
        case 0:
        {
            status = EMMessageStatusPending;
        }
            break;
        case 1:
        {
            status = EMMessageStatusDelivering;
        }
            break;
        case 2:
        {
            status = EMMessageStatusSucceed;
        }
            break;
        case 3:
        {
            status = EMMessageStatusFailed;
        }
            break;
    }
    
    return status;
}

- (int)statusToInt:(EMMessageStatus)aStatus {
    int status = 0;
    switch (aStatus) {
        case EMMessageStatusPending:
        {
            status = 0;
        }
            break;
        case EMMessageStatusDelivering:
        {
            status = 1;
        }
            break;
        case EMMessageStatusSucceed:
        {
            status = 2;
        }
            break;
        case EMMessageStatusFailed:
        {
            status = 3;
        }
            break;
    }
    
    return status;
}

+ (EMChatType)chatTypeFromInt:(int)aType {
    EMChatType type = EMChatTypeChat;
    switch (aType) {
        case 0:
            type = EMChatTypeChat;
            break;
        case 1:
            type = EMChatTypeGroupChat;
            break;
        case 2:
            type = EMChatTypeChatRoom;
            break;
    }
    
    return type;
}

+ (int)chatTypeToInt:(EMChatType)aType {
    int type;
    switch (aType) {
        case EMChatTypeChat:
            type = 0;
            break;
        case EMChatTypeGroupChat:
            type = 1;
            break;
        case EMChatTypeChatRoom:
            type = 2;
            break;
    }
    return type;
}

@end

@implementation EMMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)json {
    EMMessageBody *ret = nil;
    NSDictionary *bodyJson = json[@"body"];
    int type = [json[@"type"] intValue];
    switch (type) {
        case 0:
        {
            ret = [EMTextMessageBody fromJson:bodyJson];
        }
            break;
        case 1:
        {
            ret = [EMImageMessageBody fromJson:bodyJson];
        }
            break;
        case 2:
        {
            ret = [EMVideoMessageBody fromJson:bodyJson];
        }
            break;
        case 3:
        {
            ret = [EMLocationMessageBody fromJson:bodyJson];
        }
            break;
        case 4:
        {
            ret = [EMVoiceMessageBody fromJson:bodyJson];
        }
            break;
        case 5:
        {
            ret = [EMFileMessageBody fromJson:bodyJson];
        }
            break;
        case 6:
        {
            ret = [EMCmdMessageBody fromJson:bodyJson];
        }
            break;
        case 7:
        {
            ret = [EMCustomMessageBody fromJson:bodyJson];
        }
            break;
        case 8:
        {
            ret = [EMCombineMessageBody fromJson:bodyJson];
        }
            break;;
        default:
            break;
    }
   
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    int type = 0;
    switch (self.type) {
        case EMMessageBodyTypeText:
            type = 0;
            break;
        case EMMessageBodyTypeLocation:
            type = 3;
            break;
        case EMMessageBodyTypeCmd:
            type = 6;
            break;
        case EMMessageBodyTypeCustom:
            type = 7;
            break;
        case EMMessageBodyTypeFile:
            type = 5;
            break;
        case EMMessageBodyTypeImage:
            type = 1;
            break;
        case EMMessageBodyTypeVideo:
            type = 2;
            break;
        case EMMessageBodyTypeVoice:
            type = 4;
            break;
        case EMMessageBodyTypeCombine:
            type = 8;
            break;
        default:
            break;
    }
    ret[@"type"] = @(type);
    ret[@"operationTime"] = @(self.operationTime);
    ret[@"operationCount"] = @(self.operatorCount);
    ret[@"operatorId"] = self.operatorId;
    
    return ret;
}


@end

#pragma mark - txt

@interface EMTextMessageBody (Helper)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end


@implementation EMTextMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson {
    EMTextMessageBody *body = [[EMTextMessageBody alloc] initWithText:aJson[@"content"]];
    body.targetLanguages = aJson[@"targetLanguages"];
    return body;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"content"] = self.text;
    bodyDict[@"targetLanguages"] = self.targetLanguages;
    bodyDict[@"translations"] = self.translations;
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(0);
    return ret;
}

@end

#pragma mark - loc

@interface EMLocationMessageBody (Helper)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end


@implementation EMLocationMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson {
    double latitude = [aJson[@"latitude"] doubleValue];
    double longitude = [aJson[@"longitude"] doubleValue];
    NSString *address = aJson[@"address"];
    NSString *buildingName = aJson[@"buildingName"];
    
    EMLocationMessageBody *ret  = [[EMLocationMessageBody alloc] initWithLatitude:latitude
                                                                        longitude:longitude
                                                                          address:address buildingName:buildingName];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"address"] = self.address;
    bodyDict[@"latitude"] = @(self.latitude);
    bodyDict[@"longitude"] = @(self.longitude);
    bodyDict[@"buildingName"] = self.buildingName;
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(3);
    return ret;
}

@end

#pragma mark - cmd

@interface EMCmdMessageBody (Helper)
+ (EMCmdMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMCmdMessageBody (Helper)

+ (EMCmdMessageBody *)fromJson:(NSDictionary *)aJson {
    EMCmdMessageBody *ret = [[EMCmdMessageBody alloc] initWithAction:aJson[@"action"]];
    ret.isDeliverOnlineOnly = [aJson[@"deliverOnlineOnly"] boolValue];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"action"] = self.action;
    bodyDict[@"deliverOnlineOnly"] = @(self.isDeliverOnlineOnly);
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(6);
    return ret;
}

@end

#pragma mark - custom

@interface EMCustomMessageBody (Helper)
+ (EMCustomMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMCustomMessageBody (Helper)

+ (EMCustomMessageBody *)fromJson:(NSDictionary *)aJson {
    NSDictionary *dic = aJson[@"params"];
    EMCustomMessageBody *ret = [[EMCustomMessageBody alloc] initWithEvent:aJson[@"event"]
                                                                customExt:dic];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"event"] = self.event;
    bodyDict[@"params"] = self.customExt;
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(7);
    return ret;
}

@end

#pragma mark - file

@interface EMFileMessageBody (Helper)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMFileMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson {
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    EMFileMessageBody *ret = [[EMFileMessageBody alloc] initWithLocalPath:path
                                                              displayName:displayName];
    ret.secretKey = aJson[@"secret"];
    ret.remotePath = aJson[@"remotePath"];
    ret.fileLength = [aJson[@"fileSize"] longLongValue];
    ret.downloadStatus = [ret downloadStatusFromInt:[aJson[@"fileStatus"] intValue]];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"localPath"] = self.localPath;
    bodyDict[@"displayName"] = self.displayName;
    bodyDict[@"secret"] = self.secretKey;
    bodyDict[@"remotePath"] = self.remotePath;
    bodyDict[@"fileSize"] = @(self.fileLength);
    bodyDict[@"fileStatus"] = @([self downloadStatusToInt:self.downloadStatus]);
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(5);
    return ret;
}

- (EMDownloadStatus)downloadStatusFromInt:(int)aStatus {
    EMDownloadStatus ret = EMDownloadStatusPending;
    switch (aStatus) {
        case 0:
            ret = EMDownloadStatusDownloading;
            break;
        case 1:
            ret = EMDownloadStatusSucceed;
            break;
        case 2:
            ret = EMDownloadStatusFailed;
            break;
        case 3:
            ret = EMDownloadStatusPending;
            break;
        default:
            break;
    }
    
    return ret;
}

- (int)downloadStatusToInt:(EMDownloadStatus)aStatus {
    int ret = 0;
    switch (aStatus) {
        case EMDownloadStatusDownloading:
            ret = 0;
            break;
        case EMDownloadStatusSucceed:
            ret = 1;
            break;
        case EMDownloadStatusFailed:
            ret = 2;
            break;
        case EMDownloadStatusPending:
            ret = 3;
            break;
        default:
            break;
    }
    return ret;
}

@end

#pragma mark - img

@interface EMImageMessageBody (Helper)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMImageMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson {
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    EMImageMessageBody *ret = [[EMImageMessageBody alloc] initWithLocalPath:path
                                                           displayName:displayName];
    ret.secretKey = aJson[@"secret"];
    ret.remotePath = aJson[@"remotePath"];
    ret.fileLength = [aJson[@"fileSize"] longLongValue];
    ret.downloadStatus = [ret downloadStatusFromInt:[aJson[@"fileStatus"] intValue]];
    ret.thumbnailLocalPath = aJson[@"thumbnailLocalPath"];
    ret.thumbnailRemotePath = aJson[@"thumbnailRemotePath"];
    ret.thumbnailSecretKey = aJson[@"thumbnailSecret"];
    ret.thumbnailDownloadStatus = [ret downloadStatusFromInt:[aJson[@"thumbnailStatus"] intValue]];
    ret.size = CGSizeMake([aJson[@"width"] floatValue], [aJson[@"height"] floatValue]);
    ret.thumbnailDownloadStatus = [ret downloadStatusFromInt:[aJson[@"thumbnailStatus"] intValue]];
    ret.compressionRatio = [aJson[@"sendOriginalImage"] boolValue] ? 1.0 : 0.6;
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"thumbnailLocalPath"] = self.thumbnailLocalPath;
    bodyDict[@"thumbnailRemotePath"] = self.thumbnailRemotePath;
    bodyDict[@"thumbnailSecret"] = self.thumbnailSecretKey;
    bodyDict[@"thumbnailStatus"] = @([self downloadStatusToInt:self.thumbnailDownloadStatus]);
    bodyDict[@"fileStatus"] = @([self downloadStatusToInt:self.downloadStatus]);
    bodyDict[@"width"] = @(self.size.width);
    bodyDict[@"height"] = @(self.size.height);
    bodyDict[@"fileSize"] = @(self.fileLength);
    bodyDict[@"remotePath"] = self.remotePath;
    bodyDict[@"secret"] = self.secretKey;
    bodyDict[@"displayName"] = self.displayName;
    bodyDict[@"localPath"] = self.localPath;
    bodyDict[@"sendOriginalImage"] = self.compressionRatio == 1.0 ? @(true) : @(false);
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(1);
    return ret;
}
@end

#pragma mark - video

@interface EMVideoMessageBody (Helper)
+ (EMVideoMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMVideoMessageBody (Helper)
+ (EMVideoMessageBody *)fromJson:(NSDictionary *)aJson {
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    EMVideoMessageBody *ret = [[EMVideoMessageBody alloc] initWithLocalPath:path displayName:displayName];
    ret.duration = [aJson[@"duration"] intValue];
    ret.secretKey = aJson[@"secret"];
    ret.remotePath = aJson[@"remotePath"];
    ret.fileLength = [aJson[@"fileSize"] longLongValue];
    ret.thumbnailLocalPath = aJson[@"thumbnailLocalPath"];
    ret.thumbnailRemotePath = aJson[@"thumbnailRemotePath"];
    ret.thumbnailSecretKey = aJson[@"thumbnailSecret"];
    ret.thumbnailDownloadStatus = [ret downloadStatusFromInt:[aJson[@"thumbnailStatus"] intValue]];
    ret.thumbnailSize = CGSizeMake([aJson[@"width"] floatValue], [aJson[@"height"] floatValue]);
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"duration"] = @(self.duration);
    bodyDict[@"thumbnailLocalPath"] = self.thumbnailLocalPath;
    bodyDict[@"secret"] = self.secretKey;
    bodyDict[@"remotePath"] = self.remotePath;
    bodyDict[@"thumbnailRemotePath"] = self.thumbnailRemotePath;
    bodyDict[@"thumbnailSecretKey"] = self.thumbnailSecretKey;
    bodyDict[@"thumbnailStatus"] = @([self downloadStatusToInt:self.thumbnailDownloadStatus]);
    bodyDict[@"width"] = @(self.thumbnailSize.width);
    bodyDict[@"height"] = @(self.thumbnailSize.height);
    bodyDict[@"fileSize"] = @(self.fileLength);
    bodyDict[@"displayName"] = self.displayName;
    bodyDict[@"duration"] = @(self.duration);
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(2);
    return ret;
}
@end

#pragma mark - voice

@interface EMVoiceMessageBody (Helper)
+ (EMVoiceMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMVoiceMessageBody (Helper)
+ (EMVoiceMessageBody *)fromJson:(NSDictionary *)aJson {
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    EMVoiceMessageBody *ret = [[EMVoiceMessageBody alloc] initWithLocalPath:path displayName:displayName];
    ret.secretKey = aJson[@"secret"];
    ret.remotePath = aJson[@"remotePath"];
    ret.duration = [aJson[@"duration"] intValue];
    ret.downloadStatus = [ret downloadStatusFromInt:[aJson[@"fileStatus"] intValue]];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"duration"] = @(self.duration);
    bodyDict[@"displayName"] = self.displayName;
    bodyDict[@"localPath"] = self.localPath;
    bodyDict[@"fileSize"] = @(self.fileLength);
    bodyDict[@"secret"] = self.secretKey;
    bodyDict[@"remotePath"] = self.remotePath;
    bodyDict[@"fileStatus"] = @([self downloadStatusToInt:self.downloadStatus]);;
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(4);
    return ret;
}

@end


#pragma mark - combine

@interface EMCombineMessageBody (Helper)
+ (EMCombineMessageBody *)fromJson:(NSDictionary *)aJson;
- (NSDictionary *)toJson;
@end

@implementation EMCombineMessageBody (Helper)
+ (EMCombineMessageBody *)fromJson:(NSDictionary *)aJson {
    
    NSString *title = aJson[@"title"];
    NSString *summary = aJson[@"summary"];
    NSString *compatibleText = aJson[@"compatibleText"];
    NSArray *msgList = aJson[@"messageList"];
    EMCombineMessageBody *ret = [[EMCombineMessageBody alloc] initWithTitle:title
                                                                    summary:summary
                                                             compatibleText:compatibleText
                                                              messageIdList:msgList];

    
    ret.remotePath = aJson[@"remotePath"];
    ret.secretKey = aJson[@"secret"];
    ret.localPath = aJson[@"localPath"];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSMutableDictionary *bodyDict = [NSMutableDictionary dictionary];
    bodyDict[@"remotePath"] = self.remotePath;
    bodyDict[@"secret"] = self.secretKey;
    bodyDict[@"localPath"] = self.localPath;
    bodyDict[@"title"] = self.title;
    bodyDict[@"summary"] = self.summary;
    bodyDict[@"compatibleText"] = self.compatibleText;
    ret[@"body"] = bodyDict;
    ret[@"type"] = @(8);
    return ret;
}

@end
