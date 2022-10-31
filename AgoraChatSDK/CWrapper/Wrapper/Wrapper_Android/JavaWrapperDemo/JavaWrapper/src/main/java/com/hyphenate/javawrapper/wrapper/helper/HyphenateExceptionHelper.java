package com.hyphenate.javawrapper.wrapper.helper;


import com.hyphenate.exceptions.HyphenateException;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class HyphenateExceptionHelper {
    public static JSONObject toJson(HyphenateException e) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("code", e.getErrorCode());
        data.put("description", e.getDescription());
        return data;
    }
}

