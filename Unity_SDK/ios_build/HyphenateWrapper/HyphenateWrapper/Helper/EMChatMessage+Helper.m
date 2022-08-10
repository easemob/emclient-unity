//
//  EMMessage+Helper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import "EMChatMessage+Helper.h"
#import "Transfrom.h"

@implementation EMChatMessage (Helper)

+ (EMChatMessage *)fromJson:(NSDictionary *)aJson
{
    NSString *bodyString = aJson[@"body"];
    NSString *bodyType = aJson[@"bodyType"];
    NSDictionary *bodyDict = [Transfrom NSStringToJsonObject:bodyString];
    EMMessageBody *body = [EMMessageBody fromJson:bodyDict bodyType:bodyType];
    if (!body) {
        return nil;
    }
    
    NSString *from = aJson[@"from"];
    if (from.length == 0) {
        from = EMClient.sharedClient.currentUsername;
    }
    
    NSString *to = aJson[@"to"];
    NSString *conversationId = aJson[@"conversationId"];
    
    NSDictionary *dict = nil;
    if (![aJson[@"attributes"] isKindOfClass:[NSNull class]]) {
        dict = [EMChatMessage extFromAttributeString:aJson[@"attributes"]];
    }
    EMChatMessage *msg = [[EMChatMessage alloc] initWithConversationID:conversationId
                                                                  from:from
                                                                    to:to
                                                                  body:body
                                                                   ext:dict];
    if (aJson[@"msgId"]) {
        msg.messageId = aJson[@"msgId"];
    }
    msg.direction = ({
        [aJson[@"direction"] isEqualToString:@"send"] ? EMMessageDirectionSend : EMMessageDirectionReceive;
    });
    
    msg.chatType = [EMChatMessage chatTypeFromInt:[aJson[@"chatType"] intValue]];
    msg.status = [msg statusFromInt:[aJson[@"status"] intValue]];
    msg.localTime = [aJson[@"localTime"] longLongValue];
    msg.timestamp = [aJson[@"serverTime"] longLongValue];
    msg.isRead = [aJson[@"isRead"] boolValue];
    msg.isReadAcked = [aJson[@"hasReadAck"] boolValue];
    msg.isDeliverAcked = [aJson[@"hasDeliverAck"] boolValue];
    msg.isNeedGroupAck = [aJson[@"isNeedGroupAck"] boolValue];
    msg.isChatThreadMessage = [aJson[@"isThread"] boolValue];
//    msg.onlineState = [aJson[@"messageOnlineState"] boolValue];
    return msg;
}

