package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMChatThreadChangeListener;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.helper.EMChatThreadEventHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONException;
import org.json.JSONObject;

public class EMWrapperThreadListener implements EMChatThreadChangeListener {
    @Override
    public void onChatThreadCreated(EMChatThreadEvent event) {
        try {
            JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onChatThreadCreate, jsonObject.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onChatThreadUpdated(EMChatThreadEvent event) {
        try {
            JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onChatThreadUpdate, jsonObject.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onChatThreadDestroyed(EMChatThreadEvent event) {
        try {
            JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onChatThreadDestroy, jsonObject.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onChatThreadUserRemoved(EMChatThreadEvent event) {
        try {
            JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onUserKickOutOfChatThread, jsonObject.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void onTestChatThreadCreated(JSONObject jsonObject) {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onChatThreadCreate, jsonObject.toString()));
    }


    public void onTestChatThreadUpdated(JSONObject jsonObject) {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onChatThreadUpdate, jsonObject.toString()));
    }


    public void onTestChatThreadDestroyed(JSONObject jsonObject) {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onChatThreadDestroy, jsonObject.toString()));
    }


    public void onTestChatThreadUserRemoved(JSONObject jsonObject) {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatThreadListener, EMSDKMethod.onUserKickOutOfChatThread, jsonObject.toString()));
    }


    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
