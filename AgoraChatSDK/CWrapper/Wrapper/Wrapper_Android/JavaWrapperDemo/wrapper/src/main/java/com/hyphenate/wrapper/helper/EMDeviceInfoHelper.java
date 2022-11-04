package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMDeviceInfo;

import org.json.JSONException;
import org.json.JSONObject;

public class EMDeviceInfoHelper {
    public static JSONObject toJson(EMDeviceInfo device) throws JSONException {
        if (device == null) return null;
        JSONObject data = new JSONObject();
        data.put("resource", device.getResource());
        data.put("deviceUUID", device.getDeviceUUID());
        data.put("deviceName", device.getDeviceName());

        return data;
    }
}
