package com.hyphenate.wrapper.helper;

import org.json.JSONException;
import org.json.JSONObject;

public class EMErrorHelper {
    public static JSONObject toJson(int errorCode, String desc) {
        JSONObject data = new JSONObject();
        try{
            data.put("code", errorCode);
            data.put("desc", desc);
        }catch (JSONException e) {
            e.printStackTrace();
        }
        return data;
    }
}
