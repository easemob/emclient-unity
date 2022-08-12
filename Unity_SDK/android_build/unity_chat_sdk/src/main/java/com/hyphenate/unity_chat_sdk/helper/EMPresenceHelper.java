package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMPresence;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

public class EMPresenceHelper {
    public static JSONObject toJson(EMPresence presence) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("publisher", presence.getPublisher());
        data.put("statusDescription", presence.getExt());
        data.put("lastTime", presence.getLatestTime());
        data.put("expiryTime", presence.getExpiryTime());
        JSONObject jsonObject = new JSONObject();
        Iterator<String> iterator = presence.getStatusList().keySet().iterator();
        while (iterator.hasNext()) {
            String key = iterator.next();
            jsonObject.put(key, presence.getStatusList().get(key));
        }

        data.put("statusDetails", jsonObject);
        return data;
    }
}
