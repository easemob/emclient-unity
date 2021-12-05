package com.hyphenate.unity_chat_sdk;

import android.util.Log;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMUserInfo;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.hyphenate.unity_chat_sdk.helper.EMUserInfoHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Map;

public class EMUserInfoManagerWrapper extends EMWrapper  {
    static public EMUserInfoManagerWrapper wrapper() {
        return new EMUserInfoManagerWrapper();
    }

    private void fetchUserInfoByUserId(String jsonString, String callbackId) throws JSONException {

        String[] userIds = EMTransformHelper.jsonStringToStringArray(jsonString);
        EMUnityValueCallback<Map<String, EMUserInfo>> valueCallback = new EMUnityValueCallback<Map<String, EMUserInfo>>("Map<String, UserInfo>",callbackId){
            @Override
            public void onSuccess(Map<String, EMUserInfo> stringEMUserInfoMap) {
                JSONArray jsonArray = new JSONArray();
                for (Map.Entry<String, EMUserInfo> entry : stringEMUserInfoMap.entrySet()) {
                    JSONObject jo = new JSONObject();
                    try {
                        jo.put(entry.getKey(), EMUserInfoHelper.toJson(entry.getValue()).toString());
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                    jsonArray.put(jo);
                }
                super.sendJsonObjectToUnity(jsonArray.toString());
            }
        };
        EMClient.getInstance().userInfoManager().fetchUserInfoByUserId(userIds, valueCallback);
    }


    private void updateOwnInfo(String jsonString, String callbackId) throws  JSONException{

        Log.d("unity_sdk","will upOwnInfo: " + jsonString);
        if (jsonString == null || jsonString.length() == 0) {
            onError(callbackId, new HyphenateException(501, "userinfo contains invalid content"));
            return ;
        }
        EMUserInfo userInfo = EMUserInfoHelper.fromJson(new JSONObject(jsonString));


        EMUnityValueCallback<String> valueCallback = new EMUnityValueCallback<String>("String",callbackId){
            @Override
            public void onSuccess(String s) {
                super.sendEmptyCallback();
            }
        };

        EMClient.getInstance().userInfoManager().updateOwnInfo(userInfo,valueCallback);
    }

}