- (NSDictionary *)toJson
{
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    ret[@"from"] = self.from;
    ret[@"msgId"] = self.messageId;
    ret[@"to"] = self.to;
    ret[@"conversationId"] = self.conversationId;
    ret[@"isRead"] = @(self.isRead);
    ret[@"hasDeliverAck"] = @(self.isDeliverAcked);
    ret[@"hasReadAck"] = @(self.isReadAcked);
    ret[@"serverTime"] = @(self.timestamp);
    ret[@"localTime"] = @(self.localTime);
    ret[@"status"] = @([self statusToInt:self.status]);
    ret[@"chatType"] = @([EMChatMessage chatTypeToInt:self.chatType]);
    ret[@"direction"] = self.direction == EMMessageDirectionSend ? @"send" : @"rec";
    ret[@"bodyType"] = [self.body typeToString];
    ret[@"body"] = [Transfrom NSStringFromJsonObject:[self.body toJson]];
    ret[@"attributes"] = [EMChatMessage extToAttributeString:self.ext];
    ret[@"isNeedGroupAck"] = @(self.isNeedGroupAck);
    ret[@"messageOnlineState"] = @(self.onlineState);
    ret[@"isThread"] = @(self.isChatThreadMessage);
    
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

+ (NSDictionary *)extFromAttributeString:(NSString *)str {
    if (str.length == 0) {
        return nil;
    }
    
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    
    NSDictionary *tmpDict = [Transfrom NSStringToJsonObject:str];
    
    for (NSString *key in tmpDict.allKeys) {
        NSDictionary *valueDict = tmpDict[key];
        NSString *type = valueDict[@"type"];
        NSString *value = valueDict[@"value"];
        if ([type isEqualToString:@"b"]) {
            if ([value.lowercaseString isEqualToString:@"false"]) {
                ret[key] = @(NO);
            }else {
                ret[key] = @(YES);
            }
        }
        else if ([type isEqualToString:@"i"]) {
            ret[key] = @([value intValue]);
        }
        else if ([type isEqualToString:@"ui"]) {
            ret[key] = @([value intValue]);
        }
        else if ([type isEqualToString:@"l"]) {
            ret[key] = @([value longLongValue]);
        }
        else if ([type isEqualToString:@"f"]) {
            ret[key] = @([value floatValue]);
        }
        else if ([type isEqualToString:@"d"]) {
            ret[key] = @([value doubleValue]);
        }
        else if ([type isEqualToString:@"str"]) {
            ret[key] = value;
        }
        else if ([type isEqualToString:@"strv"]) {
            ret[key] = value;
        }
        else if ([type isEqualToString:@"jstr"]) {
            NSDictionary *infoDict = [Transfrom NSStringToJsonObject:value];
            ret[key] = infoDict;
        }
    }
    
    return ret;
}

+ (NSDictionary *)extToAttributeString:(NSDictionary *)ext {
    if (ext.allKeys.count == 0) {
        return nil;
    }
    
    NSMutableDictionary *dict = [NSMutableDictionary dictionary];
    for (NSString *key in ext.allKeys) {
        id value = ext[key];
        if ([value isKindOfClass:[NSNumber class]]) {
            const char * objCType = [((NSNumber*)value) objCType];
            if (strcmp(objCType, @encode(float)) == 0)
            {
                dict[key] = @{@"type":@"f",@"value":value};
            }
            else if (strcmp(objCType, @encode(double)) == 0)
            {
                dict[key] = @{@"type":@"d",@"value":value};
            }
            else if (strcmp(objCType, @encode(int)) == 0)
            {
                dict[key] = @{@"type":@"i",@"value":value};
            }
//            else if (strcmp(objCType, @encode(BOOL)) == 0)
//            {
//                dict[key] = @{@"type":@"b",@"value":value};
//            }
            else if (strcmp(objCType, @encode(long)) == 0)
            {
                dict[key] = @{@"type":@"l",@"value":value};
            }
            else {
                dict[key] = @{@"type":@"b",@"value":value};
            }
        }
        else if ([value isKindOfClass:[NSString class]]) {
            dict[key] = @{@"type":@"str",@"value":value};
        }
        else if ([value isKindOfClass:[NSArray class]]) {
            dict[key] = @{@"type":@"strv", @"value": value};
        }
        else if ([value isKindOfClass:[NSDictionary class]]) {
            dict[key] = @{@"type":@"jstr", @"value":value};
        }
    }
    
//    NSString *ret = [Transfrom NSStringFromJsonObject:dict];
    
    return dict;
}

@end

@implementation EMMessageBody (Helper)


+ (EMMessageBody *)fromJson:(NSDictionary *)bodyJson bodyType:(NSString *)type{
    EMMessageBody *ret = nil;
    if ([type isEqualToString:@"txt"]) {
        ret = [EMTextMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"img"]) {
        ret = [EMImageMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"loc"]) {
        ret = [EMLocationMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"video"]) {
        ret = [EMVideoMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"voice"]) {
        ret = [EMVoiceMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"file"]) {
        ret = [EMFileMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"cmd"]) {
        ret = [EMCmdMessageBody fromJson:bodyJson bodyType:nil];
    } else if ([type isEqualToString:@"custom"]) {
        ret = [EMCustomMessageBody fromJson:bodyJson bodyType:nil];
    }
    return ret;
}
- (NSString *)typeToString {
    return nil;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [NSMutableDictionary dictionary];
    NSString *type = @"";
    switch (self.type) {
        case EMMessageBodyTypeText:
            type = @"txt";
            break;
        case EMMessageBodyTypeLocation:
            type = @"loc";
            break;
        case EMMessageBodyTypeCmd:
            type = @"cmd";
            break;
        case EMMessageBodyTypeCustom:
            type = @"custom";
            break;
        case EMMessageBodyTypeFile:
            type = @"file";
            break;
        case EMMessageBodyTypeImage:
            type = @"img";
            break;
        case EMMessageBodyTypeVideo:
            type = @"video";
            break;
        case EMMessageBodyTypeVoice:
            type = @"voice";
            break;
        default:
            break;
    }
    ret[@"type"] = type;
    
    return ret;
}

+ (EMMessageBodyType)typeFromString:(NSString *)aStrType {
    
    EMMessageBodyType ret = EMMessageBodyTypeText;
    
    if([aStrType isEqualToString:@"txt"]){
        ret = EMMessageBodyTypeText;
    } else if ([aStrType isEqualToString:@"loc"]) {
        ret = EMMessageBodyTypeLocation;
    } else if ([aStrType isEqualToString:@"cmd"]) {
        ret = EMMessageBodyTypeCmd;
    } else if ([aStrType isEqualToString:@"custom"]) {
        ret = EMMessageBodyTypeCustom;
    } else if ([aStrType isEqualToString:@"file"]) {
        ret = EMMessageBodyTypeFile;
    } else if ([aStrType isEqualToString:@"img"]) {
        ret = EMMessageBodyTypeImage;
    } else if ([aStrType isEqualToString:@"video"]) {
        ret = EMMessageBodyTypeVideo;
    } else if ([aStrType isEqualToString:@"voice"]) {
        ret = EMMessageBodyTypeVoice;
    }
    return ret;
}

@end

#pragma mark - txt

@interface EMTextMessageBody (Unity)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end


@implementation EMTextMessageBody (Unity)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    EMTextMessageBody *body = [[EMTextMessageBody alloc] initWithText:aJson[@"content"]];
    body.targetLanguages = [Transfrom NSStringToJsonObject:aJson[@"targetLanguages"]];
    return body;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"content"] = self.text;
    ret[@"targetLanguages"] = [Transfrom NSStringFromJsonObject:self.targetLanguages];
    ret[@"translations"] = [Transfrom NSStringFromJsonObject:self.targetLanguages];
    return ret;
}

