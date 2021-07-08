package com.hyphenate.unity_chat_sdk;

import android.content.Context;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMOptionsHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityConnectionListener;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Method;

import util.EMSDKMethod;

public class EMClientWrapper extends EMWrapper {

    static public EMClientWrapper wrapper() {
        return new EMClientWrapper();
    }

    public void init(String options) throws JSONException {
        Context context = UnityPlayer.currentActivity.getApplicationContext();
        JSONObject jo = new JSONObject(options);
        EMClient.getInstance().init(context, EMOptionsHelper.fromJson(jo, context));
        EMClient.getInstance().setDebugMode(jo.getBoolean("debug_mode"));
        EMClient.getInstance().addConnectionListener(new EMUnityConnectionListener());
    }

    public void createAccount(String username, String password, String callbackId) {

        if (username == null || username.length() == 0 ) {
            HyphenateException e = new HyphenateException(101, "Username is invalid");
            onError(callbackId, e);
            return;
        }

        if (password == null || password.length() == 0) {
            HyphenateException e = new HyphenateException(102, "password is invalid");
            onError(callbackId, e);
            return;
        }


        asyncRunnable(()->{
            try {
                EMClient.getInstance().createAccount(username, password);
                onSuccess(null, callbackId,null);
            }catch (HyphenateException e) {
                onError( callbackId, e);
            }
        });
    }

    public void login(String username, String pwdOrToken, boolean isToken, String callbackId) {

        if (username == null || username.length() == 0 ) {
            HyphenateException e = new HyphenateException(101, "Username is invalid");
            onError(callbackId, e);
            return;
        }

        if (pwdOrToken == null || pwdOrToken.length() == 0) {
            HyphenateException e = new HyphenateException(102, "password or token is invalid");
            onError(callbackId, e);
            return;
        }

        if (!isToken) {
            EMClient.getInstance().login(username, pwdOrToken, new EMUnityCallback(callbackId));
        }else {
            EMClient.getInstance().loginWithToken(username, pwdOrToken, new EMUnityCallback(callbackId));
        }
    }

    public void logout(boolean unbindDeviceToken, String callbackId) {
        EMClient.getInstance().logout(unbindDeviceToken, new EMUnityCallback(callbackId));
    }

    public String currentUsername() {
        return EMClient.getInstance().getCurrentUser();
    }

    public boolean isConnected (){
        return EMClient.getInstance().isConnected();
    }

    public boolean isLoggedIn() {
        return EMClient.getInstance().isLoggedInBefore();
    }

    public String accessToken() {
        return EMClient.getInstance().getAccessToken();
    }
}
