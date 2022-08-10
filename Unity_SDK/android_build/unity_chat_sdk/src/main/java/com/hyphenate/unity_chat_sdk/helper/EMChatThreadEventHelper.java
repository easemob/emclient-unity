package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatThreadEvent;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMChatThreadEventHelper {
    public static JSONObject toJson(EMChatThreadEvent event) throws  JSONException{
        JSONObject data = new JSONObject();
        switch (event.getType()) {
            case UNKNOWN:
                data.put("operation", 0);
                break;
            case CREATE:
                data.put("operation", 1);
                break;
            case UPDATE:
                data.put("operation", 2);
                break;
            case DELETE:
                data.put("operation", 3);
                break;
            case UPDATE_MSG:
                data.put("operation", 4);
                break;
        }
        data.put("from", event.getFrom());
        if (event.getChatThread() != null) {
            try {
                data.put("chatThread", EMChatThreadHelper.toJson(event.getChatThread()));
            }catch (JSONException ignored) {}

        }
        return data;
    }
}
