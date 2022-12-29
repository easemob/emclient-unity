package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMMultiDeviceListener;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMWrapperMultiDeviceListener implements EMMultiDeviceListener {
    public void onContactEvent(int event, String target, String ext) {
        JSONObject data = new JSONObject();
        try {
            data.put("operation", Integer.valueOf(event));
            data.put("target", target);
            data.put("ext", ext);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.multiDeviceListener, EMSDKMethod.onContactMultiDevicesEvent, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onGroupEvent(int event, String target, List<String> userNames) {
        JSONObject data = new JSONObject();
        try {
            data.put("operation", Integer.valueOf(event));
            data.put("target", target);
            data.put("userIds", userNames);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.multiDeviceListener, EMSDKMethod.onGroupMultiDevicesEvent, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void onChatThreadEvent(int event, String target, List<String> usernames) {
        JSONObject data = new JSONObject();
        try {
            data.put("operation", Integer.valueOf(event));
            data.put("target", target);
            data.put("userIds", usernames);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.multiDeviceListener, EMSDKMethod.onThreadMultiDevicesEvent, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}