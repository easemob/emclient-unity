package com.hyphenate.unity_chat_sdk.helper;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMErrorHelper {
    public static JSONObject toJson(int errorCode, String desc) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("code", errorCode);
        data.put("description", desc);
        return data;
    }
}
