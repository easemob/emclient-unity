package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Map;

public class EMWrapperConnectionListener implements EMConnectionListener {

    @Override
    public void onConnected() {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onConnected, null));
    }

    @Override
    public void onDisconnected(int errorCode) {
        if (errorCode == 216) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onChangedImPwd, null));
        } else if (errorCode == 214) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onLoginTooManyDevice, null));
        } else if (errorCode == 217) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onKickedByOtherDevice, null));
        } else if (errorCode == 202) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onAuthFailed, null));
        } else if (errorCode == 220 || errorCode == 206) {
//            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onLoggedOtherDevice, null));
        } else if (errorCode == 207) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onRemovedFromServer, null));
        } else if (errorCode == 305) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onForbidByServer, null));
        } else if (errorCode == 8) {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onAppActiveNumberReachLimitation, null));
        }  else {
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onDisconnected, null));
        }
    }


    @Override
    public void onTokenExpired() {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onTokenExpired, null));
    }

    @Override
    public void onTokenWillExpire() {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onTokenWillExpire, null));
    }

    public void onLogout(int code, String info) {
        if (code == 206 || code == 220) {
            try {
                JSONObject jo = new JSONObject();
                jo.put("deviceName", info);
                post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.connectionListener, EMSDKMethod.onLoggedOtherDevice, jo.toString()));
            }catch (JSONException ignored) {

            }
        }
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
