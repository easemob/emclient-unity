package com.hyphenate.javawrapper.channel;

import android.content.Context;
import android.icu.text.SymbolTable;

import com.hyphenate.javawrapper.wrapper.EMWrapper;
import com.hyphenate.javawrapper.wrapper.EMWrapperListener;

import org.json.JSONException;
import org.json.JSONObject;

public abstract class EMChannel {
    long nativeListener = 0;
    public static Context context;

    EMWrapper wrapper;

    public EMChannel(long listener) {
        this.nativeListener = listener;
        wrapper = new EMWrapper();
    }

    public abstract void nativeCall(String manager, String method, String jsonString, String cid);

    public abstract String nativeGet(String manager, String method, String jsonString, String cid);

    public native void callListener(String manager, String method, String jsonString);

    public String callSDKApi(String manager, String method, String jsonString, EMChannelCallback callback) {
        try {
            JSONObject jsonObject = new JSONObject(jsonString);
            if (manager.equals("EMClient")) {
                wrapper.clientWrapper.onMethodCall(method, jsonObject, callback);
            }else if (manager.equals("EMContactManager")) {
                wrapper.clientWrapper.contactManagerWrapper.onMethodCall(method,jsonObject, callback);
            }
        } catch (JSONException e){
            callback.onError("json error");
        }
        return "";
    }
}