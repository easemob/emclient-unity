//
//  EMOptions+Helper.m
//  HyphenateWrapper
//
//  Created by 杜洁鹏 on 2021/6/7.
//

#import "EMOptions+Helper.h"
#import <HyphenateChat/EMOptions+PrivateDeploy.h>


@implementation EMOptions (Helper)
- (NSDictionary *)toJson {
    NSMutableDictionary *data = [NSMutableDictionary dictionary];
    data[@"app_key"] = self.appkey;
    data[@"auto_login"] = @(self.isAutoLogin);
    data[@"debug_model"] = @(self.enableConsoleLog);
    data[@"require_ack"] = @(self.enableRequireReadAck);
    data[@"require_delivery_ack"] = @(self.enableDeliveryAck);
    data[@"sort_message_by_server_time"] = @(self.sortMessageByServerTime);
    data[@"accept_invitation_always"] = @(self.autoAcceptFriendInvitation);
    data[@"auto_accept_group_invitation"] = @(self.autoAcceptGroupInvitation);
    data[@"delete_messages_as_exit_group"] = @(self.deleteMessagesOnLeaveGroup);
    data[@"delete_messages_as_exit_room"] = @(self.deleteMessagesOnLeaveChatroom);
    data[@"is_auto_download"] = @(self.autoDownloadThumbnail);
    data[@"is_room_owner_leave_allowed"] = @(self.canChatroomOwnerLeave);
    //    data[@"server_transfer"] = @(self.isAutoTransferMessageAttachments);
    data[@"using_https_only"] = @(self.usingHttpsOnly);
    data[@"apns_cer_name"] = self.apnsCertName;
    data[@"enable_dns_config"] = @(self.enableDnsConfig);
    data[@"im_port"] = @(self.chatPort);
    data[@"im_server"] = self.chatServer;
    data[@"rest_server"] = self.restServer;
    data[@"dns_url"] = self.dnsURL;
    data[@"area"] = @(self.area);
    return data;
}

+ (EMOptions *)fromJson:(NSDictionary *)aJson {
    
    NSString *str = aJson[@"app_key"];
    if ([str isKindOfClass:[NSNull class]] || str.length == 0) {
        return nil;
    }
    
    EMOptions *options = [EMOptions optionsWithAppkey:aJson[@"app_key"]];
    options.isAutoLogin = [aJson[@"auto_login"] boolValue];
    options.enableConsoleLog = [aJson[@"debug_mode"] boolValue];
    options.enableRequireReadAck = [aJson[@"require_ack"] boolValue];
    options.enableDeliveryAck = [aJson[@"require_delivery_ack"] boolValue];
    options.sortMessageByServerTime = [aJson[@"sort_message_by_server_time"] boolValue];
    options.autoAcceptFriendInvitation = [aJson[@"accept_invitation_always"] boolValue];
    options.autoAcceptGroupInvitation = [aJson[@"auto_accept_group_invitation"] boolValue];
    options.deleteMessagesOnLeaveGroup = [aJson[@"delete_messages_as_exit_group"] boolValue];
    options.deleteMessagesOnLeaveChatroom = [aJson[@"delete_messages_as_exit_room"] boolValue];
    options.autoDownloadThumbnail = [aJson[@"is_auto_download"] boolValue];
    options.canChatroomOwnerLeave = [aJson[@"is_room_owner_leave_allowed"] boolValue];
    //    options.isAutoTransferMessageAttachments = [aJson[@"server_transfer"] boolValue];
    options.usingHttpsOnly = [aJson[@"using_https_only"] boolValue];
    options.apnsCertName = aJson[@"apns_cer_name"];
    options.enableDnsConfig = [aJson[@"enable_dns_config"] boolValue];
    options.chatPort = [aJson[@"im_port"] intValue];
    options.chatServer = aJson[@"im_server"];
    options.restServer = aJson[@"rest_server"];
    options.dnsURL = aJson[@"dns_url"];
    options.area = [aJson[@"area"] intValue];
    
    return options;
}
@end
