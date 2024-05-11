package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMContact;

import org.json.JSONException;
import org.json.JSONObject;

public class EMContactHelper {
    public static JSONObject toJson(EMContact contact) throws JSONException {
        if (contact == null) return null;
        JSONObject data = new JSONObject();
        data.put("userId", contact.getUsername());
        data.put("remark", contact.getRemark());
        return data;
    }
}
