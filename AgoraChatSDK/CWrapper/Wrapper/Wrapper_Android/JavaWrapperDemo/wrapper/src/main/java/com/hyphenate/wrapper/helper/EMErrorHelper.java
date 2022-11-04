package com.hyphenate.wrapper.helper;

import org.json.JSONException;
import org.json.JSONObject;

public class EMErrorHelper {
    public static JSONObject toJson(int errorCode, String desc) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("code", errorCode);
        data.put("description", desc);
        return data;
    }
}
