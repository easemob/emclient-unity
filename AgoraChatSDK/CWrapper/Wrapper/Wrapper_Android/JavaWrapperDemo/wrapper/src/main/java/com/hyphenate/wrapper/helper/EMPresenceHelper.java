package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMPresence;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Map;

public class EMPresenceHelper {

    public static JSONObject toJson(EMPresence presence) throws JSONException {
        if (presence == null) return null;
        JSONObject data = new JSONObject();
        data.put("publisher", presence.getPublisher());
        data.put("desc", presence.getExt());
        data.put("lastTime", presence.getLatestTime());
        data.put("expiryTime", presence.getExpiryTime());
        JSONArray ja = new JSONArray();
        for (Map.Entry<String, Integer> entry: presence.getStatusList().entrySet()) {
            JSONObject jo = new JSONObject();
            jo.put("device", entry.getKey());
            jo.put("status", entry.getValue());
            ja.put(jo);
        }
        data.put("detail", ja);

        return data;
    }

}
