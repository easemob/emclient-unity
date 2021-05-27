package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMDeviceInfo;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMDeviceInfoHelper {

    public static JSONObject toJson(EMDeviceInfo device) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("resource", device.getResource());
        data.put("deviceUUID", device.getDeviceUUID());
        data.put("deviceName", device.getDeviceName());
        return data;
    }
}
