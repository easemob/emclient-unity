package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMessageReactionChange;

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
        ArrayList<JSONObject> list = new ArrayList<>();
        for (int i = 0; i < change.getMessageReactionList().size(); i++) {
            list.add(EMMessageReactionHelper.toJson(change.getMessageReactionList().get(i)));
        }
        data.put("reactions", list);

        return data;
    }
}