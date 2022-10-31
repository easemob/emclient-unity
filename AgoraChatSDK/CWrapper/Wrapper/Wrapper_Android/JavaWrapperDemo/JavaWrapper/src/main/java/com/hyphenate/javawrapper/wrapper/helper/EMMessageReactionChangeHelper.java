package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMMessageReactionChange;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class EMMessageReactionChangeHelper {
    public static JSONObject toJson(EMMessageReactionChange change) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("conversationId", change.getConversionID());
        data.put("messageId", change.getMessageId());
        JSONArray list = new JSONArray();
        for (int i = 0; i < change.getMessageReactionList().size(); i++) {
            list.put(EMMessageReactionHelper.toJson(change.getMessageReactionList().get(i)));
        }
        data.put("reactions", list);

        return data;
    }
}

