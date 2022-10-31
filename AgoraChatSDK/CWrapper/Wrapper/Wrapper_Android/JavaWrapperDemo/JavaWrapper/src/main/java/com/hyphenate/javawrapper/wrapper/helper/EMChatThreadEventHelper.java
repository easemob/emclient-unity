package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMChatThreadEvent;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMChatThreadEventHelper {
    public static JSONObject toJson(EMChatThreadEvent event) throws JSONException {
        JSONObject data = new JSONObject();
        switch (event.getType()) {
            case UNKNOWN:
                data.put("type", 0);
                break;
            case CREATE:
                data.put("type", 1);
                break;
            case UPDATE:
                data.put("type", 2);
                break;
            case DELETE:
                data.put("type", 3);
                break;
            case UPDATE_MSG:
                data.put("type", 4);
                break;
        }
        data.put("from", event.getFrom());
        if (event.getChatThread() != null) {
            data.put("thread", EMChatThreadHelper.toJson(event.getChatThread()));
        }
        return data;
    }
}
