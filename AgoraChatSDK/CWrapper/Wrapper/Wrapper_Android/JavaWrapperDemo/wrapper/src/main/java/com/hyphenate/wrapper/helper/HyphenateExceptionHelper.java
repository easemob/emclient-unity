package com.hyphenate.wrapper.helper;


import com.hyphenate.exceptions.HyphenateException;

import org.json.JSONException;
import org.json.JSONObject;

public class HyphenateExceptionHelper {
    public static JSONObject toJson(HyphenateException e) throws JSONException {
        if (e == null) return null;
        JSONObject data = new JSONObject();
        data.put("code", e.getErrorCode());
        data.put("description", e.getDescription());
        return data;
    }
}

