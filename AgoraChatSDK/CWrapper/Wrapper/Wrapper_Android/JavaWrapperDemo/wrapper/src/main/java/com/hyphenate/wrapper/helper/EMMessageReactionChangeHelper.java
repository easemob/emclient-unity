package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMMessageReactionChange;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class EMMessageReactionChangeHelper {
    public static JSONObject toJson(EMMessageReactionChange change) throws JSONException {
        if (change == null) return null;
        JSONObject data = new JSONObject();
        data.put("convId", change.getConversionID());
        data.put("msgId", change.getMessageId());
        JSONArray list = new JSONArray();
        for (int i = 0; i < change.getMessageReactionList().size(); i++) {
            list.put(EMMessageReactionHelper.toJson(change.getMessageReactionList().get(i)));
        }
        data.put("reactions", list);

        return data;
    }
}

