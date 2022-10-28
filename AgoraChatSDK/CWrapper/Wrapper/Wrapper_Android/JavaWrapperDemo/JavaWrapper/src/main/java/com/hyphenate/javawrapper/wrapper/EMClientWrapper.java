package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.EMMultiDeviceListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMDeviceInfo;
import com.hyphenate.chat.EMOptions;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.channel.EMChannel;
import com.hyphenate.javawrapper.channel.EMChannelCallback;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMClientWrapper extends EMBaseWrapper {

    public EMContactManagerWrapper contactManagerWrapper;

    private EMMultiDeviceListener multiDeviceListener;

    private EMConnectionListener connectionListener;

    EMClientWrapper() {

    }

    @Override
    public String onMethodCall(String method, JSONObject jsonObject, EMChannelCallback callback) throws JSONException {
        if (method.equals("init")) {
            init(jsonObject, callback);
        }
        return null;
    }


    private void init(JSONObject param, EMChannelCallback callback) throws JSONException {
        EMOptions options = EMOptionsHelper.fromJson(param, EMChannel.context);
        EMClient.getInstance().init(EMChannel.context, options);
        EMClient.getInstance().setDebugMode(param.getBoolean("debugModel"));
        bindingManagers();
        registerEaseListener();
        callback.onSuccess(null);
    }

    private void registerEaseListener() {

        multiDeviceListener = new EMMultiDeviceListener() {
            @Override
            public void onContactEvent(int event, String target, String ext) {
                Map<String, Object> data = new HashMap<>();
                data.put("event", Integer.valueOf(event));
                data.put("target", target);
                data.put("ext", ext);
                post(()-> JavaWrapper.channel.callListener("MultiDeviceListener", "onContactEvent", data.toString()));
            }

            @Override
            public void onGroupEvent(int event, String target, List<String> userNames) {
                Map<String, Object> data = new HashMap<>();
                data.put("event", Integer.valueOf(event));
                data.put("target", target);
                data.put("users", userNames);
//                post(()-> channel.invokeMethod(EMSDKMethod.onMultiDeviceGroupEvent, data));
            }

            public void onChatThreadEvent(int event, String target, List<String> usernames) {
                Map<String, Object> data = new HashMap<>();
                data.put("event", Integer.valueOf(event));
                data.put("target", target);
                data.put("users", usernames);
//                post(()-> channel.invokeMethod(EMSDKMethod.onMultiDeviceThreadEvent, data));
            }
        };

        connectionListener = new EMConnectionListener() {
            @Override
            public void onConnected() {
                Map<String, Object> data = new HashMap<>();
                data.put("connected", Boolean.TRUE);
//                post(()-> channel.invokeMethod(EMSDKMethod.onConnected, data));
            }

            @Override
            public void onLogout(int errorCode) {

            }

            @Override
            public void onDisconnected(int errorCode) {
                if (errorCode == 206) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserDidLoginFromOtherDevice, null));
                }else if (errorCode == 207) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserDidRemoveFromServer, null));
                }else if (errorCode == 305) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserDidForbidByServer, null));
                }else if (errorCode == 216) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserDidChangePassword, null));
                }else if (errorCode == 214) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserDidLoginTooManyDevice, null));
                }
                else if (errorCode == 217) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserKickedByOtherDevice, null));
                }
                else if (errorCode == 202) {
//                    EMListenerHandle.getInstance().clearHandle();
//                    post(() -> channel.invokeMethod(EMSDKMethod.onUserAuthenticationFailed, null));
                }
                else {
//                    post(() -> channel.invokeMethod(EMSDKMethod.onDisconnected, null));
                }
            }

            @Override
            public void onTokenExpired() {
//                post(()-> channel.invokeMethod(EMSDKMethod.onTokenDidExpire, null));
            }

            @Override
            public void onTokenWillExpire() {
//                post(()-> channel.invokeMethod(EMSDKMethod.onTokenWillExpire, null));
            }
        };

        EMClient.getInstance().addConnectionListener(connectionListener);
        EMClient.getInstance().addMultiDeviceListener(multiDeviceListener);

    }

    private void bindingManagers() {
        contactManagerWrapper = new EMContactManagerWrapper();
    }
}
