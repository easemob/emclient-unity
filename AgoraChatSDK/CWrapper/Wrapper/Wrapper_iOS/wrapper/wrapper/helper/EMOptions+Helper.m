//
//  EMOptions+Flutter.m
//  im_flutter_sdk
//
//  Created by 杜洁鹏 on 2020/10/12.
//

#import "EMOptions+Helper.h"
#import <HyphenateChat/EMOptions+PrivateDeploy.h>

@implementation EMOptions (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"appKey"] = self.appkey;
    data[@"autoLogin"] = @(self.isAutoLogin);
    data[@"debugModel"] = @(self.enableConsoleLog);
    data[@"requireAck"] = @(self.enableRequireReadAck);
    data[@"requireDeliveryAck"] = @(self.enableDeliveryAck);
    data[@"sortMessageByServerTime"] = @(self.sortMessageByServerTime);
    data[@"acceptInvitationAlways"] = @(self.autoAcceptFriendInvitation);
    data[@"autoAcceptGroupInvitation"] = @(self.autoAcceptGroupInvitation);
    data[@"deleteMessagesAsExitGroup"] = @(self.deleteMessagesOnLeaveGroup);
    data[@"deleteMessagesAsExitRoom"] = @(self.deleteMessagesOnLeaveChatroom);
    data[@"isAutoDownload"] = @(self.autoDownloadThumbnail);
    data[@"isRoomOwnerLeaveAllowed"] = @(self.canChatroomOwnerLeave);
    data[@"serverTransfer"] = @(self.isAutoTransferMessageAttachments);
    data[@"usingHttpsOnly"] = @(self.usingHttpsOnly);
    data[@"pushConfig"] = @{@"pushConfig": @{@"apnsCertName": self.apnsCertName}};
    data[@"enableDNSConfig"] = @(self.enableDnsConfig);
    data[@"imPort"] = @(self.chatPort);
    data[@"imServer"] = self.chatServer;
    data[@"restServer"] = self.restServer;
    data[@"dnsUrl"] = self.dnsURL;
    data[@"areaCode"] = @(self.area);
    data[@"enableEmptyConversation"] = @(self.loadEmptyConversations);
    data[@"osType"] = @(self.customOSType);
    data[@"deviceName"] = self.customDeviceName;
    data[@"useReplacedMessageContents"] = @(self.useReplacedMessageContents);
    data[@"regardImportMsgAsRead"] = @(self.regardImportMessagesAsRead);
    data[@"includeSendMessageInMessageListener"] = @(self.includeSendMessageInMessageListener);
    
    return data;
}
+ (EMOptions *)fromJson:(NSDictionary *)aJson {
    EMOptions *options = [EMOptions optionsWithAppkey:aJson[@"appKey"]];
    options.isAutoLogin = [aJson[@"autoLogin"] boolValue];
    options.enableConsoleLog = [aJson[@"debugModel"] boolValue];
    options.enableRequireReadAck = [aJson[@"requireAck"] boolValue];
    options.enableDeliveryAck = [aJson[@"requireDeliveryAck"] boolValue];
    options.sortMessageByServerTime = [aJson[@"sortMessageByServerTime"] boolValue];
    options.autoAcceptFriendInvitation = [aJson[@"acceptInvitationAlways"] boolValue];
    options.autoAcceptGroupInvitation = [aJson[@"autoAcceptGroupInvitation"] boolValue];
    options.deleteMessagesOnLeaveGroup = [aJson[@"deleteMessagesAsExitGroup"] boolValue];
    options.deleteMessagesOnLeaveChatroom = [aJson[@"deleteMessagesAsExitRoom"] boolValue];
    options.autoDownloadThumbnail = [aJson[@"isAutoDownload"] boolValue];
    options.canChatroomOwnerLeave = [aJson[@"isRoomOwnerLeaveAllowed"] boolValue];
    options.isAutoTransferMessageAttachments = [aJson[@"serverTransfer"] boolValue];
    options.usingHttpsOnly = [aJson[@"usingHttpsOnly"] boolValue];
    options.apnsCertName = aJson[@"pushConfig"][@"apnsCertName"];
    options.enableDnsConfig = [aJson[@"enableDnsConfig"] boolValue];
    if (!options.enableDnsConfig) {
        options.chatPort = [aJson[@"imPort"] intValue];
        options.chatServer = aJson[@"imServer"];
        options.restServer = aJson[@"restServer"];
    }else {
        if (aJson[@"dnsURL"] && [aJson[@"dnsURL"] length] > 0 ) {
            options.dnsURL = aJson[@"dnsURL"];
        }
    }
    options.area = [aJson[@"areaCode"] intValue];
    if(aJson[@"enableEmptyConversation"]) {
        options.loadEmptyConversations = [aJson[@"enableEmptyConversation"] boolValue];
    }
    options.customDeviceName = aJson[@"deviceName"];
    if(aJson[@"osType"]) {
        options.customOSType = [aJson[@"osType"] intValue];
    }

    if(aJson[@"useReplacedMessageContents"]) {
        options.useReplacedMessageContents = [aJson[@"useReplacedMessageContents"] boolValue];
    }
    
    if(aJson[@"regardImportMsgAsRead"]) {
        options.regardImportMessagesAsRead = [aJson[@"regardImportMsgAsRead"] boolValue];
    }

    if(aJson[@"includeSendMessageInMessageListener"]) {
        options.includeSendMessageInMessageListener = [aJson[@"includeSendMessageInMessageListener"] boolValue];
    }

    return options;
}
@end
