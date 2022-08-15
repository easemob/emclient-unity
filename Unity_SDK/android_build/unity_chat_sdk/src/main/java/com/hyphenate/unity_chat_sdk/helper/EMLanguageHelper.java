package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMLanguage;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMLanguageHelper {
    public static JSONObject toJson(EMLanguage language) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("code", language.LanguageCode);
        data.put("name", language.LanguageName);
        data.put("nativeName", language.LanguageLocalName);
        return data;
    }
}
