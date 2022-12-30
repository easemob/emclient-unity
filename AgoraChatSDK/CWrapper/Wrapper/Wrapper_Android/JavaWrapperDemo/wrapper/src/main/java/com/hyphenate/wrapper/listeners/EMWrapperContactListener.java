package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMContactListener;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONException;
import org.json.JSONObject;

public class EMWrapperContactListener implements EMContactListener {
    @Override
    public void onContactAdded(String userName) {
        JSONObject data = new JSONObject();
        try {
            data.put("userId", userName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.contactListener, EMSDKMethod.onContactAdded, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onContactDeleted(String userName) {
        JSONObject data = new JSONObject();
        try {
            data.put("userId", userName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.contactListener, EMSDKMethod.onContactDeleted, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onContactInvited(String userName, String reason) {
        JSONObject data = new JSONObject();
        try {
            data.put("userId", userName);
            data.put("msg", reason);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.contactListener, EMSDKMethod.onContactInvited, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onFriendRequestAccepted(String userName) {
        JSONObject data = new JSONObject();
        try {
            data.put("userId", userName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.contactListener, EMSDKMethod.onFriendRequestAccepted, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onFriendRequestDeclined(String userName) {
        JSONObject data = new JSONObject();
        try {
            data.put("userId", userName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.contactListener, EMSDKMethod.onFriendRequestDeclined, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
