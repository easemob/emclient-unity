package com.hyphenate.unity_chat_sdk.helper;

import java.util.HashMap;
import java.util.Map;

public class EMErrorHelper {
    public static Map<String, Object> toJson(int errorCode, String desc) {
        Map<String, Object> data = new HashMap<>();
        data.put("code", errorCode);
        data.put("description", desc);
        return data;
    }
}
