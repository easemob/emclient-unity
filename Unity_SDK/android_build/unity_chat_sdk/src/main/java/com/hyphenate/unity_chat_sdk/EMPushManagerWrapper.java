package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMPushConfigs;
import com.hyphenate.chat.EMPushManager;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMPushConfigHelper;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;

import org.json.JSONException;

import java.util.ArrayList;
import java.util.List;

public class EMPushManagerWrapper extends EMWrapper {
    static public EMPushManagerWrapper wrapper() {
        return new EMPushManagerWrapper();
    }

    private String getNoDisturbGroups(String callbackId) {
        List<String> groupIds = EMClient.getInstance().pushManager().getNoPushGroups();
        if (groupIds == null) return null;
        return EMTransformHelper.stringListToJsonArray(groupIds).toString();
    }

    private String getPushConfig() throws  JSONException{
        EMPushConfigs configs = EMClient.getInstance().pushManager().getPushConfigs();
        if (configs == null) return null;

        return EMPushConfigHelper.toJson(configs).toString();
    }

    private void getImPushConfigFromServer(String callbackId) {
        asyncRunnable(()->{
            try {
                EMPushConfigs configs = EMClient.getInstance().pushManager().getPushConfigsFromServer();
                onSuccess("EMPushConfigs", callbackId, EMPushConfigHelper.toJson(configs).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {}
        });
    }

    private void updatePushNickname(String nickname, String callbackId) {
        asyncRunnable(()->{
            try {
                EMClient.getInstance().pushManager().updatePushNickname(nickname);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void imPushNoDisturb(boolean noDisturb, int startTime, int endTime,  String callbackId) {
        asyncRunnable(()-> {
            try {
                if (noDisturb) {
                    EMClient.getInstance().pushManager().disableOfflinePush(startTime, endTime);
                }else{
                    EMClient.getInstance().pushManager().enableOfflinePush();
                }
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //
    private void updateImPushStyle(int pushStyle,  String callbackId) {
        EMPushManager.DisplayStyle style = pushStyle == 0 ? EMPushManager.DisplayStyle.SimpleBanner : EMPushManager.DisplayStyle.MessageSummary;
        EMClient.getInstance().pushManager().asyncUpdatePushDisplayStyle(style, new EMUnityCallback(callbackId));
    }
    //
    private void updateGroupPushService(String groupId, boolean noDisturb,  String callbackId) {

        List<String> groupList = new ArrayList<>();
        groupList.add(groupId);
        asyncRunnable(()-> {
            try {
                EMClient.getInstance().pushManager().updatePushServiceForGroup(groupList, noDisturb);
                onSuccess(null, callbackId, null);
            } catch(HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //

    //
    private void updateHMSPushToken(String token, String callbackId) {
        asyncRunnable(()->{
            EMClient.getInstance().sendHMSPushTokenToServer(token);
            onSuccess(null, callbackId, null);
        });
    }
    //
    private void updateFCMPushToken(String token, String callbackId) {
        asyncRunnable(()->{
            EMClient.getInstance().sendFCMTokenToServer(token);
            onSuccess(null, callbackId, null);
        });
    }

    private void updateAPNsDeviceToken(String nickname, String callbackId) {
        asyncRunnable(()->{
            onSuccess(null, callbackId, null);
        });
    }
}
