package com.hyphenate.unity_chat_sdk;

import android.util.Log;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.exceptions.HyphenateException;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.List;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import util.EMSDKMethod;
import util.ImUnitySdkPlugin;

public class EMWrapper {

    private final ExecutorService cachedThreadPool = Executors.newCachedThreadPool();

    public EMWrapper(){
    }

    public void post(Runnable runnable) {
        ImUnitySdkPlugin.handler.post(runnable);
    }

    public void asyncRunnable(Runnable runnable) {
        cachedThreadPool.execute(runnable);
    }

    public void onSuccess(String callbackId,  String type, String obj) {
        Log.d("chat_sdk", "callbackId -- " + callbackId + " type: " + type + " obj: " + obj);
        post(()-> {
            try {
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("callbackId", callbackId);

                if (obj == null && type == null) {
                    UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccess", jsonObject.toString());
                    return;
                }else {
                    jsonObject.put("type", type);
                    jsonObject.put("value", (obj  == null) ? "" : obj);
                    UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccessValue", jsonObject.toString());
                }
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    public void onError(String callbackId, HyphenateException e) {
        post(()-> {
            JSONObject jsonObject = new JSONObject();
            try {
                jsonObject.put("code", e.getErrorCode());
                jsonObject.put("desc", e.getDescription());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnError", jsonObject.toString());
            } catch (JSONException jsonException) {
                jsonException.printStackTrace();
            }
        });
    }
}