- (NSString *)typeToString {
    return @"txt";
}

@end

#pragma mark - loc

@interface EMLocationMessageBody (Unity)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end


@implementation EMLocationMessageBody (Unity)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    double latitude = [aJson[@"latitude"] doubleValue];
    double longitude = [aJson[@"longitude"] doubleValue];
    NSString *address = aJson[@"address"];
    NSString *buildingName = aJson[@"buildingName"];
    EMLocationMessageBody *ret  = [[EMLocationMessageBody alloc] initWithLatitude:latitude
                                                                        longitude:longitude
                                                                          address:address
                                                                     buildingName:buildingName];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"address"] = self.address;
    ret[@"latitude"] = @(self.latitude);
    ret[@"longitude"] = @(self.longitude);
    ret[@"buildingName"] = self.buildingName;
    return ret;
}

- (NSString *)typeToString {
    return @"loc";
}

@end

#pragma mark - cmd

@interface EMCmdMessageBody (Helper)
+ (EMCmdMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end

@implementation EMCmdMessageBody (Helper)

+ (EMCmdMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    EMCmdMessageBody *ret = [[EMCmdMessageBody alloc] initWithAction:aJson[@"action"]];
    ret.isDeliverOnlineOnly = [aJson[@"deliverOnlineOnly"] boolValue];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"action"] = self.action;
    ret[@"deliverOnlineOnly"] = @(self.isDeliverOnlineOnly);
    return ret;
}

- (NSString *)typeToString {
    return @"cmd";
}

@end

#pragma mark - custom

@interface EMCustomMessageBody (Helper)
+ (EMCustomMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end

@implementation EMCustomMessageBody (Helper)

+ (EMCustomMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    
    NSString *params = aJson[@"params"];
    
    NSDictionary *dict = [Transfrom NSStringToJsonObject:params];
    
    EMCustomMessageBody *ret = [[EMCustomMessageBody alloc] initWithEvent:aJson[@"event"]
                                                                      ext:dict];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"event"] = self.event;
    ret[@"params"] = self.customExt;
    return ret;
}

- (NSString *)typeToString {
    return @"custom";
}

@end

#pragma mark - file

@interface EMFileMessageBody (Helper)
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end

@implementation EMFileMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
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
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"localPath"] = self.localPath;
    ret[@"displayName"] = self.displayName;
    ret[@"secret"] = self.secretKey;
    ret[@"remotePath"] = self.remotePath;
    ret[@"fileSize"] = @(self.fileLength);
    ret[@"fileStatus"] = @([self downloadStatusToInt:self.downloadStatus]);
    return ret;
}

