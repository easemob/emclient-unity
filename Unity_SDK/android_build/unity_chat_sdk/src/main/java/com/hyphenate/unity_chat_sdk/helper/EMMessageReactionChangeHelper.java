package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMessageReactionChange;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class EMMessageReactionChangeHelper {
    public static  Map<String, Object> toJson(EMMessageReactionChange change) {
        Map<String, Object> data = new HashMap<>();
        data.put("conversationId", change.getConversionID());
        data.put("messageId", change.getMessageId());
        ArrayList<Map<String, Object>> list = new ArrayList<>();
        for (int i = 0; i < change.getMessageReactionList().size(); i++) {
            list.add(EMMessageReactionHelper.toJson(change.getMessageReactionList().get(i)));
        }
        data.put("reactions", list);

        return data;
    }
}