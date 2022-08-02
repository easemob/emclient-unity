package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMLanguage;

import java.util.HashMap;
import java.util.Map;

public class EMLanguageHelper {
    public static Map<String, Object> toJson(EMLanguage language) {
        Map<String, Object> data = new HashMap<>();
        data.put("code", language.LanguageCode);
        data.put("name", language.LanguageName);
        data.put("nativeName", language.LanguageLocalName);
        return data;
    }
}
