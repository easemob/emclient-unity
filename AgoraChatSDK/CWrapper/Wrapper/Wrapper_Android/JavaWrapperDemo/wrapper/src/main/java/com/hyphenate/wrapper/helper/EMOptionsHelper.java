package com.hyphenate.wrapper.helper;

import android.content.Context;

import com.hyphenate.chat.EMOptions;
import com.hyphenate.push.EMPushConfig;

import org.json.JSONException;
import org.json.JSONObject;

public class EMOptionsHelper {
    public static EMOptions fromJson(JSONObject json, Context context) throws JSONException {
        EMOptions options = new EMOptions();
        options.setAppKey(json.getString("appKey"));
        options.setAutoLogin(json.getBoolean("autoLogin"));
        options.setRequireAck(json.getBoolean("requireAck"));
        options.setRequireDeliveryAck(json.getBoolean("requireDeliveryAck"));
        options.setSortMessageByServerTime(json.getBoolean("sortMessageByServerTime"));
        options.setAcceptInvitationAlways(json.getBoolean("acceptInvitationAlways"));
        options.setAutoAcceptGroupInvitation(json.getBoolean("autoAcceptGroupInvitation"));
        options.setDeleteMessagesAsExitGroup(json.getBoolean("deleteMessagesAsExitGroup"));
        options.setDeleteMessagesAsExitChatRoom(json.getBoolean("deleteMessagesAsExitRoom"));
        options.setAutoDownloadThumbnail(json.getBoolean("isAutoDownload"));
        options.allowChatroomOwnerLeave(json.getBoolean("isRoomOwnerLeaveAllowed"));
        options.setAutoTransferMessageAttachments(json.getBoolean("serverTransfer"));
        options.setAreaCode(json.getInt("areaCode"));
        options.setUsingHttpsOnly(json.getBoolean("usingHttpsOnly"));
        options.enableDNSConfig(json.getBoolean("enableDnsConfig"));
        if (!json.getBoolean("enableDnsConfig")) {
            if (json.has("imPort")) {
                options.setImPort(json.getInt("imPort"));
            }
            if (json.has("imServer")) {
                options.setIMServer(json.getString("imServer"));
            }
            if (json.has("restServer")) {
                options.setRestServer(json.getString("restServer"));
            }
            if (json.has("dnsUrl")){
                options.setDnsUrl(json.getString("dnsUrl"));
            }
        }
        if (json.has("enableEmptyConversation")) {
            options.setLoadEmptyConversations(json.optBoolean("enableEmptyConversation"));
        }
        if (json.has("useReplacedMessageContents")) {
            options.setUseReplacedMessageContents(json.optBoolean("useReplacedMessageContents"));
        }
        if (json.has("deviceName")) {
            options.setCustomDeviceName(json.optString("deviceName"));
        }
        if (json.has("osType")) {
            options.setCustomOSPlatform(json.optInt("osType"));
        }
        if (json.has("regardImportMsgAsRead")) {
            options.setRegardImportedMsgAsRead(json.optBoolean("regardImportMsgAsRead"));
        }
        if (json.has("includeSendMessageInMessageListener")) {
            options.setIncludeSendMessageInMessageListener(json.optBoolean("includeSendMessageInMessageListener"));
        }

        if (json.has("pushConfig")) {
            EMPushConfig.Builder builder = new EMPushConfig.Builder(context);
            JSONObject pushConfig = json.getJSONObject("pushConfig");
            if (pushConfig.getBoolean("enableMiPush")) {
                builder.enableMiPush(pushConfig.getString("miAppId"), pushConfig.getString("miAppKey"));
            }
            if (pushConfig.getBoolean("enableFCM")) {
                builder.enableFCM(pushConfig.getString("fcmId"));
            }
            if (pushConfig.getBoolean("enableOppoPush")) {
                builder.enableOppoPush(pushConfig.getString("oppoAppKey"), pushConfig.getString("oppoAppSecret"));
            }
            if (pushConfig.getBoolean("enableHWPush")) {
                builder.enableHWPush();
            }
            if (pushConfig.getBoolean("enableMeiZuPush")) {
                builder.enableMeiZuPush(pushConfig.getString("mzAppId"), pushConfig.getString("mzAppKey"));
            }
            if (pushConfig.getBoolean("enableVivoPush")) {
                builder.enableVivoPush();
            }
            options.setPushConfig(builder.build());
        }
        return options;
    }
}
