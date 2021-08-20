package com.hyphenate.unity_chat_sdk.helper;

import android.content.Context;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMOptions;
import com.hyphenate.push.EMPushConfig;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMOptionsHelper {
    public static EMOptions fromJson(JSONObject json, Context context) throws JSONException {
        EMOptions options = new EMOptions();
        options.setAppKey(json.getString("app_key"));
        options.setAutoLogin(json.getBoolean("auto_login"));
        options.setRequireAck(json.getBoolean("require_ack"));
        options.setRequireDeliveryAck(json.getBoolean("require_delivery_ack"));
        options.setSortMessageByServerTime(json.getBoolean("sort_message_by_server_time"));
        options.setAcceptInvitationAlways(json.getBoolean("accept_invitation_always"));
        options.setAutoAcceptGroupInvitation(json.getBoolean("auto_accept_group_invitation"));
        options.setDeleteMessagesAsExitGroup(json.getBoolean("delete_messages_as_exit_group"));
        options.setDeleteMessagesAsExitChatRoom(json.getBoolean("delete_messages_as_exit_room"));
        options.setAutoDownloadThumbnail(json.getBoolean("is_auto_download"));
        options.allowChatroomOwnerLeave(json.getBoolean("is_room_owner_leave_allowed"));
        options.setUsingHttpsOnly(json.getBoolean("using_https_only"));

        if (json.has("enable_dns_config")) {
            if (!json.getBoolean("enable_dns_config")) {
                options.setImPort(json.getInt("im_port"));
                options.setIMServer(json.getString("im_server"));
                options.setRestServer(json.getString("rest_server"));
                options.setDnsUrl(json.getString("dns_url"));
            }
        }

        EMPushConfig.Builder builder = new EMPushConfig.Builder(context);
        if (json.getBoolean("enable_mi_push")) {
            builder.enableMiPush(json.getString("mi_app_id"), json.getString("mi_app_key"));
        }

        if (json.getBoolean("enable_fcm_push")) {
            builder.enableFCM(json.getString("fcm_id"));
        }
        if (json.getBoolean("enable_oppo_push")) {
            builder.enableOppoPush(json.getString("oppo_app_key"), json.getString("oppo_app_secret"));
        }
        if (json.getBoolean("enable_hw_push")) {
            builder.enableHWPush();
        }
        if (json.getBoolean("enable_mz_push")) {
            builder.enableMeiZuPush(json.getString("mz_app_id"), json.getString("mz_app_key"));
        }
        if (json.getBoolean("enable_vivo_push")) {
            builder.enableVivoPush();
        }

        options.setPushConfig(builder.build());

        return options;

    }

    public static JSONObject toJson(EMOptions options) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("app_key", options.getAppKey());
        data.put("auto_login", options.getAutoLogin());
        data.put("require_ack", options.getRequireAck());
        data.put("require_delivery_ack", options.getRequireDeliveryAck());
        data.put("sort_message_by_server_time", options.isSortMessageByServerTime());
        data.put("accept_invitation_always", options.getAcceptInvitationAlways());
        data.put("auto_accept_group_invitation", options.isAutoAcceptGroupInvitation());
        data.put("delete_messages_as_exit_group", options.isDeleteMessagesAsExitGroup());
        data.put("delete_messages_as_exit_room", options.isDeleteMessagesAsExitChatRoom());
        data.put("is_auto_download", options.getAutodownloadThumbnail());
        data.put("is_room_owner_leave_allowed", options.isChatroomOwnerLeaveAllowed());
//         data.put("serverTransfer", "");
//         data.put("debugModel", EMClient.getInstance().setDebugMode().);
//        data.put("push_config", EMPushConfigHelper.toJson(options.getPushConfig()));

        data.put("using_https_only", options.getUsingHttpsOnly());
        data.put("enable_dns_config", options.getEnableDNSConfig());
        data.put("im_port", options.getImPort());
        data.put("im_server", options.getImServer());
        data.put("rest_server", options.getRestServer());
        data.put("dns_url", options.getDnsUrl());

        return data;
    }
}
