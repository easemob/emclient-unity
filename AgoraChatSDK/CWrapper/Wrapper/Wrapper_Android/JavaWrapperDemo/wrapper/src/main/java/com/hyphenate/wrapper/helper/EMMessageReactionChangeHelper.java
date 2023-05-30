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
        JSONArray reactions = new JSONArray();
        for (int i = 0; i < change.getMessageReactionList().size(); i++) {
            reactions.put(EMMessageReactionHelper.toJson(change.getMessageReactionList().get(i)));
        }
        data.put("reactions", reactions);
        JSONArray operations = new JSONArray();
        for (int i = 0; i < change.getOperations().size(); i++) {
            operations.put(EMMessageReactionOperationHelper.toJson(change.getOperations().get(i)));
        }
        data.put("operations", operations);
        return data;
    }
}

