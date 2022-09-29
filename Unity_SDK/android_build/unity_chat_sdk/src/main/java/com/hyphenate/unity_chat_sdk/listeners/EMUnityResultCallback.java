package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMError;
import com.hyphenate.EMResultCallBack;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMGroupHelper;
import com.hyphenate.unity_chat_sdk.helper.EMGroupInfoHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Map;

import util.EMSDKMethod;
import util.ImUnitySdkPlugin;

public class EMUnityResultCallback implements EMResultCallBack<Map<String, Integer>> {

    private String callbackId;
    private String valueType;

    public EMUnityResultCallback(String valueType, String callbackId) {
        this.callbackId = callbackId;
        this.valueType = valueType;
    }


    public void onSuccess(Map<String, Integer> stringMap) {
        Log.d("chat_sdk", "onSuccess callbackId --  " + callbackId );
        if (callbackId == null) return;
        ImUnitySdkPlugin.handler.post(()->{
            try {
                JSONObject jo = new JSONObject();
                jo.put("callbackId", callbackId);
                if (valueType == null) {
                    UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccess", jo.toString());
                    return;
                }

                for (Map.Entry<String, Integer> entry: stringMap.entrySet()) {
                    jo.put(entry.getKey(), entry.getValue());
                }

                Log.d("chat_sdk", "back: " + jo.toString());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccessValue", jo.toString());

            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    public void onError(HyphenateException errpr) {
        Log.d("chat_sdk", "onError callbackId -- " + callbackId + " code: " + errpr.getErrorCode() + " desc: " + errpr.getDescription());
        if (callbackId == null) return;
        ImUnitySdkPlugin.handler.post(()->{
            JSONObject jo = new JSONObject();
            try {
                jo.put("callbackId", callbackId);
                jo.put("code", errpr.getErrorCode());
                jo.put("desc", errpr.getDescription());
                Log.d("unity_sdk",jo.toString());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnError", jo.toString());
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    @Override
    public void onResult(int i, Map<String, Integer> stringIntegerMap) {
        if (stringIntegerMap.size() > 0 || i == EMError.EM_NO_ERROR) {
                onSuccess(stringIntegerMap);
        }else {
            HyphenateException e = new HyphenateException(i, "");
            onError(e);
        }
    }
}
