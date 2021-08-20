package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMValueCallBack;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
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

public class EMUnityValueCallback<T> implements EMValueCallBack<T> {

    private String callbackId;
    private String valueType;

    public EMUnityValueCallback(String valueType, String callbackId) {
        this.callbackId = callbackId;
        this.valueType = valueType;
    }

    public void sendEmptyCallback() {
        JSONObject jsonObject = new JSONObject();
        try{
            jsonObject.put("callbackId", callbackId);
            UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccess", jsonObject.toString());
        }catch (JSONException e) {

        }
    }

    public void sendJsonObjectToUnity (String jsonString) {
        Log.d("chat_sdk", "getObject callbackId --  " + callbackId + " jsonString: "  + jsonString);
        if (callbackId == null) return;
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("type",valueType);
            jsonObject.put("value", jsonString);
            jsonObject.put("callbackId", callbackId);
            Log.d("chat_sdk", "back: " + jsonObject.toString());
            UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccessValue", jsonObject.toString());
        }catch (JSONException e) {

        }
    }

    @Override
    public void onSuccess(T t) {
        Log.d("chat_sdk", "onSuccess callbackId --  " + callbackId );
        if (callbackId == null) return;
        ImUnitySdkPlugin.handler.post(()->{
            try {
                JSONObject jo = new JSONObject();
                jo.put("callbackId", callbackId);
                if (valueType == null && valueType == null) {
                    UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccess", jo.toString());
                    return;
                }else if (t instanceof List) {
                    JSONArray jsonAry = new JSONArray();
                    for (Object obj: (List) t) {
                        if (obj instanceof String) {
                            jsonAry.put((String) obj);
                        } else if (obj instanceof EMGroup) {
                            JSONObject jsonObject = EMGroupHelper.toJson((EMGroup) obj);
                            jsonAry.put(jsonObject.toString());
                        } else if (obj instanceof EMGroupInfo) {
                            JSONObject jsonObject = EMGroupInfoHelper.toJson((EMGroupInfo) obj);
                            jsonAry.put(jsonObject.toString());
                        }
                    }
                    jo.put("type",valueType);
                    jo.put("value",jsonAry.toString());
                }else if(t instanceof EMCursorResult) {
                }

                Log.d("chat_sdk", "back: " + jo.toString());
                UnityPlayer.UnitySendMessage(EMSDKMethod.Callback_Obj, "OnSuccessValue", jo.toString());

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
}
