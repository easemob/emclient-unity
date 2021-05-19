package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMDeviceInfo;

import java.util.HashMap;
import java.util.Map;

public class EMDeviceInfoHelper {

    public static Map<String, Object> toJson(EMDeviceInfo device) {
        Map<String, Object> data = new HashMap<>();
        data.put("resource", device.getResource());
        data.put("deviceUUID", device.getDeviceUUID());
        data.put("deviceName", device.getDeviceName());

        return data;
    }
}
