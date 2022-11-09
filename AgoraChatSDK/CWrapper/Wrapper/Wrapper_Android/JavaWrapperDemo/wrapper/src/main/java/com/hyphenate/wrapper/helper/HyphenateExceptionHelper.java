package com.hyphenate.wrapper.helper;


import com.hyphenate.exceptions.HyphenateException;

import org.json.JSONException;
import org.json.JSONObject;

public class HyphenateExceptionHelper {
    public static JSONObject toJson(HyphenateException e){
        if (e == null) return null;
        JSONObject data = new JSONObject();
        try {
            data.put("code", e.getErrorCode());
            data.put("desc", e.getDescription());
        } catch (JSONException jsonError) {
            jsonError.printStackTrace();
        }
        return data;
    }
}