- (NSString *)typeToString {
    return @"file";
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
+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end

@implementation EMImageMessageBody (Helper)

+ (EMMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    NSData *imageData = [NSData dataWithContentsOfFile:path];
    EMImageMessageBody *ret = [[EMImageMessageBody alloc] initWithData:imageData
                                                           displayName:displayName];
    ret.secretKey = aJson[@"secret"];
    ret.remotePath = aJson[@"remotePath"];
    ret.fileLength = [aJson[@"fileSize"] longLongValue];
    ret.downloadStatus = [ret downloadStatusFromInt:[aJson[@"fileStatus"] intValue]];
    ret.thumbnailLocalPath = aJson[@"thumbnailLocalPath"];
    ret.thumbnailRemotePath = aJson[@"thumbnailRemotePath"];
    ret.thumbnailSecretKey = aJson[@"thumbnailSecret"];
    ret.size = CGSizeMake([aJson[@"width"] floatValue], [aJson[@"height"] floatValue]);
    ret.thumbnailDownloadStatus = [ret downloadStatusFromInt:[aJson[@"thumbnailStatus"] intValue]];
    ret.compressionRatio = [aJson[@"sendOriginalImage"] boolValue] ? 1.0 : 0.6;
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"localPath"] = self.localPath;
    ret[@"displayName"] = self.displayName;
    ret[@"remotePath"] = self.remotePath;
    ret[@"secret"] = self.secretKey;
    ret[@"thumbnailLocalPath"] = self.thumbnailLocalPath;
    ret[@"thumbnailRemotePath"] = self.thumbnailRemotePath;
    ret[@"thumbnailSecret"] = self.thumbnailSecretKey;
    ret[@"thumbnailStatus"] = @([self downloadStatusToInt:self.thumbnailDownloadStatus]);
    ret[@"fileStatus"] = @([self downloadStatusToInt:self.downloadStatus]);
    ret[@"width"] = @(self.size.width);
    ret[@"height"] = @(self.size.height);
    ret[@"sendOriginalImage"] = self.compressionRatio == 1.0 ? @(YES) : @(NO);
    ret[@"fileSize"] = @(self.fileLength);
    return ret;
}

- (NSString *)typeToString {
    return @"img";
}

@end

#pragma mark - video

@interface EMVideoMessageBody (Helper)
+ (EMVideoMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end

@implementation EMVideoMessageBody (Helper)
+ (EMVideoMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    EMVideoMessageBody *ret = [[EMVideoMessageBody alloc] initWithLocalPath:path displayName:displayName];
    ret.duration = [aJson[@"duration"] intValue];
    ret.fileLength = [aJson[@"fileSize"] longLongValue];
    ret.thumbnailLocalPath = aJson[@"thumbnailLocalPath"];
    ret.thumbnailRemotePath = aJson[@"thumbnailRemotePath"];
    ret.thumbnailSecretKey = aJson[@"thumbnailSecret"];
    ret.thumbnailDownloadStatus = [ret downloadStatusFromInt:[aJson[@"thumbnailStatus"] intValue]];
    ret.thumbnailSize = CGSizeMake([aJson[@"width"] floatValue], [aJson[@"height"] floatValue]);
    ret.remotePath = aJson[@"remotePath"];
    ret.secretKey = aJson[@"secret"];
    ret.downloadStatus = [ret downloadStatusFromInt:[aJson[@"fileStatus"] intValue]];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"duration"] = @(self.duration);
    ret[@"thumbnailLocalPath"] = self.thumbnailLocalPath;
    ret[@"thumbnailRemotePath"] = self.thumbnailRemotePath;
    ret[@"thumbnailSecret"] = self.thumbnailSecretKey;
    ret[@"thumbnailStatus"] = @([self downloadStatusToInt:self.thumbnailDownloadStatus]);
    ret[@"width"] = @(self.thumbnailSize.width);
    ret[@"height"] = @(self.thumbnailSize.height);
    ret[@"duration"] = @(self.duration);
    return ret;
}

- (NSString *)typeToString {
    return @"video";
}

@end

#pragma mark - voice

@interface EMVoiceMessageBody (Helper)
+ (EMVoiceMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type;
- (NSDictionary *)toJson;
- (NSString *)typeToString;
@end

@implementation EMVoiceMessageBody (Helper)
+ (EMVoiceMessageBody *)fromJson:(NSDictionary *)aJson bodyType:(NSString *)type{
    NSString *path = aJson[@"localPath"];
    NSString *displayName = aJson[@"displayName"];
    EMVoiceMessageBody *ret = [[EMVoiceMessageBody alloc] initWithLocalPath:path displayName:displayName];
    ret.duration = [aJson[@"duration"] intValue];
    ret.secretKey = aJson[@"secret"];
    ret.downloadStatus = [ret downloadStatusFromInt:[aJson[@"fileStatus"] intValue]];
    return ret;
}

- (NSDictionary *)toJson {
    NSMutableDictionary *ret = [[super toJson] mutableCopy];
    ret[@"duration"] = @(self.duration);
    return ret;
}

- (NSString *)typeToString {
    return @"voice";
}

@end
