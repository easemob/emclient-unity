package com.hyphenate.unity_chat_sdk.listeners;

import com.hyphenate.EMMultiDeviceListener;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

import util.EMSDKMethod;

public class EMUnityMultiDevicesListener implements EMMultiDeviceListener {
    @Override
    public void onContactEvent(int i, String s, String s1) {
        JSONObject data = new JSONObject();
        try {
            data.put("event", i);
            data.put("username", s);
            data.put("ext", s1);
            UnityPlayer.UnitySendMessage(EMSDKMethod.MultiDevice_Obj, "OnContactMultiDevicesEvent", data.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onGroupEvent(int i, String s, List<String> list) {
        JSONObject data = new JSONObject();
        try {
            data.put("usernames", EMTransformHelper.jsonArrayFromStringList(list).toString());
            data.put("groupId", s);
            data.put("event", i);
            UnityPlayer.UnitySendMessage(EMSDKMethod.MultiDevice_Obj, "OnGroupMultiDevicesEvent", data.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }
}
