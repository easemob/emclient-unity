package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMLanguage;

import org.json.JSONException;
import org.json.JSONObject;

public class EMLanguageHelper {
    public static JSONObject toJson(EMLanguage language) throws JSONException {
        if (language == null) return null;
        JSONObject data = new JSONObject();
        data.put("code", language.LanguageCode);
        data.put("name", language.LanguageName);
        data.put("nativeName", language.LanguageLocalName);
        return data;
    }
}
