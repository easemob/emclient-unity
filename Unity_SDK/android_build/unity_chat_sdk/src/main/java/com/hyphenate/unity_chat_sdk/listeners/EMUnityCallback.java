package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMCallBack;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import util.EMSDKMethod;
import util.ImUnitySdkPlugin;

public class EMUnityCallback implements EMCallBack {


    private String callbackId;

    public EMUnityCallback(String callbackId) {
        this.callbackId = callbackId;
    }

    @Override
    public void onSuccess() {
        Log.d("chat_sdk", "onSuccess callbackId -- " + callbackId);
        if (callbackId == null) return;
        ImUnitySdkPlugin.handler.post(()->{
            JSONObject jo = new JSONObject();
            try {
                jo.put("callbackId", callbackId);
                Log.d("unity_sdk",jo.toString());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccess", jo.toString());
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    @Override
    public void onError(int i, String s) {
        Log.d("chat_sdk", "onError callbackId -- " + callbackId + " code: " + i + " desc: " + s);
        if (callbackId == null) return;
        ImUnitySdkPlugin.handler.post(()->{
            JSONObject jo = new JSONObject();
            try {
                jo.put("callbackId", callbackId);
                jo.put("code", i);
                jo.put("desc", s);
                Log.d("unity_sdk",jo.toString());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnError", jo.toString());
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    @Override
    public void onProgress(int i, String s) {
        Log.d("chat_sdk", "onProgress callbackId -- " + callbackId + " progress: " + i);
        if (callbackId == null) return;
        ImUnitySdkPlugin.handler.post(()->{
            JSONObject jo = new JSONObject();
            try {
                jo.put("callback", callbackId);
                jo.put("progress", i);
                Log.d("unity_sdk",jo.toString());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnProgress", jo.toString());
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }
}
